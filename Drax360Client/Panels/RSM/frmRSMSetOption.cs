using System.Text;

namespace DraxClient.Panels.RSM
{
    // Modal editor for a single RSM Module-Options value. Ports VB6
    // frmRSMSetOption: validate per option number, apply the Name4 padding rules
    // by module type, and hand back a wire-ready value in ResultValue. The caller
    // (frmRSMProperties) sends it to the service via the RSMSETOPT pipe verb.
    // Display-only fields never reach here.
    public partial class frmRSMSetOption : Form
    {
        // optSetGet numbers (authoritative RSMenum.bas).
        private const int OptDhcpName = 2, OptIp = 3, OptSubnet = 4, OptGateway = 5,
                          OptReport1 = 6, OptReport2 = 7, OptName4 = 13, OptPanelNumbers = 19;

        private readonly int _optionNumber;
        private readonly string _moduleType;    // raw code, for Name4 padding
        private readonly string _optionCaption;
        private readonly string _currentValue;

        // The validated/formatted value to send. Read after ShowDialog() == OK.
        public string ResultValue { get; private set; } = "";

        public frmRSMSetOption(int optionNumber, string optionCaption, string currentValue, string moduleType)
        {
            InitializeComponent();
            TopMost = true;
            _optionNumber = optionNumber;
            _optionCaption = optionCaption ?? "";
            _currentValue = currentValue ?? "";
            _moduleType = moduleType ?? "";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            lblOption.Text = _optionCaption;
            lblCurrent.Text = _currentValue;
            tbNew.Text = _currentValue;
            tbNew.SelectAll();
            tbNew.Focus();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            string raw = tbNew.Text.Trim();
            if (!ValidateAndFormat(raw, out string formatted, out string error))
            {
                MessageBox.Show(error, "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.None;   // keep the dialog open
                return;
            }
            ResultValue = formatted;
            DialogResult = DialogResult.OK;
            Close();
        }

        private bool ValidateAndFormat(string value, out string formatted, out string error)
        {
            formatted = value;
            error = "";
            switch (_optionNumber)
            {
                case OptDhcpName:
                    if (value.Length > 24) { error = "24 characters maximum."; return false; }
                    return true;
                case OptIp:
                    if (!IsDotNotation(value)) { error = "This is not a valid IP address."; return false; }
                    return true;
                case OptSubnet:
                    if (!IsDotNotation(value)) { error = "This is not a valid subnet mask."; return false; }
                    return true;
                case OptGateway:
                    if (!IsDotNotation(value)) { error = "This is not a valid gateway address."; return false; }
                    return true;
                case OptReport1:
                case OptReport2:
                    if (!IsDotNotation(value)) { error = "This is not a valid IP address."; return false; }
                    return true;
                case OptName4:
                    formatted = FormatName4(value);
                    return true;
                case OptPanelNumbers:
                    // Comma-separated list; the service joins the parts with the
                    // wire separator. No strict validation (mirrors the legacy).
                    return true;
                default:
                    return true;
            }
        }

        // Name4 (option 13) is module-type dependent — VB6 frmRSMSetOption pads to a
        // fixed 16 chars. NO/DX → YES/NO; ZI → 4-digit zero-padded; 4I/12/IO → a
        // 0/1 bit string.
        private string FormatName4(string value)
        {
            switch ((_moduleType ?? "").Trim().ToUpperInvariant())
            {
                case "NO":
                case "DX":
                    {
                        string first = value.Length > 0 ? value.Substring(0, 1).ToUpperInvariant() : "";
                        return (first == "Y" || first == "1") ? "YES".PadRight(16) : "NO".PadRight(16);
                    }
                case "ZI":
                    {
                        int.TryParse(value, out int n);
                        return n.ToString("0000").PadRight(16);
                    }
                case "4I":
                case "12":
                case "IO":
                    {
                        var sb = new StringBuilder();
                        foreach (char c in value)
                        {
                            if (c == '0' || c == '1') sb.Append(c);
                            if (sb.Length == 16) break;
                        }
                        return sb.ToString().PadRight(16, '0');
                    }
                default:
                    return value;
            }
        }

        private static bool IsDotNotation(string s)
        {
            string[] parts = s.Split('.');
            if (parts.Length != 4) return false;
            foreach (string p in parts)
                if (!int.TryParse(p, out int v) || v < 0 || v > 255) return false;
            return true;
        }
    }
}
