using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Money.util;
using Money.db;
using Money.control;
using System.Collections;
using BrightIdeasSoftware;
using Money.instance;

namespace Money.form
{
    public partial class OptionForm : Form
    {
        #region Members
        private Hashtable editOptimalHT = null;
        private Hashtable editFixedHT = null;
        private Hashtable editUserHT = null;
        #endregion

        #region Constructor
        public OptionForm()
        {
            InitializeComponent();

            this.Text = string.Format("{0} - {1}", CommonUtils.CaptionName, this.Text);

            this.colUserDeleted.AspectGetter = delegate(object row)
            {
                return ((BaseTbl)row).DeleteFlg == 0 ? "False" : "True";
            };

            this.colFixFrequency.AspectGetter = delegate(object row)
            {
                return row;
            };

            this.colFixFrequency.AspectToStringConverter = delegate(object row)
            {
                string cellStr = "";
                if (((FixedDataTbl)row).Interval > 1)
                    cellStr = "Every " + (((FixedDataTbl)row).Interval - 1) + " {0}";
                else
                    cellStr = "Every {0}";

                string type = "";
                switch (((FixedDataTbl)row).IntervalType)
                {
                    case (int)CommonUtils.IntervalType.EveryDay:
                        type = "Day";
                        break;
                    case (int)CommonUtils.IntervalType.EveryWeek:
                        type = "Week";
                        break;
                    case (int)CommonUtils.IntervalType.EveryMonth:
                        type = "Month";
                        break;
                    default:
                        type = "Year";
                        break;
                }

                if (((FixedDataTbl)row).Interval > 1)
                    type += "s";


                cellStr = string.Format(cellStr, type);
                if (!string.IsNullOrEmpty(((FixedDataTbl)row).IntervalDetail))
                {
                    cellStr += " at ";
                    if (((FixedDataTbl)row).IntervalType == (int)CommonUtils.IntervalType.EveryMonth)
                    {
                        cellStr += "Day ";
                    }
                    cellStr += ((FixedDataTbl)row).IntervalDetail;
                }

                return cellStr;
            };

            SetInitData();
        }
        #endregion

        #region Methods
        private void SetUserListView()
        {
            MasterDB mdb = new MasterDB();
            folvUser.ClearObjects();
            folvUser.SetObjects(mdb.GetUserTable());
        }

        private void SetOptimalListView()
        {
            MasterDB mdb = new MasterDB();
            folvOptimal.ClearObjects();
            folvOptimal.SetObjects(mdb.GetSubOptimalList());
        }

        private void SetFixedListView()
        {
            FixedDataDB fdb = new FixedDataDB();
            folvFixed.ClearObjects();
            folvFixed.SetObjects(fdb.GetFixedDataList());
        }

        private void SetPasswordControlActive(bool active)
        {
            tbOldPass.Enabled = active;
            tbNewPass.Enabled = active;
            tbNewPassConf.Enabled = active;
        }

        private void ClearInputPassword()
        {
            tbOldPass.Text = "";
            tbNewPass.Text = "";
            tbNewPassConf.Text = "";
        }

        private void SetIntervalTypeItems(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("Day");
            cb.Items.Add("Week");
            cb.Items.Add("Month");
            cb.Items.Add("Year");
        }

        private void SetDeleteFlgItems(ComboBox cb)
        {
            cb.Items.Clear();
            cb.Items.Add("False");
            cb.Items.Add("True");
        }

        private void SetInitData()
        {
            SystemDB sys = new SystemDB();
            MasterDB mdb = new MasterDB();
            // 1. Basic
            //login password
            cbUseLoginPass.Checked = !string.IsNullOrEmpty(sys.GetLoginPassword());
            SetPasswordControlActive(cbUseLoginPass.Checked);

            // user list
            editUserHT = new Hashtable();
            SetUserListView();

            // 2. fixed data
            editFixedHT = new Hashtable();
            SetFixedListView();
            frequencyCtl.IntervalType = 0;

            // 3. optimal data
            editOptimalHT = new Hashtable();
            SetOptimalListView();

            // fixed tab combo box
            mdb.GetSubListFoxFixed(cbSub);

            // apply button disabled
            btnApply.Enabled = false;
            btnOK.Focus();
        }

