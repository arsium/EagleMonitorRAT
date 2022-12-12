using Eagle_Monitor_RAT_Reborn.Network;
using PacketLib.Packet;
using System.Drawing;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.PacketHandler
{
    internal class FileManagerPacketHandler
    {
        public FileManagerPacketHandler(FileManagerPacket fileManagerPacket) : base()//, ClientHandler clientHandler)
        {
            try
            {
                ClientHandler.ClientHandlersList[fileManagerPacket.BaseIp].ClientForm.fileManagerDataGridView.BeginInvoke((MethodInvoker)(() =>
                {
                    ClientHandler.ClientHandlersList[fileManagerPacket.BaseIp].ClientForm.fileManagerDataGridView.Rows.Clear();

                    int x = 0;

                    foreach (var dir in fileManagerPacket.filesAndDirs[0])
                    {
                        int rowId = ClientHandler.ClientHandlersList[fileManagerPacket.BaseIp].ClientForm.fileManagerDataGridView.Rows.Add();
                        DataGridViewRow row = ClientHandler.ClientHandlersList[fileManagerPacket.BaseIp].ClientForm.fileManagerDataGridView.Rows[rowId];
                        row.Cells["Column11"].Value = Misc.Utils.ResizeImage(Properties.Resources.imageres_4.ToBitmap(), new Size(26, 26));
                        row.Cells["Column12"].Value = dir[0].ToString();
                        row.Cells["Column13"].Value = "Directory";
                        row.Cells["Column14"].Value = Misc.Utils.Numeric2Bytes((long)dir[1]);
                    }

                    x++;

                    ClientHandler.ClientHandlersList[fileManagerPacket.BaseIp].ClientForm.fileManagerDataGridView.Sort(ClientHandler.ClientHandlersList[fileManagerPacket.BaseIp].ClientForm.Column12, System.ComponentModel.ListSortDirection.Ascending);

                    foreach (var file in fileManagerPacket.filesAndDirs[1])
                    {
 
                        Image btm = PacketLib.Utils.ImageProcessing.BytesToImage((byte[])file[1]);
                        int rowId = ClientHandler.ClientHandlersList[fileManagerPacket.BaseIp].ClientForm.fileManagerDataGridView.Rows.Add();
                        DataGridViewRow row = ClientHandler.ClientHandlersList[fileManagerPacket.BaseIp].ClientForm.fileManagerDataGridView.Rows[rowId];
                        row.Cells["Column11"].Value = Misc.Utils.ResizeImage(btm, new Size(26, 26));
                        row.Cells["Column12"].Value = file[0].ToString();
                        row.Cells["Column13"].Value = "File";
                        row.Cells["Column14"].Value = Misc.Utils.Numeric2Bytes((long)file[2]);
                        row.Tag = file[2];
                        x++;
                    }
                }));
            }
            catch { }
            return;
           
        }
    }
}
