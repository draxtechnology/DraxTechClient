namespace DraxClient.Panels.RSM
{
    partial class frmRSMProperties
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            tabs          = new TabControl();
            tabProperties = new TabPage();
            tabStatus     = new TabPage();
            tabOptions    = new TabPage();
            dgProperties  = new DataGridView();
            dgStatus      = new DataGridView();
            dgOptions     = new DataGridView();
            btClose       = new Button();
            ((System.ComponentModel.ISupportInitialize)dgProperties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgStatus).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgOptions).BeginInit();
            SuspendLayout();

            // Read-only label|value grid shared style (mirrors VB6 vsfProps —
            // two columns, no headers, whole grid display-only).
            void ConfigGrid(DataGridView g)
            {
                g.Dock                  = DockStyle.Fill;
                g.AllowUserToAddRows    = false;
                g.AllowUserToDeleteRows = false;
                g.AllowUserToResizeRows = false;
                g.ReadOnly              = true;
                g.RowHeadersVisible     = false;
                g.ColumnHeadersVisible  = false;
                g.SelectionMode         = DataGridViewSelectionMode.FullRowSelect;
                g.MultiSelect           = false;
                g.BorderStyle           = BorderStyle.None;
                g.BackgroundColor       = SystemColors.Window;
                var colField = new DataGridViewTextBoxColumn { Width = 190 };
                colField.DefaultCellStyle.ForeColor = SystemColors.GrayText;
                var colValue = new DataGridViewTextBoxColumn
                {
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                };
                g.Columns.AddRange(colField, colValue);
            }
            ConfigGrid(dgProperties);
            ConfigGrid(dgStatus);
            ConfigGrid(dgOptions);
            dgOptions.CellDoubleClick += dgOptions_CellDoubleClick;

            // Tab pages
            tabProperties.Text    = "Properties";
            tabStatus.Text        = "Status";
            tabOptions.Text       = "Module Options";
            tabProperties.Padding = new Padding(3);
            tabStatus.Padding     = new Padding(3);
            tabOptions.Padding    = new Padding(3);
            tabProperties.Controls.Add(dgProperties);
            tabStatus.Controls.Add(dgStatus);
            tabOptions.Controls.Add(dgOptions);

            // Tab control
            tabs.Location = new Point(8, 8);
            tabs.Size     = new Size(444, 372);
            tabs.TabIndex = 0;
            tabs.Controls.AddRange(new Control[] { tabProperties, tabStatus, tabOptions });
            tabs.SelectedIndexChanged += tabs_SelectedIndexChanged;

            // Close button
            btClose.Text     = "Close";
            btClose.Location = new Point(372, 388);
            btClose.Size     = new Size(80, 28);
            btClose.TabIndex = 1;
            btClose.Click   += btClose_Click;

            // Form
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode       = AutoScaleMode.Font;
            ClientSize          = new Size(460, 424);
            MaximizeBox         = false;
            MinimizeBox         = false;
            FormBorderStyle     = FormBorderStyle.FixedDialog;
            StartPosition       = FormStartPosition.CenterParent;
            Text                = "Node Properties";
            Controls.AddRange(new Control[] { tabs, btClose });

            Load        += frmRSMProperties_Load;
            FormClosing += frmRSMProperties_FormClosing;
            FormClosed  += frmRSMProperties_FormClosed;

            ((System.ComponentModel.ISupportInitialize)dgProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgStatus).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgOptions).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabs;
        private TabPage tabProperties, tabStatus, tabOptions;
        private DataGridView dgProperties, dgStatus, dgOptions;
        private Button btClose;
    }
}
