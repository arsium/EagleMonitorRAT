using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace HookHardware
{
    internal class Imports
    {
        #region "user32"
        private const string user32 = "user32.dll";
        [DllImport(user32)]
        internal static extern bool UnhookWindowsHookEx(IntPtr hook);

        [DllImport(user32)]
        internal static extern IntPtr CallNextHookEx(IntPtr hook, int nCode, IntPtr wp, IntPtr lp);

        [DllImport(user32)]
        internal static extern IntPtr SetWindowsHookEx(int id, Keyboard.LowLevelKeyboard callback, IntPtr hMod, uint dwThreadId);
        [DllImport(user32)]
        internal static extern IntPtr SetWindowsHookEx(int id, Mouse.LowLevelMouse callback, IntPtr hMod, uint dwThreadId);
        #endregion

        #region "Keyboard"
        internal const short WM_INPUT =             0x00ff;
        internal const short WM_KEYDOWN =           0x0100;
        internal const short WM_KEYUP =             0x0101;
        internal const short WM_CHAR =              0x0102;
        internal const short WM_DEADCHAR =          0x0103;
        internal const short WM_SYSKEYDOWN =        0x0104;
        internal const short WM_SYSKEYUP =          0x0105;
        internal const short WM_SYSCHAR =           0x0106;
        internal const short WM_SYSDEADCHAR = 0x0107;

        [StructLayout(LayoutKind.Sequential)]
        internal struct KBDLLHOOKSTRUCT
        {
            public Keys key;
            public int scanCode;
            public int flags;
            public int time;
            public IntPtr extra;
        }
        #endregion

        #region"Mouse"
        internal const short WM_MOUSEMOVE =         0x200;
        internal const short WM_LBUTTONDOWN =       0x201;
        internal const short WM_LBUTTONUP =         0x202;

        internal const short WM_LEFT_CLICK =        0x203;//????

        internal const short WM_RBUTTONDOWN =       0x204;
        internal const short WM_RBUTTONUP =         0x205;
        internal const short WM_RBUTTONDBLCLK =     0x206;
        internal const short WM_MOUSEWHEEL =        0x020A;
        internal const short WM_MOUSEHWHEEL =       0x020E;
        [StructLayout(LayoutKind.Sequential)]
        internal struct MSLLHOOKSTRUCT
        {
            public Point pt;
            public int mouseData;
            public int flags;
            public int time;
            public IntPtr extra;
        }
        #endregion
    }
}
