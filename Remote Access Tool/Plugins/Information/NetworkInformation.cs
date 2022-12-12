using PacketLib.Packet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static Plugin.Imports;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal class NetworkInformation
    {
        private static MIB_TCPROW_OWNER_PID[] GetAllTcpConnections()
        {
            int num = 2;
            int num2 = 0;
            uint num3 = GetExtendedTcpTable(IntPtr.Zero, ref num2, true, num, TCP_TABLE_TYPE.TCP_TABLE_OWNER_PID_ALL, 0);
            if (num3 != 0U && num3 != 122U)
            {
                throw new Exception("Error occurred when trying to query tcp table, return code: " + num3.ToString());
            }
            IntPtr intPtr = Marshal.AllocHGlobal(num2);
            MIB_TCPROW_OWNER_PID[] array;
            try
            {
                num3 = GetExtendedTcpTable(intPtr, ref num2, true, num, TCP_TABLE_TYPE.TCP_TABLE_OWNER_PID_ALL, 0);
                if (num3 != 0U)
                {
                    throw new Exception("Error occurred when trying to query tcp table, return code: " + num3.ToString());
                }
                MIB_TCPTABLE_OWNER_PID mib_TCPTABLE_OWNER_PID = (MIB_TCPTABLE_OWNER_PID)Marshal.PtrToStructure(intPtr, typeof(MIB_TCPTABLE_OWNER_PID));
                IntPtr intPtr2 = (IntPtr)((long)intPtr + (long)Marshal.SizeOf(mib_TCPTABLE_OWNER_PID.dwNumEntries));
                array = new MIB_TCPROW_OWNER_PID[mib_TCPTABLE_OWNER_PID.dwNumEntries];
                int num4 = 0;
                while ((long)num4 < (long)((ulong)mib_TCPTABLE_OWNER_PID.dwNumEntries))
                {
                    MIB_TCPROW_OWNER_PID mib_TCPROW_OWNER_PID = (MIB_TCPROW_OWNER_PID)Marshal.PtrToStructure(intPtr2, typeof(MIB_TCPROW_OWNER_PID));
                    array[num4] = mib_TCPROW_OWNER_PID;
                    intPtr2 = (IntPtr)((long)intPtr2 + (long)Marshal.SizeOf(mib_TCPROW_OWNER_PID));
                    num4++;
                }
            }
            finally
            {
                Marshal.FreeHGlobal(intPtr);
            }
            return array;
        }
        public static List<TCPInformation> AllTCPConnection()
        {
            List<TCPInformation> ret = new List<TCPInformation>();
            try
            {

                MIB_TCPROW_OWNER_PID[] allTcpConnections = GetAllTcpConnections();
                int num = allTcpConnections.Length;
                for (int i = 0; i < num; i++)
                {
                    MIB_TCPROW_OWNER_PID mib_TCPROW_OWNER_PID = allTcpConnections[i];
                    string localEndPoint = string.Format("{0}:{1}", Helpers.GetIpAddress((long)((ulong)mib_TCPROW_OWNER_PID.localAddr)), mib_TCPROW_OWNER_PID.LocalPort);
                    string remoteEndPoint = string.Format("{0}:{1}", Helpers.GetIpAddress((long)((ulong)mib_TCPROW_OWNER_PID.remoteAddr)), mib_TCPROW_OWNER_PID.RemotePort);

                    ret.Add(new TCPInformation()
                    {
                        PID = mib_TCPROW_OWNER_PID.owningPid,
                        LocalEndPoint = localEndPoint,
                        RemoteEndPoint = remoteEndPoint,
                        State = (TCPInformation.TCP_CONNECTION_STATE)mib_TCPROW_OWNER_PID.state,
                        processName = Process.GetProcessById(mib_TCPROW_OWNER_PID.owningPid).ProcessName
                    });
                }
            }
            catch
            {
            }
            return ret;
        }
    }
}
