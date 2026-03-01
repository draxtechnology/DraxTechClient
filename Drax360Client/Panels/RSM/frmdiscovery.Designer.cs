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
            btcancel.Location = new Point(0, 270);
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
            lbdhcpname.Location = new Point(13, 131);
            lbdhcpname.Name = "lbdhcpname";
            lbdhcpname.Size = new Size(78, 15);
            lbdhcpname.TabIndex = 2;
            lbdhcpname.Text = "DHCP Name:";
            lbdhcpname.TextAlign = ContentAlignment.TopRight;
            // 
            // tbdhcpname
            // 
            tbdhcpname.Location = new Point(133, 128);
            tbdhcpname.MaxLength = 24;
            tbdhcpname.Name = "tbdhcpname";
            tbdhcpname.Size = new Size(187, 23);
            tbdhcpname.TabIndex = 3;
            // 
            // pnleditoptions
            // 
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
            pnleditoptions.Location = new Point(21, 12);
            pnleditoptions.Name = "pnleditoptions";
            pnleditoptions.Size = new Size(478, 378);
            pnleditoptions.TabIndex = 4;
            // 
            // lbmacaddress
            // 
            lbmacaddress.AutoSize = true;
            lbmacaddress.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbmacaddress.Location = new Point(13, 99);
            lbmacaddress.Name = "lbmacaddress";
            lbmacaddress.Size = new Size(83, 15);
            lbmacaddress.TabIndex = 18;
            lbmacaddress.Text = "MAC Address:";
            lbmacaddress.TextAlign = ContentAlignment.TopRight;
            // 
            // tbmacaddress
            // 
            tbmacaddress.Location = new Point(133, 96);
            tbmacaddress.Name = "tbmacaddress";
            tbmacaddress.ReadOnly = true;
            tbmacaddress.Size = new Size(187, 23);
            tbmacaddress.TabIndex = 19;
            // 
            // lbserialnumber
            // 
            lbserialnumber.AutoSize = true;
            lbserialnumber.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbserialnumber.Location = new Point(13, 69);
            lbserialnumber.Name = "lbserialnumber";
            lbserialnumber.Size = new Size(90, 15);
            lbserialnumber.TabIndex = 16;
            lbserialnumber.Text = "Serial Number:";
            lbserialnumber.TextAlign = ContentAlignment.TopRight;
            // 
            // tbserialnumber
            // 
            tbserialnumber.Location = new Point(133, 66);
            tbserialnumber.Name = "tbserialnumber";
            tbserialnumber.Size = new Size(187, 23);
            tbserialnumber.TabIndex = 17;
            // 
            // lbmoduletype
            // 
            lbmoduletype.AutoSize = true;
            lbmoduletype.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbmoduletype.Location = new Point(13, 37);
            lbmoduletype.Name = "lbmoduletype";
            lbmoduletype.Size = new Size(81, 15);
            lbmoduletype.TabIndex = 14;
            lbmoduletype.Text = "Module Type:";
            lbmoduletype.TextAlign = ContentAlignment.TopRight;
            // 
            // tbmoduletype
            // 
            tbmoduletype.Location = new Point(133, 34);
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
            tbmodulenumber.Location = new Point(133, 4);
            tbmodulenumber.Name = "tbmodulenumber";
            tbmodulenumber.Size = new Size(187, 23);
            tbmodulenumber.TabIndex = 13;
            // 
            // lbgateway
            // 
            lbgateway.AutoSize = true;
            lbgateway.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbgateway.Location = new Point(13, 223);
            lbgateway.Name = "lbgateway";
            lbgateway.Size = new Size(59, 15);
            lbgateway.TabIndex = 10;
            lbgateway.Text = "Gateway:";
            lbgateway.TextAlign = ContentAlignment.TopRight;
            // 
            // tbgateway
            // 
            tbgateway.Location = new Point(133, 220);
            tbgateway.Name = "tbgateway";
            tbgateway.Size = new Size(187, 23);
            tbgateway.TabIndex = 11;
            // 
            // lbsubnetmask
            // 
            lbsubnetmask.AutoSize = true;
            lbsubnetmask.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbsubnetmask.Location = new Point(13, 193);
            lbsubnetmask.Name = "lbsubnetmask";
            lbsubnetmask.Size = new Size(82, 15);
            lbsubnetmask.TabIndex = 8;
            lbsubnetmask.Text = "Subnet Mask:";
            lbsubnetmask.TextAlign = ContentAlignment.TopRight;
            // 
            // tbsubnetmask
            // 
            tbsubnetmask.Location = new Point(133, 190);
            tbsubnetmask.Name = "tbsubnetmask";
            tbsubnetmask.Size = new Size(187, 23);
            tbsubnetmask.TabIndex = 9;
            // 
            // lbipaddress
            // 
            lbipaddress.AutoSize = true;
            lbipaddress.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbipaddress.Location = new Point(13, 161);
            lbipaddress.Name = "lbipaddress";
            lbipaddress.Size = new Size(68, 15);
            lbipaddress.TabIndex = 6;
            lbipaddress.Text = "IP Address:";
            lbipaddress.TextAlign = ContentAlignment.TopRight;
            // 
            // tbipaddress
            // 
            tbipaddress.Location = new Point(133, 158);
            tbipaddress.Name = "tbipaddress";
            tbipaddress.Size = new Size(187, 23);
            tbipaddress.TabIndex = 7;
            // 
            // btrestoretodefaults
            // 
            btrestoretodefaults.Location = new Point(350, 270);
            btrestoretodefaults.Name = "btrestoretodefaults";
            btrestoretodefaults.Size = new Size(113, 40);
            btrestoretodefaults.TabIndex = 5;
            btrestoretodefaults.Text = "Restore To Defaults";
            btrestoretodefaults.UseVisualStyleBackColor = true;
            btrestoretodefaults.Click += btrestoretodefaults_Click;
            // 
            // btsavechanges
            // 
            btsavechanges.Location = new Point(221, 270);
            btsavechanges.Name = "btsavechanges";
            btsavechanges.Size = new Size(113, 40);
            btsavechanges.TabIndex = 4;
            btsavechanges.Text = "Save Changes";
            btsavechanges.UseVisualStyleBackColor = true;
            btsavechanges.Click += btsavechanges_Click;
            // 
            // frmdiscovery
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(674, 451);
            Controls.Add(pnleditoptions);
            Controls.Add(pbdiscoveryimage);
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
    }
}