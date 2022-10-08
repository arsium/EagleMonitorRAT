using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Eagle_Monitor_RAT_Reborn.Network;

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
                clientHandler.clientForm.Show();
                clientHandler.clientForm.BringToFront();
            }
            catch (Exception)
            {
                clientHandler.clientForm = new ClientForm(clientHandler);
                clientHandler.clientForm.Show();
            }
            finally
            {
                clientHandler.clientForm.Text = $"Client : {clientHandler.fullName}";
                clientHandler.clientForm.clientLabel.Text = $"Client : {clientHandler.fullName}";
            }
        }

        internal static Image ResizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }
    }
}
