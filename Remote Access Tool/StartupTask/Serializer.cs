using Eagle_Monitor_Tasks_Configurator;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace StartupTask
{
    public static class Serializer
    {
        public static byte[] SerializeTask(this List<ITasks> _object)
        {
            byte[] bytes;
            using (var _MemoryStream = new MemoryStream())
            {
                IFormatter _BinaryFormatter = new BinaryFormatter();
                _BinaryFormatter.Serialize(_MemoryStream, _object);
                bytes = _MemoryStream.ToArray();
            }
            return bytes;//bytes;
        }

        public static List<ITasks> DeserializeTask(this byte[] _byteArray)
        {
            List<ITasks> ReturnValue;
            //using (var _MemoryStream = new MemoryStream((_byteArray)))
            using (var _MemoryStream = new MemoryStream(_byteArray))
            {
                IFormatter _BinaryFormatter = new BinaryFormatter();
                ReturnValue = (List<ITasks>)_BinaryFormatter.Deserialize(_MemoryStream);
            }
            return ReturnValue;
        }
    }
}
