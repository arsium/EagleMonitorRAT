using System.Collections.Generic;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.Misc
{
    internal class EncryptionInformation
    {
        public string publicRSAServerKey { get; set; }
        public string privateRSAServerKey { get; set; }
        public bool isEncrypted { get; set; }
        public string msg { get; set; }
        public string wallet { get; set; }
        public bool subfolders { get; set; }
        public bool checkExtensions { get; set; }
        public List<string> extensionFile { get; set; }
        public List<string> paths { get; set; }
    }
}
