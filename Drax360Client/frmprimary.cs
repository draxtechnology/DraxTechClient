
using System.IO.Pipes;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Drax360Client
{
    public partial class frmprimary : Form
    {
        #region constants
        const string kpipenamesend = "Drax360PipeSend";
        const string kpipenamereturn = "Drax360PipeReturn";
        const char kpipedelim = '|';

        #endregion

        NamedPipeServerStream pipeserverreturn;

        private bool panelconnected = false;
        private bool allowShow = false;

        public frmprimary()
        {
            InitializeComponent();
            //this.Load += frmprimary_Load;
            //this.Shown += Frmprimary_Shown;  
            lblversion.Text = "V" + Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
            this.ShowInTaskbar = false;
            PipeManager.SetMainForm(this);
        }

        private void Frmprimary_Shown(object sender, EventArgs e)
        {
            //PipeManager.Start(); // Start pipe manager here, after form is fully created and shown
            Console.Write("Frmprimary_Shown");
            Console.Out.Flush();
        }

        protected override void SetVisibleCore(bool value)
        {
            // Always create handle by calling base once with true
            if (!IsHandleCreated)
            {
                base.SetVisibleCore(true); // create handle but show window once
                this.Hide(); // immediately hide the form
            }
            else
            {
                base.SetVisibleCore(value);
            }
        }
        private void btevacuate_Click(object sender, EventArgs e)
        {
            sendcmd("Evacuate");
        }
        private void btAlert_Click(object sender, EventArgs e)
        {
            sendcmd("Alert");
        }
        private void btReset_Click(object sender, EventArgs e)
        {
            sendcmd("Reset");
        }
        private void btSilence_Click(object sender, EventArgs e)
        {
            sendcmd("Silence");
        }
        private void btDisableDevice_Click(object sender, EventArgs e)
        {
            sendcmd("DisableDevice", this.tbNode + "," + this.tbLoop + "," + this.tbZone + "," + this.tbIP);
        }
        private void btEnableDevice_Click(object sender, EventArgs e)
        {
            sendcmd("EnableDevice", this.tbNode + "," + this.tbLoop + "," + this.tbZone + "," + this.tbIP);
        }
        private void btDisableZone_Click(object sender, EventArgs e)
        {
            sendcmd("DisableZone", this.tbNode + "," + this.tbLoop + "," + this.tbZone + "," + this.tbIP);
        }
        private void btEnableZone_Click(object sender, EventArgs e)
        {
            sendcmd("EnableZone", this.tbNode + "," + this.tbLoop + "," + this.tbZone + "," + this.tbIP);
        }
        private void btgetpanel_Click(object sender, EventArgs e)
        {
            string result = sendcmd("GetPanelType");
            if (!string.IsNullOrEmpty(result) && result != "Error")
                MessageBox.Show(result);
        }
        private string sendcmd(string cmd, string parameters = "")
        {
            updatestatus("Sending");
            string strcmd = cmd;
            if (!string.IsNullOrEmpty(parameters))
            {

                strcmd += kpipedelim + parameters;
            }

            string result = "";

            try
            {
                result = Task.Run(() => sendserver(strcmd)).Result;
            }
            catch (Exception ex)
            {
                result = "Error: " + ex;
            }
            updatestatus(result);
            return result;
        }

        private async Task<string> sendserver(string message)
        {
            using (NamedPipeClientStream pipe = new NamedPipeClientStream(".", kpipenamesend, PipeDirection.InOut))
            {
                pipe.Connect(5000);
                pipe.ReadMode = PipeTransmissionMode.Message;

                byte[] ba = Encoding.Default.GetBytes(message);
                pipe.Write(ba, 0, ba.Length);

                var result = await Task.Run(() =>
                {
                    return readmessage(pipe);
                });

                string strresponse = Encoding.Default.GetString(result);

                Console.WriteLine("Response received from Send server: " + strresponse);

                return strresponse;
            }
        }

        private static byte[] readmessage(PipeStream pipe)
        {
            if (!pipe.IsConnected) return new byte[0];

            byte[] buffer = new byte[1024];
            using (var ms = new MemoryStream())
            {
                do
                {
                    var readBytes = pipe.Read(buffer, 0, buffer.Length);
                    ms.Write(buffer, 0, readBytes);
                }
                while (!pipe.IsMessageComplete);

                return ms.ToArray();
            }
        }
        private void updatestatus(string msg)
        {
            if (!string.IsNullOrEmpty(msg))
                lblstatus.Text = msg;
        }

        private void frmprimary_Load(object sender, EventArgs e)
        {
            PipeManager.SetMainForm(this);
            this.Hide(); // hide the window but keep it alive
            try
            {
                string result = sendcmd("GetPanelType");
                panelconnected = !result.StartsWith("Error");
                if (!panelconnected)
                {
                    this.lblstatus.Text = "DISCONNECTED";
                }

                if (result == "MORLEYMAX")
                {
                    this.btevacuate.Visible = false;
                }
            }
            catch { }
            button_handler();
        }

        private void button_handler()
        {
            btAlert.Enabled = panelconnected;
            btDisableDevice.Enabled = panelconnected;
            btDisableZone.Enabled = panelconnected;
            btEnableDevice.Enabled = panelconnected;
            btEnableZone.Enabled = panelconnected;
            btevacuate.Enabled = panelconnected;
            btReset.Enabled = panelconnected;
            btSilence.Enabled = panelconnected;
        }


        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAbout? about = new frmAbout();
            about.ShowDialog();
            about.Dispose();
            about = null;
        }

        private void frmprimary_FormClosing(object sender, FormClosingEventArgs e)
        {
            PipeManager.Stop();
        }

       

        private void btnrestartservice_Click(object sender, EventArgs e)
        {
            sendcmd("ServiceRestart");
        }
    }
}
