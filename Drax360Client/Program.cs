using System.Runtime.CompilerServices;
using System.Threading;

namespace DraxClient
{
    internal static class Program
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        // Single-instance guard. Named mutex acquired on Main; second launch
        // sees createdNew=false and bails out silently. The mutex is disposed
        // when Application.Run returns at app exit, releasing it for the
        // next launch.
        private const string SingleInstanceMutexName = "DraxClientSingleInstance";

        public class HiddenAppContext : ApplicationContext
        {
            private bool debug = true; // Set to true for debugging purposes
            private frmprimary _mainForm;
            public HiddenAppContext()
            {
                //var _mainForm = new frmprimary();
                var _mainForm = new frmSetup();

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
                else
                { 
                }
            }
        }
        [STAThread]
        static void Main()
        {
            using var mutex = new Mutex(initiallyOwned: true, SingleInstanceMutexName, out bool createdNew);
            if (!createdNew)
            {
                // Another DraxClient is already running on this session — bail
                // silently. The existing one is the one the user wants.
                return;
            }

            AllocConsole();  //Display console window for debugging purposes
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new HiddenAppContext());
        }
    }
}