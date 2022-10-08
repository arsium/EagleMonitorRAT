using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Utils
{
    public static class Serializer
    {
        public static byte[] SerializePacket(this IPacket _object, string key)
        {
            byte[] bytes;
            using (var _MemoryStream = new MemoryStream())
            {
                IFormatter _BinaryFormatter = new BinaryFormatter();
                _BinaryFormatter.Serialize(_MemoryStream, _object);
                bytes = _MemoryStream.ToArray();
            }
            bytes = Encryption.RSMEncrypt(Compressor.QuickLZ.Compress(bytes, 1), Encoding.Unicode.GetBytes(key));
            return bytes;
        }

        public static IPacket DeserializePacket(this byte[] _byteArray, string key)
        {
            IPacket ReturnValue;
            using (var _MemoryStream = new MemoryStream(Compressor.QuickLZ.Decompress(Encryption.RSMDecrypt(_byteArray, Encoding.Unicode.GetBytes(key)))))
            {
                IFormatter _BinaryFormatter = new BinaryFormatter();
                ReturnValue = (IPacket)_BinaryFormatter.Deserialize(_MemoryStream);
            }
            return ReturnValue;
        }

        /*public static PacketHeader ConvertBytesToHeader(this byte[] _byteArray)
        {
            if (_byteArray.Length != 8)
                return null;
            PacketHeader header = new PacketHeader();
            header.size = BitConverter.ToInt32(new byte[4] { _byteArray[0], _byteArray[1], _byteArray[2], _byteArray[3] }, 0);
            header.packetId = BitConverter.ToInt32(new byte[4] { _byteArray[4], _byteArray[5], _byteArray[6], _byteArray[7] }, 0);
            return header;
        }*/
    }
}