        private bool CheckTabSecurity()
        {
            bool bRet = false;

            try
            {
                SystemDB sys = new SystemDB();
                // 1. Password Change
                if (cbUseLoginPass.Checked)
                {
                    if (!string.IsNullOrEmpty(tbOldPass.Text) || !string.IsNullOrEmpty(tbNewPass.Text) || !string.IsNullOrEmpty(tbNewPassConf.Text))
                    {
                        if (tbOldPass.Text != sys.GetLoginPassword())
                        {
                            tcOption.SelectedIndex = 0;
                            CommonUtils.ShowError(Properties.Resources.MSG_OPT_0001);
                            ClearInputPassword();
                            return false;
                        }

                        if (tbNewPass.Text != tbNewPassConf.Text)
                        {
                            tcOption.SelectedIndex = 0;
                            CommonUtils.ShowError(Properties.Resources.MSG_OPT_0002);
                            ClearInputPassword();
                            return false;
                        }
                    }
                }

                bRet = true;
            }
            catch { }
            return bRet;
        }

        private void DeleteFixedItem()
        {
            if (folvFixed.SelectedObject == null)
            {
                CommonUtils.ShowError(Properties.Resources.MSG_SELECT);
                return;
            }

            if (CommonUtils.ShowWarningWithCancel(Properties.Resources.MSG_DELETE_CONFIRM) != DialogResult.OK)
            {
                return;
            }

            FixedDataDB fdb = new FixedDataDB();

            if (fdb.DelFixedData(((FixedDataTbl)folvFixed.SelectedObject).ID))
            {
                // refresh listview
                SetFixedListView();
            }
        }

