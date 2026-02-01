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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetup));
            btApply = new Button();
            btok = new Button();
            btcancel = new Button();
            cbComport = new ComboBox();
            label1 = new Label();
            lbStatus = new Label();
            tbBaudRate = new TextBox();
            label2 = new Label();
            label3 = new Label();
            tbDataBits = new TextBox();
            label4 = new Label();
            cbParity = new ComboBox();
            debug = new CheckBox();
            label6 = new Label();
            tbOffset = new TextBox();
            tabControl1 = new TabControl();
            tpserialsettings = new TabPage();
            tpsettings = new TabPage();
            tpadvanced = new TabPage();
            chkClassicIsolations = new CheckBox();
            lblSubAddressOffsetNumber = new Label();
            SubAddressOffsetNumber = new TextBox();
            chkSubAddressOffset = new CheckBox();
            cboHB1 = new NumericUpDown();
            label5 = new Label();
            chkIgnoreNullZone = new CheckBox();
            chkDefaultZone = new CheckBox();
            chkRefreshZonesConfig = new CheckBox();
            chkRefreshZonesStart = new CheckBox();
            tpemail = new TabPage();
            cbauthorisation = new CheckBox();
            lbpassword = new Label();
            lbuser = new Label();
            tbpassword = new TextBox();
            tbuser = new TextBox();
            lbport = new Label();
            tbport = new TextBox();
            lbsmtpserver = new Label();
            tbname = new TextBox();
            bteditemailgroups = new Button();
            tprsm = new TabPage();
            frmListen = new Button();
            btdiscovery = new Button();
            tabControl1.SuspendLayout();
            tpserialsettings.SuspendLayout();
            tpsettings.SuspendLayout();
            tpadvanced.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cboHB1).BeginInit();
            tpemail.SuspendLayout();
            tprsm.SuspendLayout();
            SuspendLayout();
            // 
            // btApply
            // 
            btApply.Location = new Point(520, 307);
            btApply.Margin = new Padding(3, 2, 3, 2);
            btApply.Name = "btApply";
            btApply.Size = new Size(82, 22);
            btApply.TabIndex = 0;
            btApply.Text = "Apply";
            btApply.UseVisualStyleBackColor = true;
            btApply.Click += btApply_Click;
            // 
            // btok
            // 
            btok.Location = new Point(432, 307);
            btok.Margin = new Padding(3, 2, 3, 2);
            btok.Name = "btok";
            btok.Size = new Size(82, 22);
            btok.TabIndex = 1;
            btok.Text = "OK";
            btok.UseVisualStyleBackColor = true;
            btok.Click += btok_Click;
            // 
            // btcancel
            // 
            btcancel.Location = new Point(607, 307);
            btcancel.Margin = new Padding(3, 2, 3, 2);
            btcancel.Name = "btcancel";
            btcancel.Size = new Size(82, 22);
            btcancel.TabIndex = 2;
            btcancel.Text = "Cancel";
            btcancel.UseVisualStyleBackColor = true;
            btcancel.Click += btcancel_Click;
            // 
            // cbComport
            // 
            cbComport.FormattingEnabled = true;
            cbComport.Location = new Point(122, 42);
            cbComport.Margin = new Padding(3, 2, 3, 2);
            cbComport.Name = "cbComport";
            cbComport.Size = new Size(81, 23);
            cbComport.TabIndex = 3;
            cbComport.SelectedIndexChanged += cbComport_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 48);
            label1.Name = "label1";
            label1.Size = new Size(69, 15);
            label1.TabIndex = 4;
            label1.Text = "Comm Port";
            // 
            // lbStatus
            // 
            lbStatus.AutoSize = true;
            lbStatus.Location = new Point(280, 16);
            lbStatus.Name = "lbStatus";
            lbStatus.Size = new Size(79, 15);
            lbStatus.TabIndex = 5;
            lbStatus.Text = "Disconnected";
            // 
            // tbBaudRate
            // 
            tbBaudRate.Location = new Point(122, 78);
            tbBaudRate.Margin = new Padding(3, 2, 3, 2);
            tbBaudRate.Name = "tbBaudRate";
            tbBaudRate.Size = new Size(81, 23);
            tbBaudRate.TabIndex = 6;
            tbBaudRate.TextChanged += tbBaudRate_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(21, 83);
            label2.Name = "label2";
            label2.Size = new Size(60, 15);
            label2.TabIndex = 7;
            label2.Text = "Baud Rate";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(21, 115);
            label3.Name = "label3";
            label3.Size = new Size(53, 15);
            label3.TabIndex = 9;
            label3.Text = "Data Bits";
            // 
            // tbDataBits
            // 
            tbDataBits.Location = new Point(122, 110);
            tbDataBits.Margin = new Padding(3, 2, 3, 2);
            tbDataBits.Name = "tbDataBits";
            tbDataBits.Size = new Size(81, 23);
            tbDataBits.TabIndex = 8;
            tbDataBits.TextChanged += tbDataBits_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(21, 154);
            label4.Name = "label4";
            label4.Size = new Size(37, 15);
            label4.TabIndex = 11;
            label4.Text = "Parity";
            // 
            // cbParity
            // 
            cbParity.FormattingEnabled = true;
            cbParity.Location = new Point(122, 148);
            cbParity.Margin = new Padding(3, 2, 3, 2);
            cbParity.Name = "cbParity";
            cbParity.Size = new Size(81, 23);
            cbParity.TabIndex = 10;
            cbParity.SelectedIndexChanged += cbParity_SelectedIndexChanged;
            // 
            // debug
            // 
            debug.AutoSize = true;
            debug.Location = new Point(32, 20);
            debug.Margin = new Padding(3, 2, 3, 2);
            debug.Name = "debug";
            debug.Size = new Size(84, 19);
            debug.TabIndex = 12;
            debug.Text = "Debug Log";
            debug.UseVisualStyleBackColor = true;
            debug.CheckedChanged += debug_CheckedChanged;
            // 
            // label6
            // 
            label6.AccessibleRole = AccessibleRole.OutlineButton;
            label6.AutoSize = true;
            label6.Location = new Point(21, 190);
            label6.Name = "label6";
            label6.Size = new Size(39, 15);
            label6.TabIndex = 15;
            label6.Text = "Offset";
            // 
            // tbOffset
            // 
            tbOffset.AccessibleRole = AccessibleRole.OutlineButton;
            tbOffset.Location = new Point(122, 184);
            tbOffset.Margin = new Padding(3, 2, 3, 2);
            tbOffset.Name = "tbOffset";
            tbOffset.Size = new Size(81, 23);
            tbOffset.TabIndex = 14;
            tbOffset.TextChanged += tbOffset_TextChanged;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tpserialsettings);
            tabControl1.Controls.Add(tpsettings);
            tabControl1.Controls.Add(tpadvanced);
            tabControl1.Controls.Add(tpemail);
            tabControl1.Controls.Add(tprsm);
            tabControl1.Location = new Point(4, 0);
            tabControl1.Margin = new Padding(3, 2, 3, 2);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(701, 296);
            tabControl1.TabIndex = 16;
            // 
            // tpserialsettings
            // 
            tpserialsettings.Controls.Add(cbComport);
            tpserialsettings.Controls.Add(label6);
            tpserialsettings.Controls.Add(label1);
            tpserialsettings.Controls.Add(tbOffset);
            tpserialsettings.Controls.Add(tbBaudRate);
            tpserialsettings.Controls.Add(label2);
            tpserialsettings.Controls.Add(lbStatus);
            tpserialsettings.Controls.Add(label4);
            tpserialsettings.Controls.Add(tbDataBits);
            tpserialsettings.Controls.Add(cbParity);
            tpserialsettings.Controls.Add(label3);
            tpserialsettings.Location = new Point(4, 24);
            tpserialsettings.Margin = new Padding(3, 2, 3, 2);
            tpserialsettings.Name = "tpserialsettings";
            tpserialsettings.Padding = new Padding(3, 2, 3, 2);
            tpserialsettings.Size = new Size(693, 268);
            tpserialsettings.TabIndex = 0;
            tpserialsettings.Text = "Serial Settings";
            tpserialsettings.UseVisualStyleBackColor = true;
            // 
            // tpsettings
            // 
            tpsettings.Controls.Add(debug);
            tpsettings.Location = new Point(4, 24);
            tpsettings.Margin = new Padding(3, 2, 3, 2);
            tpsettings.Name = "tpsettings";
            tpsettings.Padding = new Padding(3, 2, 3, 2);
            tpsettings.Size = new Size(693, 268);
            tpsettings.TabIndex = 2;
            tpsettings.Text = "Settings";
            tpsettings.UseVisualStyleBackColor = true;
            // 
            // tpadvanced
            // 
            tpadvanced.Controls.Add(chkClassicIsolations);
            tpadvanced.Controls.Add(lblSubAddressOffsetNumber);
            tpadvanced.Controls.Add(SubAddressOffsetNumber);
            tpadvanced.Controls.Add(chkSubAddressOffset);
            tpadvanced.Controls.Add(cboHB1);
            tpadvanced.Controls.Add(label5);
            tpadvanced.Controls.Add(chkIgnoreNullZone);
            tpadvanced.Controls.Add(chkDefaultZone);
            tpadvanced.Controls.Add(chkRefreshZonesConfig);
            tpadvanced.Controls.Add(chkRefreshZonesStart);
            tpadvanced.Location = new Point(4, 24);
            tpadvanced.Margin = new Padding(3, 2, 3, 2);
            tpadvanced.Name = "tpadvanced";
            tpadvanced.Padding = new Padding(3, 2, 3, 2);
            tpadvanced.Size = new Size(693, 268);
            tpadvanced.TabIndex = 1;
            tpadvanced.Text = "Advanced Panel";
            tpadvanced.UseVisualStyleBackColor = true;
            // 
            // chkClassicIsolations
            // 
            chkClassicIsolations.AutoSize = true;
            chkClassicIsolations.Location = new Point(26, 104);
            chkClassicIsolations.Margin = new Padding(3, 2, 3, 2);
            chkClassicIsolations.Name = "chkClassicIsolations";
            chkClassicIsolations.Size = new Size(115, 19);
            chkClassicIsolations.TabIndex = 18;
            chkClassicIsolations.Text = "Classic Isolations";
            chkClassicIsolations.UseVisualStyleBackColor = true;
            // 
            // lblSubAddressOffsetNumber
            // 
            lblSubAddressOffsetNumber.AccessibleRole = AccessibleRole.OutlineButton;
            lblSubAddressOffsetNumber.AutoSize = true;
            lblSubAddressOffsetNumber.Location = new Point(27, 192);
            lblSubAddressOffsetNumber.Name = "lblSubAddressOffsetNumber";
            lblSubAddressOffsetNumber.Size = new Size(107, 15);
            lblSubAddressOffsetNumber.TabIndex = 17;
            lblSubAddressOffsetNumber.Text = "Sub Address Offset";
            lblSubAddressOffsetNumber.Visible = false;
            // 
            // SubAddressOffsetNumber
            // 
            SubAddressOffsetNumber.AccessibleRole = AccessibleRole.Clock;
            SubAddressOffsetNumber.Location = new Point(167, 187);
            SubAddressOffsetNumber.Margin = new Padding(3, 2, 3, 2);
            SubAddressOffsetNumber.Name = "SubAddressOffsetNumber";
            SubAddressOffsetNumber.Size = new Size(81, 23);
            SubAddressOffsetNumber.TabIndex = 16;
            SubAddressOffsetNumber.Visible = false;
            // 
            // chkSubAddressOffset
            // 
            chkSubAddressOffset.AutoSize = true;
            chkSubAddressOffset.Location = new Point(26, 164);
            chkSubAddressOffset.Margin = new Padding(3, 2, 3, 2);
            chkSubAddressOffset.Name = "chkSubAddressOffset";
            chkSubAddressOffset.Size = new Size(126, 19);
            chkSubAddressOffset.TabIndex = 8;
            chkSubAddressOffset.TabStop = false;
            chkSubAddressOffset.Text = "Sub Address Offset";
            chkSubAddressOffset.UseVisualStyleBackColor = true;
            chkSubAddressOffset.CheckedChanged += chkSubAddressOffset_CheckedChanged;
            // 
            // cboHB1
            // 
            cboHB1.Location = new Point(167, 132);
            cboHB1.Margin = new Padding(3, 2, 3, 2);
            cboHB1.Name = "cboHB1";
            cboHB1.Size = new Size(65, 23);
            cboHB1.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(26, 134);
            label5.Name = "label5";
            label5.Size = new Size(91, 15);
            label5.TabIndex = 5;
            label5.Text = "Heartbeat Panel";
            // 
            // chkIgnoreNullZone
            // 
            chkIgnoreNullZone.AutoSize = true;
            chkIgnoreNullZone.Location = new Point(26, 81);
            chkIgnoreNullZone.Margin = new Padding(3, 2, 3, 2);
            chkIgnoreNullZone.Name = "chkIgnoreNullZone";
            chkIgnoreNullZone.Size = new Size(139, 19);
            chkIgnoreNullZone.TabIndex = 3;
            chkIgnoreNullZone.Text = "Ignore Null Zone Text";
            chkIgnoreNullZone.UseVisualStyleBackColor = true;
            // 
            // chkDefaultZone
            // 
            chkDefaultZone.AutoSize = true;
            chkDefaultZone.Location = new Point(26, 58);
            chkDefaultZone.Margin = new Padding(3, 2, 3, 2);
            chkDefaultZone.Name = "chkDefaultZone";
            chkDefaultZone.Size = new Size(118, 19);
            chkDefaultZone.TabIndex = 2;
            chkDefaultZone.Text = "Default Zone Text";
            chkDefaultZone.UseVisualStyleBackColor = true;
            // 
            // chkRefreshZonesConfig
            // 
            chkRefreshZonesConfig.AutoSize = true;
            chkRefreshZonesConfig.Location = new Point(26, 36);
            chkRefreshZonesConfig.Margin = new Padding(3, 2, 3, 2);
            chkRefreshZonesConfig.Name = "chkRefreshZonesConfig";
            chkRefreshZonesConfig.Size = new Size(262, 19);
            chkRefreshZonesConfig.TabIndex = 1;
            chkRefreshZonesConfig.Text = "Refresh Zone Texts on Configuration Change";
            chkRefreshZonesConfig.UseVisualStyleBackColor = true;
            // 
            // chkRefreshZonesStart
            // 
            chkRefreshZonesStart.AutoSize = true;
            chkRefreshZonesStart.Location = new Point(26, 14);
            chkRefreshZonesStart.Margin = new Padding(3, 2, 3, 2);
            chkRefreshZonesStart.Name = "chkRefreshZonesStart";
            chkRefreshZonesStart.Size = new Size(182, 19);
            chkRefreshZonesStart.TabIndex = 0;
            chkRefreshZonesStart.Text = "Refresh Zone Texts on Startup";
            chkRefreshZonesStart.UseVisualStyleBackColor = true;
            // 
            // tpemail
            // 
            tpemail.Controls.Add(cbauthorisation);
            tpemail.Controls.Add(lbpassword);
            tpemail.Controls.Add(lbuser);
            tpemail.Controls.Add(tbpassword);
            tpemail.Controls.Add(tbuser);
            tpemail.Controls.Add(lbport);
            tpemail.Controls.Add(tbport);
            tpemail.Controls.Add(lbsmtpserver);
            tpemail.Controls.Add(tbname);
            tpemail.Controls.Add(bteditemailgroups);
            tpemail.Location = new Point(4, 24);
            tpemail.Name = "tpemail";
            tpemail.Padding = new Padding(3);
            tpemail.Size = new Size(693, 268);
            tpemail.TabIndex = 3;
            tpemail.Text = "Email Panel";
            tpemail.UseVisualStyleBackColor = true;
            // 
            // cbauthorisation
            // 
            cbauthorisation.AutoSize = true;
            cbauthorisation.Location = new Point(12, 168);
            cbauthorisation.Name = "cbauthorisation";
            cbauthorisation.Size = new Size(98, 19);
            cbauthorisation.TabIndex = 8;
            cbauthorisation.Text = "Auhtorisation";
            cbauthorisation.UseVisualStyleBackColor = true;
            // 
            // lbpassword
            // 
            lbpassword.AutoSize = true;
            lbpassword.Location = new Point(303, 109);
            lbpassword.Name = "lbpassword";
            lbpassword.Size = new Size(60, 15);
            lbpassword.TabIndex = 7;
            lbpassword.Text = "Password:";
            // 
            // lbuser
            // 
            lbuser.AutoSize = true;
            lbuser.Location = new Point(303, 59);
            lbuser.Name = "lbuser";
            lbuser.Size = new Size(33, 15);
            lbuser.TabIndex = 7;
            lbuser.Text = "User:";
            // 
            // tbpassword
            // 
            tbpassword.Location = new Point(303, 127);
            tbpassword.Name = "tbpassword";
            tbpassword.PasswordChar = '*';
            tbpassword.Size = new Size(225, 23);
            tbpassword.TabIndex = 6;
            // 
            // tbuser
            // 
            tbuser.Location = new Point(303, 77);
            tbuser.Name = "tbuser";
            tbuser.Size = new Size(225, 23);
            tbuser.TabIndex = 6;
            // 
            // lbport
            // 
            lbport.AutoSize = true;
            lbport.Location = new Point(12, 109);
            lbport.Name = "lbport";
            lbport.Size = new Size(32, 15);
            lbport.TabIndex = 5;
            lbport.Text = "Port:";
            // 
            // tbport
            // 
            tbport.Location = new Point(12, 127);
            tbport.Name = "tbport";
            tbport.Size = new Size(225, 23);
            tbport.TabIndex = 4;
            // 
            // lbsmtpserver
            // 
            lbsmtpserver.AutoSize = true;
            lbsmtpserver.Location = new Point(12, 59);
            lbsmtpserver.Name = "lbsmtpserver";
            lbsmtpserver.Size = new Size(76, 15);
            lbsmtpserver.TabIndex = 3;
            lbsmtpserver.Text = "SMTP Server:";
            // 
            // tbname
            // 
            tbname.Location = new Point(12, 77);
            tbname.Name = "tbname";
            tbname.Size = new Size(225, 23);
            tbname.TabIndex = 2;
            // 
            // bteditemailgroups
            // 
            bteditemailgroups.Location = new Point(12, 20);
            bteditemailgroups.Name = "bteditemailgroups";
            bteditemailgroups.Size = new Size(75, 23);
            bteditemailgroups.TabIndex = 0;
            bteditemailgroups.Text = "Groups";
            bteditemailgroups.UseVisualStyleBackColor = true;
            bteditemailgroups.Click += bteditemailgroups_Click;
            // 
            // tprsm
            // 
            tprsm.Controls.Add(frmListen);
            tprsm.Controls.Add(btdiscovery);
            tprsm.Location = new Point(4, 24);
            tprsm.Name = "tprsm";
            tprsm.Size = new Size(693, 268);
            tprsm.TabIndex = 4;
            tprsm.Text = "RSM Panel";
            tprsm.UseVisualStyleBackColor = true;
            // 
            // frmListen
            // 
            frmListen.Location = new Point(15, 63);
            frmListen.Name = "frmListen";
            frmListen.Size = new Size(75, 23);
            frmListen.TabIndex = 2;
            frmListen.Text = "Listen";
            frmListen.UseVisualStyleBackColor = true;
            frmListen.Click += frmListen_Click;
            // 
            // btdiscovery
            // 
            btdiscovery.Location = new Point(15, 13);
            btdiscovery.Name = "btdiscovery";
            btdiscovery.Size = new Size(75, 23);
            btdiscovery.TabIndex = 1;
            btdiscovery.Text = "Discovery";
            btdiscovery.UseVisualStyleBackColor = true;
            btdiscovery.Click += btdiscovery_Click;
            // 
            // frmSetup
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 338);
            Controls.Add(tabControl1);
            Controls.Add(btcancel);
            Controls.Add(btok);
            Controls.Add(btApply);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmSetup";
            Text = "Setup";
            Load += frmSetup_Load;
            tabControl1.ResumeLayout(false);
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
            ResumeLayout(false);
        }

        #endregion

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
        private TabControl tabControl1;
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
    }
}