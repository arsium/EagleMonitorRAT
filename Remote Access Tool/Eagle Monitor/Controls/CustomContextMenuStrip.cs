using System.Drawing;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.Controls
{
    internal class CustomContextMenuStrip : ContextMenuStrip
    {
        public CustomContextMenuStrip() : base()
        {
            this.Renderer = new ToolStripProfessionalRenderer(new LightMenuColorTable());
            SetStyle(ControlStyles.DoubleBuffer, true); 
            UpdateStyles();
        }

        private const int CS_DROPSHADOW = 0x00020000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                //cp.ClassStyle |= CS_DROPSHADOW;  ADD
                cp.ClassStyle &= ~ CS_DROPSHADOW; //REMOVE
                return cp;
            }
        }

        internal class LightMenuColorTable : ProfessionalColorTable
        {
            public LightMenuColorTable()
            {
                // see notes
                base.UseSystemColors = false;
            }
            public override Color MenuBorder
            {
                get { return Color.FromArgb(45, 45, 45); }
            }
            public override Color MenuItemBorder
            {
                get { return Color.FromArgb(94, 148, 255); }
                //get { return Color.White; }
            }
            public override Color MenuItemSelected
            {
                //94; 148; 255
                get { return Color.FromArgb(94, 148, 255); }
            }
            public override Color MenuItemSelectedGradientBegin
            {
                get { return Color.FromArgb(45,45,45); }
            }
            public override Color MenuItemSelectedGradientEnd
            {
                get { return Color.FromArgb(45, 45, 45); }
            }
            public override Color MenuStripGradientBegin
            {
                get { return Color.FromArgb(45, 45, 45); }
            }
            public override Color MenuStripGradientEnd
            {
                get { return Color.FromArgb(45, 45, 45); }
            }
            public override Color ImageMarginGradientBegin
            {
                get { return Color.FromArgb(94, 148, 255); }
            }
            public override Color ImageMarginGradientMiddle
            {
                get { return Color.FromArgb(94, 148, 255); }
            }
            public override Color ImageMarginGradientEnd
            {
                get { return Color.FromArgb(94, 148, 255); }
            }
            public override Color ToolStripDropDownBackground
            {
                get { return Color.FromArgb(94, 148, 255); }
            }
        }
    }
}
