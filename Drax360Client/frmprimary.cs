
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

        public frmprimary()
        {
            InitializeComponent();
            lblversion.Text = "V" + Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>().Version;

            startpipeservers();
            startpipereturn();

        }

        private void startpipeservers()
        {
            pipeserverreturn = new NamedPipeServerStream(kpipenamereturn, PipeDirection.InOut, 254, PipeTransmissionMode.Message);
            Console.WriteLine("Pipe Server Return is Started (" + kpipenamereturn + ")");
        }

        private async void startpipereturn()
        {
            while (pipeserverreturn != null)
            {
                await pipeserverreturn.WaitForConnectionAsync();

                //receive message from client
                var messagebytes = readpipemessage(pipeserverreturn);
                string strresponse = Encoding.UTF8.GetString(messagebytes);
                Console.WriteLine("Message received from server return: " + strresponse);
                string strret = handlepiperesponse(strresponse);
                //prepare some response
                byte[] response = Encoding.UTF8.GetBytes(strret);

                //send response to a server
                pipeserverreturn.Write(response, 0, response.Length);
                pipeserverreturn.Disconnect();

            }

        }
        private string handlepiperesponse(string strresponse)
        {
            // this is where we need to implement the bring to the front messages
            string[] parts = strresponse.Split(kpipedelim);
            if (parts.Length > 2) return "ERROR";
            string message = parts[0];

            // brings current window to front
            this.BringToFront();
            string ret = "OK";


            return ret;
        }

        private void stoppipeserver()
        {
            if (pipeserverreturn != null)
            {

                Console.WriteLine("Pipe Server Return is stopping...");
                pipeserverreturn.Close();
                pipeserverreturn.Dispose();
                pipeserverreturn = null;
            }


        }
        private static byte[] readpipemessage(PipeStream pipe)
        {
            if (!pipe.IsConnected) return new byte[0];
            byte[] buffer = new byte[2048];
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

            frmAbout about = new frmAbout();
            about.ShowDialog();
            about.Dispose();
            about = null;
        }

        

        private void frmprimary_FormClosing(object sender, FormClosingEventArgs e)
        {
            stoppipeserver();
        }
    }
}
