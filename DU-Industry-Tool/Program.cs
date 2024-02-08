using System;
using System.Diagnostics;
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
            if (e.Exception != null)
            {
                Debug.WriteLine(e.Exception.Message);
                if (e.Exception.StackTrace != null)
                {
                    Debug.WriteLine(e.Exception.StackTrace);
                    //MessageBox.Show(e.Exception?.Message + "\r\n" + e.Exception.StackTrace,
                    //    "UI Thread Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (!(e.ExceptionObject is Exception ex)) return;
            Debug.WriteLine(ex.Message);
            if (ex.StackTrace != null)
            {
                Debug.WriteLine(ex.StackTrace);
            }
            //MessageBox.Show(((Exception)e.ExceptionObject).Message, "Non-UI Thread Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
