using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GMoney.form;
using GMoney.util;
using System.Threading;
using GMoney.db;

namespace GMoney
{
    static class Program
    {
        /// <summary>
        /// Application Main Entry Point.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                if (!MyProcess.ShowPrevProcess())
                {
                    #region Check Database
                    if (!BaseDB.CheckDB())
                    {
                        CommonUtils.ShowError(Properties.Resources.MSG_INI_0002);
                        return;
                    }
                    #endregion

                    #region Initialize Form Style
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    #endregion

                    #region Login Check
                    SystemDB sys = new SystemDB();
                    if (!string.IsNullOrEmpty(sys.GetLoginPassword()))
                    {
                        PasswordForm login = new PasswordForm();
                        if (login.ShowDialog() != DialogResult.OK)
                        {
                            return;
                        }
                        login.Dispose();
                    }
                    #endregion

                    #region Vacuum Check
                    string lastVacuumDate = sys.GetLastVacuumDate();
                    if (string.IsNullOrEmpty(lastVacuumDate))
                    {
                        sys.ChangeLastVacuumDate(DateTime.Now.ToShortDateString());
                    }
                    else
                    {
                        string oneMonthAgo = DateTime.Now.AddMonths(-1).ToShortDateString();
                        if (oneMonthAgo.CompareTo(lastVacuumDate) > 0)
                        {
                            if (CommonUtils.ShowWarningWithCancel(Properties.Resources.MSG_VACUUM_CHECK.Replace("\\r\\n", "\r\n")) == DialogResult.OK)
                            {
                                if (BaseDB.DoVacuum())
                                {
                                    sys.ChangeLastVacuumDate(DateTime.Now.ToShortDateString());
                                    CommonUtils.ShowSuccess(Properties.Resources.MSG_VACUUM);
                                }
                            }
                        }
                    }
                    #endregion

                    #region Run Main Form
                    Application.Run(new MoneyForm());
                    #endregion
                }
            }
            catch (Exception e)
            {
                CommonUtils.ShowError(e.ToString());
            }
        }
    }
}