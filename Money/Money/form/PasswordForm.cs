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

namespace Money.form
{
    public partial class PasswordForm : Form
    {
        #region Constructor
        public PasswordForm()
        {
            InitializeComponent();
            this.Text = CommonUtils.CaptionName;
        }
        #endregion

        #region Events
        private void btnOK_Click(object sender, EventArgs e)
        {
            SystemDB sys = new SystemDB();
            if (tbPwd.Text == sys.GetLoginPassword())
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                CommonUtils.ShowError(Properties.Resources.MSG_PWD_INVALID);
                tbPwd.Text = "";
                tbPwd.Focus();
            }
        }
        #endregion
    }
}