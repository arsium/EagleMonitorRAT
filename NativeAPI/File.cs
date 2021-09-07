using System.Runtime.InteropServices;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace NativeAPI
{
    public static class File
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteFile(string lpFileName);
        [DllImport("kernel32.dll", SetLastError = true)]
        public extern static bool MoveFile([MarshalAsAttribute(UnmanagedType.LPStr)] string lpExistingFileName, [MarshalAsAttribute(UnmanagedType.LPStr)] string lpNewFileName);
    }
}
