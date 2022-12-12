using Eagle_Monitor_RAT_Reborn.Network;
using PacketLib.Packet;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.PacketHandler
{
    internal class NetworkInformationPacketHandler
    {
        public NetworkInformationPacketHandler(NetworkInformationPacket networkInformationPacket) : base()//, ClientHandler clientHandler) : base()
        {
            try
            {
                ClientHandler.ClientHandlersList[networkInformationPacket.BaseIp].ClientForm.networkInformationDataGridView.BeginInvoke((MethodInvoker)(() =>
                {
                    ClientHandler.ClientHandlersList[networkInformationPacket.BaseIp].ClientForm.networkInformationDataGridView.Rows.Clear();
                    foreach (TCPInformation tcpInformation in networkInformationPacket.tcpInformationList) 
                    {
                        int rowId = ClientHandler.ClientHandlersList[networkInformationPacket.BaseIp].ClientForm.networkInformationDataGridView.Rows.Add();
                        DataGridViewRow row = ClientHandler.ClientHandlersList[networkInformationPacket.BaseIp].ClientForm.networkInformationDataGridView.Rows[rowId];
                        row.Cells["Column48"].Value = tcpInformation.PID.ToString();
                        row.Cells["Column49"].Value = tcpInformation.processName;
                        row.Cells["Column50"].Value = tcpInformation.LocalEndPoint;
                        row.Cells["Column51"].Value = tcpInformation.RemoteEndPoint;
                        row.Cells["Column52"].Value = tcpInformation.State.ToString();
                    }
                  
                }));
            }
            catch { }
            return;
           
        }
    }
}
