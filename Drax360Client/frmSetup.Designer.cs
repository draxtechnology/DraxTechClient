namespace Drax360Client
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
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            label7 = new Label();
            SubAddressOffsetNumber = new TextBox();
            chkSubAddressOffset = new CheckBox();
            cboHB1 = new NumericUpDown();
            label5 = new Label();
            chkIgnoreNullZone = new CheckBox();
            chkDefaultZone = new CheckBox();
            chkRefreshZonesConfig = new CheckBox();
            chkRefreshZonesStart = new CheckBox();
            tabPage3 = new TabPage();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cboHB1).BeginInit();
            tabPage3.SuspendLayout();
            SuspendLayout();
            // 
            // btApply
            // 
            btApply.Location = new Point(594, 409);
            btApply.Name = "btApply";
            btApply.Size = new Size(94, 29);
            btApply.TabIndex = 0;
            btApply.Text = "Apply";
            btApply.UseVisualStyleBackColor = true;
            btApply.Click += btApply_Click;
            // 
            // btok
            // 
            btok.Location = new Point(494, 409);
            btok.Name = "btok";
            btok.Size = new Size(94, 29);
            btok.TabIndex = 1;
            btok.Text = "OK";
            btok.UseVisualStyleBackColor = true;
            btok.Click += btok_Click;
            // 
            // btcancel
            // 
            btcancel.Location = new Point(694, 409);
            btcancel.Name = "btcancel";
            btcancel.Size = new Size(94, 29);
            btcancel.TabIndex = 2;
            btcancel.Text = "Cancel";
            btcancel.UseVisualStyleBackColor = true;
            btcancel.Click += btcancel_Click;
            // 
            // cbComport
            // 
            cbComport.FormattingEnabled = true;
            cbComport.Location = new Point(140, 56);
            cbComport.Name = "cbComport";
            cbComport.Size = new Size(92, 28);
            cbComport.TabIndex = 3;
            cbComport.SelectedIndexChanged += cbComport_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 64);
            label1.Name = "label1";
            label1.Size = new Size(83, 20);
            label1.TabIndex = 4;
            label1.Text = "Comm Port";
            // 
            // lbStatus
            // 
            lbStatus.AutoSize = true;
            lbStatus.Location = new Point(320, 22);
            lbStatus.Name = "lbStatus";
            lbStatus.Size = new Size(99, 20);
            lbStatus.TabIndex = 5;
            lbStatus.Text = "Disconnected";
            // 
            // tbBaudRate
            // 
            tbBaudRate.Location = new Point(140, 104);
            tbBaudRate.Name = "tbBaudRate";
            tbBaudRate.Size = new Size(92, 27);
            tbBaudRate.TabIndex = 6;
            tbBaudRate.TextChanged += tbBaudRate_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(24, 111);
            label2.Name = "label2";
            label2.Size = new Size(77, 20);
            label2.TabIndex = 7;
            label2.Text = "Baud Rate";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(24, 153);
            label3.Name = "label3";
            label3.Size = new Size(69, 20);
            label3.TabIndex = 9;
            label3.Text = "Data Bits";
            // 
            // tbDataBits
            // 
            tbDataBits.Location = new Point(140, 146);
            tbDataBits.Name = "tbDataBits";
            tbDataBits.Size = new Size(92, 27);
            tbDataBits.TabIndex = 8;
            tbDataBits.TextChanged += tbDataBits_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(24, 206);
            label4.Name = "label4";
            label4.Size = new Size(45, 20);
            label4.TabIndex = 11;
            label4.Text = "Parity";
            // 
            // cbParity
            // 
            cbParity.FormattingEnabled = true;
            cbParity.Location = new Point(140, 198);
            cbParity.Name = "cbParity";
            cbParity.Size = new Size(92, 28);
            cbParity.TabIndex = 10;
            cbParity.SelectedIndexChanged += cbParity_SelectedIndexChanged;
            // 
            // debug
            // 
            debug.AutoSize = true;
            debug.Location = new Point(37, 27);
            debug.Name = "debug";
            debug.Size = new Size(105, 24);
            debug.TabIndex = 12;
            debug.Text = "Debug Log";
            debug.UseVisualStyleBackColor = true;
            debug.CheckedChanged += debug_CheckedChanged;
            // 
            // label6
            // 
            label6.AccessibleRole = AccessibleRole.OutlineButton;
            label6.AutoSize = true;
            label6.Location = new Point(24, 253);
            label6.Name = "label6";
            label6.Size = new Size(49, 20);
            label6.TabIndex = 15;
            label6.Text = "Offset";
            // 
            // tbOffset
            // 
            tbOffset.AccessibleRole = AccessibleRole.OutlineButton;
            tbOffset.Location = new Point(140, 246);
            tbOffset.Name = "tbOffset";
            tbOffset.Size = new Size(92, 27);
            tbOffset.TabIndex = 14;
            tbOffset.TextChanged += tbOffset_TextChanged;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Location = new Point(4, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(801, 395);
            tabControl1.TabIndex = 16;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(cbComport);
            tabPage1.Controls.Add(label6);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(tbOffset);
            tabPage1.Controls.Add(tbBaudRate);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(lbStatus);
            tabPage1.Controls.Add(label4);
            tabPage1.Controls.Add(tbDataBits);
            tabPage1.Controls.Add(cbParity);
            tabPage1.Controls.Add(label3);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(793, 362);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Serial Settings";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(label7);
            tabPage2.Controls.Add(SubAddressOffsetNumber);
            tabPage2.Controls.Add(chkSubAddressOffset);
            tabPage2.Controls.Add(cboHB1);
            tabPage2.Controls.Add(label5);
            tabPage2.Controls.Add(chkIgnoreNullZone);
            tabPage2.Controls.Add(chkDefaultZone);
            tabPage2.Controls.Add(chkRefreshZonesConfig);
            tabPage2.Controls.Add(chkRefreshZonesStart);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(793, 362);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Advanced Panel";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AccessibleRole = AccessibleRole.OutlineButton;
            label7.AutoSize = true;
            label7.Location = new Point(31, 256);
            label7.Name = "label7";
            label7.Size = new Size(135, 20);
            label7.TabIndex = 17;
            label7.Text = "Sub Address Offset";
            // 
            // SubAddressOffsetNumber
            // 
            SubAddressOffsetNumber.AccessibleRole = AccessibleRole.OutlineButton;
            SubAddressOffsetNumber.Location = new Point(191, 249);
            SubAddressOffsetNumber.Name = "SubAddressOffsetNumber";
            SubAddressOffsetNumber.Size = new Size(92, 27);
            SubAddressOffsetNumber.TabIndex = 16;
            // 
            // chkSubAddressOffset
            // 
            chkSubAddressOffset.AutoSize = true;
            chkSubAddressOffset.Location = new Point(30, 219);
            chkSubAddressOffset.Name = "chkSubAddressOffset";
            chkSubAddressOffset.Size = new Size(157, 24);
            chkSubAddressOffset.TabIndex = 8;
            chkSubAddressOffset.TabStop = false;
            chkSubAddressOffset.Text = "Sub Address Offset";
            chkSubAddressOffset.UseVisualStyleBackColor = true;
            chkSubAddressOffset.CheckedChanged += chkSubAddressOffset_CheckedChanged;
            // 
            // cboHB1
            // 
            cboHB1.Location = new Point(191, 150);
            cboHB1.Name = "cboHB1";
            cboHB1.Size = new Size(74, 27);
            cboHB1.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(30, 152);
            label5.Name = "label5";
            label5.Size = new Size(115, 20);
            label5.TabIndex = 5;
            label5.Text = "Heartbeat Panel";
            // 
            // chkIgnoreNullZone
            // 
            chkIgnoreNullZone.AutoSize = true;
            chkIgnoreNullZone.Location = new Point(30, 108);
            chkIgnoreNullZone.Name = "chkIgnoreNullZone";
            chkIgnoreNullZone.Size = new Size(174, 24);
            chkIgnoreNullZone.TabIndex = 3;
            chkIgnoreNullZone.Text = "Ignore Null Zone Text";
            chkIgnoreNullZone.UseVisualStyleBackColor = true;
            // 
            // chkDefaultZone
            // 
            chkDefaultZone.AutoSize = true;
            chkDefaultZone.Location = new Point(30, 78);
            chkDefaultZone.Name = "chkDefaultZone";
            chkDefaultZone.Size = new Size(149, 24);
            chkDefaultZone.TabIndex = 2;
            chkDefaultZone.Text = "Default Zone Text";
            chkDefaultZone.UseVisualStyleBackColor = true;
            // 
            // chkRefreshZonesConfig
            // 
            chkRefreshZonesConfig.AutoSize = true;
            chkRefreshZonesConfig.Location = new Point(30, 48);
            chkRefreshZonesConfig.Name = "chkRefreshZonesConfig";
            chkRefreshZonesConfig.Size = new Size(325, 24);
            chkRefreshZonesConfig.TabIndex = 1;
            chkRefreshZonesConfig.Text = "Refresh Zone Texts on Configuration Change";
            chkRefreshZonesConfig.UseVisualStyleBackColor = true;
            // 
            // chkRefreshZonesStart
            // 
            chkRefreshZonesStart.AutoSize = true;
            chkRefreshZonesStart.Location = new Point(30, 18);
            chkRefreshZonesStart.Name = "chkRefreshZonesStart";
            chkRefreshZonesStart.Size = new Size(228, 24);
            chkRefreshZonesStart.TabIndex = 0;
            chkRefreshZonesStart.Text = "Refresh Zone Texts on Startup";
            chkRefreshZonesStart.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(debug);
            tabPage3.Location = new Point(4, 29);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(793, 362);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Settings";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // frmSetup
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl1);
            Controls.Add(btcancel);
            Controls.Add(btok);
            Controls.Add(btApply);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmSetup";
            Text = "Setup";
            Load += frmSetup_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)cboHB1).EndInit();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
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
        private TabPage tabPage1;
        private TabPage tabPage2;
        private CheckBox chkDefaultZone;
        private CheckBox chkRefreshZonesConfig;
        private CheckBox chkRefreshZonesStart;
        private TabPage tabPage3;
        private CheckBox chkIgnoreNullZone;
        private Label label5;
        private NumericUpDown cboHB1;
        private CheckBox chkSubAddressOffset;
        private Label label7;
        private TextBox SubAddressOffsetNumber;
    }
}