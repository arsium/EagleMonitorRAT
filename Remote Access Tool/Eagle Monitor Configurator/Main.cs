using EagleMonitor.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using EagleMonitor.Config;
using System.IO;
using Microsoft.VisualBasic;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor_Configurator
{
    public partial class Main : FormPattern
    {
        internal static Settings settings = new Settings();
        public Main()
        {
            InitializeComponent();
        }
        internal class Utils
        {
            [DllImport("ntdll.dll", SetLastError = true)]
            internal static extern uint NtTerminateProcess(IntPtr hProcess, int errorStatus);

            public static string GPath = Application.StartupPath;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (File.Exists(Utils.GPath + "\\config.json"))
            {
                string json = System.IO.File.ReadAllText(Utils.GPath + "\\config.json");
                settings = JsonConvert.DeserializeObject<Settings>(json);
                keyGuna2TextBox.Text = settings.key;
                foreach (int p in settings.ports)
                {
                    portListView.Items.Add(p.ToString());
                }
                notificationSoundGuna2CheckBox.Checked = settings.notificationSound;
            }
        }

        public void saveSetting() 
        {
            settings.ports = new List<int>();
            settings.key = keyGuna2TextBox.Text;
            settings.notificationSound = notificationSoundGuna2CheckBox.Checked;
            foreach (ListViewItem I in portListView.Items)
            {
                settings.ports.Add(int.Parse(I.Text));
            }
            string savedSettings = JsonConvert.SerializeObject(settings);
            File.WriteAllText(Utils.GPath + "\\config.json", savedSettings);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            saveSetting();
            Utils.NtTerminateProcess(Process.GetCurrentProcess().Handle, 0);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            saveSetting();
            Utils.NtTerminateProcess(Process.GetCurrentProcess().Handle, 0);
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

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string p = Interaction.InputBox("Port :");
            int pAdd;
            if (int.TryParse(p, out pAdd) == true)
            {
                portListView.Items.Add(p).ForeColor = Color.FromArgb(64, 64, 64);
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (portListView.SelectedItems.Count > 0)
            {
                foreach(ListViewItem listViewItem in portListView.SelectedItems)
                    listViewItem.Remove();
            }
        }

    }
}
