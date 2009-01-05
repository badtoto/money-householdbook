using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Money.form;
using Money.util;
using System.Threading;
using Money.db;

namespace Money
{
    static class Program
    {
        // アプリケーション固定名
        private static string strAppConstName = Application.ProductName;

        // 多重起動を禁止するミューテックス
        private static Mutex mutexObject;
        /// <summary>
        /// Application Main Entry Point.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Windows 2000（NT 5.0）以降のみグローバル・ミューテックス利用可
            OperatingSystem os = Environment.OSVersion;
            if ((os.Platform == PlatformID.Win32NT) && (os.Version.Major >= 5))
            {
                strAppConstName = @"Global\" + strAppConstName;
            }

            try
            {
                // ミューテックスを生成する
                mutexObject = new Mutex(false, strAppConstName);
            }
            catch
            {
                // グローバル・ミューテックスによる多重起動禁止
                CommonUtils.ShowError(string.Format(Properties.Resources.MSG_INI_0001, CommonUtils.CaptionName));
                return;
            }

            // ミューテックスを取得する
            if (mutexObject.WaitOne(0, false))
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
                // ミューテックスを解放する
                mutexObject.ReleaseMutex();
                #endregion
            }
            else
            {
                //  警告を表示して終了
                CommonUtils.ShowError(string.Format(Properties.Resources.MSG_INI_0001, CommonUtils.CaptionName));
            }

            // ミューテックスを破棄する
            mutexObject.Close();
        }
    }
}