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
    internal class ShortCutFileManagersPacketHandler
    {
        internal ShortCutFileManagersPacketHandler(ShortCutFileManagersPacket shortCutFileManagersPacket) : base() //, ClientHandler clientHandler) 
        {
            try
            {
                ClientHandler.ClientHandlersList[shortCutFileManagersPacket.baseIp].clientForm.fileManagerDataGridView.BeginInvoke((MethodInvoker)(() =>
                {
                    ClientHandler.ClientHandlersList[shortCutFileManagersPacket.baseIp].clientForm.disksGuna2ComboBox.Text = shortCutFileManagersPacket.path[0].ToString() + shortCutFileManagersPacket.path[1].ToString() + shortCutFileManagersPacket.path[2].ToString();
                    ClientHandler.ClientHandlersList[shortCutFileManagersPacket.baseIp].clientForm.labelPath.Text = shortCutFileManagersPacket.path;

                    ClientHandler.ClientHandlersList[shortCutFileManagersPacket.baseIp].clientForm.fileManagerDataGridView.Rows.Clear();

                    int x = 0;

                    foreach (var dir in shortCutFileManagersPacket.filesAndDirs[0])
                    {
                        int rowId = ClientHandler.ClientHandlersList[shortCutFileManagersPacket.baseIp].clientForm.fileManagerDataGridView.Rows.Add();
                        DataGridViewRow row = ClientHandler.ClientHandlersList[shortCutFileManagersPacket.baseIp].clientForm.fileManagerDataGridView.Rows[rowId];
                        row.Cells["Column11"].Value = Misc.Utils.ResizeImage(Properties.Resources.imageres_4.ToBitmap(), new Size(26, 26));
                        row.Cells["Column12"].Value = dir[0].ToString();
                        row.Cells["Column13"].Value = "Directory";
                        row.Cells["Column14"].Value = Misc.Utils.Numeric2Bytes((long)dir[1]);
                    }

                    x++;

                    ClientHandler.ClientHandlersList[shortCutFileManagersPacket.baseIp].clientForm.fileManagerDataGridView.Sort(ClientHandler.ClientHandlersList[shortCutFileManagersPacket.baseIp].clientForm.Column12, System.ComponentModel.ListSortDirection.Ascending);

                    foreach (var file in shortCutFileManagersPacket.filesAndDirs[1])
                    {

                        Image btm = PacketLib.Utils.ImageProcessing.BytesToImage((byte[])file[1]);
                        int rowId = ClientHandler.ClientHandlersList[shortCutFileManagersPacket.baseIp].clientForm.fileManagerDataGridView.Rows.Add();
                        DataGridViewRow row = ClientHandler.ClientHandlersList[shortCutFileManagersPacket.baseIp].clientForm.fileManagerDataGridView.Rows[rowId];
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
