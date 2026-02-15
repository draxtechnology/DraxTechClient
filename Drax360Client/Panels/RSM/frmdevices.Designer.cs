namespace DraxClient.Panels.RSM
{
    partial class frmdevices
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
            btdiscover = new Button();
            btdelete = new Button();
            btedit = new Button();
            dgdevices = new DataGridView();
            bttempdiscovery = new Button();
            ((System.ComponentModel.ISupportInitialize)dgdevices).BeginInit();
            SuspendLayout();
            // 
            // btdiscover
            // 
            btdiscover.Location = new Point(374, 415);
            btdiscover.Name = "btdiscover";
            btdiscover.Size = new Size(75, 23);
            btdiscover.TabIndex = 13;
            btdiscover.Text = "Discover";
            btdiscover.UseVisualStyleBackColor = true;
            btdiscover.Click += btdiscover_Click;
            // 
            // btdelete
            // 
            btdelete.Location = new Point(83, 415);
            btdelete.Name = "btdelete";
            btdelete.Size = new Size(75, 23);
            btdelete.TabIndex = 11;
            btdelete.Text = "Delete";
            btdelete.UseVisualStyleBackColor = true;
            btdelete.Click += btdelete_Click;
            // 
            // btedit
            // 
            btedit.Location = new Point(2, 415);
            btedit.Name = "btedit";
            btedit.Size = new Size(75, 23);
            btedit.TabIndex = 10;
            btedit.Text = "Edit";
            btedit.UseVisualStyleBackColor = true;
            btedit.Click += btedit_Click;
            // 
            // dgdevices
            // 
            dgdevices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgdevices.Location = new Point(2, 12);
            dgdevices.Name = "dgdevices";
            dgdevices.RowHeadersVisible = false;
            dgdevices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgdevices.Size = new Size(683, 397);
            dgdevices.TabIndex = 9;
            dgdevices.SelectionChanged += dgdevices_SelectionChanged;
            // 
            // bttempdiscovery
            // 
            bttempdiscovery.Location = new Point(486, 415);
            bttempdiscovery.Name = "bttempdiscovery";
            bttempdiscovery.Size = new Size(139, 23);
            bttempdiscovery.TabIndex = 14;
            bttempdiscovery.Text = "Temp Discover";
            bttempdiscovery.UseVisualStyleBackColor = true;
            bttempdiscovery.Click += bttempdiscovery_Click;
            // 
            // frmdevices
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(bttempdiscovery);
            Controls.Add(btdiscover);
            Controls.Add(btdelete);
            Controls.Add(btedit);
            Controls.Add(dgdevices);
            Name = "frmdevices";
            Text = "RSM Devices";
            ((System.ComponentModel.ISupportInitialize)dgdevices).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btdiscover;
        private Button btdelete;
        private Button btedit;
        private DataGridView dgdevices;
        private Button bttempdiscovery;
    }
}