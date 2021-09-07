using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor.Controls
{
    public class FormPattern : Form
    {
        //https://stackoverflow.com/questions/2575216/how-to-move-and-resize-a-form-without-a-border
        public FormPattern()
        {

            this.FormBorderStyle = FormBorderStyle.None; // no borders
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true); // this is to avoid visual artifacts
        }

        private bool SizeableP = true;
        private int HighB = 30;
        private int OtherB = 3;

        public bool Sizeable
        {
            get
            {
                return SizeableP;
            }
            set
            {
                SizeableP = value;
                this.Refresh();
            }
        }

        public int HighBorder
        {
            get
            {
                return HighB;
            }
            set
            {
                HighB = value;
                this.Refresh();
            }
        }
        public int BordersSize
        {
            get
            {
                return OtherB;
            }
            set
            {
                OtherB = value;
                this.Refresh();
            }
        }
        protected override void OnPaint(PaintEventArgs e) // you can safely omit this method if you want
        {
            Color C = ColorTranslator.FromHtml("#00adef");

            SolidBrush B = new SolidBrush(C);

            //e.Graphics.FillRectangle(Brushes.Green, Top);
            e.Graphics.FillRectangle(B, Top);
            e.Graphics.FillRectangle(B, Left);
            e.Graphics.FillRectangle(B, Right);
            e.Graphics.FillRectangle(B, Bottom);
        }

        private const int
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17;

        const int _ = 3; // you can rename this variable if you like

        new Rectangle Top { get { return new Rectangle(0, 0, this.ClientSize.Width, HighB); } }

        new Rectangle Left { get { return new Rectangle(0, 0, OtherB, this.ClientSize.Height); } }

        new Rectangle Bottom { get { return new Rectangle(0, this.ClientSize.Height - OtherB, this.ClientSize.Width, OtherB); } }

        new Rectangle Right { get { return new Rectangle(this.ClientSize.Width - OtherB, 0, OtherB, this.ClientSize.Height); } }

        Rectangle TopLeft { get { return new Rectangle(0, 0, OtherB, OtherB); } }
        Rectangle TopRight { get { return new Rectangle(this.ClientSize.Width - OtherB, 0, OtherB, OtherB); } }
        Rectangle BottomLeft { get { return new Rectangle(0, this.ClientSize.Height - OtherB, OtherB, OtherB); } }
        Rectangle BottomRight { get { return new Rectangle(this.ClientSize.Width - OtherB, this.ClientSize.Height - OtherB, OtherB, OtherB); } }

        new Padding Padding { get { return new Padding(OtherB, HighB, OtherB, OtherB); } }

        [DllImport("user32.dll")]
        public extern static IntPtr SendMessage(IntPtr a, int msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public extern static bool ReleaseCapture();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        protected override void WndProc(ref Message message)
        {
            try
            {
                base.WndProc(ref message);

                if (message.Msg == 0x84) // WM_NCHITTEST
                {
                    if (SizeableP == true)
                    {
                        var cursor = this.PointToClient(Cursor.Position);

                        if (TopLeft.Contains(cursor)) message.Result = (IntPtr)HTTOPLEFT;
                        else if (TopRight.Contains(cursor)) message.Result = (IntPtr)HTTOPRIGHT;
                        else if (BottomLeft.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMLEFT;
                        else if (BottomRight.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMRIGHT;

                        //else if (Top.Contains(cursor)) message.Result = (IntPtr)HTTOP;

                        else if (Left.Contains(cursor)) message.Result = (IntPtr)HTLEFT;
                        else if (Right.Contains(cursor)) message.Result = (IntPtr)HTRIGHT;
                        else if (Bottom.Contains(cursor)) message.Result = (IntPtr)HTBOTTOM;
                    }
                }
            }
            catch {}     
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {

            var cursor = this.PointToClient(Cursor.Position);

            if (0 <= cursor.Y && cursor.Y <= 30)
            {
                MoveForm_();
            }

            base.OnMouseDown(e);
        }
        protected void MoveForm_()
        {
            ReleaseCapture();
            SendMessage(this.FindForm().Handle, 161, 2, 0);
        }
        protected IntPtr MoveForm()
        {
            ReleaseCapture();
            return SendMessage(this.FindForm().Handle, 161, 2, 0);
        }
    }
}
