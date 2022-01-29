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


        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWow64Process([In] IntPtr hProcess, [Out] out bool wow64Process);

        public enum Platform
        {
            X86,
            X64,
            IA64,
            Unknown
        }

        public const ushort PROCESSOR_ARCHITECTURE_INTEL = 0;
        public const ushort PROCESSOR_ARCHITECTURE_IA64 = 6;
        public const ushort PROCESSOR_ARCHITECTURE_AMD64 = 9;
        public const ushort PROCESSOR_ARCHITECTURE_UNKNOWN = 0xFFFF;

        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEM_INFO
        {
            public ushort wProcessorArchitecture;
            public ushort wReserved;
            public uint dwPageSize;
            public IntPtr lpMinimumApplicationAddress;
            public IntPtr lpMaximumApplicationAddress;
            public UIntPtr dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public ushort wProcessorLevel;
            public ushort wProcessorRevision;
        };

        [DllImport("kernel32.dll")]
        public static extern void GetNativeSystemInfo(ref SYSTEM_INFO lpSystemInfo);

    }
}
