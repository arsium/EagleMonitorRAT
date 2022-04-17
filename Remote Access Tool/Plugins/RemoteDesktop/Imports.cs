using System;
using System.Drawing;
using System.Runtime.InteropServices;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal class Imports
    {
        #region "user32"
        private const string user32 = "user32.dll";
        [DllImport(user32)]
        internal static extern bool GetCursorInfo(ref CURSORINFOHELPER pci);
        [DllImport(user32)]
        internal static extern bool GetCursorPos(out Point lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        internal struct CURSORINFOHELPER
        {
            public Int32 cbSize;
            public Int32 flags;
            public IntPtr hCursor;
            public Point ptScreenPos;
        }
        #endregion
    }
}
