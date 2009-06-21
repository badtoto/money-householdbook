using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing.Drawing2D;

namespace GMoney.control
{
    /// <summary>
    /// NumericBox Popup Calculator
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class NumericBoxPopup : UserControl
    {
        #region Members
        private NumericBox ctb = null;
        private bool isRestart = false;
        private Color borderColor = Color.SteelBlue;
#if DEBUG1
        private Color instructionColor = Color.Maroon;
        private Color operatorColor = Color.RoyalBlue;
        private Color numberColor = Color.Black;
#endif
        private char lastOp = '\0';
        private bool isInEquals = false;
        private Stack<double> mStack = new Stack<double>();
        private char separatorChar = '.';
        #endregion

        #region Constructor
        public NumericBoxPopup(Control ctl)
        {
            InitializeComponent();
            // double buffering
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);

            btnNegative.Text = '\u00B1'.ToString();
            btnBack.Text = '\u2190'.ToString();
            ctb = (NumericBox)ctl;

            // set separator char
            SetSeparatorChar();
        }
        #endregion

        #region Overrides
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (Pen p = new Pen(borderColor, 1))
            {
                Graphics g = e.Graphics;

                // border
                g.DrawLine(p, 0, 0, this.Width, 0);
                g.DrawLine(p, 0, 0, 0, this.Height);
                g.DrawLine(p, this.Width - 1, 0, this.Width - 1, this.Height);
                g.DrawLine(p, 0, this.Height - 1, this.Width, this.Height - 1);

                // separator line
                g.DrawLine(p, 0, 8, this.Width, 8);
            }
        }
        #endregion

        #region Events
        private void btnBack_Click(object sender, EventArgs e)
        {
            DoBackSpace();
            SetFocus();
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            AddChar(((Control)sender).Text[0]);
            SetFocus();
        }

        private void btnOperator_Click(object sender, EventArgs e)
        {
            DoOpChar(((Control)sender).Text[0]);
            SetFocus();
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            DoEnter();
        }

        private void btnNegative_Click(object sender, EventArgs e)
        {
            SetText((-(ctb.Double)).ToString());
            SetFocus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetAll();
            SetFocus();
        }
        #endregion

        #region Methods
        private void SetFocus()
        {
            ctb.SetDropDownFocus();
        }

        public void DoKeyDown(object sender, KeyEventArgs e)
        {
            DoKeyEvent(e, true);
        }

        public void DoKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                CloseThis();

            DoKeyEvent(e, false);
        }

        public void DoKeyEvent(KeyEventArgs e, bool show)
        {
            if (e.Shift)
            {
                if (e.KeyCode == Keys.Oemplus)
                    btnPlus.SetHighlight(show);
                else if (e.KeyCode == Keys.Oem1)
                    btnPlus.SetHighlight(show);

                return;
            }

            switch (e.KeyCode)
            {
                case Keys.NumPad0:
                case Keys.NumPad1:
                case Keys.NumPad2:
                case Keys.NumPad3:
                case Keys.NumPad4:
                case Keys.NumPad5:
                case Keys.NumPad6:
                case Keys.NumPad7:
                case Keys.NumPad8:
                case Keys.NumPad9:
                    CalcBtn btnNumPad = (CalcBtn)GetControlByName(this, "btn" + e.KeyCode.ToString().Substring(6));
                    btnNumPad.SetHighlight(show);
                    break;
                case Keys.D0:
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                    CalcBtn btnD = (CalcBtn)GetControlByName(this, "btn" + e.KeyCode.ToString().Substring(1));
                    btnD.SetHighlight(show);
                    break;
                case Keys.Decimal:
                case Keys.OemPeriod:
                    btnDot.SetHighlight(show);
                    break;
                case Keys.Back:
                    btnBack.SetHighlight(show);
                    break;
                case Keys.Add:
                    btnPlus.SetHighlight(show);
                    break;
                case Keys.Subtract:
                case Keys.OemMinus:
                    btnMinus.SetHighlight(show);
                    break;
                case Keys.Divide:
                case Keys.OemQuestion:
                    btnDivide.SetHighlight(show);
                    break;
                case Keys.Multiply:
                    btnMultiply.SetHighlight(show);
                    break;
                case Keys.Delete:
                    if (show)
                        ResetAll();
                    btnClear.SetHighlight(show);
                    break;
                default:
                    break;
            }
        }

        public void DoKeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            switch (c)
            {
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
                case '.':
                    AddChar(c);
                    break;
                case '\b':
                    DoBackSpace();
                    break;
                case '+':
                case '-':
                case '/':
                case '*':
                    DoOpChar(c);
                    break;
                case '=':
                case '\r':
                    DoEnter();
                    break;
                default:
                    break;
            }
        }

        private Control GetControlByName(Control control, string name)
        {
            foreach (Control ctl in control.Controls)
            {
                if (ctl.Name == name)
                    return ctl;
                else if (ctl.Controls.Count > 0)
                    return GetControlByName(ctl, name);
            }
            return null;
        }

        private void ResetAll()
        {
            SetText("0");
            OpenReset();
        }

        public void OpenReset()
        {
            lastOp = '\0';
            isRestart = true;
            mStack.Clear();
        }

        private void DoBackSpace()
        {
            if (ctb.Double.ToString().Length > 1)
            {
                SetText(ctb.Double.ToString().Substring(0, ctb.Double.ToString().Length - 1));
            }
            else
            {
                SetText("0");
            }
        }

        private void DoEnter()
        {
            DoEqualsChar();
            CloseThis();
        }

        private void CloseThis()
        {
            ctb.CloseDropDown();
        }

        private void SetSeparatorChar()
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            string sep = culture.NumberFormat.NumberDecimalSeparator;
            if (!string.IsNullOrEmpty(sep))
                separatorChar = sep[0];
        }

        private void SetText(string s)
        {
            ctb.Init();
            ctb.Text = s;
        }

        private void AddChar(char c)
        {
            if (isRestart)
            {
                SetText("0");
                isRestart = false;
            }
            ctb.KeyPressed(null, new KeyPressEventArgs(c));
        }

        private void DoOpChar(char op)
        {
            if (isInEquals)
            {
                mStack.Clear();
                isInEquals = false;
            }
            mStack.Push(ctb.Double);
            DoLastOp();
            lastOp = op;
        }

        private void DoEqualsChar()
        {
            if (lastOp == '\0')
                return;

            if (!isInEquals)
            {
                isInEquals = true;
                mStack.Push(ctb.Double);
            }
            DoLastOp();
        }

        private void DoLastOp()
        {
            isRestart = true;
            if (lastOp == '\0' || mStack.Count == 1)
                return;

            double valTwo = mStack.Pop();
            double valOne = mStack.Pop();
            switch (lastOp)
            {
                case '+':
                    mStack.Push(valOne + valTwo);
                    break;
                case '-':
                    mStack.Push(valOne - valTwo);
                    break;
                case '*':
                    mStack.Push(valOne * valTwo);
                    break;
                case '/':
                    if (valTwo == 0)
                    {
                        ResetAll();
                        SetText("Error");
                        CloseThis();
                        return;
                    }
                    mStack.Push(valOne / valTwo);
                    break;
                default:
                    break;
            }
            SetText(mStack.Peek().ToString());
            if (isInEquals)
                mStack.Push(valTwo);
        }
        #endregion

