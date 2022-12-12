using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class ResumeProcessPacket : IPacket
    {

        //server
        public ResumeProcessPacket(int processId, string processName, int rowIndex) : base()
        {   
            this.PacketType = PacketType.PM_RESUME_PROCESS; 
            
            this.processId = processId;
            this.processName = processName;
            this.rowIndex = rowIndex;
        }

        //client
        public ResumeProcessPacket(bool resumed, int processId, string processName, int rowIndex, string baseIp, string HWID) : base()
        {
            this.PacketType = PacketType.PM_RESUME_PROCESS;
            this.BaseIp = baseIp;
            this.HWID = HWID;     
            
            this.resumed = resumed;
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
        public bool resumed { get; set; }
    }
}
