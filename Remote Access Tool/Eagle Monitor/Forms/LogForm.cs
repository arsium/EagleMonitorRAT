using EagleMonitor.Controls;
using EagleMonitor.Utils;
using System;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.Forms
{
    public partial class LogForm : FormPattern
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                const int CS_NOCLOSE = 0x200;
                cp.ClassStyle |= CS_NOCLOSE;
                return cp;
            }
        }
        protected override void WndProc(ref Message m)
        {

            switch (m.Msg)
            {
                case 0x0010://WM_CLOSE
                    return;

                case 0x0002://WM_DESTROY
                    return;
            }
            base.WndProc(ref m);
        }


        public LogForm()
        {
            InitializeComponent();
        }

        private void LogForm_Load(object sender, EventArgs e)
        {
            new Guna.UI2.WinForms.Helpers.DataGridViewScrollHelper(dataGridView1, guna2VScrollBar1, true);
            Miscellaneous.Enable(this.dataGridView1);
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

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo hit = dataGridView1.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.None)
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.CurrentCell = null;
                }
            }
        }

        private void LogForm_MouseLeave(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell = null;
        }
    }
}
