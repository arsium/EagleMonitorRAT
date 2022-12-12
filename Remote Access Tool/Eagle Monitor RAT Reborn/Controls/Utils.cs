using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Eagle_Monitor_RAT_Reborn.Network;
using Guna.UI2.WinForms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.Controls
{
    internal class Utils
    {
        internal static void Enable(ListView listView)
        {
            PropertyInfo aProp = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            aProp.SetValue(listView, true, null);
        }

        internal static void Enable(DataGridView dataGridView)
        {
            PropertyInfo aProp = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            aProp.SetValue(dataGridView, true, null);
        }

        internal static void MoveForm(FormPattern formPattern) 
        {
            Misc.Imports.ReleaseCapture();
            Misc.Imports.SendMessage(formPattern.FindForm().Handle, 161, 2, 0);
        }

        internal static void CloseForm(FormPattern any)
        {
            try
            {
                any.Close();
            }
            catch { }
        }

        internal static void InitiateForm(ClientHandler clientHandler) 
        {
            try
            {
                clientHandler.ClientForm.Show();
                clientHandler.ClientForm.BringToFront();
            }
            catch (Exception)
            {
                clientHandler.ClientForm = new ClientForm(clientHandler);
                clientHandler.ClientForm.Show();
            }
            finally
            {
                clientHandler.ClientForm.Text = $"Client : {clientHandler.FullName}";
                clientHandler.ClientForm.clientLabel.Text = $"Client : {clientHandler.FullName}";
            }
        }

        internal static Image ResizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        internal static void SetTabImage(Guna2TabControl guna2TabControl, Icon[] icons) 
        {
            ImageList imageList = new ImageList
            {
                ColorDepth = ColorDepth.Depth32Bit,
                ImageSize = new Size(28, 28)
            };

            foreach(Icon icon in icons) 
            {
                imageList.Images.Add(icon);
            }

            guna2TabControl.ImageList = imageList;

            for(int i = 0; i < guna2TabControl.TabCount; i++)
            {
                guna2TabControl.TabPages[i].ImageIndex = i;
            }
        }

        internal static void SetTotalClients() 
        {
            lock (Program.mainForm.totalClientLabel)
            {
                Program.mainForm.totalClientLabel.Invoke((MethodInvoker)(() =>
                {
                    Program.mainForm.totalClientLabel.Text = $"Total Clients : {ClientHandler.CurrentClientsNumber}";
                }));
            }
        }
    }
}
