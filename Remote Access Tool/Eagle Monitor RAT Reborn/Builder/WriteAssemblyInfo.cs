using System;
using Vestris.ResourceLib;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.Builder
{
    internal static class WriteAssemblyInfo
    {
        internal static void WriteAssemblyInformation(string path, string fileVer, string prodVer, string prodName, string desc, string compName, string copyright, string trademarks, string orgiName)
        {
            try
            {
                VersionResource versionResource = new VersionResource();
                versionResource.LoadFrom(path);

                versionResource.FileVersion = fileVer;
                versionResource.ProductVersion = prodVer;
                versionResource.Language = 0;

                StringFileInfo stringFileInfo = (StringFileInfo)versionResource["StringFileInfo"];
                stringFileInfo["ProductName"] = prodName;
                stringFileInfo["FileDescription"] = desc;
                stringFileInfo["CompanyName"] = compName;
                stringFileInfo["LegalCopyright"] = copyright;
                stringFileInfo["LegalTrademarks"] = trademarks;
                stringFileInfo["Assembly Version"] = versionResource.ProductVersion;
                stringFileInfo["InternalName"] = orgiName;
                stringFileInfo["OriginalFilename"] = orgiName;
                stringFileInfo["ProductVersion"] = versionResource.ProductVersion;
                stringFileInfo["FileVersion"] = versionResource.FileVersion;

                versionResource.SaveTo(path);
            }
            catch (Exception)
            { }
        }
    }
}
