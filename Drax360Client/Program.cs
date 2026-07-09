using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace DraxClient
{
    internal static class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        private static extern nint GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern nint GetSystemMenu(nint hWnd, bool bRevert);

        [DllImport("user32.dll")]
        private static extern bool EnableMenuItem(nint hMenu, uint uIDEnableItem, uint uEnable);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AttachConsole(uint dwProcessId);

        private const uint ATTACH_PARENT_PROCESS = 0xFFFFFFFF;
        private const uint SC_CLOSE = 0xF060;
        private const uint MF_BYCOMMAND = 0x0;
        private const uint MF_GRAYED = 0x1;

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
                _mainForm.Show();
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
                // Another DraxClient is already running on this session — bail. Log it
                // (to file, and to the launching terminal if there is one) so this stops
                // being a silent no-op: a second launch, e.g. from a command prompt,
                // previously just returned with nothing shown anywhere, which made it
                // look like the app had failed rather than deliberately deferred to the
                // instance already running.
                LogStartupEvent(
                    "Startup aborted: another DraxClient is already running on this "
                    + "session (single-instance mutex already held).",
                    echoToParentConsole: true);
                return;
            }

            LogStartupEvent("Startup: single-instance mutex acquired; this instance is starting.");

            // Diagnostics console. On by default so the PipeManager / pipe traffic is
            // visible without having to relaunch with a flag (the single-instance guard
            // above means a second flagged launch would just bail). Suppress it for a
            // clean operator deploy with "--no-console" or DRAXCLIENT_NOCONSOLE=1.
            bool suppressConsole =
                args.Any(a => a.Equals("--no-console", StringComparison.OrdinalIgnoreCase))
                || string.Equals(Environment.GetEnvironmentVariable("DRAXCLIENT_NOCONSOLE"), "1",
                                 StringComparison.Ordinal);
            if (!suppressConsole)
            {
                // EnableConsole();
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new HiddenAppContext());
        }

        // Allocate a diagnostics console and make it actually usable.
        private static void EnableConsole()
        {
            // AllocConsole returns false if we already have a console (e.g. launched
            // from a terminal) — in that case stdout is already wired up, so leave it.
            if (!AllocConsole())
            {
                return;
            }

            // A WinExe (Windows subsystem) starts with no valid standard handles, and
            // .NET may have cached a null Console.Out before the console existed. Rebind
            // stdout/stderr to the freshly-allocated console so Console.WriteLine — the
            // whole point of enabling this — actually surfaces. AutoFlush so lines appear
            // immediately rather than sitting in a buffer.
            var stdout = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };
            Console.SetOut(stdout);
            var stderr = new StreamWriter(Console.OpenStandardError()) { AutoFlush = true };
            Console.SetError(stderr);
            Console.Title = "DraxClient diagnostics";

            // Closing an allocated console sends CTRL_CLOSE_EVENT and terminates the
            // client. Grey out the console's close box so it can't be shut accidentally;
            // the client is meant to be exited from its own UI / tray, not this window.
            nint consoleWindow = GetConsoleWindow();
            if (consoleWindow != nint.Zero)
            {
                nint sysMenu = GetSystemMenu(consoleWindow, false);
                if (sysMenu != nint.Zero)
                {
                    EnableMenuItem(sysMenu, SC_CLOSE, MF_BYCOMMAND | MF_GRAYED);
                }
            }
        }

        // Best-effort startup diagnostics. This runs before any console or UI exists, so
        // it appends to a file under the shared ProgramData folder (C:\ProgramData\
        // DraxTechnology\draxclient-startup.log). The single-instance bail also echoes to
        // the terminal the client was launched from, if any — so starting a second copy
        // from a command prompt reports why it exited instead of silently returning.
        private static void LogStartupEvent(string message, bool echoToParentConsole = false)
        {
            string line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}  PID {Environment.ProcessId}  {message}";

            try
            {
                Directory.CreateDirectory(Paths.StorageDirectory);
                File.AppendAllText(Paths.GetFile("draxclient-startup.log"), line + Environment.NewLine);
            }
            catch
            {
                // Diagnostics must never break (or block) startup.
            }

            if (!echoToParentConsole)
            {
                return;
            }

            try
            {
                // Attach to the console we were launched from (if there is one) purely to
                // surface this line. Done ONLY on the bail path: the instance that goes on
                // to run calls EnableConsole()/AllocConsole() instead, and attaching here
                // would consume the console slot and defeat it.
                if (AttachConsole(ATTACH_PARENT_PROCESS))
                {
                    Console.Error.WriteLine(line);
                }
            }
            catch
            {
            }
        }
    }
}
