using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Globalization;
using System.Security.Cryptography;

namespace GMoney.util
{
    class CommonUtils
    {
        #region Properties
        public static string SYSTEM_BACKUP_PATH = "backup";

        public static string DB_NAME = "GMoney.db";

        public static string DB_PWD = "4a7d1ed414474e4033ac29ccb8653d9b";

        public static int DEFAULT_USER_ID = 0;

        public static int KEEP_DB_BACKUP_FILE_DAYS = 45;

        public static CultureInfo APP_CULTURE_INFO = CultureInfo.CreateSpecificCulture("en- US");

        public enum BillType
        {
            Expense = 0,
            Income = 1,
            Other = 2
        }

        public enum DeleteFlg
        {
            ON = 1,
            OFF = 0
        }

        public enum AnnualBudget
        {
            No = 0,
            Yes = 1
        }

        public enum IntervalType
        {
            EveryDay = 0,
            EveryWeek = 1,
            EveryMonth = 2,
            EveryYear = 3
        }

        public static string CaptionName
        {
            get
            {
                //return string.Format("{0} {1}", Application.ProductName, Application.ProductVersion);
                return Application.ProductName;
            }
        }

        public static string[] DAY_NAMES
        {
            get
            {
                return APP_CULTURE_INFO.DateTimeFormat.DayNames;
            }
        }

#if DEBUG1
        public static string[] MONTH_NAMES
        {
            get
            {
                string[] names = new string[12];
                for (int i = DateTime.Now.Month; i < names.Length + DateTime.Now.Month; i++)
                    names[i - DateTime.Now.Month] = APP_CULTURE_INFO.DateTimeFormat.AbbreviatedMonthNames[i % 12];
                return names;
            }
        }

        public static string[] MONTH_NAMES_WATER
        {
            get
            {
                int staMonth = DateTime.Now.Month;
                if (DateTime.Now.Month % 2 != 0)
                {
                    staMonth = staMonth - 1;
                }
                // water only 6 months
                string[] names = new string[6];
                int cnt = 0;
                while (cnt < names.Length)
                {
                    names[cnt++] = APP_CULTURE_INFO.DateTimeFormat.AbbreviatedMonthNames[(staMonth + 1) % 12];
                    staMonth = staMonth + 2;
                }
                return names;
            }
        }
#endif
        #endregion
        
        #region Methods
#if DEBUG1
        public static string Crypt(string s_Data, string s_Password, bool b_Encrypt)
        {
            byte[] u8_Salt = new byte[] { 0x26, 0x19, 0x81, 0x4E, 0xA0, 0x6D, 0x95, 0x34, 0x26, 0x75, 0x64, 0x05, 0xF6 };

            PasswordDeriveBytes i_Pass = new PasswordDeriveBytes(s_Password, u8_Salt);

            Rijndael i_Alg = Rijndael.Create();
            i_Alg.Key = i_Pass.GetBytes(32);
            i_Alg.IV = i_Pass.GetBytes(16);

            ICryptoTransform i_Trans = (b_Encrypt) ? i_Alg.CreateEncryptor() : i_Alg.CreateDecryptor();

            MemoryStream i_Mem = new MemoryStream();
            CryptoStream i_Crypt = new CryptoStream(i_Mem, i_Trans, CryptoStreamMode.Write);

            byte[] u8_Data;
            if (b_Encrypt) u8_Data = Encoding.Unicode.GetBytes(s_Data);
            else u8_Data = Convert.FromBase64String(s_Data);

            try
            {
                i_Crypt.Write(u8_Data, 0, u8_Data.Length);
                i_Crypt.FlushFinalBlock();
                if (b_Encrypt) return Convert.ToBase64String(i_Mem.ToArray());
                else return Encoding.Unicode.GetString(i_Mem.ToArray());

            }
            catch { return null; }
            finally
            {
                i_Crypt.Close();
                i_Mem.Close();
            }
        }
#endif
        public static DialogResult ShowError(string message)
        {
            return MessageBox.Show(message, Properties.Resources.MSG_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static DialogResult ShowSuccess(string message)
        {
            return MessageBox.Show(message, Properties.Resources.MSG_SUCCESS, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult ShowWarning(string message)
        {
            return MessageBox.Show(message, Properties.Resources.MSG_WARNING, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult ShowWarningWithCancel(string message)
        {
            return MessageBox.Show(message, Properties.Resources.MSG_WARNING, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }

        public static double[] GetX(int interval)
        {
            double[] x = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            if (interval < 12 && 12 % interval == 0 && interval > 1)
            {
                int j = 0;
                x = new double[12 / interval];
                for (int i = 0; i < 12; i++)
                {
                    if ((i + 1) % interval == 0)
                        x[j++] = i;
                }
            }
            return x;
        }


        public static string[] GetMonth(int interval)
        {
            if (interval < 12 && 12 % interval == 0 && interval > 1)
            {
                int staMonth = DateTime.Now.Month;
                if (DateTime.Now.Month % interval != 0)
                {
                    staMonth = staMonth - 1;
                }
                // water only 6 months
                string[] names = new string[12 / interval];
                int cnt = 0;
                while (cnt < names.Length)
                {
                    int index = (staMonth + interval) % 12 - 1;
                    if (index <= 0)
                        index += 12;
                    names[cnt++] = APP_CULTURE_INFO.DateTimeFormat.AbbreviatedMonthNames[index];
                    staMonth = staMonth + interval;
                }
                return names;
            }
            else
            {
                string[] names = new string[12];
                for (int i = DateTime.Now.Month; i < names.Length + DateTime.Now.Month; i++)
                    names[i - DateTime.Now.Month] = APP_CULTURE_INFO.DateTimeFormat.AbbreviatedMonthNames[i % 12];
                return names;
            }
        }
        #endregion
    }
}
