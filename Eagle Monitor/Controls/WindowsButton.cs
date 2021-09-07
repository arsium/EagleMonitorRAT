using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eagle_Monitor.Controls
{
    public class WindowsButton : Button
    {
        public WindowsButton()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            Color C = ColorTranslator.FromHtml("#00adef");
            Color C2 = Color.FromArgb(255, 0, 173, 239);
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderColor = C2;
            this.FlatAppearance.BorderSize = 0;
            this.BackColor = C2;
            this.FlatAppearance.MouseOverBackColor = Color.FromArgb(250, 20, 193, 255);
            this.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 0, 153, 209);
        }
    }
}
