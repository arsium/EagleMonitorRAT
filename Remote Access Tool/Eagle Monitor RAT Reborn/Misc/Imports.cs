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

        internal const int WM_VSCROLL = 277;
        internal static readonly IntPtr SB_PAGEBOTTOM = new IntPtr(7);

        [DllImport(user32, CharSet = CharSet.Auto)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
    }
}
