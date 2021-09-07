/*
 * Original Author : https://github.com/schellingb/DLLFromMemory-net 
 * 
 * DLLFromMemory.Net
 *
 * Load a native DLL from memory without the need to allow unsafe code
 *
 * Copyright (C) 2018 - 2019 by Bernhard Schelling
 *
 * Based on Memory Module.net 0.2
 * Copyright (C) 2012 - 2018 by Andreas Kanzler (andi_kanzler(at)gmx.de)
 * https://github.com/Scavanger/MemoryModule.net
 *
 * Based on Memory DLL loading code Version 0.0.4
 * Copyright (C) 2004 - 2015 by Joachim Bauch (mail(at)joachim-bauch.de)
 * https://github.com/fancycode/MemoryModule
 *
 *
 * The contents of this file are subject to the Mozilla Public License Version
 * 2.0 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS" basis,
 * WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
 * for the specific language governing rights and limitations under the
 * License.
 *
 * The Original Code is MemoryModule.c
 *
 * The Initial Developer of the Original Code is Joachim Bauch.
 *
 * Portions created by Joachim Bauch are Copyright (C) 2004 - 2015
 * Joachim Bauch. All Rights Reserved.
 *
 * Portions created by Andreas Kanzler are Copyright (C) 2012 - 2018
 * Andreas Kanzler. All Rights Reserved.
 *
 * Portions created by Bernhard Schelling are Copyright (C) 2018 - 2019
 * Bernhard Schelling. All Rights Reserved.
 *
 */

using System;
using System.Runtime.InteropServices;

public class DLLFromMemory : IDisposable
{
    public class DllException : Exception
    {
        public DllException() : base() { }
        public DllException(string message) : base(message) { }
        public DllException(string message, Exception innerException) : base(message, innerException) { }
    }

    public bool Disposed { get; private set; }
    public bool IsDll { get; private set; }

    IntPtr pCode = IntPtr.Zero;
    IntPtr pNTHeaders = IntPtr.Zero;
    IntPtr[] ImportModules;
    bool _initialized = false;
    DllEntryDelegate _dllEntry = null;
    ExeEntryDelegate _exeEntry = null;
    bool _isRelocated = false;

    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    delegate bool DllEntryDelegate(IntPtr hinstDLL, DllReason fdwReason, IntPtr lpReserved);

    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    delegate int ExeEntryDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    delegate void ImageTlsDelegate(IntPtr dllHandle, DllReason reason, IntPtr reserved);

    /// <summary>
    /// Loads a unmanged (native) DLL in the memory.
    /// </summary>
    /// <param name="data">Dll as a byte array</param>
    public DLLFromMemory(byte[] data)
    {
        Disposed = false;
        if (data == null) throw new ArgumentNullException("data");
        MemoryLoadLibrary(data);
    }

    ~DLLFromMemory()
    {
        Dispose();
    }

    /// <summary>
    /// Returns a delegate for a function inside the DLL.
    /// </summary>
    /// <typeparam name="TDelegate">The type of the delegate.</typeparam>
    /// <param name="funcName">The name of the function to be searched.</param>
    /// <returns>A delegate instance of type TDelegate</returns>
    public TDelegate GetDelegateFromFuncName<TDelegate>(string funcName) where TDelegate : class
    {
        if (!typeof(Delegate).IsAssignableFrom(typeof(TDelegate))) throw new ArgumentException(typeof(TDelegate).Name + " is not a delegate");
        TDelegate res = Marshal.GetDelegateForFunctionPointer((IntPtr)GetPtrFromFuncName(funcName), typeof(TDelegate)) as TDelegate;
        if (res == null) throw new DllException("Unable to get managed delegate");
        return res;
    }

    /// <summary>
    /// Returns a delegate for a function inside the DLL.
    /// </summary>
    /// <param name="funcName">The Name of the function to be searched.</param>
    /// <param name="delegateType">The type of the delegate to be returned.</param>
    /// <returns>A delegate instance that can be cast to the appropriate delegate type.</returns>
    public Delegate GetDelegateFromFuncName(string funcName, Type delegateType)
    {
        if (delegateType == null) throw new ArgumentNullException("delegateType");
        if (!typeof(Delegate).IsAssignableFrom(delegateType)) throw new ArgumentException(delegateType.Name + " is not a delegate");
        Delegate res = Marshal.GetDelegateForFunctionPointer(GetPtrFromFuncName(funcName), delegateType);
        if (res == null) throw new DllException("Unable to get managed delegate");
        return res;
    }

    IntPtr GetPtrFromFuncName(string funcName)
    {
        if (Disposed)                       throw new ObjectDisposedException("DLLFromMemory");
        if (string.IsNullOrEmpty(funcName)) throw new ArgumentException("funcName");
        if (!IsDll)                         throw new InvalidOperationException("Loaded Module is not a DLL");
        if (!_initialized)                  throw new InvalidOperationException("Dll is not initialized");

        IntPtr pDirectory = PtrAdd(pNTHeaders, Of.IMAGE_NT_HEADERS_OptionalHeader + (Is64BitProcess ? Of64.IMAGE_OPTIONAL_HEADER_ExportTable: Of32.IMAGE_OPTIONAL_HEADER_ExportTable));
        IMAGE_DATA_DIRECTORY Directory = PtrRead<IMAGE_DATA_DIRECTORY>(pDirectory);
        if (Directory.Size == 0) throw new DllException("Dll has no export table");

        IntPtr pExports = PtrAdd(pCode, Directory.VirtualAddress);
        IMAGE_EXPORT_DIRECTORY Exports = PtrRead<IMAGE_EXPORT_DIRECTORY>(pExports);
        if (Exports.NumberOfFunctions == 0 || Exports.NumberOfNames == 0) throw new DllException("Dll exports no functions");

        IntPtr pNameRef = PtrAdd(pCode, Exports.AddressOfNames);
        IntPtr pOrdinal = PtrAdd(pCode, Exports.AddressOfNameOrdinals);
        for (int i = 0; i < Exports.NumberOfNames; i++, pNameRef = PtrAdd(pNameRef, sizeof(uint)), pOrdinal = PtrAdd(pOrdinal, sizeof(ushort)))
        {
            uint NameRef = PtrRead<uint>(pNameRef);
            ushort Ordinal = PtrRead<ushort>(pOrdinal);
            string curFuncName = Marshal.PtrToStringAnsi(PtrAdd(pCode, NameRef));
            if (curFuncName == funcName)
            {
                if (Ordinal > Exports.NumberOfFunctions) throw new DllException("Invalid function ordinal");
                IntPtr pAddressOfFunction = PtrAdd(pCode, (Exports.AddressOfFunctions + (uint)(Ordinal * 4)));
                return PtrAdd(pCode, PtrRead<uint>(pAddressOfFunction));
            }
        }
            
        throw new DllException("Dll exports no function named " + funcName);
    }

