using EagleMonitor.Networking;
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
    internal class ShortCutFileManagersPacketParser
    {
        internal ShortCutFileManagersPacketParser(ShortCutFileManagersPacket shortCutFileManagersPacket, ClientHandler clientHandler) 
        {
            new Thread(() =>
            {
                try
                {
                    clientHandler.fileManagerForm.BeginInvoke((MethodInvoker)(() =>
                    {
                        clientHandler.fileManagerForm.fileListView.Items.Clear();

                        ImageList imageList = new ImageList
                        {
                            ColorDepth = ColorDepth.Depth32Bit,
                            ImageSize = new Size(32, 32)
                        };
                        clientHandler.fileManagerForm.fileListView.LargeImageList = imageList;
                        clientHandler.fileManagerForm.fileListView.SmallImageList = imageList;

                        int x = 0;

                        imageList.Images.Add(x.ToString(), EagleMonitor.Properties.Resources.imageres_4);

                        foreach (var dir in shortCutFileManagersPacket.filesAndDirs[0])
                        {
                            var listViewItem = clientHandler.fileManagerForm.fileListView.Items.Add(dir[0].ToString());
                            listViewItem.Tag = "FOLDER";
                            listViewItem.Name = dir[0].ToString();
                            listViewItem.ImageKey = x.ToString();
                        }

                        x++;

                        clientHandler.fileManagerForm.fileListView.Sort();

                        foreach (var file in shortCutFileManagersPacket.filesAndDirs[1])
                        {
                            Image btm = PacketLib.Utils.ImageProcessing.BytesToImage((byte[])file[1]);
                            imageList.Images.Add(x.ToString(), btm);
                            ListViewItem listViewItem = new ListViewItem(file[0].ToString());
                            listViewItem.SubItems.Add(Utils.Miscellaneous.Numeric2Bytes((long)file[2]));
                            listViewItem.Tag = file[2];
                            listViewItem.Name = file[0].ToString();
                            listViewItem.ImageKey = x.ToString();
                            clientHandler.fileManagerForm.fileListView.Items.Add(listViewItem);
                            x++;
                        }

                        clientHandler.fileManagerForm.diskComboBox.Text = shortCutFileManagersPacket.path[0].ToString();
                        clientHandler.fileManagerForm.labelPath.Text = shortCutFileManagersPacket.path;

                        clientHandler.fileManagerForm.loadingCircle1.Visible = false;
                        clientHandler.fileManagerForm.loadingCircle1.Active = false;
                    }));
                }
                catch { }
            }).Start();
        }
    }
}
