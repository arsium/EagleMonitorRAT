using System.Diagnostics;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal static class Link
    {
        internal static void OpenLink(string url) 
        {
            Process.Start(url);
        }
    }
}
