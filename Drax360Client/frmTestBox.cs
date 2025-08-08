using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drax360Client
{
    public partial class frmTestBox : Form
    {
        #region constants
        const string kpipename = "Drax360Pipe";
        const string kpipenamesend = "Drax360PipeSend";
        const string kpipenamereturn = "Drax360PipeReturn";
        const char kpipedelim = '|';

        private int _port;
        private string _address;
        private TcpClient _tcpClient;
        private bool _connected = true;
        private NetworkStream _stream;

        #endregion
        public frmTestBox()
        {
            this.TopMost = true;
            InitializeComponent();
            Console.WriteLine("frmTestBox initialized.");

            cbType.Items.Add(new ComboBoxItem { Text = "0 - Fire - Alarm", Value = "0" });
            cbType.Items.Add(new ComboBoxItem { Text = "2 - Pre - Alarm", Value = "2" });
            cbType.Items.Add(new ComboBoxItem { Text = "4 - Disablement", Value = "4" });
            cbType.Items.Add(new ComboBoxItem { Text = "6 - Test Mode", Value = "6" });
            cbType.Items.Add(new ComboBoxItem { Text = "7 - Tech Alarm", Value = "7" });
            cbType.Items.Add(new ComboBoxItem { Text = "8 - Device Fault", Value = "8" });
            cbType.Items.Add(new ComboBoxItem { Text = "10 - Maintenance", Value = "10" });
            cbType.Items.Add(new ComboBoxItem { Text = "15 - Status", Value = "15" });
            cbType.SelectedIndex = 0; // Select first item

            tbNode.Minimum = 1;
            tbNode.Maximum = 255;
            tbNode.Value = 1;
            tbNode.Increment = 1;

            tbLoop.Minimum = 1;
            tbLoop.Maximum = 255;
            tbLoop.Value = 1;
            tbLoop.Increment = 1;

            tbDevice.Minimum = 1;
            tbDevice.Maximum = 255;
            tbDevice.Value = 1;
            tbDevice.Increment = 1;
        }
        public class ComboBoxItem
        {
            public string Text { get; set; }   // Displayed in the ComboBox
            public string Value { get; set; }  // The hidden value

            public override string ToString()
            {
                return Text; // This makes ComboBox display the text
            }
        }

        private void btOn_Click(object sender, EventArgs e)
        {
            ComboBoxItem selectedItem = this.cbType.SelectedItem as ComboBoxItem;
            sendcmd("Test Box", selectedItem.Value + "," + this.tbNode.Text + "," + this.tbLoop.Text + "," + this.tbDevice.Text);
        }
        private string sendcmd(string cmd, string parameters = "")
        {
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

        private void btReset_Click(object sender, EventArgs e)
        {
            ComboBoxItem selectedItem = this.cbType.SelectedItem as ComboBoxItem;
            sendcmd("Test Box Reset", selectedItem.Value + "," + this.tbNode.Text + "," + this.tbLoop.Text + "," + this.tbDevice.Text);
        }

        private void frmTestBox_Load(object sender, EventArgs e)
        {
            Console.WriteLine("frmTestBox Load event fired.");
        }

        private void tbNode_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
