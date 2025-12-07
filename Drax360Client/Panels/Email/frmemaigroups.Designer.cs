namespace Drax360Client.Panels.Email
{
    partial class frmemaigroups
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
            btsendtest = new Button();
            btdelete = new Button();
            btedit = new Button();
            dgemailgroups = new DataGridView();
            btaddnew = new Button();
            ((System.ComponentModel.ISupportInitialize)dgemailgroups).BeginInit();
            SuspendLayout();
            // 
            // btsendtest
            // 
            btsendtest.Location = new Point(174, 415);
            btsendtest.Name = "btsendtest";
            btsendtest.Size = new Size(75, 23);
            btsendtest.TabIndex = 7;
            btsendtest.Text = "Send Test Message";
            btsendtest.UseVisualStyleBackColor = true;
            // 
            // btdelete
            // 
            btdelete.Location = new Point(93, 415);
            btdelete.Name = "btdelete";
            btdelete.Size = new Size(75, 23);
            btdelete.TabIndex = 6;
            btdelete.Text = "Delete";
            btdelete.UseVisualStyleBackColor = true;
            btdelete.Click += btdelete_Click;
            // 
            // btedit
            // 
            btedit.Location = new Point(12, 415);
            btedit.Name = "btedit";
            btedit.Size = new Size(75, 23);
            btedit.TabIndex = 5;
            btedit.Text = "Edit";
            btedit.UseVisualStyleBackColor = true;
            btedit.Click += btedit_Click;
            // 
            // dgemailgroups
            // 
            dgemailgroups.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgemailgroups.Location = new Point(12, 12);
            dgemailgroups.Name = "dgemailgroups";
            dgemailgroups.RowHeadersVisible = false;
            dgemailgroups.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgemailgroups.Size = new Size(683, 397);
            dgemailgroups.TabIndex = 4;
            dgemailgroups.CellContentClick += dgemailgroups_CellContentClick;
            dgemailgroups.CellValueChanged += dgemailgroups_CellValueChanged;
            dgemailgroups.SelectionChanged += dgemailgroups_SelectionChanged;
            // 
            // btaddnew
            // 
            btaddnew.Location = new Point(384, 415);
            btaddnew.Name = "btaddnew";
            btaddnew.Size = new Size(75, 23);
            btaddnew.TabIndex = 8;
            btaddnew.Text = "Add New";
            btaddnew.UseVisualStyleBackColor = true;
            btaddnew.Click += btaddnew_Click;
            // 
            // frmemaigroups
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(713, 450);
            Controls.Add(btaddnew);
            Controls.Add(btsendtest);
            Controls.Add(btdelete);
            Controls.Add(btedit);
            Controls.Add(dgemailgroups);
            Name = "frmemaigroups";
            Text = "Email Groups";
            FormClosing += frmemaigroups_FormClosing;
            ((System.ComponentModel.ISupportInitialize)dgemailgroups).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btsendtest;
        private Button btdelete;
        private Button btedit;
        private DataGridView dgemailgroups;
        private Button btaddnew;
    }
}