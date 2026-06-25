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
            label10 = new Label();
            cbStopBits = new ComboBox();
            lblComCounterPanel1 = new Label();
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
            lbfrom = new Label();
            tbfrom = new TextBox();
            cbauthorisation = new CheckBox();
            tprsm = new TabPage();
            btnNodes = new Button();
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
            tpInspire = new TabPage();
            gbModuleOffset = new GroupBox();
            rbOffsetNode = new RadioButton();
            rbOffsetLoop = new RadioButton();
            lblOffsetAmount = new Label();
            tbInspireOffset = new TextBox();
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
            pnlFooter.Location = new Point(0, 318);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(0, 10, 12, 10);
            pnlFooter.Size = new Size(590, 52);
            pnlFooter.TabIndex = 1;
            // 
            // btcancel
            // 
            btcancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btcancel.Location = new Point(487, 10);
            btcancel.Name = "btcancel";
            btcancel.Size = new Size(80, 30);
            btcancel.TabIndex = 2;
            btcancel.Text = "Cancel";
            btcancel.Click += btcancel_Click;
            // 
            // btApply
            // 
            btApply.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btApply.Location = new Point(397, 11);
            btApply.Name = "btApply";
            btApply.Size = new Size(80, 30);
            btApply.TabIndex = 1;
            btApply.Text = "Apply";
            btApply.Click += btApply_Click;
            // 
            // btok
            // 
            btok.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btok.Location = new Point(307, 11);
            btok.Name = "btok";
            btok.Size = new Size(80, 30);
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
            tabPage.Controls.Add(tpInspire);
            tabPage.Dock = DockStyle.Fill;
            tabPage.Location = new Point(0, 0);
            tabPage.Name = "tabPage";
            tabPage.SelectedIndex = 0;
            tabPage.Size = new Size(590, 318);
            tabPage.TabIndex = 0;
            // 
            // tpserialsettings
            // 
            tpserialsettings.BackColor = Color.FromArgb(245, 246, 250);
            tpserialsettings.Controls.Add(label10);
            tpserialsettings.Controls.Add(cbStopBits);
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
            tpserialsettings.Location = new Point(4, 24);
            tpserialsettings.Name = "tpserialsettings";
            tpserialsettings.Padding = new Padding(16, 14, 16, 8);
            tpserialsettings.Size = new Size(582, 290);
            tpserialsettings.TabIndex = 0;
            tpserialsettings.Text = "Serial Settings";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(393, 166);
            label10.Name = "label10";
            label10.Size = new Size(53, 15);
            label10.TabIndex = 10;
            label10.Tag = "fieldlabel";
            label10.Text = "Stop Bits";
            // 
            // cbStopBits
            // 
            cbStopBits.DropDownStyle = ComboBoxStyle.DropDownList;
            cbStopBits.FormattingEnabled = true;
            cbStopBits.Location = new Point(393, 184);
            cbStopBits.Name = "cbStopBits";
            cbStopBits.Size = new Size(140, 23);
            cbStopBits.TabIndex = 9;
            // 
            // lblComCounterPanel1
            // 
            lblComCounterPanel1.Location = new Point(189, 49);
            lblComCounterPanel1.Name = "lblComCounterPanel1";
            lblComCounterPanel1.Size = new Size(374, 20);
            lblComCounterPanel1.TabIndex = 8;
            lblComCounterPanel1.Text = " - ";
            lblComCounterPanel1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(217, 15);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(349, 23);
            progressBar1.TabIndex = 7;
            // 
            // pnlStatusDot
            // 
            pnlStatusDot.BackColor = Color.Transparent;
            pnlStatusDot.Location = new Point(20, 24);
            pnlStatusDot.Name = "pnlStatusDot";
            pnlStatusDot.Size = new Size(14, 14);
            pnlStatusDot.TabIndex = 0;
            // 
            // cbDataBits
            // 
            cbDataBits.FormattingEnabled = true;
            cbDataBits.Location = new Point(16, 184);
            cbDataBits.Name = "cbDataBits";
            cbDataBits.Size = new Size(140, 23);
            cbDataBits.TabIndex = 6;
            // 
            // lbStatus
            // 
            lbStatus.AutoSize = true;
            lbStatus.Location = new Point(42, 23);
            lbStatus.Name = "lbStatus";
            lbStatus.Size = new Size(79, 15);
            lbStatus.TabIndex = 0;
            lbStatus.Text = "Disconnected";
            // 
            // cbBaudRate
            // 
            cbBaudRate.FormattingEnabled = true;
            cbBaudRate.Location = new Point(217, 122);
            cbBaudRate.Name = "cbBaudRate";
            cbBaudRate.Size = new Size(140, 23);
            cbBaudRate.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 104);
            label1.Name = "label1";
            label1.Size = new Size(69, 15);
            label1.TabIndex = 0;
            label1.Tag = "fieldlabel";
            label1.Text = "Comm Port";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = SystemColors.ControlDarkDark;
            label7.Location = new Point(16, 66);
            label7.Name = "label7";
            label7.Size = new Size(132, 15);
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
            cbComport.Location = new Point(16, 122);
            cbComport.Name = "cbComport";
            cbComport.Size = new Size(140, 23);
            cbComport.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 166);
            label3.Name = "label3";
            label3.Size = new Size(53, 15);
            label3.TabIndex = 2;
            label3.Tag = "fieldlabel";
            label3.Text = "Data Bits";
            // 
            // lblCalibration
            // 
            lblCalibration.AutoSize = true;
            lblCalibration.Location = new Point(16, 234);
            lblCalibration.Name = "lblCalibration";
            lblCalibration.Size = new Size(81, 15);
            lblCalibration.TabIndex = 2;
            lblCalibration.Tag = "section";
            lblCalibration.Text = "CALIBRATION";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(217, 104);
            label2.Name = "label2";
            label2.Size = new Size(60, 15);
            label2.TabIndex = 1;
            label2.Tag = "fieldlabel";
            label2.Text = "Baud Rate";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(16, 256);
            label6.Name = "label6";
            label6.Size = new Size(39, 15);
            label6.TabIndex = 3;
            label6.Tag = "fieldlabel";
            label6.Text = "Offset";
            // 
            // tbOffset
            // 
            tbOffset.Location = new Point(217, 248);
            tbOffset.Name = "tbOffset";
            tbOffset.Size = new Size(78, 23);
            tbOffset.TabIndex = 4;
            // 
            // cbParity
            // 
            cbParity.DropDownStyle = ComboBoxStyle.DropDownList;
            cbParity.FormattingEnabled = true;
            cbParity.Location = new Point(217, 184);
            cbParity.Name = "cbParity";
            cbParity.Size = new Size(140, 23);
            cbParity.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(217, 166);
            label4.Name = "label4";
            label4.Size = new Size(37, 15);
            label4.TabIndex = 3;
            label4.Tag = "fieldlabel";
            label4.Text = "Parity";
            // 
            // tpsettings
            // 
            tpsettings.BackColor = Color.FromArgb(245, 246, 250);
            tpsettings.Controls.Add(debug);
            tpsettings.Location = new Point(4, 24);
            tpsettings.Name = "tpsettings";
            tpsettings.Padding = new Padding(16, 14, 16, 8);
            tpsettings.Size = new Size(582, 290);
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
            tpadvanced.Size = new Size(582, 290);
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
            tpemail.Controls.Add(lbfrom);
            tpemail.Controls.Add(tbfrom);
            tpemail.Controls.Add(cbauthorisation);
            tpemail.Location = new Point(4, 24);
            tpemail.Name = "tpemail";
            tpemail.Padding = new Padding(16, 14, 16, 8);
            tpemail.Size = new Size(582, 290);
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
            cbauthorisation.Size = new Size(180, 19);
            cbauthorisation.TabIndex = 5;
            cbauthorisation.Text = "Requires SMTP Authorisation";
            //
            // lbfrom
            //
            lbfrom.AutoSize = true;
            lbfrom.Location = new Point(320, 170);
            lbfrom.Name = "lbfrom";
            lbfrom.Size = new Size(80, 15);
            lbfrom.TabIndex = 6;
            lbfrom.Tag = "fieldlabel";
            lbfrom.Text = "From Address";
            //
            // tbfrom
            //
            tbfrom.Location = new Point(320, 188);
            tbfrom.Name = "tbfrom";
            tbfrom.Size = new Size(250, 23);
            tbfrom.TabIndex = 6;
            //
            // tprsm
            // 
            tprsm.BackColor = Color.FromArgb(245, 246, 250);
            tprsm.Controls.Add(btnNodes);
            tprsm.Location = new Point(4, 24);
            tprsm.Name = "tprsm";
            tprsm.Padding = new Padding(16, 14, 16, 8);
            tprsm.Size = new Size(582, 290);
            tprsm.TabIndex = 4;
            tprsm.Text = "RSM";
            // 
            // btnNodes
            // 
            btnNodes.Location = new Point(140, 21);
            btnNodes.Margin = new Padding(3, 4, 3, 4);
            btnNodes.Name = "btnNodes";
            btnNodes.Size = new Size(114, 40);
            btnNodes.TabIndex = 3;
            btnNodes.Text = "Node Status";
            btnNodes.Click += btnNodes_Click;
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
            tbGent.Location = new Point(4, 24);
            tbGent.Name = "tbGent";
            tbGent.Padding = new Padding(3);
            tbGent.Size = new Size(582, 290);
            tbGent.TabIndex = 5;
            tbGent.Text = "Gent Panel";
            // 
            // ExtendedTextifOver
            // 
            ExtendedTextifOver.Location = new Point(205, 165);
            ExtendedTextifOver.Name = "ExtendedTextifOver";
            ExtendedTextifOver.Size = new Size(90, 23);
            ExtendedTextifOver.TabIndex = 12;
            // 
            // cboDelimiter
            // 
            cboDelimiter.FormattingEnabled = true;
            cboDelimiter.Items.AddRange(new object[] { "Comma", "Tab" });
            cboDelimiter.Location = new Point(205, 194);
            cboDelimiter.Name = "cboDelimiter";
            cboDelimiter.Size = new Size(90, 23);
            cboDelimiter.TabIndex = 11;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(56, 196);
            label9.Name = "label9";
            label9.Size = new Size(106, 15);
            label9.TabIndex = 10;
            label9.Text = "Delimter Character";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(56, 168);
            label8.Name = "label8";
            label8.Size = new Size(135, 15);
            label8.TabIndex = 9;
            label8.Text = "Use if Panel text is over -";
            // 
            // btBrowse
            // 
            btBrowse.Location = new Point(477, 124);
            btBrowse.Name = "btBrowse";
            btBrowse.Size = new Size(75, 23);
            btBrowse.TabIndex = 8;
            btBrowse.Text = "Browse";
            btBrowse.UseVisualStyleBackColor = true;
            btBrowse.Click += btBrowse_Click;
            // 
            // txtFilePath
            // 
            txtFilePath.Location = new Point(162, 125);
            txtFilePath.Name = "txtFilePath";
            txtFilePath.Size = new Size(309, 23);
            txtFilePath.TabIndex = 6;
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
            // tpInspire
            //
            tpInspire.Controls.Add(gbModuleOffset);
            tpInspire.Controls.Add(lblOffsetAmount);
            tpInspire.Controls.Add(tbInspireOffset);
            tpInspire.Location = new Point(4, 24);
            tpInspire.Name = "tpInspire";
            tpInspire.Padding = new Padding(3);
            tpInspire.Size = new Size(582, 290);
            tpInspire.TabIndex = 6;
            tpInspire.Text = "Inspire Panel";
            tpInspire.UseVisualStyleBackColor = true;
            //
            // gbModuleOffset
            //
            gbModuleOffset.Controls.Add(rbOffsetNode);
            gbModuleOffset.Controls.Add(rbOffsetLoop);
            gbModuleOffset.Location = new Point(90, 30);
            gbModuleOffset.Name = "gbModuleOffset";
            gbModuleOffset.Size = new Size(220, 80);
            gbModuleOffset.TabIndex = 0;
            gbModuleOffset.TabStop = false;
            gbModuleOffset.Text = "Module Offset";
            //
            // rbOffsetNode
            //
            rbOffsetNode.AutoSize = true;
            rbOffsetNode.Checked = true;
            rbOffsetNode.Location = new Point(24, 38);
            rbOffsetNode.Name = "rbOffsetNode";
            rbOffsetNode.Size = new Size(55, 19);
            rbOffsetNode.TabIndex = 0;
            rbOffsetNode.TabStop = true;
            rbOffsetNode.Text = "Node";
            //
            // rbOffsetLoop
            //
            rbOffsetLoop.AutoSize = true;
            rbOffsetLoop.Location = new Point(130, 38);
            rbOffsetLoop.Name = "rbOffsetLoop";
            rbOffsetLoop.Size = new Size(52, 19);
            rbOffsetLoop.TabIndex = 1;
            rbOffsetLoop.Text = "Loop";
            //
            // lblOffsetAmount
            //
            lblOffsetAmount.AutoSize = true;
            lblOffsetAmount.Location = new Point(90, 128);
            lblOffsetAmount.Name = "lblOffsetAmount";
            lblOffsetAmount.Size = new Size(86, 15);
            lblOffsetAmount.TabIndex = 1;
            lblOffsetAmount.Tag = "fieldlabel";
            lblOffsetAmount.Text = "Offset Amount";
            //
            // tbInspireOffset
            //
            tbInspireOffset.Location = new Point(200, 125);
            tbInspireOffset.Name = "tbInspireOffset";
            tbInspireOffset.Size = new Size(80, 23);
            tbInspireOffset.TabIndex = 2;
            //
            // frmSetup
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(590, 370);
            Controls.Add(tabPage);
            Controls.Add(pnlFooter);
            Icon = (Icon)resources.GetObject("$this.Icon");
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
        private Label lbfrom;
        private TextBox tbfrom;
        private CheckBox cbauthorisation;
        private TabPage tprsm;
        private Button btnNodes;
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
        private Label label10;
        private ComboBox cbStopBits;
        private TabPage tpInspire;
        private GroupBox gbModuleOffset;
        private RadioButton rbOffsetNode;
        private RadioButton rbOffsetLoop;
        private Label lblOffsetAmount;
        private TextBox tbInspireOffset;
    }
}