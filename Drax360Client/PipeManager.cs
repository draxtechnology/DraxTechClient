using System;
using System.IO;
using System.IO.Pipes;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DraxClient
{
    public static class PipeManager
    {
        private static Form? _mainForm;
        private const string PipeNameReturn = "DraxTechnologyPipeReturn";
        private const char PipeDelimiter = '|';

        private static NamedPipeServerStream? _pipeServer;
        private static frmTestBox? _testBox;
        private static frmAbout? _aboutBox;
        private static frmSetup? _setup;

        // Built once, reused on every server creation to avoid repeated SID/descriptor work.
        private static readonly PipeSecurity _pipeSecurity = BuildPipeSecurity();

        private static PipeSecurity BuildPipeSecurity()
        {
            var sec = new PipeSecurity();
            sec.AddAccessRule(new PipeAccessRule(
                new SecurityIdentifier(WellKnownSidType.WorldSid, null),
                PipeAccessRights.FullControl,
                AccessControlType.Allow));
            return sec;
        }

        private static NamedPipeServerStream CreatePipeServer()
        {
            try
            {
                var pipe = NamedPipeServerStreamAcl.Create(
                    PipeNameReturn, PipeDirection.InOut, 254,
                    PipeTransmissionMode.Message, PipeOptions.Asynchronous,
                    0, 0, _pipeSecurity);
                Console.WriteLine($"[{PipeNameReturn}] ready");
                return pipe;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ACL pipe failed ({ex.GetType().Name}) — using default ACL");
                var pipe = new NamedPipeServerStream(
                    PipeNameReturn, PipeDirection.InOut, 254,
                    PipeTransmissionMode.Message, PipeOptions.Asynchronous);
                Console.WriteLine($"[{PipeNameReturn}] ready (default ACL)");
                return pipe;
            }
        }

        public static void Start()
        {
            Console.WriteLine($"Pipe Server Return is Starting ({PipeNameReturn})");
            Task.Run(() => ListenLoop());
        }

        private static async Task ListenLoop()
        {
            // Pre-create the first server on a background thread so any slow security
            // descriptor work happens during startup rather than in the hot path.
            var pipeServer = await Task.Run(CreatePipeServer);

            while (true)
            {
                try
                {
                    await pipeServer.WaitForConnectionAsync();

                    // Kick off next server creation IN PARALLEL with handling this connection.
                    // By the time the service sends the next command the new server is ready.
                    var nextServerTask = Task.Run(CreatePipeServer);

                    _ = Task.Run(() => HandleClient(pipeServer));

                    pipeServer = await nextServerTask;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Pipe loop error ({ex.GetType().Name}): {ex.Message}");
                    await Task.Delay(500);
                    try { pipeServer = await Task.Run(CreatePipeServer); }
                    catch { await Task.Delay(2000); }
                }
            }
        }

        private static void HandleClient(NamedPipeServerStream pipeServer)
        {
            try
            {
                byte[] messageBytes = ReadPipeMessage(pipeServer);
                string strResponse = Encoding.UTF8.GetString(messageBytes);
                Console.WriteLine("Message received: " + strResponse);

                string result = HandlePipeCommand(strResponse);

                byte[] response = Encoding.UTF8.GetBytes(result);
                pipeServer.Write(response, 0, response.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Pipe error: {ex.Message}");
            }
            finally
            {
                pipeServer?.Disconnect();
                pipeServer?.Dispose();
            }
        }

        private static string HandlePipeCommand(string command)
        {
            switch (command)
            {
                case string s when s.Contains("|NWM:TBSHOW"):
                    ShowTestBox();
                    return "OK";

                case string s when s.Contains("|NWM:TBHIDE"):
                    HideTestBox();
                    return "OK";

                case string s when s.Contains("|NWM:SHOWABOUT"):
                    ShowAbout();
                    return "OK";

                case string s when s.Contains("|GEN:SETUPSHOW"):
                    Setup();
                    return "OK";

                case string s when s.Contains("|NWM:CLOSEALLWINDOWS"):
                    // HideTestBox();
                    return "OK";

                default:
                    // Optionally handle unknown responses
                    return "UNKNOWN_COMMAND";
            }
        }

        public static void ShowTestBox()
        {
            if (_mainForm == null) return;
            _mainForm.BeginInvoke((MethodInvoker)(() =>
            {
                if (_testBox == null || _testBox.IsDisposed)
                {
                    _testBox = new frmTestBox();
                    _testBox.StartPosition = FormStartPosition.CenterParent;
                    _testBox.StartPosition = FormStartPosition.CenterScreen;
                    _testBox.FormClosed += (s, e) => _testBox = null;
                    _testBox.Show();
                    _testBox.BringToFront();
                    _testBox.Activate();
                    _testBox.Focus();
                }
                else
                {
                    if (!_testBox.Visible)
                        _testBox.Show();
                    _testBox.BringToFront();
                    _testBox.Activate();
                    _testBox.Focus();
                }
            }));
        }

        public static void ShowAbout()
        {
            if (_mainForm == null) return;
            _mainForm.BeginInvoke((MethodInvoker)(() =>
            {
                if (_aboutBox == null || _aboutBox.IsDisposed)
                {
                    _aboutBox = new frmAbout();
                    _aboutBox.StartPosition = FormStartPosition.CenterParent;
                    _aboutBox.StartPosition = FormStartPosition.CenterScreen;
                    _aboutBox.FormClosed += (s, e) => _aboutBox = null;
                    _aboutBox.Show();
                    _aboutBox.BringToFront();
                    _aboutBox.Activate();
                    _aboutBox.Focus();
                }
                else
                {
                    if (!_aboutBox.Visible)
                        _aboutBox.Show();
                    _aboutBox.BringToFront();
                    _aboutBox.Activate();
                    _aboutBox.Focus();

                }
            }));
        }

        public static void Setup()
        {
            if (_mainForm == null) return;
            _mainForm.BeginInvoke((MethodInvoker)(() =>
            {
                if (_setup == null || _setup.IsDisposed)
                {
                    _setup = new frmSetup();
                    _setup.StartPosition = FormStartPosition.CenterParent;
                    _setup.StartPosition = FormStartPosition.CenterScreen;
                    _setup.FormClosed += (s, e) => _setup = null;
                    _setup.Show();
                    _setup.BringToFront();
                    _setup.Activate();
                    _setup.Focus();
                }
                else
                {
                    if (!_setup.Visible)
                        _setup.Show();
                    _setup.BringToFront();
                    _setup.Activate();
                    _setup.Focus();

                }
            }));
        }

        private static void HideTestBox()
        {
            if (_mainForm == null) return;
            if (_mainForm.InvokeRequired)
            {
                _mainForm.BeginInvoke(new Action(HideTestBox));
                return;
            }

            if (_testBox != null && !_testBox.IsDisposed)
            {
                _testBox.Hide();
            }
        }

        private static byte[] ReadPipeMessage(PipeStream pipe)
        {
            if (!pipe.IsConnected) return new byte[0];

            byte[] buffer = new byte[2048];
            using (var ms = new MemoryStream())
            {
                do
                {
                    int readBytes = pipe.Read(buffer, 0, buffer.Length);
                    ms.Write(buffer, 0, readBytes);
                }
                while (!pipe.IsMessageComplete);

                return ms.ToArray();
            }
        }

        public static void Stop()
        {
            if (_pipeServer != null)
            {
                Console.WriteLine("Pipe Server is stopping...");
                _pipeServer.Close();
                _pipeServer.Dispose();
                _pipeServer = null;
            }
        }
        public static void SetMainForm(Form mainForm)
        {
            _mainForm = mainForm;
        }
    }
}
