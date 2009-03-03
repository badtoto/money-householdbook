using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data.Common;
using GMoney.util;

namespace GMoney.db
{
    class SystemDB : BaseDB
    {
        #region Commons
        private string GetValue(string app_name)
        {
            string value = "";
            SQLiteConnection conn = null;
            try
            {
                conn = GetConnection();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select app_value from s_system where app_name = @name";
                cmd.Parameters.AddWithValue("name", app_name);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    value = reader["app_value"].ToString();
                }
            }
            catch
            {
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
            return value;
        }

        private bool SetValue(string app_name, string app_value)
        {
            bool bRet = false;
            SQLiteConnection conn = null;
            DbTransaction tran = null;
            try
            {
                conn = GetConnection();
                tran = conn.BeginTransaction();

                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "update s_system set app_value = @value where app_name = @name";
                cmd.Parameters.AddWithValue("name", app_name);
                cmd.Parameters.AddWithValue("value", app_value);
                cmd.ExecuteNonQuery();

                tran.Commit();

                bRet = true;
            }
            catch (Exception e)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
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
        #endregion

        #region System Parameters
        public string GetLoginPassword()
        {
            return GetValue("login_password");
        }

        public bool ChangeLoginPassword(string password)
        {
            return SetValue("login_password", password);
        }

        public string GetLastVacuumDate()
        {
            return GetValue("last_vacuum_date");
        }

        public bool ChangeLastVacuumDate(string value)
        {
            return SetValue("last_vacuum_date", value);
        }
        #endregion
    }
}