    /// <summary>
    /// Call entry point of executable.
    /// </summary>
    /// <returns>Exitcode of executable</returns>
    public int MemoryCallEntryPoint()
    {
        if (Disposed) throw new ObjectDisposedException("DLLFromMemory");
        if (IsDll || _exeEntry == null || !_isRelocated) throw new DllException("Unable to call entry point. Is loaded module a dll?");
        return _exeEntry();
    }

    void MemoryLoadLibrary(byte[] data)
    {
        if (data.Length < Marshal.SizeOf(typeof(IMAGE_DOS_HEADER))) throw new DllException("Not a valid executable file");
        IMAGE_DOS_HEADER DosHeader = BytesReadStructAt<IMAGE_DOS_HEADER>(data, 0);
        if (DosHeader.e_magic != Win.IMAGE_DOS_SIGNATURE) throw new BadImageFormatException("Not a valid executable file");

        if (data.Length < DosHeader.e_lfanew + Marshal.SizeOf(typeof(IMAGE_NT_HEADERS))) throw new DllException("Not a valid executable file");
        IMAGE_NT_HEADERS OrgNTHeaders = BytesReadStructAt<IMAGE_NT_HEADERS>(data, DosHeader.e_lfanew);

        if (OrgNTHeaders.Signature != Win.IMAGE_NT_SIGNATURE)       throw new BadImageFormatException("Not a valid PE file");
        if (OrgNTHeaders.FileHeader.Machine != GetMachineType())    throw new BadImageFormatException("Machine type doesn't fit (i386 vs. AMD64)");
        if ((OrgNTHeaders.OptionalHeader.SectionAlignment & 1) > 0) throw new BadImageFormatException("Wrong section alignment"); //Only support multiple of 2
        if (OrgNTHeaders.OptionalHeader.AddressOfEntryPoint == 0)   throw new DllException("Module has no entry point");

        SYSTEM_INFO systemInfo;
        Win.GetNativeSystemInfo(out systemInfo);
        uint lastSectionEnd = 0;
        int ofSection = Win.IMAGE_FIRST_SECTION(DosHeader.e_lfanew, OrgNTHeaders.FileHeader.SizeOfOptionalHeader);
        for (int i = 0; i != OrgNTHeaders.FileHeader.NumberOfSections; i++, ofSection += Sz.IMAGE_SECTION_HEADER)
        {
            IMAGE_SECTION_HEADER Section = BytesReadStructAt<IMAGE_SECTION_HEADER>(data, ofSection);
            uint endOfSection = Section.VirtualAddress + (Section.SizeOfRawData > 0 ? Section.SizeOfRawData : OrgNTHeaders.OptionalHeader.SectionAlignment);
            if (endOfSection > lastSectionEnd) lastSectionEnd = endOfSection;
        }

        uint alignedImageSize   = AlignValueUp(OrgNTHeaders.OptionalHeader.SizeOfImage, systemInfo.dwPageSize);
        uint alignedLastSection = AlignValueUp(lastSectionEnd, systemInfo.dwPageSize);
        if (alignedImageSize != alignedLastSection) throw new BadImageFormatException("Wrong section alignment");

        IntPtr oldHeader_OptionalHeader_ImageBase;
        if (Is64BitProcess) oldHeader_OptionalHeader_ImageBase = (IntPtr)unchecked((long)(OrgNTHeaders.OptionalHeader.ImageBaseLong));
        else                oldHeader_OptionalHeader_ImageBase = (IntPtr)unchecked((int)(OrgNTHeaders.OptionalHeader.ImageBaseLong>>32));

        // reserve memory for image of library
        pCode = Win.VirtualAlloc(oldHeader_OptionalHeader_ImageBase, (UIntPtr)OrgNTHeaders.OptionalHeader.SizeOfImage, AllocationType.RESERVE | AllocationType.COMMIT, MemoryProtection.READWRITE);
        //pCode = IntPtr.Zero; //test relocation with this

        // try to allocate memory at arbitrary position
        if (pCode == IntPtr.Zero) pCode = Win.VirtualAlloc(IntPtr.Zero, (UIntPtr)OrgNTHeaders.OptionalHeader.SizeOfImage, AllocationType.RESERVE | AllocationType.COMMIT, MemoryProtection.READWRITE);

        if (pCode == IntPtr.Zero) throw new DllException("Out of Memory");

        if (Is64BitProcess && PtrSpanBoundary(pCode, alignedImageSize, 32))
        {
            // Memory block may not span 4 GB (32 bit) boundaries.
            System.Collections.Generic.List<IntPtr> BlockedMemory = new System.Collections.Generic.List<IntPtr>();
            while (PtrSpanBoundary(pCode, alignedImageSize, 32))
            {
                BlockedMemory.Add(pCode);
                pCode = Win.VirtualAlloc(IntPtr.Zero, (UIntPtr)alignedImageSize, AllocationType.RESERVE | AllocationType.COMMIT, MemoryProtection.READWRITE);
                if (pCode == IntPtr.Zero) break;
            }
            foreach (IntPtr ptr in BlockedMemory) Win.VirtualFree(ptr, IntPtr.Zero, AllocationType.RELEASE);
            if (pCode == IntPtr.Zero) throw new DllException("Out of Memory");
        }

        // commit memory for headers
        IntPtr headers = Win.VirtualAlloc(pCode, (UIntPtr)OrgNTHeaders.OptionalHeader.SizeOfHeaders, AllocationType.COMMIT, MemoryProtection.READWRITE);
        if (headers == IntPtr.Zero) throw new DllException("Out of Memory");

        // copy PE header to code
        Marshal.Copy(data, 0, headers, (int)(OrgNTHeaders.OptionalHeader.SizeOfHeaders));
        pNTHeaders = PtrAdd(headers, DosHeader.e_lfanew);

        IntPtr locationDelta = PtrSub(pCode, oldHeader_OptionalHeader_ImageBase);
        if (locationDelta != IntPtr.Zero)
        {
            // update relocated position
            Marshal.OffsetOf(typeof(IMAGE_NT_HEADERS), "OptionalHeader");
            Marshal.OffsetOf(typeof(IMAGE_OPTIONAL_HEADER), "ImageBaseLong");
            IntPtr pImageBase = PtrAdd(pNTHeaders, Of.IMAGE_NT_HEADERS_OptionalHeader + (Is64BitProcess ? Of64.IMAGE_OPTIONAL_HEADER_ImageBase : Of32.IMAGE_OPTIONAL_HEADER_ImageBase));
            PtrWrite(pImageBase, pCode);
        }

        // copy sections from DLL file block to new memory location
        CopySections(ref OrgNTHeaders, pCode, pNTHeaders, data);

        // adjust base address of imported data
        _isRelocated = (locationDelta != IntPtr.Zero ? PerformBaseRelocation(ref OrgNTHeaders, pCode, locationDelta) : true);

        // load required dlls and adjust function table of imports
        ImportModules = BuildImportTable(ref OrgNTHeaders, pCode);

        // mark memory pages depending on section headers and release
        // sections that are marked as "discardable"
        FinalizeSections(ref OrgNTHeaders, pCode, pNTHeaders, systemInfo.dwPageSize);

        // TLS callbacks are executed BEFORE the main loading
        ExecuteTLS(ref OrgNTHeaders, pCode, pNTHeaders);

        // get entry point of loaded library
        IsDll = ((OrgNTHeaders.FileHeader.Characteristics & Win.IMAGE_FILE_DLL) != 0);
        if (OrgNTHeaders.OptionalHeader.AddressOfEntryPoint != 0)
        {
            if (IsDll)
            {
                // notify library about attaching to process
                IntPtr dllEntryPtr = PtrAdd(pCode, OrgNTHeaders.OptionalHeader.AddressOfEntryPoint);
                _dllEntry = (DllEntryDelegate)Marshal.GetDelegateForFunctionPointer(dllEntryPtr, typeof(DllEntryDelegate));

                _initialized = (_dllEntry != null && _dllEntry(pCode, DllReason.DLL_PROCESS_ATTACH, IntPtr.Zero));
                if (!_initialized) throw new DllException("Can't attach DLL to process");
            }
            else
            {
                IntPtr exeEntryPtr = PtrAdd(pCode, OrgNTHeaders.OptionalHeader.AddressOfEntryPoint);
                _exeEntry = (ExeEntryDelegate)Marshal.GetDelegateForFunctionPointer(exeEntryPtr, typeof(ExeEntryDelegate));
            }
        }
    }

