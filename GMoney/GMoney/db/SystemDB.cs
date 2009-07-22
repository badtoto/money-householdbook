using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data.Common;
using GMoney.util;
using System.Drawing;

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
                int ret = cmd.ExecuteNonQuery();

                if (ret <= 0)
                {
                    cmd = conn.CreateCommand();
                    cmd.CommandText = "insert into s_system(app_name, app_value) values (@name, @value)";
                    cmd.Parameters.AddWithValue("name", app_name);
                    cmd.Parameters.AddWithValue("value", app_value);
                    cmd.ExecuteNonQuery();
                }

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

        public Point GetFormLocation()
        {
            Point p = Point.Empty;

            string x = GetValue("point_x");
            string y = GetValue("point_y");
            if ((!string.IsNullOrEmpty(x)) && (!string.IsNullOrEmpty(y)))
            {
                try
                {
                    p = new Point(Convert.ToInt32(x), Convert.ToInt32(y));
                }
                catch { }
            }

            return p;
        }

        public Size GetFormSize()
        {
            Size s = Size.Empty;

            string width = GetValue("size_width");
            string height = GetValue("size_height");
            if ((!string.IsNullOrEmpty(width)) && (!string.IsNullOrEmpty(height)))
            {
                try
                {
                    s = new Size(Convert.ToInt32(width), Convert.ToInt32(height));
                }
                catch { }
            }

            return s;
        }

        public bool SetFormLocation(Point p)
        {
            return SetValue("point_x", p.X.ToString()) && SetValue("point_y", p.Y.ToString());
        }

        public bool SetFormSize(Size s)
        {
            return SetValue("size_width", s.Width.ToString()) && SetValue("size_height", s.Height.ToString());
        }

        #endregion
    }
}
