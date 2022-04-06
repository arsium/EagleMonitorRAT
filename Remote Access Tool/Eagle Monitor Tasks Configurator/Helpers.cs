
/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_Tasks_Configurator
{
    internal class Helpers
    {
        internal static string SplitPath(string P)
        {
            string[] spl = P.Split('\\');
            return spl[spl.Length - 1];
        }
    }
}
