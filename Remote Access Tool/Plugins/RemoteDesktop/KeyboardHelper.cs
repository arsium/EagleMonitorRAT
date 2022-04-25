using System.Runtime.InteropServices;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
|| Inspiration : QuasarRAT  ||
*/

namespace Plugin
{
    internal class KeyboardHelper
    {
        internal static void KeyPress(byte key, bool keyDown)
        {
            Imports.INPUT[] inputs = {
                new Imports.INPUT
                {
                    type = Imports.INPUT_KEYBOARD,
                    u = new Imports.InputUnion
                    {
                        ki = new Imports.KEYBDINPUT
                        {
                            wVk = key,
                            wScan = 0,
                            dwFlags = keyDown ? Imports.KEYEVENTF_KEYDOWN : Imports.KEYEVENTF_KEYUP,
                            dwExtraInfo = Imports.GetMessageExtraInfo()
                        }
                    }
                }
            };
            Imports.SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Imports.INPUT)));
        }
    }
}
