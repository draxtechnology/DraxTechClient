using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Drax360Client
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            this.TopMost = true;
            InitializeComponent();
            lblversion.Text = "V" + Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
        }

        private void btok_Click(object sender, EventArgs e)
        {
            this.Close();
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
