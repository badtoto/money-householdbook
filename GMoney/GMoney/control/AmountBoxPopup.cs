
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace Money.control
{
    public partial class AmountBoxPopup : Form
    {
        #region Private Members
        private NumericBox ctb = null;
        private bool isRestart = false;
        private Color borderColor = Color.SteelBlue;
#if DEBUG
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
        public AmountBoxPopup(Control ctl)
        {
            InitializeComponent();
            // double buffering
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);

            btnNegative.Text = '\u00B1'.ToString();
            btnBack.Text = '\u2190'.ToString();
            ctb = (NumericBox)ctl;

            btnDot.Enabled = ctb.DecimalPlaces > 0;

            // set separator char
            SetSeparatorChar();

            #region Set Position
            int x = ctl.Location.X;
            int y = ctl.Location.Y;
            Control parent = ctl.Parent;
            while (null != parent.Parent)
            {
                x += parent.Location.X;
                y += parent.Location.Y;
                parent = parent.Parent;
                if (null != parent && parent is Form)
                    break;
            }

            Form f = ctl.FindForm();
            Point pt = f.PointToScreen(new Point(x, y));
            Rectangle screen = Screen.PrimaryScreen.WorkingArea;
            if (screen.Height < pt.Y + Height + ctl.Height)
                pt = new Point(pt.X + ctl.Width - Width, pt.Y - Height - 1);
            else
                pt = new Point(pt.X + ctl.Width - Width, pt.Y + ctl.Height + 1);
            this.Location = pt;
            #endregion
        }
        #endregion

        #region Override
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Pen p = new Pen(borderColor, 1);
            Graphics g = e.Graphics;
            g.DrawLine(p, 0, 0, this.Width, 0);
            g.DrawLine(p, 0, 0, 0, this.Height);
            g.DrawLine(p, this.Width - 1, 0, this.Width - 1, this.Height);
            g.DrawLine(p, 0, this.Height - 1, this.Width, this.Height - 1);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            char c = e.KeyChar;
            EventArgs ev = EventArgs.Empty;
            if (Char.IsDigit(c) || c == separatorChar)
                AddChar(c);
            else
            {
                switch (c)
                {
                    case '\b':
                        DoBackSpace(); ;
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
        }
        #endregion

        #region Event
        private void btnBack_Click(object sender, EventArgs e)
        {
            DoBackSpace();
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            AddChar(((Button)sender).Text[0]);
        }

        private void btnOperator_Click(object sender, EventArgs e)
        {
            DoOpChar(((Button)sender).Text[0]);
        }

        private void btnResult_Click(object sender, EventArgs e)
        {
            DoEnter();
        }

        private void btnNegative_Click(object sender, EventArgs e)
        {
            SetText((-(ctb.Double)).ToString());
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetAll();
        }

        private void CalculatorForm_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Methods
        private void ResetAll()
        {
            SetText("0");
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
            Close();
            ctb.Focus();
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
            ctb.Text = s;
            tableLayoutPanel.Focus();
        }

        private void AddChar(char c)
        {
            string s = ctb.Double.ToString();
            if (c == separatorChar &&
                s.Contains(separatorChar.ToString()) && !isRestart)
                return;

            if (s == "0")
                s = c.ToString();
            else
                s += c;
            SetText(s);
            if (isRestart)
            {
                SetText(c.ToString());
                isRestart = false;
            }
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
                        this.Close();
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

#if DEBUG
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
}