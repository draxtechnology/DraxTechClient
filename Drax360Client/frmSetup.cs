using CryptoModule;
using DraxClient.Panels.Email;
using DraxClient.Panels.RSM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DraxClient.frmTestBox;
using static System.Windows.Forms.DataFormats;

namespace DraxClient
{
    public partial class frmSetup : Form
    {
        const string kpipenamesend = "DraxTechnologyPipeSend";
        const char kpipedelim = '|';

        private ProgressBar progressBarConnection;
        private System.Windows.Forms.Timer timerProgress;
        private System.Windows.Forms.Timer _connectionPollTimer;
        private bool _isConnecting = false;
        private bool _isDisconnecting = false;
        private bool _lastKnownConnected;
        private bool _pollInProgress;
        // Most-recent "Data Last Received: <ts>" timestamp seen from the
        // service. The progress bar ticks forward every time this changes.
        private string _lastDataTimestamp = "";
        // When we last saw a *new* timestamp value — drives Marquee hysteresis
        // so the bar doesn't flicker on/off between 1-second polls when the
        // service's timestamp is seconds-precision and multiple polls within
        // the same second see the same value.
        private DateTime _lastFreshTimestampAt = DateTime.MinValue;
        private const double kMarqueeInertiaSeconds = 10.0;

        // ── Palette ──────────────────────────────────────────────────────────
        static readonly Color clrBackground = Color.FromArgb(245, 246, 250);
        static readonly Color clrSurface = Color.White;
        static readonly Color clrBorder = Color.FromArgb(220, 223, 230);
        static readonly Color clrAccent = Color.FromArgb(37, 99, 235);   // blue-600
        static readonly Color clrAccentHover = Color.FromArgb(29, 78, 216);
        static readonly Color clrTextPrimary = Color.FromArgb(15, 23, 42);
        static readonly Color clrTextMuted = Color.FromArgb(100, 116, 139);
        static readonly Color clrGreen = Color.FromArgb(22, 163, 74);
        static readonly Color clrRed = Color.FromArgb(220, 38, 38);

        #region private variables
        private string _panelType = "";
        #endregion

        public frmSetup()
        {
            InitializeComponent();

            // Stay above AMX overlays once an event lands; matches frmAbout
            // and frmTestBox which both set this in their constructors.
            this.TopMost = true;

            // Initialize after InitializeComponent

            ApplyModernStyling();
            LoadFormData();

        }

        private void TimerProgress_Tick(object sender, EventArgs e)
        {
            // Legacy fake animation. The progress bar is now driven by
            // PollConnectionTick reading the service's "Data Last Received"
            // timestamps — see UpdateProgressBarFromStatus. Left as a no-op
            // until we remove the timer entirely.
        }

        private void OnSerialPortConnected()
        {
            // Marquee starts in UpdateProgressBarFromStatus once we see a
            // "Data Last Received" tick from the service.
            SetMarquee(false);
            _lastDataTimestamp = "";
            _lastFreshTimestampAt = DateTime.MinValue;
        }

        private void OnSerialPortDisconnected()
        {
            SetMarquee(false);
            _lastDataTimestamp = "";
            _lastFreshTimestampAt = DateTime.MinValue;
        }

        // Marquee style = always-moving stripe while panel data is flowing.
        // Continuous style with Value=0 = static empty bar (port closed or
        // panel silent). Mike's feedback was that a quantitative bar driven
        // by byte-burst events looked erratic; the Marquee just signals
        // "link alive" without pretending to measure rate.
        private void SetMarquee(bool on)
        {
            if (on)
            {

                progressBar1.Style = ProgressBarStyle.Blocks;
                if (progressBar1.Value + 20 > 100)
                {
                    progressBar1.Value = 0;
                }
                progressBar1.Value = progressBar1.Value + 20;
                _lastFreshTimestampAt = DateTime.Now;

                int glNumHeartbeats = Convert.ToInt32(sendcmd("GetPanelHandShake"));
                int glNumMessages = Convert.ToInt32(sendcmd("GetPanelNumMessages"));

                this.lblComCounterPanel1.Text = "Heart Beats: " + glNumHeartbeats.ToString() + " - Messages: " + glNumMessages;

                // Me.lblComCounterPanel1.Caption = Format$(glNumHeartbeats, "#,###,###,##0") & " - " & Format$(glNumMessages, "#,###,###,##0")

            }
        }

