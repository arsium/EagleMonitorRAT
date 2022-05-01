using System;
using System.Drawing;
using System.Runtime.InteropServices;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
|| Inspiration : QuasarRAT  ||
*/

namespace Plugin
{
    internal class Imports
    {
        #region "user32"
        private const string user32 = "user32.dll";
        [DllImport(user32)]
        internal static extern bool DrawIcon(IntPtr hDC, int X, int Y, IntPtr hIcon);
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



        internal const int INPUT_MOUSE = 0;
        internal const int INPUT_KEYBOARD = 1;

        internal const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        internal const uint MOUSEEVENTF_LEFTUP = 0x0004;
        internal const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        internal const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        internal const uint MOUSEEVENTF_WHEEL = 0x0800;
        internal const uint KEYEVENTF_KEYDOWN = 0x0000;
        internal const uint KEYEVENTF_KEYUP = 0x0002;

        internal const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        internal const uint MOUSEEVENTF_MIDDLEUP = 0x0040;

        [DllImport(user32)]
        internal static extern IntPtr GetMessageExtraInfo();

        [DllImport(user32)]
        internal static extern uint SendInput(uint nInputs,[MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs, int cbSize);

        [StructLayout(LayoutKind.Sequential)]
        internal struct INPUT
        {
            internal uint type;
            internal InputUnion u;
            internal static int Size => Marshal.SizeOf(typeof(INPUT));
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct InputUnion
        {
            [FieldOffset(0)]
            internal MOUSEINPUT mi;
            [FieldOffset(0)]
            internal KEYBDINPUT ki;
            [FieldOffset(0)]
            internal HARDWAREINPUT hi;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct MOUSEINPUT
        {
            internal int dx;
            internal int dy;
            internal int mouseData;
            internal uint dwFlags;
            internal uint time;
            internal IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct KEYBDINPUT
        {
            internal ushort wVk;
            internal ushort wScan;
            internal uint dwFlags;
            internal uint time;
            internal IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct HARDWAREINPUT
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }

        [DllImport(user32)]
        internal static extern bool SetCursorPos(int x, int y);

        #endregion
    }
}