    static void CopySections(ref IMAGE_NT_HEADERS OrgNTHeaders, IntPtr pCode, IntPtr pNTHeaders, byte[] data)
    {
        IntPtr pSection = Win.IMAGE_FIRST_SECTION(pNTHeaders, OrgNTHeaders.FileHeader.SizeOfOptionalHeader);
        for (int i = 0; i < OrgNTHeaders.FileHeader.NumberOfSections; i++, pSection = PtrAdd(pSection, Sz.IMAGE_SECTION_HEADER))
        {
            IMAGE_SECTION_HEADER Section = PtrRead<IMAGE_SECTION_HEADER>(pSection);
            if (Section.SizeOfRawData == 0)
            {
                // section doesn't contain data in the dll itself, but may define uninitialized data
                uint size = OrgNTHeaders.OptionalHeader.SectionAlignment;
                if (size > 0)
                {
                    IntPtr dest = Win.VirtualAlloc(PtrAdd(pCode, Section.VirtualAddress), (UIntPtr)size, AllocationType.COMMIT, MemoryProtection.READWRITE);
                    if (dest == IntPtr.Zero) throw new DllException("Unable to allocate memory");
                
                    // Always use position from file to support alignments smaller than page size (allocation above will align to page size).
                    dest = PtrAdd(pCode, Section.VirtualAddress);
                
                    // NOTE: On 64bit systems we truncate to 32bit here but expand again later when "PhysicalAddress" is used.
                    PtrWrite(PtrAdd(pSection, Of.IMAGE_SECTION_HEADER_PhysicalAddress), unchecked((uint)(ulong)(long)dest));

                    Win.MemSet(dest, 0, (UIntPtr)size);
                }

                // section is empty
                continue;
            }
            else
            {
                // commit memory block and copy data from dll
                IntPtr dest = Win.VirtualAlloc(PtrAdd(pCode, Section.VirtualAddress), (UIntPtr)Section.SizeOfRawData, AllocationType.COMMIT, MemoryProtection.READWRITE);
                if (dest == IntPtr.Zero) throw new DllException("Out of memory");

                // Always use position from file to support alignments smaller than page size (allocation above will align to page size).
                dest = PtrAdd(pCode, Section.VirtualAddress);
                Marshal.Copy(data, checked((int)Section.PointerToRawData), dest, checked((int)Section.SizeOfRawData));

                // NOTE: On 64bit systems we truncate to 32bit here but expand again later when "PhysicalAddress" is used.
                PtrWrite(PtrAdd(pSection, Of.IMAGE_SECTION_HEADER_PhysicalAddress), unchecked((uint)(ulong)(long)dest));
            }
        }
    }

