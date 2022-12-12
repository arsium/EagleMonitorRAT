using PacketLib.Packet;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib
{
    public interface IPacket
    {
        PacketType PacketType { get; }
        PacketState PacketState { get; set; }
        byte[] Plugin { get; set; }
        string BaseIp { get; set; }
        string HWID { get; set; }
        string Status { get; set; }
        string DatePacketStatus { get; set; }
        int PacketSize { get; set; }
    }
}
