using EagleMonitor.Networking;
using PacketLib.Packet;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

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
                        string[] cpuFeatures = informationPacket.information.hardwareInformation.cpuInformation["CPU"][0].Split('\n');

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

                        foreach (KeyValuePair<string, string> hardware in informationPacket.information.hardwareInformation.hardwareInformation) 
                        {
                            int rowId = clientHandler.informationForm.dataGridView2.Rows.Add();
                            DataGridViewRow row = clientHandler.informationForm.dataGridView2.Rows[rowId];
                            row.Cells["Column3"].Value = hardware.Key;
                            row.Cells["Column4"].Value = hardware.Value;
                        }

                        foreach (KeyValuePair<string, string> system in informationPacket.information.systemInformation.systemInformation)
                        {
                            int rowId = clientHandler.informationForm.dataGridView3.Rows.Add();
                            DataGridViewRow row = clientHandler.informationForm.dataGridView3.Rows[rowId];
                            row.Cells["Column5"].Value = system.Key;
                            row.Cells["Column6"].Value = system.Value;
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
