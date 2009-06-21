using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.Windows.Forms.VisualStyles;
using System.Drawing.Drawing2D;

namespace GMoney.control
{
    [DesignTimeVisible(true), Designer(typeof(NumericBox.NumericBoxControlDesigner)), DefaultProperty("Text"), DefaultEvent("TextChanged")]
    public partial class NumericBox : UserControl
    {
        #region Members
        private bool isCleared;
        private bool isNegative;
        private bool[] changePosition;
        private bool keyProcessed;
        private Point position;
        private string textDisplay;
        private int groupSize;
        private char decimalSeparator;
        private char groupSeparator;
        private string negativeSign;
        private string textChange;
        private char[] x;
        private char[] xz;
        private int xzn;
        private char[] y;
        private char[] yz;


        private Rectangle rectCalcBtnArea;
        private int calcBtnHeight;
        private int calcBtnWidth;

        private Rectangle rectTextArea;
        private int textHeight;
        private int textWidth;

        private bool mouseEnter;
        private bool mouseDown;

        private ToolStripControlHost toolStripControlHost;
        private ToolStripDropDown toolStripDropDown;
        private NumericBoxPopup nbp;
        private bool dropDownShown;
        #endregion

        #region Properties
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        new public event EventHandler TextChanged
        {
            add { base.TextChanged += value; }
            remove { base.TextChanged -= value; }
        }

        private double minimum = -999999999999999;
        [Category("Category"), DefaultValue(typeof(double), "-999999999999999"), Browsable(true), Bindable(true)]
        public double Minimum
        {
            get
            {
                return minimum;
            }
            set
            {
                minimum = value;
            }
        }

        private double maximum = 999999999999999;
        [Category("Category"), DefaultValue(typeof(double), "999999999999999"), Browsable(true), Bindable(true)]
        public double Maximum
        {
            get
            {
                return maximum;
            }
            set
            {
                maximum = value;
            }
        }