        private bool ApplyChanges()
        {
            bool bRet = false;

            try
            {
                // 1. password
                if (CheckTabSecurity())
                {
                    if ((!(string.IsNullOrEmpty(tbOldPass.Text)
                      && string.IsNullOrEmpty(tbNewPass.Text)
                      && string.IsNullOrEmpty(tbNewPassConf.Text)))
                      || !cbUseLoginPass.Checked
                        )
                    {
                        SystemDB sys = new SystemDB();
                        if (!sys.ChangeLoginPassword(tbNewPass.Text))
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }

                if (editUserHT.Values.Count > 0)
                {
                    MasterDB mdb = new MasterDB();
                    if (!mdb.UpdUserData(editUserHT.Values))
                    {
                        return false;
                    }
                    editUserHT.Clear();
                }

                // 2. fixed data
                if (editFixedHT.Values.Count > 0)
                {
                    FixedDataDB fdb = new FixedDataDB();
                    if (!fdb.UpdFixedData(editFixedHT.Values))
                    {
                        return false;
                    }
                    editFixedHT.Clear();
                }
                BillDB edb = new BillDB();
                edb.CheckExpenseFixed();
                ((MoneyForm)Owner).SetDispData(-1, DateTime.Now);


                // 3. optimal data
                if (editOptimalHT.Values.Count > 0)
                {
                    MasterDB mdb = new MasterDB();
                    if (!mdb.UpdOptimalData(editOptimalHT.Values))
                    {
                        return false;
                    }
                    editOptimalHT.Clear();
                }
                bRet = true;
            }
            catch { }
            return bRet;
        }

        private void AddFixedData()
        {
            FixedDataTbl tbl = new FixedDataTbl();
            tbl.UserId = (int)((ListItem)cbUser.SelectedItem).ID;
            tbl.SubId = (int)((ListItem)cbSub.SelectedItem).ID;
            tbl.Interval = frequencyCtl.Interval;
            tbl.IntervalType = frequencyCtl.IntervalType;
            tbl.IntervalDetail = frequencyCtl.IntervalDetail;
            tbl.StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            tbl.Amount = ctbAmount.Double;

            FixedDataDB fdb = new FixedDataDB();
            if (fdb.NewFixedData(tbl))
            {
                // clear amount textbox
                ctbAmount.Text = "0";
                // refresh
                SetFixedListView();
            }
        }

        private void AddUserData()
        {
            MasterDB mdb = new MasterDB();
            if (mdb.NewUser(tbUserName.Text.Trim()) != -1)
            {
                // clear amount textbox
                tbUserName.Text = "";
                // refresh
                SetUserListView();
            }
        }
        #endregion

        #region Events
        private void cbUseLoginPass_CheckedChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
            SetPasswordControlActive(cbUseLoginPass.Checked);
            if (!cbUseLoginPass.Checked)
            {
                ClearInputPassword();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (btnApply.Enabled)
            {
                if (!ApplyChanges())
                {
                    return;
                }
                // when chart, repaint
                if (((MoneyForm)Owner).GetTabIndex() == 1)
                {
                    ((MoneyForm)Owner).SetChart();
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (!ApplyChanges())
            {
                return;
            }
            btnApply.Enabled = false;
        }

        private void tpSecurity_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        void ListItemValueChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void option_CellEditStarting(object sender, CellEditEventArgs e)
        {
            if (e.Column.Text == colOptOptimal.Text || e.Column.Text == colOptRange.Text || e.Column.Text == colFixAmount.Text)
            {
                NumericBox ctb = new NumericBox();
                ctb.Bounds = e.CellBounds;
                ctb.Font = ((ObjectListView)sender).Font;
                ctb.AllowNegative = false;
                if (e.Column.Text == colOptOptimal.Text)
                {
                    ctb.Text = ((SubTbl)e.RowObject).Optimal.ToString();
                }
                else if (e.Column.Text == colOptRange.Text)
                {
                    ctb.Text = ((SubTbl)e.RowObject).Range.ToString();
                }
                else if (e.Column.Text == colFixAmount.Text)
                {
                    ctb.Text = ((FixedDataTbl)e.RowObject).Amount.ToString();
                }
                ctb.TextChanged += new EventHandler(ListItemValueChanged);
                e.Control = ctb;
            }
            else if (e.Column.Text == colFixFrequency.Text)
            {
                FrequencyCtl ctl = new FrequencyCtl();
                ctl.Bounds = e.CellBounds;
                ctl.Font = ((ObjectListView)sender).Font;
                ctl.Interval = ((FixedDataTbl)e.RowObject).Interval;
                ctl.IntervalType = ((FixedDataTbl)e.RowObject).IntervalType;
                ctl.IntervalDetail = ((FixedDataTbl)e.RowObject).IntervalDetail;
                ctl.FrequencyCtlValueChanged += new EventHandler(ListItemValueChanged);
                e.Control = ctl;
            }
            else if (e.Column.Text == colUserName.Text)
            {
                TextBox tb = new TextBox();
                tb.Bounds = e.CellBounds;
                tb.Font = ((ObjectListView)sender).Font;
                tb.Text = ((BaseTbl)e.RowObject).Name;
                tb.TextChanged += new EventHandler(ListItemValueChanged);
                e.Control = tb;
            }
            else if (e.Column.Text == colUserDeleted.Text)
            {
                ComboBox cb = new ComboBox();
                cb.Bounds = e.CellBounds;
                cb.Font = ((ObjectListView)sender).Font;
                cb.DropDownStyle = ComboBoxStyle.DropDownList;
                SetDeleteFlgItems(cb);
                cb.SelectedIndex = ((BaseTbl)e.RowObject).DeleteFlg;
                cb.SelectedIndexChanged += new EventHandler(ListItemValueChanged);
                e.Control = cb;
            }
            else if (e.Column.Text == colFixStartDate.Text)
            {
                DateTimePicker dt = new DateTimePicker();
                dt.Bounds = e.CellBounds;
                dt.Font = ((ObjectListView)sender).Font;
                DateTime dtt = ((FixedDataTbl)e.RowObject).StartDate;
                if (dtt < dt.MinDate)
                    dtt = dt.MinDate;
                dt.Value = dtt;
                dt.ValueChanged += new EventHandler(ListItemValueChanged);
                e.Control = dt;
            }
            else if (e.Column.Text == colFixEndDate.Text)
            {
                DateTimePicker dt = new DateTimePicker();
                dt.Bounds = e.CellBounds;
                dt.Font = ((ObjectListView)sender).Font;
                DateTime dtt = ((FixedDataTbl)e.RowObject).EndDate;
                if (dtt > dt.MaxDate)
                    dtt = dt.MaxDate;
                dt.Value = dtt;
                dt.ValueChanged += new EventHandler(ListItemValueChanged);
                e.Control = dt;
            }
            else
            {
                return;
            }
        }

        private void option_CellEditFinishing(object sender, CellEditEventArgs e)
        {
            // remove event handler
            if (e.Column.Text == colOptOptimal.Text)
            {
                ((SubTbl)e.RowObject).Optimal = ((NumericBox)e.Control).Double;
                ((NumericBox)e.Control).TextChanged -= new EventHandler(ListItemValueChanged);
            }
            else if (e.Column.Text == colOptRange.Text)
            {
                ((SubTbl)e.RowObject).Range = ((NumericBox)e.Control).Double;
                ((NumericBox)e.Control).TextChanged -= new EventHandler(ListItemValueChanged);
            }
            else if (e.Column.Text == colFixAmount.Text)
            {
                ((FixedDataTbl)e.RowObject).Amount = ((NumericBox)e.Control).Double;
                ((NumericBox)e.Control).TextChanged -= new EventHandler(ListItemValueChanged);
            }
            else if (e.Column.Text == colUserDeleted.Text)
            {
                ((BaseTbl)e.RowObject).DeleteFlg = ((ComboBox)e.Control).SelectedIndex;
                ((ComboBox)e.Control).SelectedIndexChanged -= new EventHandler(ListItemValueChanged);
            }
            else if (e.Column.Text == colUserName.Text)
            {
                ((BaseTbl)e.RowObject).Name = ((TextBox)e.Control).Text;
                ((TextBox)e.Control).TextChanged -= new EventHandler(ListItemValueChanged);
            }
            else if (e.Column.Text == colFixFrequency.Text)
            {
                ((FixedDataTbl)e.RowObject).Interval = ((FrequencyCtl)e.Control).Interval;
                ((FixedDataTbl)e.RowObject).IntervalType = ((FrequencyCtl)e.Control).IntervalType;
                ((FixedDataTbl)e.RowObject).IntervalDetail = ((FrequencyCtl)e.Control).IntervalDetail;
                ((FrequencyCtl)e.Control).FrequencyCtlValueChanged -= new EventHandler(ListItemValueChanged);
            }
            else if (e.Column.Text == colFixStartDate.Text)
            {
                if (((DateTimePicker)e.Control).Value > ((FixedDataTbl)e.RowObject).EndDate)
                {
                    CommonUtils.ShowError(Properties.Resources.MSG_OPT_0003);
                    e.Cancel = true;
                    return;
                }
                DateTime tmp = ((DateTimePicker)e.Control).Value;
                ((FixedDataTbl)e.RowObject).StartDate = new DateTime(tmp.Year, tmp.Month, tmp.Day, 0, 0, 0);
                ((DateTimePicker)e.Control).ValueChanged -= new EventHandler(ListItemValueChanged);
            }
            else if (e.Column.Text == colFixEndDate.Text)
            {
                DateTime tmp = ((DateTimePicker)e.Control).Value;
                ((FixedDataTbl)e.RowObject).EndDate = new DateTime(tmp.Year, tmp.Month, tmp.Day, 23, 59, 59);
                if (((DateTimePicker)e.Control).Value < ((FixedDataTbl)e.RowObject).StartDate)
                {
                    CommonUtils.ShowError(Properties.Resources.MSG_OPT_0003);
                    e.Cancel = true;
                    return;
                }
                ((DateTimePicker)e.Control).ValueChanged -= new EventHandler(ListItemValueChanged);
            }

            // Any updating will have been down in the SelectedIndexChanged event handler
            // Here we simply make the list redraw the involved ListViewItem
            ((ObjectListView)sender).RefreshItem(e.ListViewItem);

            // We have updated the model object, so we cancel the auto update
            e.Cancel = true;

            string ObjectName = ((BrightIdeasSoftware.ObjectListView)(sender)).Name;
            if (ObjectName == this.folvFixed.Name)
            {
                FixedDataTbl tbl = (FixedDataTbl)e.RowObject;
                editFixedHT[tbl.ID] = tbl;
            }
            else if (ObjectName == this.folvOptimal.Name)
            {
                SubTbl tbl = (SubTbl)e.RowObject;
                editOptimalHT[tbl.ID] = tbl;
            }
            else if (ObjectName == this.folvUser.Name)
            {
                BaseTbl tbl = (BaseTbl)e.RowObject;
                editUserHT[tbl.ID] = tbl;
            }
        }

        private void btnFixedNew_Click(object sender, EventArgs e)
        {
            AddFixedData();
        }

        private void folvFixed_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteFixedItem();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteFixedItem();
        }

        private void btnFixedDel_Click(object sender, EventArgs e)
        {
            DeleteFixedItem();
        }

        private void btnUserNew_Click(object sender, EventArgs e)
        {
            AddUserData();
        }

        private void ctbAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddFixedData();
            }
        }

        private void tbUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddUserData();
            }
        }

        private void tcOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tcOption.SelectedIndex)
            {
                case 1:
                    MasterDB mdb = new MasterDB();
                    mdb.GetUserList(cbUser);
                    break;
                default:
                    break;
            }
        }

        private void optionAcceptButtonOff(object sender, EventArgs e)
        {
            this.AcceptButton = null;
        }

        private void optionAcceptButtonOn(object sender, EventArgs e)
        {
            this.AcceptButton = btnOK;
        }
        #endregion
    }
}