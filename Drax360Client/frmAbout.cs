using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DraxClient
{
    public partial class frmAbout : Form
    {
        private const string kpipenamesend = "DraxTechnologyPipeSend";
        private const char kpipedelim = '|';

        public frmAbout()
        {
            this.TopMost = true;
            InitializeComponent();

            string clientVersion = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
            lblclientversion.Text = "Client Version: V" + clientVersion;

            string _panelType = sendcmd("GetPanelType");
            string versions = sendcmd("GETVERSIONS");
            string[] parts = versions.Split('|');
            lblversion.Text = "Service Version: V" + (parts.Length > 0 && !parts[0].StartsWith("Error") ? parts[0] : "Unavailable");
            lblpanelversion.Text = _panelType + " Panel Version: V" + (parts.Length > 1 ? parts[1] : "Unavailable");
        }

        private string sendcmd(string cmd, string parameters = "")
        {
            string strcmd = cmd;
            if (!string.IsNullOrEmpty(parameters))
                strcmd += kpipedelim + parameters;
            try { return Task.Run(() => sendserver(strcmd)).Result; }
            catch { return "Error"; }
        }

        private async Task<string> sendserver(string message)
        {
            using (NamedPipeClientStream pipe = new NamedPipeClientStream(".", kpipenamesend, PipeDirection.InOut))
            {
                pipe.Connect(3000);
                pipe.ReadMode = PipeTransmissionMode.Message;
                byte[] ba = Encoding.Default.GetBytes(message);
                pipe.Write(ba, 0, ba.Length);
                var result = await Task.Run(() => readmessage(pipe));
                return Encoding.Default.GetString(result);
            }
        }

        private static byte[] readmessage(PipeStream pipe)
        {
            byte[] buffer = new byte[1024];
            using (var ms = new MemoryStream())
            {
                do { var rb = pipe.Read(buffer, 0, buffer.Length); ms.Write(buffer, 0, rb); }
                while (!pipe.IsMessageComplete);
                return ms.ToArray();
            }
        }

        private void btok_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btcopy_Click(object sender, EventArgs e)
        {
            string info = lblversion.Text + Environment.NewLine
                        + lblpanelversion.Text + Environment.NewLine
                        + lblclientversion.Text;
            Clipboard.SetText(info);
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            this.lbcopyright.Text = "Copyright Drax Ltd 2007 - " + System.DateTime.Now.Year.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
