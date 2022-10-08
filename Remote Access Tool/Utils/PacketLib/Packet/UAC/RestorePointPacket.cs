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
            this.packetType = PacketType.UAC_GET_RESTORE_POINT;
        }

        public RestorePointPacket(List<RestorePoint> restorePoints, string baseIp, string HWID) : base()
        {

            packetType = PacketType.UAC_GET_RESTORE_POINT;
            this.baseIp = baseIp;
            this.HWID = HWID;

            this.restorePoints = restorePoints;
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public PacketState packetState { get; set; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }
        public int packetSize { get; set; }

        public List<RestorePoint> restorePoints { get; set; }

    }
}
