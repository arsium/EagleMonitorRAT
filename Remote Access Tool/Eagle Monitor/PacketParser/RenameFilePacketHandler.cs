using EagleMonitor.Networking;
using PacketLib.Packet;
using System.Windows.Forms;

namespace EagleMonitor.PacketParser
{
    internal class RenameFilePacketHandler
    {
        public RenameFilePacketHandler(RenameFilePacket renameFilePacket, ClientHandler clientHandler) : base() 
        {
            try
            {
                if (renameFilePacket.isRenamed)
                {
                    clientHandler.fileManagerForm.BeginInvoke((MethodInvoker)(() =>
                    {
                        if (clientHandler.fileManagerForm.fileListView.Items[renameFilePacket.oldName] != null)
                        {
                            clientHandler.fileManagerForm.fileListView.Items[renameFilePacket.oldName].SubItems[0].Text = renameFilePacket.newName;
                        }
                    }));
                }
            }
            catch { }
        }
    }
}
