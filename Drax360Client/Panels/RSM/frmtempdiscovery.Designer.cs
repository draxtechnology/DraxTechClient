namespace DraxClient.Panels.RSM
{
    partial class frmtempdiscovery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmtempdiscovery));
            tbname = new TextBox();
            lblname = new Label();
            btnok = new Button();
            btncancel = new Button();
            lbip = new Label();
            tbip = new TextBox();
            SuspendLayout();
            // 
            // tbname
            // 
            tbname.Location = new Point(144, 31);
            tbname.Name = "tbname";
            tbname.Size = new Size(227, 23);
            tbname.TabIndex = 0;
            tbname.TextChanged += tbname_TextChanged;
            // 
            // lblname
            // 
            lblname.AutoSize = true;
            lblname.Location = new Point(27, 37);
            lblname.Name = "lblname";
            lblname.Size = new Size(42, 15);
            lblname.TabIndex = 1;
            lblname.Text = "Name:";
            // 
            // btnok
            // 
            btnok.Location = new Point(40, 120);
            btnok.Name = "btnok";
            btnok.Size = new Size(112, 65);
            btnok.TabIndex = 2;
            btnok.Text = "OK";
            btnok.UseVisualStyleBackColor = true;
            btnok.Click += btnok_Click;
            // 
            // btncancel
            // 
            btncancel.Location = new Point(220, 120);
            btncancel.Name = "btncancel";
            btncancel.Size = new Size(112, 65);
            btncancel.TabIndex = 3;
            btncancel.Text = "Cancel";
            btncancel.UseVisualStyleBackColor = true;
            btncancel.Click += btncancel_Click;
            // 
            // lbip
            // 
            lbip.AutoSize = true;
            lbip.Location = new Point(27, 69);
            lbip.Name = "lbip";
            lbip.Size = new Size(20, 15);
            lbip.TabIndex = 5;
            lbip.Text = "IP:";
            // 
            // tbip
            // 
            tbip.Location = new Point(144, 63);
            tbip.Name = "tbip";
            tbip.Size = new Size(227, 23);
            tbip.TabIndex = 4;
            // 
            // frmtempdiscovery
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lbip);
            Controls.Add(tbip);
            Controls.Add(btncancel);
            Controls.Add(btnok);
            Controls.Add(lblname);
            Controls.Add(tbname);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "frmtempdiscovery";
            Text = "frmtempdiscovery";
            Load += frmtempdiscovery_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tbname;
        private Label lblname;
        private Button btnok;
        private Button btncancel;
        private Label lbip;
        private TextBox tbip;
    }
}