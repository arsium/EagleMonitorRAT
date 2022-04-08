using System;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

using System.Runtime.InteropServices;

namespace Eagle_Monitor_Tasks_Configurator
{
    internal class Helpers
    {
        internal static string SplitPath(string P)
        {
            string[] spl = P.Split('\\');
            return spl[spl.Length - 1];
        } 
        [DllImport("ntdll.dll")]
        internal static extern uint NtTerminateProcess(IntPtr hProcess, int errorStatus);

        internal static string GPath = Application.StartupPath;

    }
}
