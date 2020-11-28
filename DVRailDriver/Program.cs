using System;
using System.Windows.Forms;

namespace DVRailDriver
{
    static class Program
    {
        public static readonly string AppPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
