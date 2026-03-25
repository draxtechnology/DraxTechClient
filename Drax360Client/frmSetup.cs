using DraxClient.Panels.Email;
using DraxClient.Panels.RSM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TcpListenerApp;
using static DraxClient.frmTestBox;

namespace DraxClient
{
    public partial class frmSetup : Form
    {
        const string kpipenamesend = "DraxTechnologyPipeSend";
        const char kpipedelim = '|';

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
            ApplyModernStyling();
            LoadFormData();
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
            StyleTabControl(tpGent);

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
                pnlStatusDot.Paint += PaintDot_Green;
            else
                pnlStatusDot.Paint += PaintDot_Red;
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

        // ── Data loading (original constructor logic, cleaned up) ─────────────
        private void LoadFormData()
        {
            // COM ports
            for (int i = 1; i <= 10; i++)
                cbComport.Items.Add(new ComboBoxItem { Text = $"COM{i}", Value = i.ToString() });

            _panelType = sendcmd("GetPanelType");

            string result = sendcmd("SETTINGSGET|PANEL1,COMMPORT");
            foreach (ComboBoxItem item in cbComport.Items)
            {
                if (item.Value == result) { cbComport.SelectedItem = item; break; }
            }

            this.tbDataBits.Text = sendcmd("SETTINGSGET|SETUP,DataBits");
            this.tbBaudRate.Text = sendcmd("SETTINGSGET|SETUP,BaudRate");

            // Offset / panel-type-specific
            switch (_panelType)
            {
                case "ADVANCED":
                    this.tbOffset.Text = sendcmd("SETTINGSGET|SETUP,AMX1OFFSET");
                    break;
                case "EMAIL":
                    load_email_groups();
                    break;
                case "GENT":
                    this.tbOffset.Text = sendcmd("SETTINGSGET|SETUP,GIAMX1OFFSET");
                    break;
                case "MOLREYZX":
                case "MORLEYMAX":
                case "NOTIFIER":
                case "PEARL":
                case "SYNCRO":
                    this.tbOffset.Text = sendcmd("SETTINGSGET|PANEL1,AMX1OFFSET");
                    break;
                case "RSM":
                    load_rsm();
                    break;
                case "TAKTIS":
                    this.tbOffset.Text = sendcmd("SETTINGSGET|SETUP,AMX1OFFSET");
                    break;
            }

            // Status
            string status = sendcmd("GETCOMMPORTSTATUS|COM3");
            bool connected = status.ToLower() == "connected";
            this.lbStatus.Text = connected ? "Connected" : "Disconnected";
            this.lbStatus.ForeColor = connected ? clrGreen : clrRed;
            UpdateStatusDot(connected);

            // Parity
            cbParity.Items.Add(new ComboBoxItem { Text = "Even", Value = "Even" });
            cbParity.Items.Add(new ComboBoxItem { Text = "Odd", Value = "Odd" });
            cbParity.Items.Add(new ComboBoxItem { Text = "None", Value = "None" });
            result = sendcmd("SETTINGSGET|SETUP,PARITY");
            foreach (ComboBoxItem item in cbParity.Items)
            {
                if (item.Value == result) { cbParity.SelectedItem = item; break; }
            }

            // Debug / data-logging checkbox
            result = "0";
            if (_panelType == "ADVANCED") result = sendcmd("SETTINGSGET|MAIN,DesignTime");
            else if (_panelType == "GENT") result = sendcmd("SETTINGSGET|MAIN,DataLogging");
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
            sendcmd("SETTINGSSAVE");
            sendcmd("ServiceRestart");
        }

        private void btok_Click(object sender, EventArgs e)
        {
            sendcmd("SETTINGSSAVE");
            sendcmd("ServiceRestart");
            this.Close();
        }

        private void btcancel_Click(object sender, EventArgs e) => this.Close();

        private void cbComport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbComport.SelectedItem is ComboBoxItem item)
                sendcmd($"SETTINGSSET|PANEL1,COMMPORT,{item.Value}");
        }

        private void debug_CheckedChanged(object sender, EventArgs e)
            => sendcmd($"SETTINGSSET|SETUP,DATALOGGING,{(debug.Checked ? "1" : "0")}");

        private void tbBaudRate_TextChanged(object sender, EventArgs e)
            => sendcmd($"SETTINGSSET|SETUP,BAUDRATE,{this.tbBaudRate.Text}");

        private void tbDataBits_TextChanged(object sender, EventArgs e)
            => sendcmd($"SETTINGSSET|SETUP,DATABITS,{this.tbDataBits.Text}");

        private void cbParity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbParity.SelectedItem is ComboBoxItem item)
                sendcmd($"SETTINGSSET|SETUP,PARITY,{item.Value}");
        }

        private void tbOffset_TextChanged(object sender, EventArgs e)
            => sendcmd($"SETTINGSSET|SETUP,GIAMX1OFFSET,{this.tbOffset.Text}");

        private void frmSetup_Load(object sender, EventArgs e)
            => sendcmd("SETTINGSRELOAD");

        private void load_email_groups() => this.tpGent.SelectedTab = tpemail;
        private void load_rsm() => this.tpGent.SelectedTab = tprsm;

        private void chkSubAddressOffset_CheckedChanged(object sender, EventArgs e)
        {
            this.SubAddressOffsetNumber.Visible = chkSubAddressOffset.Checked;
            this.lblSubAddressOffsetNumber.Visible = chkSubAddressOffset.Checked;
        }

        private void bteditemailgroups_Click(object sender, EventArgs e)
            => new frmemaigroups().ShowDialog();

        private void btdiscovery_Click(object sender, EventArgs e)
            => new frmdiscovery().ShowDialog();

        private void frmListen_Click(object sender, EventArgs e)
            => new TcpListenerForm().ShowDialog();

        private void btdevices_Click(object sender, EventArgs e)
            => new frmdevices().ShowDialog();

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
    }
}