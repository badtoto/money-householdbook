using System;
using System.Collections.Generic;
using System.Text;
using Money.control;
using System.Data.SQLite;
using System.Data.Common;
using Money.util;
using System.Collections;
using System.Data;
using Money.instance;

namespace Money.db
{
    class FixedDataDB : BaseDB
    {
        #region Instance
        /// <summary>
        /// New Fixed Data
        /// </summary>
        /// <param name="tbl"></param>
        /// <returns></returns>
        public bool NewFixedData(FixedDataTbl tbl)
        {
            bool bRet = false;
            SQLiteConnection conn = null;
            DbTransaction tran = null;
            try
            {
                conn = GetConnection();
                tran = conn.BeginTransaction();

                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "insert into d_fixed_data values (null, @user_id, @sub_id, @interval_type, @interval, @detail, @amount, @start_date, @end_date, @create_date, @update_date);";
                cmd.Parameters.AddWithValue("user_id", tbl.UserId);
                cmd.Parameters.AddWithValue("sub_id", tbl.SubId);
                cmd.Parameters.AddWithValue("interval_type", tbl.IntervalType);
                cmd.Parameters.AddWithValue("interval", tbl.Interval);
                cmd.Parameters.AddWithValue("detail", tbl.IntervalDetail);
                cmd.Parameters.AddWithValue("amount", tbl.Amount);
                cmd.Parameters.AddWithValue("start_date", tbl.StartDate);
                cmd.Parameters.AddWithValue("end_date", tbl.EndDate);
                cmd.Parameters.AddWithValue("create_date", DateTime.Now);
                cmd.Parameters.AddWithValue("update_date", DateTime.Now);
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

        /// <summary>
        /// Update Fixed Data
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public bool UpdFixedData(ICollection collection)
        {
            bool bRet = false;
            SQLiteConnection conn = null;
            DbTransaction tran = null;
            SQLiteCommand cmd = null;
            try
            {
                conn = GetConnection();
                tran = conn.BeginTransaction();

                foreach (FixedDataTbl tbl in collection)
                {
                    cmd = conn.CreateCommand();
                    cmd.CommandText = "update d_fixed_data set user_id = @user_id, sub_id = @sub_id, interval_type = @interval_type, interval = @interval, detail= @detail, amount = @amount, start_date=@start_date, end_date = @end_date, update_date = @update_date where id = @id;";
                    cmd.Parameters.AddWithValue("user_id", tbl.UserId);
                    cmd.Parameters.AddWithValue("sub_id", tbl.SubId);
                    cmd.Parameters.AddWithValue("interval_type", tbl.IntervalType);
                    cmd.Parameters.AddWithValue("interval", tbl.Interval);
                    cmd.Parameters.AddWithValue("detail", tbl.IntervalDetail);
                    cmd.Parameters.AddWithValue("amount", tbl.Amount);
                    cmd.Parameters.AddWithValue("start_date", tbl.StartDate);
                    cmd.Parameters.AddWithValue("end_date", tbl.EndDate);
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
        /// Delete Fixed Data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DelFixedData(int id)
        {
            bool bRet = false;
            SQLiteConnection conn = null;
            DbTransaction tran = null;
            try
            {
                conn = GetConnection();
                tran = conn.BeginTransaction();

                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "delete from d_fixed_data where id = @id;";
                cmd.Parameters.AddWithValue("id", id);
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

        #region Get List
        /// <summary>
        /// Get Fixed Data List
        /// </summary>
        /// <returns></returns>
        public ArrayList GetFixedDataList()
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

                cmd.CommandText = "select * from v_fixed_data order by major_sort, sub_sort";

                myCmd = new SQLiteDataAdapter(cmd);
                dtSet = new DataSet();
                myCmd.Fill(dtSet);
                dt = dtSet.Tables[0];

                foreach (DataRow myRow in dt.Rows)
                {
                    FixedDataTbl tbl = new FixedDataTbl();
                    tbl.ID = Convert.ToInt32(myRow["id"].ToString());
                    tbl.UserId = Convert.ToInt32(myRow["user_id"].ToString());
                    tbl.UserName = myRow["user_name"].ToString();
                    tbl.SubId = Convert.ToInt32(myRow["sub_id"].ToString());
                    tbl.Name = myRow["sub_name"].ToString();
                    tbl.IntervalType = Convert.ToInt32(myRow["interval_type"].ToString());
                    tbl.Interval = Convert.ToInt32(myRow["interval"].ToString());
                    tbl.IntervalDetail = myRow["detail"].ToString();
                    tbl.Amount = Convert.ToDouble(myRow["amount"].ToString());
                    tbl.StartDate = DateTime.Parse(myRow["start_date"].ToString());
                    tbl.EndDate = DateTime.Parse(myRow["end_date"].ToString());
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
        #endregion
    }
}
