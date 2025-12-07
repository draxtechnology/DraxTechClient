using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drax360Client.Panels.Email
{
    public partial class frmemaigroups : Form
    {
        PanelEmailClient? panelemailclient = null;

        public frmemaigroups()
        {
            InitializeComponent();
            load_email_groups();
        }

        private void load_email_groups()
        {
            panelemailclient = new PanelEmailClient();

            bind_grid();
        }

        private void bind_grid()
        {
            dgemailgroups.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn titlename = new DataGridViewTextBoxColumn();
            titlename.DataPropertyName = "Name";
            titlename.HeaderText = "Name";
            titlename.Name = "colName";
            titlename.Width = 300;

            DataGridViewCheckBoxColumn titlenabled = new DataGridViewCheckBoxColumn();
            titlenabled.DataPropertyName = "Enabled";
            titlenabled.HeaderText = "Enabled";
            titlenabled.Name = "colEnabled";

            DataGridViewTextBoxColumn titledescription = new DataGridViewTextBoxColumn();
            titledescription.DataPropertyName = "Description";
            titledescription.HeaderText = "Description";
            titledescription.Name = "colDescription";
            titledescription.Width = 300;

            dgemailgroups.Columns.Add(titlename);
            dgemailgroups.Columns.Add(titlenabled);
            dgemailgroups.Columns.Add(titledescription);

            //dgemailgroups.ReadOnly = true;
            dgemailgroups.DataSource = null;

            if (panelemailclient != null)
            {
                dgemailgroups.DataSource = panelemailclient.Groups;
            }
            index_changed();
        }

        private void dgemailgroups_SelectionChanged(object sender, EventArgs e)
        {
            index_changed();
        }

        // Safely resolves the selected index. Returns -1 when no valid selection exists.
        private int GetSelectedIndex()
        {
            if (panelemailclient == null) return -1;

            if (dgemailgroups.SelectedRows.Count > 0)
            {
                int idx = dgemailgroups.SelectedRows[0].Index;
                return idx;
            }

            if (dgemailgroups.CurrentRow != null)
            {
                return dgemailgroups.CurrentRow.Index;
            }

            return -1;
        }

        private void index_changed()
        {
            bool enabled = false;

            int index = GetSelectedIndex();
            if (panelemailclient != null && index >= 0 && index < panelemailclient.Groups.Count)
            {
                enabled = true;
            }

            btedit.Enabled = enabled;
            btdelete.Enabled = enabled;
            btsendtest.Enabled = enabled;
        }

        private void btedit_Click(object sender, EventArgs e)
        {
            int index = GetSelectedIndex();
            if (index < 0 || panelemailclient == null) return;
            if (index >= panelemailclient.Groups.Count) return;

            frmemailgroup frm = new frmemailgroup(panelemailclient.Groups[index]);
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                panelemailclient.Groups.RemoveAt(index);
                panelemailclient.Groups.Insert(index, frm.group);

                dgemailgroups.Refresh();
                //dgemailgroups.ClearSelection();
                index_changed();
            }
            frm.Dispose();
        }

        private void btdelete_Click(object sender, EventArgs e)
        {
            int index = GetSelectedIndex();
            if (index < 0 || panelemailclient == null) return;
            if (index >= panelemailclient.Groups.Count) return;

            string groupname = panelemailclient.Groups[index].Name;
            DialogResult dr = MessageBox.Show("Are you sure you want to delete email group " + groupname + "?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                panelemailclient.Groups.RemoveAt(index);

                bind_grid();
            }
        }

        // Pseudocode / Plan:
        // 1. Create a new Group with default values.
        // 2. Show the edit dialog for the new group.
        // 3. If the dialog returns OK:
        //    a. Add the new group to panelemailclient.Groups (if panelemailclient != null).
        //    b. Re-bind the grid so it reflects the new item.
        //    c. Compute the index of the newly added group (Count - 1).
        //    d. Clear any existing selection.
        //    e. If the new index is valid:
        //       - Select the corresponding row.
        //       - Set CurrentCell to a cell in that row so selection is visible.
        //       - Scroll the grid so the new row is visible (set FirstDisplayedScrollingRowIndex).
        //    f. Update UI state by calling index_changed() (bind_grid already calls it, but ensure correctness).
        // 4. Dispose the dialog.
        private void btaddnew_Click(object sender, EventArgs e)
        {
            Group group = new Group();
            group.Enabled = true;
            group.Name = "New Group";
            group.Description = "";
            frmemailgroup frm = new frmemailgroup(group);
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (panelemailclient != null)
                {
                    panelemailclient.Groups.Add(frm.group);


                    // Re-bind to refresh the grid and its data source
                    bind_grid();

                    // Select the newly added row
                    if (panelemailclient != null)
                    {
                        int newIndex = panelemailclient.Groups.Count - 1;
                        if (newIndex >= 0 && dgemailgroups.Rows.Count > newIndex)
                        {
                            dgemailgroups.ClearSelection();

                            // Select the row and set CurrentCell to make selection visible
                            dgemailgroups.Rows[newIndex].Selected = true;
                            if (dgemailgroups.Rows[newIndex].Cells.Count > 0)
                            {
                                dgemailgroups.CurrentCell = dgemailgroups.Rows[newIndex].Cells[0];
                            }

                            // Ensure the row is scrolled into view
                            try
                            {
                                dgemailgroups.FirstDisplayedScrollingRowIndex = newIndex;
                            }
                            catch
                            {
                                // Ignore if unable to set scrolling index (guards against exceptional states)
                            }

                            // Update button states
                            index_changed();
                        }
                    }
                }
            }
            frm.Dispose();
        }

        private void frmemaigroups_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Ensure any current cell edits are committed so CellValueChanged fires before we save
            try
            {
                if (dgemailgroups.IsCurrentCellInEditMode)
                {
                    dgemailgroups.EndEdit();
                }

                dgemailgroups.CommitEdit(DataGridViewDataErrorContexts.Commit);

                // End edit on the CurrencyManager (if bound) to push changes to the underlying list
                var cm = this.BindingContext[dgemailgroups.DataSource] as CurrencyManager;
                cm?.EndCurrentEdit();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Warning: failed to commit dgemailgroups edits before save: " + ex);
            }

            if (panelemailclient != null)
            {
                panelemailclient.Save();
            }
        }

        private void dgemailgroups_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgemailgroups_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore header or invalid rows
            if (e.RowIndex < 0) return;
            if (panelemailclient.Groups == null) return;
            if (e.RowIndex >= panelemailclient.Groups.Count) return;

            var row = dgemailgroups.Rows[e.RowIndex];

            // Safely read cell values and update the underlying Address instance.
            var grp = panelemailclient.Groups[e.RowIndex];
            string name = row.Cells["colName"].Value.ToString();
            bool enabled = (bool) row.Cells["colEnabled"].Value;
            string description  = row.Cells["colDescription"].Value.ToString();


            grp.Name = name;
            grp.Enabled= enabled;
            grp.Description = description;
        }
    }
}

