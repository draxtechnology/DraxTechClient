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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            cbType = new ComboBox();
            btOn = new Button();
            btReset = new Button();
            tbNode = new NumericUpDown();
            tbLoop = new NumericUpDown();
            tbDevice = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)tbNode).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbLoop).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbDevice).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(29, 39);
            label1.Name = "label1";
            label1.Size = new Size(46, 20);
            label1.TabIndex = 0;
            label1.Text = "Node";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(163, 39);
            label2.Name = "label2";
            label2.Size = new Size(43, 20);
            label2.TabIndex = 2;
            label2.Text = "Loop";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(293, 39);
            label3.Name = "label3";
            label3.Size = new Size(54, 20);
            label3.TabIndex = 4;
            label3.Text = "Device";
            // 
            // cbType
            // 
            cbType.FormattingEnabled = true;
            cbType.Location = new Point(35, 100);
            cbType.Name = "cbType";
            cbType.Size = new Size(151, 28);
            cbType.TabIndex = 6;
            // 
            // btOn
            // 
            btOn.Location = new Point(212, 99);
            btOn.Name = "btOn";
            btOn.Size = new Size(94, 29);
            btOn.TabIndex = 7;
            btOn.Text = "ON";
            btOn.UseVisualStyleBackColor = true;
            btOn.Click += btOn_Click;
            // 
            // btReset
            // 
            btReset.Location = new Point(339, 97);
            btReset.Name = "btReset";
            btReset.Size = new Size(76, 31);
            btReset.TabIndex = 8;
            btReset.Text = "RESET";
            btReset.UseVisualStyleBackColor = true;
            btReset.Click += btReset_Click;
            // 
            // tbNode
            // 
            tbNode.Location = new Point(81, 32);
            tbNode.Name = "tbNode";
            tbNode.Size = new Size(62, 27);
            tbNode.TabIndex = 9;
            // 
            // tbLoop
            // 
            tbLoop.Location = new Point(212, 32);
            tbLoop.Name = "tbLoop";
            tbLoop.Size = new Size(62, 27);
            tbLoop.TabIndex = 10;
            // 
            // tbDevice
            // 
            tbDevice.Location = new Point(354, 32);
            tbDevice.Name = "tbDevice";
            tbDevice.Size = new Size(61, 27);
            tbDevice.TabIndex = 11;
            // 
            // frmTestBox
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(448, 162);
            Controls.Add(tbDevice);
            Controls.Add(tbLoop);
            Controls.Add(tbNode);
            Controls.Add(btReset);
            Controls.Add(btOn);
            Controls.Add(cbType);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
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

        private Label label1;
        private Label label2;
        private Label label3;
        private ComboBox cbType;
        private Button btOn;
        private Button btReset;
        private NumericUpDown tbNode;
        private NumericUpDown tbLoop;
        private NumericUpDown tbDevice;
    }
}