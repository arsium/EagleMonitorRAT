using EagleMonitor.Forms;
using System;
using System.Threading;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor
{
    internal static class Program
    {

        private static void MemCleaner()
        {
            Thread.Sleep(30000);
            PacketLib.Utils.Miscellaneous.CleanMemory();
        }
        private static void EndMemoryCleaner(IAsyncResult ar)
        {
            memoryCleaner.EndInvoke(ar);
            memoryCleaner.BeginInvoke(new AsyncCallback(EndMemoryCleaner), null);
        }

        private delegate void MemoryCleaner();
        private static MemoryCleaner memoryCleaner;
        [STAThread]
        static void Main()
        {
            AppContext.SetSwitch("Switch.UseLegacyAccessibilityFeatures.3", true);  //remove auto header selected with full row selected in datagridview
            AppContext.SetSwitch("Switch.UseLegacyAccessibilityFeatures.2", true);  //remove auto header selected with full row selected in datagridview
            AppContext.SetSwitch("Switch.UseLegacyAccessibilityFeatures", true);    //remove auto header selected with full row selected in datagridview
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            memoryCleaner = new MemoryCleaner(MemCleaner);
            memoryCleaner.BeginInvoke(new AsyncCallback(EndMemoryCleaner), null);
            mainForm = new Main();
            //logForm = new LogForm();
            massForm = new MassForm();
            //Program.logForm.Show();
            Application.Run(mainForm);

        }

        internal static MassForm massForm { get; set; }
        internal static Main mainForm { get; set; }
        //internal static LogForm logForm { get; set; }
    }
}
