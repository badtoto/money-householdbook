using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Money.control
{
    [DefaultEvent("DateChanged")]
    public partial class MonthYearSelector : UserControl
    {
        #region event handler
        public event EventHandler DateChanged;
        #endregion

        #region private members
        private int minYear = Money.util.MessageUtils.SYSTEM_START_YEAR;
        private bool monthVisible = true;
        #endregion

        #region constructor
        public MonthYearSelector()
        {
            InitializeComponent();
            SetDropDownList();
        }
        #endregion

        #region private function
        private void SetDropDownList()
        {
            int staYear = minYear;
            int endYear = DateTime.Now.Year + 1;

            cbDate.Items.Clear();
            for (int i = staYear; i <= endYear; i++)
            {
                if (monthVisible)
                {
                    for (int j = 1; j < 13; j++)
                    {
                        cbDate.Items.Add(string.Format("{0}/{1:00}", i, j));
                    }
                }
                else
                {
                    cbDate.Items.Add(i.ToString());
                }
            }
            if (monthVisible)
            {
                cbDate.Text = string.Format("{0}/{1:00}", DateTime.Now.Year, DateTime.Now.Month);
            }
            else
            {
                cbDate.Text = DateTime.Now.Year.ToString();
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            cbDate.SelectedIndex--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            cbDate.SelectedIndex++;
        }

        private void Date_ValueChanged(object sender, EventArgs e)
        {
            btnPrev.Enabled = !(cbDate.SelectedIndex == 0);
            btnNext.Enabled = !(cbDate.SelectedIndex == cbDate.Items.Count - 1);
            OnDateChanged(e);
        }

        #endregion

        #region public properties
        [Category("Category"), DefaultValue(true), Description("Montn Selector Visibility"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Bindable(true)]
        public bool MonthVisible
        {
            set
            {
                monthVisible = value;
                int moveDistance = Font.Height + 3;
                if (monthVisible)
                {
                    cbDate.Size = new System.Drawing.Size((cbDate.Width + moveDistance), cbDate.Height);
                    btnNext.Location = new System.Drawing.Point((btnNext.Location.X + moveDistance), btnNext.Location.Y);
                }
                else
                {
                    cbDate.Size = new System.Drawing.Size((cbDate.Width - moveDistance), cbDate.Height);
                    btnNext.Location = new System.Drawing.Point((btnNext.Location.X - moveDistance), btnNext.Location.Y);
                }
                SetDropDownList();
            }
            get { return monthVisible; }
        }

        [Category("Category"), Description("Minimum Year"), Browsable(true), Bindable(true)]
        public int MinYear
        {
            set { minYear = value; }
            get { return minYear; }
        }

        [Category("Category"), Description("Date"), Browsable(false), Bindable(true)]
        public string Value
        {
            get
            {
                return cbDate.Text;
            }
        }

        [Category("Category"), Description("Year"), Browsable(false), Bindable(true)]
        public int Year
        {
            get
            {
                return int.Parse(cbDate.Text.Substring(0, 4));
            }
        }
        #endregion

        #region protected function
        protected void OnDateChanged(EventArgs e)
        {
            if (DateChanged != null)
            {
                DateChanged(this, e);
            }
        }
        #endregion
    }
}
