using System.Collections.Generic;
using System.IO;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal class GetDisks
    {
        internal static List<string> GetAllDisks() 
        {
            List<string> disksList = new List<string>();
            DriveInfo[] D = DriveInfo.GetDrives();
            foreach (DriveInfo S in D)
            {
                disksList.Add(S.Name);
            }

            return disksList;
        }
    }
}
