using System;
using System.Security.Cryptography;

/* 
|| AUTHOR https://bhf.im/threads/438711/ ||
|| Note : set padding to Zeros to fix Invalid Padding : ttps://stackoverflow.com/questions/8583112/padding-is-invalid-and-cannot-be-removed ||
*/

namespace Plugin.Encryption
{
    internal class RSM
    {
        public static byte[] RSMEncrypt(byte[] input, byte[] key)
        {
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(key, new byte[8], 1);

            AesManaged rijndaelManaged = new AesManaged
            {
                Mode = CipherMode.CBC,

                Padding = PaddingMode.Zeros,

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

            AesManaged rijndaelManaged = new AesManaged
            {
                Mode = CipherMode.CBC,

                Padding = PaddingMode.Zeros,

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
