using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using Money.util;
using System.IO;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Collections;
using System.Data;
using Money.instance;


namespace Money.db
{
    class BaseDB
    {
        #region private members
        private static string DB = CommonUtils.DB_NAME;
        private static string DB_PWD = CommonUtils.DB_PWD;
        public string SQLiteDB = Path.Combine(Application.StartupPath, DB);
        public bool IsDebug { set; get; }
        public DateTime StartDate { set; get; }
        public int Days { set; get; }
        #endregion

        public BaseDB()
        {
            IsDebug = false;
        }

        public BaseDB(string s)
        {
            if (!string.IsNullOrEmpty(s))
                SQLiteDB = Path.Combine(s, DB);
        }

        #region user function
        /// <summary>
        /// get connection
        /// </summary>
        /// <returns></returns>
        protected SQLiteConnection GetConnection()
        {
            SQLiteConnection cnn = new SQLiteConnection();
            cnn.ConnectionString = @"Data Source=" + SQLiteDB;

            if (!IsDebug)
                cnn.SetPassword(DB_PWD);

            cnn.Open();
            return cnn;
        }

        public ArrayList GetMajorList()
        {
            ArrayList list = new ArrayList();
            SQLiteConnection conn = null;
            try
            {
                conn = GetConnection();

                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from m_major where id > 0 and delete_flg = " + (int)CommonUtils.DeleteFlg.OFF + " order by sort ";

                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(Int32.Parse(reader["id"].ToString()));
                }

            }
            catch (Exception e)
            {
                CommonUtils.ShowError(e.Message);
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
                    list.Add(Convert.ToInt32(myRow["ID"].ToString()));
                }
            }
            catch (Exception e)
            {
                CommonUtils.ShowError(e.Message);
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

        public Dictionary<int, ArrayList> GetSubList(ArrayList majorList)
        {
            Dictionary<int, ArrayList> dic = new Dictionary<int, ArrayList>();
            SQLiteConnection conn = null;
            try
            {
                conn = GetConnection();

                SQLiteCommand cmd = null;

                foreach (int li in majorList)
                {
                    ArrayList list = new ArrayList();
                    cmd = conn.CreateCommand();
                    cmd.CommandText = "select * from m_sub where delete_flg = " + (int)CommonUtils.DeleteFlg.OFF + " and major_id = @major_id order by sort ";

                    cmd.Parameters.AddWithValue("major_id", li);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(Int32.Parse(reader["id"].ToString()));
                    }

                    dic.Add(li, list);
                }
            }
            catch (Exception e)
            {
                CommonUtils.ShowError(e.Message);
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
            return dic;
        }

        public bool NewBill(ArrayList userList, ArrayList majorList, Dictionary<int, ArrayList>subList, long count)
        {
            bool bRet = false;
            SQLiteConnection conn = null;
            DbTransaction tran = null;
            SQLiteCommand cmd = null;
            Random random = null;

            try
            {
                conn = GetConnection();
                tran = conn.BeginTransaction();

                random = new Random();

                for (int i = 0; i < count; i++)
                {
                    BillTbl tbl = new BillTbl();
                    tbl.Amount = random.Next(1, 1000);
                    tbl.AnnualBudget = random.Next(0, 1);
                    tbl.BillType = random.Next(0, 2);
                    tbl.Date = StartDate.AddDays(random.Next(0, Days));
                    tbl.MajorId = (int)majorList[random.Next(0, majorList.Count - 1)];
                    ArrayList l = subList[tbl.MajorId];
                    tbl.SubId = (int)l[random.Next(0, l.Count - 1)];
                    tbl.UserId = (int)userList[random.Next(0, userList.Count - 1)];

                    cmd = conn.CreateCommand();
                    cmd.CommandText = "insert into d_bill (id, bill_date, user_id, sub_id, payment_id, annual_budget, amount, remarks, create_date, update_date) values (null, @bill_date, @user_id, @sub_id, @payment_id, @annual_budget, @amount, @remarks, @create_date, @update_date);";
                    cmd.Parameters.AddWithValue("bill_date", tbl.Date.ToShortDateString());
                    cmd.Parameters.AddWithValue("user_id", tbl.UserId);
                    cmd.Parameters.AddWithValue("sub_id", tbl.SubId);
                    cmd.Parameters.AddWithValue("payment_id", tbl.PaymentId);
                    cmd.Parameters.AddWithValue("annual_budget", tbl.AnnualBudget);
                    cmd.Parameters.AddWithValue("amount", tbl.Amount);
                    cmd.Parameters.AddWithValue("remarks", tbl.Remarks);
                    cmd.Parameters.AddWithValue("create_date", DateTime.Now);
                    cmd.Parameters.AddWithValue("update_date", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }



                tran.Commit();


                cmd = conn.CreateCommand();
                cmd.CommandText = "vacuum;";
                cmd.ExecuteNonQuery();

                bRet = true;
            }
            catch (Exception e)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
                MessageBox.Show(e.Message);
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
                random = null;
            }
            return bRet;
        }

        #endregion

    }
}
