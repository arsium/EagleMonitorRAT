using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using static PacketLib.Packet.InformationPacket;

/*
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
|| Note : This code comes from a program I dumped ||
*/
namespace Plugin
{
    internal class NetworkInformation
    {
        internal class SocketConnectionIPV4
        {

            [DllImport("iphlpapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern uint GetExtendedTcpTable(IntPtr pTcpTable, ref int pdwSize, bool bOrder, int ulAf, SocketConnectionIPV4.TcpTableClass tableClass, uint reserved = 0U);


            [DllImport("iphlpapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern uint GetExtendedUdpTable(IntPtr pUdpTable, ref int pdwSize, bool bOrder, int ulAf, SocketConnectionIPV4.UdpTableClass tableClass, uint reserved = 0U);


            public static int GetAvailablePort(int startingPort)
            {
                List<int> list = new List<int>();
                IPGlobalProperties ipglobalProperties = IPGlobalProperties.GetIPGlobalProperties();
                TcpConnectionInformation[] activeTcpConnections = ipglobalProperties.GetActiveTcpConnections();
                list.AddRange(from n in activeTcpConnections
                              where n.LocalEndPoint.Port >= startingPort
                              select n.LocalEndPoint.Port);
                IPEndPoint[] source = ipglobalProperties.GetActiveTcpListeners();
                list.AddRange(from n in source
                              where n.Port >= startingPort
                              select n.Port);
                source = ipglobalProperties.GetActiveUdpListeners();
                list.AddRange(from n in source
                              where n.Port >= startingPort
                              select n.Port);
                list.Sort();
                for (int i = startingPort; i < 65535; i++)
                {
                    bool flag = !list.Contains(i);
                    if (flag)
                    {
                        return i;
                    }
                }
                return 0;
            }


            public static List<TcpProcessRecord> GetAllTcpConnections()
            {
                int cb = 0;
                List<TcpProcessRecord> list = new List<TcpProcessRecord>();
                uint extendedTcpTable = SocketConnectionIPV4.GetExtendedTcpTable(IntPtr.Zero, ref cb, true, 2, SocketConnectionIPV4.TcpTableClass.TCP_TABLE_OWNER_PID_ALL, 0U);
                IntPtr intPtr = Marshal.AllocHGlobal(cb);
                try
                {
                    extendedTcpTable = SocketConnectionIPV4.GetExtendedTcpTable(intPtr, ref cb, true, 2, SocketConnectionIPV4.TcpTableClass.TCP_TABLE_OWNER_PID_ALL, 0U);
                    bool flag = extendedTcpTable > 0U;
                    if (flag)
                    {
                        return new List<TcpProcessRecord>();
                    }
                    SocketConnectionIPV4.MIB_TCPTABLE_OWNER_PID mib_TCPTABLE_OWNER_PID = (SocketConnectionIPV4.MIB_TCPTABLE_OWNER_PID)Marshal.PtrToStructure(intPtr, typeof(SocketConnectionIPV4.MIB_TCPTABLE_OWNER_PID));
                    IntPtr intPtr2 = (IntPtr)((long)intPtr + Marshal.SizeOf(mib_TCPTABLE_OWNER_PID.dwNumEntries));//(IntPtr)((long)intPtr + (long)Marshal.SizeOf<uint>(mib_TCPTABLE_OWNER_PID.dwNumEntries));
                    int num = 0;
                    while ((long)num < (long)((ulong)mib_TCPTABLE_OWNER_PID.dwNumEntries))
                    {
                        SocketConnectionIPV4.MIB_TCPROW_OWNER_PID mib_TCPROW_OWNER_PID = (SocketConnectionIPV4.MIB_TCPROW_OWNER_PID)Marshal.PtrToStructure(intPtr2, typeof(SocketConnectionIPV4.MIB_TCPROW_OWNER_PID));
                        list.Add(new TcpProcessRecord(new IPAddress((long)((ulong)mib_TCPROW_OWNER_PID.localAddr)), new IPAddress((long)((ulong)mib_TCPROW_OWNER_PID.remoteAddr)), BitConverter.ToUInt16(new byte[]
                        {
                        mib_TCPROW_OWNER_PID.localPort[1],
                        mib_TCPROW_OWNER_PID.localPort[0]
                        }, 0), BitConverter.ToUInt16(new byte[]
                        {
                        mib_TCPROW_OWNER_PID.remotePort[1],
                        mib_TCPROW_OWNER_PID.remotePort[0]
                        }, 0), mib_TCPROW_OWNER_PID.owningPid, (TcpProcessRecord.MibTcpState)mib_TCPROW_OWNER_PID.state));
                        intPtr2 = (IntPtr)((long)intPtr2 + Marshal.SizeOf(mib_TCPROW_OWNER_PID));//(IntPtr)((long)intPtr2 + (long)Marshal.SizeOf<SocketConnectionIPV4.MIB_TCPROW_OWNER_PID>(mib_TCPROW_OWNER_PID));
                        num++;
                    }
                }
                catch (OutOfMemoryException)
                {
                }
                catch (Exception)
                {
                }
                finally
                {
                    Marshal.FreeHGlobal(intPtr);
                }
                return (list != null) ? list.Distinct<TcpProcessRecord>().ToList<TcpProcessRecord>() : new List<TcpProcessRecord>();
            }


            public static List<UdpProcessRecord> GetAllUdpConnections()
            {
                int cb = 0;
                List<UdpProcessRecord> list = new List<UdpProcessRecord>();
                uint extendedUdpTable = SocketConnectionIPV4.GetExtendedUdpTable(IntPtr.Zero, ref cb, true, 2, SocketConnectionIPV4.UdpTableClass.UDP_TABLE_OWNER_PID, 0U);
                IntPtr intPtr = Marshal.AllocHGlobal(cb);
                try
                {
                    extendedUdpTable = SocketConnectionIPV4.GetExtendedUdpTable(intPtr, ref cb, true, 2, SocketConnectionIPV4.UdpTableClass.UDP_TABLE_OWNER_PID, 0U);
                    bool flag = extendedUdpTable > 0U;
                    if (flag)
                    {
                        return new List<UdpProcessRecord>();
                    }
                    SocketConnectionIPV4.MIB_UDPTABLE_OWNER_PID mib_UDPTABLE_OWNER_PID = (SocketConnectionIPV4.MIB_UDPTABLE_OWNER_PID)Marshal.PtrToStructure(intPtr, typeof(SocketConnectionIPV4.MIB_UDPTABLE_OWNER_PID));
                    IntPtr intPtr2 = (IntPtr)((long)intPtr + Marshal.SizeOf(mib_UDPTABLE_OWNER_PID.dwNumEntries));//(IntPtr)((long)intPtr + (long)Marshal.SizeOf<uint>(mib_UDPTABLE_OWNER_PID.dwNumEntries));
                    int num = 0;
                    while ((long)num < (long)((ulong)mib_UDPTABLE_OWNER_PID.dwNumEntries))
                    {
                        MIB_UDPROW_OWNER_PID mib_UDPROW_OWNER_PID = (SocketConnectionIPV4.MIB_UDPROW_OWNER_PID)Marshal.PtrToStructure(intPtr2, typeof(SocketConnectionIPV4.MIB_UDPROW_OWNER_PID));
                        list.Add(new UdpProcessRecord(new IPAddress((long)((ulong)mib_UDPROW_OWNER_PID.localAddr)), (uint)BitConverter.ToUInt16(new byte[]
                        {
                        mib_UDPROW_OWNER_PID.localPort[1],
                        mib_UDPROW_OWNER_PID.localPort[0]
                        }, 0), mib_UDPROW_OWNER_PID.owningPid));
                        intPtr2 = (IntPtr)((long)intPtr2 + Marshal.SizeOf(mib_UDPROW_OWNER_PID));
                        num++;
                    }
                }
                catch (OutOfMemoryException)
                {
                }
                catch (Exception)
                {
                }
                finally
                {
                    Marshal.FreeHGlobal(intPtr);
                }
                return (list != null) ? list.Distinct<UdpProcessRecord>().ToList<UdpProcessRecord>() : new List<UdpProcessRecord>();
            }


            private const int AF_INET = 2;


            public enum Protocol
            {
                TCP,
                UDP
            }

            public enum TcpTableClass
            {
                TCP_TABLE_BASIC_LISTENER,
                TCP_TABLE_BASIC_CONNECTIONS,
                TCP_TABLE_BASIC_ALL,
                TCP_TABLE_OWNER_PID_LISTENER,
                TCP_TABLE_OWNER_PID_CONNECTIONS,
                TCP_TABLE_OWNER_PID_ALL,
                TCP_TABLE_OWNER_MODULE_LISTENER,
                TCP_TABLE_OWNER_MODULE_CONNECTIONS,
                TCP_TABLE_OWNER_MODULE_ALL
            }

            public enum UdpTableClass
            {
                UDP_TABLE_BASIC,
                UDP_TABLE_OWNER_PID,
                UDP_TABLE_OWNER_MODULE
            }

            public enum MibTcpState
            {
                CLOSED = 1,
                LISTENING,
                SYN_SENT,
                SYN_RCVD,
                ESTABLISHED,
                FIN_WAIT1,
                FIN_WAIT2,
                CLOSE_WAIT,
                CLOSING,
                LAST_ACK,
                TIME_WAIT,
                DELETE_TCB,
                NONE = 0
            }

            public struct MIB_TCPROW_OWNER_PID
            {
                public SocketConnectionIPV4.MibTcpState state;

                public uint localAddr;

                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
                public byte[] localPort;

                public uint remoteAddr;

                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
                public byte[] remotePort;

                public int owningPid;
            }

            public struct MIB_TCPTABLE_OWNER_PID
            {
                public uint dwNumEntries;

                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.Struct)]
                public SocketConnectionIPV4.MIB_TCPROW_OWNER_PID[] table;
            }

            /*[StructLayout(LayoutKind.Sequential)]
            public class TcpProcessRecord
            {
                public IPAddress LocalAddress { get; set; }

                public ushort LocalPort { get; set; }

                public IPAddress RemoteAddress { get; set; }

                public ushort RemotePort { get; set; }

                public SocketConnectionIPV4.MibTcpState State { get; set; }

                public int ProcessId { get; set; }
                public string ProcessName { get; set; }

                public TcpProcessRecord(IPAddress localIp, IPAddress remoteIp, ushort localPort, ushort remotePort, int pId, SocketConnectionIPV4.MibTcpState state)
                {
                    this.LocalAddress = localIp;
                    this.RemoteAddress = remoteIp;
                    this.LocalPort = localPort;
                    this.RemotePort = remotePort;
                    this.State = state;
                    this.ProcessId = pId;
                    bool flag = Process.GetProcesses().Any((Process process) => process.Id == pId);
                    if (flag)
                    {
                        this.ProcessName = Process.GetProcessById(this.ProcessId).ProcessName;
                    }
                }
            }*/

            public struct MIB_UDPROW_OWNER_PID
            {
                public uint localAddr;

                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
                public byte[] localPort;

                public int owningPid;
            }

            public struct MIB_UDPTABLE_OWNER_PID
            {
                public uint dwNumEntries;

                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.Struct)]
                public UdpProcessRecord[] table;
            }

           /* [StructLayout(LayoutKind.Sequential)]
            public class UdpProcessRecord
            {
                public IPAddress LocalAddress { get; set; }

                public uint LocalPort { get; set; }

                public int ProcessId { get; set; }

                public string ProcessName { get; set; }

                public UdpProcessRecord(IPAddress localAddress, uint localPort, int pId)
                {
                    this.LocalAddress = localAddress;
                    this.LocalPort = localPort;
                    this.ProcessId = pId;
                    bool flag = Process.GetProcesses().Any((Process process) => process.Id == pId);
                    if (flag)
                    {
                        this.ProcessName = Process.GetProcessById(this.ProcessId).ProcessName;
                    }
                }
            }*/
        }
    }
}
