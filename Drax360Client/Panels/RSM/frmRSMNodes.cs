using System.IO.Pipes;
using System.Text;
using System.Text.Json;

namespace DraxClient.Panels.RSM
{
    // Live node-status grid for the RSM (SmartWatch) Network Manager.
    // Mirrors VB6 frmRSMNodes / vsfNodes (7 columns: Node, Site Name, Node Name,
    // Node Type, Status, Messages, Address). Polls RSMNODES on the named pipe
    // every 2 s and colours Status cells green (ONLINE) or red (OFFLINE).
    //
    // Also owns device management (previously a separate frmdevices): nodes can
    // be named/provisioned via Properties (edit), Discovery (add), and Delete —
    // all persisted to devices.json so the service can enrich log lines.
    public partial class frmRSMNodes : Form
    {
        private const string kPipeName = "DraxTechnologyPipeSend";
        private readonly object _fileLock = new();
        private static readonly string _devicesPath = Paths.GetFile("devices.json");

        private List<Device> _devices = new();
        private System.Windows.Forms.Timer _timer;

        public frmRSMNodes()
        {
            InitializeComponent();
            TopMost = true;
        }

        private void frmRSMNodes_Load(object sender, EventArgs e)
        {
            Paths.MigrateLegacyFile("devices.json");
            LoadDevices();
            Refresh_Grid();
            _timer = new System.Windows.Forms.Timer { Interval = 2000 };
            _timer.Tick += (_, _) => Refresh_Grid();
            _timer.Start();
        }

        private void frmRSMNodes_FormClosed(object sender, FormClosedEventArgs e)
        {
            _timer?.Stop();
            _timer?.Dispose();
        }

        // ── Device list (devices.json) ────────────────────────────────────────

        private void LoadDevices()
        {
            lock (_fileLock)
            {
                try
                {
                    if (!File.Exists(_devicesPath)) return;
                    string json = File.ReadAllText(_devicesPath);
                    var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    _devices = JsonSerializer.Deserialize<List<Device>>(json, opts) ?? new List<Device>();
                }
                catch { /* keep empty list on error */ }
            }
        }

        private void SaveDevices()
        {
            lock (_fileLock)
            {
                try
                {
                    string dir = Path.GetDirectoryName(_devicesPath)!;
                    if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                    var opts = new JsonSerializerOptions { WriteIndented = true };
                    string json = JsonSerializer.Serialize(_devices, opts);
                    string tmp = _devicesPath + ".tmp";
                    File.WriteAllText(tmp, json, Encoding.UTF8);
                    File.Copy(tmp, _devicesPath, overwrite: true);
                    File.Delete(tmp);
                }
                catch { }
            }
        }

        private Device? FindDeviceByIP(string ip)
            => _devices.FirstOrDefault(d =>
                string.Equals(d.IP?.Trim(), ip?.Trim(), StringComparison.OrdinalIgnoreCase));

        private bool IsDuplicateIP(string ip, Guid excludeId)
            => _devices.Any(d =>
                d.ID != excludeId &&
                string.Equals(d.IP?.Trim(), ip?.Trim(), StringComparison.OrdinalIgnoreCase));

        // ── Live node grid ────────────────────────────────────────────────────

        private void Refresh_Grid()
        {
            string json;
            try { json = SendCmd("RSMNODES"); }
            catch { return; }

            List<NodeSnapshot> nodes;
            try
            {
                nodes = JsonSerializer.Deserialize<List<NodeSnapshot>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                    ?? new List<NodeSnapshot>();
            }
            catch { return; }

            int selectedNode = -1;
            if (dgNodes.SelectedRows.Count > 0 && dgNodes.SelectedRows[0].Tag is int n)
                selectedNode = n;

            dgNodes.SuspendLayout();
            dgNodes.Rows.Clear();

            foreach (var node in nodes)
            {
                // Enrich Name + Site from devices.json if the service hasn't
                // populated them yet. Site is always blank from the service today
                // (it has no per-node Site store), so it comes from config here.
                // Matched by IP for now — stable-key (serial) match is a later step.
                string name = node.Name;
                string site = node.Site;
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(site))
                {
                    var d = FindDeviceByIP(node.Address);
                    if (d != null)
                    {
                        if (string.IsNullOrEmpty(name)) name = d.Name;
                        if (string.IsNullOrEmpty(site)) site = d.Site;
                    }
                }

                int idx = dgNodes.Rows.Add(node.Node, site, name,
                    node.TypeText, node.Status, node.Messages, node.Address);

                DataGridViewRow row = dgNodes.Rows[idx];
                row.Tag = node.Node;

                bool online = string.Equals(node.Status, "ONLINE", StringComparison.OrdinalIgnoreCase);
                DataGridViewCell cell = row.Cells[colStatus.Index];
                cell.Style.BackColor = online ? Color.Lime : Color.Red;
                cell.Style.ForeColor = Color.Black;
                cell.Style.SelectionBackColor = cell.Style.BackColor;
                cell.Style.SelectionForeColor = Color.Black;

                if (node.Node == selectedNode) row.Selected = true;
            }

            dgNodes.ResumeLayout();
            UpdateButtons();
        }

