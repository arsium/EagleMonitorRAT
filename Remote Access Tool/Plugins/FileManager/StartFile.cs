using System.Diagnostics;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal class StartFile
    {
        internal static void StartAProcess(string path) 
        {
            Process.Start(path);
        }
    }
}
