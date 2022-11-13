using System;
using System.Runtime.InteropServices;

/*
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Offline.Special
{
    internal static partial class Commons
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]//for manual shellcode
        private delegate IntPtr GetPeb();
        internal static IntPtr GetPebAddressDynamicWithoutAllocate()
        {
            IntPtr pebFunction = Resolver.GetExportAddress("ntdll.dll", "RtlGetCurrentPeb");

            GetPeb pebAddress = (GetPeb)Marshal.GetDelegateForFunctionPointer(pebFunction, typeof(GetPeb));

            return pebAddress();
        }

        internal static T ReadStructAt<T>(IntPtr pStructure)
        {
            int size = Marshal.SizeOf(typeof(T));
            IntPtr ptr = Marshal.AllocHGlobal(size);
            CopyMemory(ptr, pStructure, (uint)size);
            T res = (T)Marshal.PtrToStructure(ptr, typeof(T));
            Marshal.FreeHGlobal(ptr);
            return res;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void CopyMemory
        (
            IntPtr pDest,
            IntPtr pSrc,
            uint length
        );

        [DllImport("ntdll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        internal static extern NtStatus NtQueryInformationProcess
           (
               IntPtr ProcessHandle,
               ProcessInfoClass ProcessInforationClass,
               IntPtr ProcessInformation,
               uint ProcessInformationLength,
               out uint ReturnLength
           );
    }
}
