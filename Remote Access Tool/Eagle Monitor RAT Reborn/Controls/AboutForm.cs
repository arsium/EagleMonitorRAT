using Eagle_Monitor_RAT_Reborn.Controls;
using Eagle_Monitor_RAT_Reborn.Network;
using Leaf.xNet;
using Newtonsoft.Json;
using PacketLib;
using PacketLib.Packet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static Eagle_Monitor_RAT_Reborn.Misc.Utils;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn
{
    public partial class AboutForm : FormPattern
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            RowsAdder("Name", CoreAssembly.Name);
            RowsAdder("Version", CoreAssembly.Version.ToString());
            RowsAdder("Author", "Arsium");
            RowsAdder("License", "AGPL-3.0");
            RowsAdder("Source", "https://github.com/arsium/EagleMonitorRAT");
            aboutDataGridView.ClearSelection();
            aboutDataGridView.CurrentCell = null;
        }

        private void RowsAdder(string nameOfDetail, string detail)
        {
            int rowId = aboutDataGridView.Rows.Add();
            DataGridViewRow row = aboutDataGridView.Rows[rowId];
            row.Cells["Column1"].Value = nameOfDetail;
            row.Cells["Column2"].Value = detail;
        }

        private void closeGuna2ControlBox_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void logoPictureBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
           Utils.MoveForm(this);
        }

        private void versionLabel_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Utils.MoveForm(this);
        }

        //https://stackoverflow.com/questions/4314673/how-to-deselect-all-selected-rows-in-a-datagridview-control
        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGridView.HitTestInfo hit = aboutDataGridView.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.None)
                {
                    aboutDataGridView.ClearSelection();
                    aboutDataGridView.CurrentCell = null;
                }
            }
        }

        private void MainForm_MouseLeave(object sender, EventArgs e)
        {
            aboutDataGridView.ClearSelection();
            aboutDataGridView.CurrentCell = null;
        }
    }
}
