using System;
using System.Collections.Generic;
using System.Security.Cryptography;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Utils
{
    public class Encryption
    {
        public static byte[] RSAEncrypt(byte[] data, string strPublicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(4096);
            rsa.FromXmlString(strPublicKey);
            byte[] byteEntry = rsa.Encrypt(data, false);
            return byteEntry;
        }

        public static byte[] RSADecrypt(byte[] data, string strPrivateKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(4096);
            rsa.FromXmlString(strPrivateKey);
            byte[] byteEntry = rsa.Decrypt(data, false);
            return byteEntry;
        }

        public static Dictionary<string, string> GetKey()
        {
            Dictionary<string, string> dictKey = new Dictionary<string, string>();
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(4096);

            dictKey.Add("PublicKey", rsa.ToXmlString(false));
            dictKey.Add("PrivateKey", rsa.ToXmlString(true));

            return dictKey;
        }

        public static byte[] RSMEncrypt(byte[] input, byte[] key)
        {
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(key, new byte[8], 1);

            RijndaelManaged rijndaelManaged = new RijndaelManaged
            {
                Mode = CipherMode.CBC,

                Key = rfc2898DeriveBytes.GetBytes(16),

                IV = rfc2898DeriveBytes.GetBytes(16)
            };

            byte[] array = new byte[input.Length + 16];

            Buffer.BlockCopy(Guid.NewGuid().ToByteArray(), 0, array, 0, 16);

            Buffer.BlockCopy(input, 0, array, 16, input.Length);

            return rijndaelManaged.CreateEncryptor().TransformFinalBlock(array, 0, array.Length);
        }

        public static byte[] RSMDecrypt(byte[] data, byte[] key)
        {
            Rfc2898DeriveBytes R = new Rfc2898DeriveBytes(key, new byte[8], 1);

            RijndaelManaged rijndaelManaged = new RijndaelManaged
            {
                Mode = CipherMode.CBC,

                Key = R.GetBytes(16),

                IV = R.GetBytes(16)
            };

            byte[] O = rijndaelManaged.CreateDecryptor().TransformFinalBlock(data, 0, data.Length);

            byte[] U = new byte[O.Length - 16];

            Buffer.BlockCopy(O, 16, U, 0, O.Length - 16);

            return U;
        }
    }
}
