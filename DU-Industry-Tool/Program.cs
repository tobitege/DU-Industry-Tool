using System;
using System.Windows.Forms;
using Bluegrams.Application;

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
            PortableJsonSettingsProvider.SettingsFileName = "DU-Industry-Tool.usersettings.json";
            PortableSettingsProviderBase.SettingsDirectory = Application.StartupPath;
            PortableJsonSettingsProvider.ApplyProvider(Properties.Settings.Default);
            Application.Run(new MainForm());
        }
    }
}