    static bool PerformBaseRelocation(ref IMAGE_NT_HEADERS OrgNTHeaders, IntPtr pCode, IntPtr delta)
    {
        if (OrgNTHeaders.OptionalHeader.BaseRelocationTable.Size == 0) return (delta == IntPtr.Zero);

        for (IntPtr pRelocation = PtrAdd(pCode, OrgNTHeaders.OptionalHeader.BaseRelocationTable.VirtualAddress);;)
        {
            IMAGE_BASE_RELOCATION Relocation = PtrRead<IMAGE_BASE_RELOCATION>(pRelocation);
            if (Relocation.VirtualAdress == 0) break;

            IntPtr pDest = PtrAdd(pCode, Relocation.VirtualAdress);
            IntPtr pRelInfo = PtrAdd(pRelocation, Sz.IMAGE_BASE_RELOCATION);
            uint RelCount = ((Relocation.SizeOfBlock - Sz.IMAGE_BASE_RELOCATION) / 2);
            for (uint i = 0; i != RelCount ; i++, pRelInfo = PtrAdd(pRelInfo, sizeof(ushort)))
            {
                ushort relInfo = (ushort)Marshal.PtrToStructure(pRelInfo, typeof(ushort));
                BasedRelocationType type = (BasedRelocationType)(relInfo >> 12); // the upper 4 bits define the type of relocation
                int offset = (relInfo & 0xfff); // the lower 12 bits define the offset
                IntPtr pPatchAddr = PtrAdd(pDest, offset);

                switch (type)
                {
                    case BasedRelocationType.IMAGE_REL_BASED_ABSOLUTE:
                        // skip relocation
                        break;
                    case BasedRelocationType.IMAGE_REL_BASED_HIGHLOW:
                        // change complete 32 bit address
                        int patchAddrHL = (int)Marshal.PtrToStructure(pPatchAddr, typeof(int));
                        patchAddrHL += (int)delta;
                        Marshal.StructureToPtr(patchAddrHL, pPatchAddr, false);
                        break;
                    case BasedRelocationType.IMAGE_REL_BASED_DIR64:
                        long patchAddr64 = (long)Marshal.PtrToStructure(pPatchAddr, typeof(long));
                        patchAddr64 += (long)delta;
                        Marshal.StructureToPtr(patchAddr64, pPatchAddr, false);
                        break;
                }
            }

            // advance to next relocation block
            pRelocation = PtrAdd(pRelocation, Relocation.SizeOfBlock);
        }
        return true;
    }

    static IntPtr[] BuildImportTable(ref IMAGE_NT_HEADERS OrgNTHeaders, IntPtr pCode)
    {
        System.Collections.Generic.List<IntPtr> ImportModules = new System.Collections.Generic.List<IntPtr>();
        uint NumEntries = OrgNTHeaders.OptionalHeader.ImportTable.Size / Sz.IMAGE_IMPORT_DESCRIPTOR;
        IntPtr pImportDesc = PtrAdd(pCode, OrgNTHeaders.OptionalHeader.ImportTable.VirtualAddress);
        for (uint i = 0; i != NumEntries; i++, pImportDesc = PtrAdd(pImportDesc, Sz.IMAGE_IMPORT_DESCRIPTOR))
        {
            IMAGE_IMPORT_DESCRIPTOR ImportDesc = PtrRead<IMAGE_IMPORT_DESCRIPTOR>(pImportDesc);
            if (ImportDesc.Name == 0) break;

            IntPtr handle = Win.LoadLibrary(PtrAdd(pCode, ImportDesc.Name));
            if (PtrIsInvalidHandle(handle))
            {
                foreach (IntPtr m in ImportModules) Win.FreeLibrary(m);
                ImportModules.Clear();
                throw new DllException("Can't load libary " + Marshal.PtrToStringAnsi(PtrAdd(pCode, ImportDesc.Name)));
            }
            ImportModules.Add(handle);

            IntPtr pThunkRef, pFuncRef;
            if (ImportDesc.OriginalFirstThunk > 0)
            {
                pThunkRef = PtrAdd(pCode, ImportDesc.OriginalFirstThunk);
                pFuncRef = PtrAdd(pCode, ImportDesc.FirstThunk);
            }
            else
            {
                // no hint table
                pThunkRef = PtrAdd(pCode, ImportDesc.FirstThunk);
                pFuncRef = PtrAdd(pCode, ImportDesc.FirstThunk);
            }
            for (int SzRef = IntPtr.Size; ; pThunkRef = PtrAdd(pThunkRef, SzRef), pFuncRef = PtrAdd(pFuncRef, SzRef))
            {
                IntPtr ReadThunkRef = PtrRead<IntPtr>(pThunkRef), WriteFuncRef;
                if (ReadThunkRef == IntPtr.Zero) break;
                if (Win.IMAGE_SNAP_BY_ORDINAL(ReadThunkRef))
                {
                    WriteFuncRef = Win.GetProcAddress(handle, Win.IMAGE_ORDINAL(ReadThunkRef));
                }
                else
                {
                    WriteFuncRef = Win.GetProcAddress(handle, PtrAdd(PtrAdd(pCode, ReadThunkRef), Of.IMAGE_IMPORT_BY_NAME_Name));
                }
                if (WriteFuncRef == IntPtr.Zero) throw new DllException("Can't get adress for imported function");
                PtrWrite(pFuncRef, WriteFuncRef);
            }
        }
        return (ImportModules.Count > 0 ? ImportModules.ToArray() : null);
    }

