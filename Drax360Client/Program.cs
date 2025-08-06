using System.Runtime.CompilerServices;

namespace Drax360Client
{
    internal static class Program
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        public class HiddenAppContext : ApplicationContext
        {
            public HiddenAppContext()
            {
                var mainForm = new frmprimary();

                // Force the creation of the form's handle
                var handle = mainForm.Handle;

                PipeManager.SetMainForm(mainForm);
                PipeManager.Start();
            }
        }
        [STAThread]
        static void Main()
        {
            // AllocConsole();  Display console window for debugging purposes
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new HiddenAppContext());
        }
    }
}