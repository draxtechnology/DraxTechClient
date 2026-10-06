namespace DraxClient
{
    partial class frmTestBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTestBox));
            lbnode = new Label();
            lbloop = new Label();
            label3 = new Label();
            cbType = new ComboBox();
            btOn = new Button();
            btReset = new Button();
            btResetAll = new Button();
            tbNode = new NumericUpDown();
            tbLoop = new NumericUpDown();
            tbDevice = new NumericUpDown();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)tbNode).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbLoop).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbDevice).BeginInit();
            SuspendLayout();
            // 
            // lbnode
            // 
            lbnode.AutoSize = true;
            lbnode.Location = new Point(25, 29);
            lbnode.Name = "lbnode";
            lbnode.Size = new Size(36, 15);
            lbnode.TabIndex = 0;
            lbnode.Text = "Node";
            // 
            // lbloop
            // 
            lbloop.AutoSize = true;
            lbloop.Location = new Point(143, 29);
            lbloop.Name = "lbloop";
            lbloop.Size = new Size(34, 15);
            lbloop.TabIndex = 2;
            lbloop.Text = "Loop";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(256, 29);
            label3.Name = "label3";
            label3.Size = new Size(42, 15);
            label3.TabIndex = 4;
            label3.Text = "Device";
            // 
            // cbType
            // 
            cbType.FormattingEnabled = true;
            cbType.Location = new Point(31, 75);
            cbType.Margin = new Padding(3, 2, 3, 2);
            cbType.Name = "cbType";
            cbType.Size = new Size(133, 23);
            cbType.TabIndex = 6;
            // 
            // btOn
            // 
            btOn.BackColor = Color.FromArgb(26, 43, 74);
            btOn.FlatAppearance.BorderSize = 0;
            btOn.FlatStyle = FlatStyle.Flat;
            btOn.ForeColor = Color.White;
            btOn.Location = new Point(186, 76);
            btOn.Margin = new Padding(3, 2, 3, 2);
            btOn.Name = "btOn";
            btOn.Size = new Size(82, 22);
            btOn.TabIndex = 7;
            btOn.Text = "ON";
            btOn.UseVisualStyleBackColor = false;
            btOn.Click += btOn_Click;
            // 
            // btReset
            // 
            btReset.BackColor = Color.FromArgb(226, 75, 74);
            btReset.FlatAppearance.BorderSize = 0;
            btReset.FlatStyle = FlatStyle.Flat;
            btReset.ForeColor = Color.White;
            btReset.Location = new Point(297, 75);
            btReset.Margin = new Padding(3, 2, 3, 2);
            btReset.Name = "btReset";
            btReset.Size = new Size(82, 23);
            btReset.TabIndex = 8;
            btReset.Text = "RESET";
            btReset.UseVisualStyleBackColor = false;
            btReset.Click += btReset_Click;
            // 
            // btResetAll
            // 
            btResetAll.BackColor = Color.FromArgb(226, 75, 74);
            btResetAll.FlatAppearance.BorderSize = 0;
            btResetAll.FlatStyle = FlatStyle.Flat;
            btResetAll.ForeColor = Color.White;
            btResetAll.Location = new Point(297, 111);
            btResetAll.Margin = new Padding(3, 2, 3, 2);
            btResetAll.Name = "btResetAll";
            btResetAll.Size = new Size(82, 23);
            btResetAll.TabIndex = 12;
            btResetAll.Text = "RESET ALL";
            btResetAll.UseVisualStyleBackColor = false;
            btResetAll.Click += btResetAll_Click;
            // 
            // tbNode
            // 
            tbNode.Location = new Point(71, 24);
            tbNode.Margin = new Padding(3, 2, 3, 2);
            tbNode.Name = "tbNode";
            tbNode.Size = new Size(54, 23);
            tbNode.TabIndex = 9;
            // 
            // tbLoop
            // 
            tbLoop.Location = new Point(186, 24);
            tbLoop.Margin = new Padding(3, 2, 3, 2);
            tbLoop.Name = "tbLoop";
            tbLoop.Size = new Size(54, 23);
            tbLoop.TabIndex = 10;
            // 
            // tbDevice
            // 
            tbDevice.Location = new Point(310, 24);
            tbDevice.Margin = new Padding(3, 2, 3, 2);
            tbDevice.Name = "tbDevice";
            tbDevice.Size = new Size(53, 23);
            tbDevice.TabIndex = 11;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(226, 75, 74);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.White;
            button1.Location = new Point(297, 111);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(82, 23);
            button1.TabIndex = 12;
            button1.Text = "RESET ALL";
            button1.UseVisualStyleBackColor = false;
            button1.Click += btResetAll_Click;
            // 
            // frmTestBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(394, 147);
            Controls.Add(button1);
            Controls.Add(btResetAll);
            Controls.Add(tbDevice);
            Controls.Add(tbLoop);
            Controls.Add(tbNode);
            Controls.Add(btReset);
            Controls.Add(btOn);
            Controls.Add(cbType);
            Controls.Add(label3);
            Controls.Add(lbloop);
            Controls.Add(lbnode);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmTestBox";
            Text = "Test Box                                                                                                                                                     ";
            Load += frmTestBox_Load;
            ((System.ComponentModel.ISupportInitialize)tbNode).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbLoop).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbDevice).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbnode;
        private Label lbloop;
        private Label label3;
        private ComboBox cbType;
        private Button btOn;
        private Button btReset;
        private Button btResetAll;
        private NumericUpDown tbNode;
        private NumericUpDown tbLoop;
        private NumericUpDown tbDevice;
        private Button button1;
    }
}