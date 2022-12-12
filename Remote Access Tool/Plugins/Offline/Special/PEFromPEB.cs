using System;
using System.Runtime.InteropServices;
using static Offline.Special.Commons;

/*
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
|| This technique randomizes some PE fields. Some of those fields cannot be random otherwise the process will crash. ||
*/

namespace Offline.Special
{
    internal static class PEFromPEB
    {
        /*[UnmanagedFunctionPointer(CallingConvention.Winapi)]
        delegate NtStatus NtProtectVirtualMemory
        (
            IntPtr ProcessHandle,
            ref IntPtr BaseAddress,
            ref uint NumberOfBytesToProtect,
            PageAccessType NewAccessProtection,
            ref PageAccessType OldAccessProtection
        );

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        delegate NtStatus NtWriteVirtualMemory
        (
            IntPtr ProcessHandle,
            IntPtr BaseAddress,
            byte[] Buffer,
            uint NumberOfBytesToWrite,
            out uint NumberOfBytesWritten
        );

        static NtProtectVirtualMemory ntProtectVirtualMemory;
        static NtWriteVirtualMemory ntWriteVirtualMemory;

        static void PrepareDelegate()
        {
            IntPtr ntVirtualProtect = Resolver.GetExportAddress("ntdll.dll", "NtProtectVirtualMemory");
            ntProtectVirtualMemory = (NtProtectVirtualMemory)Marshal.GetDelegateForFunctionPointer(ntVirtualProtect, typeof(NtProtectVirtualMemory));

            IntPtr ntWriteVirtual = Resolver.GetExportAddress("ntdll.dll", "NtWriteVirtualMemory");
            ntWriteVirtualMemory = (NtWriteVirtualMemory)Marshal.GetDelegateForFunctionPointer(ntWriteVirtual, typeof(NtWriteVirtualMemory));

        }*/

        private static byte[] GetByteArray(int sizeInKb)
        {
            Random rnd = new Random();
            byte[] b = new byte[sizeInKb * 1024]; // convert kb to byte
            rnd.NextBytes(b);
            return b;
        }

        internal unsafe static bool BlockIt()
        {
            //PrepareDelegate();
            NtStatus n;
            if (IntPtr.Size == 4)
            {
                PEB32 peb32 = (PEB32)Marshal.PtrToStructure(GetPebAddressDynamicWithoutAllocate(), typeof(PEB32));
                ImageDosHeader dos32 = ReadStructAt<ImageDosHeader>(peb32.ImageBaseAddress);

                IntPtr address = peb32.ImageBaseAddress + 2;
                uint sizeOfPatch = (uint)(sizeof(ImageDosHeader) - 6);
                PageAccessType oldOne = new PageAccessType();

                n = ntProtectVirtualMemorySafe((IntPtr)(-1), ref address, ref sizeOfPatch, PageAccessType.PAGE_READWRITE, ref oldOne);

                if (n != NtStatus.Success)
                    return false;

                //Patching DOS (keeping the magic field with MZ)
                n = ntWriteVirtualMemorySafe((IntPtr)(-1), (IntPtr)(peb32.ImageBaseAddress + 2), GetByteArray(sizeof(ImageDosHeader) - 6), (uint)(sizeof(ImageDosHeader) - 6), out _);
                //Patching File Header (keeping signature field (PE) + machine)
                n = ntWriteVirtualMemorySafe((IntPtr)(-1), (IntPtr)(peb32.ImageBaseAddress + dos32.e_lfanew + 4 + 2), GetByteArray(sizeof(ImageFileHeader) - 2 - 2 - 2), (uint)(sizeof(ImageFileHeader) - 2 - 2 - 2), out _);
                //Patching Optional Header (keeping all ImageDataDirectory)
                n = ntWriteVirtualMemorySafe((IntPtr)(-1), (IntPtr)(peb32.ImageBaseAddress + dos32.e_lfanew + 4 + sizeof(ImageFileHeader)), GetByteArray(70), (uint)(70), out _);
                //Setting old protection
                n = ntProtectVirtualMemorySafe((IntPtr)(-1), ref address, ref sizeOfPatch, oldOne, ref oldOne);

            }
            else 
            {
                PEB64 peb64 = (PEB64)Marshal.PtrToStructure(GetPebAddressDynamicWithoutAllocate(), typeof(PEB64));
                ImageDosHeader dos64 = ReadStructAt<ImageDosHeader>(peb64.ImageBaseAddress);

                IntPtr address = peb64.ImageBaseAddress + 2;
                uint sizeOfPatch = (uint)(sizeof(ImageDosHeader) - 6);
                PageAccessType oldOne = new PageAccessType();

                n = ntProtectVirtualMemorySafe((IntPtr)(-1), ref address, ref sizeOfPatch, PageAccessType.PAGE_READWRITE, ref oldOne);

                if (n != NtStatus.Success)
                    return false;

                //Patching DOS (keeping the magic field with MZ)
                n = ntWriteVirtualMemorySafe((IntPtr)(-1), (IntPtr)(peb64.ImageBaseAddress + 2), GetByteArray(sizeof(ImageDosHeader) - 6), (uint)(sizeof(ImageDosHeader) - 6), out _);
                //Patching File Header (keeping signature field (PE) + machine)
                n = ntWriteVirtualMemorySafe((IntPtr)(-1), (IntPtr)(peb64.ImageBaseAddress + dos64.e_lfanew + 4 + 2), GetByteArray(sizeof(ImageFileHeader) - 2 - 2 - 2), (uint)(sizeof(ImageFileHeader) - 2 - 2 - 2), out _);
                //Patching Optional Header (keeping all ImageDataDirectory)
                n = ntWriteVirtualMemorySafe((IntPtr)(-1), (IntPtr)(peb64.ImageBaseAddress + dos64.e_lfanew + 4 + sizeof(ImageFileHeader)), GetByteArray(74), (uint)(74), out _);
                //Setting old protection
                n = ntProtectVirtualMemorySafe((IntPtr)(-1), ref address, ref sizeOfPatch, oldOne, ref oldOne);
            }
            if (n == NtStatus.Success)
                return true;
            return false;
        }
    }
}
