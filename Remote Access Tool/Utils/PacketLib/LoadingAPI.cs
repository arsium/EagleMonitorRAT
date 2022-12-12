using PacketLib.Utils;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib
{
    public class LoadingAPI
    {
        public Host Host { get; set; }
        public string BaseIp { get; set; }
        public string HWID { get; set; }
        public string Key { get; set; }
        public IPacket CurrentPacket { get; set; }
    }
}