    static void FinalizeSections(ref IMAGE_NT_HEADERS OrgNTHeaders, IntPtr pCode, IntPtr pNTHeaders, uint PageSize)
    {
        UIntPtr imageOffset = (Is64BitProcess ? (UIntPtr)(unchecked((ulong)pCode.ToInt64()) & 0xffffffff00000000) : UIntPtr.Zero);
        IntPtr pSection = Win.IMAGE_FIRST_SECTION(pNTHeaders, OrgNTHeaders.FileHeader.SizeOfOptionalHeader);
        IMAGE_SECTION_HEADER Section = PtrRead<IMAGE_SECTION_HEADER>(pSection);
        SectionFinalizeData sectionData = new SectionFinalizeData();
        sectionData.Address = PtrBitOr(PtrAdd((IntPtr)0, Section.PhysicalAddress), imageOffset);
        sectionData.AlignedAddress = PtrAlignDown(sectionData.Address, (UIntPtr)PageSize);
        sectionData.Size = GetRealSectionSize(ref Section, ref OrgNTHeaders);
        sectionData.Characteristics = Section.Characteristics;
        sectionData.Last = false;
        pSection = PtrAdd(pSection, Sz.IMAGE_SECTION_HEADER);

        // loop through all sections and change access flags
        for (int i = 1; i < OrgNTHeaders.FileHeader.NumberOfSections; i++, pSection = PtrAdd(pSection, Sz.IMAGE_SECTION_HEADER))
        {
            Section = PtrRead<IMAGE_SECTION_HEADER>(pSection);
            IntPtr sectionAddress = PtrBitOr(PtrAdd((IntPtr)0, Section.PhysicalAddress), imageOffset);
            IntPtr alignedAddress = PtrAlignDown(sectionAddress, (UIntPtr)PageSize);
            IntPtr sectionSize = GetRealSectionSize(ref Section, ref OrgNTHeaders);

            // Combine access flags of all sections that share a page
            // TODO(fancycode): We currently share flags of a trailing large section with the page of a first small section. This should be optimized.
            IntPtr a = PtrAdd(sectionData.Address, sectionData.Size);
            ulong b = unchecked((ulong)a.ToInt64()), c = unchecked((ulong)alignedAddress);

            if (sectionData.AlignedAddress == alignedAddress || unchecked((ulong)PtrAdd(sectionData.Address, sectionData.Size).ToInt64()) > unchecked((ulong)alignedAddress))
            {
                // Section shares page with previous
                if ((Section.Characteristics & Win.IMAGE_SCN_MEM_DISCARDABLE) == 0 || (sectionData.Characteristics & Win.IMAGE_SCN_MEM_DISCARDABLE) == 0)
                {
                    sectionData.Characteristics = (sectionData.Characteristics | Section.Characteristics) & ~Win.IMAGE_SCN_MEM_DISCARDABLE;
                }
                else
                {
                    sectionData.Characteristics |= Section.Characteristics;
                }
                sectionData.Size = PtrSub(PtrAdd(sectionAddress, sectionSize), sectionData.Address);
                continue;
            }

            FinalizeSection(sectionData, PageSize, OrgNTHeaders.OptionalHeader.SectionAlignment);

            sectionData.Address = sectionAddress;
            sectionData.AlignedAddress = alignedAddress;
            sectionData.Size = sectionSize;
            sectionData.Characteristics = Section.Characteristics;
        }
        sectionData.Last = true;
        FinalizeSection(sectionData, PageSize, OrgNTHeaders.OptionalHeader.SectionAlignment);
    }

    static void FinalizeSection(SectionFinalizeData SectionData, uint PageSize, uint SectionAlignment)
    {
        if (SectionData.Size == IntPtr.Zero)
            return;

        if ((SectionData.Characteristics & Win.IMAGE_SCN_MEM_DISCARDABLE) > 0)
        {
            // section is not needed any more and can safely be freed
            if (SectionData.Address == SectionData.AlignedAddress &&
                (SectionData.Last ||
                    SectionAlignment == PageSize ||
                    (unchecked((ulong)SectionData.Size.ToInt64()) % PageSize) == 0)
                )
            {
                // Only allowed to decommit whole pages
                Win.VirtualFree(SectionData.Address, SectionData.Size, AllocationType.DECOMMIT);
            }
            return;
        }

        // determine protection flags based on characteristics
        int readable   = (SectionData.Characteristics & (uint)ImageSectionFlags.IMAGE_SCN_MEM_READ)    != 0 ? 1 : 0;
        int writeable  = (SectionData.Characteristics & (uint)ImageSectionFlags.IMAGE_SCN_MEM_WRITE)   != 0 ? 1 : 0;
        int executable = (SectionData.Characteristics & (uint)ImageSectionFlags.IMAGE_SCN_MEM_EXECUTE) != 0 ? 1 : 0;
        uint protect = (uint)ProtectionFlags[executable, readable, writeable];
        if ((SectionData.Characteristics & Win.IMAGE_SCN_MEM_NOT_CACHED) > 0) protect |= Win.PAGE_NOCACHE;

        // change memory access flags
        uint oldProtect;
        if (!Win.VirtualProtect(SectionData.Address, SectionData.Size, protect, out oldProtect))
            throw new DllException("Error protecting memory page");
    }

    static void ExecuteTLS(ref IMAGE_NT_HEADERS OrgNTHeaders, IntPtr pCode, IntPtr pNTHeaders)
    {
        if (OrgNTHeaders.OptionalHeader.TLSTable.VirtualAddress == 0) return;
        IMAGE_TLS_DIRECTORY tlsDir = PtrRead<IMAGE_TLS_DIRECTORY>(PtrAdd(pCode, OrgNTHeaders.OptionalHeader.TLSTable.VirtualAddress));
        IntPtr pCallBack = tlsDir.AddressOfCallBacks;
        if (pCallBack != IntPtr.Zero)
        {
            for (IntPtr Callback; (Callback = PtrRead<IntPtr>(pCallBack)) != IntPtr.Zero; pCallBack = PtrAdd(pCallBack, IntPtr.Size))
            {
                ImageTlsDelegate tls = (ImageTlsDelegate)Marshal.GetDelegateForFunctionPointer(Callback, typeof(ImageTlsDelegate));
                tls(pCode, DllReason.DLL_PROCESS_ATTACH, IntPtr.Zero);
            }
        }
    }

    /// <summary>
    /// Check if the process runs in 64bit mode or in 32bit mode
    /// </summary>
    /// <returns>True if process is 64bit, false if it is 32bit</returns>
    public static bool Is64BitProcess { get { return IntPtr.Size == 8; } }

    static uint GetMachineType() { return (IntPtr.Size == 8 ? Win.IMAGE_FILE_MACHINE_AMD64 : Win.IMAGE_FILE_MACHINE_I386); }

    static uint AlignValueUp(uint value, uint alignment) { return (value + alignment - 1) & ~(alignment - 1); }

    static IntPtr GetRealSectionSize(ref IMAGE_SECTION_HEADER Section, ref IMAGE_NT_HEADERS NTHeaders)
    {
        uint size = Section.SizeOfRawData;
        if (size == 0)
        {
            if ((Section.Characteristics & Win.IMAGE_SCN_CNT_INITIALIZED_DATA) > 0)
            {
                size = NTHeaders.OptionalHeader.SizeOfInitializedData;
            }
            else if ((Section.Characteristics & Win.IMAGE_SCN_CNT_UNINITIALIZED_DATA) > 0)
            {
                size = NTHeaders.OptionalHeader.SizeOfUninitializedData;
            }
        }
        return (IntPtr.Size == 8 ? (IntPtr)unchecked((long)size) : (IntPtr)unchecked((int)size));
    }

