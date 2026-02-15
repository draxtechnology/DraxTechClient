using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DraxClient.Panels.RSM
{
    public partial class frmtempdiscovery : Form
    {
        public Device OurDevice { get; }
        public frmtempdiscovery(Device ourdevice)
        {
            InitializeComponent();
            OurDevice = ourdevice;

            

            tbname.Text = OurDevice.Name;
            tbip.Text = OurDevice.IP;
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            OurDevice.Name = tbname.Text;
            OurDevice.IP = tbip.Text;
            DialogResult = DialogResult.OK;
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void tbname_TextChanged(object sender, EventArgs e)
        {
            button_handler();
        }
        private void button_handler()
        {
            btnok.Enabled = !string.IsNullOrWhiteSpace(tbname.Text);
        }

        private void frmtempdiscovery_Load(object sender, EventArgs e)
        {
            button_handler();
        }
    }
}
