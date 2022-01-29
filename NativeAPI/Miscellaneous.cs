using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace NativeAPI
{
    public class Miscellaneous
    {
        public const int EWX_LOGOFF = 0x0;
        public const int EWX_SHUTDOWN = 0x1;
        public const int EWX_REBOOT = 0x2;
        public const int EWX_FORCE = 0x4;
        public const int EWX_POWEROFF = 0x8;
        public const int EWX_FORCEIFHUNG = 0x10;

        public const int SHTDN_REASON_MINOR_BLUESCREEN = 0xF;
        public const int SHTDN_REASON_MAJOR_SOFTWARE = 0x30000;

        public const uint SPI_SETDESKWALLPAPER = 0x14;
        public const uint SPIF_UPDATEINIFILE = 0x01;
        public const uint SPIF_SENDWININICHANGE = 0x02;

        public const int APPCOMMAND_VOLUME_MUTE = 0x80000;
        public const int APPCOMMAND_VOLUME_DOWN = 0x90000;
        public const int APPCOMMAND_VOLUME_UP = 0xA0000;
        public const int WM_APPCOMMAND = 0x319;


        [DllImport("user32.dll", EntryPoint = "ShowWindow", ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);
        [DllImport("user32.dll", EntryPoint = "FindWindowEx", ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);
        [DllImport("user32.dll", EntryPoint = "FindWindow", ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public extern static bool ShowSystemCursor(bool viewable);
        [DllImport("user32.dll")]
        public extern static bool SetDeskWallPaper(string path);
        [DllImport("user32.dll")]
        public static extern uint SystemParametersInfo(uint action, uint uParam, string vParam, uint winIni);



        [DllImport("PowrProf.dll", ExactSpelling = true, SetLastError = true)]
        public extern static bool SetSuspendState(bool bHibernate, bool bForce, bool bWakeupEventsDisabled);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public extern static bool ExitWindowsEx(int flg, int rea);
        [DllImport("ntdll.dll")]
        public extern static uint RtlAdjustPrivilege(int Privilege, bool bEnablePrivilege, bool IsThreadPrivilege, out bool PreviousValue);
        [DllImport("ntdll.dll")]
        public extern static uint NtRaiseHardError(uint ErrorStatus, uint NumberOfParameters, uint UnicodeStringParameterMask, IntPtr Parameters, uint ValidResponseOption, out uint Response);



        [DllImport("user32.dll")]
        public extern static IntPtr SendMessage(IntPtr hWnd, WM_Message Msg, SC_Message wParam, string lParam);
        [DllImport("user32.dll")]
        public extern static IntPtr SendMessage(IntPtr hWnd, WM_Message Msg, IntPtr wParam, int lParam);
        [DllImport("user32.dll")]
        public extern static IntPtr SendMessage(IntPtr hWnd, WM_Message Msg, SC_Message wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public extern static IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public extern static IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public extern static IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, int lParam);


        [DllImport("user32.dll")]
        public extern static IntPtr PostMessage(IntPtr hWnd, WM_Message Msg, SC_Message wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public extern static IntPtr PostMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        public enum WM_Message : uint
        {
            WM_DESTROY = 0x2,
            WM_CLOSE = 0x10,
            WM_SYSCOMMAND = 0x112,
            WM_SETTEXT = 0x000C,
            WM_APPCOMMAND = 0x0319
    }
        public enum SC_Message : uint
        {
            NONE = 0,
            SC_CLOSE = 0xF060,
            SC_MAXIMIZE = 0xF030,
            SC_MINIMIZE = 0xF020,
            SC_RESTORE = 0xF120
        }

        [DllImport("DwmApi.dll")]
        public extern static int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS pMarInset);

        [DllImport("user32.dll")]
        public extern static int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
     
        [StructLayout(LayoutKind.Sequential)]
        public partial struct MARGINS
        {
            public int Left;
            public int Right;
            public int Top;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct AccentPolicy
        {
            public AccentState AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
        }

        public enum WindowCompositionAttribute
        {
            WCA_UNDEFINED = 0,
            WCA_NCRENDERING_ENABLED = 1,
            WCA_NCRENDERING_POLICY = 2,
            WCA_TRANSITIONS_FORCEDISABLED = 3,
            WCA_ALLOW_NCPAINT = 4,
            WCA_CAPTION_BUTTON_BOUNDS = 5,
            WCA_NONCLIENT_RTL_LAYOUT = 6,
            WCA_FORCE_ICONIC_REPRESENTATION = 7,
            WCA_EXTENDED_FRAME_BOUNDS = 8,
            WCA_HAS_ICONIC_BITMAP = 9,
            WCA_THEME_ATTRIBUTES = 10,
            WCA_NCRENDERING_EXILED = 11,
            WCA_NCADORNMENTINFO = 12,
            WCA_EXCLUDED_FROM_LIVEPREVIEW = 13,
            WCA_VIDEO_OVERLAY_ACTIVE = 14,
            WCA_FORCE_ACTIVEWINDOW_APPEARANCE = 15,
            WCA_DISALLOW_PEEK = 16,
            WCA_CLOAK = 17,
            WCA_CLOAKED = 18,
            WCA_ACCENT_POLICY = 19,
            WCA_FREEZE_REPRESENTATION = 20,
            WCA_EVER_UNCLOAKED = 21,
            WCA_VISUAL_OWNER = 22,
            WCA_HOLOGRAPHIC = 23,
            WCA_EXCLUDED_FROM_DDA = 24,
            WCA_PASSIVEUPDATEMODE = 25,
            WCA_LAST = 26
        }

        public enum AccentState
        {
            ACCENT_DISABLED = 0,
            ACCENT_ENABLE_GRADIENT = 1,
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
            ACCENT_ENABLE_BLURBEHIND = 3,
            ACCENT_ENABLE_ACRYLICBLURBEHIND = 4,
            ACCENT_ENABLE_HOSTBACKDROP = 5,
            ACCENT_INVALID_STATE = 6
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public extern static bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, Int32 X, Int32 Y, Int32 cx, Int32 cy, Int32 uFlags);

        public const int HWND_BOTTOM = 1;
        public const int HWND_NOTOPMOST = -2;
        public const int HWND_TOP = 0;
        public const int HWND_TOPMOST = -1;


        public const Int32 SWP_NOSIZE = 0x1;
        public const Int32 SWP_NOMOVE = 0x2;
        public const Int32 SWP_NOREDRAW = 0x8;
        public const Int32 SWP_DEFERERASE = 0x2000;



        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public extern static IntPtr SetWindowsHookEx(int id, Hook.KeyBoardHooking.LowLevelKeyboardProc callback, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public extern static IntPtr SetWindowsHookEx(int id, Hook.MouseHooking.LowLevelMouseProc callback, IntPtr hMod, uint dwThreadId);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public extern static IntPtr GetModuleHandle(string name);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public extern static bool UnhookWindowsHookEx(IntPtr hook);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public extern static IntPtr CallNextHookEx(IntPtr hook, int nCode, IntPtr wp, IntPtr lp);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public extern static short GetAsyncKeyState(Keys key);

    }
}
