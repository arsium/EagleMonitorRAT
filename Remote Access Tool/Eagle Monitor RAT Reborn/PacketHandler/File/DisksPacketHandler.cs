using Eagle_Monitor_RAT_Reborn.Network;
using PacketLib.Packet;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.PacketHandler
{
    internal class DisksPacketHandler
    {
        public DisksPacketHandler(DiskPacket diskPacket) : base()//, ClientHandler clientHandler) 
        {
            try
            {
                ClientHandler.ClientHandlersList[diskPacket.BaseIp].ClientForm.fileManagerDataGridView.BeginInvoke((MethodInvoker)(() =>
                {
                    ClientHandler.ClientHandlersList[diskPacket.BaseIp].ClientForm.disksGuna2ComboBox.Items.Clear();
                    ClientHandler.ClientHandlersList[diskPacket.BaseIp].ClientForm.fileManagerDataGridView.Rows.Clear();
                    foreach (string disk in diskPacket.disksList)
                    {
                        ClientHandler.ClientHandlersList[diskPacket.BaseIp].ClientForm.disksGuna2ComboBox.Items.Add(disk);
                    }
                    ClientHandler.ClientHandlersList[diskPacket.BaseIp].ClientForm.disksGuna2ComboBox.SelectedItem = ClientHandler.ClientHandlersList[diskPacket.BaseIp].ClientForm.disksGuna2ComboBox.Items[0];
                }));
            }
            catch { }
        }
    }
}
