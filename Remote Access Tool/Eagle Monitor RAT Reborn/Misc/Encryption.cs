using System.Collections.Generic;
using System.Security.Cryptography;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.Misc
{
    internal class Encryption
    {
        internal static byte[] RSAEncrypt(byte[] data, string strPublicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(4096);//4096
            rsa.FromXmlString(strPublicKey);
            byte[] byteEntry = rsa.Encrypt(data, false);
            return byteEntry;
        }

        internal static byte[] RSADecrypt(byte[] data, string strPrivateKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(4096);//4096
            rsa.FromXmlString(strPrivateKey);
            byte[] byteEntry = rsa.Decrypt(data, false);
            return byteEntry;
        }

        internal static Dictionary<string, string> GetKey()
        {
            Dictionary<string, string> dictKey = new Dictionary<string, string>();
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(4096);//4096

            dictKey.Add("PublicKey", rsa.ToXmlString(false));
            dictKey.Add("PrivateKey", rsa.ToXmlString(true));

            return dictKey;
        }
    }
}
