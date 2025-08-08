using System.Runtime.CompilerServices;

namespace Drax360Client
{
    internal static class Program
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        public class HiddenAppContext : ApplicationContext
        {
            private bool debug = true; // Set to true for debugging purposes
            private frmprimary _mainForm;
            public HiddenAppContext()
            {
                var _mainForm = new frmprimary();

                // Force the creation of the form's handle
                var handle = _mainForm.Handle;

                PipeManager.SetMainForm(_mainForm);
                PipeManager.Start();

                if (debug)
                {
                    _mainForm.Show();
                    _mainForm.WindowState = FormWindowState.Normal;
                    _mainForm.BringToFront();
                    _mainForm.Activate();
                }
            }
        }
        [STAThread]
        static void Main()
        {
            AllocConsole();  //Display console window for debugging purposes
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new HiddenAppContext());
        }
    }
}