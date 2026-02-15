using DraxClient.Panels.Email;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DraxClient.Panels.RSM
{
    public partial class frmdevices : Form
    {
        private readonly object _fileLock = new();
        private static readonly string FileName = "devices.json";
        private static readonly string StorageDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DraxClient");
        private static readonly string StorageFilePath = Path.Combine(StorageDirectory, FileName);

        public List<Device> Devices = new List<Device>();


        public frmdevices()
        {
            InitializeComponent();
            if (load_devices())
            {
                bind_grid();
            }
        }
        private bool load_devices()
        {
            lock (_fileLock)
            {
                try
                {
                    if (!File.Exists(StorageFilePath)) return false;

                    string json = File.ReadAllText(StorageFilePath);
                    if (string.IsNullOrWhiteSpace(json)) return false;

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        ReadCommentHandling = JsonCommentHandling.Skip,
                        AllowTrailingCommas = true
                    };

                    var loaded = JsonSerializer.Deserialize<List<Device>>(json, options);
                    if (loaded != null)
                    {
                        Devices = loaded;
                        return true;
                    }

                    return false;
                }
                catch
                {
                    // swallow exceptions for resiliency; callers can decide next steps.
                    return false;
                }
            }


        }

        public bool Save()
        {
            lock (_fileLock)
            {
                try
                {
                    if (!Directory.Exists(StorageDirectory))
                    {
                        Directory.CreateDirectory(StorageDirectory);
                    }

                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true
                    };

                    string json = JsonSerializer.Serialize(Devices, options);

                    // Write atomically: write to temp then move
                    string temp = StorageFilePath + ".tmp";
                    File.WriteAllText(temp, json, System.Text.Encoding.UTF8);
                    File.Copy(temp, StorageFilePath, overwrite: true);
                    File.Delete(temp);

                    return true;
                }
                catch
                {
                    // swallow exceptions for resiliency
                    return false;
                }
            }
        }


        private void bind_grid()
        {
            dgdevices.DataSource = null;
            dgdevices.Columns.Clear();
            dgdevices.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn tbcname = new DataGridViewTextBoxColumn();
            tbcname.DataPropertyName = "Name";
            tbcname.HeaderText = "Name";
            tbcname.Name = "colName";
            tbcname.Width = 300;


            DataGridViewTextBoxColumn tbcip = new DataGridViewTextBoxColumn();
            tbcip.DataPropertyName = "IP";
            tbcip.HeaderText = "IP";
            tbcip.Name = "colIP";
            tbcip.Width = 100;


            dgdevices.Columns.Add(tbcname);
            dgdevices.Columns.Add(tbcip);


            //dgemailgroups.ReadOnly = true;
            dgdevices.DataSource = Devices;


            index_changed();
        }

        private void dgemailgroups_SelectionChanged(object sender, EventArgs e)
        {
            index_changed();
        }

        // Safely resolves the selected index. Returns -1 when no valid selection exists.
        private int GetSelectedIndex()
        {


            if (dgdevices.SelectedRows.Count > 0)
            {
                int idx = dgdevices.SelectedRows[0].Index;
                return idx;
            }

            if (dgdevices.CurrentRow != null)
            {
                return dgdevices.CurrentRow.Index;
            }

            return -1;
        }

        private void index_changed()
        {
            bool enabled = false;

            int index = GetSelectedIndex();


            btedit.Enabled = enabled;
            btdelete.Enabled = enabled;

        }

        // returns true when the provided IP matches an existing device IP,
        // excluding the optional index (useful when editing an existing entry)
        private bool IsDuplicateIp(string ip, int excludeIndex = -1)
        {
            if (string.IsNullOrWhiteSpace(ip)) return false;

            string candidate = ip.Trim();
            return Devices
                .Select((d, i) => new { Device = d, Index = i })
                .Any(x => ( x.Index != excludeIndex)
                          && string.Equals(x.Device?.IP?.Trim(), candidate, StringComparison.OrdinalIgnoreCase));
        }

        private void btedit_Click(object sender, EventArgs e)
        {
            int index = GetSelectedIndex();
            bool hasSelection = index >= 0 && index < Devices.Count;
            if (!hasSelection) return;

            frmtempdiscovery frm = new frmtempdiscovery(Devices[index]);

            var res = frm.ShowDialog();
            if (res != DialogResult.OK) return;

            // prevent duplicate IPs: if any other device (different index) has same IP, block the change
            string newIp = frm.OurDevice?.IP?.Trim() ?? string.Empty;
            if (!string.IsNullOrEmpty(newIp) && IsDuplicateIp(newIp, index))
            {
                MessageBox.Show("Another device with the same IP address already exists.", "Duplicate IP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Devices[index] = frm.OurDevice;


            Save();

            bind_grid();
            button_handler();
        }



        private void btdelete_Click(object sender, EventArgs e)
        {
            int index = GetSelectedIndex();
            bool hasSelection = index >= 0 && index < Devices.Count;
            if (!hasSelection) return;

            DialogResult dr = MessageBox.Show("Are you sure you want to delete " + Devices[index].Name + "?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                Devices.RemoveAt(index);
                Save();

                bind_grid();
                button_handler();
            }
        }



        private void btdiscover_Click(object sender, EventArgs e)
        {
            frmdiscovery frm = new frmdiscovery();
            var res = frm.ShowDialog();
            if (res == DialogResult.OK)
            {
                // reload devices
            }
        }

        // used as a placeholder for the actual discovery form, which is not yet implemented. This will allow us to test the flow of opening the discovery form and reloading devices without having the actual discovery logic in place.
        private void bttempdiscovery_Click(object sender, EventArgs e)
        {
            frmtempdiscovery frm = new frmtempdiscovery(new Device());

            var res = frm.ShowDialog();
            if (res == DialogResult.OK)
            {

                // prevent duplicate IPs: if any other device (different index) has same IP, block the change
                string newIp = frm.OurDevice?.IP?.Trim() ?? string.Empty;
                if (!string.IsNullOrEmpty(newIp) && IsDuplicateIp(newIp))
                {
                    MessageBox.Show("Another device with the same IP address already exists.", "Duplicate IP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                Devices.Add(frm.OurDevice);
                Save();
                bind_grid();

                // select the newly added row
                int newIndex = Devices.Count - 1;
                if (newIndex >= 0 && newIndex < dgdevices.Rows.Count)
                {
                    dgdevices.ClearSelection();

                    var row = dgdevices.Rows[newIndex];
                    row.Selected = true;

                    // Set CurrentCell so keyboard focus and CurrentRow are correct (guard cell count)
                    if (dgdevices.Columns.Count > 0)
                    {
                        dgdevices.CurrentCell = row.Cells[0];
                    }

                    // Try to scroll the new row into view, center it if possible
                    try
                    {
                        int displayCount = dgdevices.DisplayedRowCount(false);
                        int firstToShow = Math.Max(0, newIndex - (displayCount / 2));
                        dgdevices.FirstDisplayedScrollingRowIndex = Math.Min(firstToShow, Math.Max(0, dgdevices.Rows.Count - 1));
                    }
                    catch
                    {
                        // ignored - setting FirstDisplayedScrollingRowIndex can throw if control not yet fully initialized
                    }
                }

                button_handler();
            }
        }

        private void dgdevices_SelectionChanged(object sender, EventArgs e)
        {
            button_handler();
        }

        private void button_handler()
        {
            int index = GetSelectedIndex();
            bool hasSelection = index >= 0 && index < Devices.Count;
            btedit.Enabled = hasSelection;
            btdelete.Enabled = hasSelection;
        }


    }
}