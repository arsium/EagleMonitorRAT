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
        #region "kernel32"
        private const string kernel32 = "kernel32.dll";
        [DllImport(kernel32)]
        internal static extern bool MoveFile([MarshalAsAttribute(UnmanagedType.LPStr)] string lpExistingFileName, [MarshalAsAttribute(UnmanagedType.LPStr)] string lpNewFileName);
        #endregion
        #region "shell32"
        //https://docs.microsoft.com/en-us/windows/win32/shell/knownfolderid
        private const string shell32 = "shell32.dll";
        [DllImport(shell32)]
        internal static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken, out IntPtr pszPath);
        internal const string FOLDERID_Downloads = "{374DE290-123F-4565-9164-39C4925E467B}";
        internal const string FOLDERID_Profile = "{5E6C858F-0E22-4760-9AFE-EA3317B67173}";
        #endregion
    }
}
