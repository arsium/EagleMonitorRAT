using System.Drawing;
using System.IO;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Utils
{
    public class ImageProcessing
    {
        public static Image BytesToImage(byte[] bytes)
        {
            using (MemoryStream mStream = new MemoryStream(bytes))
            {
                return Image.FromStream(mStream);
            }
        }
        public static byte[] ImageToBytes(Image img)
        {
            using (var ms = new MemoryStream())
            {
                (new Bitmap(img)).Save(ms, img.RawFormat);
                return ms.ToArray();
            }
        }
    }
}
