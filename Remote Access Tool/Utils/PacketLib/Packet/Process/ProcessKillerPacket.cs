using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class ProcessKillerPacket : IPacket
    {

        //server
        public ProcessKillerPacket(int processId, string processName, int rowIndex) : base()
        {
  
            this.PacketType = PacketType.PM_KILL_PROCESS;    
            
            this.processId = processId;
            this.processName = processName;
            this.rowIndex = rowIndex;
        }

        //client
        public ProcessKillerPacket(bool killed, int processId, string processName, int rowIndex, string baseIp, string HWID) : base() 
        {

            this.PacketType = PacketType.PM_KILL_PROCESS;
            this.BaseIp = baseIp;
            this.HWID = HWID;       
            
            this.killed = killed;
            this.processId = processId;
            this.processName = processName;
            this.rowIndex = rowIndex;
        }

        public string HWID { get; set; }
        public string BaseIp { get; set; }
        public byte[] Plugin { get; set; }
        public PacketType PacketType { get; }
        public PacketState PacketState { get; set; }
        public string Status { get; set; }
        public string DatePacketStatus { get; set; }
        public int PacketSize { get; set; }

        public int rowIndex { get; set; }
        public string processName { get; set; }
        public int processId { get; set; }
        public bool killed { get; set; }
    }
}