        private bool isReadOnly = false;
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
                        showPopupCalculator = false;
                    }
                    base.Invalidate();
                }
            }
        }

        private bool showPopupCalculator = false;
        [Category("Category"), DefaultValue(false), Description("Show Popup Calculator"), Browsable(true)]
        public bool ShowPopupCalculator
        {
            get { return showPopupCalculator; }
            set
            {
                if (!isReadOnly)
                {
                    if (showPopupCalculator != value)
                    {
                        showPopupCalculator = value;
                        base.Invalidate();
                    }
                }
                else
                {
                    showPopupCalculator = false;
                }
            }
        }

        private bool allowNegative = true;
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
                if (!allowNegative)
                {
                    minimum = 0;
                }
            }
        }

        private Color negativeColor = Color.Red;
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

        private int decimalPlaces;
        [Category("Category"), DefaultValue(0), Description("Decimal Number of digits after the decimal separator"), Browsable(true), TypeConverter(typeof(DigitsDecimalsConverter))]
        public int DecimalPlaces
        {
            get { return decimalPlaces; }
            set
            {
                decimalPlaces = value;
                string text1 = textDisplay;
                Init();

                maximum += 0.999999999;
                minimum -= 0.999999999;

                maximum = Convert.ToDouble(FormatString(maximum));
                minimum = Convert.ToDouble(FormatString(minimum));

                textChange = text1;
                base.Invalidate();
            }
        }

        private string text = "";
        [Category("Category"), DefaultValue("0"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Bindable(true)]
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
                    this.text = FormatString(CheckNum(Convert.ToDouble(value)));
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
            Graphics g = e.Graphics;
            #endregion

            #region Set Rectangle
            calcBtnHeight = this.Height - 2;
            calcBtnWidth = GMoney.control.ControlUtils.ComboInfoHelper.GetComboDropDownWidth();
            rectCalcBtnArea = new Rectangle(clientRect.Width - calcBtnWidth - 1, clientRect.Y + 1, calcBtnWidth, calcBtnHeight);

            textHeight = this.Height;
            textWidth = this.Width - 5;
            if (showPopupCalculator)
            {
                textWidth -= calcBtnWidth;
            }
            rectTextArea = new Rectangle(2, clientRect.Y, textWidth, textHeight);
            #endregion

            #region Draw TextBox & DropDownButton
            // draw textbox
            DrawBorder(g);

            // draw dropdownbutton
            if (showPopupCalculator)
                DrawButton(g);
            #endregion

            #region Draw Text & Cursor
            Size txtSize = new Size(0, 0);
            IntPtr ptr = g.GetHdc();
            SelectObject(ptr, Font.ToHfont());

            #region Draw Text
            GetTextExtentPoint32(ptr, textDisplay, textDisplay.Length, ref txtSize);
            int x = clientRect.Width - txtSize.Width - 3;
            int y = clientRect.Y + 4;
            if (showPopupCalculator)
            {
                x -= calcBtnWidth;
            }

            Color txtColor = allowNegative && Double < 0 ? negativeColor : ForeColor;
            SetTextColor(ptr, (((txtColor.B & 0xff) << 0x10) | ((txtColor.G & 0xff) << 8)) | txtColor.R);
            ExtTextOut(ptr, x, y, 4, ref clientRect, textDisplay, textDisplay.Length, IntPtr.Zero);
            #endregion

            #region Set Cursor Position
            Point cursorPoint = new Point();
            cursorPoint.Y = 3;
            if (decimalPlaces > 0)
            {
                for (int i = 1; i <= (decimalPlaces + 2); i++)
                {
                    if (changePosition[i])
                    {
                        if (i < (decimalPlaces + 2) && textDisplay.Length > ((decimalPlaces + 2) - i))
                        {
                            string str = textDisplay.Substring(textDisplay.Length - ((decimalPlaces + 2) - i), (decimalPlaces + 2) - i);
                            GetTextExtentPoint32(ptr, str, str.Length, ref txtSize);
                            cursorPoint.X = clientRect.Width - txtSize.Width - 3;
                        }
                        else
                        {
                            cursorPoint.X = clientRect.Width - 3;
                        }
                    }
                }
            }
            else
            {
                cursorPoint.X = clientRect.Width - 4;
            }

            if (showPopupCalculator)
                cursorPoint.X -= calcBtnWidth;

            position = cursorPoint;
            if (Focused)
            {
                SetCaretPos(position.X, position.Y);
            }
            #endregion

            g.ReleaseHdc(ptr);

            #endregion

            #region Bind Event
            if (textChange != textDisplay)
            {
                textChange = textDisplay;
                OnTextChanged(EventArgs.Empty);
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
                    Cursor = Cursors.Default;
                    if (mouseEnter == false)
                    {
                        mouseEnter = true;
                        using (Graphics g = CreateGraphics())
                            DrawButton(g);
                    }
                }
                else
                {
                    Cursor = Cursors.IBeam;
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
            if (Enabled && !isReadOnly && showPopupCalculator && e.Button == MouseButtons.Left && isMouseInPopupBtn)
            {
                if (mouseDown == false)
                {
                    mouseDown = true;
                    using (Graphics g = CreateGraphics())
                        DrawButton(g);
                }

                if (dropDownShown == false)
                {
                    dropDownShown = true;
                    nbp.OpenReset();
                    toolStripDropDown.Show(this, this.Width - nbp.Width, this.Height + 1);
                    toolStripDropDown.Focus();
                }
                else
                {
                    dropDownShown = false;
                    toolStripDropDown.Close();
                }
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
            if (textDisplay == (negativeSign + FormatString(0)))
            {
                textDisplay = FormatString(0);
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
        #endregion

        #region Dll Import
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

        [DllImport("gdi32.dll")]
        public static extern int GetTextExtentPoint32(IntPtr hdc, string str, int len, ref Size size);

        [DllImport("gdi32.dll")]
        public static extern int SelectObject(IntPtr hdc, IntPtr hgdiObj);

        [DllImport("gdi32.dll")]
        public static extern int ExtTextOut(IntPtr hdc, int x, int y, int options, ref Rectangle clip, string str, int len, IntPtr spacings);
        
        [DllImport("gdi32.dll")]
        public static extern int SetTextColor(IntPtr hdc, int color);
        #endregion

        #region Constructor
        public NumericBox()
        {
            this.isCleared = true;
            this.position = Point.Empty;
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
            this.textChange = "";
            this.InitializeComponent();
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.FixedHeight, true);
            base.SetStyle(ControlStyles.Selectable, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.Height = this.Font.Height + 7;
            base.KeyPress += new KeyPressEventHandler(this.KeyPressed);
            Init();

            mouseDown = false;
            mouseEnter = false;
            dropDownShown = false;

            nbp = new NumericBoxPopup(this);
            toolStripControlHost = new ToolStripControlHost(nbp);
            toolStripControlHost.Margin = Padding.Empty;
            toolStripDropDown = new ToolStripDropDown();
            toolStripDropDown.Items.Add(toolStripControlHost);
            toolStripDropDown.BackColor = nbp.BackColor;
            toolStripDropDown.AutoSize = true;
            toolStripDropDown.DropShadowEnabled = false;
            toolStripDropDown.Padding = Padding.Empty;
            toolStripDropDown.KeyPress += new KeyPressEventHandler(nbp.DoKeyPress);
            toolStripDropDown.KeyDown += new KeyEventHandler(nbp.DoKeyDown);
            toolStripDropDown.KeyUp += new KeyEventHandler(nbp.DoKeyUp);
            toolStripDropDown.Closed += new ToolStripDropDownClosedEventHandler(toolStripDropDown_Closed);
        }

        private void toolStripDropDown_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            if (!isMouseInPopupBtn)
                dropDownShown = false;
        }
        #endregion

        #region Methods
        public void CloseDropDown()
        {
            toolStripDropDown.Close();
        }

        public void SetDropDownFocus()
        {
            toolStripDropDown.Focus();
        }

        private bool isMouseInPopupBtn
        {
            get
            {
                return this.rectCalcBtnArea.Contains(this.PointToClient(Control.MousePosition));
            }
        }

        private void DrawBorder(Graphics g)
        {
            ControlUtils.DrawTextBoxBorder(g, ClientRectangle, !this.Focused || !this.Enabled);
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
            ControlUtils.DrawDropDown(g, rectCalcBtnArea, state);
        }

        public void Init()
        {
            NumberFormatInfo numberFormatInfo = CultureInfo.CurrentCulture.NumberFormat;
            decimalSeparator = numberFormatInfo.NumberDecimalSeparator.ToCharArray()[0];
            groupSeparator = numberFormatInfo.NumberGroupSeparator.ToCharArray()[0];
            groupSize = numberFormatInfo.NumberGroupSizes[0];
            negativeSign = numberFormatInfo.NegativeSign;
            isNegative = false;
            for (int i = 1; i <= (decimalPlaces + 2); i++)
            {
                changePosition[i] = false;
            }
            changePosition[1] = true;
            textDisplay = FormatString(0);
            textChange = textDisplay;
            Text = textDisplay;
        }

        private string FormatString(double d)
        {
            string formatStr = "n" + decimalPlaces;
            return d.ToString(formatStr);
        }

        private void ShowOriginal()
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

        private void TextValidated()
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
            Text = textDisplay;
        }

        private double CheckNum(double dd)
        {
            if (dd > maximum)
            {
                dd = maximum;
            }
            else if (dd < minimum)
            {
                dd = minimum;
            }
            return dd;
        }

        private void PositionChange(Keys k)
        {
            int i;
            switch (k)
            {
                case Keys.End:
                    for (i = 1; i <= (decimalPlaces + 2); i++)
                    {
                        if (changePosition[i])
                        {
                            break;
                        }
                    }
                    if (i < (decimalPlaces + 2))
                    {
                        changePosition[i] = false;
                        changePosition[decimalPlaces + 2] = true;
                        base.Invalidate();
                    }
                    return;

                case Keys.Home:
                    for (i = 1; i <= (decimalPlaces + 2); i++)
                    {
                        if (changePosition[i])
                        {
                            break;
                        }
                    }
                    if (i > 1)
                    {
                        changePosition[i] = false;
                        changePosition[1] = true;
                        base.Invalidate();
                    }
                    return;

                case Keys.Left:
                    for (i = 1; i <= (decimalPlaces + 2); i++)
                    {
                        if (changePosition[i])
                        {
                            break;
                        }
                    }
                    if (i > 1)
                    {
                        changePosition[i] = false;
                        changePosition[i - 1] = true;
                        base.Invalidate();
                    }
                    return;

                case Keys.Up:
                    return;

                case Keys.Right:
                    for (i = 1; i <= (decimalPlaces + 2); i++)
                    {
                        if (changePosition[i])
                        {
                            break;
                        }
                    }

                    if (i < (decimalPlaces + 2))
                    {
                        changePosition[i] = false;
                        changePosition[i + 1] = true;
                        base.Invalidate();
                    }
                    return;

                case Keys.Decimal:
                    if (decimalPlaces <= 0)
                    {
                        return;
                    }
                    for (i = 1; i <= (decimalPlaces + 2); i++)
                    {
                        if (changePosition[i])
                        {
                            break;
                        }
                    }
                    break;

                default:
                    return;
            }
            if (i > 1)
            {
                changePosition[i] = false;
                changePosition[1] = true;
            }
            else
            {
                changePosition[1] = false;
                changePosition[2] = true;
            }
            base.Invalidate();
        }

        private void BackSpace()
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
                        for (int i = 1; i <= xzn; i++)
                        {
                            xz[i] = xz[i + 1];
                        }
                        xzn--;
                        TextValidated();
                        base.Invalidate();
                    }
                }
                else
                {
                    int i = 1;
                    while (i <= (decimalPlaces + 2))
                    {
                        if (changePosition[i])
                        {
                            break;
                        }
                        i++;
                    }
                    if ((i > 2) && (i <= (decimalPlaces + 2)))
                    {
                        changePosition[i] = false;
                        changePosition[i - 1] = true;
                        ShowOriginal();
                        yz[i - 2] = '0';
                        TextValidated();
                        base.Invalidate();
                    }
                    else if (i == 2)
                    {
                        changePosition[i] = false;
                        changePosition[i - 1] = true;
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

        private void InsertKey(char k)
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
                        for (int i = 1; i <= xzn; i++)
                        {
                            xz[(xzn - i) + 2] = xz[(xzn - i) + 1];
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
                int i = 1;
                while (i <= (decimalPlaces + 2))
                {
                    if (changePosition[i])
                    {
                        break;
                    }
                    i++;
                }
                if ((i > 1) && (i < (decimalPlaces + 2)))
                {
                    changePosition[i] = false;
                    changePosition[i + 1] = true;
                    ShowOriginal();
                    yz[i - 1] = k;
                    TextValidated();
                    base.Invalidate();
                }
            }
        }

        private void SignalChange(Keys k)
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

        public void KeyPressed(object sender, KeyPressEventArgs e)
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

        public class NumericBoxControlDesigner : ControlDesigner
        {
            public override SelectionRules SelectionRules
            {
                get { return (base.SelectionRules & ~(SelectionRules.BottomSizeable | SelectionRules.TopSizeable)); }
            }
        }
        #endregion
    }
}
