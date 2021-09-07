using Eagle_Monitor.Controls;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor.Forms
{
    public partial class NotificationForm : FormPattern
    {
        public string ClientHWID { get; set; }
        public string IP_Origin { get; set; }
        public NotificationForm(string ClientHWID, string IP_Origin)
        {
            this.ClientHWID = ClientHWID;
            this.IP_Origin = IP_Origin;
            InitializeComponent();
        }

        private void NotificationForm_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width , Screen.PrimaryScreen.WorkingArea.Height - this.Height);
            Utilities.AnimateWindow(this.Handle, 250, Utilities.dwFlags.AW_BLEND);
            Task.Run(() => 
            {
                new Thread(() => { Thread.Sleep(3500); Utilities.AnimateWindow(this.Handle, 250, Utilities.dwFlags.AW_HOR_POSITIVE | Utilities.dwFlags.AW_HIDE); this.Close(); }).Start();
            });
        }
    }
}
