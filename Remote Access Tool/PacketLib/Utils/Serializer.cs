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
            return Encryption.RSMEncrypt(Compressor.QuickLZ.Compress(bytes, 1), Encoding.Unicode.GetBytes(key));//bytes;
        }

        public static IPacket DeserializePacket(this byte[] _byteArray, string key)
        {
            IPacket ReturnValue;
            //using (var _MemoryStream = new MemoryStream((_byteArray)))
            using (var _MemoryStream = new MemoryStream(Compressor.QuickLZ.Decompress(Encryption.RSMDecrypt(_byteArray, Encoding.Unicode.GetBytes(key)))))
            {
                IFormatter _BinaryFormatter = new BinaryFormatter();
                ReturnValue = (IPacket)_BinaryFormatter.Deserialize(_MemoryStream);
            }
            return ReturnValue;
        }
    }
}