        // ── Styling ───────────────────────────────────────────────────────────
        private void ApplyModernStyling()
        {
            // Form
            this.Text = "Setup";
            this.BackColor = clrBackground;
            this.Font = new Font("Segoe UI", 9f, FontStyle.Regular);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Padding = new Padding(0);

            // Tab control
            StyleTabControl(tabPage);

            // Labels – section headers get bold+muted treatment in Paint
            StyleSectionLabels();

            // Inputs
            foreach (Control c in GetAllControls(this))
            {
                if (c is TextBox tb) StyleTextBox(tb);
                if (c is ComboBox cb) StyleComboBox(cb);
                if (c is NumericUpDown nud) StyleNumeric(nud);
                if (c is CheckBox chk) StyleCheckBox(chk);
                if (c is Button btn) StyleButton(btn);
                if (c is Panel pnl && pnl.Tag?.ToString() == "card") StyleCard(pnl);
            }

            // Status label
            StyleStatusLabel(lbStatus);

            // Footer strip
            StyleFooterPanel(pnlFooter);

            // Primary button
            StyleButtonPrimary(btok);
        }

        private void StyleTabControl(TabControl tc)
        {
            tc.DrawMode = TabDrawMode.OwnerDrawFixed;
            tc.SizeMode = TabSizeMode.Fixed;
            tc.ItemSize = new Size(110, 32);
            tc.Appearance = TabAppearance.FlatButtons;
            tc.Padding = new Point(0, 0);
            tc.BackColor = clrBackground;
            tc.DrawItem += TabControl_DrawItem;
            tc.SelectedIndexChanged += (s, e) => tc.Invalidate();
        }

        private void TabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabControl tc = (TabControl)sender;
            bool selected = e.Index == tc.SelectedIndex;
            Rectangle r = tc.GetTabRect(e.Index);

            using (SolidBrush bg = new SolidBrush(selected ? clrSurface : clrBackground))
                e.Graphics.FillRectangle(bg, r);

            if (selected)
            {
                using (Pen accent = new Pen(clrAccent, 2))
                    e.Graphics.DrawLine(accent, r.Left, r.Bottom - 1, r.Right, r.Bottom - 1);
            }

            Color txtColor = selected ? clrAccent : clrTextMuted;
            using (SolidBrush br = new SolidBrush(txtColor))
            {
                StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                Font f = new Font("Segoe UI", 8.5f, selected ? FontStyle.Bold : FontStyle.Regular);
                e.Graphics.DrawString(tc.TabPages[e.Index].Text, f, br, r, sf);
            }
        }

        private void StyleSectionLabels()
        {
            // Applied per label in designer; here we do a sweep for any Label with Tag="section"
            foreach (Label lbl in GetAllControls(this).OfType<Label>().Where(l => l.Tag?.ToString() == "section"))
            {
                lbl.Font = new Font("Segoe UI", 7.5f, FontStyle.Bold);
                lbl.ForeColor = clrTextMuted;
                lbl.AutoSize = true;
            }
        }

        private void StyleTextBox(TextBox tb)
        {
            tb.BorderStyle = BorderStyle.FixedSingle;
            tb.BackColor = clrSurface;
            tb.ForeColor = clrTextPrimary;
            tb.Font = new Font("Segoe UI", 9f);
            tb.Height = 26;
        }

        private void StyleComboBox(ComboBox cb)
        {
            cb.FlatStyle = FlatStyle.Flat;
            cb.BackColor = clrSurface;
            cb.ForeColor = clrTextPrimary;
            cb.Font = new Font("Segoe UI", 9f);
            cb.DrawMode = DrawMode.OwnerDrawFixed;
            cb.ItemHeight = 22;
            cb.DrawItem += ComboBox_DrawItem;
        }

