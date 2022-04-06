using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows.Forms;
using static PacketLib.Utils.Native;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Utils
{
    public class Miscellaneous
    {
        internal static string GPath = Application.StartupPath;

        public static string Check64Bit()
        {
            if (Environment.Is64BitProcess)
            {
                return "64";
            }
            else
            {
                return "32";
            }
        }
        public static string Privilege()
        {
            var ID = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(ID);
            if (principal.IsInRole(WindowsBuiltInRole.Administrator))
            {
                return "Admin";
            }
            else
            {
                return "User";
            }
        }

        public static void CleanMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            EmptyWorkingSet(Process.GetCurrentProcess().Handle);
        }
    }
}
