using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using GMoney.control;
using System.Data.SQLite;
using GMoney.util;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Data.Common;
using GMoney.instance;

namespace GMoney.db
{
    class MasterDB : BaseDB
    {
        #region Get List
        public bool GetMajorList(ComboBox cb, int majorType, bool isUpdate)
        {
            cb.Items.Clear();
            bool bRet = false;
            SQLiteConnection conn = null;
            try
            {
                conn = GetConnection();

                SQLiteCommand cmd = conn.CreateCommand();
                if (majorType == (int)CommonUtils.BillType.Income)
                {
                    if (isUpdate)
                        cmd.CommandText = "select * from m_major where id > 0 and type = @major_type order by sort ";
                    else
                        cmd.CommandText = "select * from m_major where id > 0 and delete_flg = " + (int)CommonUtils.DeleteFlg.OFF + " and type = @major_type order by sort ";
                }
                else
                {
                    if (isUpdate)
                        cmd.CommandText = "select * from m_major where id > 0 and type <> @major_type order by sort ";
                    else
                        cmd.CommandText = "select * from m_major where id > 0 and delete_flg = " + (int)CommonUtils.DeleteFlg.OFF + " and type <> @major_type order by sort ";
                }
                cmd.Parameters.AddWithValue("major_type", (int)CommonUtils.BillType.Income);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ListItem item = new ListItem();
                    item.ID = Int32.Parse(reader["id"].ToString());
                    item.Name = reader["name"].ToString();
                    cb.Items.Add(item);
                }

                cb.SelectedIndex = 0;
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

        public bool GetSubList(ComboBox cb, int major_id, bool isUpdate)
        {
            cb.Items.Clear();
            bool bRet = false;
            SQLiteConnection conn = null;
            try
            {
                conn = GetConnection();

                SQLiteCommand cmd = conn.CreateCommand();
                if (isUpdate)
                    cmd.CommandText = "select * from m_sub where major_id = @major_id order by sort ";
                else
                    cmd.CommandText = "select * from m_sub where delete_flg = " + (int)CommonUtils.DeleteFlg.OFF + " and major_id = @major_id order by sort ";

                cmd.Parameters.AddWithValue("major_id", major_id);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ListItem item = new ListItem();
                    item.ID = Int32.Parse(reader["id"].ToString());
                    item.Name = reader["name"].ToString();
                    cb.Items.Add(item);
                }
                cb.SelectedIndex = 0;
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

        public bool GetSubList(ToolStripComboBox cb)
        {
            cb.Items.Clear();
            bool bRet = false;
            SQLiteConnection conn = null;
            try
            {
                conn = GetConnection();

                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT ID, Name FROM v_sub WHERE ID = 0 OR ID IN (SELECT DISTINCT sub_id FROM d_bill) ORDER BY major_sort, sub_sort ";
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ListItem item = new ListItem();
                    item.ID = Int32.Parse(reader["id"].ToString());
                    item.Name = reader["name"].ToString();
                    cb.Items.Add(item);
                }
                cb.SelectedIndex = 0;
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

        public bool GetUserList(ComboBox cb)
        {
            return GetUserList(cb, null);
        }

        public bool GetUserList(ComboBox cb, ListItem selectedItem)
        {
            cb.Items.Clear();
            bool bRet = false;
            SQLiteConnection conn = null;
            try
            {
                conn = GetConnection();

                SQLiteCommand cmd = conn.CreateCommand();
                if (selectedItem != null)
                    cmd.CommandText = "select * from m_user order by id ";
                else
                    cmd.CommandText = "select * from m_user where delete_flg = " + (int)CommonUtils.DeleteFlg.OFF + " order by id ";
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ListItem item = new ListItem();
                    item.ID = Int32.Parse(reader["id"].ToString());
                    item.Name = reader["name"].ToString();
                    cb.Items.Add(item);
                }

                if (selectedItem != null)
                    cb.SelectedItem = selectedItem;
                else
                    cb.SelectedIndex = 0;

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

        public ArrayList GetUserTable()
        {
            ArrayList list = new ArrayList();

            SQLiteConnection conn = null;
            SQLiteCommand cmd = null;
            SQLiteDataAdapter myCmd = null;
            DataSet dtSet = null;
            DataTable dt = null;

            try
            {
                conn = GetConnection();

                cmd = conn.CreateCommand();

                cmd.CommandText = "select * from m_user order by id";

                myCmd = new SQLiteDataAdapter(cmd);
                dtSet = new DataSet();
                myCmd.Fill(dtSet);
                dt = dtSet.Tables[0];
                foreach (DataRow myRow in dt.Rows)
                {
                    BaseTbl tbl = new BaseTbl();
                    tbl.ID = Convert.ToInt32(myRow["ID"].ToString());
                    tbl.Name = myRow["Name"].ToString();
                    tbl.CreateDate = Convert.ToDateTime(myRow["create_date"]);
                    tbl.UpdateDate = Convert.ToDateTime(myRow["update_date"]);
                    tbl.DeleteFlg = Convert.ToInt32(myRow["delete_flg"].ToString());
                    list.Add(tbl);
                }
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

            return list;
        }

        public ArrayList GetSubOptimalList()
        {
            ArrayList list = new ArrayList();

            SQLiteConnection conn = null;
            SQLiteCommand cmd = null;
            SQLiteDataAdapter myCmd = null;
            DataSet dtSet = null;
            DataTable dt = null;

            try
            {
                conn = GetConnection();

                cmd = conn.CreateCommand();

                cmd.CommandText = "select ID, Name, Optimal, Range from v_sub order by major_sort, sub_sort";

                myCmd = new SQLiteDataAdapter(cmd);
                dtSet = new DataSet();
                myCmd.Fill(dtSet);
                dt = dtSet.Tables[0];
                foreach (DataRow myRow in dt.Rows)
                {
                    SubTbl tbl = new SubTbl();
                    tbl.ID = Convert.ToInt32(myRow["ID"].ToString());
                    tbl.Name = myRow["Name"].ToString();
                    tbl.Optimal = Convert.ToDouble(myRow["Optimal"].ToString());
                    tbl.Range = Convert.ToDouble(myRow["Range"].ToString());
                    list.Add(tbl);
                }
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

            return list;
        }

        public bool GetSubListFoxFixed(ComboBox cb)
        {
            cb.Items.Clear();
            bool bRet = false;
            SQLiteConnection conn = null;
            try
            {
                conn = GetConnection();

                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from v_sub where sub_id > 0 order by major_sort, sub_sort ";
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ListItem item = new ListItem();
                    item.ID = Int32.Parse(reader["id"].ToString());
                    item.Name = reader["name"].ToString();
                    cb.Items.Add(item);
                }
                cb.SelectedIndex = 0;
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

        public ArrayList GetCategoryList()
        {
            ArrayList list = new ArrayList();
            SQLiteConnection conn = null;
            try
            {
                conn = GetConnection();

                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from v_sub where sub_id > 0 order by bill_type, major_sort, sub_sort ";
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BillTbl item = new BillTbl();
                    item.BillType = Int32.Parse(reader["bill_type"].ToString());
                    item.MajorId = Int32.Parse(reader["major_id"].ToString());
                    item.MajorName = reader["major_name"].ToString();
                    item.MajorSort = Int32.Parse(reader["major_sort"].ToString());
                    item.SubId = Int32.Parse(reader["sub_id"].ToString());
                    item.SubName = reader["sub_name"].ToString();
                    item.SubSort = Int32.Parse(reader["sub_sort"].ToString());
                    list.Add(item);
                }
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
            return list;
        }
        #endregion

        #region Optimal & Range
        /// <summary>
        /// Update Optimal & Range to Sub Category
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public bool UpdOptimalData(ICollection collection)
        {
            bool bRet = false;
            SQLiteConnection conn = null;
            DbTransaction tran = null;
            SQLiteCommand cmd = null;
            try
            {
                conn = GetConnection();
                tran = conn.BeginTransaction();

                foreach (SubTbl tbl in collection)
                {
                    cmd = conn.CreateCommand();
                    cmd.CommandText = "update m_sub set optimal = @optimal, range = @range, update_date = @update_date where id = @id;";
                    cmd.Parameters.AddWithValue("optimal", tbl.Optimal);
                    cmd.Parameters.AddWithValue("range", tbl.Range);
                    cmd.Parameters.AddWithValue("update_date", DateTime.Now);
                    cmd.Parameters.AddWithValue("id", tbl.ID);
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

        /// <summary>
        /// Get [Optimal , Range] By Sub Category
        /// </summary>
        /// <param name="sub_id"></param>
        /// <returns></returns>
        public double[] GetSubOptimal(int sub_id)
        {
            double[] optimal = { 0, 0 };
            SQLiteConnection conn = null;
            try
            {
                conn = GetConnection();

                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from v_sub where id = ? ";
                cmd.Parameters.AddWithValue("id", sub_id);
                SQLiteDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    optimal[0] = double.Parse(reader["optimal"].ToString());
                    optimal[1] = double.Parse(reader["range"].ToString());
                }
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
            return optimal;
        }
        #endregion

        #region Add New User
        /// <summary>
        /// Add User
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>User ID(-1:Error, Others: user_id)</returns>
        public int NewUser(string userName)
        {
            int bRet = -1;
            SQLiteConnection conn = null;
            DbTransaction tran = null;
            SQLiteCommand cmd = null;
            SQLiteDataReader reader = null;

            try
            {
                conn = GetConnection();

                cmd = conn.CreateCommand();
                cmd.CommandText = "select * from m_user where name = @name;";
                cmd.Parameters.AddWithValue("name", userName);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                    throw new Exception(Properties.Resources.MSG_EXIST_USER);

                tran = conn.BeginTransaction();
                cmd = conn.CreateCommand();
                cmd.CommandText = "insert into m_user (id, name, create_date, update_date, delete_flg) values (null, @name, @create_date, @update_date, @delete_flg);";
                cmd.Parameters.AddWithValue("name", userName);
                cmd.Parameters.AddWithValue("create_date", DateTime.Now);
                cmd.Parameters.AddWithValue("update_date", DateTime.Now);
                cmd.Parameters.AddWithValue("delete_flg", (int)CommonUtils.DeleteFlg.OFF);
                cmd.ExecuteNonQuery();

                tran.Commit();

                cmd = conn.CreateCommand();
                cmd.CommandText = "select id from m_user where name = ?;";
                cmd.Parameters.AddWithValue("name", userName);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                    bRet = Convert.ToInt32(reader[0].ToString());
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

        /// <summary>
        /// Update Optimal & Range to Sub Category
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public bool UpdUserData(ICollection collection)
        {
            bool bRet = false;
            SQLiteConnection conn = null;
            DbTransaction tran = null;
            SQLiteCommand cmd = null;
            try
            {
                conn = GetConnection();
                tran = conn.BeginTransaction();

                foreach (BaseTbl tbl in collection)
                {
                    cmd = conn.CreateCommand();
                    cmd.CommandText = "update m_user set name = @name, delete_flg = @delete_flg, update_date = @update_date where id = @id;";
                    cmd.Parameters.AddWithValue("name", tbl.Name);
                    cmd.Parameters.AddWithValue("delete_flg", tbl.DeleteFlg);
                    cmd.Parameters.AddWithValue("update_date", DateTime.Now);
                    cmd.Parameters.AddWithValue("id", tbl.ID);
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
    }
}
