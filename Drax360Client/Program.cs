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
            private frmprimary _mainForm;
            public HiddenAppContext()
            {
                // frmprimary is the persistent hidden owner — ShowInTaskbar=false
                // and a SetVisibleCore override that keeps the handle alive after
                // the first hide. Using frmSetup here was the cause of Mike's
                // "Pipe error: Invoke or BeginInvoke cannot be called on a control
                // until the window handle has been created": closing the Setup
                // form disposed it, then the next service push (NWM:TBSHOW etc.)
                // tried to marshal onto a handle-less form.
                var _mainForm = new frmprimary();
                var handle = _mainForm.Handle;
                //_mainForm.Show();
                PipeManager.SetMainForm(_mainForm);
                PipeManager.Start();
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