        private void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            ComboBox cb = (ComboBox)sender;
            bool selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            e.DrawBackground();
            Color bg = selected ? Color.FromArgb(219, 234, 254) : clrSurface;
            Color fg = selected ? clrAccent : clrTextPrimary;
            using (SolidBrush br = new SolidBrush(bg))
                e.Graphics.FillRectangle(br, e.Bounds);
            using (SolidBrush tbr = new SolidBrush(fg))
                e.Graphics.DrawString(cb.Items[e.Index].ToString(), cb.Font, tbr,
                    new Rectangle(e.Bounds.X + 4, e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Height));
            e.DrawFocusRectangle();
        }

        private void StyleNumeric(NumericUpDown nud)
        {
            nud.BorderStyle = BorderStyle.FixedSingle;
            nud.BackColor = clrSurface;
            nud.ForeColor = clrTextPrimary;
            nud.Font = new Font("Segoe UI", 9f);
        }

        private void StyleCheckBox(CheckBox chk)
        {
            chk.FlatStyle = FlatStyle.Flat;
            chk.ForeColor = clrTextPrimary;
            chk.Font = new Font("Segoe UI", 9f);
            chk.FlatAppearance.BorderColor = clrBorder;
        }

        private void StyleButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = clrBorder;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(241, 245, 249);
            btn.BackColor = clrSurface;
            btn.ForeColor = clrTextPrimary;
            btn.Font = new Font("Segoe UI", 9f);
            btn.Cursor = Cursors.Hand;
            btn.Height = 30;
        }

        private void StyleButtonPrimary(Button btn)
        {
            btn.BackColor = clrAccent;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = clrAccentHover;
            btn.FlatAppearance.MouseOverBackColor = clrAccentHover;
            btn.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.Height = 30;
        }

        private void StyleCard(Panel pnl)
        {
            pnl.BackColor = clrSurface;
            pnl.Paint += (s, e) =>
            {
                using (Pen p = new Pen(clrBorder, 1))
                {
                    int r = 6;
                    Rectangle rc = new Rectangle(0, 0, pnl.Width - 1, pnl.Height - 1);
                    using (GraphicsPath gp = RoundedRect(rc, r))
                    {
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        e.Graphics.DrawPath(p, gp);
                    }
                }
            };
        }

        private void StyleStatusLabel(Label lbl)
        {
            lbl.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            lbl.AutoSize = true;
        }

        private void StyleFooterPanel(Panel pnl)
        {
            pnl.BackColor = clrSurface;
            pnl.Paint += (s, e) =>
            {
                using (Pen p = new Pen(clrBorder))
                    e.Graphics.DrawLine(p, 0, 0, pnl.Width, 0);
            };
        }

        // ── Status dot helper (drawn on pnlStatusDot) ─────────────────────────
        private void UpdateStatusDot(bool connected)
        {
            pnlStatusDot.Paint -= PaintDot_Red;
            pnlStatusDot.Paint -= PaintDot_Green;
            if (connected)
            {
                pnlStatusDot.Paint += PaintDot_Green;
                OnSerialPortConnected();
            }
            else
            {
                pnlStatusDot.Paint += PaintDot_Red;
                OnSerialPortDisconnected();
            }
            pnlStatusDot.Invalidate();
        }

        private void PaintDot_Green(object sender, PaintEventArgs e) => PaintDot(e, clrGreen);
        private void PaintDot_Red(object sender, PaintEventArgs e) => PaintDot(e, clrRed);

        private void PaintDot(PaintEventArgs e, Color c)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (SolidBrush br = new SolidBrush(c))
                e.Graphics.FillEllipse(br, 2, 2, pnlStatusDot.Width - 4, pnlStatusDot.Height - 4);
        }

        // Service replies "CONNECTED" before any data arrives, then switches to
        // "Data Last Received: <timestamp>" once the panel actually starts
        // talking (DraxService.cs:1460-1467). Both mean the COM port is open.
        // Anything else ("DISCONNECTED", "ERROR", "Error: ...", empty) is not.
        private static bool IsConnectedStatus(string status)
        {
            if (string.IsNullOrEmpty(status)) return false;
            string lower = status.ToLower();
            return lower.StartsWith("connected") || lower.StartsWith("data last received");
        }

        private async void PollConnectionTick(object sender, EventArgs e)
        {
            if (_pollInProgress) return;
            // Capture the comport name on the UI thread; we can't touch the
            // ComboBox from inside the Task.Run lambda below (WinForms controls
            // are thread-affine to the thread that created them).
            string comport = cbComport.Text;
            if (string.IsNullOrEmpty(comport)) return;
            _pollInProgress = true;
            try
            {
                string status = await Task.Run(() => sendcmd("GETCOMMPORTSTATUS|" + comport));
                bool connected = IsConnectedStatus(status);
                if (connected != _lastKnownConnected)
                {
                    _lastKnownConnected = connected;
                    this.lbStatus.Text = connected ? "Connected" : "Disconnected";
                    this.lbStatus.ForeColor = connected ? clrGreen : clrRed;
                    UpdateStatusDot(connected);
                }
                UpdateProgressBarFromStatus(status);
            }
            finally { _pollInProgress = false; }
        }

        // Progress bar shows handshake activity as a Marquee animation —
        // continuous moving stripe while panel data is flowing, static
        // empty bar otherwise. Driven by the "Data Last Received: <ts>"
        // reply from the service. The first cut toggled Marquee on/off per
        // poll based on whether the timestamp changed since the previous
        // tick, but the service's timestamp is seconds-precision so multiple
        // polls within the same second saw the same value and the bar
        // flickered. Hysteresis now: record when we last saw a *fresh* value
        // and keep Marquee running for kMarqueeInertiaSeconds after that.
        // Reads as smooth motion while data flows, stops cleanly when the
        // panel goes silent for more than a few seconds.
        private void UpdateProgressBarFromStatus(string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                SetMarquee(false);
                _lastDataTimestamp = "";
                _lastFreshTimestampAt = DateTime.MinValue;
                return;
            }
            string lower = status.ToLower();
            if (lower.StartsWith("data last received"))
            {
                int colon = status.IndexOf(':');
                string ts = colon >= 0 ? status.Substring(colon + 1).Trim() : "";
                if (!string.IsNullOrEmpty(ts) && ts != _lastDataTimestamp)
                {
                    _lastDataTimestamp = ts;
                    _lastFreshTimestampAt = DateTime.Now;
                }
                bool fresh = (DateTime.Now - _lastFreshTimestampAt).TotalSeconds
                             > kMarqueeInertiaSeconds;
                SetMarquee(fresh);
            }
            else
            {
                // "CONNECTED" with no data yet, "DISCONNECTED", or "ERROR".
                SetMarquee(false);
                _lastDataTimestamp = "";
                _lastFreshTimestampAt = DateTime.MinValue;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_connectionPollTimer != null)
            {
                _connectionPollTimer.Stop();
                _connectionPollTimer.Tick -= PollConnectionTick;
                _connectionPollTimer.Dispose();
                _connectionPollTimer = null;
            }
            base.OnFormClosing(e);
        }

        // ── Data loading (original constructor logic, cleaned up) ─────────────
        private void LoadFormData()
        {
            timerProgress = new System.Windows.Forms.Timer();
            timerProgress.Interval = 1;
            timerProgress.Tick += TimerProgress_Tick;

            tabPage.TabPages.Remove(tprsm);
            tabPage.TabPages.Remove(tpemail);
            tabPage.TabPages.Remove(tpadvanced);
            tabPage.TabPages.Remove(tbGent);
            tabPage.TabPages.Remove(tpInspire);
            // COM ports
            for (int i = 1; i <= 10; i++)
                cbComport.Items.Add(new ComboBoxItem { Text = $"COM{i}", Value = i.ToString() });

            _panelType = sendcmd("GetPanelType");
            switch (_panelType)
            {
                case "GENT":
                    tabPage.TabPages.Add(tbGent);
                    break;

                case "ADVANCED":
                    tabPage.TabPages.Add(tpadvanced);
                    break;

                case "INSPIRE":
                    tabPage.TabPages.Add(tpInspire);
                    break;

                case "RSM":
                    tabPage.TabPages.Remove(tpserialsettings);
                    tabPage.TabPages.Add(tprsm);
                    break;

                case "EMAIL":
                    tabPage.TabPages.Remove(tpserialsettings);
                    tabPage.TabPages.Add(tpemail);
                    break;
            }

            string result = sendcmd("SETTINGSGET|PANEL1,COMMPORT");
            foreach (ComboBoxItem item in cbComport.Items)
            {
                if (item.Value == result) { cbComport.SelectedItem = item; break; }
            }

            // Offset / panel-type-specific
            switch (_panelType)
            {
                case "ADVANCED":
                    this.tbOffset.Text = sendcmd("SETTINGSGET|SETUP,AMX1OFFSET");
                    break;
                case "EMAIL":
                    load_email_groups();
                    this.tbname.Text = sendcmd("SETTINGSGET|EMAIL,SMTPSERVER");
                    this.tbuser.Text = sendcmd("SETTINGSGET|EMAIL,LOGINNAME");
                    string smtppassword = sendcmd("SETTINGSGET|EMAIL,PASSWORD");
                    this.tbpassword.Text = AesDecryptor.DecryptOpenSSLCtr(smtppassword, "");
                    this.tbport.Text = sendcmd("SETTINGSGET|EMAIL,SMTPPORT");
                    this.tbfrom.Text = sendcmd("SETTINGSGET|EMAIL,FROM");
                    result = sendcmd("SETTINGSGET|EMAIL,SMTPAUTHORISATION");
                    this.cbauthorisation.Checked = result == "1" || result == "True";

                    break;
                case "GENT":
                    this.tbOffset.Text = sendcmd("SETTINGSGET|SETUP,GIAMX1OFFSET");
                    result = sendcmd("SETTINGSGET|SETUP,OutStationFaultsGenFault");
                    this.chkOutStationFaults.Checked = result == "1" || result == "True";

                    result = sendcmd("SETTINGSGET|SETUP,DisplayChkSumFails");
                    this.chkDisplayChkSumFails.Checked = result == "1" || result == "True";

                    result = sendcmd("SETTINGSGET|SETUP,DisablePanelText");
                    this.chkDisablePanelText.Checked = result == "1" || result == "True";

                    result = sendcmd("SETTINGSGET|SETUP,DisplayUnknownEvents");
                    this.chkDisplayUnknown.Checked = result == "1" || result == "True";

                    result = sendcmd("SETTINGSGET|SETUP,EXTENDEDTEXT");
                    this.chkExtendedText.Checked = result == "1" || result == "True";

                    result = sendcmd("SETTINGSGET|SETUP,EXTENDEDTEXTFILEPATH");
                    this.txtFilePath.Text = result;

                    result = sendcmd("SETTINGSGET|SETUP,UseExtendedTextIfOver");
                    this.ExtendedTextifOver.Text = result;

                    result = sendcmd("SETTINGSGET|SETUP,DELIMITER");
                    this.cboDelimiter.SelectedIndex = Convert.ToInt32(result) - 1;

                    break;
                case "MOLREYZX":
                case "MORLEYMAX":
                case "NOTIFIER":
                case "INSPRE":
                case "PEARL":
                case "SYNCRO":
                    this.tbOffset.Text = sendcmd("SETTINGSGET|PANEL1,AMX1OFFSET");
                    break;
                case "INSPIRE":
                    // Inspire module offset lives in SETUP (the service reads
                    // SETUP/giAmx1Offset + SETUP/ModuleOffsetMode in PanelInspire).
                    this.tbInspireOffset.Text = sendcmd("SETTINGSGET|SETUP,GIAMX1OFFSET");
                    if (this.tbInspireOffset.Text == "") this.tbInspireOffset.Text = "0";
                    break;
                case "RSM":
                    load_rsm();
                    break;
                case "TAKTIS":
                    this.tbOffset.Text = sendcmd("SETTINGSGET|SETUP,AMX1OFFSET");
                    break;
            }

            if (tbOffset.Text == "") tbOffset.Text = "0";

            // Status
            string status = sendcmd("GETCOMMPORTSTATUS|" + cbComport.Text);
            bool connected = IsConnectedStatus(status);
            _lastKnownConnected = connected;
            this.lbStatus.Text = connected ? "Connected" : "Disconnected";
            this.lbStatus.ForeColor = connected ? clrGreen : clrRed;
            UpdateStatusDot(connected);

            // Live polling so cable removal / re-insertion (or the service
            // restarting) shows up without re-opening the form. Service can
            // reply with either "CONNECTED" or "Data Last Received: <ts>" once
            // the panel starts talking — both mean "port open", so the parse
            // accepts both prefixes.
            _connectionPollTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            _connectionPollTimer.Tick += PollConnectionTick;
            _connectionPollTimer.Start();

            // Data Bits
            cbDataBits.Items.Add(new ComboBoxItem { Text = "8", Value = "8" });
            cbDataBits.Items.Add(new ComboBoxItem { Text = "7", Value = "7" });
            result = sendcmd("SETTINGSGET|SETUP,DataBits");
            foreach (ComboBoxItem item in cbDataBits.Items)
            {
                if (item.Value == result) { cbDataBits.SelectedItem = item; break; }
            }

            // Baud Raute
            if (_panelType == "NOTIFIER" || _panelType == "INPSIRE")
            {
                cbBaudRate.Items.Add(new ComboBoxItem { Text = "9600", Value = "9600" });
                cbBaudRate.Enabled = false;
            }
            else
            {
                cbBaudRate.Items.Add(new ComboBoxItem { Text = "300", Value = "300" });
                cbBaudRate.Items.Add(new ComboBoxItem { Text = "600", Value = "600" });
                cbBaudRate.Items.Add(new ComboBoxItem { Text = "1200", Value = "600" });
                cbBaudRate.Items.Add(new ComboBoxItem { Text = "2400", Value = "1200" });
                cbBaudRate.Items.Add(new ComboBoxItem { Text = "4800", Value = "4800" });
                cbBaudRate.Items.Add(new ComboBoxItem { Text = "9600", Value = "9600" });
                cbBaudRate.Items.Add(new ComboBoxItem { Text = "19200", Value = "19200" });
                cbBaudRate.Items.Add(new ComboBoxItem { Text = "38400", Value = "38400" });
            }
            result = sendcmd("SETTINGSGET|SETUP,BaudRate");
            foreach (ComboBoxItem item in cbBaudRate.Items)
            {
                if (item.Value == result) { cbBaudRate.SelectedItem = item; break; }
            }

            // Parity
            cbParity.Items.Add(new ComboBoxItem { Text = "Even", Value = "Even" });
            cbParity.Items.Add(new ComboBoxItem { Text = "Odd", Value = "Odd" });
            cbParity.Items.Add(new ComboBoxItem { Text = "None", Value = "None" });
            result = sendcmd("SETTINGSGET|SETUP,PARITY");
            foreach (ComboBoxItem item in cbParity.Items)
            {
                if (item.Value.ToLower() == result.ToLower()) { cbParity.SelectedItem = item; break; }
            }

            // Parity
            cbStopBits.Items.Add(new ComboBoxItem { Text = "1", Value = "1" });
            cbStopBits.Items.Add(new ComboBoxItem { Text = "1.5", Value = "1.5" });
            cbStopBits.Items.Add(new ComboBoxItem { Text = "2", Value = "2" });
            result = sendcmd("SETTINGSGET|SETUP,STOPBITS");
            foreach (ComboBoxItem item in cbStopBits.Items)
            {
                if (item.Value.ToLower() == result.ToLower()) { cbStopBits.SelectedItem = item; break; }
            }

            // Debug / data-logging checkbox
            result = "0";
            if (_panelType == "ADVANCED") result = sendcmd("SETTINGSGET|MAIN,DesignTime");
            else if (_panelType == "GENT") result = sendcmd("SETTINGSGET|SETUP,DataLogging");
            this.debug.Checked = result == "1" || result == "True";

            // Advanced-only controls
            if (_panelType == "ADVANCED")
            {
                cboHB1.Minimum = 0; cboHB1.Maximum = 99; cboHB1.Value = 1; cboHB1.Increment = 1;

                this.chkRefreshZonesStart.Checked = sendcmd("SETTINGSGET|SETUP,RefreshZonesStart") == "1";
                this.chkRefreshZonesConfig.Checked = sendcmd("SETTINGSGET|SETUP,RefreshZonesConfig") == "1";
                this.chkDefaultZone.Checked = sendcmd("SETTINGSGET|SETUP,DefaultZoneText") == "1";
                this.chkIgnoreNullZone.Checked = sendcmd("SETTINGSGET|SETUP,IgnoreNullZone") == "1";

                result = sendcmd("SETTINGSGET|SETUP,gbUseSubAddressOffset");
                bool showSub = chkSubAddressOffset.Checked;
                this.SubAddressOffsetNumber.Visible = showSub;
                this.lblSubAddressOffsetNumber.Visible = showSub;

                this.chkClassicIsolations.Checked = sendcmd("SETTINGSGET|SETUP,UseClassicIsolations") == "1";
            }
            if (_panelType == "INSPIRE")
            {
                string ModuleOffsetMode = sendcmd("SETTINGSGET|SETUP,ModuleOffsetMode");
                if (ModuleOffsetMode == "Loop")
                {
                    this.rbOffsetLoop.Checked = true;
                    this.tbInspireOffset.Text = sendcmd("SETTINGSGET|SETUP,ModuleOffset");
                    if (this.tbInspireOffset.Text.Length == 0)
                    {
                        this.tbInspireOffset.Text = "10";
                    }

                }
                else
                {
                    this.rbOffsetNode.Checked = true;
                    this.tbInspireOffset.Text = sendcmd("SETTINGSGET|SETUP,ModuleOffset");
                    if (this.tbInspireOffset.Text.Length == 0)
                    {
                        this.tbInspireOffset.Text = "100";
                    }
                }
            }
        }

        // ── Pipe helpers (unchanged) ──────────────────────────────────────────
        private string sendcmd(string cmd, string parameters = "")
        {
            string strcmd = cmd;
            if (!string.IsNullOrEmpty(parameters))
                strcmd += kpipedelim + parameters;
            try { return Task.Run(() => sendserver(strcmd)).Result; }
            catch (Exception ex) { return "Error: " + ex; }
        }

        private async Task<string> sendserver(string message)
        {
            using (NamedPipeClientStream pipe = new NamedPipeClientStream(".", kpipenamesend, PipeDirection.InOut))
            {
                pipe.Connect(5000);
                pipe.ReadMode = PipeTransmissionMode.Message;
                byte[] ba = Encoding.Default.GetBytes(message);
                pipe.Write(ba, 0, ba.Length);
                var result = await Task.Run(() => readmessage(pipe));
                string strresponse = Encoding.Default.GetString(result);
                Console.WriteLine("Response received from Send server: " + strresponse);
                return strresponse;
            }
        }

        private static byte[] readmessage(PipeStream pipe)
        {
            byte[] buffer = new byte[1024];
            using (var ms = new MemoryStream())
            {
                do { var rb = pipe.Read(buffer, 0, buffer.Length); ms.Write(buffer, 0, rb); }
                while (!pipe.IsMessageComplete);
                return ms.ToArray();
            }
        }

        // ── Event handlers (original, unchanged) ─────────────────────────────
        private void btApply_Click(object sender, EventArgs e)
        {
            savesettings();
            // sendcmd("ServiceRestart");
        }

        private void btok_Click(object sender, EventArgs e)
        {
            savesettings();
            // sendcmd("ServiceRestart");
            this.Close();
        }

        private void savesettings()
        {
            if (_panelType == "EMAIL")
            {
                sendcmd($"SETTINGSSET|EMAIL,SMTPSERVER,{this.tbname.Text}");
                sendcmd($"SETTINGSSET|EMAIL,LOGINNAME,{this.tbuser.Text}");
                string encryptedPassword = AesDecryptor.EncryptOpenSSLCtr(this.tbpassword.Text, "");
                sendcmd($"SETTINGSSET|EMAIL,PASSWORD,{encryptedPassword}");
                sendcmd($"SETTINGSSET|EMAIL,SMTPPORT,{this.tbport.Text}");
                sendcmd($"SETTINGSSET|EMAIL,FROM,{this.tbfrom.Text}");
                sendcmd($"SETTINGSSET|EMAIL,SMTPAUTHORISATION,{(cbauthorisation.Checked ? "1" : "0")}");
                sendcmd($"SETTINGSSET|EMAIL,DATALOGGING,{(debug.Checked ? "1" : "0")}");
            }
            else
            {
                if (cbComport.SelectedItem is ComboBoxItem item)
                    sendcmd($"SETTINGSSET|PANEL1,COMMPORT,{item.Value}");

                sendcmd($"SETTINGSSET|SETUP,DATALOGGING,{(debug.Checked ? "1" : "0")}");
                sendcmd($"SETTINGSSET|SETUP,BAUDRATE,{this.cbBaudRate.Text}");
                sendcmd($"SETTINGSSET|SETUP,DATABITS,{this.cbDataBits.Text}");
                sendcmd($"SETTINGSSET|SETUP,PARITY,{this.cbParity.Text}");
                sendcmd($"SETTINGSSET|SETUP,STOPBITS,{this.cbStopBits.Text}");
                sendcmd($"SETTINGSSET|SETUP,GIAMX1OFFSET,{this.tbOffset.Text}");
                if (_panelType == "GENT")
                {
                    sendcmd($"SETTINGSSET|SETUP,OutStationFaultsGenFault,{(chkOutStationFaults.Checked ? "1" : "0")}");
                    sendcmd($"SETTINGSSET|SETUP,DisplayChkSumFails,{(chkDisplayChkSumFails.Checked ? "1" : "0")}");
                    sendcmd($"SETTINGSSET|SETUP,DisablePanelText,{(chkDisablePanelText.Checked ? "1" : "0")}");
                    sendcmd($"SETTINGSSET|SETUP,DisplayUnknownEvents,{(chkDisplayUnknown.Checked ? "1" : "0")}");
                    sendcmd($"SETTINGSSET|SETUP,EXTENDEDTEXT,{(chkExtendedText.Checked ? "1" : "0")}");
                    sendcmd($"SETTINGSSET|SETUP,EXTENDEDTEXTFILEPATH,{txtFilePath.Text}");
                    sendcmd($"SETTINGSSET|SETUP,UseExtendedTextIfOver,{ExtendedTextifOver.Text}");
                    sendcmd($"SETTINGSSET|SETUP,DELIMITER,{(cboDelimiter.SelectedIndex) + 1}");
                }
                if (_panelType == "INSPIRE")
                {
                    // Overwrites the generic GIAMX1OFFSET write above with the
                    // value from the Inspire tab, plus the Node/Loop axis mode.
                    sendcmd($"SETTINGSSET|SETUP,ModuleOffset,{this.tbInspireOffset.Text}");
                    sendcmd($"SETTINGSSET|SETUP,ModuleOffsetMode,{(rbOffsetLoop.Checked ? "Loop" : "Node")}");
                }
            }
            sendcmd("SETTINGSSAVE");
        }

        private void btcancel_Click(object sender, EventArgs e) => this.Close();

        private void frmSetup_Load(object sender, EventArgs e)
            => sendcmd("SETTINGSRELOAD");

        private void load_email_groups() => this.tabPage.SelectedTab = tpemail;
        private void load_rsm() => this.tabPage.SelectedTab = tpserialsettings;

        private void chkSubAddressOffset_CheckedChanged(object sender, EventArgs e)
        {
            this.SubAddressOffsetNumber.Visible = chkSubAddressOffset.Checked;
            this.lblSubAddressOffsetNumber.Visible = chkSubAddressOffset.Checked;
        }

        private void bteditemailgroups_Click(object sender, EventArgs e)
            => new frmemaigroups().ShowDialog();

        private void btdiscovery_Click(object sender, EventArgs e)
            => new frmdiscovery().ShowDialog();

        private void btnNodes_Click(object sender, EventArgs e)
            => new frmRSMNodes().ShowDialog();

        private void label7_Click(object sender, EventArgs e) { }
        private void label9_Click(object sender, EventArgs e) { }

        // ── Utility ───────────────────────────────────────────────────────────
        private IEnumerable<Control> GetAllControls(Control root)
        {
            foreach (Control c in root.Controls)
            {
                yield return c;
                foreach (Control child in GetAllControls(c))
                    yield return child;
            }
        }

        private static GraphicsPath RoundedRect(Rectangle r, int radius)
        {
            int d = radius * 2;
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            gp.CloseFigure();
            return gp;
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select a File";
                openFileDialog.Filter = "All Files (*.*)|*.*";
                // Common filter examples:
                // "Text Files (*.txt)|*.txt"
                // "Excel Files (*.xlsx)|*.xlsx"
                // "Images (*.jpg;*.png)|*.jpg;*.png"

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = openFileDialog.FileName;
                }
            }
        }

        private void rbOffsetLoop_CheckedChanged(object sender, EventArgs e)
        {
            this.tbInspireOffset.Text = sendcmd("SETTINGSGET|SETUP,ModuleOffset");
            if (this.tbInspireOffset.Text.Length == 0)
            {
                this.tbInspireOffset.Text = "10";
            }
        }

        private void rbOffsetNode_CheckedChanged(object sender, EventArgs e)
        {
            this.tbInspireOffset.Text = sendcmd("SETTINGSGET|SETUP,ModuleOffset");
            if (this.tbInspireOffset.Text.Length == 0)
            {
                this.tbInspireOffset.Text = "100";
            }
        }
    }
}