namespace DraxClient.Panels.Email
{
    partial class frmemailgroup
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
            tbname = new TextBox();
            lbname = new Label();
            lbdescription = new Label();
            tbdescription = new TextBox();
            lbfilterwords = new Label();
            tbfilterwords = new TextBox();
            cbincludelocation = new CheckBox();
            cbincludenodetextinfiltering = new CheckBox();
            cbusehtmlemail = new CheckBox();
            cbusebcc = new CheckBox();
            cbsendreportsonly = new CheckBox();
            btsave = new Button();
            btcancel = new Button();
            dgaddresses = new DataGridView();
            btaddaddress = new Button();
            ((System.ComponentModel.ISupportInitialize)dgaddresses).BeginInit();
            SuspendLayout();
            // 
            // tbname
            // 
            tbname.Location = new Point(17, 29);
            tbname.Name = "tbname";
            tbname.Size = new Size(225, 23);
            tbname.TabIndex = 0;
            // 
            // lbname
            // 
            lbname.AutoSize = true;
            lbname.Location = new Point(17, 11);
            lbname.Name = "lbname";
            lbname.Size = new Size(42, 15);
            lbname.TabIndex = 1;
            lbname.Text = "Name:";
            // 
            // lbdescription
            // 
            lbdescription.AutoSize = true;
            lbdescription.Location = new Point(17, 69);
            lbdescription.Name = "lbdescription";
            lbdescription.Size = new Size(70, 15);
            lbdescription.TabIndex = 3;
            lbdescription.Text = "Description:";
            // 
            // tbdescription
            // 
            tbdescription.Location = new Point(17, 87);
            tbdescription.Name = "tbdescription";
            tbdescription.Size = new Size(225, 23);
            tbdescription.TabIndex = 2;
            // 
            // lbfilterwords
            // 
            lbfilterwords.AutoSize = true;
            lbfilterwords.Location = new Point(17, 132);
            lbfilterwords.Name = "lbfilterwords";
            lbfilterwords.Size = new Size(73, 15);
            lbfilterwords.TabIndex = 5;
            lbfilterwords.Text = "Filter Words:";
            // 
            // tbfilterwords
            // 
            tbfilterwords.Location = new Point(17, 150);
            tbfilterwords.Multiline = true;
            tbfilterwords.Name = "tbfilterwords";
            tbfilterwords.Size = new Size(225, 56);
            tbfilterwords.TabIndex = 4;
            // 
            // cbincludelocation
            // 
            cbincludelocation.AutoSize = true;
            cbincludelocation.Location = new Point(20, 239);
            cbincludelocation.Name = "cbincludelocation";
            cbincludelocation.Size = new Size(197, 19);
            cbincludelocation.TabIndex = 6;
            cbincludelocation.Text = "Include Location Text In Filtering";
            cbincludelocation.UseVisualStyleBackColor = true;
            // 
            // cbincludenodetextinfiltering
            // 
            cbincludenodetextinfiltering.AutoSize = true;
            cbincludenodetextinfiltering.Location = new Point(20, 264);
            cbincludenodetextinfiltering.Name = "cbincludenodetextinfiltering";
            cbincludenodetextinfiltering.Size = new Size(180, 19);
            cbincludenodetextinfiltering.TabIndex = 7;
            cbincludenodetextinfiltering.Text = "Include Node Text In Filtering";
            cbincludenodetextinfiltering.UseVisualStyleBackColor = true;
            // 
            // cbusehtmlemail
            // 
            cbusehtmlemail.AutoSize = true;
            cbusehtmlemail.Location = new Point(20, 289);
            cbusehtmlemail.Name = "cbusehtmlemail";
            cbusehtmlemail.Size = new Size(113, 19);
            cbusehtmlemail.TabIndex = 8;
            cbusehtmlemail.Text = "Use HTML Email";
            cbusehtmlemail.UseVisualStyleBackColor = true;
            // 
            // cbusebcc
            // 
            cbusebcc.AutoSize = true;
            cbusebcc.Location = new Point(20, 314);
            cbusebcc.Name = "cbusebcc";
            cbusebcc.Size = new Size(71, 19);
            cbusebcc.TabIndex = 9;
            cbusebcc.Text = "Use BCC";
            cbusebcc.UseVisualStyleBackColor = true;
            // 
            // cbsendreportsonly
            // 
            cbsendreportsonly.AutoSize = true;
            cbsendreportsonly.Location = new Point(20, 339);
            cbsendreportsonly.Name = "cbsendreportsonly";
            cbsendreportsonly.Size = new Size(123, 19);
            cbsendreportsonly.TabIndex = 10;
            cbsendreportsonly.Text = "Send Reports Only";
            cbsendreportsonly.UseVisualStyleBackColor = true;
            // 
            // btsave
            // 
            btsave.Location = new Point(594, 410);
            btsave.Name = "btsave";
            btsave.Size = new Size(75, 23);
            btsave.TabIndex = 11;
            btsave.Text = "Save";
            btsave.UseVisualStyleBackColor = true;
            btsave.Click += btsave_Click;
            // 
            // btcancel
            // 
            btcancel.Location = new Point(677, 410);
            btcancel.Name = "btcancel";
            btcancel.Size = new Size(75, 23);
            btcancel.TabIndex = 11;
            btcancel.Text = "Cancel";
            btcancel.UseVisualStyleBackColor = true;
            btcancel.Click += btcancel_Click;
            // 
            // dgaddresses
            // 
            dgaddresses.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgaddresses.Location = new Point(295, 29);
            dgaddresses.Name = "dgaddresses";
            dgaddresses.Size = new Size(466, 267);
            dgaddresses.TabIndex = 12;
            // 
            // btaddaddress
            // 
            btaddaddress.Location = new Point(294, 310);
            btaddaddress.Name = "btaddaddress";
            btaddaddress.Size = new Size(116, 23);
            btaddaddress.TabIndex = 13;
            btaddaddress.Text = "Add Email";
            btaddaddress.UseVisualStyleBackColor = true;
            btaddaddress.Click += btaddaddress_Click;
            // 
            // frmemailgroup
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(785, 445);
            Controls.Add(btaddaddress);
            Controls.Add(dgaddresses);
            Controls.Add(btcancel);
            Controls.Add(btsave);
            Controls.Add(cbsendreportsonly);
            Controls.Add(cbusebcc);
            Controls.Add(cbusehtmlemail);
            Controls.Add(cbincludenodetextinfiltering);
            Controls.Add(cbincludelocation);
            Controls.Add(lbfilterwords);
            Controls.Add(tbfilterwords);
            Controls.Add(lbdescription);
            Controls.Add(tbdescription);
            Controls.Add(lbname);
            Controls.Add(tbname);
            Name = "frmemailgroup";
            Text = "Email Group";
            ((System.ComponentModel.ISupportInitialize)dgaddresses).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tbname;
        private Label lbname;
        private Label lbdescription;
        private TextBox tbdescription;
        private Label lbfilterwords;
        private TextBox tbfilterwords;
        private CheckBox cbincludelocation;
        private CheckBox cbincludenodetextinfiltering;
        private CheckBox cbusehtmlemail;
        private CheckBox cbusebcc;
        private CheckBox cbsendreportsonly;
        private Button btsave;
        private Button btcancel;
        private DataGridView dgaddresses;
        private Button btaddaddress;
    }
}