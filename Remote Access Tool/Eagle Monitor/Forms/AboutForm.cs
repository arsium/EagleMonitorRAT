using EagleMonitor.Controls;
using System;
using System.Windows.Forms;
using System.Reflection;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
|| Source : https://stackoverflow.com/questions/4941288/how-can-i-get-the-executing-assembly-version
*/

namespace EagleMonitor.Forms
{
    public partial class AboutForm : FormPattern
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        internal class CoreAssembly
        {
            internal static readonly Assembly Reference = typeof(CoreAssembly).Assembly;
            internal static readonly Version Version = Reference.GetName().Version;
            internal static readonly string Name = Reference.GetName().Name;
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            RowsAdder("Name", CoreAssembly.Name);
            RowsAdder("Version", CoreAssembly.Version.ToString());
            RowsAdder("Author", "Arsium");
            RowsAdder("License", "MIT");
            RowsAdder("Source", "https://github.com/arsium/EagleMonitorRAT");
            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell = null;
        }

        private void RowsAdder(string nameOfDetail, string detail) 
        {
            int rowId = dataGridView1.Rows.Add();
            DataGridViewRow row = dataGridView1.Rows[rowId];
            row.Cells["Column1"].Value = nameOfDetail;
            row.Cells["Column2"].Value = detail;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AboutForm_MouseLeave(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell = null;
        }
    }
}
