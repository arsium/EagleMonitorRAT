using System;
using System.Runtime.InteropServices;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal class Imports
    {
        #region "Structures"
        internal struct MIB_TCPROW_OWNER_PID
        {
            public ushort LocalPort
            {
                get
                {
                    return BitConverter.ToUInt16(new byte[] { this.localPort2, this.localPort1 }, 0);
                }
            }
            public ushort RemotePort
            {
                get
                {
                    return BitConverter.ToUInt16(new byte[] { this.remotePort2, this.remotePort1 }, 0);
                }
            }

            public uint state;
            public uint localAddr;
            public byte localPort1;
            public byte localPort2;
            public byte localPort3;
            public byte localPort4;
            public uint remoteAddr;
            public byte remotePort1;
            public byte remotePort2;
            public byte remotePort3;
            public byte remotePort4;
            public int owningPid;
        }

        internal struct MIB_TCPTABLE_OWNER_PID
        {
            internal uint dwNumEntries;

            internal MIB_TCPROW_OWNER_PID table;
        }
        #endregion
        #region "Enums"

        public enum TCP_TABLE_TYPE
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
        #endregion
        #region "Functions"
        [DllImport("Ws2_32.dll")]
        internal static extern ushort ntohs(ushort netshort);

        [DllImport("iphlpapi.dll", SetLastError = true)]
        internal static extern uint GetExtendedTcpTable(IntPtr pTcpTable, ref int dwOutBufLen, bool sort, int ipVersion, TCP_TABLE_TYPE tblClass, int reserved);
        #endregion
    }
}
