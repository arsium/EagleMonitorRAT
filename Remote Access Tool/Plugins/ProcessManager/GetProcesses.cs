using PacketLib.Packet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal class GetProcesses
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEM_INFO
        {
            public ushort wProcessorArchitecture;
            public ushort wReserved;
            public uint dwPageSize;
            public IntPtr lpMinimumApplicationAddress;
            public IntPtr lpMaximumApplicationAddress;
            public UIntPtr dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public ushort wProcessorLevel;
            public ushort wProcessorRevision;
        };

        [DllImport("kernel32.dll")]
        public static extern void GetNativeSystemInfo(ref SYSTEM_INFO lpSystemInfo);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWow64Process([In] IntPtr hProcess, [Out] out bool wow64Process);

        public const ushort PROCESSOR_ARCHITECTURE_INTEL = 0;
        public const ushort PROCESSOR_ARCHITECTURE_IA64 = 6;
        public const ushort PROCESSOR_ARCHITECTURE_AMD64 = 9;
        public const ushort PROCESSOR_ARCHITECTURE_UNKNOWN = 0xFFFF;

        private static bool InternalCheckIsWow64(IntPtr Handle)
        {
            if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) ||
                Environment.OSVersion.Version.Major >= 6)
            {
                bool retVal;
                if (!IsWow64Process(Handle, out retVal))
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
                SYSTEM_INFO sys_info = new SYSTEM_INFO();
              GetNativeSystemInfo(ref sys_info);

                switch (sys_info.wProcessorArchitecture)
                {
                    case PROCESSOR_ARCHITECTURE_IA64:
                        return "IA64";

                    case PROCESSOR_ARCHITECTURE_AMD64:
                        return "64";

                    case PROCESSOR_ARCHITECTURE_INTEL:
                        return "32";

                    default:
                        return "Unknown";
                }
            }
        }

        public static List<Proc> GetAllProcesses() 
        {
            List<Proc> procs = new List<Proc> ();
            foreach (Process p in Process.GetProcesses()) 
            {
                Proc proc = new Proc();
                MemoryStream stream = new MemoryStream();
                try
                {
                    Bitmap bitmap = System.Drawing.Icon.ExtractAssociatedIcon(p.MainModule.FileName).ToBitmap();
                    bitmap.Save(stream, ImageFormat.Png);
                    proc.processIcon = stream.ToArray();
                }
                catch (Exception)
                {
                }

                proc.processId = p.Id;
                proc.processName = p.ProcessName;
                if(p.MainWindowTitle == "")
                    proc.processWindowTitle = "N/A";
                else
                    proc.processWindowTitle = p.MainWindowTitle;
                proc.processWindowHandle = p.MainWindowHandle.ToString();

                try
                {
                    proc.is64Bit = CheckIf64Bit(p.Handle);
                }
                catch (Exception)
                {
                    proc.is64Bit = "?";
                }

                //yield return proc;
                procs.Add(proc);
            }
            return procs;
        }
    }
}
