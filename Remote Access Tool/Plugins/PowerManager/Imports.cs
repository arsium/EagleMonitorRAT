using System;
using System.Runtime.InteropServices;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal static class Imports
    {
        #region "ntdll"
        private const string ntdll = "ntdll.dll";

		[Flags]
		internal enum NTSTATUS : uint
		{
			STATUS_SUCCESS,
			STATUS_INFO_LENGTH_MISMATCH =       0xC0000004,
			STATUS_UNKNOWN =                    0xFFFFFFFF
		}

        internal enum RTL_PRIVILEGES : uint
		{
			SeCreateTokenPrivilege =            1,
			SeAssignPrimaryTokenPrivilege =     2,
			SeLockMemoryPrivilege =             3,
			SeIncreaseQuotaPrivilege =          4,
			SeUnsolicitedInputPrivilege =       5,
			SeMachineAccountPrivilege =         6,
			SeTcbPrivilege =                    7,
			SeSecurityPrivilege =               8,
			SeTakeOwnershipPrivilege =          9,
			SeLoadDriverPrivilege =             10,
			SeSystemProfilePrivilege =          11,
			SeSystemtimePrivilege =             12,
			SeProfileSingleProcessPrivilege =   13,
			SeIncreaseBasePriorityPrivilege =   14,
			SeCreatePagefilePrivilege =         15,
			SeCreatePermanentPrivilege =        16,
			SeBackupPrivilege =                 17,
			SeRestorePrivilege =                18,
			SeShutdownPrivilege =               19,
			SeDebugPrivilege =                  20,
			SeAuditPrivilege =                  21,
			SeSystemEnvironmentPrivilege =      22,
			SeChangeNotifyPrivilege =           23,
			SeRemoteShutdownPrivilege =         24,
			SeUndockPrivilege =                 25,
			SeSyncAgentPrivilege =              26,
			SeEnableDelegationPrivilege =       27,
			SeManageVolumePrivilege =           28,
			SeImpersonatePrivilege =            29,
			SeCreateGlobalPrivilege =           30,
			SeTrustedCredManAccessPrivilege =   31,
			SeRelabelPrivilege =                32,
			SeIncreaseWorkingSetPrivilege =     33,
			SeTimeZonePrivilege =               34,
			SeCreateSymbolicLinkPrivilege =     35
		}

        internal enum HARDERROR_RESPONSE_OPTION : uint 
        {
            OptionAbortRetryIgnore =            0,
            OptionOk =                          1,
            OptionOkCancel =                    2,
            OptionRetryCancel =                 3,
            OptionYesNo =                       4,
            OptionYesNoCancel =                 5,
            OptionShutdownSystem =              6
        }

        internal enum HARDERROR_RESPONSE : uint 
        {
            ResponseReturnToCaller =            0,
            ResponseNotHandled =                1,
            ResponseAbort =                     2,
            ResponseCancel =                    3,
            ResponseIgnore =                    4,
            ResponseNo =                        5,
            ResponseOk =                        6,
            ResponseRetry =                     7,
            ResponseYes =                       8
        }


        [DllImport(ntdll)]
		internal static extern NTSTATUS RtlAdjustPrivilege(RTL_PRIVILEGES privilege, bool bEnablePrivilege, bool IsThreadPrivilege, out bool PreviousValue);
        [DllImport(ntdll)]
        internal static extern NTSTATUS NtRaiseHardError(uint ErrorStatus, uint NumberOfParameters, uint UnicodeStringParameterMask, IntPtr Parameters, HARDERROR_RESPONSE_OPTION ValidResponseOption, out HARDERROR_RESPONSE Response);
        #endregion

        #region "user32"
        private const string user32 = "user32.dll";

        //https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-exitwindowsex
        //https://docs.microsoft.com/en-us/windows/win32/shutdown/system-shutdown-reason-codes
        public enum EXIT_WINDOWS_FLAGS : uint
        {
            EWX_HYBRID_SHUTDOWN = 0x00400000,
            EWX_LOGOFF = 0x00000000,
            EWX_POWEROFF = 0x00000008,
            EWX_REBOOT = 0x00000002,
            EWX_RESTARTAPPS = 0x00000040,
            EWX_SHUTDOWN = 0x00000001,

            EWX_FORCE = 0x00000004,
            EWX_FORCEIFHUNG = 0x00000010
        }

        public enum EXIT_WINDOWS_REASON : uint
        {
            SHTDN_REASON_MAJOR_APPLICATION = 0x00040000,
            SHTDN_REASON_MAJOR_HARDWARE = 0x00010000,
            SHTDN_REASON_MAJOR_LEGACY_API = 0x00070000,
            SHTDN_REASON_MAJOR_OPERATINGSYSTEM = 0x00020000,
            SHTDN_REASON_MAJOR_OTHER = 0x00000000,
            SHTDN_REASON_MAJOR_POWER = 0x00060000,
            SHTDN_REASON_MAJOR_SOFTWARE = 0x00030000,
            SHTDN_REASON_MAJOR_SYSTEM = 0x00050000,

            SHTDN_REASON_MINOR_BLUESCREEN = 0x0000000F,
            SHTDN_REASON_MINOR_CORDUNPLUGGED = 0x0000000b,
            SHTDN_REASON_MINOR_DISK = 0x00000007,
            SHTDN_REASON_MINOR_ENVIRONMENT = 0x0000000c,
            SHTDN_REASON_MINOR_HARDWARE_DRIVER = 0x0000000d,
            SHTDN_REASON_MINOR_HOTFIX = 0x00000011,
            SHTDN_REASON_MINOR_HOTFIX_UNINSTALL = 0x00000017,
            SHTDN_REASON_MINOR_HUNG = 0x00000005,
            SHTDN_REASON_MINOR_INSTALLATION = 0x00000002,
            SHTDN_REASON_MINOR_MAINTENANCE = 0x00000001,
            SHTDN_REASON_MINOR_MMC = 0x00000019,
            SHTDN_REASON_MINOR_NETWORK_CONNECTIVITY = 0x00000014,
            SHTDN_REASON_MINOR_NETWORKCARD = 0x00000009,
            SHTDN_REASON_MINOR_OTHER = 0x00000000,
            SHTDN_REASON_MINOR_OTHERDRIVER = 0x0000000e,
            SHTDN_REASON_MINOR_POWER_SUPPLY = 0x0000000a,
            SHTDN_REASON_MINOR_PROCESSOR = 0x00000008,
            SHTDN_REASON_MINOR_RECONFIG = 0x00000004,
            SHTDN_REASON_MINOR_SECURITY = 0x00000013,
            SHTDN_REASON_MINOR_SECURITYFIX = 0x00000012,
            SHTDN_REASON_MINOR_SECURITYFIX_UNINSTALL = 0x00000018,
            SHTDN_REASON_MINOR_SERVICEPACK = 0x00000010,
            SHTDN_REASON_MINOR_SERVICEPACK_UNINSTALL = 0x00000016,
            SHTDN_REASON_MINOR_TERMSRV = 0x00000020,
            SHTDN_REASON_MINOR_UNSTABLE = 0x00000006,
            SHTDN_REASON_MINOR_UPGRADE = 0x00000003,
            SHTDN_REASON_MINOR_WMI = 0x00000015,

            SHTDN_REASON_FLAG_USER_DEFINED = 0x40000000,
            SHTDN_REASON_FLAG_PLANNED = 0x80000000
        }

        [DllImport(user32)]
        internal static extern bool ExitWindowsEx(EXIT_WINDOWS_FLAGS uFlags, EXIT_WINDOWS_REASON dwReason);
        [DllImport(user32)]
        internal static extern bool LockWorkStation();
        #endregion

        #region "powrprof"
        private const string powrprof = "PowrProf.dll";
        [DllImport(powrprof)]
        internal static extern bool SetSuspendState(bool bHibernate, bool bForce, bool bWakeupEventsDisabled);
        #endregion
    }
}
