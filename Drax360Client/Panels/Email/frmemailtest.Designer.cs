namespace DraxClient.Panels.Email
{
    partial class frmemailtest
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

        public frmemailtest()
        {
            InitializeComponent();
            LoadFormData();
        }


        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmemailtest));
            textBox1 = new TextBox();
            btsend = new Button();
            btcancel = new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(43, 43);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(395, 23);
            textBox1.TabIndex = 0;
            // 
            // btsend
            // 
            btsend.Location = new Point(299, 167);
            btsend.Name = "btsend";
            btsend.Size = new Size(75, 23);
            btsend.TabIndex = 1;
            btsend.Text = "Send";
            btsend.UseVisualStyleBackColor = true;
            btsend.Click += btsend_Click;
            // 
            // btcancel
            // 
            btcancel.Location = new Point(415, 167);
            btcancel.Name = "btcancel";
            btcancel.Size = new Size(75, 23);
            btcancel.TabIndex = 2;
            btcancel.Text = "Cancel";
            btcancel.UseVisualStyleBackColor = true;
            btcancel.Click += btcancel_Click;
            // 
            // frmemailtest
            // 
            ClientSize = new Size(506, 212);
            Controls.Add(btcancel);
            Controls.Add(btsend);
            Controls.Add(textBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "frmemailtest";
            Text = "TEST MESSAGE - up to 28 Characters";
            ResumeLayout(false);
            PerformLayout();
        }

        private TextBox textBox1;
        private Button btsend;
        private Button btcancel;
    }
}
