namespace DraxClient
{
    partial class frmAbout
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
            label1 = new Label();
            btok = new Button();
            lblversion = new Label();
            lbcopyright = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F);
            label1.Location = new Point(144, 104);
            label1.Name = "label1";
            label1.Size = new Size(309, 25);
            label1.TabIndex = 0;
            label1.Text = "Drax Technology Network Manager";
            // 
            // btok
            // 
            btok.Location = new Point(595, 260);
            btok.Margin = new Padding(3, 2, 3, 2);
            btok.Name = "btok";
            btok.Size = new Size(94, 39);
            btok.TabIndex = 1;
            btok.Text = "OK";
            btok.UseVisualStyleBackColor = true;
            btok.Click += btok_Click;
            // 
            // lblversion
            // 
            lblversion.AutoSize = true;
            lblversion.Location = new Point(634, 316);
            lblversion.Name = "lblversion";
            lblversion.Size = new Size(27, 15);
            lblversion.TabIndex = 3;
            lblversion.Text = "xxxx";
            lblversion.TextAlign = ContentAlignment.TopCenter;
            // 
            // lbcopyright
            // 
            lbcopyright.AutoSize = true;
            lbcopyright.Location = new Point(144, 146);
            lbcopyright.Name = "lbcopyright";
            lbcopyright.Size = new Size(141, 15);
            lbcopyright.TabIndex = 4;
            lbcopyright.Text = "Copyright Drax Ltd - 2026";
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 10000;
            timer1.Tick += timer1_Tick;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(530, 9);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(159, 44);
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // frmAbout
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(700, 338);
            Controls.Add(pictureBox1);
            Controls.Add(lbcopyright);
            Controls.Add(lblversion);
            Controls.Add(btok);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmAbout";
            Text = "About Network Manager";
            Load += frmAbout_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btok;
        private Label lblversion;
        private Label lbcopyright;
        private System.Windows.Forms.Timer timer1;
        private PictureBox pictureBox1;
    }
}