using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using GMoney.util;
using System.IO;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace GMoney.db
{
    class BaseDB
    {
        #region Members
        private static string[] CREATE_TABLES = Regex.Split(Properties.Resources.create, System.Environment.NewLine, RegexOptions.IgnoreCase);
        private static string DB = CommonUtils.DB_NAME;
        private static string DB_PWD = CommonUtils.DB_PWD;
        public static string SQLiteDB = Path.Combine(Application.StartupPath, DB);
        #endregion

        #region Methods
        /// <summary>
        /// check db file
        /// </summary>
        /// <returns></returns>
        public static bool CheckDB()
        {
            bool bRet = false;
            DbTransaction tran = null;
            SQLiteConnection conn = null;

            try
            {
                if (!File.Exists(SQLiteDB))
                {
                    // if not exist, create new
                    SQLiteConnection.CreateFile(SQLiteDB);

                    // create table
                    conn = GetConnection();

                    tran = conn.BeginTransaction();
                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        foreach (string sql in CREATE_TABLES)
                        {
                            if (string.IsNullOrEmpty(sql) || sql.Trim().StartsWith(";"))
                            {
                                continue;
                            }
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tran.Commit();
                }

                bRet = true;
            }
            catch (Exception e)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
                CommonUtils.ShowError(e.ToString());

                try
                {
                    File.Delete(SQLiteDB);
                }
                catch { }
            }
            finally
            {
                if (conn != null)
                {
                    try
                    {
                        conn.Close();
                    }
                    catch { }
                    conn = null;
                }
            }

            return bRet;
        }

        /// <summary>
        /// Do vacuum
        /// </summary>
        /// <returns></returns>
        public static bool DoVacuum()
        {
            bool bRet = false;
            SQLiteConnection conn = null;

            try
            {
                conn = GetConnection();

                using (SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "vacuum;";
                    cmd.ExecuteNonQuery();
                }

                bRet = true;
            }
            catch (Exception e)
            {
                CommonUtils.ShowError(e.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    try
                    {
                        conn.Close();
                    }
                    catch { }
                    conn = null;
                }
            }

            return bRet;
        }

        /// <summary>
        /// get connection
        /// </summary>
        /// <returns></returns>
        protected static SQLiteConnection GetConnection()
        {
            SQLiteConnection cnn = new SQLiteConnection();
            cnn.ConnectionString = @"Data Source=" + SQLiteDB;
#if !DEBUG // when debug , not need password
            cnn.SetPassword(DB_PWD);
#endif
            cnn.Open();
            return cnn;
        }
        #endregion
    }
}
