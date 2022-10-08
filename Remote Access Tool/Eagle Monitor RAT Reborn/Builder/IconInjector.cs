using System;
using System.Windows.Forms;
using Vestris.ResourceLib;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.Builder
{
    internal static class IconInjector
    {
        internal static void SetIcon(string iconPath, string exePath)
        {
            try
            {
                IconFile iconFile = new IconFile(iconPath);
                IconDirectoryResource iconDirectoryResource = new IconDirectoryResource(iconFile);
                iconDirectoryResource.SaveTo(exePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
