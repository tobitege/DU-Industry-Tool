using System;
using System.Threading;
using System.Windows.Forms;

namespace DU_Industry_Tool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.SetHighDpiMode(HighDpiMode.SystemAware);

            // Exception event handlers
            Application.ThreadException += new ThreadExceptionEventHandler(App_UIThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.Run(new MainForm());
        }

        private static void App_UIThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "UI Thread Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(((Exception)e.ExceptionObject).Message, "Non-UI Thread Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
