
using DraxClient.Panels.Email;
using DraxClient.Panels.RSM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TcpListenerApp;
using static DraxClient.frmTestBox;


namespace DraxClient
{
    public partial class frmSetup : Form
    {
        const string kpipenamesend = "Drax360PipeSend";
        const char kpipedelim = '|';


        #region private variables

        #endregion
        public frmSetup()
        {
            InitializeComponent();

            for (int i = 1; i <= 10; i++)
            {
                cbComport.Items.Add(new ComboBoxItem
                {
                    Text = $"Comm {i}",
                    Value = i.ToString()
                });
            }

            string paneltype = sendcmd("GetPanelType");
            //paneltype = "ADVANCED";    // For testing purposes

            string result = sendcmd($"SETTINGSGET|PANEL1,COMMPORT");
            // find the item with matching Value
            foreach (ComboBoxItem item in cbComport.Items)
            {
                if (item.Value == result)
                {
                    cbComport.SelectedItem = item;
                    break;
                }
            }

            this.tbDataBits.Text = sendcmd($"SETTINGSGET|SETUP,DataBits");
            this.tbBaudRate.Text = sendcmd($"SETTINGSGET|SETUP,BaudRate");

            switch (paneltype)
            {
                case "ADVANCED":
                    this.tbOffset.Text = sendcmd("SETTINGSGET|SETUP,AMX1OFFSET");
                    break;

                case "EMAIL":
                    load_email_groups();
                    break;

                case "GENT":
                    this.tbOffset.Text = sendcmd("SETTINGSGET|SETUP,GIAMX1OFFSET");
                    break;

                case "MOLREYZX":
                    this.tbOffset.Text = sendcmd("SETTINGSGET|PANEL1,AMX1OFFSET");
                    break;

                case "MORLEYMAX":
                    this.tbOffset.Text = sendcmd("SETTINGSGET|PANEL1,AMX1OFFSET");
                    break;

                case "NOTIFIER":
                    this.tbOffset.Text = sendcmd("SETTINGSGET|PANEL1,AMX1OFFSET");
                    break;

                case "PEARL":
                    this.tbOffset.Text = sendcmd("SETTINGSGET|PANEL1,AMX1OFFSET");
                    break;

                case "RSM":
                    load_rsm();
                    break;

                case "SYNCRO":
                    this.tbOffset.Text = sendcmd("SETTINGSGET|PANEL1,AMX1OFFSET");
                    break;
                case "TAKTIS":
                    this.tbOffset.Text = sendcmd("SETTINGSGET|SETUP,AMX1OFFSET");
                    break;
            }

            this.lbStatus.Text = sendcmd($"GETCOMMPORTSTATUS|COM3");
            if (this.lbStatus.Text.ToLower() == "connected")
            {
                this.lbStatus.ForeColor = Color.Green;
            }
            else
            {
                this.lbStatus.ForeColor = Color.Red;
            }

            cbParity.Items.Add(new ComboBoxItem { Text = "Even", Value = "Even" });
            cbParity.Items.Add(new ComboBoxItem { Text = "Odd", Value = "Odd" });
            cbParity.Items.Add(new ComboBoxItem { Text = "None", Value = "None" });
            result = sendcmd($"SETTINGSGET|SETUP,PARITY");
            // find the item with matching Value
            foreach (ComboBoxItem item in cbParity.Items)
            {
                if (item.Value == result)
                {
                    cbParity.SelectedItem = item;
                    break;
                }
            }

            result = "0";
            switch (paneltype)
            {
                case "ADVANCED":
                    result = sendcmd("SETTINGSGET|MAIN,DesignTime");
                    break;

                case "GENT":
                    result = sendcmd("SETTINGSGET|MAIN,DataLogging");
                    break;
            }

            if (result == "1" || result == "True")
            {
                this.debug.Checked = true;
            }
            else
            {
                this.debug.Checked = false;
            }

            if (paneltype == "ADVANCED")
            {
                cboHB1.Minimum = 0;
                cboHB1.Maximum = 99;
                cboHB1.Value = 1;
                cboHB1.Increment = 1;

                result = sendcmd($"SETTINGSGET|SETUP,RefreshZonesStart");
                if (result == "1")
                {
                    this.chkRefreshZonesStart.Checked = true;
                }
                else
                {
                    this.chkRefreshZonesStart.Checked = false;
                }

                result = sendcmd($"SETTINGSGET|SETUP,RefreshZonesConfig");
                if (result == "1")
                {
                    this.chkRefreshZonesConfig.Checked = true;
                }
                else
                {
                    this.chkRefreshZonesConfig.Checked = false;
                }

                result = sendcmd($"SETTINGSGET|SETUP,DefaultZoneText");
                if (result == "1")
                {
                    this.chkDefaultZone.Checked = true;
                }
                else
                {
                    this.chkDefaultZone.Checked = false;
                }

                result = sendcmd($"SETTINGSGET|SETUP,IgnoreNullZone");
                if (result == "1")
                {
                    this.chkIgnoreNullZone.Checked = true;
                }
                else
                {
                    this.chkIgnoreNullZone.Checked = false;
                }

                result = sendcmd($"SETTINGSGET|SETUP,IgnoreNullZone");
                if (result == "1")
                {
                    this.chkIgnoreNullZone.Checked = true;
                }
                else
                {
                    this.chkIgnoreNullZone.Checked = false;
                }

                result = sendcmd($"SETTINGSGET|SETUP,gbUseSubAddressOffset");
                if (chkSubAddressOffset.Checked)
                {
                    this.SubAddressOffsetNumber.Visible = true;
                    this.lblSubAddressOffsetNumber.Visible = true;
                }
                else
                {
                    this.SubAddressOffsetNumber.Visible = false;
                }

                result = sendcmd($"SETTINGSGET|SETUP,UseClassicIsolations");
                if (result == "1")
                {
                    this.chkClassicIsolations.Checked = true;
                }
                else
                {
                    this.chkClassicIsolations.Checked = false;
                }
            }
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
        private void btApply_Click(object sender, EventArgs e)
        {
            sendcmd("SETTINGSSAVE");
            sendcmd("ServiceRestart");
        }
        private void btok_Click(object sender, EventArgs e)
        {
            sendcmd("SETTINGSSAVE");
            sendcmd("ServiceRestart");
            this.Close();
        }

        private void btcancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbComport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbComport.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedValue = selectedItem.Value;

                sendcmd($"SETTINGSSET|PANEL1,COMMPORT,{selectedValue}");
            }
        }

