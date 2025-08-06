using System.Runtime.CompilerServices;

namespace Drax360Client
{
    internal static class Program
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AllocConsole();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mainForm = new frmprimary();
            PipeManager.SetMainForm(mainForm);

            Application.Run(mainForm);

        }
    }
}