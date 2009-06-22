using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Globalization;
using System.Security.Cryptography;

namespace Money.util
{
    class CommonUtils
    {
        #region Properties
        public static string SYSTEM_BACKUP_PATH = "backup";

        public static string DB_NAME = "money.db";

        public static string DB_PWD = "4a7d1ed414474e4033ac29ccb8653d9b";

        public static int DEFAULT_USER_ID = 0;

        public static int MAX_CHART_YEARS = 4;

        public static int KEEP_DB_BACKUP_FILE_DAYS = 45;

        public static int SYSTEM_START_YEAR = 2005;

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

        public static string[] MONTH_NAMES
        {
            get
            {
                string[] names = new string[12];
                for (int i = 0; i < names.Length; i++)
                    names[i] = APP_CULTURE_INFO.DateTimeFormat.AbbreviatedMonthNames[i];
                return names;
            }
        }

        public static string[] MONTH_NAMES_WATER
        {
            get
            {
                // water only 6 months
                string[] names = new string[6];
                for (int i = 0; i < names.Length; i++)
                    names[i] = APP_CULTURE_INFO.DateTimeFormat.AbbreviatedMonthNames[i * 2 + 1];
                return names;
            }
        }
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
            return MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static DialogResult ShowSuccess(string message)
        {
            return MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
    }
}
