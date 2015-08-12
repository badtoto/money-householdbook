using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace Money.control
{
    [DesignTimeVisible(true), Designer(typeof(AmountBox.AmountBoxControlDesigner)), DefaultProperty("Text")	,DefaultEvent("TextChanged")]
    public partial class AmountBox : UserControl
    {
        #region Utils
        public enum Msg
        {
            // Fields
            WM_CONTEXTMENU = 0x7b,
            WM_COPY = 0x301,
            WM_NULL = 0,
            WM_REFLECT = 0x2000,
            WM_USER = 0x400,
            WM_XPTHEME = 0x31a
        }

        public enum PART_STATES_EDITTEXT
        {
            // Fields
            ASSIST = 7,
            DISABLED = 4,
            FOCUSED = 5,
            HOT = 2,
            NORMAL = 1,
            READONLY = 6,
            SELECTED = 3
        }

        public enum PARTS_EDIT
        {
            // Fields
            CARET = 2,
            EDITTEXT = 1
        }

        public enum Style
        {
            XpStyle,
            Normal
        }

        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public RECT(int l, int t, int r, int b)
            {
                left = l;
                top = t;
                right = r;
                bottom = b;
            }

            public RECT(Rectangle r)
            {
                left = r.Left;
                top = r.Top;
                right = r.Right;
                bottom = r.Bottom;
            }

            public Rectangle ToRectangle()
            {
                return new Rectangle(left, top, right, bottom);
            }
        }

        public class Win32Util
        {
            public static void ExtTextOut(IntPtr hdc, int x, int y, Rectangle clip, string str)
            {
                Win32Util.RECT rect1;
                rect1.top = clip.Top;
                rect1.left = clip.Left;
                rect1.bottom = clip.Bottom;
                rect1.right = clip.Right;
                Win32Util.Win32API.ExtTextOut(hdc, x, y, 4, ref rect1, str, str.Length, IntPtr.Zero);
            }

            public static void FillRect(IntPtr hdc, Rectangle clip, Color color)
            {
                Win32Util.RECT rect1;
                rect1.top = clip.Top;
                rect1.left = clip.Left;
                rect1.bottom = clip.Bottom;
                rect1.right = clip.Right;
                int num1 = (((color.B & 0xff) << 0x10) | ((color.G & 0xff) << 8)) | color.R;
                IntPtr ptr1 = Win32Util.Win32API.CreateSolidBrush(num1);
                Win32Util.Win32API.FillRect(hdc, ref rect1, ptr1);
            }

            public static int GET_X_LPARAM(int lParam)
            {
                return (lParam & 0xffff);
            }

            public static int GET_Y_LPARAM(int lParam)
            {
                return (lParam >> 0x10);
            }

            public static Point GetPointFromLPARAM(int lParam)
            {
                return new Point(Win32Util.GET_X_LPARAM(lParam), Win32Util.GET_Y_LPARAM(lParam));
            }

            public static Size GetTextExtent(IntPtr hdc, string str)
            {
                Win32Util.SIZE size1;
                size1.cx = 0;
                size1.cy = 0;
                Win32Util.Win32API.GetTextExtentPoint32(hdc, str, str.Length, ref size1);
                return new Size(size1.cx, size1.cy);
            }

            public static int HIGH_ORDER(int param)
            {
                return (param >> 0x10);
            }

            public static int LOW_ORDER(int param)
            {
                return (param & 0xffff);
            }

            public static void SelectObject(IntPtr hdc, IntPtr handle)
            {
                Win32Util.Win32API.SelectObject(hdc, handle);
            }

            public static void SetBkColor(IntPtr hdc, Color color)
            {
                int num1 = (((color.B & 0xff) << 0x10) | ((color.G & 0xff) << 8)) | color.R;
                Win32Util.Win32API.SetBkColor(hdc, num1);
            }

            public static void SetBkMode(IntPtr hdc, int mode)
            {
                Win32Util.Win32API.SetBkMode(hdc, mode);
            }

            public static void SetTextColor(IntPtr hdc, Color color)
            {
                int num1 = (((color.B & 0xff) << 0x10) | ((color.G & 0xff) << 8)) | color.R;
                Win32Util.Win32API.SetTextColor(hdc, num1);
            }

            private const int ETO_CLIPPED = 4;
            private const int ETO_OPAQUE = 2;
            public const int OPAQUE = 2;
            public const int TRANSPARENT = 1;

            [StructLayout(LayoutKind.Sequential)]
            private struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct SIZE
            {
                public int cx;
                public int cy;
            }

            private class Win32API
            {
                // Methods
                [DllImport("gdi32")]
                internal static extern IntPtr CreateSolidBrush(int crColor);
                [DllImport("gdi32.dll")]
                public static extern int ExtTextOut(IntPtr hdc, int x, int y, int options, ref Win32Util.RECT clip, string str, int len, IntPtr spacings);
                [DllImport("User32.dll", CharSet = CharSet.Auto)]
                internal static extern int FillRect(IntPtr hDC, ref Win32Util.RECT rect, IntPtr hBrush);
                [DllImport("gdi32.dll")]
                public static extern int GetTextExtentPoint32(IntPtr hdc, string str, int len, ref Win32Util.SIZE size);
                [DllImport("gdi32.dll")]
                public static extern int SelectObject(IntPtr hdc, IntPtr hgdiObj);
                [DllImport("gdi32.dll")]
                public static extern int SetBkColor(IntPtr hdc, int color);
                [DllImport("gdi32.dll")]
                public static extern int SetBkMode(IntPtr hdc, int mode);
                [DllImport("gdi32.dll")]
                public static extern int SetTextColor(IntPtr hdc, int color);
            }
        }

        public class XPTheme
        {
            // Methods
            [DllImport("uxtheme.dll")]
            public static extern void CloseThemeData(IntPtr ht);

            [DllImport("uxtheme.dll")]
            public static extern void DrawThemeBackground(IntPtr ht, IntPtr hd, int iPartId, int iStateId, ref RECT rect,
                                                          ref RECT cliprect);

            [DllImport("uxtheme.dll")]
            public static extern void DrawThemeEdge(IntPtr ht, IntPtr hd, int iPartId, int iStateId, ref RECT destrect,
                                                    int uedge, int uflags, ref RECT contentrect);

            [DllImport("uxtheme.dll")]
            public static extern void DrawThemeLine(IntPtr ht, IntPtr hd, int iStateId, ref RECT rect, int dwDtlFlags);

            [DllImport("uxtheme.dll")]
            public static extern void DrawThemeParentBackground(IntPtr h, IntPtr hDC, ref RECT rect);

            [DllImport("uxtheme.dll")]
            public static extern void DrawThemeText(IntPtr ht, IntPtr hd, int iPartId, int iStateId,
                                                    [MarshalAs(UnmanagedType.LPTStr)] string psztext, int charcount,
                                                    int dwtextflags, int dwtextflags2, ref RECT rect);

            [DllImport("uxtheme.dll")]
            public static extern bool IsAppThemed();

            [DllImport("uxtheme.dll")]
            public static extern int IsThemeActive();

            [DllImport("uxtheme.dll")]
            public static extern IntPtr OpenThemeData(IntPtr h, [MarshalAs(UnmanagedType.LPTStr)] string pszClassList);

            [DllImport("uxtheme.dll")]
            public static extern void SetWindowTheme(IntPtr h, [MarshalAs(UnmanagedType.LPTStr)] string pszSubAppName,
                                                     [MarshalAs(UnmanagedType.LPTStr)] string pszSubIdList);
        }
        #endregion

        #region Private Properties
        private bool isReadOnly;
        private bool isCleared;
        private bool isNegative;
        private bool[] changePosition;
        private char decimalSeparator;
        private int groupSize;
        private char groupSeparator;
        private bool isCompatible;
        private bool keyProcessed;
        private bool allowNegative;
        protected IntPtr _theme;
        private int decimalPlaces;
        private Color negativeColor;
        private string text = "";
        private int nShiftLeft;
        private Point position;
        private string textDisplay;
        private string negativeSign;
        private string textChange;
        private char[] x;
        private char[] xz;
        private int xzn;
        private char[] y;
        private char[] yz;

        private Rectangle rectCalcBtnArea; 
        private bool isShowPopupCalculator = false;

        private bool mouseEnter = false;
        private bool mouseDown = false;
        #endregion

        #region Overrides
        protected override void OnFontChanged(EventArgs e)
        {
            OnResize(EventArgs.Empty);
        }

        protected override void OnResize(EventArgs e)
        {
            base.Height = Font.Height + 7;
            base.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            #region Initialize
            base.OnPaint(e);
            Rectangle clientRect = base.ClientRectangle;
            #endregion

            #region Calculator Button Rectangle
            int calcBtnHeight = this.Height - 2;
            int calcBtnWidth = calcBtnHeight - 3;
            rectCalcBtnArea = new Rectangle(clientRect.Width - calcBtnWidth - 1, clientRect.Y + 1, calcBtnWidth, calcBtnHeight);
            #endregion

            #region Draw Textbox
            ControlPaint.DrawBorder3D(e.Graphics, clientRect, Border3DStyle.Sunken);
            if ((isCompatible && (XPTheme.IsThemeActive() == 1)) &&
                ((_theme == IntPtr.Zero)))
            {
                _theme = XPTheme.OpenThemeData(IntPtr.Zero, "Edit");
            }
            IntPtr ptr1 = Font.ToHfont();
            IntPtr ptr2 = e.Graphics.GetHdc();
            Win32Util.SetBkMode(ptr2, 1);
            Win32Util.SelectObject(ptr2, ptr1);
            if (!base.Enabled)
            {
                Win32Util.SetTextColor(ptr2, Color.FromName("ControlDarkDark"));
            }
            else if (textDisplay.IndexOf("-") != -1)
            {
                Win32Util.SetTextColor(ptr2, negativeColor);
            }
            else
            {
                Win32Util.SetTextColor(ptr2, ForeColor);
            }
            if (_theme != IntPtr.Zero)
            {
                RECT rect1 = new RECT(base.ClientRectangle);
                IntPtr ptr3 = ptr2;
                XPTheme.DrawThemeBackground(_theme, ptr3, 1, base.Enabled ? 1 : 4, ref rect1, ref rect1);
                Rectangle rectangle2 = base.ClientRectangle;
                rectangle2.Inflate(-1, -1);
                Win32Util.FillRect(ptr2, rectangle2, BackColor);
            }
            Size size1 = Win32Util.GetTextExtent(ptr2, textDisplay);
            if (isShowPopupCalculator)
            {
                Win32Util.ExtTextOut(ptr2, (clientRect.Width - size1.Width) - nShiftLeft - calcBtnWidth, clientRect.Y + 4, clientRect,
                         textDisplay);
            }
            else
            {
                Win32Util.ExtTextOut(ptr2, (clientRect.Width - size1.Width) - nShiftLeft, clientRect.Y + 3, clientRect,
                                     textDisplay);
            }

            #region Set Cursor Position
            Point cursorPoint = new Point();
            if (decimalPlaces > 0)
            {
                for (int num1 = 1; num1 <= (decimalPlaces + 2); num1++)
                {
                    if (changePosition[num1])
                    {
                        cursorPoint.Y = 3;
                        if (num1 < (decimalPlaces + 2) && textDisplay.Length > ((decimalPlaces + 2) - num1))
                        {
                            string text1 =
                                textDisplay.Substring(textDisplay.Length - ((decimalPlaces + 2) - num1),
                                                   (decimalPlaces + 2) - num1);
                            Size size2 = Win32Util.GetTextExtent(ptr2, text1);
                            cursorPoint.X = (clientRect.Width - size2.Width) - nShiftLeft;
                        }
                        else
                        {
                            cursorPoint.X = clientRect.Width - nShiftLeft;
                        }
                    }
                }
            }
            else
            {
                cursorPoint.Y = 3;
                cursorPoint.X = clientRect.Width - nShiftLeft;
            }
            if (isShowPopupCalculator)
                cursorPoint.X -= calcBtnWidth;

            position = cursorPoint;
            if (Focused)
            {
                SetCaretPos(position.X, position.Y);
            }
            #endregion

            e.Graphics.ReleaseHdc(ptr2);
            #endregion

            #region Bind Event
            if (textChange != textDisplay)
            {
                textChange = textDisplay;
                OnTextChanged(EventArgs.Empty);
            }
            #endregion

            #region Draw Pop Up Button
            if (isShowPopupCalculator)
            {
                DrawButton(e.Graphics);
            }
            #endregion
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (ShowPopupCalculator && Enabled && !isReadOnly)
            {
                if (isMouseInPopupBtn)
                {
                    if (mouseEnter == false)
                    {
                        mouseEnter = true;
                        using (Graphics g = CreateGraphics())
                            DrawButton(g);
                    }
                }
                else
                {
                    if (mouseEnter == true)
                    {
                        mouseEnter = false;
                        using (Graphics g = CreateGraphics())
                            DrawButton(g);
                    }
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (ShowPopupCalculator && Enabled && !isReadOnly && mouseEnter)
            {
                mouseEnter = false;
                using (Graphics g = CreateGraphics())
                    DrawButton(g);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (ShowPopupCalculator && Enabled && !isReadOnly && e.Button == MouseButtons.Left && mouseDown == true)
            {
                mouseDown = false;
                using (Graphics g = CreateGraphics())
                    DrawButton(g);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (Enabled && !isReadOnly && isShowPopupCalculator && e.Button == MouseButtons.Left && isMouseInPopupBtn)
            {
                if (mouseDown == false)
                {
                    mouseDown = true;
                    using (Graphics g = CreateGraphics())
                        DrawButton(g);
                }
                AmountBoxPopup calcF = new AmountBoxPopup(this);
                calcF.Show();
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            if (decimalPlaces > 0)
            {
                for (int num1 = 1; num1 <= (decimalPlaces + 2); num1++)
                {
                    changePosition[num1] = false;
                }
                changePosition[1] = true;
            }
            base.Invalidate();
            Size size1 = new Size(1, Font.Height);
            CreateCaret(base.Handle, IntPtr.Zero, size1.Width, size1.Height);
            SetCaretPos(position.X, position.Y);
            ShowCaret(base.Handle);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            if (textDisplay == (negativeSign + TextInitial()))
            {
                textDisplay = TextInitial();
                base.Invalidate();
            }
            HideCaret(base.Handle);
            DestroyCaret();
        }

        protected override bool IsInputKey(Keys keyData)
        {
            keyProcessed = true;
            switch (keyData)
            {
                case Keys.End:
                    PositionChange(Keys.End);
                    break;

                case Keys.Home:
                    PositionChange(Keys.Home);
                    break;

                case Keys.Left:
                    PositionChange(Keys.Left);
                    return true;

                case Keys.Up:
                    PositionChange(Keys.Left);
                    return true;

                case Keys.Right:
                    PositionChange(Keys.Right);
                    return true;

                case Keys.Down:
                    PositionChange(Keys.Right);
                    return true;

                case Keys.Delete:
                    {
                        if (!isReadOnly)
                        {
                            string text1 = textDisplay;
                            Init();
                            textChange = text1;
                            base.Invalidate();
                        }
                        break;
                    }
                case Keys.Add:
                    if (!isReadOnly && allowNegative)
                    {
                        SignalChange(Keys.Add);
                        base.Invalidate();
                    }
                    break;

                case Keys.Subtract:
                case Keys.OemMinus:
                    if (!isReadOnly && allowNegative)
                    {
                        SignalChange(Keys.Subtract);
                        base.Invalidate();
                    }
                    break;

                case Keys.Back:
                    if (!isReadOnly)
                    {
                        BackSpace();
                    }
                    break;

                case Keys.Escape:
                    break;

                default:
                    if (!isReadOnly && ((Keys.Shift & keyData) != Keys.None) && ((Keys.Oemplus & keyData) == Keys.Oemplus))
                    {
                        if (allowNegative)
                        {
                            SignalChange(Keys.Add);
                            base.Invalidate();
                        }
                    }
                    else
                    {
                        if ((Keys.Control & keyData) != Keys.None)
                        {
                            return true;
                        }
                        if ((Keys.Alt & keyData) != Keys.None)
                        {
                            return true;
                        }
                        keyProcessed = false;
                    }
                    break;
            }
            return base.IsInputKey(keyData);
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x7b:
                    {
                        Point point1 = Win32Util.GetPointFromLPARAM((int)m.LParam);
                        point1 = base.PointToClient(point1);
                        break;
                    }
                case 0x31a:
                    if (_theme != IntPtr.Zero)
                    {
                        XPTheme.CloseThemeData(_theme);
                        _theme = IntPtr.Zero;
                    }
                    base.Invalidate();
                    break;
            }
            base.WndProc(ref m);
        }
        #endregion

        #region Import External Methods
        [DllImport("user32.dll")]
        public static extern int CreateCaret(IntPtr hwnd, IntPtr hbm, int cx, int cy);

        [DllImport("user32.dll")]
        public static extern int DestroyCaret();

        [DllImport("user32.dll")]
        public static extern int HideCaret(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern int ShowCaret(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern int SetCaretPos(int x, int y);
        #endregion

        #region Constructor
        public AmountBox()
        {
            this.isReadOnly = false;
            this.allowNegative = true;
            this.negativeColor = Color.Red;
            this.isCleared = true;
            this.position = Point.Empty;
            this._theme = IntPtr.Zero;
            this.decimalPlaces = 0;
            this.x = new char[40];
            this.y = new char[10];
            this.xz = new char[40];
            this.yz = new char[10];
            this.changePosition = new bool[12];
            this.textDisplay = "";
            this.text = "";
            this.groupSize = 3;
            this.groupSeparator = ',';
            this.decimalSeparator = '.';
            this.negativeSign = "-";
            this.nShiftLeft = 4;
            this.textChange = "";
            PlatformID mid1 = Environment.OSVersion.Platform;
            Version version1 = Environment.OSVersion.Version;
            Version version2 = new Version("5.1.2600.0");
            this.isCompatible = (version1 >= version2) && (mid1 == PlatformID.Win32NT);
            this.InitializeComponent();
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.FixedHeight, true);
            base.SetStyle(ControlStyles.Selectable, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.Height = this.Font.Height + 7;
            base.KeyPress += new KeyPressEventHandler(this.KeyPressed);
            this.Init();
        }
        #endregion

        #region Public Properties
        [Category("Category"), DefaultValue(false), Description("Read Only"), Browsable(true)]
        public bool ReadOnly
        {
            get { return isReadOnly; }
            set
            {
                if (isReadOnly != value)
                {
                    isReadOnly = value;
                    if (isReadOnly)
                    {
                        isShowPopupCalculator = false;
                    }
                    base.Invalidate();
                }
            }
        }

        [Category("Category"), DefaultValue(false), Description("Show Popup Calculator"), Browsable(true)]
        public bool ShowPopupCalculator
        {
            get { return isShowPopupCalculator; }
            set
            {
                if (!isReadOnly)
                {
                    if (isShowPopupCalculator != value)
                    {
                        isShowPopupCalculator = value;
                        base.Invalidate();
                    }
                }
                else
                {
                    isShowPopupCalculator = false;
                }
            }
        }

        [Category("Category"), DefaultValue(true), Description("Enables Negative Numbers"), Browsable(true)]
        public bool AllowNegative
        {
            get { return allowNegative; }
            set
            {
                if (allowNegative != value)
                {
                    allowNegative = value;
                    base.Invalidate();
                }
            }
        }

        [Category("Category"), Description("Color for Negative Numbers"), DefaultValue(typeof(Color), "Red"), Browsable(true)]
        public Color NegativeColor
        {
            get { return negativeColor; }
            set
            {
                if (negativeColor != value)
                {
                    negativeColor = value;
                    base.Invalidate();
                }
            }
        }

        [Category("Category"), DefaultValue(0), Description("Decimal Number of digits after the decimal separator"), Browsable(true), TypeConverter(typeof(DigitsDecimalsConverter))]
        public int DecimalPlaces
        {
            get { return decimalPlaces; }
            set
            {
                decimalPlaces = value;
                string text1 = textDisplay;
                Init();
                textChange = text1;
                base.Invalidate();
            }
        }

        [Category("Category"), DefaultValue("0"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
           Bindable(true)]
        public override string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                double Dummy = new double();
                if (!double.TryParse(value, System.Globalization.NumberStyles.Any, null, out Dummy))
                {
                    value = "0";
                }
                if (this.text != value)
                {
                    string format = "n" + this.decimalPlaces;
                    this.text = Convert.ToDouble(value).ToString(format);
                    this.textDisplay = this.text;
                    if (this.textDisplay.IndexOf("-") != -1)
                    {
                        this.isNegative = true;
                    }
                    else
                    {
                        this.isNegative = false;
                    }
                    base.Invalidate();
                }
                if (this.textChange != this.textDisplay)
                {
                    this.textChange = this.textDisplay;
                    this.OnTextChanged(EventArgs.Empty);
                }
            }
        }

        [Category("Category"), DefaultValue(0), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Bindable(true)]
        public double Double
        {
            get
            {
                try
                {
                    return Convert.ToDouble(this.text.Replace(",", ""));
                }
                catch
                {
                    return 0;
                }
            }
        }

        #endregion

        #region Methods
        private bool isMouseInPopupBtn
        {
            get
            {
                return this.rectCalcBtnArea.Contains(this.PointToClient(Control.MousePosition));
            }
        }

        private void DrawButton(Graphics g)
        {
            ComboBoxState state = ComboBoxState.Disabled;
            if (Enabled && !isReadOnly)
            {
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
            }
            ComboBoxRenderer.DrawDropDownButton(g, rectCalcBtnArea, state);
        }

        protected void Init()
        {
            NumberFormatInfo numberFormatInfo = CultureInfo.CurrentCulture.NumberFormat;
            decimalSeparator = numberFormatInfo.NumberDecimalSeparator.ToCharArray()[0];
            groupSeparator = numberFormatInfo.NumberGroupSeparator.ToCharArray()[0];
            groupSize = numberFormatInfo.NumberGroupSizes[0];
            negativeSign = numberFormatInfo.NegativeSign;
            isNegative = false;
            for (int num1 = 1; num1 <= (decimalPlaces + 2); num1++)
            {
                changePosition[num1] = false;
            }
            changePosition[1] = true;
            textDisplay = TextInitial();
            textChange = textDisplay;
            text = textDisplay;
        }

        protected string TextInitial()
        {
            string text1 = "";
            if (decimalPlaces > 0)
            {
                text1 = "0" + decimalSeparator.ToString();
                for (int num1 = 1; num1 <= decimalPlaces; num1++)
                {
                    text1 = text1 + "0";
                }
                return text1;
            }
            return "0";
        }

        protected void ShowOriginal()
        {
            int num2 = textDisplay.Length - ((decimalPlaces > 0) ? (decimalPlaces + 1) : 0) - (isNegative ? 1 : 0);
            xzn = 0;
            int num1 = 1;
            while (num1 <= num2)
            {
                if ((num1 % (groupSize + 1)) != 0)
                {
                    xz[num1 - (num1 / (groupSize + 1))] =
                        textDisplay.Substring((num2 - num1) + (isNegative ? 1 : 0), 1).ToCharArray()[0];
                    xzn++;
                }
                num1++;
            }
            if (decimalPlaces > 0)
            {
                for (num1 = 1; num1 <= decimalPlaces; num1++)
                {
                    yz[num1] = textDisplay.Substring((num2 + num1) + (isNegative ? 1 : 0), 1).ToCharArray()[0];
                }
            }
        }

        protected void TextValidated()
        {
            textDisplay = "";
            int num1 = 1;
            while (num1 <= (xzn + ((xzn - 1) / groupSize)))
            {
                if ((num1 % (groupSize + 1)) == 0)
                {
                    x[num1] = groupSeparator;
                }
                else
                {
                    x[num1] = xz[num1 - (num1 / (groupSize + 1))];
                }
                textDisplay = x[num1].ToString() + textDisplay;
                num1++;
            }
            if (decimalPlaces > 0)
            {
                x[0] = decimalSeparator;
                textDisplay = textDisplay + x[0].ToString();
                for (num1 = 1; num1 <= decimalPlaces; num1++)
                {
                    y[num1] = yz[num1];
                    textDisplay = textDisplay + y[num1].ToString();
                }
            }
            if (isNegative)
            {
                textDisplay = negativeSign + textDisplay;
            }
            text = textDisplay;
        }

        protected void PositionChange(Keys k)
        {
            int num1;
            switch (k)
            {
                case Keys.End:
                    for (num1 = 1; num1 <= (decimalPlaces + 2); num1++)
                    {
                        if (changePosition[num1])
                        {
                            break;
                        }
                    }
                    if (num1 < (decimalPlaces + 2))
                    {
                        changePosition[num1] = false;
                        changePosition[decimalPlaces + 2] = true;
                        base.Invalidate();
                    }
                    return;

                case Keys.Home:
                    for (num1 = 1; num1 <= (decimalPlaces + 2); num1++)
                    {
                        if (changePosition[num1])
                        {
                            break;
                        }
                    }
                    if (num1 > 1)
                    {
                        changePosition[num1] = false;
                        changePosition[1] = true;
                        base.Invalidate();
                    }
                    return;

                case Keys.Left:
                    for (num1 = 1; num1 <= (decimalPlaces + 2); num1++)
                    {
                        if (changePosition[num1])
                        {
                            break;
                        }
                    }
                    if (num1 > 1)
                    {
                        changePosition[num1] = false;
                        changePosition[num1 - 1] = true;
                        base.Invalidate();
                    }
                    return;

                case Keys.Up:
                    return;

                case Keys.Right:
                    for (num1 = 1; num1 <= (decimalPlaces + 2); num1++)
                    {
                        if (changePosition[num1])
                        {
                            break;
                        }
                    }
                    if (num1 < (decimalPlaces + 2))
                    {
                        changePosition[num1] = false;
                        changePosition[num1 + 1] = true;
                        base.Invalidate();
                    }
                    return;

                case Keys.Decimal:
                    if (decimalPlaces <= 0)
                    {
                        return;
                    }
                    for (num1 = 1; num1 <= (decimalPlaces + 2); num1++)
                    {
                        if (changePosition[num1])
                        {
                            break;
                        }
                    }
                    break;

                default:
                    return;
            }
            if (num1 > 1)
            {
                changePosition[num1] = false;
                changePosition[1] = true;
            }
            else
            {
                changePosition[1] = false;
                changePosition[2] = true;
            }
            base.Invalidate();
        }

        protected void BackSpace()
        {
            if (isCleared)
            {
                if (changePosition[1])
                {
                    ShowOriginal();
                    if ((xzn == 1) && (xz[1] != '0'))
                    {
                        xz[1] = '0';
                        TextValidated();
                        base.Invalidate();
                    }
                    else if (xzn > 1)
                    {
                        for (int num1 = 1; num1 <= xzn; num1++)
                        {
                            xz[num1] = xz[num1 + 1];
                        }
                        xzn--;
                        TextValidated();
                        base.Invalidate();
                    }
                }
                else
                {
                    int num2 = 1;
                    while (num2 <= (decimalPlaces + 2))
                    {
                        if (changePosition[num2])
                        {
                            break;
                        }
                        num2++;
                    }
                    if ((num2 > 2) && (num2 <= (decimalPlaces + 2)))
                    {
                        changePosition[num2] = false;
                        changePosition[num2 - 1] = true;
                        ShowOriginal();
                        yz[num2 - 2] = '0';
                        TextValidated();
                        base.Invalidate();
                    }
                    else if (num2 == 2)
                    {
                        changePosition[num2] = false;
                        changePosition[num2 - 1] = true;
                        base.Invalidate();
                    }
                }
                isCleared = false;
            }
            else
            {
                isCleared = true;
            }
        }

        protected void InsertKey(char k)
        {
            if (isReadOnly)
                return;
            if (changePosition[1])
            {
                if (xzn <= 20)
                {
                    ShowOriginal();
                    if ((xzn == 1) && (xz[1] == '0'))
                    {
                        xz[1] = k;
                    }
                    else
                    {
                        for (int num1 = 1; num1 <= xzn; num1++)
                        {
                            xz[(xzn - num1) + 2] = xz[(xzn - num1) + 1];
                        }
                        xz[1] = k;
                        xzn++;
                    }
                    TextValidated();
                    base.Invalidate();
                }
            }
            else
            {
                int num2 = 1;
                while (num2 <= (decimalPlaces + 2))
                {
                    if (changePosition[num2])
                    {
                        break;
                    }
                    num2++;
                }
                if ((num2 > 1) && (num2 < (decimalPlaces + 2)))
                {
                    changePosition[num2] = false;
                    changePosition[num2 + 1] = true;
                    ShowOriginal();
                    yz[num2 - 1] = k;
                    TextValidated();
                    base.Invalidate();
                }
            }
        }

        protected void SignalChange(Keys k)
        {
            if (k == Keys.Add)
            {
                if (isNegative)
                {
                    ShowOriginal();
                    isNegative = false;
                    TextValidated();
                    base.Invalidate();
                }
            }
            else if ((k == Keys.Subtract) && !isNegative)
            {
                ShowOriginal();
                isNegative = true;
                TextValidated();
                base.Invalidate();
            }
        }

        private void KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (!keyProcessed)
            {
                switch (e.KeyChar)
                {
                    case ',':
                    case '.':
                        PositionChange(Keys.Decimal);
                        return;

                    case '-':
                    case '/':
                        return;

                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        InsertKey(e.KeyChar);
                        return;
                }
            }
        }
        #endregion

        #region Classes
        public class DigitsDecimalsConverter : Int16Converter
        {
            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            }

            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            {
                return false;
            }

            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
        }

        public class AmountBoxControlDesigner : ControlDesigner
        {
            public override SelectionRules SelectionRules
            {
                get { return (base.SelectionRules & ~(SelectionRules.BottomSizeable | SelectionRules.TopSizeable)); }
            }
        }
        #endregion
    }
}
