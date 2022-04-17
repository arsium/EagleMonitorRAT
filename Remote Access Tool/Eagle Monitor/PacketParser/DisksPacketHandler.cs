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
                        foreach (string disk in diskPacket.disksList)
                        {
                            clientHandler.fileManagerForm.diskComboBox.Items.Add(disk);
                        }                  
                        clientHandler.fileManagerForm.diskComboBox.SelectedItem = clientHandler.fileManagerForm.diskComboBox.Items[0];
                    }));
                }
                catch { }
            }).Start();
        }
    }
}
