using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor.Controls
{
    internal class ControlsDrawing
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);
        private enum ScrollBarDirection
        {
            SB_HORZ = 0,
            SB_VERT = 1,
            SB_CTL = 2,
            SB_BOTH = 3
        }

        public static void Enable(ListView listView)
        {
            PropertyInfo aProp = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            aProp.SetValue(listView, true, null);
        }
        public static void Enable(TabControl tabControl)
        {
            PropertyInfo aProp = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            aProp.SetValue(tabControl, true, null);
        }
        public static void Enable(ContextMenuStrip contextMenuStrip)
        {
            PropertyInfo aProp = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            aProp.SetValue(contextMenuStrip, true, null);
        }
        public static void Enable(PictureBox pictureBox)
        {
            PropertyInfo aProp = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            aProp.SetValue(pictureBox, true, null);
        }
        public static void Enable(Label label)
        {
            PropertyInfo aProp = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            aProp.SetValue(label, true, null);
        }

        //Thx to stackoverflow  : https://stackoverflow.com/questions/32307778/change-the-border-color-of-winforms-menu-dropdown-list
        /// <summary>
        /// Dark Theme for Context Menu
        /// </summary>
        internal class DarkMenuColorTable : ProfessionalColorTable
        {
            public DarkMenuColorTable()
            {
                // see notes
                base.UseSystemColors = false;
            }
            public override System.Drawing.Color MenuBorder
            {
                get { return Color.FromArgb(30, 30, 30); }
            }
            public override System.Drawing.Color MenuItemBorder
            {
                get { return Color.FromArgb(30, 30, 30); }
            }
            public override Color MenuItemSelected
            {
                get { return Color.FromArgb(20, 193, 255); }//20; 193; 255    0, 173, 239
            }
            public override Color MenuItemSelectedGradientBegin
            {
                get { return Color.FromArgb(30, 30, 30); }
            }
            public override Color MenuItemSelectedGradientEnd
            {
                get { return Color.FromArgb(30, 30, 30); }
            }
            public override Color MenuStripGradientBegin
            {
                get { return Color.FromArgb(30, 30, 30); }
            }
            public override Color MenuStripGradientEnd
            {
                get { return Color.FromArgb(30, 30, 30); }
            }
            public override Color ImageMarginGradientBegin
            {
                get { return Color.FromArgb(0, 173, 239); }
            }
            public override Color ImageMarginGradientMiddle
            {
                get { return Color.FromArgb(0, 173, 239); }
            }
            public override Color ImageMarginGradientEnd
            {
                get { return Color.FromArgb(0, 173, 239); }
            }
            public override Color ToolStripDropDownBackground
            {
                get { return Color.FromArgb(0, 173, 239); }
            }
        }

        /// <summary>
        /// Light Theme for Context Menu
        /// </summary>
        internal class LightMenuColorTable : ProfessionalColorTable
        {
            public LightMenuColorTable()
            {
                // see notes
                base.UseSystemColors = false;
            }
            public override System.Drawing.Color MenuBorder
            {
                get { return Color.White; }
            }
            public override System.Drawing.Color MenuItemBorder
            {
                get { return Color.White; }
            }
            public override Color MenuItemSelected
            {
                get { return Color.FromArgb(20, 193, 255); }
            }
            public override Color MenuItemSelectedGradientBegin
            {
                get { return Color.White; }
            }
            public override Color MenuItemSelectedGradientEnd
            {
                get { return Color.White; }
            }
            public override Color MenuStripGradientBegin
            {
                get { return Color.White; }
            }
            public override Color MenuStripGradientEnd
            {
                get { return Color.White; }
            }
            public override Color ImageMarginGradientBegin
            {
                get { return Color.FromArgb(0, 173, 239); }
            }
            public override Color ImageMarginGradientMiddle
            {
                get { return Color.FromArgb(0, 173, 239); }
            }
            public override Color ImageMarginGradientEnd
            {
                get { return Color.FromArgb(0, 173, 239); }
            }
            public override Color ToolStripDropDownBackground
            {
                get { return Color.FromArgb(0, 173, 239); }
            }
        }

        internal static void FixControls()
        {
            int width = StartForm.M.clientsListView.Width;

            StartForm.M.clientsListView.Columns[0].Width = (width / 10) + 50;
            StartForm.M.clientsListView.Columns[1].Width = width / 10;
            StartForm.M.clientsListView.Columns[2].Width = width / 10;
            StartForm.M.clientsListView.Columns[3].Width = width / 10;
            StartForm.M.clientsListView.Columns[4].Width = width / 10;
            StartForm.M.clientsListView.Columns[5].Width = width / 10;
            StartForm.M.clientsListView.Columns[6].Width = width / 10;
            StartForm.M.clientsListView.Columns[7].Width = width / 10;
            StartForm.M.clientsListView.Columns[8].Width = width / 10;
            StartForm.M.clientsListView.Columns[9].Width = width / 10;
            StartForm.M.clientsListView.Invalidate();
            StartForm.M.clientsListView.Update();

          /*  width = StartForm.M.tasksListView.Width;

            StartForm.M.tasksListView.Columns[0].Width = width / 2;
            StartForm.M.tasksListView.Columns[1].Width = width / 2;
            StartForm.M.tasksListView.Invalidate();
            StartForm.M.tasksListView.Update();*/

            width = StartForm.M.massListView.Width;

            StartForm.M.massListView.Columns[0].Width = width / 2;
            StartForm.M.massListView.Columns[1].Width = width / 2;
            StartForm.M.massListView.Invalidate();
            StartForm.M.massListView.Update();

            StartForm.M.panel8.Width = StartForm.M.Width - 80;
            StartForm.M.keyTextBox.Height = StartForm.M.Height - 270;
            StartForm.M.xuiCustomGroupbox1.Height = StartForm.M.Height - 132;

            ShowScrollBar(StartForm.M.clientsListView.Handle, (int)ControlsDrawing.ScrollBarDirection.SB_HORZ, false);
            //ShowScrollBar(StartForm.M.clientsListView.Handle, (int)ControlsDrawing.ScrollBarDirection.SB_VERT, false);
            //ShowScrollBar(StartForm.M.tasksListView.Handle, (int)ControlsDrawing.ScrollBarDirection.SB_BOTH, false);
            ShowScrollBar(StartForm.M.massListView.Handle, (int)ControlsDrawing.ScrollBarDirection.SB_BOTH, false);
        }

        /// <summary>
        /// This function adds 3 handlers for DrawListViewColumnHeaderEventHandler , DrawListViewSubItemEventHandler and DrawListViewItemEventHandler 
        /// to make listview adjustable with color and theme. Partially from stackoverflow.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="backColor"></param>
        /// <param name="foreColor"></param>
        /// <param name="Selected"></param>
        internal static void colorListViewHeader(ref ListView list, Color backColor, Color foreColor, Color Selected)
        {
            if (list.Name == "logsListView" || list.Name == "clientsListView")
            { list.Scrollable = true; }
            else { list.Scrollable = false; }
            list.OwnerDraw = true;
            list.DrawColumnHeader += new DrawListViewColumnHeaderEventHandler
            (
                (sender, e) => headerDraw(sender, e, backColor, foreColor)
            );
            list.DrawSubItem += new DrawListViewSubItemEventHandler
            (
                (sender, e) => DrawSubItem(sender, e)
            );
            list.DrawItem += new DrawListViewItemEventHandler
            (
                (sender, e) => DrawItem(sender, e, backColor, Selected)
            );
            ControlsDrawing.Enable(list);
            /*list.BackColor = backColor;
            list.ForeColor = foreColor;*/
        }

        private static void headerDraw(object sender, DrawListViewColumnHeaderEventArgs e, Color backColor, Color foreColor)
        {
            using (SolidBrush backBrush = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(backBrush, e.Bounds);
            }

            using (SolidBrush foreBrush = new SolidBrush(foreColor))
            {
                e.Graphics.DrawString(e.Header.Text, e.Font, foreBrush, e.Bounds);
            }
        }

        private static void DrawItem(object sender, DrawListViewItemEventArgs e , Color Normal, Color Selected)
        {
            //e.DrawDefault = true;
            if (e.Item.Selected == true)
            {
                //e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(60, 60, 60)), e.Bounds);
                e.Graphics.FillRectangle(new SolidBrush(Selected), e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(Normal), e.Bounds);
                //e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(30, 30, 30)), e.Bounds);
                //e.DrawText(TextFormatFlags.VerticalCenter);
            }
            if (((ListView)sender).Name == "clientsListView") 
            {
                //e.DrawText(TextFormatFlags.VerticalCenter);
                if (e.Item.ImageKey != null || e.Item.ImageIndex != -1)
                {
                    Rectangle rectangle = new Rectangle(e.Bounds.X, e.Bounds.Y, 28, 28);
                    e.Graphics.DrawImage(StartForm.M.countryImageList.Images[e.Item.ImageKey], rectangle);
                }
                else { return; }
            }
        }

        private static void DrawSubItem(object sender,  DrawListViewSubItemEventArgs e)
        {
            //TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

            using (StringFormat sf = new StringFormat())
            {
                sf.FormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.FitBlackBox;
                sf.Alignment = StringAlignment.Near;
                sf.LineAlignment = StringAlignment.Center;

                if (e.Header.Index == 0 && ((ListView)sender).Name == "clientsListView")
                {
                    //TextRenderer.DrawText(e.Graphics, e.SubItem.Text, new Font(new FontFamily("Segoe UI"), (float)8.25, FontStyle.Regular), new Point(e.Bounds.X, e.Bounds.Y), e.Item.ForeColor);
                    e.Graphics.DrawString(e.SubItem.Text, new Font(new FontFamily("Segoe UI"), (float)8.25, FontStyle.Regular), new SolidBrush(e.Item.ForeColor), new RectangleF(e.Bounds.X + 28 , e.Bounds.Y, e.Bounds.Width, e.Bounds.Height), sf);
                }
                else
                {
                    //TextRenderer.DrawText(e.Graphics, e.SubItem.Text, new Font(new FontFamily("Segoe UI"), (float)8.25, FontStyle.Regular), new Point(e.Bounds.X, e.Bounds.Y), e.Item.ForeColor);
                    e.Graphics.DrawString(e.SubItem.Text, new Font(new FontFamily("Segoe UI"), (float)8.25, FontStyle.Regular), new SolidBrush(e.Item.ForeColor), new RectangleF(e.Bounds.X , e.Bounds.Y, e.Bounds.Width, e.Bounds.Height), sf);
                }  
                //e.DrawText(flags);        
            }
        }

        internal static void HoverPictureBox(PictureBox pictureBox, EventArgs e) 
        {
            pictureBox.BackColor = Color.FromArgb(230, 230, 230);
        }

        internal static void DownPictureBox(PictureBox pictureBox, EventArgs e)
        {
            pictureBox.BackColor = Color.FromArgb(20, 193, 255);
        }
        internal static void UpPictureBox(PictureBox pictureBox, EventArgs e)
        {
            pictureBox.BackColor = Color.White;
        }

    }
}