#if DEBUG1
        #region Public Properties
        public Color CaculatorBorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }

        public Color CaculatorBackColor
        {
            get
            {
                return BackColor;
            }
            set
            {
                BackColor = value;
            }
        }

        public Color CaculatorNumberColor
        {
            get { return numberColor; }
            set { numberColor = value; }
        }

        public Color CaculatorOperatorColor
        {
            get { return operatorColor; }
            set { operatorColor = value; }
        }

        public Color CaculatorInstructionColor
        {
            get { return instructionColor; }
            set { instructionColor = value; }
        }

        #endregion
#endif
    }

    /// <summary>
    /// Button for Calculator
    /// </summary>
    [DesignTimeVisible(false)]
    class CalcBtn : Button
    {
        #region Members
        private Color normalBackColor;
        #endregion

        #region Properties
        private Color borderColor = Color.FromArgb(127, 157, 185);
        [Category("Appearance"), Description("Border Color"), Browsable(true)]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                if (borderColor != value)
                {
                    borderColor = value;
                    base.Invalidate();
                }
            }
        }

        private int borderWidth = 1;
        [Category("Appearance"), Description("Border Width"), DefaultValue(1), Browsable(true)]
        public int BorderWidth
        {
            get { return borderWidth; }
            set
            {
                if (borderWidth != value)
                {
                    borderWidth = value;
                    base.Invalidate();
                }
            }
        }

        private Color highlightColor = Color.Bisque;
        [Category("Appearance"), Description("Highlight Color"), DefaultValue(typeof(Color), "Bisque"), Browsable(true)]
        public Color HighlightColor
        {
            get { return highlightColor; }
            set { highlightColor = value; }
        }
        #endregion

        #region Constructor
        public CalcBtn()
        {
            SetStyle(ControlStyles.UserPaint, true);
        }
        #endregion

        #region Overrides
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // draw background
            DrawBackground(g);

            // draw text
            DrawText(g);

            // draw border
            DrawBorder(g);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            SetHighlight(true);
            DrawBackground(CreateGraphics());
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            SetHighlight(false);
            DrawBackground(CreateGraphics());
        }
        #endregion

        #region Methods
        public void SetHighlight(bool show)
        {
            if (show)
            {
                normalBackColor = this.BackColor;
                this.BackColor = highlightColor;
            }
            else
            {
                this.BackColor = normalBackColor;
            }
        }

        public void DrawBackground(Graphics g)
        {
            using (SolidBrush sb = new SolidBrush(BackColor))
            {
                g.FillRectangle(sb, ClientRectangle);
            }
        }

        public void DrawText(Graphics g)
        {
            using (SolidBrush sb = new SolidBrush(ForeColor))
            {
                using (StringFormat sf = new StringFormat())
                {
                    // text align center
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    sf.FormatFlags = StringFormatFlags.NoWrap;
                    sf.Trimming = StringTrimming.None;

                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.DrawString(Text, Font, sb, ClientRectangle, sf);
                    g.SmoothingMode = SmoothingMode.None;
                }
            }
        }

        public void DrawBorder(Graphics g)
        {
            if (borderWidth > 0 && borderColor != Color.Transparent)
            {
                using (Pen p = new Pen(borderColor, borderWidth))
                {
                    // up
                    g.DrawLine(p, 0, 0, this.Width, 0);
                    // left
                    g.DrawLine(p, 0, 0, 0, this.Height);
                    // right
                    g.DrawLine(p, this.Width - 1, 0, this.Width - 1, this.Height);
                    // down
                    g.DrawLine(p, 0, this.Height - 1, this.Width, this.Height - 1);
                }
            }
        }
        #endregion
    }
}