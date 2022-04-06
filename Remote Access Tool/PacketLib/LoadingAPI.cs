using PacketLib.Utils;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib
{
    public class LoadingAPI
    {
        public Host host { get; set; }
        public string baseIp { get; set; }
        public string HWID { get; set; }
        public string key { get; set; }
        public IPacket currentPacket { get; set; }
    }
}
