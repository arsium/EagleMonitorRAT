using PacketLib;
using Plugin.Properties;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal class HardwareInformation
    {
        private delegate IntPtr CPUNative();
        internal static string CPU 
        {
            get 
            {
                DLLFromMemory cpuInfo;

                if (IntPtr.Size == 4)
                {
                    cpuInfo = new DLLFromMemory(Resources.CPU);
                    CPUNative C = (CPUNative)cpuInfo.GetDelegateFromFuncName("CpuInformation", typeof(CPUNative));
                    return Marshal.PtrToStringUni(C());
                }

                if (IntPtr.Size == 8)
                {
                    cpuInfo = new DLLFromMemory(Resources.CPU64);
                    CPUNative C = (CPUNative)cpuInfo.GetDelegateFromFuncName("CpuInformation", typeof(CPUNative));
                    return Marshal.PtrToStringUni(C());
                }
                return "";
            }
        }

        internal static Dictionary<string, string> Information() 
        {
            Dictionary<string, string> infoList = new Dictionary<string, string>();
            infoList.Add("CPU", CPU);
            WMIHelper(@"\root\CIMV2", "SELECT Caption FROM Win32_OperatingSystem", ref infoList);
            WMIHelper(@"\root\CIMV2", "SELECT CSName FROM Win32_OperatingSystem", ref infoList);
            WMIHelper(@"\root\CIMV2", "SELECT Manufacturer FROM Win32_OperatingSystem", ref infoList);
            WMIHelper(@"\root\CIMV2", "SELECT OSArchitecture FROM Win32_OperatingSystem", ref infoList);
            WMIHelper(@"\root\CIMV2", "SELECT OSProductSuite FROM Win32_OperatingSystem", ref infoList);
            WMIHelper(@"\root\CIMV2", "SELECT OSType FROM Win32_OperatingSystem", ref infoList);
            WMIHelper(@"\root\CIMV2", "SELECT ProductType FROM Win32_OperatingSystem", ref infoList);
            WMIHelper(@"\root\CIMV2", "SELECT RegisteredUser FROM Win32_OperatingSystem", ref infoList);
            WMIHelper(@"\root\CIMV2", "SELECT SystemDirectory FROM Win32_OperatingSystem", ref infoList);
            WMIHelper(@"\root\CIMV2", "SELECT SystemDrive FROM Win32_OperatingSystem", ref infoList);
            WMIHelper(@"\root\CIMV2", "SELECT Version FROM Win32_OperatingSystem", ref infoList);
            return infoList;
        }

        private static void WMIHelper(string path, string request, ref Dictionary<string, string> dict)
        {
            WMILib.WMIRequest req = new WMILib.WMIRequest(path, request);
            req.Request();
            if (req.RequestReturns.Count == 1)
            {
                foreach (var a in req.RequestReturns)
                {
                    foreach (var c in a)
                    {
                        dict.Add(c.Key, c.Value);
                    }
                }
            }
        }
    }
}
