using System;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Utils
{
    //From Async HWID Generator + https://stackoverflow.com/questions/2333149/how-to-fast-get-hardware-id-in-c
    internal class HwidGen
    {
        private static string identifier(string wmiClass, string wmiProperty)
        {
            var mbs = new ManagementObjectSearcher("Select "+ wmiProperty + " From " + wmiClass);
            ManagementObjectCollection mbsList = mbs.Get();
            string result = "";
            foreach (ManagementObject mo in mbsList)
            {
                try
                {
                    result = mo["wmiProperty"].ToString();
                }
                catch (Exception)
                {
                    return result;
                }
                break;
            }
            return result;
        }
        private static string procId()
        {
            return identifier("Win32_processor", "ProcessorId")
            + identifier("Win32_processor", "Manufacturer")
            + identifier("Win32_processor", "Name")
            + identifier("Win32_processor", "NumberOfCores");
        }
        private static string baseId()
        {
            return identifier("Win32_BaseBoard", "Model")
            + identifier("Win32_BaseBoard", "Manufacturer")
            + identifier("Win32_BaseBoard", "Name")
            + identifier("Win32_BaseBoard", "SerialNumber");
        }
        private static string GetHash(string strToHash)
        {
            MD5CryptoServiceProvider md5Obj = new MD5CryptoServiceProvider();
            byte[] bytesToHash = Encoding.ASCII.GetBytes(strToHash);
            bytesToHash = md5Obj.ComputeHash(bytesToHash);
            StringBuilder strResult = new StringBuilder();
            foreach (byte b in bytesToHash)
                strResult.Append(b.ToString("x2"));
            return strResult.ToString().Substring(0, 20).ToUpper();
        }
        public static string HWID()
        {
            return GetHash(string.Concat(baseId(), Environment.ProcessorCount, Environment.UserName,
                   Environment.MachineName, Environment.OSVersion
                   , new DriveInfo(Path.GetPathRoot(Environment.SystemDirectory)).TotalSize, procId()));

        }
    }
}
