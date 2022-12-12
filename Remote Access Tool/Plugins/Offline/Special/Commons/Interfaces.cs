using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Offline.Special
{
    internal static partial class Commons
    {
        [ComImport, Guid("6EDD6D74-C007-4E75-B76A-E5740995E24C"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface ILua
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
            void Method1();
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
            void Method2();
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
            void Method3();
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
            void Method4();
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
            void Method5();
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
            void Method6();
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime), PreserveSig]
            Parser.HRESULT ShellExec(
                [In, MarshalAs(UnmanagedType.LPWStr)] string file,
                [In, MarshalAs(UnmanagedType.LPWStr)] string paramaters,
                [In, MarshalAs(UnmanagedType.LPWStr)] string directory,
                [In] uint fMask,
                [In] uint nShow);
        }
    }
}
