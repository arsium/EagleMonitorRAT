using System;
using System.Reflection;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Updater
{
    internal class Utils
    {
        internal class CoreAssembly
        {
            internal static readonly Assembly Reference = typeof(CoreAssembly).Assembly;
            internal static readonly Version Version = Reference.GetName().Version;
            internal static readonly string Name = Reference.GetName().Name;
        }

        internal static int[] VersionSplitter(string version) 
        {
            string[] split = version.Split('.');
            return new int[] { int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]), int.Parse(split[3]) };
        }
    }
}