        private void dgNodes_SelectionChanged(object sender, EventArgs e) => UpdateButtons();

        private void UpdateButtons()
        {
            bool sel = dgNodes.SelectedRows.Count > 0;
            btProperties.Enabled = sel;
            btDelete.Enabled = sel;
            btRestart.Enabled = false; // pending RSMRESTART pipe verb
            btLicense.Enabled = sel;
        }

        // ── Button handlers ───────────────────────────────────────────────────

        private void btClose_Click(object sender, EventArgs e) => Close();

        // Properties — open the read-only per-node properties window
        // (VB6 frmRSMProperties: Properties / Status / Module Options pages).
        private void btProperties_Click(object sender, EventArgs e)
        {
            if (dgNodes.SelectedRows.Count == 0) return;
            DataGridViewRow sel = dgNodes.SelectedRows[0];
            int node = sel.Tag is int n ? n : (int)(sel.Cells[colNode.Index].Value ?? 0);
            string name = sel.Cells[colName.Index].Value?.ToString() ?? "";
            string site = sel.Cells[colSite.Index].Value?.ToString() ?? "";

            using var frm = new frmRSMProperties(node, name, site);
            frm.ShowDialog();
        }

        // Double-click a node to edit its device entry (Name / Site / IP) in the
        // discovery dialog. This was previously behind the Properties button;
        // Properties now opens the read-only properties window, so device editing
        // lives here until the Module-Options edit page lands (later tier).
        private void dgNodes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgNodes.SelectedRows.Count == 0) return;
            string ip = dgNodes.SelectedRows[0].Cells[colAddress.Index].Value?.ToString() ?? "";

            Device device = FindDeviceByIP(ip) ?? new Device { IP = ip };
            bool isNew = FindDeviceByIP(ip) == null;

            using var frm = new frmdiscovery { OurDevice = device, IsNew = isNew };
            if (frm.ShowDialog() != DialogResult.OK) return;

            string newIp = frm.OurDevice.IP?.Trim() ?? "";
            if (!string.IsNullOrEmpty(newIp) && IsDuplicateIP(newIp, device.ID))
            {
                MessageBox.Show("Another device with this IP already exists.", "Duplicate IP",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (isNew)
                _devices.Add(frm.OurDevice);
            else
            {
                int i = _devices.FindIndex(d => d.ID == device.ID);
                if (i >= 0) _devices[i] = frm.OurDevice;
            }

            SaveDevices();
            Refresh_Grid();
        }

        // Delete — remove the device entry for the selected node's IP
        private void btDelete_Click(object sender, EventArgs e)
        {
            if (dgNodes.SelectedRows.Count == 0) return;
            int node = (int)(dgNodes.SelectedRows[0].Cells[colNode.Index].Value ?? 0);
            string ip = dgNodes.SelectedRows[0].Cells[colAddress.Index].Value?.ToString() ?? "";

            var device = FindDeviceByIP(ip);
            if (device == null)
            {
                MessageBox.Show($"Node {node} has no device entry to remove.", "Nothing to delete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var dr = MessageBox.Show(
                $"Remove the device entry for node {node} ({device.Name}, {ip})?",
                "Confirm Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr != DialogResult.Yes) return;

            _devices.Remove(device);
            SaveDevices();
            Refresh_Grid();
        }

        // Discovery — add a new device entry
        private void btDiscovery_Click(object sender, EventArgs e)
        {
            using var frm = new frmdiscovery { OurDevice = new Device(), IsNew = true };
            if (frm.ShowDialog() != DialogResult.OK) return;

            string newIp = frm.OurDevice.IP?.Trim() ?? "";
            if (!string.IsNullOrEmpty(newIp) && IsDuplicateIP(newIp, frm.OurDevice.ID))
            {
                MessageBox.Show("A device with this IP already exists.", "Duplicate IP",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _devices.Add(frm.OurDevice);
            SaveDevices();
            Refresh_Grid();
        }

        private void btLicense_Click(object sender, EventArgs e)
            => MessageBox.Show("Install License not yet implemented.", "Install License",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

        // ── Pipe helper ───────────────────────────────────────────────────────

        private static string SendCmd(string cmd)
        {
            using var pipe = new NamedPipeClientStream(".", kPipeName, PipeDirection.InOut);
            pipe.Connect(3000);
            pipe.ReadMode = PipeTransmissionMode.Message;
            byte[] msg = Encoding.Default.GetBytes(cmd);
            pipe.Write(msg, 0, msg.Length);
            byte[] buf = new byte[4096];
            using var ms = new MemoryStream();
            do { int r = pipe.Read(buf, 0, buf.Length); ms.Write(buf, 0, r); }
            while (!pipe.IsMessageComplete);
            return Encoding.Default.GetString(ms.ToArray());
        }

        // POCO matching PanelRSM.BuildNodeSnapshot() JSON schema
        private class NodeSnapshot
        {
            public int Node { get; set; }
            public string Site { get; set; } = "";
            public string Name { get; set; } = "";
            public string Type { get; set; } = "";
            public string TypeText { get; set; } = "";
            public string Status { get; set; } = "";
            public long Messages { get; set; }
            public string Address { get; set; } = "";
        }
    }
}