        private void debug_CheckedChanged(object sender, EventArgs e)
        {
            if (debug.Checked)
            {
                sendcmd($"SETTINGSSET|SETUP,DATALOGGING,1");
            }
            else
            {
                sendcmd($"SETTINGSSET|SETUP,DATALOGGING,0");
            }
        }

        private void tbBaudRate_TextChanged(object sender, EventArgs e)
        {
            sendcmd($"SETTINGSSET|SETUP,DATABITS," + this.tbBaudRate.Text);
        }

        private void tbDataBits_TextChanged(object sender, EventArgs e)
        {
            sendcmd($"SETTINGSSET|SETUP,DATABITS," + this.tbDataBits.Text);
        }

        private void cbParity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbParity.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedValue = selectedItem.Value;

                sendcmd($"SETTINGSSET|SETUP,PARITY,{selectedValue}");
            }
        }

        private void tbOffset_TextChanged(object sender, EventArgs e)
        {
            sendcmd($"SETTINGSSET|SETUP,GIAMX1OFFSET," + this.tbOffset.Text);
        }

        private void frmSetup_Load(object sender, EventArgs e)
        {
            string result = sendcmd($"SETTINGSRELOAD");
        }
        private void load_email_groups()
        {

            this.tabControl1.SelectedTab = tpemail;



        }

        private void load_rsm()
        {

            this.tabControl1.SelectedTab = tprsm;



        }

        private void chkSubAddressOffset_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSubAddressOffset.Checked)
            {
                this.SubAddressOffsetNumber.Visible = true;
                this.lblSubAddressOffsetNumber.Visible = true;
            }
            else
            {
                this.SubAddressOffsetNumber.Visible = false;
                this.lblSubAddressOffsetNumber.Visible = false;
            }
        }

        private void bteditemailgroups_Click(object sender, EventArgs e)
        {
            frmemaigroups frmemail = new frmemaigroups();
            frmemail.ShowDialog();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void btdiscovery_Click(object sender, EventArgs e)
        {
            frmdiscovery fdiscover = new frmdiscovery();
            fdiscover.ShowDialog();
        }

        private void frmListen_Click(object sender, EventArgs e)
        {
            TcpListenerForm frmListen = new TcpListenerForm();
            frmListen.ShowDialog();
        }

        private void btdevices_Click(object sender, EventArgs e)
        {
            frmdevices fdev = new frmdevices();
            fdev.ShowDialog();
        }
    }
}
