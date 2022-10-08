using Eagle_Monitor_RAT_Reborn.Misc;
using System;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            AppContext.SetSwitch("Switch.UseLegacyAccessibilityFeatures.3", true);  //remove auto header selected with full row selected in datagridview
            AppContext.SetSwitch("Switch.UseLegacyAccessibilityFeatures.2", true);  //remove auto header selected with full row selected in datagridview
            AppContext.SetSwitch("Switch.UseLegacyAccessibilityFeatures", true);    //remove auto header selected with full row selected in datagridview

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            mainForm = new MainForm();
            Application.Run(mainForm);
        }

        internal static MainForm mainForm { get; set; }
        internal static Settings settings { get; set; }
    }
}
