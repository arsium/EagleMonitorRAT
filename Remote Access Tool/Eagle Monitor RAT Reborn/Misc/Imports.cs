using System;
using System.Runtime.InteropServices;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.Misc
{
    internal class Imports
    {
        private const String ntdll = "ntdll.dll";
        private const String user32 = "user32.dll";

        [DllImport(ntdll, SetLastError = true)]
        internal static extern uint NtTerminateProcess(IntPtr hProcess, int errorStatus);

        [DllImport(user32)]
        internal extern static bool ReleaseCapture();
        [DllImport(user32)]
        internal extern static IntPtr SendMessage(IntPtr a, int msg, int wParam, int lParam);
    }
}
