namespace DraxClient.Panels.RSM
{
    partial class frmRSMNodes
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
            dgNodes      = new DataGridView();
            colNode      = new DataGridViewTextBoxColumn();
            colSite      = new DataGridViewTextBoxColumn();
            colName      = new DataGridViewTextBoxColumn();
            colType      = new DataGridViewTextBoxColumn();
            colStatus    = new DataGridViewTextBoxColumn();
            colMessages  = new DataGridViewTextBoxColumn();
            colAddress   = new DataGridViewTextBoxColumn();
            btProperties = new Button();
            btDelete     = new Button();
            btDiscovery  = new Button();
            btLicense    = new Button();
            btRestart    = new Button();
            btClose      = new Button();
            ((System.ComponentModel.ISupportInitialize)dgNodes).BeginInit();
            SuspendLayout();

            // dgNodes
            dgNodes.AllowUserToAddRows    = false;
            dgNodes.AllowUserToDeleteRows = false;
            dgNodes.ReadOnly              = true;
            dgNodes.RowHeadersVisible     = false;
            dgNodes.SelectionMode         = DataGridViewSelectionMode.FullRowSelect;
            dgNodes.MultiSelect           = false;
            dgNodes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgNodes.Location = new Point(0, 0);
            dgNodes.Size     = new Size(960, 520);
            dgNodes.TabIndex = 0;
            dgNodes.Columns.AddRange(colNode, colSite, colName, colType, colStatus, colMessages, colAddress);
            dgNodes.SelectionChanged += dgNodes_SelectionChanged;
            dgNodes.CellDoubleClick  += dgNodes_CellDoubleClick;

            colNode.HeaderText     = "Node";     colNode.Width     = 50;  colNode.DefaultCellStyle.Alignment     = DataGridViewContentAlignment.MiddleCenter;
            colSite.HeaderText     = "Site Name"; colSite.Width    = 120;
            colName.HeaderText     = "Node Name"; colName.Width    = 200; colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colType.HeaderText     = "Node Type"; colType.Width    = 120;
            colStatus.HeaderText   = "Status";    colStatus.Width  = 80;  colStatus.DefaultCellStyle.Alignment   = DataGridViewContentAlignment.MiddleCenter;
            colMessages.HeaderText = "Messages";  colMessages.Width = 80; colMessages.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colAddress.HeaderText  = "Address";   colAddress.Width = 140;

            colNode.DataPropertyName     = "";
            colSite.DataPropertyName     = "";
            colName.DataPropertyName     = "";
            colType.DataPropertyName     = "";
            colStatus.DataPropertyName   = "";
            colMessages.DataPropertyName = "";
            colAddress.DataPropertyName  = "";

            // Buttons (bottom row, mirroring VB6 frmRSMNodes layout)
            int btnY = 528, btnH = 28;
            btProperties.Text = "Properties";  btProperties.Location = new Point(4,   btnY); btProperties.Size = new Size(90, btnH); btProperties.Click += btProperties_Click;
            btDelete.Text     = "Delete";       btDelete.Location     = new Point(100, btnY); btDelete.Size     = new Size(75, btnH); btDelete.Click     += btDelete_Click;
            btDiscovery.Text  = "Discovery";    btDiscovery.Location  = new Point(181, btnY); btDiscovery.Size  = new Size(90, btnH); btDiscovery.Click  += btDiscovery_Click;
            btLicense.Text    = "Install License"; btLicense.Location = new Point(277, btnY); btLicense.Size   = new Size(105, btnH); btLicense.Click    += btLicense_Click;
            btRestart.Text    = "Restart Module"; btRestart.Location  = new Point(388, btnY); btRestart.Size   = new Size(110, btnH); btRestart.Enabled  = false;
            btClose.Text      = "Close";         btClose.Location     = new Point(876, btnY); btClose.Size     = new Size(80, btnH); btClose.Click       += btClose_Click;

            // Form
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode       = AutoScaleMode.Font;
            ClientSize          = new Size(960, 564);
            MaximizeBox         = false;
            MinimizeBox         = false;
            Text                = "Connect Remote Site Monitoring Node Configuration and Status";
            Controls.AddRange(new Control[] {
                dgNodes,
                btProperties, btDelete, btDiscovery, btLicense, btRestart, btClose
            });

            Load        += frmRSMNodes_Load;
            FormClosed  += frmRSMNodes_FormClosed;

            ((System.ComponentModel.ISupportInitialize)dgNodes).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgNodes;
        private DataGridViewTextBoxColumn colNode, colSite, colName, colType, colStatus, colMessages, colAddress;
        private Button btProperties, btDelete, btDiscovery, btLicense, btRestart, btClose;
    }
}
