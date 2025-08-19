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
            label5 = new Label();
            label6 = new Label();
            tbOffset = new TextBox();
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
            cbComport.Location = new Point(141, 46);
            cbComport.Name = "cbComport";
            cbComport.Size = new Size(92, 28);
            cbComport.TabIndex = 3;
            cbComport.SelectedIndexChanged += cbComport_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(47, 54);
            label1.Name = "label1";
            label1.Size = new Size(83, 20);
            label1.TabIndex = 4;
            label1.Text = "Comm Port";
            // 
            // lbStatus
            // 
            lbStatus.AutoSize = true;
            lbStatus.Location = new Point(267, 49);
            lbStatus.Name = "lbStatus";
            lbStatus.Size = new Size(99, 20);
            lbStatus.TabIndex = 5;
            lbStatus.Text = "Disconnected";
            // 
            // tbBaudRate
            // 
            tbBaudRate.Location = new Point(141, 94);
            tbBaudRate.Name = "tbBaudRate";
            tbBaudRate.Size = new Size(92, 27);
            tbBaudRate.TabIndex = 6;
            tbBaudRate.TextChanged += tbBaudRate_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(47, 101);
            label2.Name = "label2";
            label2.Size = new Size(77, 20);
            label2.TabIndex = 7;
            label2.Text = "Baud Rate";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(47, 148);
            label3.Name = "label3";
            label3.Size = new Size(69, 20);
            label3.TabIndex = 9;
            label3.Text = "Data Bits";
            // 
            // tbDataBits
            // 
            tbDataBits.Location = new Point(141, 141);
            tbDataBits.Name = "tbDataBits";
            tbDataBits.Size = new Size(92, 27);
            tbDataBits.TabIndex = 8;
            tbDataBits.TextChanged += tbDataBits_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(47, 196);
            label4.Name = "label4";
            label4.Size = new Size(45, 20);
            label4.TabIndex = 11;
            label4.Text = "Parity";
            // 
            // cbParity
            // 
            cbParity.FormattingEnabled = true;
            cbParity.Location = new Point(141, 188);
            cbParity.Name = "cbParity";
            cbParity.Size = new Size(92, 28);
            cbParity.TabIndex = 10;
            cbParity.SelectedIndexChanged += cbParity_SelectedIndexChanged;
            // 
            // debug
            // 
            debug.AutoSize = true;
            debug.Location = new Point(141, 284);
            debug.Name = "debug";
            debug.Size = new Size(18, 17);
            debug.TabIndex = 12;
            debug.UseVisualStyleBackColor = true;
            debug.CheckedChanged += debug_CheckedChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(47, 284);
            label5.Name = "label5";
            label5.Size = new Size(83, 20);
            label5.TabIndex = 13;
            label5.Text = "Debug Log";
            // 
            // label6
            // 
            label6.AccessibleRole = AccessibleRole.OutlineButton;
            label6.AutoSize = true;
            label6.Location = new Point(47, 243);
            label6.Name = "label6";
            label6.Size = new Size(49, 20);
            label6.TabIndex = 15;
            label6.Text = "Offset";
            // 
            // tbOffset
            // 
            tbOffset.AccessibleRole = AccessibleRole.OutlineButton;
            tbOffset.Location = new Point(141, 236);
            tbOffset.Name = "tbOffset";
            tbOffset.Size = new Size(92, 27);
            tbOffset.TabIndex = 14;
            tbOffset.TextChanged += tbOffset_TextChanged;
            // 
            // frmSetup
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label6);
            Controls.Add(tbOffset);
            Controls.Add(label5);
            Controls.Add(debug);
            Controls.Add(label4);
            Controls.Add(cbParity);
            Controls.Add(label3);
            Controls.Add(tbDataBits);
            Controls.Add(label2);
            Controls.Add(tbBaudRate);
            Controls.Add(lbStatus);
            Controls.Add(label1);
            Controls.Add(cbComport);
            Controls.Add(btcancel);
            Controls.Add(btok);
            Controls.Add(btApply);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmSetup";
            Text = "Setup";
            Load += frmSetup_Load;
            ResumeLayout(false);
            PerformLayout();
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
        private Label label5;
        private Label label6;
        private TextBox tbOffset;
    }
}