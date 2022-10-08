using System;
using System.Collections.Generic;
using static PacketLib.Packet.ProcessInjectionPacket;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.Misc
{
    internal class Settings
    {
        public List<int> ports { get; set; }
        public string key { get; set; }
        public bool notificationSound { get; set; }
        public bool notificationIcon{ get; set; }
        public bool autoSaveRecovery { get; set; }
        public bool autoRemoveRowWhenFileIsDownloaded { get; set; }
        public string flagsPackName { get; set; }
        public INJECTION_METHODS processInjectionMethod { get; set; }
        public int bufferSize { get; set; }
        public bool autoGenerateRSAKey { get; set; }
        public List<Tuple<string, string>> hosts { get; set; }
        //public List<IPacket> onConnectPackets { get; set; }
    }
}
