using System;
using static HookHardware.Imports;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace HookHardware
{
    public class Keyboard
    {
        protected static IntPtr ptrHook;
        internal delegate IntPtr LowLevelKeyboard(int nCode, IntPtr wParam, IntPtr lParam);
        internal static LowLevelKeyboard callback;

		public void AbortHook()
		{
			try
			{
				//MessageBox.Show(ptrHook.ToString("x"));
				if (ptrHook != IntPtr.Zero)
					UnhookWindowsHookEx(ptrHook);
			}
			catch { }
		}
		public void HookKeyboard()
		{
			callback = new LowLevelKeyboard(BlockKey);
			ptrHook = SetWindowsHookEx(13, callback, System.Diagnostics.Process.GetCurrentProcess().MainModule.BaseAddress, 0);
			//MessageBox.Show(ptrHook.ToString("x"));
		}

		protected static IntPtr BlockKey(int nCode, IntPtr wp, IntPtr lp)
		{
			try
			{
				if (nCode >= 0)
				{

					if (wp == (IntPtr)WM_KEYDOWN)
					{
						return wp;
					}

					if (wp == (IntPtr)0x101)
					{
						return wp;
					}

					if (wp == (IntPtr)0x105)
					{
						return wp;
					}

					if (wp == (IntPtr)WM_SYSKEYDOWN)
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
