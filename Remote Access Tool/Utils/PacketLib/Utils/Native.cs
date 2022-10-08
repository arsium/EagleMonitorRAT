using System;
using System.Runtime.InteropServices;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Utils
{
    internal class Native
    {
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);

        [DllImport("psapi")]
        public extern static bool EmptyWorkingSet(IntPtr hfandle);

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKilobytes);


        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteFile(string lpFileName);
        [DllImport("kernel32.dll", SetLastError = true)]
        public extern static bool MoveFile([MarshalAsAttribute(UnmanagedType.LPStr)] string lpExistingFileName, [MarshalAsAttribute(UnmanagedType.LPStr)] string lpNewFileName);
    }
}
