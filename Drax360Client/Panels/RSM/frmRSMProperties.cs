using System.IO.Pipes;
using System.Text;
using System.Text.Json;

namespace DraxClient.Panels.RSM
{
    // Read-only per-node "Node Properties" window for the RSM (SmartWatch)
    // Network Manager. Ports the three read-only pages of VB6 frmRSMProperties —
    // Properties, Status, Module Options — driven by the service's RSMNODE pipe
    // verb (PanelRSM.BuildNodePropertiesSnapshot). Volatile values refresh on a
    // 2 s timer, mirroring the legacy tmrUpdate.
    //
    // The editable Module-Options SET path (DHCP/IP/subnet/gateway/reports/
    // master-panel-ID and the per-type options) is a later tier — this window is
    // display-only for now.
    public partial class frmRSMProperties : Form
    {
        private const string kPipeName = "DraxTechnologyPipeSend";
        private readonly int _node;
        private readonly string _fallbackName;
        private readonly string _fallbackSite;
        private System.Windows.Forms.Timer _timer;
        private int _refreshTick;
        private string _type = "";   // raw module-type code from the last snapshot

        // fallbackName/Site come from the grid row, which already enriches from
        // devices.json by IP — used when the service hasn't populated them yet.
        public frmRSMProperties(int node, string fallbackName = "", string fallbackSite = "")
        {
            InitializeComponent();
            TopMost = true;
            _node = node;
            _fallbackName = fallbackName ?? "";
            _fallbackSite = fallbackSite ?? "";
            Text = $"Node {node} Properties";
        }

        private void frmRSMProperties_Load(object sender, EventArgs e)
        {
            Refresh_Pages();
            // Ask the service to fetch this node's option values (GET batch) so
            // the Module Options page fills in. The GAK replies land a moment
            // later and are picked up by the next Refresh_Pages tick.
            RequestModuleOptions();
            _timer = new System.Windows.Forms.Timer { Interval = 2000 };
            _timer.Tick += OnTick;
            _timer.Start();
        }

        private void frmRSMProperties_FormClosed(object sender, FormClosedEventArgs e)
        {
            _timer?.Stop();
            _timer?.Dispose();
        }

        private void OnTick(object sender, EventArgs e)
        {
            Refresh_Pages();
            // While the operator is on the Options page, re-issue the GET batch
            // every ~10 s to catch any replies that didn't come back first time
            // (mirrors the legacy form re-requesting on a later tick).
            _refreshTick++;
            if (tabs.SelectedTab == tabOptions && _refreshTick % 5 == 0)
                RequestModuleOptions();
        }

