using System;
using System.Windows.Forms;

namespace Drax360Client
{
    public class HiddenAppContext : ApplicationContext
    {
        public HiddenAppContext()
        {
            // Create your primary logic form (but don't show it)
            var mainForm = new frmprimary();

            // Set and start pipe services
            PipeManager.SetMainForm(mainForm);
            PipeManager.Start();

            // Optional: hook into Application exit if needed
            // Application.ApplicationExit += (s, e) => { /* Cleanup */ };
        }
    }
}