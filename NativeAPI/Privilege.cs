using System.Runtime.InteropServices;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace NativeAPI
{
	public enum NTSTATUS : uint
	{
		STATUS_SUCCESS,
		STATUS_INFO_LENGTH_MISMATCH = 0xC0000004
	}

	public enum _PRIVILEGES : int
	{
		SeCreateTokenPrivilege = 1,
		SeAssignPrimaryTokenPrivilege,
		SeLockMemoryPrivilege,
		SeIncreaseQuotaPrivilege,
		SeUnsolicitedInputPrivilege,
		SeMachineAccountPrivilege,
		SeTcbPrivilege,
		SeSecurityPrivilege,
		SeTakeOwnershipPrivilege,
		SeLoadDriverPrivilege,
		SeSystemProfilePrivilege,
		SeSystemtimePrivilege,
		SeProfileSingleProcessPrivilege,
		SeIncreaseBasePriorityPrivilege,
		SeCreatePagefilePrivilege,
		SeCreatePermanentPrivilege,
		SeBackupPrivilege,
		SeRestorePrivilege,
		SeShutdownPrivilege,
		SeDebugPrivilege,
		SeAuditPrivilege,
		SeSystemEnvironmentPrivilege,
		SeChangeNotifyPrivilege,
		SeRemoteShutdownPrivilege,
		SeUndockPrivilege,
		SeSyncAgentPrivilege,
		SeEnableDelegationPrivilege,
		SeManageVolumePrivilege,
		SeImpersonatePrivilege,
		SeCreateGlobalPrivilege,
		SeTrustedCredManAccessPrivilege,
		SeRelabelPrivilege,
		SeIncreaseWorkingSetPrivilege,
		SeTimeZonePrivilege,
		SeCreateSymbolicLinkPrivilege
	}

	public static class STATUS
	{
		public static string ReturnNTSTATUS(this NTSTATUS n)
		{
			if (n != NTSTATUS.STATUS_SUCCESS)
			{
				return ("0x" + n.ToString("X"));
			}
			return n.ToString();
		}
	}

	public static class Functions 
	{
		[DllImport("ntdll.dll")]
		public static extern NTSTATUS RtlAdjustPrivilege(_PRIVILEGES Privilege, bool bEnablePrivilege, bool IsThreadPrivilege, out bool PreviousValue);
	} 
}
