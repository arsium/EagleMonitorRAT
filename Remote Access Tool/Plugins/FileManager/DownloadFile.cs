using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal class DownloadFile
    {
        internal static byte[] ReadFile(string path)
        {
            byte[] result = null;
            try
            {
                result = System.IO.File.ReadAllBytes(path);
            }
            catch{}
            return result;
        }
    }
}
