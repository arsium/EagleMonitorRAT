using System;
using System.Runtime.InteropServices;

/*
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Offline.Special
{
    internal static partial class Commons
    {
        #region "PE"
        [StructLayout(LayoutKind.Sequential)]
        internal struct ImageDosHeader
        {
            public ushort e_magic;    // Magic number
            public ushort e_cblp;     // Bytes on last page of file
            public ushort e_cp;       // Pages in file
            public ushort e_crlc;     // Relocations
            public ushort e_cparhdr;  // Size of header in paragraphs
            public ushort e_minalloc; // Minimum extra paragraphs needed
            public ushort e_maxalloc; // Maximum extra paragraphs needed
            public ushort e_ss;       // Initial (relative) SS value
            public ushort e_sp;       // Initial SP value
            public ushort e_csum;     // Checksum
            public ushort e_ip;       // Initial IP value
            public ushort e_cs;       // Initial (relative) CS value
            public ushort e_lfarlc;   // File address of relocation table
            public ushort e_ovno;     // Overlay number
            public ushort e_res1a, e_res1b, e_res1c, e_res1d; // Reserved words //    WORD   e_res[4];
            public ushort e_oemid;    // OEM identifier (for e_oeminfo)
            public ushort e_oeminfo;  // OEM information; e_oemid specific
            public ushort e_res2a, e_res2b, e_res2c, e_res2d, e_res2e, e_res2f, e_res2g, e_res2h, e_res2i, e_res2j; // Reserved words     WORD   e_res2[10];  
            public int e_lfanew;      // File address of new exe header
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct ImageOptionalHeader32
        {
            public MagicType Magic;
            public byte MajorLinkerVersion;
            public byte MinorLinkerVersion;
            public uint SizeOfCode;
            public uint SizeOfInitializedData;
            public uint SizeOfUninitializedData;
            public uint AddressOfEntryPoint;
            public uint BaseOfCode;
            public uint BaseOfData;
            public uint ImageBaseLong;
            public uint SectionAlignment;
            public uint FileAlignment;
            public ushort MajorOperatingSystemVersion;
            public ushort MinorOperatingSystemVersion;
            public ushort MajorImageVersion;
            public ushort MinorImageVersion;
            public ushort MajorSubsystemVersion;
            public ushort MinorSubsystemVersion;
            public uint Win32VersionValue;
            public uint SizeOfImage;
            public uint SizeOfHeaders;
            public uint CheckSum;
            public SubSystemType Subsystem;
            public DllCharacteristics DllCharacteristics;
            public IntPtr SizeOfStackReserve;
            public IntPtr SizeOfStackCommit;
            public IntPtr SizeOfHeapReserve;
            public IntPtr SizeOfHeapCommit;
            public uint LoaderFlags;
            public uint NumberOfRvaAndSizes;
            public ImageDataDirectory ExportTable;//ImageExportDirectory
            public ImageDataDirectory ImportTable;
            public ImageDataDirectory ResourceTable;//ImageResourceDirectory
            public ImageDataDirectory ExceptionTable;
            public ImageDataDirectory CertificateTable;
            public ImageDataDirectory BaseRelocationTable;
            public ImageDataDirectory Debug;//ImageDebugDirectory
            public ImageDataDirectory Architecture;
            public ImageDataDirectory GlobalPtr;
            public ImageDataDirectory TLSTable;
            public ImageDataDirectory LoadConfigTable;
            public ImageDataDirectory BoundImport;
            public ImageDataDirectory IAT;
            public ImageDataDirectory DelayImportDescriptor;
            public ImageDataDirectory CLRRuntimeHeader;
            public ImageDataDirectory Reserved;
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct ImageDataDirectory
        {
            public uint VirtualAddress;
            public uint Size;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ImageOptionalHeader64
        {
            public MagicType Magic;
            public byte MajorLinkerVersion;
            public byte MinorLinkerVersion;
            public uint SizeOfCode;
            public uint SizeOfInitializedData;
            public uint SizeOfUninitializedData;
            public uint AddressOfEntryPoint;
            public uint BaseOfCode;
            public ulong ImageBaseLong;
            public uint SectionAlignment;
            public uint FileAlignment;
            public ushort MajorOperatingSystemVersion;
            public ushort MinorOperatingSystemVersion;
            public ushort MajorImageVersion;
            public ushort MinorImageVersion;
            public ushort MajorSubsystemVersion;
            public ushort MinorSubsystemVersion;
            public uint Win32VersionValue;
            public uint SizeOfImage;
            public uint SizeOfHeaders;
            public uint CheckSum;
            public SubSystemType Subsystem;
            public DllCharacteristics DllCharacteristics;
            public IntPtr SizeOfStackReserve;
            public IntPtr SizeOfStackCommit;
            public IntPtr SizeOfHeapReserve;
            public IntPtr SizeOfHeapCommit;
            public uint LoaderFlags;
            public uint NumberOfRvaAndSizes;
            public ImageDataDirectory ExportTable;//ImageExportDirectory
            public ImageDataDirectory ImportTable;
            public ImageDataDirectory ResourceTable;//ImageResourceDirectory
            public ImageDataDirectory ExceptionTable;
            public ImageDataDirectory CertificateTable;
            public ImageDataDirectory BaseRelocationTable;
            public ImageDataDirectory Debug;//ImageDebugDirectory
            public ImageDataDirectory Architecture;
            public ImageDataDirectory GlobalPtr;
            public ImageDataDirectory TLSTable;
            public ImageDataDirectory LoadConfigTable;
            public ImageDataDirectory BoundImport;
            public ImageDataDirectory IAT;
            public ImageDataDirectory DelayImportDescriptor;
            public ImageDataDirectory CLRRuntimeHeader;
            public ImageDataDirectory Reserved;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct ImageExportDirectory
        {
            [FieldOffset(0)]
            public int Characteristics;
            [FieldOffset(4)]
            public int TimeDateStamp;
            [FieldOffset(8)]
            public short MajorVersion;
            [FieldOffset(10)]
            public short MinorVersion;
            [FieldOffset(12)]
            public int NameRVA;
            [FieldOffset(16)]
            public int OrdinalBase;
            [FieldOffset(20)]
            public int NumberOfFunctions;      //Address TableEntries
            [FieldOffset(24)]
            public int NumberOfNames;          // Number of Name Pointers
            [FieldOffset(28)]
            public int AddressOfFunctions;     // RVA from base of image Export Address Table RVA 
            [FieldOffset(32)]
            public int AddressOfNames;         // RVA from base of image Name Pointer RVA
            [FieldOffset(36)]
            public int AddressOfNameOrdinals;  // RVA from base of image OrdinalTable RVA
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ImageFileHeader
        {
            public Machine Machine;
            public ushort NumberOfSections;
            public uint TimeDateStamp;
            public uint PointerToSymbolTable;
            public uint NumberOfSymbols;
            public ushort SizeOfOptionalHeader;
            public Characteristics Characteristics;
        }
        #endregion      
        #region "Process"
        internal static readonly IntPtr CurrentProc = (IntPtr)(-1);

        [StructLayout(LayoutKind.Sequential)]
        internal struct ProcessBasicInformation
        {
            public NtStatus ExitStatus;
            public IntPtr PebBaseAddress;
            public IntPtr AffinityMask;
            public int BasePriority;
            public IntPtr UniqueProcessId;
            public IntPtr InheritedFromUniqueProcessId;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal unsafe struct PEB32
        {
            [MarshalAs(UnmanagedType.U1)]
            public bool InheritedAddressSpace;
            [MarshalAs(UnmanagedType.U1)]
            public bool ReadImageFileExecOptions;
            [MarshalAs(UnmanagedType.U1)]
            public bool BeingDebugged;
            public PebFlags PebFlags;
            public int Mutant;
            public IntPtr ImageBaseAddress;
            public IntPtr Ldr; // PPEB_LDR_DATA
            public IntPtr ProcessParameters; // PRTL_USER_PROCESS_PARAMETERS
            public IntPtr SubSystemData;//int
            public IntPtr ProcessHeap;//int
            public IntPtr FastPebLock;//int -> PRTL_CRITICAL_SECTION

            public IntPtr FastPebLockRoutine;// -> PPEBLOCKROUTINE
            public IntPtr FastPebUnlockRoutine;
            public uint EnvironmentUpdateCount;
            public IntPtr KernelCallbackTable;
            public uint Reserved1;
            public uint Reserved2;
            public IntPtr FreeList;// -> PPEB_FREE_BLOCK
            public uint TlsExpansionCounter;
            public IntPtr TlsBitmap; // -> PRTL_BITMAP
            public uint TlsBitmapBits1;
            public uint TlsBitmapBits2;
            public IntPtr ReadOnlySharedMemoryBase;
            public IntPtr ReadOnlySharedMemoryHeap;
            public void** ReadOnlyStaticServerData;

            public IntPtr AnsiCodePageData;                  /* 058/0a0 */
            public IntPtr OemCodePageData;                   /* 05c/0a8 */
            public IntPtr UnicodeCaseTableData;              /* 060/0b0 */
            public uint NumberOfProcessors;
            public uint NtGlobalFlags;


            ///
            public LargeInteger CriticalSectionTimeout;            /* LARGE_INTEGER */
            public UIntPtr HeapSegmentReserve;                /* SIZE_T */
            public UIntPtr HeapSegmentCommit;                 /* SIZE_T */
            public UIntPtr HeapDeCommitTotalFreeThreshold;    /* SIZE_T */
            public UIntPtr HeapDeCommitFreeBlockThreshold;    /* SIZE_T */
            public uint NumberOfHeaps;                     /* 088/0e8 */
            public uint MaximumNumberOfHeaps;              /* 08c/0ec */
            public void** ProcessHeaps;                      /* PVOID* */
            IntPtr GdiSharedHandleTable;              /* PVOID */
            IntPtr ProcessStarterHelper;              /* PVOID */
            IntPtr GdiDCAttributeList;                /* PVOID */
            IntPtr LoaderLock;                        /* PVOID */
            public uint OSMajorVersion;                    /* ULONG */
            public uint OSMinorVersion;                    /* ULONG */
            public uint OSBuildNumber;                     /* ULONG */ //WORKS
            public uint OSPlatformId;                      /* ULONG */
            public SubSystemType4Bytes ImageSubSystem;                    /* ULONG */
            public uint ImageSubSystemMajorVersion;        /* ULONG */
            public uint ImageSubSystemMinorVersion;        /* ULONG */
            public uint ImageProcessAffinityMask;          /* ULONG */
        }

        [StructLayout(LayoutKind.Sequential)]
        internal unsafe struct PEB64
        {
            [MarshalAs(UnmanagedType.U1)]
            public bool InheritedAddressSpace;
            [MarshalAs(UnmanagedType.U1)]
            public bool ReadImageFileExecOptions;
            [MarshalAs(UnmanagedType.U1)]
            public bool BeingDebugged;
            public PebFlags PebFlags;
            public IntPtr Mutant;
            public IntPtr ImageBaseAddress;
            public IntPtr Ldr; // PPEB_LDR_DATA
            public IntPtr ProcessParameters; // PRTL_USER_PROCESS_PARAMETERS
            public IntPtr SubSystemData;
            public IntPtr ProcessHeap;
            public IntPtr FastPebLock;

            public IntPtr FastPebLockRoutine;// -> PPEBLOCKROUTINE
            public IntPtr FastPebUnlockRoutine;
            public uint EnvironmentUpdateCount;
            public IntPtr KernelCallbackTable;
            public uint Reserved1;
            public uint Reserved2;
            public IntPtr FreeList;// -> PPEB_FREE_BLOCK
            public uint TlsExpansionCounter;
            public IntPtr TlsBitmap; // -> PRTL_BITMAP
            public uint TlsBitmapBits1;
            public uint TlsBitmapBits2;
            public IntPtr ReadOnlySharedMemoryBase;
            public IntPtr ReadOnlySharedMemoryHeap;
            public void** ReadOnlyStaticServerData;

            public IntPtr AnsiCodePageData;                  /* 058/0a0 */
            public IntPtr OemCodePageData;                   /* 05c/0a8 */
            public IntPtr UnicodeCaseTableData;              /* 060/0b0 */
            public uint NumberOfProcessors;
            public uint NtGlobalFlags;


            ///
            public LargeInteger CriticalSectionTimeout;            /* LARGE_INTEGER */
            public UIntPtr HeapSegmentReserve;                /* SIZE_T */
            public UIntPtr HeapSegmentCommit;                 /* SIZE_T */
            public UIntPtr HeapDeCommitTotalFreeThreshold;    /* SIZE_T */
            public UIntPtr HeapDeCommitFreeBlockThreshold;    /* SIZE_T */
            public uint NumberOfHeaps;                     /* 088/0e8 */
            public uint MaximumNumberOfHeaps;              /* 08c/0ec */
            public void** ProcessHeaps;                      /* PVOID* */
            IntPtr GdiSharedHandleTable;              /* PVOID */
            IntPtr ProcessStarterHelper;              /* PVOID */
            IntPtr GdiDCAttributeList;                /* PVOID */
            IntPtr LoaderLock;                        /* PVOID */
            public uint OSMajorVersion;                    /* ULONG */
            public uint OSMinorVersion;                    /* ULONG */
            public uint OSBuildNumber;                     /* ULONG */ //WORKS
            public uint OSPlatformId;                      /* ULONG */
            public SubSystemType4Bytes ImageSubSystem;                    /* ULONG */
            public uint ImageSubSystemMajorVersion;        /* ULONG */
            public uint ImageSubSystemMinorVersion;        /* ULONG */
            public uint ImageProcessAffinityMask;          /* ULONG */

        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ListEntry
        {
            public IntPtr Flink;
            public IntPtr Blink;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct PebLdrData
        {
            public uint Length;
            public bool Initialized;
            public IntPtr SsHandle;
            public ListEntry InLoadOrderModuleList;
            public ListEntry InMemoryOrderModuleList;
            public ListEntry InInitializationOrderModuleList;
            public IntPtr EntryInProgress;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct LdrDataTableEntry
        {
            public ListEntry InLoadOrderLinks;
            public ListEntry InMemoryOrderLinks;
            public ListEntry InInitializationOrderLinks;
            public IntPtr DllBase; //ModuleBaseAddress()
            public IntPtr EntryPoint;
            public uint SizeOfImage;
            public UnicodeString FullDllName;
            public UnicodeString BaseDllName;
            public uint Flags;
            public ushort LoadCount;
            public ushort TlsIndex;
        }
        #endregion 
        #region "Misc"
        [StructLayout(LayoutKind.Sequential)]
        internal struct UnicodeString : IDisposable
        {
            public ushort Length;
            public ushort MaximumLength;
            private IntPtr buffer;

            public UnicodeString(string s)
            {
                Length = (ushort)(s.Length * 2);
                MaximumLength = (ushort)(Length + 2);
                buffer = Marshal.StringToHGlobalUni(s);
            }

            public void Dispose()
            {
                Marshal.FreeHGlobal(buffer);
                buffer = IntPtr.Zero;
            }

            public override string ToString()
            {
                return Marshal.PtrToStringUni(buffer);
            }
        }


        [StructLayout(LayoutKind.Explicit, Size = 8)]
        internal struct LargeInteger
        {
            [FieldOffset(0)] public long QuadPart;

            [FieldOffset(0)] public uint LowPart;
            [FieldOffset(4)] public int HighPart;

            [FieldOffset(0)] public int LowPartAsInt;
            [FieldOffset(0)] public uint LowPartAsUInt;

            [FieldOffset(4)] public int HighPartAsInt;
            [FieldOffset(4)] public uint HighPartAsUInt;
        }
        #endregion
        #region "Shutdown"
        internal enum HARDERROR_RESPONSE_OPTION : uint
        {
            OptionAbortRetryIgnore = 0,
            OptionOk = 1,
            OptionOkCancel = 2,
            OptionRetryCancel = 3,
            OptionYesNo = 4,
            OptionYesNoCancel = 5,
            OptionShutdownSystem = 6
        }

        internal enum HARDERROR_RESPONSE : uint
        {
            ResponseReturnToCaller = 0,
            ResponseNotHandled = 1,
            ResponseAbort = 2,
            ResponseCancel = 3,
            ResponseIgnore = 4,
            ResponseNo = 5,
            ResponseOk = 6,
            ResponseRetry = 7,
            ResponseYes = 8
        }

        internal enum RTL_PRIVILEGES : uint
        {
            SeCreateTokenPrivilege = 1,
            SeAssignPrimaryTokenPrivilege = 2,
            SeLockMemoryPrivilege = 3,
            SeIncreaseQuotaPrivilege = 4,
            SeUnsolicitedInputPrivilege = 5,
            SeMachineAccountPrivilege = 6,
            SeTcbPrivilege = 7,
            SeSecurityPrivilege = 8,
            SeTakeOwnershipPrivilege = 9,
            SeLoadDriverPrivilege = 10,
            SeSystemProfilePrivilege = 11,
            SeSystemtimePrivilege = 12,
            SeProfileSingleProcessPrivilege = 13,
            SeIncreaseBasePriorityPrivilege = 14,
            SeCreatePagefilePrivilege = 15,
            SeCreatePermanentPrivilege = 16,
            SeBackupPrivilege = 17,
            SeRestorePrivilege = 18,
            SeShutdownPrivilege = 19,
            SeDebugPrivilege = 20,
            SeAuditPrivilege = 21,
            SeSystemEnvironmentPrivilege = 22,
            SeChangeNotifyPrivilege = 23,
            SeRemoteShutdownPrivilege = 24,
            SeUndockPrivilege = 25,
            SeSyncAgentPrivilege = 26,
            SeEnableDelegationPrivilege = 27,
            SeManageVolumePrivilege = 28,
            SeImpersonatePrivilege = 29,
            SeCreateGlobalPrivilege = 30,
            SeTrustedCredManAccessPrivilege = 31,
            SeRelabelPrivilege = 32,
            SeIncreaseWorkingSetPrivilege = 33,
            SeTimeZonePrivilege = 34,
            SeCreateSymbolicLinkPrivilege = 35
        }
        #endregion
    }
}
