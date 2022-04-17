using System;
using System.Runtime.InteropServices;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Persistence
{
    internal class Imports
    {
        [DllImport("ntdll.dll")]
        internal static extern uint NtTerminateProcess(IntPtr hProcess, int erStatus);

        [DllImport("kernel32.dll")]
        internal static extern bool SetFileAttributes(string lpFileName, FileAttributes dwFileAttributes);

        [Flags]
        internal enum FileAttributes : uint
        {
            Readonly = 0x00000001,
            Hidden = 0x00000002,
            System = 0x00000004,
        }
    }
}
