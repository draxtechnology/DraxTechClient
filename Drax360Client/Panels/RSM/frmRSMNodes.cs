using System.IO.Pipes;
using System.Text;
using System.Text.Json;

namespace DraxClient.Panels.RSM
{
    // Live node-status grid for the RSM (SmartWatch) Network Manager.
    // Mirrors VB6 frmRSMNodes / vsfNodes (7 columns: Node, Site Name, Node Name,
    // Node Type, Status, Messages, Address). Polls RSMNODES on the named pipe
    // every 2 s and colours Status cells green (ONLINE) or red (OFFLINE).
    public partial class frmRSMNodes : Form
    {
        private const string kPipeName = "DraxTechnologyPipeSend";
        private System.Windows.Forms.Timer _timer;

        public frmRSMNodes()
        {
            InitializeComponent();
            TopMost = true;
        }

        private void frmRSMNodes_Load(object sender, EventArgs e)
        {
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

        private void Refresh_Grid()
        {
            string json;
            try
            {
                json = SendCmd("RSMNODES");
            }
            catch
            {
                return; // service not available — keep old data
            }

            List<NodeSnapshot> nodes;
            try
            {
                nodes = JsonSerializer.Deserialize<List<NodeSnapshot>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                    ?? new List<NodeSnapshot>();
            }
            catch
            {
                return;
            }

            // Preserve selected row
            int selectedNode = -1;
            if (dgNodes.SelectedRows.Count > 0 && dgNodes.SelectedRows[0].Tag is int n)
                selectedNode = n;

            dgNodes.SuspendLayout();
            dgNodes.Rows.Clear();

            foreach (var node in nodes)
            {
                int idx = dgNodes.Rows.Add(
                    node.Node,
                    node.Site,
                    node.Name,
                    node.TypeText,
                    node.Status,
                    node.Messages,
                    node.Address);

                DataGridViewRow row = dgNodes.Rows[idx];
                row.Tag = node.Node;

                bool online = string.Equals(node.Status, "ONLINE", StringComparison.OrdinalIgnoreCase);
                DataGridViewCell statusCell = row.Cells[colStatus.Index];
                statusCell.Style.BackColor = online ? Color.Lime : Color.Red;
                statusCell.Style.ForeColor = Color.Black;
                statusCell.Style.SelectionBackColor = statusCell.Style.BackColor;
                statusCell.Style.SelectionForeColor = Color.Black;

                if (node.Node == selectedNode)
                    row.Selected = true;
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
            btRestart.Enabled = false; // TODO: enable once RSMRESTART pipe verb exists
            btLicense.Enabled = sel;
        }

        private void btClose_Click(object sender, EventArgs e) => Close();

        private void btDiscovery_Click(object sender, EventArgs e)
            => new frmdiscovery().ShowDialog();

        private void btDelete_Click(object sender, EventArgs e)
        {
            if (dgNodes.SelectedRows.Count == 0) return;
            int node = (int)(dgNodes.SelectedRows[0].Cells[colNode.Index].Value ?? 0);
            var dr = MessageBox.Show(
                $"Remove node {node} from the list? (No command is sent to the panel.)",
                "Confirm Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
                Refresh_Grid();
        }

        private void btProperties_Click(object sender, EventArgs e)
        {
            // Properties dialog not yet implemented — placeholder
            MessageBox.Show("Properties dialog coming soon.", "Properties",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btLicense_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Install License not yet implemented.", "Install License",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static string SendCmd(string cmd)
        {
            using var pipe = new NamedPipeClientStream(".", kPipeName, PipeDirection.InOut);
            pipe.Connect(3000);
            pipe.ReadMode = PipeTransmissionMode.Message;
            byte[] msg = Encoding.Default.GetBytes(cmd);
            pipe.Write(msg, 0, msg.Length);

            byte[] buf = new byte[4096];
            using var ms = new MemoryStream();
            do
            {
                int read = pipe.Read(buf, 0, buf.Length);
                ms.Write(buf, 0, read);
            } while (!pipe.IsMessageComplete);

            return Encoding.Default.GetString(ms.ToArray());
        }

        // POCO matching the RSMNODES JSON schema from PanelRSM.BuildNodeSnapshot()
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
