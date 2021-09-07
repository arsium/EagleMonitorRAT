using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
||https://stackoverflow.com/questions/1446547/how-to-convert-an-object-to-a-byte-array-in-c-sharp
*/

namespace Shared
{
    public static class Serializer
    {
        [Serializable]
        public class Data
        {
            public string HWID { get; set; }
            public string IP_Origin { get; set; }
            public PacketTypes.PacketType Type { get; set; }
            public byte[] Plugin;
            //public object[] Misc;
            public dynamic DataReturn;
            public ReturnError returnError { get; set; }
        }
        [Serializable]
        public class ReturnError
        {
            public bool hasError { get; set; }
            public string errorDescription { get; set; }
        }

        public static byte[] Serialize(this Data _object)
        {
            byte[] bytes;
            using (var _MemoryStream = new MemoryStream())
            {
                IFormatter _BinaryFormatter = new BinaryFormatter();
                _BinaryFormatter.Serialize(_MemoryStream, _object);
                bytes = _MemoryStream.ToArray();
            }
            return Shared.Compressor.QuickLZ.Compress(bytes,1);
        }

        public static Data Deserialize(this byte[] _byteArray)
        {
            Data ReturnValue;
            using (var _MemoryStream = new MemoryStream(Shared.Compressor.QuickLZ.Decompress(_byteArray)))
            {
                IFormatter _BinaryFormatter = new BinaryFormatter();
                ReturnValue = (Data)_BinaryFormatter.Deserialize(_MemoryStream);
            }
            return ReturnValue;
        }

        public static int SendData(Socket S, byte[] data)
        {
            lock (S)
            {
                int total = 0;
                int size = data.Length;
                int datalft = size;
                byte[] datasize = new byte[4];
                S.Poll(-1, SelectMode.SelectWrite);
                datasize = BitConverter.GetBytes(size);
                int sent = S.Send(datasize);
                while (total < size)
                {
                    sent = S.Send(data, total, size, SocketFlags.None);
                    total += sent;
                    datalft -= sent;
                }
                return total;
            }
        }
    }
}
