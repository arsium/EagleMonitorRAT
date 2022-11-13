using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static Offline.Special.Commons;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Offline.Special
{
    internal static class Resolver
    {
        internal unsafe static IntPtr GetModuleBaseAddress(string name, IntPtr processH)
        {
            int nHandleInfoSize = (int)sizeof(ProcessBasicInformation);
            IntPtr ipHandlePointer = Marshal.AllocHGlobal((int)nHandleInfoSize);
            uint nLength = 0;

            NtStatus n = NtQueryInformationProcess(processH, ProcessInfoClass.ProcessBasicInformation, ipHandlePointer, (uint)nHandleInfoSize, out nLength);

            if (n != NtStatus.Success)
                return IntPtr.Zero;

            ProcessBasicInformation processBasicInformation = new ProcessBasicInformation();
            processBasicInformation = (ProcessBasicInformation)Marshal.PtrToStructure(ipHandlePointer, processBasicInformation.GetType());

            if (IntPtr.Size == 4)
            {
                PEB32 peb32 = new PEB32();
                peb32 = (PEB32)Marshal.PtrToStructure(processBasicInformation.PebBaseAddress, peb32.GetType());

                PebLdrData pebLdrData = (PebLdrData)Marshal.PtrToStructure(peb32.Ldr, typeof(PebLdrData));

                //dte->DllBase != null
                for (LdrDataTableEntry* dte = (LdrDataTableEntry*)pebLdrData.InLoadOrderModuleList.Flink; dte->DllBase != IntPtr.Zero; dte = (LdrDataTableEntry*)dte->InLoadOrderLinks.Flink)
                {
                    if (dte->BaseDllName.ToString().ToLower() == name)
                    {
                        return dte->DllBase;
                    }
                }
            }
            else
            {
                PEB64 peb64 = new PEB64();
                peb64 = (PEB64)Marshal.PtrToStructure(processBasicInformation.PebBaseAddress, peb64.GetType());

                PebLdrData pebLdrData = (PebLdrData)Marshal.PtrToStructure(peb64.Ldr, typeof(PebLdrData));

                //dte->DllBase != null
                for (LdrDataTableEntry* dte = (LdrDataTableEntry*)pebLdrData.InLoadOrderModuleList.Flink; dte->DllBase != IntPtr.Zero; dte = (LdrDataTableEntry*)dte->InLoadOrderLinks.Flink)
                {
                    if (dte->BaseDllName.ToString().ToLower() == name)
                    {
                        return dte->DllBase;
                    }
                }
            }
            return IntPtr.Zero;
        }

        internal static IntPtr GetExportAddress(string moduleName, string functionName)
        {
            Dictionary<int, ExportedFunction> listOfFunctions = new Dictionary<int, ExportedFunction>();

            IntPtr moduleBaseAddr = GetModuleBaseAddress(moduleName, (IntPtr)(-1));

            ImageDosHeader dosHeader = (ImageDosHeader)Marshal.PtrToStructure(moduleBaseAddr, typeof(ImageDosHeader));
            if (IntPtr.Size == 4)
            {

                ImageOptionalHeader32 peHeader = (ImageOptionalHeader32)Marshal.PtrToStructure(moduleBaseAddr + dosHeader.e_lfanew + 4 + Marshal.SizeOf(typeof(ImageFileHeader)), typeof(ImageOptionalHeader32));
                ImageExportDirectory exportHeader = (ImageExportDirectory)Marshal.PtrToStructure(moduleBaseAddr + (int)peHeader.ExportTable.VirtualAddress, typeof(ImageExportDirectory));

                IntPtr pNames = IntPtr.Add(moduleBaseAddr, exportHeader.AddressOfNames);
                IntPtr pNameOrdinals = IntPtr.Add(moduleBaseAddr, exportHeader.AddressOfNameOrdinals);
                IntPtr pFunctions = IntPtr.Add(moduleBaseAddr, exportHeader.AddressOfFunctions);

                for (int i = 0; i <= exportHeader.NumberOfFunctions - 1; i++)
                {
                    int rva = Marshal.ReadInt32(pFunctions, i * 4);
                    if (rva != 0)
                    {
                        listOfFunctions.Add(exportHeader.OrdinalBase + i, new ExportedFunction()
                        {
                            Ordinal = exportHeader.OrdinalBase + i,
                            RelativeAddress = rva,
                            FullAddress = (IntPtr)((int)moduleBaseAddr + rva)
                        });
                    }
                }

                for (int i = 0; i <= exportHeader.NumberOfNames - 1; i++)
                {
                    int ordinal = exportHeader.OrdinalBase + Marshal.ReadInt16(pNameOrdinals, i * 2);
                    ExportedFunction entry = listOfFunctions[ordinal];
                    IntPtr nameAddress = IntPtr.Add(moduleBaseAddr, Marshal.ReadInt32(pNames, i * 4));
                    entry.Name = Marshal.PtrToStringAnsi(nameAddress);
                    if (entry.Name == functionName)
                        return entry.FullAddress;
                }
            }
            else
            {

                ImageOptionalHeader64 peHeader = (ImageOptionalHeader64)Marshal.PtrToStructure(moduleBaseAddr + dosHeader.e_lfanew + 4 + Marshal.SizeOf(typeof(ImageFileHeader)), typeof(ImageOptionalHeader64));
                ImageExportDirectory exportHeader = (ImageExportDirectory)Marshal.PtrToStructure(moduleBaseAddr + (int)peHeader.ExportTable.VirtualAddress, typeof(ImageExportDirectory));

                IntPtr pNames = IntPtr.Add(moduleBaseAddr, exportHeader.AddressOfNames);
                IntPtr pNameOrdinals = IntPtr.Add(moduleBaseAddr, exportHeader.AddressOfNameOrdinals);
                IntPtr pFunctions = IntPtr.Add(moduleBaseAddr, exportHeader.AddressOfFunctions);

                for (int i = 0; i <= exportHeader.NumberOfFunctions - 1; i++)
                {
                    int rva = Marshal.ReadInt32(pFunctions, i * 4);
                    if (rva != 0)
                    {
                        listOfFunctions.Add(exportHeader.OrdinalBase + i, new ExportedFunction()
                        {
                            Ordinal = exportHeader.OrdinalBase + i,
                            RelativeAddress = rva,
                            FullAddress = (IntPtr)((long)moduleBaseAddr + rva)
                        });
                    }
                }

                for (int i = 0; i <= exportHeader.NumberOfNames - 1; i++)
                {
                    int ordinal = exportHeader.OrdinalBase + Marshal.ReadInt16(pNameOrdinals, i * 2);
                    ExportedFunction entry = listOfFunctions[ordinal];
                    IntPtr nameAddress = IntPtr.Add(moduleBaseAddr, Marshal.ReadInt32(pNames, i * 4));
                    entry.Name = Marshal.PtrToStringAnsi(nameAddress);
                    if (entry.Name == functionName)
                        return entry.FullAddress;
                }
            }
            return IntPtr.Zero;
        }

        public class ExportedFunction
        {
            public int Ordinal { get; set; }
            public string Name { get; set; }
            public int RelativeAddress { get; set; }
            public IntPtr FullAddress { get; set; }
        }
    }
}