    public void Close() { ((IDisposable)this).Dispose(); }

    void IDisposable.Dispose()
    {
        Dispose();
        GC.SuppressFinalize(this);
    }

    public void Dispose()
    {
        if (_initialized)
        {
            if (_dllEntry != null) _dllEntry.Invoke(pCode, DllReason.DLL_PROCESS_DETACH, IntPtr.Zero);
            _initialized = false;
        }

        if (ImportModules != null)
        {
            foreach (IntPtr m in ImportModules) if (!PtrIsInvalidHandle(m)) Win.FreeLibrary(m);
            ImportModules = null;
        }

        if (pCode != IntPtr.Zero)
        { 
            Win.VirtualFree(pCode, IntPtr.Zero, AllocationType.RELEASE);
            pCode = IntPtr.Zero;
            pNTHeaders = IntPtr.Zero;
        }

        Disposed = true;
    }

    // Protection flags for memory pages (Executable, Readable, Writeable)
    static readonly PageProtection[,,] ProtectionFlags = new PageProtection[2,2,2]
    {
        {
            // not executable
            { PageProtection.NOACCESS, PageProtection.WRITECOPY },
            { PageProtection.READONLY, PageProtection.READWRITE }
        },
        {
            // executable
            { PageProtection.EXECUTE, PageProtection.EXECUTE_WRITECOPY },
            { PageProtection.EXECUTE_READ, PageProtection.EXECUTE_READWRITE }
        }
    };

    struct SectionFinalizeData
    {
        internal IntPtr Address;
        internal IntPtr AlignedAddress;
        internal IntPtr Size;
        internal uint Characteristics;
        internal bool Last;
    }

    class Of
    {
        internal const int IMAGE_NT_HEADERS_OptionalHeader = 24;
        internal const int IMAGE_SECTION_HEADER_PhysicalAddress = 8;
        internal const int IMAGE_IMPORT_BY_NAME_Name = 2;
    }

    class Of32
    {
        internal const int IMAGE_OPTIONAL_HEADER_ImageBase = 28;
        internal const int IMAGE_OPTIONAL_HEADER_ExportTable = 96;
    }

    class Of64
    {
        internal const int IMAGE_OPTIONAL_HEADER_ImageBase = 24;
        internal const int IMAGE_OPTIONAL_HEADER_ExportTable = 112;
    }

    class Sz
    {
        internal const int IMAGE_SECTION_HEADER = 40;
        internal const int IMAGE_BASE_RELOCATION = 8;
        internal const int IMAGE_IMPORT_DESCRIPTOR = 20;
    }

