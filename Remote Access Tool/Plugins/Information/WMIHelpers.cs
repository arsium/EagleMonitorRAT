
/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

/*            
Win32NT = Environment.OSVersion.Platform == PlatformID.Win32NT;
XpOrHigher = Win32NT && Environment.OSVersion.Version.Major >= 5;
VistaOrHigher = Win32NT && Environment.OSVersion.Version.Major >= 6;
SevenOrHigher = Win32NT && (Environment.OSVersion.Version >= new Version(6, 1));
EightOrHigher = Win32NT && (Environment.OSVersion.Version >= new Version(6, 2, 9200));
EightPointOneOrHigher = Win32NT && (Environment.OSVersion.Version >= new Version(6, 3));
TenOrHigher = Win32NT && (Environment.OSVersion.Version >= new Version(10, 0));
RunningOnMono = Type.GetType("Mono.Runtime") != null;
*/

namespace Plugin
{
    internal class WMIHelpers
    {
        internal enum OSType
        {
            AIX = 9,
            ASERIES = 0x20,
            ATTUNIX = 3,
            BeOS = 53,
            BS2000 = 35,
            BSDUNIX = 41,
            DC_OS = 23,
            DECNT = 5,
            Dedicated = 59,
            DGUX = 4,
            DigitalUNIX = 6,
            EPOC = 49,
            FreeBSD = 42,
            GNUHurd = 44,
            HP_MPE = 54,
            HPUX = 8,
            Inferno = 47,
            InteractiveUNIX = 40,
            IRIX = 28,
            IxWorks = 50,
            JavaVM = 13,
            LINUX = 36,
            Lynx = 37,
            MACHKernel = 46,
            MACROS = 2,
            MiNT = 52,
            MSDOS = 14,
            MVS = 10,
            NCR3000 = 20,
            NetBSD = 43,
            NetWare = 21,
            NextStep = 55,
            OpenVMS = 7,
            OS_390 = 60,
            OS2 = 12,
            OS400 = 11,
            OS9 = 45,
            OSF = 22,
            Other = 1,
            PalmPilot = 56,
            QNX = 48,
            ReliantUNIX = 24,
            Rhapsody = 57,
            SCOOpenServer = 26,
            SCOUnixWare = 25,
            Sequent = 27,
            Solaris = 29,
            SunOS = 30,
            TandemNSK = 33,
            TandemNT = 34,
            TPF = 62,
            U6000 = 0x1F,
            Unknown = 0,
            VM_ESA = 39,
            VSE = 61,
            VxWorks = 51,
            WIN3x = 0xF,
            WIN95 = 0x10,
            WIN98 = 17,
            WINCE = 19,
            Windows2000 = 58,
            WINNT = 18,
            XENIX = 38
        }
    }
}
