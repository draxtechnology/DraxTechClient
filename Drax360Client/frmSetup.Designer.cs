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
            tabPage = new TabControl();
            tpserialsettings = new TabPage();
            progressBar1 = new ProgressBar();
            pnlStatusDot = new Panel();
            cbDataBits = new ComboBox();
            lbStatus = new Label();
            cbBaudRate = new ComboBox();
            label1 = new Label();
            label7 = new Label();
            cbComport = new ComboBox();
            label3 = new Label();
            lblCalibration = new Label();
            label2 = new Label();
            label6 = new Label();
            tbOffset = new TextBox();
            cbParity = new ComboBox();
            label4 = new Label();
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
            btdevices = new Button();
            tbGent = new TabPage();
            ExtendedTextifOver = new TextBox();
            cboDelimiter = new ComboBox();
            label9 = new Label();
            label8 = new Label();
            btBrowse = new Button();
            txtFilePath = new TextBox();
            chkExtendedText = new CheckBox();
            chkDisplayUnknown = new CheckBox();
            chkDisablePanelText = new CheckBox();
            chkDisplayChkSumFails = new CheckBox();
            chkOutStationFaults = new CheckBox();
            lblComCounterPanel1 = new Label();
            pnlFooter.SuspendLayout();
            tabPage.SuspendLayout();
            tpserialsettings.SuspendLayout();
            tpsettings.SuspendLayout();
            tpadvanced.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cboHB1).BeginInit();
            tpemail.SuspendLayout();
            tprsm.SuspendLayout();
            tbGent.SuspendLayout();
            SuspendLayout();
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.White;
            pnlFooter.Controls.Add(btcancel);
            pnlFooter.Controls.Add(btApply);
            pnlFooter.Controls.Add(btok);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 424);
            pnlFooter.Margin = new Padding(3, 4, 3, 4);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(0, 13, 14, 13);
            pnlFooter.Size = new Size(674, 69);
            pnlFooter.TabIndex = 1;
            // 
            // btcancel
            // 
            btcancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btcancel.Location = new Point(557, 13);
            btcancel.Margin = new Padding(3, 4, 3, 4);
            btcancel.Name = "btcancel";
            btcancel.Size = new Size(91, 40);
            btcancel.TabIndex = 2;
            btcancel.Text = "Cancel";
            btcancel.Click += btcancel_Click;
            // 
            // btApply
            // 
            btApply.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btApply.Location = new Point(454, 15);
            btApply.Margin = new Padding(3, 4, 3, 4);
            btApply.Name = "btApply";
            btApply.Size = new Size(91, 40);
            btApply.TabIndex = 1;
            btApply.Text = "Apply";
            btApply.Click += btApply_Click;
            // 
            // btok
            // 
            btok.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btok.Location = new Point(351, 15);
            btok.Margin = new Padding(3, 4, 3, 4);
            btok.Name = "btok";
            btok.Size = new Size(91, 40);
            btok.TabIndex = 0;
            btok.Text = "OK";
            btok.Click += btok_Click;
            // 
            // tabPage
            // 
            tabPage.Controls.Add(tpserialsettings);
            tabPage.Controls.Add(tpsettings);
            tabPage.Controls.Add(tpadvanced);
            tabPage.Controls.Add(tpemail);
            tabPage.Controls.Add(tprsm);
            tabPage.Controls.Add(tbGent);
            tabPage.Dock = DockStyle.Fill;
            tabPage.Location = new Point(0, 0);
            tabPage.Margin = new Padding(3, 4, 3, 4);
            tabPage.Name = "tabPage";
            tabPage.SelectedIndex = 0;
            tabPage.Size = new Size(674, 424);
            tabPage.TabIndex = 0;
            // 
            // tpserialsettings
            // 
            tpserialsettings.BackColor = Color.FromArgb(245, 246, 250);
            tpserialsettings.Controls.Add(lblComCounterPanel1);
            tpserialsettings.Controls.Add(progressBar1);
            tpserialsettings.Controls.Add(pnlStatusDot);
            tpserialsettings.Controls.Add(cbDataBits);
            tpserialsettings.Controls.Add(lbStatus);
            tpserialsettings.Controls.Add(cbBaudRate);
            tpserialsettings.Controls.Add(label1);
            tpserialsettings.Controls.Add(label7);
            tpserialsettings.Controls.Add(cbComport);
            tpserialsettings.Controls.Add(label3);
            tpserialsettings.Controls.Add(lblCalibration);
            tpserialsettings.Controls.Add(label2);
            tpserialsettings.Controls.Add(label6);
            tpserialsettings.Controls.Add(tbOffset);
            tpserialsettings.Controls.Add(cbParity);
            tpserialsettings.Controls.Add(label4);
            tpserialsettings.Location = new Point(4, 29);
            tpserialsettings.Margin = new Padding(3, 4, 3, 4);
            tpserialsettings.Name = "tpserialsettings";
            tpserialsettings.Padding = new Padding(18, 19, 18, 11);
            tpserialsettings.Size = new Size(666, 391);
            tpserialsettings.TabIndex = 0;
            tpserialsettings.Text = "Serial Settings";
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(216, 20);
            progressBar1.Margin = new Padding(3, 4, 3, 4);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(431, 31);
            progressBar1.TabIndex = 7;
            // 
            // pnlStatusDot
            // 
            pnlStatusDot.BackColor = Color.Transparent;
            pnlStatusDot.Location = new Point(23, 32);
            pnlStatusDot.Margin = new Padding(3, 4, 3, 4);
            pnlStatusDot.Name = "pnlStatusDot";
            pnlStatusDot.Size = new Size(16, 19);
            pnlStatusDot.TabIndex = 0;
            // 
            // cbDataBits
            // 
            cbDataBits.FormattingEnabled = true;
            cbDataBits.Location = new Point(18, 245);
            cbDataBits.Margin = new Padding(3, 4, 3, 4);
            cbDataBits.Name = "cbDataBits";
            cbDataBits.Size = new Size(159, 28);
            cbDataBits.TabIndex = 6;
            // 
            // lbStatus
            // 
            lbStatus.AutoSize = true;
            lbStatus.Location = new Point(48, 31);
            lbStatus.Name = "lbStatus";
            lbStatus.Size = new Size(99, 20);
            lbStatus.TabIndex = 0;
            lbStatus.Text = "Disconnected";
            // 
            // cbBaudRate
            // 
            cbBaudRate.FormattingEnabled = true;
            cbBaudRate.Location = new Point(248, 163);
            cbBaudRate.Margin = new Padding(3, 4, 3, 4);
            cbBaudRate.Name = "cbBaudRate";
            cbBaudRate.Size = new Size(159, 28);
            cbBaudRate.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 139);
            label1.Name = "label1";
            label1.Size = new Size(83, 20);
            label1.TabIndex = 0;
            label1.Tag = "fieldlabel";
            label1.Text = "Comm Port";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = SystemColors.ControlDarkDark;
            label7.Location = new Point(18, 88);
            label7.Name = "label7";
            label7.Size = new Size(162, 20);
            label7.TabIndex = 4;
            label7.Tag = "fieldlabel";
            label7.Text = "PORT CONFIGURATION";
            // 
            // cbComport
            // 
            cbComport.BackColor = SystemColors.Window;
            cbComport.DropDownStyle = ComboBoxStyle.DropDownList;
            cbComport.FlatStyle = FlatStyle.Popup;
            cbComport.FormattingEnabled = true;
            cbComport.Location = new Point(18, 163);
            cbComport.Margin = new Padding(3, 4, 3, 4);
            cbComport.Name = "cbComport";
            cbComport.Size = new Size(159, 28);
            cbComport.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(18, 221);
            label3.Name = "label3";
            label3.Size = new Size(69, 20);
            label3.TabIndex = 2;
            label3.Tag = "fieldlabel";
            label3.Text = "Data Bits";
            // 
            // lblCalibration
            // 
            lblCalibration.AutoSize = true;
            lblCalibration.Location = new Point(18, 321);
            lblCalibration.Name = "lblCalibration";
            lblCalibration.Size = new Size(100, 20);
            lblCalibration.TabIndex = 2;
            lblCalibration.Tag = "section";
            lblCalibration.Text = "CALIBRATION";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(248, 139);
            label2.Name = "label2";
            label2.Size = new Size(77, 20);
            label2.TabIndex = 1;
            label2.Tag = "fieldlabel";
            label2.Text = "Baud Rate";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(18, 351);
            label6.Name = "label6";
            label6.Size = new Size(49, 20);
            label6.TabIndex = 3;
            label6.Tag = "fieldlabel";
            label6.Text = "Offset";
            // 
            // tbOffset
            // 
            tbOffset.Location = new Point(201, 347);
            tbOffset.Margin = new Padding(3, 4, 3, 4);
            tbOffset.Name = "tbOffset";
            tbOffset.Size = new Size(159, 27);
            tbOffset.TabIndex = 4;
            // 
            // cbParity
            // 
            cbParity.DropDownStyle = ComboBoxStyle.DropDownList;
            cbParity.FormattingEnabled = true;
            cbParity.Location = new Point(248, 245);
            cbParity.Margin = new Padding(3, 4, 3, 4);
            cbParity.Name = "cbParity";
            cbParity.Size = new Size(159, 28);
            cbParity.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(248, 221);
            label4.Name = "label4";
            label4.Size = new Size(45, 20);
            label4.TabIndex = 3;
            label4.Tag = "fieldlabel";
            label4.Text = "Parity";
            // 
            // tpsettings
            // 
            tpsettings.BackColor = Color.FromArgb(245, 246, 250);
            tpsettings.Controls.Add(debug);
            tpsettings.Location = new Point(4, 29);
            tpsettings.Margin = new Padding(3, 4, 3, 4);
            tpsettings.Name = "tpsettings";
            tpsettings.Padding = new Padding(18, 19, 18, 11);
            tpsettings.Size = new Size(666, 391);
            tpsettings.TabIndex = 1;
            tpsettings.Text = "Settings";
            // 
            // debug
            // 
            debug.AutoSize = true;
            debug.Location = new Point(37, 32);
            debug.Margin = new Padding(3, 4, 3, 4);
            debug.Name = "debug";
            debug.Size = new Size(154, 24);
            debug.TabIndex = 0;
            debug.Text = "Enable Debug Log";
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
            tpadvanced.Location = new Point(4, 29);
            tpadvanced.Margin = new Padding(3, 4, 3, 4);
            tpadvanced.Name = "tpadvanced";
            tpadvanced.Padding = new Padding(18, 19, 18, 11);
            tpadvanced.Size = new Size(666, 391);
            tpadvanced.TabIndex = 2;
            tpadvanced.Text = "Advanced Panel";
            // 
            // chkRefreshZonesStart
            // 
            chkRefreshZonesStart.AutoSize = true;
            chkRefreshZonesStart.Location = new Point(30, 24);
            chkRefreshZonesStart.Margin = new Padding(3, 4, 3, 4);
            chkRefreshZonesStart.Name = "chkRefreshZonesStart";
            chkRefreshZonesStart.Size = new Size(228, 24);
            chkRefreshZonesStart.TabIndex = 0;
            chkRefreshZonesStart.Text = "Refresh Zone Texts on Startup";
            // 
            // chkRefreshZonesConfig
            // 
            chkRefreshZonesConfig.AutoSize = true;
            chkRefreshZonesConfig.Location = new Point(30, 59);
            chkRefreshZonesConfig.Margin = new Padding(3, 4, 3, 4);
            chkRefreshZonesConfig.Name = "chkRefreshZonesConfig";
            chkRefreshZonesConfig.Size = new Size(325, 24);
            chkRefreshZonesConfig.TabIndex = 1;
            chkRefreshZonesConfig.Text = "Refresh Zone Texts on Configuration Change";
            // 
            // chkDefaultZone
            // 
            chkDefaultZone.AutoSize = true;
            chkDefaultZone.Location = new Point(30, 93);
            chkDefaultZone.Margin = new Padding(3, 4, 3, 4);
            chkDefaultZone.Name = "chkDefaultZone";
            chkDefaultZone.Size = new Size(149, 24);
            chkDefaultZone.TabIndex = 2;
            chkDefaultZone.Text = "Default Zone Text";
            // 
            // chkIgnoreNullZone
            // 
            chkIgnoreNullZone.AutoSize = true;
            chkIgnoreNullZone.Location = new Point(30, 128);
            chkIgnoreNullZone.Margin = new Padding(3, 4, 3, 4);
            chkIgnoreNullZone.Name = "chkIgnoreNullZone";
            chkIgnoreNullZone.Size = new Size(174, 24);
            chkIgnoreNullZone.TabIndex = 3;
            chkIgnoreNullZone.Text = "Ignore Null Zone Text";
            // 
            // chkClassicIsolations
            // 
            chkClassicIsolations.AutoSize = true;
            chkClassicIsolations.Location = new Point(30, 163);
            chkClassicIsolations.Margin = new Padding(3, 4, 3, 4);
            chkClassicIsolations.Name = "chkClassicIsolations";
            chkClassicIsolations.Size = new Size(142, 24);
            chkClassicIsolations.TabIndex = 4;
            chkClassicIsolations.Text = "Classic Isolations";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(30, 208);
            label5.Name = "label5";
            label5.Size = new Size(115, 20);
            label5.TabIndex = 5;
            label5.Tag = "fieldlabel";
            label5.Text = "Heartbeat Panel";
            // 
            // cboHB1
            // 
            cboHB1.Location = new Point(251, 208);
            cboHB1.Margin = new Padding(3, 4, 3, 4);
            cboHB1.Name = "cboHB1";
            cboHB1.Size = new Size(80, 27);
            cboHB1.TabIndex = 5;
            // 
            // chkSubAddressOffset
            // 
            chkSubAddressOffset.AutoSize = true;
            chkSubAddressOffset.Location = new Point(30, 251);
            chkSubAddressOffset.Margin = new Padding(3, 4, 3, 4);
            chkSubAddressOffset.Name = "chkSubAddressOffset";
            chkSubAddressOffset.Size = new Size(157, 24);
            chkSubAddressOffset.TabIndex = 6;
            chkSubAddressOffset.TabStop = false;
            chkSubAddressOffset.Text = "Sub Address Offset";
            chkSubAddressOffset.CheckedChanged += chkSubAddressOffset_CheckedChanged;
            // 
            // lblSubAddressOffsetNumber
            // 
            lblSubAddressOffsetNumber.AutoSize = true;
            lblSubAddressOffsetNumber.Location = new Point(30, 285);
            lblSubAddressOffsetNumber.Name = "lblSubAddressOffsetNumber";
            lblSubAddressOffsetNumber.Size = new Size(193, 20);
            lblSubAddressOffsetNumber.TabIndex = 7;
            lblSubAddressOffsetNumber.Tag = "fieldlabel";
            lblSubAddressOffsetNumber.Text = "Sub Address Offset Number";
            lblSubAddressOffsetNumber.Visible = false;
            // 
            // SubAddressOffsetNumber
            // 
            SubAddressOffsetNumber.Location = new Point(251, 281);
            SubAddressOffsetNumber.Margin = new Padding(3, 4, 3, 4);
            SubAddressOffsetNumber.Name = "SubAddressOffsetNumber";
            SubAddressOffsetNumber.Size = new Size(91, 27);
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
            tpemail.Location = new Point(4, 29);
            tpemail.Margin = new Padding(3, 4, 3, 4);
            tpemail.Name = "tpemail";
            tpemail.Padding = new Padding(18, 19, 18, 11);
            tpemail.Size = new Size(666, 391);
            tpemail.TabIndex = 3;
            tpemail.Text = "Email";
            // 
            // bteditemailgroups
            // 
            bteditemailgroups.Location = new Point(18, 19);
            bteditemailgroups.Margin = new Padding(3, 4, 3, 4);
            bteditemailgroups.Name = "bteditemailgroups";
            bteditemailgroups.Size = new Size(114, 40);
            bteditemailgroups.TabIndex = 0;
            bteditemailgroups.Text = "Edit Groups";
            bteditemailgroups.Click += bteditemailgroups_Click;
            // 
            // lbsmtpserver
            // 
            lbsmtpserver.AutoSize = true;
            lbsmtpserver.Location = new Point(18, 83);
            lbsmtpserver.Name = "lbsmtpserver";
            lbsmtpserver.Size = new Size(91, 20);
            lbsmtpserver.TabIndex = 1;
            lbsmtpserver.Tag = "fieldlabel";
            lbsmtpserver.Text = "SMTP Server";
            // 
            // tbname
            // 
            tbname.Location = new Point(18, 107);
            tbname.Margin = new Padding(3, 4, 3, 4);
            tbname.Name = "tbname";
            tbname.Size = new Size(285, 27);
            tbname.TabIndex = 1;
            // 
            // lbport
            // 
            lbport.AutoSize = true;
            lbport.Location = new Point(18, 152);
            lbport.Name = "lbport";
            lbport.Size = new Size(35, 20);
            lbport.TabIndex = 2;
            lbport.Tag = "fieldlabel";
            lbport.Text = "Port";
            // 
            // tbport
            // 
            tbport.Location = new Point(18, 176);
            tbport.Margin = new Padding(3, 4, 3, 4);
            tbport.Name = "tbport";
            tbport.Size = new Size(114, 27);
            tbport.TabIndex = 2;
            // 
            // lbuser
            // 
            lbuser.AutoSize = true;
            lbuser.Location = new Point(366, 83);
            lbuser.Name = "lbuser";
            lbuser.Size = new Size(75, 20);
            lbuser.TabIndex = 3;
            lbuser.Tag = "fieldlabel";
            lbuser.Text = "Username";
            // 
            // tbuser
            // 
            tbuser.Location = new Point(366, 107);
            tbuser.Margin = new Padding(3, 4, 3, 4);
            tbuser.Name = "tbuser";
            tbuser.Size = new Size(285, 27);
            tbuser.TabIndex = 3;
            // 
            // lbpassword
            // 
            lbpassword.AutoSize = true;
            lbpassword.Location = new Point(366, 152);
            lbpassword.Name = "lbpassword";
            lbpassword.Size = new Size(70, 20);
            lbpassword.TabIndex = 4;
            lbpassword.Tag = "fieldlabel";
            lbpassword.Text = "Password";
            // 
            // tbpassword
            // 
            tbpassword.Location = new Point(366, 176);
            tbpassword.Margin = new Padding(3, 4, 3, 4);
            tbpassword.Name = "tbpassword";
            tbpassword.PasswordChar = '*';
            tbpassword.Size = new Size(285, 27);
            tbpassword.TabIndex = 4;
            // 
            // cbauthorisation
            // 
            cbauthorisation.AutoSize = true;
            cbauthorisation.Location = new Point(18, 227);
            cbauthorisation.Margin = new Padding(3, 4, 3, 4);
            cbauthorisation.Name = "cbauthorisation";
            cbauthorisation.Size = new Size(222, 24);
            cbauthorisation.TabIndex = 5;
            cbauthorisation.Text = "Requires SMTP Authorisation";
            // 
            // tprsm
            // 
            tprsm.BackColor = Color.FromArgb(245, 246, 250);
            tprsm.Controls.Add(btdevices);
            tprsm.Location = new Point(4, 29);
            tprsm.Margin = new Padding(3, 4, 3, 4);
            tprsm.Name = "tprsm";
            tprsm.Padding = new Padding(18, 19, 18, 11);
            tprsm.Size = new Size(666, 391);
            tprsm.TabIndex = 4;
            tprsm.Text = "RSM";
            // 
            // btdevices
            // 
            btdevices.Location = new Point(265, 21);
            btdevices.Margin = new Padding(3, 4, 3, 4);
            btdevices.Name = "btdevices";
            btdevices.Size = new Size(114, 40);
            btdevices.TabIndex = 2;
            btdevices.Text = "Devices";
            btdevices.Click += btdevices_Click;
            // 
            // tbGent
            // 
            tbGent.BackColor = Color.FromArgb(245, 246, 250);
            tbGent.BackgroundImageLayout = ImageLayout.Center;
            tbGent.Controls.Add(ExtendedTextifOver);
            tbGent.Controls.Add(cboDelimiter);
            tbGent.Controls.Add(label9);
            tbGent.Controls.Add(label8);
            tbGent.Controls.Add(btBrowse);
            tbGent.Controls.Add(txtFilePath);
            tbGent.Controls.Add(chkExtendedText);
            tbGent.Controls.Add(chkDisplayUnknown);
            tbGent.Controls.Add(chkDisablePanelText);
            tbGent.Controls.Add(chkDisplayChkSumFails);
            tbGent.Controls.Add(chkOutStationFaults);
            tbGent.Location = new Point(4, 29);
            tbGent.Margin = new Padding(3, 4, 3, 4);
            tbGent.Name = "tbGent";
            tbGent.Padding = new Padding(3, 4, 3, 4);
            tbGent.Size = new Size(666, 391);
            tbGent.TabIndex = 5;
            tbGent.Text = "Gent Panel";
            // 
            // ExtendedTextifOver
            // 
            ExtendedTextifOver.Location = new Point(234, 220);
            ExtendedTextifOver.Margin = new Padding(3, 4, 3, 4);
            ExtendedTextifOver.Name = "ExtendedTextifOver";
            ExtendedTextifOver.Size = new Size(102, 27);
            ExtendedTextifOver.TabIndex = 12;
            // 
            // cboDelimiter
            // 
            cboDelimiter.FormattingEnabled = true;
            cboDelimiter.Items.AddRange(new object[] { "Comma", "Tab" });
            cboDelimiter.Location = new Point(234, 259);
            cboDelimiter.Margin = new Padding(3, 4, 3, 4);
            cboDelimiter.Name = "cboDelimiter";
            cboDelimiter.Size = new Size(102, 28);
            cboDelimiter.TabIndex = 11;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(64, 261);
            label9.Name = "label9";
            label9.Size = new Size(134, 20);
            label9.TabIndex = 10;
            label9.Text = "Delimter Character";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(64, 224);
            label8.Name = "label8";
            label8.Size = new Size(171, 20);
            label8.TabIndex = 9;
            label8.Text = "Use if Panel text is over -";
            // 
            // btBrowse
            // 
            btBrowse.Location = new Point(545, 165);
            btBrowse.Margin = new Padding(3, 4, 3, 4);
            btBrowse.Name = "btBrowse";
            btBrowse.Size = new Size(86, 31);
            btBrowse.TabIndex = 8;
            btBrowse.Text = "Browse";
            btBrowse.UseVisualStyleBackColor = true;
            btBrowse.Click += btBrowse_Click;
            // 
            // txtFilePath
            // 
            txtFilePath.Location = new Point(185, 167);
            txtFilePath.Margin = new Padding(3, 4, 3, 4);
            txtFilePath.Name = "txtFilePath";
            txtFilePath.Size = new Size(353, 27);
            txtFilePath.TabIndex = 6;
            // 
            // chkExtendedText
            // 
            chkExtendedText.AutoSize = true;
            chkExtendedText.Location = new Point(41, 172);
            chkExtendedText.Margin = new Padding(3, 4, 3, 4);
            chkExtendedText.Name = "chkExtendedText";
            chkExtendedText.Size = new Size(152, 24);
            chkExtendedText.TabIndex = 5;
            chkExtendedText.Text = "Use Extended Text";
            // 
            // chkDisplayUnknown
            // 
            chkDisplayUnknown.AutoSize = true;
            chkDisplayUnknown.Location = new Point(41, 139);
            chkDisplayUnknown.Margin = new Padding(3, 4, 3, 4);
            chkDisplayUnknown.Name = "chkDisplayUnknown";
            chkDisplayUnknown.Size = new Size(191, 24);
            chkDisplayUnknown.TabIndex = 4;
            chkDisplayUnknown.Text = "Display Unknown Events";
            // 
            // chkDisablePanelText
            // 
            chkDisablePanelText.AutoSize = true;
            chkDisablePanelText.Location = new Point(41, 105);
            chkDisablePanelText.Margin = new Padding(3, 4, 3, 4);
            chkDisablePanelText.Name = "chkDisablePanelText";
            chkDisablePanelText.Size = new Size(151, 24);
            chkDisablePanelText.TabIndex = 3;
            chkDisablePanelText.Text = "Disable Panel Text";
            // 
            // chkDisplayChkSumFails
            // 
            chkDisplayChkSumFails.AutoSize = true;
            chkDisplayChkSumFails.Location = new Point(41, 72);
            chkDisplayChkSumFails.Margin = new Padding(3, 4, 3, 4);
            chkDisplayChkSumFails.Name = "chkDisplayChkSumFails";
            chkDisplayChkSumFails.Size = new Size(169, 24);
            chkDisplayChkSumFails.TabIndex = 2;
            chkDisplayChkSumFails.Text = "Display ChkSum Fails";
            // 
            // chkOutStationFaults
            // 
            chkOutStationFaults.AutoSize = true;
            chkOutStationFaults.Location = new Point(41, 39);
            chkOutStationFaults.Margin = new Padding(3, 4, 3, 4);
            chkOutStationFaults.Name = "chkOutStationFaults";
            chkOutStationFaults.Size = new Size(280, 24);
            chkOutStationFaults.TabIndex = 1;
            chkOutStationFaults.Text = "All Outstation Faults Are General Fault";
            // 
            // lblComCounterPanel1
            // 
            lblComCounterPanel1.AutoSize = true;
            lblComCounterPanel1.Location = new Point(370, 65);
            lblComCounterPanel1.Name = "lblComCounterPanel1";
            lblComCounterPanel1.Size = new Size(19, 20);
            lblComCounterPanel1.TabIndex = 8;
            lblComCounterPanel1.Text = "- ";
            // 
            // frmSetup
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(674, 493);
            Controls.Add(tabPage);
            Controls.Add(pnlFooter);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmSetup";
            Text = "Setup";
            Load += frmSetup_Load;
            pnlFooter.ResumeLayout(false);
            tabPage.ResumeLayout(false);
            tpserialsettings.ResumeLayout(false);
            tpserialsettings.PerformLayout();
            tpsettings.ResumeLayout(false);
            tpsettings.PerformLayout();
            tpadvanced.ResumeLayout(false);
            tpadvanced.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)cboHB1).EndInit();
            tpemail.ResumeLayout(false);
            tpemail.PerformLayout();
            tprsm.ResumeLayout(false);
            tbGent.ResumeLayout(false);
            tbGent.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        // ── Field declarations ───────────────────────────────────────────────
        private Panel pnlFooter;
        private Panel pnlStatusDot;
        private Label lblCalibration;
        private Button btApply;
        private Button btok;
        private Button btcancel;
        private ComboBox cbComport;
        private Label label1;
        private Label lbStatus;
        private Label label2;
        private Label label3;
        private Label label4;
        private ComboBox cbParity;
        private CheckBox debug;
        private Label label6;
        private TextBox tbOffset;
        private TabControl tabPage;
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
        private Button btdevices;
        private TabPage tbGent;
        private CheckBox chkExtendedText;
        private CheckBox chkDisplayUnknown;
        private CheckBox chkDisablePanelText;
        private CheckBox chkDisplayChkSumFails;
        private CheckBox chkOutStationFaults;
        private Label label7;
        private ComboBox cbDataBits;
        private ComboBox cbBaudRate;
        private ProgressBar progressBar1;
        private Button btBrowse;
        private TextBox txtFilePath;
        private TextBox ExtendedTextifOver;
        private ComboBox cboDelimiter;
        private Label label9;
        private Label label8;
        private Label lblComCounterPanel1;
    }
}