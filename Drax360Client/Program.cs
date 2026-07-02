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
        static void Main(string[] args)
        {
            using var mutex = new Mutex(initiallyOwned: true, SingleInstanceMutexName, out bool createdNew);
            if (!createdNew)
            {
                // Another DraxClient is already running on this session — bail
                // silently. The existing one is the one the user wants.
                return;
            }

            // Console is opt-in for diagnostics only — operators shouldn't get a
            // stray console window they could accidentally close (which would kill
            // the client). Enable with "DraxClient.exe --console" (or --debug).
            if (args.Any(a => a.Equals("--console", StringComparison.OrdinalIgnoreCase)
                           || a.Equals("--debug", StringComparison.OrdinalIgnoreCase)))
            {
                AllocConsole();  // diagnostics only; surfaces the PipeManager console output
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new HiddenAppContext());
        }
    }
}