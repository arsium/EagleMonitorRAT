using Eagle_Monitor_RAT_Reborn.Network;
using PacketLib.Packet;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.PacketHandler
{
    internal class DeleteRestorePointPacketHandler
    {
        public DeleteRestorePointPacketHandler(DeleteRestorePointPacket deleteRestorePointPacket) : base()//, ClientHandler clientHandler)
        {
            try
            {
                ClientHandler.ClientHandlersList[deleteRestorePointPacket.BaseIp].ClientForm.restorePointDataGridView.BeginInvoke((MethodInvoker)(() =>
                {
                    foreach (DataGridViewRow row in ClientHandler.ClientHandlersList[deleteRestorePointPacket.BaseIp].ClientForm.restorePointDataGridView.Rows)
                    {
                        if (int.Parse(row.Cells[0].Value.ToString()) == deleteRestorePointPacket.index)
                        {
                            ClientHandler.ClientHandlersList[deleteRestorePointPacket.BaseIp].ClientForm.restorePointDataGridView.Rows.Remove(row);
                            break;
                        }
                    }
                }));
            }
            catch { }
            return;
            /*try
            {
                ClientHandler.ClientHandlersList[deleteRestorePointPacket.baseIp].clientForm.restorePointDataGridView.BeginInvoke((MethodInvoker)(() =>
                {
                    foreach (DataGridViewRow row in ClientHandler.ClientHandlersList[deleteRestorePointPacket.baseIp].clientForm.restorePointDataGridView.Rows)
                    {
                        if (int.Parse(row.Cells[0].Value.ToString()) == deleteRestorePointPacket.index)
                        {
                            ClientHandler.ClientHandlersList[deleteRestorePointPacket.baseIp].clientForm.restorePointDataGridView.Rows.Remove(row);
                            break;
                        }
                    }
                }));
            }
            catch { }
            return;*/
        }
    }
}
