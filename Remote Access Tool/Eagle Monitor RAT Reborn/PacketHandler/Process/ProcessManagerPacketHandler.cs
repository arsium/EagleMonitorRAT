using Eagle_Monitor_RAT_Reborn.Network;
using PacketLib.Packet;
using System.Drawing;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.PacketHandler
{
    internal class ProcessManagerPacketHandler
    {
        public ProcessManagerPacketHandler(ProcessManagerPacket processManagerPacket) : base()//, ClientHandler clientHandler) : base() 
        {
            try
            {
                Bitmap resized = new Bitmap(Properties.Resources.imageres_15.ToBitmap(), new Size((int)(Properties.Resources.imageres_15.ToBitmap().Size.Width / 1.5), (int)(Properties.Resources.imageres_15.ToBitmap().Size.Height / 1.5)));

                ClientHandler.ClientHandlersList[processManagerPacket.BaseIp].ClientForm.processDataGridView.BeginInvoke((MethodInvoker)(() =>
                {
                    ClientHandler.ClientHandlersList[processManagerPacket.BaseIp].ClientForm.processDataGridView.Rows.Clear();
                    foreach (Proc proc in processManagerPacket.processes)
                    {
                        int rowId = ClientHandler.ClientHandlersList[processManagerPacket.BaseIp].ClientForm.processDataGridView.Rows.Add();

                        DataGridViewRow row = ClientHandler.ClientHandlersList[processManagerPacket.BaseIp].ClientForm.processDataGridView.Rows[rowId];
                        if (proc.processIcon != null)
                        {
                            row.Cells["Column15"].Value = new Bitmap(PacketLib.Utils.ImageProcessing.BytesToImage(proc.processIcon), resized.Size);
                        }
                        else
                        {
                            row.Cells["Column15"].Value = resized;
                        }

                        row.Cells["Column16"].Value = proc.processId.ToString();
                        row.Cells["Column17"].Value = proc.processName;
                        row.Cells["Column18"].Value = proc.processWindowTitle;
                        row.Cells["Column19"].Value = proc.processWindowHandle;
                        row.Cells["Column20"].Value = proc.is64Bit;
                    }
                }));
                return;
            }
            catch { }
            /* new Thread(() =>
             {
                 try
                 {
                     Bitmap resized = new Bitmap(Properties.Resources.imageres_15.ToBitmap(), new Size((int)(Properties.Resources.imageres_15.ToBitmap().Size.Width / 1.5), (int)(Properties.Resources.imageres_15.ToBitmap().Size.Height / 1.5)));

                     clientHandler.clientForm.processDataGridView.BeginInvoke((MethodInvoker)(() =>
                     {
                         clientHandler.clientForm.processDataGridView.Rows.Clear();
                         foreach (Proc proc in processManagerPacket.processes)
                         {
                             int rowId = clientHandler.clientForm.processDataGridView.Rows.Add();

                             DataGridViewRow row = clientHandler.clientForm.processDataGridView.Rows[rowId];
                             if (proc.processIcon != null)
                             {
                                 row.Cells["Column15"].Value = new Bitmap(PacketLib.Utils.ImageProcessing.BytesToImage(proc.processIcon), resized.Size);
                             }
                             else
                             {
                                 row.Cells["Column15"].Value = resized;
                             }

                             row.Cells["Column16"].Value = proc.processId.ToString();
                             row.Cells["Column17"].Value = proc.processName;
                             row.Cells["Column18"].Value = proc.processWindowTitle;
                             row.Cells["Column19"].Value = proc.processWindowHandle;
                             row.Cells["Column20"].Value = proc.is64Bit;
                         }
                     }));
                     return;
                 }
                 catch { }
             }).Start();*/
        }
    }
}
