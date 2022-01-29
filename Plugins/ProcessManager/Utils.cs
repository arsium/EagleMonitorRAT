using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin
{
    internal class Utils
    {
        private static bool InternalCheckIsWow64(IntPtr Handle)
        {
            if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) ||
                Environment.OSVersion.Version.Major >= 6)
            {
                bool retVal;
                if (!NativeAPI.Process.IsWow64Process(Handle, out retVal))
                {
                    return false;
                }
                return retVal;
            }
            else
            {
                return false;
            }
        }

        internal static string CheckIf64Bit(IntPtr Handle)
        {
            if (InternalCheckIsWow64(Handle))
            {
                return "32";//only true if process is 32-bit running on a 64-bit OS.
            }
            else 
            {
                NativeAPI.Process.SYSTEM_INFO sys_info = new NativeAPI.Process.SYSTEM_INFO();
                NativeAPI.Process.GetNativeSystemInfo(ref sys_info);

                switch (sys_info.wProcessorArchitecture)
                {
                    case NativeAPI.Process.PROCESSOR_ARCHITECTURE_IA64:
                        return "IA64";

                    case NativeAPI.Process.PROCESSOR_ARCHITECTURE_AMD64:
                        return "64";

                    case NativeAPI.Process.PROCESSOR_ARCHITECTURE_INTEL:
                        return "32";

                    default:
                        return "Unknown";
                }
            }
        }
    }
}