    [StructLayout(LayoutKind.Sequential)] struct IMAGE_DOS_HEADER
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
        public ushort e_res1a,e_res1b,e_res1c,e_res1d; // Reserved words
        public ushort e_oemid;    // OEM identifier (for e_oeminfo)
        public ushort e_oeminfo;  // OEM information; e_oemid specific
        public ushort e_res2a,e_res2b,e_res2c,e_res2d,e_res2e,e_res2f,e_res2g,e_res2h,e_res2i,e_res2j; // Reserved words
        public int e_lfanew;      // File address of new exe header
    }

    [StructLayout(LayoutKind.Sequential)] struct IMAGE_NT_HEADERS
    {
        public uint Signature;
        public IMAGE_FILE_HEADER FileHeader;
        public IMAGE_OPTIONAL_HEADER OptionalHeader;
    }

    [StructLayout(LayoutKind.Sequential)] struct IMAGE_FILE_HEADER
    {
        public ushort Machine;
        public ushort NumberOfSections;
        public uint TimeDateStamp;
        public uint PointerToSymbolTable;
        public uint NumberOfSymbols;
        public ushort SizeOfOptionalHeader;
        public ushort Characteristics;
    }

    [StructLayout(LayoutKind.Sequential)] struct IMAGE_OPTIONAL_HEADER
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
        public DllCharacteristicsType DllCharacteristics;
        public IntPtr SizeOfStackReserve;
        public IntPtr SizeOfStackCommit;
        public IntPtr SizeOfHeapReserve;
        public IntPtr SizeOfHeapCommit;
        public uint LoaderFlags;
        public uint NumberOfRvaAndSizes;
        public IMAGE_DATA_DIRECTORY ExportTable;
        public IMAGE_DATA_DIRECTORY ImportTable;
        public IMAGE_DATA_DIRECTORY ResourceTable;
        public IMAGE_DATA_DIRECTORY ExceptionTable;
        public IMAGE_DATA_DIRECTORY CertificateTable;
        public IMAGE_DATA_DIRECTORY BaseRelocationTable;
        public IMAGE_DATA_DIRECTORY Debug;
        public IMAGE_DATA_DIRECTORY Architecture;
        public IMAGE_DATA_DIRECTORY GlobalPtr;
        public IMAGE_DATA_DIRECTORY TLSTable;
        public IMAGE_DATA_DIRECTORY LoadConfigTable;
        public IMAGE_DATA_DIRECTORY BoundImport;
        public IMAGE_DATA_DIRECTORY IAT;
        public IMAGE_DATA_DIRECTORY DelayImportDescriptor;
        public IMAGE_DATA_DIRECTORY CLRRuntimeHeader;
        public IMAGE_DATA_DIRECTORY Reserved;
    }

    [StructLayout(LayoutKind.Sequential)] struct IMAGE_DATA_DIRECTORY
    {
        public uint VirtualAddress;
        public uint Size;
    }

    [StructLayout(LayoutKind.Sequential)] struct IMAGE_SECTION_HEADER
    {
        public ulong Name; //8 byte string
        public uint PhysicalAddress;
        public uint VirtualAddress;
        public uint SizeOfRawData;
        public uint PointerToRawData;
        public uint PointerToRelocations;
        public uint PointerToLinenumbers;
        public ushort NumberOfRelocations;
        public ushort NumberOfLinenumbers;
        public uint Characteristics;
    }

    [StructLayout(LayoutKind.Sequential)] struct IMAGE_BASE_RELOCATION
    {
        public uint VirtualAdress;
        public uint SizeOfBlock;
    }

    [StructLayout(LayoutKind.Sequential)] struct IMAGE_IMPORT_DESCRIPTOR
    {
        public uint OriginalFirstThunk;
        public uint TimeDateStamp;
        public uint ForwarderChain;
        public uint Name;
        public uint FirstThunk;
    }

    [StructLayout(LayoutKind.Sequential)] struct IMAGE_EXPORT_DIRECTORY
    {
        public uint Characteristics;
        public uint TimeDateStamp;
        public ushort MajorVersion;
        public ushort MinorVersion;
        public uint Name;
        public uint Base;
        public uint NumberOfFunctions;
        public uint NumberOfNames;
        public uint AddressOfFunctions;     // RVA from base of image
        public uint AddressOfNames;         // RVA from base of image
        public uint AddressOfNameOrdinals;  // RVA from base of image
    }

    [StructLayout(LayoutKind.Sequential)] struct SYSTEM_INFO
    {
        public ushort wProcessorArchitecture;
        public ushort wReserved;
        public uint dwPageSize;
        public IntPtr lpMinimumApplicationAddress;
        public IntPtr lpMaximumApplicationAddress;
        public IntPtr dwActiveProcessorMask;
        public uint dwNumberOfProcessors;
        public uint dwProcessorType;
        public uint dwAllocationGranularity;
        public ushort wProcessorLevel;
        public ushort wProcessorRevision;
    };

    [StructLayout(LayoutKind.Sequential)] struct IMAGE_TLS_DIRECTORY
    {
        public IntPtr StartAddressOfRawData;
        public IntPtr EndAddressOfRawData;
        public IntPtr AddressOfIndex;
        public IntPtr AddressOfCallBacks;
        public IntPtr SizeOfZeroFill;
        public uint Characteristics;
    }

    enum MagicType : ushort
    {
        IMAGE_NT_OPTIONAL_HDR32_MAGIC = 0x10b,
        IMAGE_NT_OPTIONAL_HDR64_MAGIC = 0x20b
    }

    enum SubSystemType : ushort
    {
        IMAGE_SUBSYSTEM_UNKNOWN = 0,
        IMAGE_SUBSYSTEM_NATIVE = 1,
        IMAGE_SUBSYSTEM_WINDOWS_GUI = 2,
        IMAGE_SUBSYSTEM_WINDOWS_CUI = 3,
        IMAGE_SUBSYSTEM_POSIX_CUI = 7,
        IMAGE_SUBSYSTEM_WINDOWS_CE_GUI = 9,
        IMAGE_SUBSYSTEM_EFI_APPLICATION = 10,
        IMAGE_SUBSYSTEM_EFI_BOOT_SERVICE_DRIVER = 11,
        IMAGE_SUBSYSTEM_EFI_RUNTIME_DRIVER = 12,
        IMAGE_SUBSYSTEM_EFI_ROM = 13,
        IMAGE_SUBSYSTEM_XBOX = 14
    }

    enum DllCharacteristicsType : ushort
    {
        RES_0 = 0x0001,
        RES_1 = 0x0002,
        RES_2 = 0x0004,
        RES_3 = 0x0008,
        IMAGE_DLL_CHARACTERISTICS_DYNAMIC_BASE = 0x0040,
        IMAGE_DLL_CHARACTERISTICS_FORCE_INTEGRITY = 0x0080,
        IMAGE_DLL_CHARACTERISTICS_NX_COMPAT = 0x0100,
        IMAGE_DLLCHARACTERISTICS_NO_ISOLATION = 0x0200,
        IMAGE_DLLCHARACTERISTICS_NO_SEH = 0x0400,
        IMAGE_DLLCHARACTERISTICS_NO_BIND = 0x0800,
        RES_4 = 0x1000,
        IMAGE_DLLCHARACTERISTICS_WDM_DRIVER = 0x2000,
        IMAGE_DLLCHARACTERISTICS_TERMINAL_SERVER_AWARE = 0x8000
    }

    enum BasedRelocationType
    {
        IMAGE_REL_BASED_ABSOLUTE = 0,
        IMAGE_REL_BASED_HIGH = 1,
        IMAGE_REL_BASED_LOW = 2,
        IMAGE_REL_BASED_HIGHLOW = 3,
        IMAGE_REL_BASED_HIGHADJ = 4,
        IMAGE_REL_BASED_MIPS_JMPADDR = 5,
        IMAGE_REL_BASED_MIPS_JMPADDR16 = 9,
        IMAGE_REL_BASED_IA64_IMM64 = 9,
        IMAGE_REL_BASED_DIR64 = 10
    }

    enum AllocationType : uint
    {
        COMMIT = 0x1000,
        RESERVE = 0x2000,
        RESET = 0x80000,
        LARGE_PAGES = 0x20000000,
        PHYSICAL = 0x400000,
        TOP_DOWN = 0x100000,
        WRITE_WATCH = 0x200000,
        DECOMMIT = 0x4000,
        RELEASE = 0x8000
    }

    enum MemoryProtection : uint
    {
        EXECUTE = 0x10,
        EXECUTE_READ = 0x20,
        EXECUTE_READWRITE = 0x40,
        EXECUTE_WRITECOPY = 0x80,
        NOACCESS = 0x01,
        READONLY = 0x02,
        READWRITE = 0x04,
        WRITECOPY = 0x08,
        GUARD_Modifierflag = 0x100,
        NOCACHE_Modifierflag = 0x200,
        WRITECOMBINE_Modifierflag = 0x400
    }

    enum PageProtection
    {
        NOACCESS = 0x01,
        READONLY = 0x02,
        READWRITE = 0x04,
        WRITECOPY = 0x08,
        EXECUTE = 0x10,
        EXECUTE_READ = 0x20,
        EXECUTE_READWRITE = 0x40,
        EXECUTE_WRITECOPY = 0x80,
        GUARD = 0x100,
        NOCACHE = 0x200,
        WRITECOMBINE = 0x400,
    }

    enum ImageSectionFlags : uint
    {
        IMAGE_SCN_LNK_NRELOC_OVFL = 0x01000000,  // Section contains extended relocations.
        IMAGE_SCN_MEM_DISCARDABLE = 0x02000000,  // Section can be discarded.
        IMAGE_SCN_MEM_NOT_CACHED = 0x04000000,  // Section is not cachable.
        IMAGE_SCN_MEM_NOT_PAGED = 0x08000000, // Section is not pageable.
        IMAGE_SCN_MEM_SHARED = 0x10000000,  // Section is shareable.
        IMAGE_SCN_MEM_EXECUTE = 0x20000000, // Section is executable.
        IMAGE_SCN_MEM_READ = 0x40000000, // Section is readable.
        IMAGE_SCN_MEM_WRITE = 0x80000000  // Section is writeable.
    }

    enum DllReason : uint
    {
        DLL_PROCESS_ATTACH = 1,
        DLL_THREAD_ATTACH = 2,
        DLL_THREAD_DETACH = 3,
        DLL_PROCESS_DETACH = 0
    }

    class Win
    {
        public const ushort IMAGE_DOS_SIGNATURE = 0x5A4D;
        public const uint IMAGE_NT_SIGNATURE = 0x00004550;
        public const uint IMAGE_FILE_MACHINE_I386 = 0x014c;
        public const uint IMAGE_FILE_MACHINE_AMD64 = 0x8664;
        public const uint PAGE_NOCACHE = 0x200;
        public const uint IMAGE_SCN_CNT_INITIALIZED_DATA = 0x00000040;
        public const uint IMAGE_SCN_CNT_UNINITIALIZED_DATA = 0x00000080;
        public const uint IMAGE_SCN_MEM_DISCARDABLE = 0x02000000;
        public const uint IMAGE_SCN_MEM_NOT_CACHED = 0x04000000;
        public const uint IMAGE_FILE_DLL = 0x2000;

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr VirtualAlloc(IntPtr lpAddress, UIntPtr dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

        [DllImport("msvcrt.dll", EntryPoint = "memset", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        public static extern IntPtr MemSet(IntPtr dest, int c, UIntPtr count);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr LoadLibrary(IntPtr lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, IntPtr procName);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool VirtualFree(IntPtr lpAddress, IntPtr dwSize, AllocationType dwFreeType);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool VirtualProtect(IntPtr lpAddress, IntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern void GetNativeSystemInfo(out SYSTEM_INFO lpSystemInfo);

        // Equivalent to the IMAGE_FIRST_SECTION macro
        public static IntPtr IMAGE_FIRST_SECTION(IntPtr pNTHeader, ushort ntheader_FileHeader_SizeOfOptionalHeader)
        {
            return PtrAdd(pNTHeader, Of.IMAGE_NT_HEADERS_OptionalHeader + (int)ntheader_FileHeader_SizeOfOptionalHeader);
        }
        
        // Equivalent to the IMAGE_FIRST_SECTION macro
        public static int IMAGE_FIRST_SECTION(int lfanew, ushort ntheader_FileHeader_SizeOfOptionalHeader)
        {
            return lfanew + Of.IMAGE_NT_HEADERS_OptionalHeader + ntheader_FileHeader_SizeOfOptionalHeader;
        }

        // Equivalent to the IMAGE_ORDINAL32/64 macros
        public static IntPtr IMAGE_ORDINAL(IntPtr ordinal)
        {
            return (IntPtr)(int)(unchecked((ulong)ordinal.ToInt64()) & 0xffff);
        }
        
        // Equivalent to the IMAGE_SNAP_BY_ORDINAL32/64 macro
        public static bool IMAGE_SNAP_BY_ORDINAL(IntPtr ordinal)
        {
            return (IntPtr.Size == 8 ? (ordinal.ToInt64() < 0) : (ordinal.ToInt32() < 0));
        }
    }

    static T PtrRead<T>(IntPtr ptr) { return (T)Marshal.PtrToStructure(ptr, typeof(T)); }
    static void PtrWrite<T>(IntPtr ptr, T val) { Marshal.StructureToPtr(val, ptr, false); }
    static IntPtr PtrAdd(IntPtr p, int v)     { return (IntPtr)(p.ToInt64() + v); }
    static IntPtr PtrAdd(IntPtr p, uint v)    { return (IntPtr.Size == 8 ? (IntPtr)(p.ToInt64() + unchecked((long)v)) : (IntPtr)(p.ToInt32() + unchecked((int)v))); }
    static IntPtr PtrAdd(IntPtr p, IntPtr v)  { return (IntPtr.Size == 8 ? (IntPtr)(p.ToInt64() + v.ToInt64()) : (IntPtr)(p.ToInt32() + v.ToInt32())); }
    static IntPtr PtrAdd(IntPtr p, UIntPtr v) { return (IntPtr.Size == 8 ? (IntPtr)(p.ToInt64() + unchecked((long)v.ToUInt64())) : (IntPtr)(p.ToInt32() + unchecked((int)v.ToUInt32()))); }
    static IntPtr PtrSub(IntPtr p, IntPtr v)  { return (IntPtr.Size == 8 ? (IntPtr)(p.ToInt64() - v.ToInt64()) : (IntPtr)(p.ToInt32() - v.ToInt32())); }
    static IntPtr PtrBitOr(IntPtr p, UIntPtr v) { return (IntPtr.Size == 8 ? (IntPtr)unchecked((long)(unchecked((ulong)p.ToInt64()) | v.ToUInt64())) : (IntPtr)unchecked((int)(unchecked((uint)p.ToInt32()) | v.ToUInt32()))); }
    static IntPtr PtrAlignDown(IntPtr p, UIntPtr align) { return (IntPtr)unchecked((long)(unchecked((ulong)p.ToInt64()) & ~(align.ToUInt64() - 1))); }
    static bool PtrIsInvalidHandle(IntPtr h) { return (h == IntPtr.Zero || h == (IntPtr.Size == 8 ? (IntPtr)(long)-1 : (IntPtr)(int)-1)); }
    static bool PtrSpanBoundary(IntPtr p, uint Size, int BoundaryBits) { return ((unchecked((ulong)p.ToInt64()) >> BoundaryBits) < ((unchecked((ulong)(p.ToInt64())) + Size) >> BoundaryBits)); }

    static T BytesReadStructAt<T>(byte[] buf, int offset)
    {
        int size = Marshal.SizeOf(typeof(T));
        IntPtr ptr = Marshal.AllocHGlobal(size);
        Marshal.Copy(buf, offset, ptr, size);
        T res = (T)Marshal.PtrToStructure(ptr, typeof(T));
        Marshal.FreeHGlobal(ptr);
        return res;
    }
}