        private void tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabs.SelectedTab == tabOptions) RequestModuleOptions();
        }

        private void RequestModuleOptions()
        {
            try { SendCmd("RSMNODEGET|" + _node); } catch { /* fire-and-forget */ }
        }

        private void btClose_Click(object sender, EventArgs e) => Close();

        // ── Page population ─────────────────────────────────────────────────────

        private void Refresh_Pages()
        {
            string json;
            try { json = SendCmd("RSMNODE|" + _node); }
            catch { return; }

            NodeProperties p;
            try
            {
                p = JsonSerializer.Deserialize<NodeProperties>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch { return; }
            if (p == null) return;

            _type = p.Type ?? "";

            string name = string.IsNullOrEmpty(p.Name) ? _fallbackName : p.Name;
            string site = string.IsNullOrEmpty(p.Site) ? _fallbackSite : p.Site;

            FillGrid(dgProperties, new (string, string)[]
            {
                ("Node Number",              p.Node.ToString()),
                ("Site Name",                site),
                ("Node Name",                name),
                ("Node Type",                p.NodeType),
                ("Module Type",              p.ModuleType),
                ("Reported IP Address",      p.ReportedIP),
                ("Online Status",            p.OnlineStatus),
                ("Serial Number",            p.Serial),
                ("Module Options",           p.ModuleOptions),
                ("Software Version",         p.SoftwareVersion),
                ("Last Known IP Address",    p.LastKnownIP),
            });

            FillGrid(dgStatus, new (string, string)[]
            {
                ("Node Number",           p.Node.ToString()),
                ("Status",                p.OnlineStatus),
                ("Online at",             p.OnlineAt),
                ("Last message",          p.LastMessage),
                ("Received messages",     p.RxMessages.ToString()),
                ("Transmitted messages",  p.TxMessages.ToString()),
                ("Last Recorded Restart", p.LastRestart),
            });

            FillGrid(dgOptions, new (string, string)[]
            {
                ("Node Number",              p.Node.ToString()),
                ("DHCP Name",                p.DhcpName),
                ("IP Address",               p.IpAddress),
                ("Subnet Mask",              p.SubnetMask),
                ("Gateway",                  p.Gateway),
                ("Reporting To [1]",         p.ReportsTo1),
                ("Reporting To [2]",         p.ReportsTo2),
                ("TCP Port",                 p.TcpPort),
                ("Listening Port",           p.ListeningPort),
                ("Master Panel ID",          p.MasterPanelID),
                ("Site Name",                site),
                ("Node Name",                name),
                (OptionFieldCaption(p.Type), p.ReverseInputs),
            });

            // Colour the online/offline cell green/red, matching the live grid.
            PaintStatusCell(dgProperties, "Online Status", p.OnlineStatus);
            PaintStatusCell(dgStatus, "Status", p.OnlineStatus);
        }

        private static void FillGrid(DataGridView grid, (string Field, string Value)[] rows)
        {
            grid.SuspendLayout();
            if (grid.Rows.Count != rows.Length)
            {
                grid.Rows.Clear();
                foreach (var r in rows) grid.Rows.Add(r.Field, r.Value);
            }
            else
            {
                // Update values in place so the timer refresh doesn't flicker or
                // lose the user's selection.
                for (int i = 0; i < rows.Length; i++)
                {
                    grid.Rows[i].Cells[0].Value = rows[i].Field;
                    grid.Rows[i].Cells[1].Value = rows[i].Value;
                }
            }
            grid.ResumeLayout();
        }

        private static void PaintStatusCell(DataGridView grid, string field, string status)
        {
            bool online = string.Equals(status, "Online", StringComparison.OrdinalIgnoreCase);
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (!string.Equals(row.Cells[0].Value?.ToString(), field, StringComparison.Ordinal))
                    continue;
                DataGridViewCell cell = row.Cells[1];
                cell.Style.BackColor = online ? Color.Lime : Color.Red;
                cell.Style.ForeColor = Color.Black;
                cell.Style.SelectionBackColor = cell.Style.BackColor;
                cell.Style.SelectionForeColor = Color.Black;
                break;
            }
        }

        // Per-module-type option caption (VB6 frmRSMProperties row 12 — always the
        // setgetName4 slot, caption varies by module type).
        private static string OptionFieldCaption(string typeCode)
        {
            switch ((typeCode ?? "").Trim().ToUpperInvariant())
            {
                case "4I":
                case "12":
                case "IO": return "Reverse Inputs";
                case "ZI": return "Level 4 Access Code";
                case "NO": return "Networked Panel";
                case "DX": return "Apollo Protocol";
                default:   return "Reverse Inputs";
            }
        }

        // ── Pipe helper (mirrors frmRSMNodes.SendCmd) ───────────────────────────

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

        // POCO matching PanelRSM.BuildNodePropertiesSnapshot() JSON schema.
        private class NodeProperties
        {
            // Properties page
            public int Node { get; set; }
            public string Site { get; set; } = "";
            public string Name { get; set; } = "";
            public string Type { get; set; } = "";          // raw module-type code
            public string NodeType { get; set; } = "";       // expanded label
            public string ModuleType { get; set; } = "";
            public string ReportedIP { get; set; } = "";
            public string OnlineStatus { get; set; } = "";
            public string Serial { get; set; } = "";
            public string ModuleOptions { get; set; } = "";
            public string SoftwareVersion { get; set; } = "";
            public string LastKnownIP { get; set; } = "";

            // Status page
            public string OnlineAt { get; set; } = "";
            public string LastMessage { get; set; } = "";
            public long RxMessages { get; set; }
            public long TxMessages { get; set; }
            public string LastRestart { get; set; } = "";

            // Module Options page
            public string DhcpName { get; set; } = "";
            public string IpAddress { get; set; } = "";
            public string SubnetMask { get; set; } = "";
            public string Gateway { get; set; } = "";
            public string ReportsTo1 { get; set; } = "";
            public string ReportsTo2 { get; set; } = "";
            public string TcpPort { get; set; } = "";
            public string ListeningPort { get; set; } = "";
            public string MasterPanelID { get; set; } = "";
            public string ReverseInputs { get; set; } = "";
        }
    }
}
