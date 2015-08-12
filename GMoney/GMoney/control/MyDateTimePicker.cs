using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms.VisualStyles;

namespace Money.control
{
	public class MyDateTimePicker: DateTimePicker
	{	
        #region Dll Import
        [DllImport("user32.dll", EntryPoint="SendMessageA")]
		private static extern int SendMessage (IntPtr hwnd, int wMsg, IntPtr wParam, object lParam);

		[DllImport("user32")]
		private static extern IntPtr GetWindowDC (IntPtr hWnd );

		[DllImport("user32")]
		private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC );
        #endregion

        #region Private Members
        const int WM_ERASEBKGND = 0x14;
		const int WM_PAINT = 0xF;
		const int WM_NC_HITTEST = 0x84;
		const int WM_NC_PAINT = 0x85;
		const int WM_PRINTCLIENT = 0x318;
		const int WM_SETCURSOR = 0x20;

#if DEBUG
        private bool mouseEnter;
        private bool mouseDown;
#endif

		private bool DroppedDown = false;
		private int InvalidateSince = 0;
		private static int DropDownButtonWidth = 17;

#if DEBUG
        private bool isMouseIn
        {
            get { return ClientRect.Contains(this.PointToClient(Control.MousePosition)); }
        }
#endif

        private Rectangle ClientRect
        {
            get { return new Rectangle(ClientRectangle.X, ClientRectangle.Y, Size.Width, Size.Height); }
        }

        private Rectangle DropDownRect
        {
            get { return new Rectangle(ClientRect.Width - DropDownButtonWidth - 1, 1, DropDownButtonWidth, ClientRect.Height - 2); }
        }
        #endregion

        #region Constructor
        static MyDateTimePicker()
		{
            DropDownButtonWidth = Money.control.ControlUtils.ComboInfoHelper.GetComboDropDownWidth();
        }

		public MyDateTimePicker()
			: base()
		{
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
        }
        #endregion

        #region Override Methods & Events
        protected override void OnValueChanged(EventArgs eventargs)
		{
			base.OnValueChanged (eventargs);
			this.Invalidate();
		}

		protected override void WndProc(ref Message m)
		{	
			IntPtr hDC = IntPtr.Zero;
			Graphics gdc = null;
			switch (m.Msg)
			{
				case WM_NC_PAINT:	
					hDC = GetWindowDC(m.HWnd);
					gdc = Graphics.FromHdc(hDC);
					SendMessage(this.Handle, WM_ERASEBKGND, hDC, 0);
					SendPrintClientMsg();
					SendMessage(this.Handle, WM_PAINT, IntPtr.Zero, 0);
					DrawBorder(gdc);
					m.Result = (IntPtr) 1;	// indicate msg has been processed
					ReleaseDC(m.HWnd, hDC);
					gdc.Dispose();				
					break;
				case WM_PAINT:	
					base.WndProc(ref m);
					hDC = GetWindowDC(m.HWnd);
					gdc = Graphics.FromHdc(hDC);
					DrawDropDown(gdc);
					DrawBorder(gdc);
					ReleaseDC(m.HWnd, hDC);
					gdc.Dispose();				
					break;
				/*
				// We don't need this anymore, handle by WM_SETCURSOR
				case WM_NC_HITTEST: 
					base.WndProc(ref m);
					if (DroppedDown)
					{
						OverrideDropDown(gdc);
						OverrideControlBorder(gdc);
					}
					break;
				*/
                case WM_SETCURSOR:
                    base.WndProc(ref m);
                    // The value 3 is discovered by trial on error, and cover all kinds of scenarios
                    // InvalidateSince < 2 wil have problem if the control is not in focus and dropdown is clicked
                    if (DroppedDown && InvalidateSince < 3)
                    {
                        Invalidate();
                        InvalidateSince++;
                    }
                    break;
				default:
					base.WndProc(ref m);
					break;
			}
		}

#if DEBUG
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
#endif

		protected override void OnDropDown(EventArgs eventargs)
		{
			InvalidateSince = 0;
			DroppedDown = true;
			base.OnDropDown (eventargs);
#if DEBUG
            mouseEnter = isMouseIn;
            mouseDown = true;
            using (Graphics g = CreateGraphics())
                DrawDropDown(g);
#endif
        }

		protected override void OnCloseUp(EventArgs eventargs)
		{
			DroppedDown = false;
			base.OnCloseUp (eventargs);
#if DEBUG
            mouseEnter = isMouseIn;
            mouseDown = true;
            using (Graphics g = CreateGraphics())
                DrawDropDown(g);
#endif
        }
	
		protected override void OnLostFocus(System.EventArgs e)
		{
			base.OnLostFocus(e);
#if DEBUG
            mouseEnter = false;
#endif
            this.Invalidate();
		}

		protected override void OnGotFocus(System.EventArgs e)
		{
			base.OnGotFocus(e);
			this.Invalidate();
		}		

		protected override void OnResize(EventArgs e)
		{
			base.OnResize (e);
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
            ControlUtils.DrawTextBoxBorder(g, ClientRect, !this.Focused || !this.Enabled);
        }

        private void DrawDropDown(Graphics g)
        {
            if (!this.ShowUpDown)
            {
#if DEBUG
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
                ControlUtils.DrawDropDown(g, rectCalcBtnArea, state);
#endif
                ControlUtils.DrawDropDown(g, DropDownRect, ComboBoxState.Normal);
            }
        }
        #endregion
    }
}
