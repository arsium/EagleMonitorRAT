using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Shared
{
    public static class Utils
    {
		public static byte[] b = { 0 };
		public const int BufferSize = 50 * 1024;//1024 * 16; //16KB

		[DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);

        [DllImport("psapi")]
        public extern static bool EmptyWorkingSet(IntPtr hfandle);
        public static void ClearMem()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            EmptyWorkingSet(Process.GetCurrentProcess().Handle);
        }

		public static string Numeric2Bytes(double b)
		{
			string tempNumeric2Bytes = null;
			string[] bSize = new string[9];
			int i = 0;

			bSize[0] = "Bytes";
			bSize[1] = "KB"; //Kilobytes
			bSize[2] = "MB"; //Megabytes
			bSize[3] = "GB"; //Gigabytes
			bSize[4] = "TB"; //Terabytes
			bSize[5] = "PB"; //Petabytes
			bSize[6] = "EB"; //Exabytes
			bSize[7] = "ZB"; //Zettabytes
			bSize[8] = "YB"; //Yottabytes

			double toDouble = (double)b; // Make sure var is a Double (not just
						   // variant)
			for (i = bSize.GetUpperBound(0); i >= 0; i--)
			{
				if (toDouble >= (Math.Pow(1024, i)))
				{
					tempNumeric2Bytes = ThreeNonZeroDigits(toDouble / (Math.Pow(1024, i))) + " " + bSize[i];
					break;
				}
			}
			return tempNumeric2Bytes;
		}

		private static string ThreeNonZeroDigits(double value)
		{
			if (value >= 100)
			{
				// No digits after the decimal.
				return Microsoft.VisualBasic.Strings.Format(Convert.ToInt32(value));
			}
			else if (value >= 10)
			{
				// One digit after the decimal.
				return value.ToString("0.0");
			}
			else
			{
				return value.ToString("0.00");
			}
		}

		public enum Algo
		{
			Blowfish,
			Cast5,
			Cast6,
			DesEde,
			DesEngine,
			Dstu7624,
			Gost28147,
			Idea,
			Noekeon,
			RC2,
			RC532,
			RC6,
			Rijndael,
			Seed,
			Serpent,
			Skipjack,
			SM4,
			Tea,
			Threefish,
			Tnepres,
			Twofish,
			Xtea,
			Chacha,
			Chacha7539,
			Salsa20,
			XSalsa20,
			Vmpc,
			RC4,
			VmpcKsa3,
			HC256,
			Isaac
		}

		[Serializable]
		public class ReturnHelper 
		{
			public bool CheckError { get; set; }
			public string ErrorDescription { get; set; }
			
			public ReturnHelper(bool checkError, string errorDescription) 		
			{
				this.CheckError = checkError;
				this.ErrorDescription = errorDescription;
			}
		}
	}
}
