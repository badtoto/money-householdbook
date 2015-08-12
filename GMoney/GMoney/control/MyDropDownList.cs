using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Forms.VisualStyles;

namespace Money.control
{
	[ToolboxBitmap(typeof(System.Windows.Forms.ComboBox))]
	public class MyDropDownList: ComboBox
	{
        #region Private Members
        const int WM_ERASEBKGND = 0x14;
        const int WM_PAINT = 0xF;
        const int WM_NC_PAINT = 0x85;
        const int WM_PRINTCLIENT = 0x318;
        private static int DropDownButtonWidth = 17;

        private bool mouseEnter;
        private bool mouseDown;

        private Rectangle DropDownRect
        {
            get { return new Rectangle(this.Width - DropDownButtonWidth - 1, 1, DropDownButtonWidth, this.Height - 2); }
        }

        private bool isMouseIn
        {
            get { return ClientRectangle.Contains(this.PointToClient(Control.MousePosition)); }
        }
        #endregion

        #region Dll Import
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, object lParam);

        [DllImport("user32")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        #endregion

        #region Constructor
        static MyDropDownList()
        {
            //DropDownButtonWidth = ComboInfoHelper.GetComboDropDownWidth() + 2;
            DropDownButtonWidth = Money.control.ControlUtils.ComboInfoHelper.GetComboDropDownWidth();
        }

        public MyDropDownList()
            : base()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            base.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        #endregion

        #region Override Methods & Events
        protected override void OnSelectedValueChanged(EventArgs e)
        {
            base.OnSelectedValueChanged(e);
            this.Invalidate();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            this.Invalidate();
        }

        protected override void WndProc(ref Message m)
        {
            IntPtr hDC = IntPtr.Zero;
            Graphics gdc = null;
            switch (m.Msg)
            {
                // WM_NC_PAINT message is received 
                //when the control needs to paint its border. 
                //Here I trapped the message and send WM_PRINTCLIENT message 
                //so that the ComboBox will draw its client area properly, 
                //then followed by drawing a flat border around it.
                case WM_NC_PAINT:
                    hDC = GetWindowDC(this.Handle);
                    gdc = Graphics.FromHdc(hDC);
                    SendMessage(this.Handle, WM_ERASEBKGND, hDC, 0);
                    SendPrintClientMsg();	// send to draw client area
                    DrawBorder(gdc);
                    m.Result = (IntPtr)1;	// indicate msg has been processed			
                    ReleaseDC(m.HWnd, hDC);
                    gdc.Dispose();

                    break;
                // WM_PAINT message is received 
                //when the control needs to paint portion of its windows. 
                //ComboBox internally embeds a textbox and will draw the textbox with 3D border. 
                //A quick way to achieve the flat look is to paint a rectangle 
                //overlaying the 3D border of the textbox, therefore it will appear flat. 
                //We then paint the flat dropdown and border over it. 
                //Overriding the border is optional here, 
                //but I did it for the user experience where if the control is in focus, 
                //it will have a black line border or otherwise none.
                case WM_PAINT:
                    base.WndProc(ref m);
                    // flatten the border area again
                    hDC = GetWindowDC(this.Handle);
                    gdc = Graphics.FromHdc(hDC);
                    //Pen p = new Pen((this.Enabled ? BackColor : SystemColors.Control), 2);
                    //gdc.DrawRectangle(p, new Rectangle(2, 2, this.Width - 3, this.Height - 3));
                    DrawDropDown(gdc);
                    DrawBorder(gdc);
                    ReleaseDC(m.HWnd, hDC);
                    gdc.Dispose();

                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            mouseEnter = true;
            using (Graphics g = CreateGraphics())
                DrawDropDown(g);
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            mouseEnter = true;
            using (Graphics g = CreateGraphics())
                DrawDropDown(g);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            mouseEnter = false;
            using (Graphics g = CreateGraphics())
                DrawDropDown(g);
        }

        protected override void OnDropDown(EventArgs e)
        {
            base.OnDropDown(e);
            mouseEnter = isMouseIn;
            mouseDown = true;
            using (Graphics g = CreateGraphics())
                DrawDropDown(g);
        }

        protected override void OnDropDownClosed(EventArgs e)
        {
            base.OnDropDownClosed(e);
            mouseEnter = isMouseIn;
            mouseDown = false;
            using (Graphics g = CreateGraphics())
                DrawDropDown(g);
        }

        protected override void OnLostFocus(System.EventArgs e)
        {
            base.OnLostFocus(e);
            mouseEnter = false;
            this.Invalidate();
        }

        protected override void OnGotFocus(System.EventArgs e)
        {
            base.OnGotFocus(e);
            this.Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Invalidate();
        }
        #endregion

        #region Methods
        private void SendPrintClientMsg()
        {
            // We send this message for the control to redraw the client area
            Graphics gClient = this.CreateGraphics();
            IntPtr ptrClientDC = gClient.GetHdc();
            SendMessage(this.Handle, WM_PRINTCLIENT, ptrClientDC, 0);
            gClient.ReleaseHdc(ptrClientDC);
            gClient.Dispose();
        }

        private void DrawBorder(Graphics g)
        {
            ControlUtils.DrawTextBoxBorder(g, ClientRectangle, !this.Focused || !this.Enabled);
        }

        public void DrawDropDown(Graphics g)
        {
            ComboBoxState state = ComboBoxState.Disabled;
            if (mouseDown)
            {
                state = ComboBoxState.Pressed;
            }
            else if (mouseEnter)
            {
                state = ComboBoxState.Hot;
            }
            else
            {
                state = ComboBoxState.Normal;
            }
            ControlUtils.DrawDropDown(g, DropDownRect, state);
        }
        #endregion

        #region Unused Properties & Events
        /// <summary>This property is not relevant for this class.</summary>
        /// <returns>This property is not relevant for this class.</returns>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public new ComboBoxStyle DropDownStyle
        {
            get { return base.DropDownStyle; }
        }

        /// <summary>This property is not relevant for this class.</summary>
        /// <returns>This property is not relevant for this class.</returns>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public new bool Enabled
        {
            get { return base.Enabled; }
        }
        #endregion
    }
}
