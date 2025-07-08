using System;
using System.Windows.Forms;
using ROAutomationToolkit.Forms;
using ROAutomationToolkit.Services;

namespace ROAutomationToolkit
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var keySender = new KeySenderService();
            var profileService = new ProfileService();

            Application.Run(new MainForm(keySender, profileService));
        }
    }
}