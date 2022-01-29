using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NativeAPI
{
    public class Information
    {
        private static string FormatBytesRam(ulong kBytesToFormat)
        {

            string sToStringKb = new string(kBytesToFormat.ToString().Reverse().ToArray());

            string sFormatted = "";

            for (int i = 0; i < sToStringKb.Length - 1; i++)
            {
                if (sToStringKb.Length - i == 1)
                {
                    sFormatted += sToStringKb.Substring(i, 1); break;
                }

                if (sToStringKb.Length - i == 2)
                {
                    sFormatted += sToStringKb.Substring(i, 2); break;
                }

                if (i % 3 == 0)
                {
                    sFormatted += sToStringKb.Substring(i, 3) + ".";
                }
            }

           return new string(sFormatted.ToString().Reverse().ToArray());
        }
        public unsafe string GetRAM() 
        {
            ulong TotalRam;
            NativeImports.GetPhysicallyInstalledSystemMemory(&TotalRam);
            return FormatBytesRam(TotalRam);
		}


        public unsafe string GetSystemDirectoryPath()
        {
            uint uSize = 1;
            char[] cBuf = new char[uSize];
            uint uRealSize = 0;
            fixed (char* m = &cBuf[0])
            {
                uRealSize = NativeImports.GetSystemDirectoryW(m, uSize);
            }
            char[] cNewbuf = new char[uRealSize];
            fixed (char* m = &cNewbuf[0])
            {
                NativeImports.GetSystemDirectoryW(m, uRealSize);
            }
            return new string(cNewbuf);// -1 for eof or \0
        }

        public unsafe string GetSystemRoot() 
        {
            return Marshal.PtrToStringAuto((IntPtr)NativeImports.RtlGetNtSystemRoot());
        }

        public string GetFirmwareType() 
        {
            int nHandleInfoSize = 0x10000;
            IntPtr ipHandlePointer = Marshal.AllocHGlobal(nHandleInfoSize);
            int nLength = 0;
            NTSTATUS nTSTATUS;
            while ((nTSTATUS = NativeImports.NtQuerySystemInformation(NativeImports._SYSTEM_INFORMATION_CLASS.SystemBootEnvironmentInformation, ipHandlePointer, nHandleInfoSize, out nLength)) == NTSTATUS.STATUS_INFO_LENGTH_MISMATCH)
            {
                nHandleInfoSize = nLength;
                Marshal.FreeHGlobal(ipHandlePointer);
                ipHandlePointer = Marshal.AllocHGlobal(nLength);
            }

            if (nTSTATUS != NTSTATUS.STATUS_SUCCESS)
                return "Unknown";

            NativeImports._SYSTEM_BOOT_ENVIRONMENT_INFORMATION sYSTEM_BOOT_ENVIRONMENT_INFORMATION  = new NativeImports._SYSTEM_BOOT_ENVIRONMENT_INFORMATION();

            sYSTEM_BOOT_ENVIRONMENT_INFORMATION = (NativeImports._SYSTEM_BOOT_ENVIRONMENT_INFORMATION)Marshal.PtrToStructure(ipHandlePointer, sYSTEM_BOOT_ENVIRONMENT_INFORMATION.GetType());

            return sYSTEM_BOOT_ENVIRONMENT_INFORMATION.FirmwareType.ToString();
        }

        private static class NativeImports 
        {

            [DllImport("kernel32.dll", SetLastError = true)]
            public unsafe extern static bool GetPhysicallyInstalledSystemMemory(ulong* TotalMemoryInKilobytes);
            [DllImport("kernel32.dll", SetLastError = true)]
            public unsafe extern static uint GetSystemDirectoryW(char* lpBuffer, uint uSize);

            [DllImport("ntdll.dll", CharSet = CharSet.Unicode)]
            public unsafe static extern void* RtlGetNtSystemRoot();

            [DllImport("ntdll.dll", CharSet = CharSet.Unicode)]
            public static extern NTSTATUS NtQuerySystemInformation(NativeImports._SYSTEM_INFORMATION_CLASS SystemInformationClass, IntPtr SystemInformation, int SystemInformationLength, out int ReturnLength);

            [StructLayout(LayoutKind.Sequential)]
            public struct _SYSTEM_BOOT_ENVIRONMENT_INFORMATION
            {
                public Guid BootIdentifier;
                public _FIRMWARE_TYPE FirmwareType;
                public ulong BootFlags;
            }

            public enum _FIRMWARE_TYPE
            {
                Unknown,
                Bios,
                Uefi,
                Max
            }
            public enum _SYSTEM_INFORMATION_CLASS
            {
                SystemBasicInformation = 00,
                SystemProcessorInformation = 01,
                SystemPerformanceInformation = 02,
                SystemTimeOfDayInformation = 03,
                SystemPathInformation = 04,
                SystemProcessInformation = 05,
                SystemCallCountInformation = 06,
                SystemDeviceInformation = 07,
                SystemProcessorPerformanceInformation = 08,
                SystemFlagsInformation = 09,
                SystemCallTimeInformation = 010,
                SystemModuleInformation = 011,
                SystemLocksInformation = 012,
                SystemStackTraceInformation = 013,
                SystemPagedPoolInformation = 014,
                SystemNonPagedPoolInformation = 015,
                SystemHandleInformation = 016,
                SystemObjectInformation = 017,
                SystemPageFileInformation = 018,
                SystemVdmInstemulInformation = 019,
                SystemVdmBopInformation = 020,
                SystemFileCacheInformation = 021,
                SystemPoolTagInformation = 022,
                SystemInterruptInformation = 023,
                SystemDpcBehaviorInformation = 024,
                SystemFullMemoryInformation = 025,
                SystemLoadGdiDriverInformation = 026,
                SystemUnloadGdiDriverInformation = 027,
                SystemTimeAdjustmentInformation = 028,
                SystemSummaryMemoryInformation = 029,
                SystemMirrorMemoryInformation = 030,
                SystemPerformanceTraceInformation = 031,
                SystemObsolete0 = 032,
                SystemExceptionInformation = 033,
                SystemCrashDumpStateInformation = 034,
                SystemKernelDebuggerInformation = 035,
                SystemContextSwitchInformation = 036,
                SystemRegistryQuotaInformation = 037,
                SystemExtendServiceTableInformation = 038,
                SystemPrioritySeperation = 039,
                SystemVerifierAddDriverInformation = 040,
                SystemVerifierRemoveDriverInformation = 041,
                SystemProcessorIdleInformation = 042,
                SystemLegacyDriverInformation = 043,
                SystemCurrentTimeZoneInformation = 044,
                SystemLookasideInformation = 045,
                SystemTimeSlipNotification = 046,
                SystemSessionCreate = 047,
                SystemSessionDetach = 048,
                SystemSessionInformation = 049,
                SystemRangeStartInformation = 050,
                SystemVerifierInformation = 051,
                SystemVerifierThunkExtend = 052,
                SystemSessionProcessInformation = 053,
                SystemLoadGdiDriverInSystemSpace = 054,
                SystemNumaProcessorMap = 055,
                SystemPrefetcherInformation = 056,
                SystemExtendedProcessInformation = 057,
                SystemRecommendedSharedDataAlignment = 058,
                SystemComPlusPackage = 059,
                SystemNumaAvailableMemory = 060,
                SystemProcessorPowerInformation = 061,
                SystemEmulationBasicInformation = 062,
                SystemEmulationProcessorInformation = 063,
                SystemExtendedHandleInformation = 064,
                SystemLostDelayedWriteInformation = 065,
                SystemBigPoolInformation = 066,
                SystemSessionPoolTagInformation = 067,
                SystemSessionMappedViewInformation = 068,
                SystemHotpatchInformation = 069,
                SystemObjectSecurityMode = 070,
                SystemWatchdogTimerHandler = 071,
                SystemWatchdogTimerInformation = 072,
                SystemLogicalProcessorInformation = 073,
                SystemWow64SharedInformationObsolete = 074,
                SystemRegisterFirmwareTableInformationHandler = 075,
                SystemFirmwareTableInformation = 076,
                SystemModuleInformationEx = 077,
                SystemVerifierTriageInformation = 078,
                SystemSuperfetchInformation = 079,
                SystemMemoryListInformation = 080,
                SystemFileCacheInformationEx = 081,
                SystemThreadPriorityClientIdInformation = 082,
                SystemProcessorIdleCycleTimeInformation = 083,
                SystemVerifierCancellationInformation = 084,
                SystemProcessorPowerInformationEx = 085,
                SystemRefTraceInformation = 086,
                SystemSpecialPoolInformation = 087,
                SystemProcessIdInformation = 088,
                SystemErrorPortInformation = 089,
                SystemBootEnvironmentInformation = 090,
                SystemHypervisorInformation = 091,
                SystemVerifierInformationEx = 092,
                SystemTimeZoneInformation = 093,
                SystemImageFileExecutionOptionsInformation = 094,
                SystemCoverageInformation = 095,
                SystemPrefetchPatchInformation = 096,
                SystemVerifierFaultsInformation = 097,
                SystemSystemPartitionInformation = 098,
                SystemSystemDiskInformation = 099,
                SystemProcessorPerformanceDistribution = 0100,
                SystemNumaProximityNodeInformation = 0101,
                SystemDynamicTimeZoneInformation = 0102,
                SystemCodeIntegrityInformation = 0103,
                SystemProcessorMicrocodeUpdateInformation = 0104,
                SystemProcessorBrandString = 0105,
                SystemVirtualAddressInformation = 0106,
                SystemLogicalProcessorAndGroupInformation = 0107,
                SystemProcessorCycleTimeInformation = 0108,
                SystemStoreInformation = 0109,
                SystemRegistryAppendString = 0110,
                SystemAitSamplingValue = 0111,
                SystemVhdBootInformation = 0112,
                SystemCpuQuotaInformation = 0113,
                SystemNativeBasicInformation = 0114,
                SystemErrorPortTimeouts = 0115,
                SystemLowPriorityIoInformation = 0116,
                SystemBootEntropyInformation = 0117,
                SystemVerifierCountersInformation = 0118,
                SystemPagedPoolInformationEx = 0119,
                SystemSystemPtesInformationEx = 0120,
                SystemNodeDistanceInformation = 0121,
                SystemAcpiAuditInformation = 0122,
                SystemBasicPerformanceInformation = 0123,
                SystemQueryPerformanceCounterInformation = 0124,
                SystemSessionBigPoolInformation = 0125,
                SystemBootGraphicsInformation = 0126,
                SystemScrubPhysicalMemoryInformation = 0127,
                SystemBadPageInformation = 0128,
                SystemProcessorProfileControlArea = 0129,
                SystemCombinePhysicalMemoryInformation = 0130,
                SystemEntropyInterruptTimingInformation = 0131,
                SystemConsoleInformation = 0132,
                SystemPlatformBinaryInformation = 0133,
                SystemPolicyInformation = 0134,
                SystemHypervisorProcessorCountInformation = 0135,
                SystemDeviceDataInformation = 0136,
                SystemDeviceDataEnumerationInformation = 0137,
                SystemMemoryTopologyInformation = 0138,
                SystemMemoryChannelInformation = 0139,
                SystemBootLogoInformation = 0140,
                SystemProcessorPerformanceInformationEx = 0141,
                SystemCriticalProcessErrorLogInformation = 0142,
                SystemSecureBootPolicyInformation = 0143,
                SystemPageFileInformationEx = 0144,
                SystemSecureBootInformation = 0145,
                SystemEntropyInterruptTimingRawInformation = 0146,
                SystemPortableWorkspaceEfiLauncherInformation = 0147,
                SystemFullProcessInformation = 0148,
                SystemKernelDebuggerInformationEx = 0149,
                SystemBootMetadataInformation = 0150,
                SystemSoftRebootInformation = 0151,
                SystemElamCertificateInformation = 0152,
                SystemOfflineDumpConfigInformation = 0153,
                SystemProcessorFeaturesInformation = 0154,
                SystemRegistryReconciliationInformation = 0155,
                SystemEdidInformation = 0156,
                SystemManufacturingInformation = 0157,
                SystemEnergyEstimationConfigInformation = 0158,
                SystemHypervisorDetailInformation = 0159,
                SystemProcessorCycleStatsInformation = 0160,
                SystemVmGenerationCountInformation = 0161,
                SystemTrustedPlatformModuleInformation = 0162,
                SystemKernelDebuggerFlags = 0163,
                SystemCodeIntegrityPolicyInformation = 0164,
                SystemIsolatedUserModeInformation = 0165,
                SystemHardwareSecurityTestInterfaceResultsInformation = 0166,
                SystemSingleModuleInformation = 0167,
                SystemAllowedCpuSetsInformation = 0168,
                SystemVsmProtectionInformation = 0169,
                SystemInterruptCpuSetsInformation = 0170,
                SystemSecureBootPolicyFullInformation = 0171,
                SystemCodeIntegrityPolicyFullInformation = 0172,
                SystemAffinitizedInterruptProcessorInformation = 0173,
                SystemRootSiloInformation = 0174,
                SystemCpuSetInformation = 0175,
                SystemCpuSetTagInformation = 0176,
                SystemWin32WerStartCallout = 0177,
                SystemSecureKernelProfileInformation = 0178,
                SystemCodeIntegrityPlatformManifestInformation = 0179,
                SystemInterruptSteeringInformation = 0180,
                SystemSupportedProcessorArchitectures = 0181,
                SystemMemoryUsageInformation = 0182,
                SystemCodeIntegrityCertificateInformation = 0183,
                SystemPhysicalMemoryInformation = 0184,
                SystemControlFlowTransition = 0185,
                SystemKernelDebuggingAllowed = 0186,
                SystemActivityModerationExeState = 0187,
                SystemActivityModerationUserSettings = 0188,
                SystemCodeIntegrityPoliciesFullInformation = 0189,
                SystemCodeIntegrityUnlockInformation = 0190,
                SystemIntegrityQuotaInformation = 0191,
                SystemFlushInformation = 0192,
                SystemProcessorIdleMaskInformation = 0193,
                SystemSecureDumpEncryptionInformation = 0194,
                SystemWriteConstraintInformation = 0195,
                SystemKernelVaShadowInformation = 0196,
                SystemHypervisorSharedPageInformation = 0197,
                SystemFirmwareBootPerformanceInformation = 0198,
                SystemCodeIntegrityVerificationInformation = 0199,
                SystemFirmwarePartitionInformation = 0200,
                SystemSpeculationControlInformation = 0201,
                SystemDmaGuardPolicyInformation = 0202,
                SystemEnclaveLaunchControlInformation = 0203,
                SystemWorkloadAllowedCpuSetsInformation = 0204,
                SystemCodeIntegrityUnlockModeInformation = 0205,
                SystemLeapSecondInformation = 0206,
                SystemFlags2Information = 0207,
                SystemSecurityModelInformation = 0208,
                SystemCodeIntegritySyntheticCacheInformation = 0209,
                SystemFeatureConfigurationInformation = 0210,
                SystemFeatureConfigurationSectionInformation = 0211,
                SystemFeatureUsageSubscriptionInformation = 0212,
                SystemSecureSpeculationControlInformation = 0213,
                SystemSpacesBootInformation = 0214,
                SystemFwRamdiskInformation = 0215,
                SystemWheaIpmiHardwareInformation = 0216,
                SystemDifSetRuleClassInformation = 0217,
                SystemDifClearRuleClassInformation = 0218,
                SystemDifApplyPluginVerificationOnDriver = 0219,
                SystemDifRemovePluginVerificationOnDriver = 0220,
                SystemShadowStackInformation = 0221,
                SystemBuildVersionInformation = 0222,
                SystemPoolLimitInformation = 0223,
                SystemCodeIntegrityAddDynamicStore = 0224,
                SystemCodeIntegrityClearDynamicStores = 0225,
                SystemPoolZeroingInformation = 0227,
                MaxSystemInfoClass = 0228
            }
        }
    }
}
