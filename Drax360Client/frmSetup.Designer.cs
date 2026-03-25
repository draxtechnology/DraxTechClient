namespace DraxClient
{
    partial class frmSetup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetup));
            pnlFooter = new Panel();
            btcancel = new Button();
            btApply = new Button();
            btok = new Button();
            tpGent = new TabControl();
            tpserialsettings = new TabPage();
            pnlStatusBar = new Panel();
            pnlStatusDot = new Panel();
            lbStatus = new Label();
            pnlSerialCard = new Panel();
            label7 = new Label();
            label1 = new Label();
            cbComport = new ComboBox();
            label2 = new Label();
            tbBaudRate = new TextBox();
            label3 = new Label();
            tbDataBits = new TextBox();
            label4 = new Label();
            cbParity = new ComboBox();
            lblCalibration = new Label();
            label6 = new Label();
            tbOffset = new TextBox();
            tpsettings = new TabPage();
            debug = new CheckBox();
            tpadvanced = new TabPage();
            chkRefreshZonesStart = new CheckBox();
            chkRefreshZonesConfig = new CheckBox();
            chkDefaultZone = new CheckBox();
            chkIgnoreNullZone = new CheckBox();
            chkClassicIsolations = new CheckBox();
            label5 = new Label();
            cboHB1 = new NumericUpDown();
            chkSubAddressOffset = new CheckBox();
            lblSubAddressOffsetNumber = new Label();
            SubAddressOffsetNumber = new TextBox();
            tpemail = new TabPage();
            bteditemailgroups = new Button();
            lbsmtpserver = new Label();
            tbname = new TextBox();
            lbport = new Label();
            tbport = new TextBox();
            lbuser = new Label();
            tbuser = new TextBox();
            lbpassword = new Label();
            tbpassword = new TextBox();
            cbauthorisation = new CheckBox();
            tprsm = new TabPage();
            btdiscovery = new Button();
            frmListen = new Button();
            btdevices = new Button();
            tabPage1 = new TabPage();
            chkExtendedText = new CheckBox();
            chkDisplayUnknown = new CheckBox();
            chkDisablePanelText = new CheckBox();
            chkDisplayChkSumFails = new CheckBox();
            chkOutStationFaults = new CheckBox();
            pnlFooter.SuspendLayout();
            tpGent.SuspendLayout();
            tpserialsettings.SuspendLayout();
            pnlStatusBar.SuspendLayout();
            pnlSerialCard.SuspendLayout();
            tpsettings.SuspendLayout();
            tpadvanced.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cboHB1).BeginInit();
            tpemail.SuspendLayout();
            tprsm.SuspendLayout();
            tabPage1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.White;
            pnlFooter.Controls.Add(btcancel);
            pnlFooter.Controls.Add(btApply);
            pnlFooter.Controls.Add(btok);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 338);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(0, 10, 12, 10);
            pnlFooter.Size = new Size(755, 52);
            pnlFooter.TabIndex = 1;
            // 
            // btcancel
            // 
            btcancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btcancel.Location = new Point(614, 11);
            btcancel.Name = "btcancel";
            btcancel.Size = new Size(80, 30);
            btcancel.TabIndex = 2;
            btcancel.Text = "Cancel";
            btcancel.Click += btcancel_Click;
            // 
            // btApply
            // 
            btApply.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btApply.Location = new Point(526, 11);
            btApply.Name = "btApply";
            btApply.Size = new Size(80, 30);
            btApply.TabIndex = 1;
            btApply.Text = "Apply";
            btApply.Click += btApply_Click;
            // 
            // btok
            // 
            btok.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btok.Location = new Point(438, 11);
            btok.Name = "btok";
            btok.Size = new Size(80, 30);
            btok.TabIndex = 0;
            btok.Text = "OK";
            btok.Click += btok_Click;
            // 
            // tpGent
            // 
            tpGent.Controls.Add(tpserialsettings);
            tpGent.Controls.Add(tpsettings);
            tpGent.Controls.Add(tpadvanced);
            tpGent.Controls.Add(tpemail);
            tpGent.Controls.Add(tprsm);
            tpGent.Controls.Add(tabPage1);
            tpGent.Dock = DockStyle.Fill;
            tpGent.Location = new Point(0, 0);
            tpGent.Name = "tpGent";
            tpGent.SelectedIndex = 0;
            tpGent.Size = new Size(755, 338);
            tpGent.TabIndex = 0;
            // 
            // tpserialsettings
            // 
            tpserialsettings.BackColor = Color.FromArgb(245, 246, 250);
            tpserialsettings.Controls.Add(pnlStatusBar);
            tpserialsettings.Controls.Add(pnlSerialCard);
            tpserialsettings.Controls.Add(lblCalibration);
            tpserialsettings.Controls.Add(label6);
            tpserialsettings.Controls.Add(tbOffset);
            tpserialsettings.Location = new Point(4, 24);
            tpserialsettings.Name = "tpserialsettings";
            tpserialsettings.Padding = new Padding(16, 14, 16, 8);
            tpserialsettings.Size = new Size(747, 310);
            tpserialsettings.TabIndex = 0;
            tpserialsettings.Text = "Serial Settings";
            // 
            // pnlStatusBar
            // 
            pnlStatusBar.BackColor = Color.White;
            pnlStatusBar.Controls.Add(pnlStatusDot);
            pnlStatusBar.Controls.Add(lbStatus);
            pnlStatusBar.Location = new Point(16, 14);
            pnlStatusBar.Name = "pnlStatusBar";
            pnlStatusBar.Size = new Size(660, 36);
            pnlStatusBar.TabIndex = 0;
            // 
            // pnlStatusDot
            // 
            pnlStatusDot.BackColor = Color.Transparent;
            pnlStatusDot.Location = new Point(12, 11);
            pnlStatusDot.Name = "pnlStatusDot";
            pnlStatusDot.Size = new Size(14, 14);
            pnlStatusDot.TabIndex = 0;
            // 
            // lbStatus
            // 
            lbStatus.AutoSize = true;
            lbStatus.Location = new Point(34, 10);
            lbStatus.Name = "lbStatus";
            lbStatus.Size = new Size(79, 15);
            lbStatus.TabIndex = 0;
            lbStatus.Text = "Disconnected";
            // 
            // pnlSerialCard
            // 
            pnlSerialCard.BackColor = Color.White;
            pnlSerialCard.Controls.Add(label7);
            pnlSerialCard.Controls.Add(label1);
            pnlSerialCard.Controls.Add(cbComport);
            pnlSerialCard.Controls.Add(label2);
            pnlSerialCard.Controls.Add(tbBaudRate);
            pnlSerialCard.Controls.Add(label3);
            pnlSerialCard.Controls.Add(tbDataBits);
            pnlSerialCard.Controls.Add(label4);
            pnlSerialCard.Controls.Add(cbParity);
            pnlSerialCard.Location = new Point(16, 58);
            pnlSerialCard.Name = "pnlSerialCard";
            pnlSerialCard.Padding = new Padding(16, 12, 16, 12);
            pnlSerialCard.Size = new Size(660, 180);
            pnlSerialCard.TabIndex = 1;
            pnlSerialCard.Tag = "card";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = SystemColors.ControlDarkDark;
            label7.Location = new Point(19, 12);
            label7.Name = "label7";
            label7.Size = new Size(132, 15);
            label7.TabIndex = 4;
            label7.Tag = "fieldlabel";
            label7.Text = "PORT CONFIGURATION";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(19, 55);
            label1.Name = "label1";
            label1.Size = new Size(69, 15);
            label1.TabIndex = 0;
            label1.Tag = "fieldlabel";
            label1.Text = "Comm Port";
            // 
            // cbComport
            // 
            cbComport.BackColor = SystemColors.Window;
            cbComport.DropDownStyle = ComboBoxStyle.DropDownList;
            cbComport.FormattingEnabled = true;
            cbComport.Location = new Point(19, 73);
            cbComport.Name = "cbComport";
            cbComport.Size = new Size(140, 23);
            cbComport.TabIndex = 0;
            cbComport.SelectedIndexChanged += cbComport_SelectedIndexChanged;
            cbComport.FlatStyle = FlatStyle.Popup;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(250, 55);
            label2.Name = "label2";
            label2.Size = new Size(60, 15);
            label2.TabIndex = 1;
            label2.Tag = "fieldlabel";
            label2.Text = "Baud Rate";
            // 
            // tbBaudRate
            // 
            tbBaudRate.Location = new Point(250, 73);
            tbBaudRate.Name = "tbBaudRate";
            tbBaudRate.Size = new Size(140, 23);
            tbBaudRate.TabIndex = 1;
            tbBaudRate.TextChanged += tbBaudRate_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(19, 117);
            label3.Name = "label3";
            label3.Size = new Size(53, 15);
            label3.TabIndex = 2;
            label3.Tag = "fieldlabel";
            label3.Text = "Data Bits";
            // 
            // tbDataBits
            // 
            tbDataBits.Location = new Point(19, 135);
            tbDataBits.Name = "tbDataBits";
            tbDataBits.Size = new Size(140, 23);
            tbDataBits.TabIndex = 2;
            tbDataBits.TextChanged += tbDataBits_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(250, 117);
            label4.Name = "label4";
            label4.Size = new Size(37, 15);
            label4.TabIndex = 3;
            label4.Tag = "fieldlabel";
            label4.Text = "Parity";
            // 
            // cbParity
            // 
            cbParity.DropDownStyle = ComboBoxStyle.DropDownList;
            cbParity.FormattingEnabled = true;
            cbParity.Location = new Point(250, 135);
            cbParity.Name = "cbParity";
            cbParity.Size = new Size(140, 23);
            cbParity.TabIndex = 3;
            cbParity.SelectedIndexChanged += cbParity_SelectedIndexChanged;
            // 
            // lblCalibration
            // 
            lblCalibration.AutoSize = true;
            lblCalibration.Location = new Point(16, 241);
            lblCalibration.Name = "lblCalibration";
            lblCalibration.Size = new Size(81, 15);
            lblCalibration.TabIndex = 2;
            lblCalibration.Tag = "section";
            lblCalibration.Text = "CALIBRATION";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(16, 263);
            label6.Name = "label6";
            label6.Size = new Size(39, 15);
            label6.TabIndex = 3;
            label6.Tag = "fieldlabel";
            label6.Text = "Offset";
            // 
            // tbOffset
            // 
            tbOffset.Location = new Point(176, 260);
            tbOffset.Name = "tbOffset";
            tbOffset.Size = new Size(140, 23);
            tbOffset.TabIndex = 4;
            tbOffset.TextChanged += tbOffset_TextChanged;
            // 
            // tpsettings
            // 
            tpsettings.BackColor = Color.FromArgb(245, 246, 250);
            tpsettings.Controls.Add(debug);
            tpsettings.Location = new Point(4, 24);
            tpsettings.Name = "tpsettings";
            tpsettings.Padding = new Padding(16, 14, 16, 8);
            tpsettings.Size = new Size(747, 310);
            tpsettings.TabIndex = 1;
            tpsettings.Text = "Settings";
            // 
            // debug
            // 
            debug.AutoSize = true;
            debug.Location = new Point(32, 24);
            debug.Name = "debug";
            debug.Size = new Size(122, 19);
            debug.TabIndex = 0;
            debug.Text = "Enable Debug Log";
            debug.CheckedChanged += debug_CheckedChanged;
            // 
            // tpadvanced
            // 
            tpadvanced.BackColor = Color.FromArgb(245, 246, 250);
            tpadvanced.Controls.Add(chkRefreshZonesStart);
            tpadvanced.Controls.Add(chkRefreshZonesConfig);
            tpadvanced.Controls.Add(chkDefaultZone);
            tpadvanced.Controls.Add(chkIgnoreNullZone);
            tpadvanced.Controls.Add(chkClassicIsolations);
            tpadvanced.Controls.Add(label5);
            tpadvanced.Controls.Add(cboHB1);
            tpadvanced.Controls.Add(chkSubAddressOffset);
            tpadvanced.Controls.Add(lblSubAddressOffsetNumber);
            tpadvanced.Controls.Add(SubAddressOffsetNumber);
            tpadvanced.Location = new Point(4, 24);
            tpadvanced.Name = "tpadvanced";
            tpadvanced.Padding = new Padding(16, 14, 16, 8);
            tpadvanced.Size = new Size(747, 310);
            tpadvanced.TabIndex = 2;
            tpadvanced.Text = "Advanced Panel";
            // 
            // chkRefreshZonesStart
            // 
            chkRefreshZonesStart.AutoSize = true;
            chkRefreshZonesStart.Location = new Point(26, 18);
            chkRefreshZonesStart.Name = "chkRefreshZonesStart";
            chkRefreshZonesStart.Size = new Size(182, 19);
            chkRefreshZonesStart.TabIndex = 0;
            chkRefreshZonesStart.Text = "Refresh Zone Texts on Startup";
            // 
            // chkRefreshZonesConfig
            // 
            chkRefreshZonesConfig.AutoSize = true;
            chkRefreshZonesConfig.Location = new Point(26, 44);
            chkRefreshZonesConfig.Name = "chkRefreshZonesConfig";
            chkRefreshZonesConfig.Size = new Size(262, 19);
            chkRefreshZonesConfig.TabIndex = 1;
            chkRefreshZonesConfig.Text = "Refresh Zone Texts on Configuration Change";
            // 
            // chkDefaultZone
            // 
            chkDefaultZone.AutoSize = true;
            chkDefaultZone.Location = new Point(26, 70);
            chkDefaultZone.Name = "chkDefaultZone";
            chkDefaultZone.Size = new Size(118, 19);
            chkDefaultZone.TabIndex = 2;
            chkDefaultZone.Text = "Default Zone Text";
            // 
            // chkIgnoreNullZone
            // 
            chkIgnoreNullZone.AutoSize = true;
            chkIgnoreNullZone.Location = new Point(26, 96);
            chkIgnoreNullZone.Name = "chkIgnoreNullZone";
            chkIgnoreNullZone.Size = new Size(139, 19);
            chkIgnoreNullZone.TabIndex = 3;
            chkIgnoreNullZone.Text = "Ignore Null Zone Text";
            // 
            // chkClassicIsolations
            // 
            chkClassicIsolations.AutoSize = true;
            chkClassicIsolations.Location = new Point(26, 122);
            chkClassicIsolations.Name = "chkClassicIsolations";
            chkClassicIsolations.Size = new Size(115, 19);
            chkClassicIsolations.TabIndex = 4;
            chkClassicIsolations.Text = "Classic Isolations";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(26, 156);
            label5.Name = "label5";
            label5.Size = new Size(91, 15);
            label5.TabIndex = 5;
            label5.Tag = "fieldlabel";
            label5.Text = "Heartbeat Panel";
            // 
            // cboHB1
            // 
            cboHB1.Location = new Point(220, 156);
            cboHB1.Name = "cboHB1";
            cboHB1.Size = new Size(70, 23);
            cboHB1.TabIndex = 5;
            // 
            // chkSubAddressOffset
            // 
            chkSubAddressOffset.AutoSize = true;
            chkSubAddressOffset.Location = new Point(26, 188);
            chkSubAddressOffset.Name = "chkSubAddressOffset";
            chkSubAddressOffset.Size = new Size(126, 19);
            chkSubAddressOffset.TabIndex = 6;
            chkSubAddressOffset.TabStop = false;
            chkSubAddressOffset.Text = "Sub Address Offset";
            chkSubAddressOffset.CheckedChanged += chkSubAddressOffset_CheckedChanged;
            // 
            // lblSubAddressOffsetNumber
            // 
            lblSubAddressOffsetNumber.AutoSize = true;
            lblSubAddressOffsetNumber.Location = new Point(26, 214);
            lblSubAddressOffsetNumber.Name = "lblSubAddressOffsetNumber";
            lblSubAddressOffsetNumber.Size = new Size(154, 15);
            lblSubAddressOffsetNumber.TabIndex = 7;
            lblSubAddressOffsetNumber.Tag = "fieldlabel";
            lblSubAddressOffsetNumber.Text = "Sub Address Offset Number";
            lblSubAddressOffsetNumber.Visible = false;
            // 
            // SubAddressOffsetNumber
            // 
            SubAddressOffsetNumber.Location = new Point(220, 211);
            SubAddressOffsetNumber.Name = "SubAddressOffsetNumber";
            SubAddressOffsetNumber.Size = new Size(80, 23);
            SubAddressOffsetNumber.TabIndex = 7;
            SubAddressOffsetNumber.Visible = false;
            // 
            // tpemail
            // 
            tpemail.BackColor = Color.FromArgb(245, 246, 250);
            tpemail.Controls.Add(bteditemailgroups);
            tpemail.Controls.Add(lbsmtpserver);
            tpemail.Controls.Add(tbname);
            tpemail.Controls.Add(lbport);
            tpemail.Controls.Add(tbport);
            tpemail.Controls.Add(lbuser);
            tpemail.Controls.Add(tbuser);
            tpemail.Controls.Add(lbpassword);
            tpemail.Controls.Add(tbpassword);
            tpemail.Controls.Add(cbauthorisation);
            tpemail.Location = new Point(4, 24);
            tpemail.Name = "tpemail";
            tpemail.Padding = new Padding(16, 14, 16, 8);
            tpemail.Size = new Size(747, 310);
            tpemail.TabIndex = 3;
            tpemail.Text = "Email";
            // 
            // bteditemailgroups
            // 
            bteditemailgroups.Location = new Point(16, 14);
            bteditemailgroups.Name = "bteditemailgroups";
            bteditemailgroups.Size = new Size(100, 30);
            bteditemailgroups.TabIndex = 0;
            bteditemailgroups.Text = "Edit Groups";
            bteditemailgroups.Click += bteditemailgroups_Click;
            // 
            // lbsmtpserver
            // 
            lbsmtpserver.AutoSize = true;
            lbsmtpserver.Location = new Point(16, 62);
            lbsmtpserver.Name = "lbsmtpserver";
            lbsmtpserver.Size = new Size(73, 15);
            lbsmtpserver.TabIndex = 1;
            lbsmtpserver.Tag = "fieldlabel";
            lbsmtpserver.Text = "SMTP Server";
            // 
            // tbname
            // 
            tbname.Location = new Point(16, 80);
            tbname.Name = "tbname";
            tbname.Size = new Size(250, 23);
            tbname.TabIndex = 1;
            // 
            // lbport
            // 
            lbport.AutoSize = true;
            lbport.Location = new Point(16, 114);
            lbport.Name = "lbport";
            lbport.Size = new Size(29, 15);
            lbport.TabIndex = 2;
            lbport.Tag = "fieldlabel";
            lbport.Text = "Port";
            // 
            // tbport
            // 
            tbport.Location = new Point(16, 132);
            tbport.Name = "tbport";
            tbport.Size = new Size(100, 23);
            tbport.TabIndex = 2;
            // 
            // lbuser
            // 
            lbuser.AutoSize = true;
            lbuser.Location = new Point(320, 62);
            lbuser.Name = "lbuser";
            lbuser.Size = new Size(60, 15);
            lbuser.TabIndex = 3;
            lbuser.Tag = "fieldlabel";
            lbuser.Text = "Username";
            // 
            // tbuser
            // 
            tbuser.Location = new Point(320, 80);
            tbuser.Name = "tbuser";
            tbuser.Size = new Size(250, 23);
            tbuser.TabIndex = 3;
            // 
            // lbpassword
            // 
            lbpassword.AutoSize = true;
            lbpassword.Location = new Point(320, 114);
            lbpassword.Name = "lbpassword";
            lbpassword.Size = new Size(57, 15);
            lbpassword.TabIndex = 4;
            lbpassword.Tag = "fieldlabel";
            lbpassword.Text = "Password";
            // 
            // tbpassword
            // 
            tbpassword.Location = new Point(320, 132);
            tbpassword.Name = "tbpassword";
            tbpassword.PasswordChar = '*';
            tbpassword.Size = new Size(250, 23);
            tbpassword.TabIndex = 4;
            // 
            // cbauthorisation
            // 
            cbauthorisation.AutoSize = true;
            cbauthorisation.Location = new Point(16, 170);
            cbauthorisation.Name = "cbauthorisation";
            cbauthorisation.Size = new Size(146, 19);
            cbauthorisation.TabIndex = 5;
            cbauthorisation.Text = "Requires Authorisation";
            // 
            // tprsm
            // 
            tprsm.BackColor = Color.FromArgb(245, 246, 250);
            tprsm.Controls.Add(btdiscovery);
            tprsm.Controls.Add(frmListen);
            tprsm.Controls.Add(btdevices);
            tprsm.Location = new Point(4, 24);
            tprsm.Name = "tprsm";
            tprsm.Padding = new Padding(16, 14, 16, 8);
            tprsm.Size = new Size(747, 310);
            tprsm.TabIndex = 4;
            tprsm.Text = "RSM";
            // 
            // btdiscovery
            // 
            btdiscovery.Location = new Point(16, 16);
            btdiscovery.Name = "btdiscovery";
            btdiscovery.Size = new Size(100, 30);
            btdiscovery.TabIndex = 0;
            btdiscovery.Text = "Discovery";
            btdiscovery.Click += btdiscovery_Click;
            // 
            // frmListen
            // 
            frmListen.Location = new Point(124, 16);
            frmListen.Name = "frmListen";
            frmListen.Size = new Size(100, 30);
            frmListen.TabIndex = 1;
            frmListen.Text = "Listen";
            frmListen.Click += frmListen_Click;
            // 
            // btdevices
            // 
            btdevices.Location = new Point(232, 16);
            btdevices.Name = "btdevices";
            btdevices.Size = new Size(100, 30);
            btdevices.TabIndex = 2;
            btdevices.Text = "Devices";
            btdevices.Click += btdevices_Click;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.FromArgb(245, 246, 250);
            tabPage1.BackgroundImageLayout = ImageLayout.Center;
            tabPage1.Controls.Add(chkExtendedText);
            tabPage1.Controls.Add(chkDisplayUnknown);
            tabPage1.Controls.Add(chkDisablePanelText);
            tabPage1.Controls.Add(chkDisplayChkSumFails);
            tabPage1.Controls.Add(chkOutStationFaults);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(747, 310);
            tabPage1.TabIndex = 5;
            tabPage1.Text = "Gent Panel";
            // 
            // chkExtendedText
            // 
            chkExtendedText.AutoSize = true;
            chkExtendedText.Location = new Point(36, 129);
            chkExtendedText.Name = "chkExtendedText";
            chkExtendedText.Size = new Size(120, 19);
            chkExtendedText.TabIndex = 5;
            chkExtendedText.Text = "Use Extended Text";
            // 
            // chkDisplayUnknown
            // 
            chkDisplayUnknown.AutoSize = true;
            chkDisplayUnknown.Location = new Point(36, 104);
            chkDisplayUnknown.Name = "chkDisplayUnknown";
            chkDisplayUnknown.Size = new Size(155, 19);
            chkDisplayUnknown.TabIndex = 4;
            chkDisplayUnknown.Text = "Display Unknown Events";
            // 
            // chkDisablePanelText
            // 
            chkDisablePanelText.AutoSize = true;
            chkDisablePanelText.Location = new Point(36, 79);
            chkDisablePanelText.Name = "chkDisablePanelText";
            chkDisablePanelText.Size = new Size(120, 19);
            chkDisablePanelText.TabIndex = 3;
            chkDisablePanelText.Text = "Disable Panel Text";
            // 
            // chkDisplayChkSumFails
            // 
            chkDisplayChkSumFails.AutoSize = true;
            chkDisplayChkSumFails.Location = new Point(36, 54);
            chkDisplayChkSumFails.Name = "chkDisplayChkSumFails";
            chkDisplayChkSumFails.Size = new Size(138, 19);
            chkDisplayChkSumFails.TabIndex = 2;
            chkDisplayChkSumFails.Text = "Display ChkSum Fails";
            // 
            // chkOutStationFaults
            // 
            chkOutStationFaults.AutoSize = true;
            chkOutStationFaults.Location = new Point(36, 29);
            chkOutStationFaults.Name = "chkOutStationFaults";
            chkOutStationFaults.Size = new Size(226, 19);
            chkOutStationFaults.TabIndex = 1;
            chkOutStationFaults.Text = "All Outstation Faults Are General Fault";
            // 
            // frmSetup
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(755, 390);
            Controls.Add(tpGent);
            Controls.Add(pnlFooter);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmSetup";
            Text = "Setup";
            Load += frmSetup_Load;
            pnlFooter.ResumeLayout(false);
            tpGent.ResumeLayout(false);
            tpserialsettings.ResumeLayout(false);
            tpserialsettings.PerformLayout();
            pnlStatusBar.ResumeLayout(false);
            pnlStatusBar.PerformLayout();
            pnlSerialCard.ResumeLayout(false);
            pnlSerialCard.PerformLayout();
            tpsettings.ResumeLayout(false);
            tpsettings.PerformLayout();
            tpadvanced.ResumeLayout(false);
            tpadvanced.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)cboHB1).EndInit();
            tpemail.ResumeLayout(false);
            tpemail.PerformLayout();
            tprsm.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        // ── Field declarations ───────────────────────────────────────────────
        private Panel pnlFooter;
        private Panel pnlStatusBar;
        private Panel pnlStatusDot;
        private Panel pnlSerialCard;
        private Label lblCalibration;
        private Button btApply;
        private Button btok;
        private Button btcancel;
        private ComboBox cbComport;
        private Label label1;
        private Label lbStatus;
        private TextBox tbBaudRate;
        private Label label2;
        private Label label3;
        private TextBox tbDataBits;
        private Label label4;
        private ComboBox cbParity;
        private CheckBox debug;
        private Label label6;
        private TextBox tbOffset;
        private TabControl tpGent;
        private TabPage tpserialsettings;
        private TabPage tpadvanced;
        private CheckBox chkDefaultZone;
        private CheckBox chkRefreshZonesConfig;
        private CheckBox chkRefreshZonesStart;
        private TabPage tpsettings;
        private CheckBox chkIgnoreNullZone;
        private Label label5;
        private NumericUpDown cboHB1;
        private CheckBox chkSubAddressOffset;
        private Label lblSubAddressOffsetNumber;
        private TextBox SubAddressOffsetNumber;
        private CheckBox chkClassicIsolations;
        private TabPage tpemail;
        private Button bteditemailgroups;
        private Label lbuser;
        private TextBox tbuser;
        private Label lbport;
        private TextBox tbport;
        private Label lbsmtpserver;
        private TextBox tbname;
        private Label lbpassword;
        private TextBox tbpassword;
        private CheckBox cbauthorisation;
        private TabPage tprsm;
        private Button btdiscovery;
        private Button frmListen;
        private Button btdevices;
        private TabPage tabPage1;
        private CheckBox chkExtendedText;
        private CheckBox chkDisplayUnknown;
        private CheckBox chkDisablePanelText;
        private CheckBox chkDisplayChkSumFails;
        private CheckBox chkOutStationFaults;
        private Label label7;
    }
}