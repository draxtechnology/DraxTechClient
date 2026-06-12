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
            btcopy = new Button();
            lblversion = new Label();
            lbcopyright = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            pictureBox1 = new PictureBox();
            lblpanelversion = new Label();
            lblclientversion = new Label();
            label2 = new Label();
            bindingSource1 = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            SuspendLayout();
            // 
            // btcopy
            // 
            btcopy.Location = new Point(197, 259);
            btcopy.Margin = new Padding(3, 2, 3, 2);
            btcopy.Name = "btcopy";
            btcopy.Size = new Size(190, 39);
            btcopy.TabIndex = 8;
            btcopy.Text = "Copy Version Info";
            btcopy.UseVisualStyleBackColor = true;
            btcopy.Click += btcopy_Click;
            // 
            // lblversion
            // 
            lblversion.AutoSize = true;
            lblversion.Location = new Point(206, 168);
            lblversion.Name = "lblversion";
            lblversion.Size = new Size(27, 15);
            lblversion.TabIndex = 3;
            lblversion.Text = "xxxx";
            lblversion.TextAlign = ContentAlignment.TopCenter;
            // 
            // lbcopyright
            // 
            lbcopyright.AutoSize = true;
            lbcopyright.Location = new Point(206, 314);
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
            pictureBox1.Location = new Point(197, 8);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(190, 59);
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // lblpanelversion
            // 
            lblpanelversion.AutoSize = true;
            lblpanelversion.Location = new Point(206, 132);
            lblpanelversion.Name = "lblpanelversion";
            lblpanelversion.Size = new Size(27, 15);
            lblpanelversion.TabIndex = 6;
            lblpanelversion.Text = "xxxx";
            lblpanelversion.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblclientversion
            // 
            lblclientversion.AutoSize = true;
            lblclientversion.Location = new Point(206, 204);
            lblclientversion.Name = "lblclientversion";
            lblclientversion.Size = new Size(27, 15);
            lblclientversion.TabIndex = 7;
            lblclientversion.Text = "xxxx";
            lblclientversion.TextAlign = ContentAlignment.TopCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14F);
            label2.Location = new Point(206, 85);
            label2.Name = "label2";
            label2.Size = new Size(164, 25);
            label2.TabIndex = 9;
            label2.Text = "Network Manager";
            // 
            // frmAbout
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(537, 338);
            Controls.Add(label2);
            Controls.Add(lblclientversion);
            Controls.Add(lblpanelversion);
            Controls.Add(pictureBox1);
            Controls.Add(lbcopyright);
            Controls.Add(lblversion);
            Controls.Add(btcopy);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmAbout";
            Text = "About Network Manager";
            Load += frmAbout_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btcopy;
        private Label lblversion;
        private Label lbcopyright;
        private System.Windows.Forms.Timer timer1;
        private PictureBox pictureBox1;
        private Label lblpanelversion;
        private Label lblclientversion;
        private Label label2;
        private BindingSource bindingSource1;
    }
}