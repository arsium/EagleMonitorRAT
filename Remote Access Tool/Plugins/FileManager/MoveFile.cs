using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal class MoveFile
    {
        internal static bool RenameFile(string oldPath, string newPath) 
        {
            try
            {
                return Imports.MoveFile(oldPath, newPath);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
