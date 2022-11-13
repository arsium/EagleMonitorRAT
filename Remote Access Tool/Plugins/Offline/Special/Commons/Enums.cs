using System;

/*
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Offline.Special
{
    internal static partial class Commons
    {
        #region "PE"
        internal enum MagicType : ushort
        {
            IMAGE_NT_OPTIONAL_HDR32_MAGIC = 0x10b,      //PE32
            IMAGE_NT_OPTIONAL_HDR64_MAGIC = 0x20b,      //PE32+
            IMAGE_ROM_OPTIONAL_HDR_MAGIC = 0x107
        }

        internal enum SubSystemType : ushort
        {
            IMAGE_SUBSYSTEM_UNKNOWN = 0,
            IMAGE_SUBSYSTEM_NATIVE = 1,
            IMAGE_SUBSYSTEM_WINDOWS_GUI = 2,
            IMAGE_SUBSYSTEM_WINDOWS_CUI = 3,
            IMAGE_SUBSYSTEM_OS2_CUI = 5,
            IMAGE_SUBSYSTEM_POSIX_CUI = 7,
            IMAGE_SUBSYSTEM_NATIVE_WINDOWS = 8,
            IMAGE_SUBSYSTEM_WINDOWS_CE_GUI = 9,
            IMAGE_SUBSYSTEM_EFI_APPLICATION = 10,
            IMAGE_SUBSYSTEM_EFI_BOOT_SERVICE_DRIVER = 11,
            IMAGE_SUBSYSTEM_EFI_RUNTIME_DRIVER = 12,
            IMAGE_SUBSYSTEM_EFI_ROM = 13,
            IMAGE_SUBSYSTEM_XBOX = 14,
            IMAGE_SUBSYSTEM_WINDOWS_BOOT_APPLICATION = 16,
            IMAGE_SUBSYSTEM_XBOX_CODE_CATALOG = 17
        }

        internal enum DllCharacteristics : ushort
        {
            IMAGE_LIBRARY_PROCESS_INIT = 0x0001,//RES_0
            IMAGE_LIBRARY_PROCESS_TERM = 0x0002,//RES_1
            IMAGE_LIBRARY_THREAD_INIT = 0x0004,//RES_2
            IMAGE_LIBRARY_THREAD_TERM = 0x0008,//RES_3
            IMAGE_DLLCHARACTERISTICS_HIGH_ENTROPY_VA = 0x0020,
            IMAGE_DLL_CHARACTERISTICS_DYNAMIC_BASE = 0x0040,
            IMAGE_DLL_CHARACTERISTICS_FORCE_INTEGRITY = 0x0080,
            IMAGE_DLL_CHARACTERISTICS_NX_COMPAT = 0x0100,
            IMAGE_DLLCHARACTERISTICS_NO_ISOLATION = 0x0200,
            IMAGE_DLLCHARACTERISTICS_NO_SEH = 0x0400,
            IMAGE_DLLCHARACTERISTICS_NO_BIND = 0x0800,
            IMAGE_DLLCHARACTERISTICS_APPCONTAINER = 0x1000,//RES_4
            IMAGE_DLLCHARACTERISTICS_WDM_DRIVER = 0x2000,
            IMAGE_DLLCHARACTERISTICS_GUARD_CF = 0x4000,
            IMAGE_DLLCHARACTERISTICS_TERMINAL_SERVER_AWARE = 0x8000
        }

        internal enum Characteristics : ushort
        {
            IMAGE_FILE_RELOCS_STRIPPED = 0x0001,
            IMAGE_FILE_EXECUTABLE_IMAGE = 0x0002,
            IMAGE_FILE_LINE_NUMS_STRIPPED = 0x0004,
            IMAGE_FILE_LOCAL_SYMS_STRIPPED = 0x0008,
            IMAGE_FILE_AGGRESIVE_WS_TRIM = 0x0010,
            IMAGE_FILE_LARGE_ADDRESS_AWARE = 0x0020,
            IMAGE_FILE_BYTES_REVERSED_LO = 0x0080,
            IMAGE_FILE_32BIT_MACHINE = 0x0100,
            IMAGE_FILE_DEBUG_STRIPPED = 0x0200,
            IMAGE_FILE_REMOVABLE_RUN_FROM_SWAP = 0x0400,
            IMAGE_FILE_NET_RUN_FROM_SWAP = 0x0800,
            IMAGE_FILE_SYSTEM = 0x1000,
            IMAGE_FILE_DLL = 0x2000,
            IMAGE_FILE_UP_SYSTEM_ONLY = 0x4000,
            IMAGE_FILE_BYTES_REVERSED_HI = 0x8000
        }

        internal enum Machine : ushort
        {
            IMAGE_FILE_MACHINE_UNKNOWN = 0,
            IMAGE_FILE_MACHINE_TARGET_HOST = 0x0001,  // Useful for indicating we want to interact with the host and not a WoW guest.
            IMAGE_FILE_MACHINE_I386 = 0x014c, // Intel 386.
            IMAGE_FILE_MACHINE_R3000 = 0x0162, // MIPS little-endian, =0x160 big-endian
            IMAGE_FILE_MACHINE_R4000 = 0x0166,// MIPS little-endian
            IMAGE_FILE_MACHINE_R10000 = 0x0168,// MIPS little-endian
            IMAGE_FILE_MACHINE_WCEMIPSV2 = 0x0169,// MIPS little-endian WCE v2
            IMAGE_FILE_MACHINE_ALPHA = 0x0184,// Alpha_AXP
            IMAGE_FILE_MACHINE_SH3 = 0x01a2,// SH3 little-endian
            IMAGE_FILE_MACHINE_SH3DSP = 0x01a3,
            IMAGE_FILE_MACHINE_SH3E = 0x01a4, // SH3E little-endian
            IMAGE_FILE_MACHINE_SH4 = 0x01a6, // SH4 little-endian
            IMAGE_FILE_MACHINE_SH5 = 0x01a8,// SH5
            IMAGE_FILE_MACHINE_ARM = 0x01c0,// ARM Little-Endian
            IMAGE_FILE_MACHINE_THUMB = 0x01c2,// ARM Thumb/Thumb-2 Little-Endian
            IMAGE_FILE_MACHINE_ARMNT = 0x01c4,// ARM Thumb-2 Little-Endian
            IMAGE_FILE_MACHINE_AM33 = 0x01d3,
            IMAGE_FILE_MACHINE_POWERPC = 0x01F0, // IBM PowerPC Little-Endian
            IMAGE_FILE_MACHINE_POWERPCFP = 0x01f1,
            IMAGE_FILE_MACHINE_IA64 = 0x0200, // Intel 64
            IMAGE_FILE_MACHINE_MIPS16 = 0x0266, // MIPS
            IMAGE_FILE_MACHINE_ALPHA64 = 0x0284,// ALPHA64
            IMAGE_FILE_MACHINE_MIPSFPU = 0x0366,// MIPS
            IMAGE_FILE_MACHINE_MIPSFPU16 = 0x0466,// MIPS
            IMAGE_FILE_MACHINE_AXP64 = IMAGE_FILE_MACHINE_ALPHA64,
            IMAGE_FILE_MACHINE_TRICORE = 0x0520,// Infineon
            IMAGE_FILE_MACHINE_CEF = 0x0CEF,
            IMAGE_FILE_MACHINE_EBC = 0x0EBC, // EFI Byte Code
            IMAGE_FILE_MACHINE_AMD64 = 0x8664,// AMD64 (K8)
            IMAGE_FILE_MACHINE_M32R = 0x9041,// M32R little-endian
            IMAGE_FILE_MACHINE_ARM64 = 0xAA64,// ARM64 Little-Endian
            IMAGE_FILE_MACHINE_CEE = 0xC0EE
        }
        #endregion
        #region "Process"
        internal enum ProcessInfoClass
        {
            ProcessBasicInformation = 0,
            ProcessQuotaLimits = 1,
            ProcessIoCounters = 2,
            ProcessVmCounters = 3,
            ProcessTimes = 4,
            ProcessBasePriority = 5,
            ProcessRaisePriority = 6,
            ProcessDebugPort = 7,
            ProcessExceptionPort = 8,
            ProcessAccessToken = 9,
            ProcessLdtInformation = 10,
            ProcessLdtSize = 11,
            ProcessDefaultHardErrorMode = 12,
            ProcessIoPortHandlers = 13,
            ProcessPooledUsageAndLimits = 14,
            ProcessWorkingSetWatch = 15,
            ProcessUserModeIOPL = 16,
            ProcessEnableAlignmentFaultFixup = 17,
            ProcessPriorityClass = 18,
            ProcessWx86Information = 19,
            ProcessHandleCount = 20,
            ProcessAffinityMask = 21,
            ProcessPriorityBoost = 22,
            ProcessDeviceMap = 23,
            ProcessSessionInformation = 24,
            ProcessForegroundInformation = 25,
            ProcessWow64Information = 26,
            ProcessImageFileName = 27,
            ProcessLUIDDeviceMapsEnabled = 28,
            ProcessBreakOnTermination = 29,
            ProcessDebugObjectHandle = 30,
            ProcessDebugFlags = 31,
            ProcessHandleTracing = 32,
            ProcessExecuteFlags = 34,
            ProcessTlsInformation = 35,
            ProcessCookie = 36,
            ProcessImageInformation = 37,
            ProcessCycleTime = 38,
            ProcessPagePriority = 39,
            ProcessInstrumentationCallback = 40,
            ProcessThreadStackAllocation = 41,
            ProcessWorkingSetWatchEx = 42,
            ProcessImageFileNameWin32 = 43,
            ProcessImageFileMapping = 44,
            ProcessAffinityUpdateMode = 45,
            ProcessMemoryAllocationMode = 46,
            ProcessGroupInformation = 47,
            ProcessTokenVirtualizationEnabled = 48,
            ProcessConsoleHostProcess = 49,
            ProcessWindowInformation = 50,
            MaxProcessInfoClass
        }

        internal enum NtStatus : uint
        {
            Success = 0,
            Informational = 0x40000000,
            Error = 0xc0000000
        }

        [Flags]
        internal enum PebFlags : byte
        {
            None = 0,
            ImageUsesLargePages = 0x01,
            IsProtectedProcess = 0x02,
            IsImageDynamicallyRelocated = 0x04,
            SkipPatchingUser32Forwarders = 0x08,
            IsPackagedProcess = 0x10,
            IsAppContainer = 0x20,
            IsProtectedProcessLight = 0x40,
            IsLongPathAwareProcess = 0x80,
        }

        internal enum SubSystemType4Bytes : uint
        {
            IMAGE_SUBSYSTEM_UNKNOWN = 0,
            IMAGE_SUBSYSTEM_NATIVE = 1,
            IMAGE_SUBSYSTEM_WINDOWS_GUI = 2,
            IMAGE_SUBSYSTEM_WINDOWS_CUI = 3,
            IMAGE_SUBSYSTEM_OS2_CUI = 5,
            IMAGE_SUBSYSTEM_POSIX_CUI = 7,
            IMAGE_SUBSYSTEM_NATIVE_WINDOWS = 8,
            IMAGE_SUBSYSTEM_WINDOWS_CE_GUI = 9,
            IMAGE_SUBSYSTEM_EFI_APPLICATION = 10,
            IMAGE_SUBSYSTEM_EFI_BOOT_SERVICE_DRIVER = 11,
            IMAGE_SUBSYSTEM_EFI_RUNTIME_DRIVER = 12,
            IMAGE_SUBSYSTEM_EFI_ROM = 13,
            IMAGE_SUBSYSTEM_XBOX = 14,
            IMAGE_SUBSYSTEM_WINDOWS_BOOT_APPLICATION = 16,
            IMAGE_SUBSYSTEM_XBOX_CODE_CATALOG = 17
        }

        [Flags]
        internal enum PageAccessType : uint
        {
            PAGE_NOACCESS = 0x01,
            PAGE_READONLY = 0x02,
            PAGE_READWRITE = 0x04,
            PAGE_WRITECOPY = 0x08,
            PAGE_EXECUTE = 0x10,
            PAGE_EXECUTE_READ = 0x20,
            PAGE_EXECUTE_READWRITE = 0x40,
            PAGE_EXECUTE_WRITECOPY = 0x80,
            PAGE_GUARD = 0x100,
            PAGE_NOCACHE = 0x200,
            PAGE_WRITECOMBINE = 0x400,
            PAGE_GRAPHICS_NOACCESS = 0x0800,
            PAGE_GRAPHICS_READONLY = 0x1000,
            PAGE_GRAPHICS_READWRITE = 0x2000,
            PAGE_GRAPHICS_EXECUTE = 0x4000,
            PAGE_GRAPHICS_EXECUTE_READ = 0x8000,
            PAGE_GRAPHICS_EXECUTE_READWRITE = 0x10000,
            PAGE_GRAPHICS_COHERENT = 0x20000,
            PAGE_GRAPHICS_NOCACHE = 0x40000,
            PAGE_ENCLAVE_THREAD_CONTROL = 0x80000000,
            PAGE_REVERT_TO_FILE_MAP = 0x80000000,
            PAGE_TARGETS_NO_UPDATE = 0x40000000,
            PAGE_TARGETS_INVALID = 0x40000000,
            PAGE_ENCLAVE_UNVALIDATED = 0x20000000,
            PAGE_ENCLAVE_MASK = 0x10000000,
            PAGE_ENCLAVE_DECOMMIT = (PAGE_ENCLAVE_MASK | 0),
            PAGE_ENCLAVE_SS_FIRST = (PAGE_ENCLAVE_MASK | 1),
            PAGE_ENCLAVE_SS_REST = (PAGE_ENCLAVE_MASK | 2)
        }

        [Flags]
        internal enum MemoryAllocationType : uint
        {
            MEM_COMMIT = 0x00001000,
            MEM_RESERVE = 0x00002000,
            MEM_REPLACE_PLACEHOLDER = 0x00004000,
            MEM_RESERVE_PLACEHOLDER = 0x00040000,
            MEM_RESET = 0x00080000,
            MEM_TOP_DOWN = 0x00100000,
            MEM_WRITE_WATCH = 0x00200000,
            MEM_PHYSICAL = 0x00400000,
            MEM_ROTATE = 0x00800000,
            MEM_DIFFERENT_IMAGE_BASE_OK = 0x00800000,
            MEM_RESET_UNDO = 0x01000000,
            MEM_LARGE_PAGES = 0x20000000,
            MEM_4MB_PAGES = 0x80000000,
            MEM_64K_PAGES = (MEM_LARGE_PAGES | MEM_PHYSICAL),
            MEM_UNMAP_WITH_TRANSIENT_BOOST = 0x00000001,
            MEM_COALESCE_PLACEHOLDERS = 0x00000001,
            MEM_PRESERVE_PLACEHOLDER = 0x00000002,
            MEM_DECOMMIT = 0x00004000,
            MEM_RELEASE = 0x00008000,
            MEM_FREE = 0x00010000
        }
        #endregion
        #region "COM Object"

        [Flags]
        internal enum CLSCTX
        {
            CLSCTX_INPROC_SERVER = 0x1,
            CLSCTX_INPROC_HANDLER = 0x2,
            CLSCTX_LOCAL_SERVER = 0x4,
            CLSCTX_REMOTE_SERVER = 0x10,
            CLSCTX_NO_CODE_DOWNLOAD = 0x400,
            CLSCTX_NO_CUSTOM_MARSHAL = 0x1000,
            CLSCTX_ENABLE_CODE_DOWNLOAD = 0x2000,
            CLSCTX_NO_FAILURE_LOG = 0x4000,
            CLSCTX_DISABLE_AAA = 0x8000,
            CLSCTX_ENABLE_AAA = 0x10000,
            CLSCTX_FROM_DEFAULT_CONTEXT = 0x20000,
            CLSCTX_INPROC = CLSCTX_INPROC_SERVER | CLSCTX_INPROC_HANDLER,
            CLSCTX_SERVER = CLSCTX_INPROC_SERVER | CLSCTX_LOCAL_SERVER | CLSCTX_REMOTE_SERVER,
            CLSCTX_ALL = CLSCTX_SERVER | CLSCTX_INPROC_HANDLER
        }
        #endregion     
    }
}
