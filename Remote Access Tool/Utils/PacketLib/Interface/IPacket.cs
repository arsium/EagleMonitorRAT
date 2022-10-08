using PacketLib.Packet;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib
{
    public interface IPacket
    {
        PacketType packetType { get; }
        PacketState packetState { get; set; }
        byte[] plugin { get; set; }
        string baseIp { get; set; }
        string HWID { get; set; }
        string status { get; set; }
        string datePacketStatus { get; set; }
        int packetSize { get; set; }
    }
}
