namespace DraxClient.Panels.RSM
{
    partial class frmdiscovery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmdiscovery));
            pbdiscoveryimage = new PictureBox();
            btcancel = new Button();
            lbdhcpname = new Label();
            tbdhcpname = new TextBox();
            pnleditoptions = new Panel();
            lblname = new Label();
            tbname = new TextBox();
            progressBar1 = new ProgressBar();
            label3 = new Label();
            tbsoftwarever = new TextBox();
            label2 = new Label();
            tbreportto2 = new TextBox();
            label1 = new Label();
            tbreportto1 = new TextBox();
            lbmacaddress = new Label();
            tbmacaddress = new TextBox();
            lbserialnumber = new Label();
            tbserialnumber = new TextBox();
            lbmoduletype = new Label();
            tbmoduletype = new TextBox();
            lbmodulenumber = new Label();
            tbmodulenumber = new TextBox();
            lbgateway = new Label();
            tbgateway = new TextBox();
            lbsubnetmask = new Label();
            tbsubnetmask = new TextBox();
            lbipaddress = new Label();
            tbipaddress = new TextBox();
            btrestoretodefaults = new Button();
            btsavechanges = new Button();
            btclose = new Button();
            ((System.ComponentModel.ISupportInitialize)pbdiscoveryimage).BeginInit();
            pnleditoptions.SuspendLayout();
            SuspendLayout();
            // 
            // pbdiscoveryimage
            // 
            pbdiscoveryimage.Image = (Image)resources.GetObject("pbdiscoveryimage.Image");
            pbdiscoveryimage.Location = new Point(21, 12);
            pbdiscoveryimage.Name = "pbdiscoveryimage";
            pbdiscoveryimage.Size = new Size(363, 323);
            pbdiscoveryimage.SizeMode = PictureBoxSizeMode.StretchImage;
            pbdiscoveryimage.TabIndex = 0;
            pbdiscoveryimage.TabStop = false;
            // 
            // btcancel
            // 
            btcancel.Location = new Point(0, 393);
            btcancel.Name = "btcancel";
            btcancel.Size = new Size(113, 40);
            btcancel.TabIndex = 1;
            btcancel.Text = "Cancel";
            btcancel.UseVisualStyleBackColor = true;
            btcancel.Click += btcancel_Click;
            // 
            // lbdhcpname
            // 
            lbdhcpname.AutoSize = true;
            lbdhcpname.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbdhcpname.Location = new Point(13, 170);
            lbdhcpname.Name = "lbdhcpname";
            lbdhcpname.Size = new Size(78, 15);
            lbdhcpname.TabIndex = 2;
            lbdhcpname.Text = "DHCP Name:";
            lbdhcpname.TextAlign = ContentAlignment.TopRight;
            // 
            // tbdhcpname
            // 
            tbdhcpname.Location = new Point(128, 167);
            tbdhcpname.MaxLength = 24;
            tbdhcpname.Name = "tbdhcpname";
            tbdhcpname.Size = new Size(187, 23);
            tbdhcpname.TabIndex = 3;
            tbdhcpname.TextChanged += tbdhcpname_TextChanged;
            // 
            // pnleditoptions
            // 
            pnleditoptions.Controls.Add(lblname);
            pnleditoptions.Controls.Add(tbname);
            pnleditoptions.Controls.Add(progressBar1);
            pnleditoptions.Controls.Add(label3);
            pnleditoptions.Controls.Add(tbsoftwarever);
            pnleditoptions.Controls.Add(label2);
            pnleditoptions.Controls.Add(tbreportto2);
            pnleditoptions.Controls.Add(label1);
            pnleditoptions.Controls.Add(tbreportto1);
            pnleditoptions.Controls.Add(lbmacaddress);
            pnleditoptions.Controls.Add(tbmacaddress);
            pnleditoptions.Controls.Add(lbserialnumber);
            pnleditoptions.Controls.Add(tbserialnumber);
            pnleditoptions.Controls.Add(lbmoduletype);
            pnleditoptions.Controls.Add(tbmoduletype);
            pnleditoptions.Controls.Add(lbmodulenumber);
            pnleditoptions.Controls.Add(tbmodulenumber);
            pnleditoptions.Controls.Add(lbgateway);
            pnleditoptions.Controls.Add(tbgateway);
            pnleditoptions.Controls.Add(lbsubnetmask);
            pnleditoptions.Controls.Add(tbsubnetmask);
            pnleditoptions.Controls.Add(lbipaddress);
            pnleditoptions.Controls.Add(tbipaddress);
            pnleditoptions.Controls.Add(btrestoretodefaults);
            pnleditoptions.Controls.Add(btsavechanges);
            pnleditoptions.Controls.Add(lbdhcpname);
            pnleditoptions.Controls.Add(btcancel);
            pnleditoptions.Controls.Add(tbdhcpname);
            pnleditoptions.Location = new Point(12, 2);
            pnleditoptions.Name = "pnleditoptions";
            pnleditoptions.Size = new Size(478, 470);
            pnleditoptions.TabIndex = 4;
            // 
            // lblname
            // 
            lblname.AutoSize = true;
            lblname.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblname.Location = new Point(13, 139);
            lblname.Name = "lblname";
            lblname.Size = new Size(43, 15);
            lblname.TabIndex = 27;
            lblname.Text = "Name:";
            lblname.TextAlign = ContentAlignment.TopRight;
            // 
            // tbname
            // 
            tbname.Location = new Point(128, 136);
            tbname.MaxLength = 24;
            tbname.Name = "tbname";
            tbname.Size = new Size(187, 23);
            tbname.TabIndex = 28;
            tbname.TextChanged += tbname_TextChanged;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(175, 439);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(113, 23);
            progressBar1.TabIndex = 26;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.Location = new Point(13, 368);
            label3.Name = "label3";
            label3.Size = new Size(78, 15);
            label3.TabIndex = 24;
            label3.Text = "S/W Version:";
            label3.TextAlign = ContentAlignment.TopRight;
            // 
            // tbsoftwarever
            // 
            tbsoftwarever.Location = new Point(128, 360);
            tbsoftwarever.Name = "tbsoftwarever";
            tbsoftwarever.ReadOnly = true;
            tbsoftwarever.Size = new Size(187, 23);
            tbsoftwarever.TabIndex = 25;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.Location = new Point(13, 335);
            label2.Name = "label2";
            label2.Size = new Size(100, 15);
            label2.TabIndex = 22;
            label2.Text = "Reporting To [2]:";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // tbreportto2
            // 
            tbreportto2.Location = new Point(128, 327);
            tbreportto2.Name = "tbreportto2";
            tbreportto2.Size = new Size(187, 23);
            tbreportto2.TabIndex = 23;
            tbreportto2.TextChanged += tbreportto2_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.Location = new Point(13, 302);
            label1.Name = "label1";
            label1.Size = new Size(100, 15);
            label1.TabIndex = 20;
            label1.Text = "Reporting To [1]:";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // tbreportto1
            // 
            tbreportto1.Location = new Point(128, 298);
            tbreportto1.Name = "tbreportto1";
            tbreportto1.Size = new Size(187, 23);
            tbreportto1.TabIndex = 21;
            tbreportto1.TextChanged += tbreportto1_TextChanged;
            // 
            // lbmacaddress
            // 
            lbmacaddress.AutoSize = true;
            lbmacaddress.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbmacaddress.Location = new Point(13, 106);
            lbmacaddress.Name = "lbmacaddress";
            lbmacaddress.Size = new Size(83, 15);
            lbmacaddress.TabIndex = 18;
            lbmacaddress.Text = "MAC Address:";
            lbmacaddress.TextAlign = ContentAlignment.TopRight;
            // 
            // tbmacaddress
            // 
            tbmacaddress.Location = new Point(128, 103);
            tbmacaddress.Name = "tbmacaddress";
            tbmacaddress.ReadOnly = true;
            tbmacaddress.Size = new Size(187, 23);
            tbmacaddress.TabIndex = 19;
            // 
            // lbserialnumber
            // 
            lbserialnumber.AutoSize = true;
            lbserialnumber.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbserialnumber.Location = new Point(13, 73);
            lbserialnumber.Name = "lbserialnumber";
            lbserialnumber.Size = new Size(90, 15);
            lbserialnumber.TabIndex = 16;
            lbserialnumber.Text = "Serial Number:";
            lbserialnumber.TextAlign = ContentAlignment.TopRight;
            // 
            // tbserialnumber
            // 
            tbserialnumber.Location = new Point(128, 68);
            tbserialnumber.Name = "tbserialnumber";
            tbserialnumber.Size = new Size(187, 23);
            tbserialnumber.TabIndex = 17;
            tbserialnumber.TextChanged += tbserialnumber_TextChanged;
            // 
            // lbmoduletype
            // 
            lbmoduletype.AutoSize = true;
            lbmoduletype.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbmoduletype.Location = new Point(13, 40);
            lbmoduletype.Name = "lbmoduletype";
            lbmoduletype.Size = new Size(81, 15);
            lbmoduletype.TabIndex = 14;
            lbmoduletype.Text = "Module Type:";
            lbmoduletype.TextAlign = ContentAlignment.TopRight;
            // 
            // tbmoduletype
            // 
            tbmoduletype.Location = new Point(128, 36);
            tbmoduletype.Name = "tbmoduletype";
            tbmoduletype.ReadOnly = true;
            tbmoduletype.Size = new Size(187, 23);
            tbmoduletype.TabIndex = 15;
            // 
            // lbmodulenumber
            // 
            lbmodulenumber.AutoSize = true;
            lbmodulenumber.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbmodulenumber.Location = new Point(13, 7);
            lbmodulenumber.Name = "lbmodulenumber";
            lbmodulenumber.Size = new Size(101, 15);
            lbmodulenumber.TabIndex = 12;
            lbmodulenumber.Text = "Module Number:";
            lbmodulenumber.TextAlign = ContentAlignment.TopRight;
            // 
            // tbmodulenumber
            // 
            tbmodulenumber.Location = new Point(128, 4);
            tbmodulenumber.Name = "tbmodulenumber";
            tbmodulenumber.Size = new Size(187, 23);
            tbmodulenumber.TabIndex = 13;
            tbmodulenumber.TextChanged += tbmodulenumber_TextChanged;
            // 
            // lbgateway
            // 
            lbgateway.AutoSize = true;
            lbgateway.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbgateway.Location = new Point(13, 269);
            lbgateway.Name = "lbgateway";
            lbgateway.Size = new Size(59, 15);
            lbgateway.TabIndex = 10;
            lbgateway.Text = "Gateway:";
            lbgateway.TextAlign = ContentAlignment.TopRight;
            // 
            // tbgateway
            // 
            tbgateway.Location = new Point(128, 266);
            tbgateway.Name = "tbgateway";
            tbgateway.Size = new Size(187, 23);
            tbgateway.TabIndex = 11;
            tbgateway.TextChanged += tbgateway_TextChanged;
            // 
            // lbsubnetmask
            // 
            lbsubnetmask.AutoSize = true;
            lbsubnetmask.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbsubnetmask.Location = new Point(13, 236);
            lbsubnetmask.Name = "lbsubnetmask";
            lbsubnetmask.Size = new Size(82, 15);
            lbsubnetmask.TabIndex = 8;
            lbsubnetmask.Text = "Subnet Mask:";
            lbsubnetmask.TextAlign = ContentAlignment.TopRight;
            // 
            // tbsubnetmask
            // 
            tbsubnetmask.Location = new Point(128, 233);
            tbsubnetmask.Name = "tbsubnetmask";
            tbsubnetmask.Size = new Size(187, 23);
            tbsubnetmask.TabIndex = 9;
            tbsubnetmask.TextChanged += tbsubnetmask_TextChanged;
            // 
            // lbipaddress
            // 
            lbipaddress.AutoSize = true;
            lbipaddress.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbipaddress.Location = new Point(13, 203);
            lbipaddress.Name = "lbipaddress";
            lbipaddress.Size = new Size(68, 15);
            lbipaddress.TabIndex = 6;
            lbipaddress.Text = "IP Address:";
            lbipaddress.TextAlign = ContentAlignment.TopRight;
            // 
            // tbipaddress
            // 
            tbipaddress.Location = new Point(128, 200);
            tbipaddress.Name = "tbipaddress";
            tbipaddress.Size = new Size(187, 23);
            tbipaddress.TabIndex = 7;
            tbipaddress.TextChanged += tbipaddress_TextChanged_1;
            // 
            // btrestoretodefaults
            // 
            btrestoretodefaults.Location = new Point(350, 393);
            btrestoretodefaults.Name = "btrestoretodefaults";
            btrestoretodefaults.Size = new Size(113, 40);
            btrestoretodefaults.TabIndex = 5;
            btrestoretodefaults.Text = "Restore Defaults";
            btrestoretodefaults.UseVisualStyleBackColor = true;
            btrestoretodefaults.Click += btrestoretodefaults_Click;
            // 
            // btsavechanges
            // 
            btsavechanges.Location = new Point(175, 393);
            btsavechanges.Name = "btsavechanges";
            btsavechanges.Size = new Size(113, 40);
            btsavechanges.TabIndex = 4;
            btsavechanges.Text = "Save Changes";
            btsavechanges.UseVisualStyleBackColor = true;
            btsavechanges.Click += btsavechanges_Click;
            // 
            // btclose
            // 
            btclose.Location = new Point(137, 357);
            btclose.Name = "btclose";
            btclose.Size = new Size(111, 31);
            btclose.TabIndex = 5;
            btclose.Text = "Close";
            btclose.UseVisualStyleBackColor = true;
            btclose.Click += btclose_Click;
            // 
            // frmdiscovery
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(500, 474);
            ControlBox = false;
            Controls.Add(pnleditoptions);
            Controls.Add(pbdiscoveryimage);
            Controls.Add(btclose);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmdiscovery";
            Text = "Discovery";
            Load += frmdiscovery_Load;
            ((System.ComponentModel.ISupportInitialize)pbdiscoveryimage).EndInit();
            pnleditoptions.ResumeLayout(false);
            pnleditoptions.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pbdiscoveryimage;
        private Button btcancel;
        private Label lbdhcpname;
        private TextBox tbdhcpname;
        private Panel pnleditoptions;
        private Button btsavechanges;
        private Button btrestoretodefaults;
        private Label lbgateway;
        private TextBox tbgateway;
        private Label lbsubnetmask;
        private TextBox tbsubnetmask;
        private Label lbipaddress;
        private TextBox tbipaddress;
        private Label lbmacaddress;
        private TextBox tbmacaddress;
        private Label lbserialnumber;
        private TextBox tbserialnumber;
        private Label lbmoduletype;
        private TextBox tbmoduletype;
        private Label lbmodulenumber;
        private TextBox tbmodulenumber;
        private Label label2;
        private TextBox tbreportto2;
        private Label label1;
        private TextBox tbreportto1;
        private Label label3;
        private TextBox tbsoftwarever;
        private ProgressBar progressBar1;
        private Button btclose;
        private Label lblname;
        private TextBox tbname;
    }
}