using System;
using System.Runtime.InteropServices;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace NativeAPI
{
    public static class Process
    {
        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern uint NtTerminateProcess(IntPtr hProcess, int errorStatus);
        [DllImport("ntdll.dll")]
        public extern static uint ZwTerminateProcess(IntPtr ProcHandle, int ErrorStatus = 0);
        [DllImport("kernel32.dll")]
        public extern static bool TerminateProcess(IntPtr Handle, uint uExitCoed);
        [DllImport("ntdll.dll")]
        public extern static uint NtSuspendProcess(IntPtr ProcHandle);
        [DllImport("ntdll.dll")]
        public extern static uint ZwSuspendProcess(IntPtr ProcHandle);
        [DllImport("ntdll.dll")]
        public extern static uint NtResumeProcess(IntPtr ProcessHandle);
        [DllImport("ntdll.dll")]
        public extern static uint ZwResumeProcess(IntPtr ProcessHandle);
        [DllImport("user32.dll")]
        public extern static bool EndTask(IntPtr hWnd, bool fShutDown, bool fForce);
    }
}
