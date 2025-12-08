using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drax360Client.Panels.Email
{
    public partial class frmemailgroup : Form
    {
        public Group group { get; set; }
        private BindingSource _addressesSource;

        public frmemailgroup(Group group)
        {
            this.group = group;

            InitializeComponent();
            bind_grid();

            this.tbname.Text = group.Name;
            this.tbdescription.Text = group.Description;
            this.tbfilterwords.Text = group.Keywords;   

            cbincludelocation.Checked = group.LocText;
            cbincludenodetextinfiltering.Checked = group.NodeText;
            cbusehtmlemail.Checked = group.HTML;
            cbusebcc.Checked = group.BCC;
            cbsendreportsonly.Checked = group.Reports;
        }

        private void btsave_Click(object sender, EventArgs e)
        {

            // save changes to group
            group.Name = this.tbname.Text;
            group.Description = this.tbdescription.Text;
            group.Keywords = this.tbfilterwords.Text;  
            group.LocText = cbincludelocation.Checked;
            group.NodeText = cbincludenodetextinfiltering.Checked;
            group.HTML = cbusehtmlemail.Checked;
            group.BCC = cbusebcc.Checked;
            group.Reports = cbsendreportsonly.Checked;

            this.DialogResult = DialogResult.OK;
        }

        private void btcancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }


        private void bind_grid()
        {
            dgaddresses.AutoGenerateColumns = false;
            dgaddresses.AllowUserToAddRows = false; // optional, avoids new-row placeholder

            DataGridViewTextBoxColumn titleemail = new DataGridViewTextBoxColumn();
            titleemail.Name = "colEmail";
            titleemail.DataPropertyName = "Email";
            titleemail.HeaderText = "Email";
            titleemail.Width = 300;

           

            // Button column to delete the current address row
            DataGridViewButtonColumn deleteButton = new DataGridViewButtonColumn();
            deleteButton.Name = "colDelete";
            deleteButton.HeaderText = "";
            deleteButton.Text = "Delete";
            deleteButton.UseColumnTextForButtonValue = true;
            deleteButton.Width = 60;

            dgaddresses.Columns.Add(titleemail);
            
           
            dgaddresses.Columns.Add(deleteButton);

            // dgaddresses.ReadOnly = true;
            dgaddresses.DataSource = null;

            _addressesSource = new BindingSource();
            _addressesSource.DataSource = new BindingList<Address>(group.Addresses ?? new List<Address>());
            dgaddresses.DataSource = _addressesSource;

            // Commit edits immediately when current cell is dirty (useful for checkboxes or quick edits)
            dgaddresses.CurrentCellDirtyStateChanged += Dgaddresses_CurrentCellDirtyStateChanged;
            // When a cell value changes, ensure the underlying Address instance is updated
            dgaddresses.CellValueChanged += Dgaddresses_CellValueChanged;
            // Handle add/delete button clicks
            dgaddresses.CellContentClick += Dgaddresses_CellContentClick;
            // Optional: handle user-added rows to initialize underlying list (if adding is allowed)
            dgaddresses.UserAddedRow += Dgaddresses_UserAddedRow;
            // Optional: handle user-deleted rows to keep state consistent
            dgaddresses.UserDeletingRow += Dgaddresses_UserDeletingRow;
        }

        private void RefreshAddressesBinding()
        {
            // Rebind to force the grid to refresh when underlying List<T> is modified.
            dgaddresses.DataSource = null;
            dgaddresses.DataSource = group.Addresses;
            dgaddresses.Refresh();
        }

        private void Dgaddresses_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
        {
            if (dgaddresses.IsCurrentCellDirty)
            {
                // Commit the edit so CellValueChanged fires immediately
                dgaddresses.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void Dgaddresses_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            // Ignore header or invalid rows
            if (e.RowIndex < 0) return;
            if (group == null) return;
            if (e.RowIndex >= group.Addresses.Count) return;

            var row = dgaddresses.Rows[e.RowIndex];

            // Safely read cell values and update the underlying Address instance.
            var addr = group.Addresses[e.RowIndex];
            var emailCell = row.Cells["colEmail"].Value;
           

            addr.Email = emailCell?.ToString() ?? string.Empty;
           
        }

        private void Dgaddresses_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (group == null) return;

            var columnName = dgaddresses.Columns[e.ColumnIndex].Name;

            if (columnName == "colAdd")
            {
                // Insert a new Address after the clicked row
                int insertIndex = e.RowIndex + 1;
                var newAddr = new Address();
                if (insertIndex >= 0 && insertIndex <= group.Addresses.Count)
                {
                    group.Addresses.Insert(insertIndex, newAddr);
                }
                else
                {
                    group.Addresses.Add(newAddr);
                    insertIndex = group.Addresses.Count - 1;
                }

                RefreshAddressesBinding();

                // Select the new row and put focus in the email cell for easier editing
                if (insertIndex >= 0 && insertIndex < dgaddresses.Rows.Count)
                {
                    dgaddresses.ClearSelection();
                    var targetRow = dgaddresses.Rows[insertIndex];
                    targetRow.Selected = true;
                    dgaddresses.CurrentCell = targetRow.Cells["colEmail"];
                    dgaddresses.BeginEdit(true);
                }
            }
            else if (columnName == "colDelete")
            {
                int rowIndex = e.RowIndex;
                if (rowIndex < 0 || rowIndex >= group.Addresses.Count) return;

                var addr = group.Addresses[rowIndex];
                string display = string.IsNullOrWhiteSpace(addr.Email) ? addr.Pin : addr.Email;
                var dr = MessageBox.Show("Are you sure you want to delete address '" + display + "'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    group.Addresses.RemoveAt(rowIndex);
                    RefreshAddressesBinding();
                }
            }
        }

        private void Dgaddresses_UserAddedRow(object? sender, DataGridViewRowEventArgs e)
        {
            // If users can add rows directly in the grid, ensure the underlying list has a corresponding Address.
            // DataGridView already adds a new bound item when bound to a List<T>, but if you use a non-binding list,
            // you may need to handle initialization here. For safety, ensure list size covers the new row index.
            int newRowIndex = e.Row.Index;
            if (newRowIndex < 0) return;
            while (newRowIndex >= group.Addresses.Count)
            {
                group.Addresses.Add(new Address());
            }

            RefreshAddressesBinding();
        }

        private void Dgaddresses_UserDeletingRow(object? sender, DataGridViewRowCancelEventArgs e)
        {
            // Remove the underlying Address when a row is deleted by the user.
            int rowIndex = e.Row.Index;
            if (rowIndex < 0) return;
            if (rowIndex < group.Addresses.Count)
            {
                group.Addresses.RemoveAt(rowIndex);
            }

            RefreshAddressesBinding();
        }

        private void btaddaddress_Click(object sender, EventArgs e)
        {

            // Insert a new Address after the clicked row
            int insertIndex = group.Addresses.Count + 1;
            var newAddr = new Address();
            if (insertIndex >= 0 && insertIndex <= group.Addresses.Count)
            {
                group.Addresses.Add(newAddr);
            }
            else
            {
                group.Addresses.Add(newAddr);
                insertIndex = group.Addresses.Count - 1;
            }

            RefreshAddressesBinding();

            // Select the new row and put focus in the email cell for easier editing
            if (insertIndex >= 0 && insertIndex < dgaddresses.Rows.Count)
            {
                dgaddresses.ClearSelection();
                var targetRow = dgaddresses.Rows[insertIndex];
                targetRow.Selected = true;
                dgaddresses.CurrentCell = targetRow.Cells["colEmail"];
                dgaddresses.BeginEdit(true);
            }
        }
    }
}
