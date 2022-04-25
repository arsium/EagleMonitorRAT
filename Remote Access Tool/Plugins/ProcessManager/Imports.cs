using System;
using System.Runtime.InteropServices;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal static class Imports
    {
        [DllImport("ntdll.dll", SetLastError = true)]
        internal static extern uint NtTerminateProcess(IntPtr ProcHandle, int ErrorStatus = 0);

        [DllImport("ntdll.dll", SetLastError = true)]
        internal extern static uint ZwTerminateProcess(IntPtr ProcHandle, int ErrorStatus = 0);

        [DllImport("user32.dll")]
        internal extern static bool EndTask(IntPtr hWnd, bool fShutDown, bool fForce);

        [DllImport("user32.dll")]
        internal extern static IntPtr SendMessage(IntPtr hWnd, WM_Message Msg, SC_Message wParam, IntPtr lParam);

        public enum WM_Message : uint
        {
            WM_DESTROY = 0x2,
            WM_CLOSE = 0x10,
            WM_SYSCOMMAND = 0x112,
            WM_SETTEXT = 0x000C,
            WM_APPCOMMAND = 0x0319
        }
        public enum SC_Message : uint
        {
            NONE = 0,
            SC_CLOSE = 0xF060,
            SC_MAXIMIZE = 0xF030,
            SC_MINIMIZE = 0xF020,
            SC_RESTORE = 0xF120
        }

        [DllImport("ntdll.dll")]
        public extern static uint NtSuspendProcess(IntPtr ProcHandle);
        [DllImport("ntdll.dll")]
        public extern static uint ZwSuspendProcess(IntPtr ProcHandle);

        [DllImport("ntdll.dll")]
        public extern static uint NtResumeProcess(IntPtr ProcHandle);
        [DllImport("ntdll.dll")]
        public extern static uint ZwResumeProcess(IntPtr ProcHandle);
    }
}
