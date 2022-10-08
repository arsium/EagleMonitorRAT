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
                ClientHandler.ClientHandlersList[diskPacket.baseIp].clientForm.fileManagerDataGridView.BeginInvoke((MethodInvoker)(() =>
                {
                    ClientHandler.ClientHandlersList[diskPacket.baseIp].clientForm.disksGuna2ComboBox.Items.Clear();
                    ClientHandler.ClientHandlersList[diskPacket.baseIp].clientForm.fileManagerDataGridView.Rows.Clear();
                    foreach (string disk in diskPacket.disksList)
                    {
                        ClientHandler.ClientHandlersList[diskPacket.baseIp].clientForm.disksGuna2ComboBox.Items.Add(disk);
                    }
                    ClientHandler.ClientHandlersList[diskPacket.baseIp].clientForm.disksGuna2ComboBox.SelectedItem = ClientHandler.ClientHandlersList[diskPacket.baseIp].clientForm.disksGuna2ComboBox.Items[0];
                }));
            }
            catch { }
            /*new Thread(() =>
            {
                try
                {
                    clientHandler.clientForm.BeginInvoke((MethodInvoker)(() =>
                    {
                        clientHandler.clientForm.disksGuna2ComboBox.Items.Clear();
                        clientHandler.clientForm.fileManagerDataGridView.Rows.Clear();
                        foreach (string disk in diskPacket.disksList)
                        {
                            clientHandler.clientForm.disksGuna2ComboBox.Items.Add(disk);
                        }
                        clientHandler.clientForm.disksGuna2ComboBox.SelectedItem = clientHandler.clientForm.disksGuna2ComboBox.Items[0];
                    }));
                }
                catch { }
            }).Start();*/
        }
    }
}
