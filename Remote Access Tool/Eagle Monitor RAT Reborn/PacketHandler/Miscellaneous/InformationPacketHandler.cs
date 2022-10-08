using Eagle_Monitor_RAT_Reborn.Network;
using PacketLib.Packet;
using System.Collections.Generic;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.PacketHandler
{
    internal class InformationPacketHandler
    {
        public InformationPacketHandler(InformationPacket informationPacket) : base()//, ClientHandler clientHandler) : base()
        {
            try
            {
                ClientHandler.ClientHandlersList[informationPacket.baseIp].clientForm.cpuDataGridView.BeginInvoke((MethodInvoker)(() =>
                {
                    ClientHandler.ClientHandlersList[informationPacket.baseIp].clientForm.cpuDataGridView.Rows.Clear();
                    ClientHandler.ClientHandlersList[informationPacket.baseIp].clientForm.componentsDataGridView.Rows.Clear();
                    ClientHandler.ClientHandlersList[informationPacket.baseIp].clientForm.systemInformationDataGridView.Rows.Clear();

                    string[] cpuFeatures = informationPacket.information.hardwareInformation.cpuInformation["CPU"][0].Split('\n');

                    int rowIdName = ClientHandler.ClientHandlersList[informationPacket.baseIp].clientForm.cpuDataGridView.Rows.Add();

                    DataGridViewRow row = ClientHandler.ClientHandlersList[informationPacket.baseIp].clientForm.cpuDataGridView.Rows[rowIdName];
                    row.Cells["Column36"].Value = cpuFeatures[0];

                    int rowIdVendor = ClientHandler.ClientHandlersList[informationPacket.baseIp].clientForm.cpuDataGridView.Rows.Add();

                    row = ClientHandler.ClientHandlersList[informationPacket.baseIp].clientForm.cpuDataGridView.Rows[rowIdVendor];
                    row.Cells["Column36"].Value = cpuFeatures[1];

                    for (int i = 2; i < cpuFeatures.Length - 1; i++)
                    {
                        string[] SplitFeature = cpuFeatures[i].Split(' ');
                        int rowId = ClientHandler.ClientHandlersList[informationPacket.baseIp].clientForm.cpuDataGridView.Rows.Add();

                        row = ClientHandler.ClientHandlersList[informationPacket.baseIp].clientForm.cpuDataGridView.Rows[rowId];
                        row.Cells["Column36"].Value = SplitFeature[0];


                        if (SplitFeature[1] == "1")
                            row.Cells["Column37"].Value = "Yes";
                        else
                            row.Cells["Column37"].Value = "No";
                    }

                    foreach (KeyValuePair<string, string> hardware in informationPacket.information.hardwareInformation.hardwareInformation)
                    {
                        int rowId = ClientHandler.ClientHandlersList[informationPacket.baseIp].clientForm.componentsDataGridView.Rows.Add();
                        row = ClientHandler.ClientHandlersList[informationPacket.baseIp].clientForm.componentsDataGridView.Rows[rowId];
                        row.Cells["Column38"].Value = hardware.Key;
                        row.Cells["Column39"].Value = hardware.Value;
                    }

                    foreach (KeyValuePair<string, string> system in informationPacket.information.systemInformation.systemInformation)
                    {
                        int rowId = ClientHandler.ClientHandlersList[informationPacket.baseIp].clientForm.systemInformationDataGridView.Rows.Add();
                        row = ClientHandler.ClientHandlersList[informationPacket.baseIp].clientForm.systemInformationDataGridView.Rows[rowId];
                        row.Cells["Column40"].Value = system.Key;
                        row.Cells["Column41"].Value = system.Value;
                    }
                }));
            }
            catch { }
            return;
            /*new Thread(() =>
            {
                try
                {
                    clientHandler.clientForm.BeginInvoke((MethodInvoker)(() =>
                    {
                        string[] cpuFeatures = informationPacket.information.hardwareInformation.cpuInformation["CPU"][0].Split('\n');

                        int rowIdName = clientHandler.clientForm.cpuDataGridView.Rows.Add();

                        DataGridViewRow row = clientHandler.clientForm.cpuDataGridView.Rows[rowIdName];
                        row.Cells["Column36"].Value = cpuFeatures[0];

                        int rowIdVendor = clientHandler.clientForm.cpuDataGridView.Rows.Add();

                        row = clientHandler.clientForm.cpuDataGridView.Rows[rowIdVendor];
                        row.Cells["Column36"].Value = cpuFeatures[1];

                        for (int i = 2; i < cpuFeatures.Length - 1; i++)
                        {
                            string[] SplitFeature = cpuFeatures[i].Split(' ');
                            int rowId = clientHandler.clientForm.cpuDataGridView.Rows.Add();

                            row = clientHandler.clientForm.cpuDataGridView.Rows[rowId];
                            row.Cells["Column36"].Value = SplitFeature[0];


                            if (SplitFeature[1] == "1")
                                row.Cells["Column37"].Value = "Yes";
                            else
                                row.Cells["Column37"].Value = "No";
                        }

                        foreach (KeyValuePair<string, string> hardware in informationPacket.information.hardwareInformation.hardwareInformation)
                        {
                            int rowId = clientHandler.clientForm.componentsDataGridView.Rows.Add();
                            row = clientHandler.clientForm.componentsDataGridView.Rows[rowId];
                            row.Cells["Column38"].Value = hardware.Key;
                            row.Cells["Column39"].Value = hardware.Value;
                        }

                        foreach (KeyValuePair<string, string> system in informationPacket.information.systemInformation.systemInformation)
                        {
                            int rowId = clientHandler.clientForm.systemInformationDataGridView.Rows.Add();
                            row = clientHandler.clientForm.systemInformationDataGridView.Rows[rowId];
                            row.Cells["Column40"].Value = system.Key;
                            row.Cells["Column41"].Value = system.Value;
                        }
                    }));
                }
                catch { }
            }).Start();*/
        }
    }
}
