using System;
using System.Windows.Forms;

namespace Eagle_Monitor
{
    static class Program
    {
        /// <summary>
        /// EntryPoint
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartForm());
        }
    }
}
