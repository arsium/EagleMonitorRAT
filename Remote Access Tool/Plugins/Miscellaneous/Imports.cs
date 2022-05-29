using System;
using System.Runtime.InteropServices;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
|| Inspiration : inferno from LimerBoy : https://github.com/arsium/Inferno ||
*/

namespace Plugin
{
    internal static class Imports
    {
		#region "ntdll"
		private const string ntdll = "ntdll.dll";
		[DllImport(ntdll)]
		internal static extern uint NtTerminateProcess(IntPtr hProcess, int errorStatus);
		#endregion
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

        internal const int DMDO_DEFAULT = 0;
        internal const int DMDO_90 = 1;
        internal const int DMDO_180 = 2;
        internal const int DMDO_270 = 3;

        internal const int ENUM_CURRENT_SETTINGS = -1;


		internal enum Orientations
		{
			DEGREES_CW_0 = 0,
			DEGREES_CW_90 = 3,
			DEGREES_CW_180 = 2,
			DEGREES_CW_270 = 1
		}

		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
		internal struct DEVMODE
		{
			internal const int CCHDEVICENAME = 32;
			internal const int CCHFORMNAME = 32;

			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
			[FieldOffset(0)]
			internal string dmDeviceName;
			[FieldOffset(32)]
			internal Int16 dmSpecVersion;
			[FieldOffset(34)]
			internal Int16 dmDriverVersion;
			[FieldOffset(36)]
			internal Int16 dmSize;
			[FieldOffset(38)]
			internal Int16 dmDriverExtra;
			[FieldOffset(40)]
			internal DM dmFields;

			[FieldOffset(44)]
			private Int16 dmOrientation;
			[FieldOffset(46)]
			private Int16 dmPaperSize;
			[FieldOffset(48)]
			private Int16 dmPaperLength;
			[FieldOffset(50)]
			private Int16 dmPaperWidth;
			[FieldOffset(52)]
			private Int16 dmScale;
			[FieldOffset(54)]
			private Int16 dmCopies;
			[FieldOffset(56)]
			private Int16 dmDefaultSource;
			[FieldOffset(58)]
			private Int16 dmPrintQuality;

			[FieldOffset(44)]
			internal POINTL dmPosition;
			[FieldOffset(52)]
			internal Int32 dmDisplayOrientation;
			[FieldOffset(56)]
			internal Int32 dmDisplayFixedOutput;

			[FieldOffset(60)]
			internal short dmColor;
			[FieldOffset(62)]
			internal short dmDuplex;
			[FieldOffset(64)]
			internal short dmYResolution;
			[FieldOffset(66)]
			internal short dmTTOption;
			[FieldOffset(68)]
			internal short dmCollate;
			[FieldOffset(72)]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
			internal string dmFormName;
			[FieldOffset(102)]
			internal Int16 dmLogPixels;
			[FieldOffset(104)]
			internal Int32 dmBitsPerPel;
			[FieldOffset(108)]
			internal Int32 dmPelsWidth;
			[FieldOffset(112)]
			internal Int32 dmPelsHeight;
			[FieldOffset(116)]
			internal Int32 dmDisplayFlags;
			[FieldOffset(116)]
			internal Int32 dmNup;
			[FieldOffset(120)]
			internal Int32 dmDisplayFrequency;
		}

		// See: https://msdn.microsoft.com/en-us/library/windows/desktop/dd183569(v=vs.85).aspx
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
		internal struct DISPLAY_DEVICE
		{
			[MarshalAs(UnmanagedType.U4)]
			internal int cb;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			internal string DeviceName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			internal string DeviceString;
			[MarshalAs(UnmanagedType.U4)]
			internal DisplayDeviceStateFlags StateFlags;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			internal string DeviceID;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			internal string DeviceKey;
		}

		// See: https://msdn.microsoft.com/de-de/library/windows/desktop/dd162807(v=vs.85).aspx
		[StructLayout(LayoutKind.Sequential)]
		internal struct POINTL
		{
			private long x;
			private long y;
		}

		internal enum DISP_CHANGE : int
		{
			Successful = 0,
			Restart = 1,
			Failed = -1,
			BadMode = -2,
			NotUpdated = -3,
			BadFlags = -4,
			BadParam = -5,
			BadDualView = -6
		}

		// http://www.pinvoke.net/default.aspx/Enums/DisplayDeviceStateFlags.html
		[Flags()]
		internal enum DisplayDeviceStateFlags : int
		{
			/// <summary>The device is part of the desktop.</summary>
			AttachedToDesktop = 0x1,
			MultiDriver = 0x2,
			/// <summary>The device is part of the desktop.</summary>
			PrimaryDevice = 0x4,
			/// <summary>Represents a pseudo device used to mirror application drawing for remoting or other purposes.</summary>
			MirroringDriver = 0x8,
			/// <summary>The device is VGA compatible.</summary>
			VGACompatible = 0x10,
			/// <summary>The device is removable; it cannot be the primary display.</summary>
			Removable = 0x20,
			/// <summary>The device has more display modes than its output devices support.</summary>
			ModesPruned = 0x8000000,
			Remote = 0x4000000,
			Disconnect = 0x2000000
		}

		// http://www.pinvoke.net/default.aspx/user32/ChangeDisplaySettingsFlags.html
		[Flags()]
		internal enum DisplaySettingsFlags : int
		{
			CDS_NONE = 0,
			CDS_UPDATEREGISTRY = 0x1,
			CDS_TEST = 0x2,
			CDS_FULLSCREEN = 0x4,
			CDS_GLOBAL = 0x8,
			CDS_SET_PRIMARY = 0x10,
			CDS_VIDEOPARAMETERS = 0x20,
			CDS_ENABLE_UNSAFE_MODES = 0x100,
			CDS_DISABLE_UNSAFE_MODES = 0x200,
			CDS_RESET = 0x40000000,
			CDS_RESET_EX = 0x20000000,
			CDS_NORESET = 0x10000000
		}

		[Flags()]
		internal enum DM : int
		{
			Orientation = 0x1,
			PaperSize = 0x2,
			PaperLength = 0x4,
			PaperWidth = 0x8,
			Scale = 0x10,
			Position = 0x20,
			NUP = 0x40,
			DisplayOrientation = 0x80,
			Copies = 0x100,
			DefaultSource = 0x200,
			PrintQuality = 0x400,
			Color = 0x800,
			Duplex = 0x1000,
			YResolution = 0x2000,
			TTOption = 0x4000,
			Collate = 0x8000,
			FormName = 0x10000,
			LogPixels = 0x20000,
			BitsPerPixel = 0x40000,
			PelsWidth = 0x80000,
			PelsHeight = 0x100000,
			DisplayFlags = 0x200000,
			DisplayFrequency = 0x400000,
			ICMMethod = 0x800000,
			ICMIntent = 0x1000000,
			MediaType = 0x2000000,
			DitherType = 0x4000000,
			PanningWidth = 0x8000000,
			PanningHeight = 0x10000000,
			DisplayFixedOutput = 0x20000000
		}

		
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

        [DllImport(user32)]
        internal extern static DISP_CHANGE ChangeDisplaySettingsEx(string lpszDeviceName, ref DEVMODE lpDevMode, IntPtr hwnd, DisplaySettingsFlags dwflags, IntPtr lParam);

        [DllImport(user32)]
        internal extern static bool EnumDisplayDevices(string lpDevice, uint iDevNum, ref DISPLAY_DEVICE lpDisplayDevice, uint dwFlags);

        [DllImport(user32)]
        internal extern static int EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);

        #endregion
    }
}
