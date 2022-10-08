using System.Drawing;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.Controls
{
    internal class CustomContextMenuStrip : ContextMenuStrip
    {
        public CustomContextMenuStrip() : base()
        {
            this.Renderer = new ToolStripProfessionalRenderer(new LightMenuColorTable());
        //    this.BackColor = Color.White;
          //  this.ForeColor = Color.FromArgb(80, 80, 80);
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
                get { return Color.FromArgb(240, 240, 240); }
            }
            public override Color MenuItemBorder
            {
                get { return Color.FromArgb(205, 151, 249); }
                //get { return Color.White; }
            }
            public override Color MenuItemSelected
            {
                //94; 148; 255
                get { return Color.FromArgb(205, 151, 249); }
            }
            public override Color MenuItemSelectedGradientBegin
            {
                get { return Color.FromArgb(240, 240, 240); }
            }
            public override Color MenuItemSelectedGradientEnd
            {
                get { return Color.FromArgb(240, 240, 240); }
            }
            public override Color MenuStripGradientBegin
            {
                get { return Color.FromArgb(240, 240, 240); }
            }
            public override Color MenuStripGradientEnd
            {
                get { return Color.FromArgb(240, 240, 240); }
            }
            public override Color ImageMarginGradientBegin
            {
                get { return Color.FromArgb(205, 151, 249); }
            }
            public override Color ImageMarginGradientMiddle
            {
                get { return Color.FromArgb(205, 151, 249); }
            }
            public override Color ImageMarginGradientEnd
            {
                get { return Color.FromArgb(205, 151, 249); }
            }
            public override Color ToolStripDropDownBackground
            {
                get { return Color.FromArgb(205, 151, 249); }
            }
        }
    }
}
