using System;
using System.Runtime.InteropServices;

namespace PacketLib
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class PacketHeader
    {
        [MarshalAs(UnmanagedType.I4)]
        public int size;
        [MarshalAs(UnmanagedType.I4)]
        public int packetId;
    }
}
