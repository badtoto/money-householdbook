﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Money.db;
using Money.control;
using Money.util;
using System.Timers;
using Money.instance;

namespace Money.form
{
    public partial class BillForm : Form
    {
        #region Members
        private bool IsUpdate = false;
        private BillTbl tbl = new BillTbl();
        #endregion

        #region Constructor
        public BillForm(int id)
        {
            InitializeComponent();

            this.Text = string.Format("{0} - {1}", CommonUtils.CaptionName, this.Text);

            tbl.ID = id;
            IsUpdate = id != 0;

            SetDispData();
        }
        #endregion

        #region Methods
        private void SetDispData()
        {
            if (IsUpdate)
            {
                BillDB bdb = new BillDB();
                if (bdb.GetBill(tbl))
                {
                    // set bill type
                    cbBillType.SelectedIndex = tbl.BillType == (int)CommonUtils.BillType.Income ? (int)CommonUtils.BillType.Income : (int)CommonUtils.BillType.Expense;
                    // set annual budget
                    cbAnnualBudget.Checked = tbl.IsAnnualBudget;
                }
                else
                {
                    return;
                }
            }
            else
            {
                // default expense
                cbBillType.SelectedIndex = 0;
            }

            // Get Master List
            MasterDB db = new MasterDB();
            db.GetMajorList(cbMajor, cbBillType.SelectedIndex, IsUpdate);
            db.GetUserList(cbUser);
            //db.GetPaymentList(cbPayment);

            if (IsUpdate)
            {
                // set user
                for (int i = 0; i < cbUser.Items.Count; i++)
                {
                    ListItem item = (ListItem)cbUser.Items[i];
                    if (((int)item.ID) == tbl.UserId)
                    {
                        cbUser.SelectedIndex = i;
                        break;
                    }
                }

                // set category
                for (int i = 0; i < cbMajor.Items.Count; i++)
                {
                    ListItem item = (ListItem)cbMajor.Items[i];
                    if (((int)item.ID) == tbl.MajorId)
                    {
                        cbMajor.SelectedIndex = i;
                        break;
                    }
                }
                for (int i = 0; i < cbSub.Items.Count; i++)
                {
                    ListItem item = (ListItem)cbSub.Items[i];
                    if (((int)item.ID) == tbl.SubId)
                    {
                        cbSub.SelectedIndex = i;
                        break;
                    }
                }
                dtpDate.Value = tbl.Date;
                double d = tbl.Amount;
                if (d < 0)
                    d = -d;
                tbAmount.Text = d.ToString();
                tbRemarks.Text = tbl.Remarks;

                // component visible
                this.AcceptButton = btnOK;
                this.cbBillType.Visible = false;
                this.labelType.Visible = false;
                this.cbSplit.Visible = false;
                this.nudMonths.Visible = false;
                this.labelSplit.Visible = false;
                this.tbAmount.Select();
            }
        }

        private void AddBill()
        {
            bool resetListView = true;

            if (tbl.ID == 0)
            {
                tbl = new BillTbl();
            }
            tbl.Date = dtpDate.Value;
            tbl.BillType = cbBillType.SelectedIndex;
            if (cbUser.SelectedItem != null)
            {
                tbl.UserId = (int)((ListItem)cbUser.SelectedItem).ID;
            }
            else
            {
                string user_name = cbUser.Text.Trim();

                if (!string.IsNullOrEmpty(user_name))
                {
                    MasterDB mdb = new MasterDB();
                    int user_id = mdb.NewUser(user_name);
                    if (user_id == -1)
                    {
                        return;
                    }
                    tbl.UserId = user_id;
                    // reset user combobox
                    mdb.GetUserList(cbUser, new ListItem(user_id, user_name));
                }
                else
                {
                    tbl.UserId = Money.util.CommonUtils.DEFAULT_USER_ID;
                    tbl.UserName = user_name;
                }
            }
            tbl.SubId = (int)((ListItem)cbSub.SelectedItem).ID;
            tbl.Amount = tbAmount.Double;
            tbl.Remarks = tbRemarks.Text;
            //tbl.PaymentId = (int)((ListItem)cbPayment.SelectedItem).ID;
            tbl.AnnualBudget = cbAnnualBudget.Checked ? 1 : 0;
            if (cbSplit.Checked)
            {
                tbl.SplitMonths = Decimal.ToInt32(nudMonths.Value);
            }

            BillDB db = new BillDB();
            if (tbl.ID != 0)
            {
                if (!db.UpdBill(tbl))
                {
                    return;
                }
            }
            else
            {
                if (tbl.Amount != 0)
                {
                    if (!db.NewBill(tbl))
                    {
                        return;
                    }
                }
                else
                {
                    resetListView = false;
                }
            }

            if (resetListView)
                ((MoneyForm)Owner).SetDispData(tbl.ID, tbl.Date);

            if (!IsUpdate)
            {
                cbAnnualBudget.Checked = false;
                tbAmount.Text = "0";
                tbRemarks.Text = string.Empty;
                tbl = new BillTbl();
            }

            tbAmount.Focus();
        }

        private void SetSuggestRemarks()
        {
            tbRemarks.AutoCompleteCustomSource.Clear();
            BillDB db = new BillDB();
            int s_id = (int)((ListItem)cbSub.SelectedItem).ID;
            tbRemarks.AutoCompleteCustomSource.AddRange(db.GetRemarkSuggests(s_id));
        }
        #endregion

        #region Events
        private void cbMajor_SelectedIndexChanged(object sender, EventArgs e)
        {
            MasterDB db = new MasterDB();
            db.GetSubList(cbSub, (int)((ListItem)cbMajor.SelectedItem).ID, IsUpdate);
            if (cbSub.Items.Count == 1)
            {
                tbAmount.Focus();
            }
            else
            {
                cbSub.Focus();
            }
        }

        private void cbSub_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsUpdate)
            {
                tbl = new BillTbl();
            }
            SetSuggestRemarks();
            tbAmount.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            AddBill();

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void tbEnter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddBill();
            }
        }

        private void cbBillType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbAnnualBudget.Visible = (cbBillType.SelectedIndex == 0);

            MasterDB db = new MasterDB();
            db.GetMajorList(cbMajor, cbBillType.SelectedIndex, IsUpdate);
        }

        private void cbSplit_CheckedChanged(object sender, EventArgs e)
        {
            nudMonths.Enabled = cbSplit.Checked;
        }
        #endregion
    }
}