using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data.Common;
using System.Collections;
using System.Data;
using GMoney.control;
using GMoney.util;
using GMoney.instance;
using System.Globalization;

namespace GMoney.db
{
    class BillDB : BaseDB
    {
        #region Instance
        /// <summary>
        /// New Bill
        /// </summary>
        /// <param name="tbl"></param>
        /// <returns></returns>
        public bool NewBill(BillTbl tbl)
        {
            bool bRet = false;
            SQLiteConnection conn = null;
            DbTransaction tran = null;
            SQLiteCommand cmd = null;

            try
            {
                conn = GetConnection();
                tran = conn.BeginTransaction();

                double amount = (int)((tbl.Amount / tbl.SplitMonths) + 0.5);

                for (int i = 0; i < tbl.SplitMonths; i++)
                {
                    cmd = conn.CreateCommand();
                    cmd.CommandText = "insert into d_bill (id, bill_date, user_id, sub_id, payment_id, annual_budget, amount, remarks, create_date, update_date) values (null, @bill_date, @user_id, @sub_id, @payment_id, @annual_budget, @amount, @remarks, @create_date, @update_date);";
                    cmd.Parameters.AddWithValue("bill_date", tbl.Date.AddMonths(i).ToShortDateString());
                    cmd.Parameters.AddWithValue("user_id", tbl.UserId);
                    cmd.Parameters.AddWithValue("sub_id", tbl.SubId);
                    cmd.Parameters.AddWithValue("payment_id", tbl.PaymentId);
                    cmd.Parameters.AddWithValue("annual_budget", tbl.AnnualBudget);
                    cmd.Parameters.AddWithValue("amount", amount);
                    cmd.Parameters.AddWithValue("remarks", tbl.Remarks);
                    cmd.Parameters.AddWithValue("create_date", DateTime.Now);
                    cmd.Parameters.AddWithValue("update_date", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }

                tran.Commit();

                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT last_insert_rowid() ";
                SQLiteDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    tbl.ID = Int32.Parse(reader[0].ToString());
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
        /// Update Bill
        /// </summary>
        /// <param name="tbl"></param>
        /// <returns></returns>
        public bool UpdBill(BillTbl tbl)
        {
            bool bRet = false;
            SQLiteConnection conn = null;
            DbTransaction tran = null;
            try
            {
                conn = GetConnection();
                tran = conn.BeginTransaction();

                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "update d_bill set bill_date = @bill_date, user_id = @user_id, sub_id = @sub_id, payment_id = @payment_id, annual_budget = @annual_budget, amount = @amount, remarks = @remarks, update_date = @update_date where id = @id;";
                cmd.Parameters.AddWithValue("bill_date", tbl.Date.ToShortDateString());
                cmd.Parameters.AddWithValue("user_id", tbl.UserId);
                cmd.Parameters.AddWithValue("sub_id", tbl.SubId);
                cmd.Parameters.AddWithValue("payment_id", tbl.PaymentId);
                cmd.Parameters.AddWithValue("annual_budget", tbl.AnnualBudget);
                cmd.Parameters.AddWithValue("amount", tbl.Amount);
                cmd.Parameters.AddWithValue("remarks", tbl.Remarks);
                cmd.Parameters.AddWithValue("update_date", DateTime.Now);
                cmd.Parameters.AddWithValue("id", tbl.ID);
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
        /// Set Bills
        /// </summary>
        /// <param name="tbl"></param>
        /// <param name="myRow"></param>
        private void SetBillTbl(BillTbl tbl, DataRow myRow)
        {
            tbl.ID = Convert.ToInt32(myRow["ID"].ToString());
            tbl.UserId = Convert.ToInt32(myRow["user_id"].ToString());
            tbl.UserName = myRow["user_name"].ToString();
            tbl.MajorId = Convert.ToInt32(myRow["major_id"].ToString());
            tbl.MajorName = myRow["major_name"].ToString();
            tbl.MajorSort = Convert.ToInt32(myRow["major_sort"]);
            tbl.SubId = Convert.ToInt32(myRow["sub_id"].ToString());
            tbl.SubName = myRow["sub_name"].ToString();
            tbl.SubSort = Convert.ToInt32(myRow["sub_sort"]);
            tbl.Remarks = myRow["remarks"].ToString();
            tbl.BillType = Convert.ToInt32(myRow["bill_type"]);
            double d = Convert.ToDouble(myRow["amount"].ToString());
            tbl.Amount = (tbl.BillType == (int)CommonUtils.BillType.Income ? d : -d);
            tbl.Date = DateTime.Parse(myRow["bill_date"].ToString());
            tbl.AnnualBudget = Convert.ToInt32(myRow["annual_budget"].ToString());
            tbl.CreateDate = DateTime.Parse(myRow["create_date"].ToString());
            tbl.UpdateDate = DateTime.Parse(myRow["update_date"].ToString());
        }

        /// <summary>
        /// Get Bill
        /// </summary>
        /// <param name="tbl"></param>
        /// <returns></returns>
        public bool GetBill(BillTbl tbl)
        {
            bool bRet = false;
            SQLiteConnection conn = null;
            SQLiteDataAdapter myCmd = null;
            DataSet dtSet = null;
            DataTable dt = null;
            try
            {
                conn = GetConnection();

                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from v_bill where id = @id;";
                cmd.Parameters.AddWithValue("id", tbl.ID);

                myCmd = new SQLiteDataAdapter(cmd);
                dtSet = new DataSet();
                myCmd.Fill(dtSet);
                dt = dtSet.Tables[0];

                foreach (DataRow myRow in dt.Rows)
                {
                    SetBillTbl(tbl, myRow);
                    bRet = true;
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
            return bRet;
        }

        /// <summary>
        /// Delete Bill
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool DelBill(string ids)
        {
            bool bRet = false;
            SQLiteConnection conn = null;
            DbTransaction tran = null;
            try
            {
                conn = GetConnection();
                tran = conn.BeginTransaction();

                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = string.Format("delete from d_bill where id in {0};", ids);
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

        #region Chart
        #region Pie & Bar Chart in Home Tab
        /// <summary>
        /// Get Expense sum by MajorName/SubName for Month Pie & Bar Chart
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public ArrayList GetExpenseDetailForPieChart(string strDate, bool showDetail, ArrayList majorIdList)
        {
            ArrayList list = new ArrayList();

            SQLiteConnection conn = null;
            SQLiteCommand cmd = null;
            SQLiteDataAdapter myCmd = null;
            DataSet dtSet = null;
            DataTable dt = null;
            string sql = "";

            try
            {
                conn = GetConnection();

                cmd = conn.CreateCommand();

                sql += "select ";

                if (showDetail)
                    sql += " sub_name as Name, sub_id as ID";
                else
                    sql += " major_name as Name, major_id as ID";

                sql += ", sum(amount) as Amount from v_bill where bill_type <> " + (int)CommonUtils.BillType.Income + "";

                if (!string.IsNullOrEmpty(strDate))
                {
                    if (strDate.Length == 7)
                        sql += " and substr(bill_date, 0, 7) = @date";
                    else if (strDate.Length == 4)
                        sql += " and substr(bill_date, 0, 4) = @date";
                }

                if (strDate.Length == 7)
                    sql += " and annual_budget <> " + (int)CommonUtils.AnnualBudget.Yes + "";

                if (showDetail && (majorIdList != null && majorIdList.Count > 0))
                {
                    sql += " and major_id in (";
                    foreach (int o in majorIdList)
                        sql += o + ", ";

                    sql = sql.Substring(0, sql.Length - 2);
                    sql += ")";
                }

                sql += " group by";

                if (showDetail)
                    sql += " sub_name, sub_id";
                else
                    sql += " major_name, major_id";

                sql += " having sum(amount) > 0";
                sql += " order by major_sort, sub_sort";

                cmd.CommandText = sql;

                if (!string.IsNullOrEmpty(strDate))
                {
                    cmd.Parameters.AddWithValue("date", strDate);
                }

                myCmd = new SQLiteDataAdapter(cmd);
                dtSet = new DataSet();
                myCmd.Fill(dtSet);
                dt = dtSet.Tables[0];

                foreach (DataRow myRow in dt.Rows)
                {
                    BaseTbl tbl = new BaseTbl();
                    tbl.ID = Convert.ToInt32(myRow["ID"].ToString());
                    tbl.Name = myRow["Name"].ToString();
                    tbl.Amount = Convert.ToDouble(myRow["Amount"].ToString());
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

        #region Line Chart in Chart Tab
        /// <summary>
        /// Get Monthly Amount By Year For Chart 
        /// </summary>
        /// <param name="sub_id"></param>
        /// <param name="withTax"></param>
        /// <returns></returns>
        public Hashtable GetMonthlyAmountByYearForChart(int[] year, int sub_id, bool withTax)
        {
            Hashtable ht = new Hashtable();
            ArrayList list = new ArrayList();
            Hashtable htData = new Hashtable();

            DateTime staDate = new DateTime(year[0], DateTime.Now.AddMonths(1).Month, 1);
            DateTime endDate = new DateTime(year[1], DateTime.Now.Month, DateTime.Now.Day);
            if (sub_id == 9)
            {
                if (endDate.Month % 2 == 1)
                {
                    endDate = endDate.AddDays(-endDate.Day);
                }
                else
                {
                    staDate = staDate.AddMonths(1);
                }
            }

            SQLiteConnection conn = null;
            SQLiteCommand cmd = null;
            SQLiteDataReader reader = null;
            string strDate = string.Empty;
            string amount = string.Empty;

            try
            {
                conn = GetConnection();

                cmd = conn.CreateCommand();
                string sql = string.Empty;
                if (sub_id == 0)
                {
                    sql = "select substr(bill_date, 0, 7) as date, sum(amount) as amount from v_bill where annual_budget <> " + (int)CommonUtils.AnnualBudget.Yes;
                    // TODO: Hard coding for sub_id
                    if (!withTax)
                        sql += " and bill_type = " + (int)CommonUtils.BillType.Expense + " ";
                    else
                        sql += " and bill_type <> " + (int)CommonUtils.BillType.Income + " ";
                }
                else
                {
                    sql = "select substr(bill_date, 0, 7) as date, sum(amount) as amount from v_bill where sub_id = @sub_id ";
                }
                sql += " and bill_date between @bill_date_1 and @bill_date_2 ";
                sql += " group by substr(bill_date, 0, 7) order by substr(bill_date, 0, 7)";
                cmd.CommandText = sql;

                cmd.Parameters.AddWithValue("bill_date_1", staDate.ToShortDateString());
                cmd.Parameters.AddWithValue("bill_date_2", endDate.ToShortDateString());
                if (sub_id != 0)
                    cmd.Parameters.AddWithValue("sub_id", sub_id);

                reader = cmd.ExecuteReader();

                string bDate = string.Empty;
                while (reader.Read())
                {
                    htData.Add(reader["date"].ToString(), Convert.ToDouble(reader["amount"].ToString()));
                }

                DateTime date = staDate;
                int cnt = 0;
                while (date <= endDate)
                {
                    strDate = string.Format("{0}{2}{1:00}", date.Year, date.Month, CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator);
                    if (htData[strDate] == null)
                        list.Add((double)0);
                    else
                        list.Add(htData[strDate]);
                    
                    cnt++;
                    if ((sub_id == 9 && cnt % 6 == 0) || (sub_id != 9 && cnt % 12 == 0))
                    {
                        ht.Add(date.Year.ToString(), list);
                        list = new ArrayList();
                    }
                    date = date.AddMonths(1);
                    if (sub_id == 9)
                        date = date.AddMonths(1);
                }
#if DEBUG1
                //for (int i = year[0]; i <= year[1]; i++)
                //{
                //    list = new ArrayList();
                //    for (int j = 1; j <= 12; j++)
                //    {
                //        if (sub_id == 9 && j % 2 == 1)
                //            continue;

                //        strDate = string.Format("{0}{2}{1:00}", i, j, CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator);
                //        if (htData[strDate] == null)
                //            list.Add(sum);
                //        else
                //            list.Add(htData[strDate]);
                //    }
                //    ht.Add(i.ToString(), list);
                //}
#endif
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
            return ht;
        }
        #endregion
        #endregion

        #region Get Detail
        /// <summary>
        /// Get Detail List
        /// </summary>
        /// <param name="strDate"></param>
        /// <param name="strSearch"></param>
        /// <param name="type_id"></param>
        /// <param name="sortByUser"></param>
        /// <param name="onlyAnnual"></param>
        /// <returns></returns>
        public object[] GetDetail(string strDate, string strSearch, int type_id, bool sortByUser, bool onlyAnnual)
        {
            Dictionary<int, object> dataList = new Dictionary<int, object>();
            double categoryTotal = 0;
            double total = 0;
            double totalAB = 0;
            int group_id = -1;
            string group_name = string.Empty;
            ArrayList billList = new ArrayList();

            SQLiteConnection conn = null;
            SQLiteCommand cmd = null;
            SQLiteDataAdapter myCmd = null;
            DataSet dtSet = null;
            DataTable dt = null;
            bool where = false;

            try
            {
                conn = GetConnection();

                cmd = conn.CreateCommand();

                string sql = "select * from v_bill ";

                if (!string.IsNullOrEmpty(strDate))
                {
                    where = true;
                    if (strDate.Length == 7)
                        sql += " where substr(bill_date, 0, 7) = @date";
                    else if (strDate.Length == 4)
                        sql += " where substr(bill_date, 0, 4) = @date";
                }

                if (onlyAnnual)
                {
                    if (where)
                        sql += " and ";
                    else
                    {
                        where = true;
                        sql += " where ";
                    }
                    sql += "annual_budget = " + (int)CommonUtils.AnnualBudget.Yes + " ";
                }

                if (type_id != -1)
                {
                    if (where)
                        sql += " and ";
                    else
                    {
                        where = true;
                        sql += " where ";
                    }

                    if (type_id == (int)CommonUtils.BillType.Expense)
                    {
                        sql += " bill_type =" + (int)CommonUtils.BillType.Expense + " ";
                    }
                    else
                    {
                        sql += " bill_type <>" + (int)CommonUtils.BillType.Expense + " ";
                    }
                }

                if (!string.IsNullOrEmpty(strSearch))
                {
                    if (where)
                        sql += " and ";
                    else
                        sql += " where ";

                    sql += " ( lower(remarks) like @remarks escape '/' or lower(user_name) like @user_name escape '/' or bill_date like @bill_date escape '/' or lower(major_name) like @major_name escape '/' or lower(sub_name) like @sub_name escape '/' or amount like @amount escape '/' ) ";
                }

                if (sortByUser)
                    sql += " order by user_id, bill_type, major_sort, sub_sort, bill_date;";
                else
                    sql += " order by bill_type, major_sort, sub_sort, bill_date;";
                cmd.CommandText = sql;

                if (!string.IsNullOrEmpty(strDate))
                {
                    cmd.Parameters.AddWithValue("date", strDate);
                }

                if (!string.IsNullOrEmpty(strSearch))
                {
                    strSearch = strSearch.ToLower();
                    strSearch = strSearch.Replace("/", "//");
                    strSearch = strSearch.Replace("%", "/%");
                    cmd.Parameters.AddWithValue("remarks", "%" + strSearch + "%");
                    cmd.Parameters.AddWithValue("user_name", "%" + strSearch + "%");
                    cmd.Parameters.AddWithValue("bill_date", "%" + strSearch + "%");
                    cmd.Parameters.AddWithValue("major_name", "%" + strSearch + "%");
                    cmd.Parameters.AddWithValue("sub_name", "%" + strSearch + "%");
                    cmd.Parameters.AddWithValue("amount", "%" + strSearch);
                }

                myCmd = new SQLiteDataAdapter(cmd);
                dtSet = new DataSet();
                myCmd.Fill(dtSet);
                dt = dtSet.Tables[0];

                foreach (DataRow myRow in dt.Rows)
                {
                    BillTbl tbl = new BillTbl();
                    SetBillTbl(tbl, myRow);
                    total += tbl.Amount;
                    if (tbl.IsAnnualBudget)
                        totalAB += tbl.Amount;

                    if (sortByUser)
                    {
                        if (group_id == -1)
                        {
                            group_id = tbl.UserId;
                            group_name = tbl.UserName;
                            categoryTotal = tbl.Amount;
                            billList.Add(tbl);

                        }
                        else if (group_id != tbl.UserId)
                        {
                            object[] obj = { group_name, categoryTotal, billList };
                            dataList.Add(group_id, obj);
                            group_id = tbl.UserId;
                            group_name = tbl.UserName;
                            categoryTotal = tbl.Amount;
                            billList = new ArrayList();
                            billList.Add(tbl);
                        }
                        else
                        {
                            billList.Add(tbl);
                            categoryTotal += tbl.Amount;
                        }
                    }
                    else
                    {
                        if (group_id == -1)
                        {
                            group_id = tbl.MajorId;
                            group_name = tbl.MajorName;
                            categoryTotal = tbl.Amount;
                            billList.Add(tbl);

                        }
                        else if (group_id != tbl.MajorId)
                        {
                            object[] obj = { group_name, categoryTotal, billList };
                            dataList.Add(group_id, obj);
                            group_id = tbl.MajorId;
                            group_name = tbl.MajorName;
                            categoryTotal = tbl.Amount;
                            billList = new ArrayList();
                            billList.Add(tbl);
                        }
                        else
                        {
                            billList.Add(tbl);
                            categoryTotal += tbl.Amount;
                        }
                    }
                }

                if (group_id != -1)
                {
                    object[] obj = { group_name, categoryTotal, billList };
                    dataList.Add(group_id, obj);
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

            object[] result = { total, dataList, totalAB };
            return result;
        }
        #endregion

        #region AutoCompleteString
        /// <summary>
        /// Get Auto Complete Remarks
        /// </summary>
        /// <param name="s_id"></param>
        /// <returns></returns>
        public string[] GetRemarkSuggests(int s_id)
        {
            ArrayList sc = new ArrayList();

            SQLiteConnection conn = null;
            SQLiteCommand cmd = null;
            SQLiteDataReader reader = null;

            try
            {
                conn = GetConnection();

                cmd = conn.CreateCommand();
                cmd.CommandText = "select distinct remarks from d_bill where sub_id = @sub_id order by remarks;";
                cmd.Parameters.AddWithValue("sub_id", s_id);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                    sc.Add(reader[0].ToString());
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
            return (string[])sc.ToArray(typeof(string));
        }

        /// <summary>
        /// Get Auto Complete Suggests
        /// </summary>
        /// <returns></returns>
        public string[] GetSearchSuggests()
        {
            ArrayList sc = new ArrayList();

            SQLiteConnection conn = null;
            SQLiteCommand cmd = null;
            SQLiteDataReader reader = null;

            try
            {
                // TODO: hard coding
                sc.Add("Expense");
                sc.Add("Income");
                sc.Add("Annual");

                conn = GetConnection();

                cmd = conn.CreateCommand();
                cmd.CommandText = "select name from m_major union select name from m_sub union select distinct remarks from d_bill";
                reader = cmd.ExecuteReader();

                while (reader.Read())
                    sc.Add(reader[0].ToString());
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
            return (string[])sc.ToArray(typeof(string));
        }
        #endregion

        #region Frequency Data
        /// <summary>
        /// Check frequency fixed data for system
        /// </summary>
        /// <returns></returns>
        public bool CheckExpenseFixed()
        {
            bool bRet = false;

            ArrayList addList = new ArrayList();
 
            SQLiteConnection conn = null;
            SQLiteCommand cmd = null;
            SQLiteDataReader reader = null;
            FixedDataTbl fixedData = null;

            try
            {
                conn = GetConnection();

                cmd = conn.CreateCommand();
                cmd.CommandText = "select * from v_fixed_data order by major_sort, sub_sort;";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    fixedData = new FixedDataTbl();
                    fixedData.ID = int.Parse(reader["id"].ToString());
                    fixedData.UserId = int.Parse(reader["user_id"].ToString());
                    fixedData.SubId = int.Parse(reader["sub_id"].ToString());
                    fixedData.IntervalType = int.Parse(reader["interval_type"].ToString());
                    fixedData.Interval = int.Parse(reader["interval"].ToString());
                    fixedData.BillType = int.Parse(reader["bill_type"].ToString());
                    fixedData.Amount = double.Parse(reader["amount"].ToString());
                    fixedData.IntervalDetail = reader["detail"].ToString();
                    fixedData.StartDate = (DateTime)reader["start_date"];
                    fixedData.EndDate = (DateTime)reader["end_date"];

                    // set default start date (if haven't added, begin)
                    DateTime addStartDate = fixedData.StartDate;
                    cmd = conn.CreateCommand();
                    cmd.CommandText = "select max(bill_date) as bill_date from d_bill where fixed_id = @fixed_id and user_id = @user_id and sub_id = @sub_id and bill_date >= @date;";
                    cmd.Parameters.AddWithValue("fixed_id", fixedData.ID);
                    cmd.Parameters.AddWithValue("user_id", fixedData.UserId);
                    cmd.Parameters.AddWithValue("sub_id", fixedData.SubId);
                    cmd.Parameters.AddWithValue("date", fixedData.StartDate.ToShortDateString());
                    SQLiteDataReader reader2 = cmd.ExecuteReader();
                    if (reader2.Read())
                    {
                        if (!string.IsNullOrEmpty(reader2["bill_date"].ToString()))
                        {
                            // if added, selected the max, and get the next add date
                            addStartDate = DateTime.Parse(reader2["bill_date"].ToString() + " 00:00:00");
                            switch (fixedData.IntervalType)
                            {
                                case (int)CommonUtils.IntervalType.EveryDay:
                                    addStartDate = addStartDate.AddDays(fixedData.Interval);
                                    break;
                                case (int)CommonUtils.IntervalType.EveryWeek:
                                    addStartDate = addStartDate.AddDays(fixedData.Interval * 7);
                                    break;
                                case (int)CommonUtils.IntervalType.EveryYear:
                                    addStartDate = addStartDate.AddYears(fixedData.Interval);
                                    break;
                                default:
                                    addStartDate = new DateTime(addStartDate.Year, addStartDate.Month + fixedData.Interval, Convert.ToInt32(fixedData.IntervalDetail));
                                    break;
                            }
                        }
                        else
                        {
                            switch (fixedData.IntervalType)
                            {
                                case (int)CommonUtils.IntervalType.EveryDay:
                                    // do nothing
                                    break;
                                case (int)CommonUtils.IntervalType.EveryWeek:
                                    while (true)
                                    {
                                        if (addStartDate.DayOfWeek.ToString() == fixedData.IntervalDetail)
                                        {
                                            break;
                                        }
                                        addStartDate = addStartDate.AddDays(1);
                                    }
                                    break;
                                case (int)CommonUtils.IntervalType.EveryYear:
                                    if (!(addStartDate.Month == 1 && addStartDate.Day == 1))
                                        addStartDate = new DateTime(addStartDate.Year + 1, 1, 1);
                                    break;
                                default:
                                    DateTime tmp = new DateTime(addStartDate.Year, addStartDate.Month, Convert.ToInt32(fixedData.IntervalDetail));
                                    if (Convert.ToInt32(fixedData.IntervalDetail) < addStartDate.Day)
                                        addStartDate = tmp.AddMonths(1);
                                    else
                                        addStartDate = tmp;
                                    break;
                            }
                        }
                    }

                    while (addStartDate <= DateTime.Now && DateTime.Now <= fixedData.EndDate)
                    {
                        BillTbl tbl = new BillTbl();
                        tbl.BillType = fixedData.BillType;
                        tbl.UserId = fixedData.UserId;
                        tbl.SubId = fixedData.SubId;
                        tbl.Amount = fixedData.Amount;
                        tbl.Date = addStartDate;
                        tbl.FixedId = fixedData.ID;

                        tbl.Remarks = string.Empty;

                        addList.Add(tbl);

                        switch (fixedData.IntervalType)
                        {
                            case (int)CommonUtils.IntervalType.EveryDay:
                                addStartDate = addStartDate.AddDays(fixedData.Interval);
                                break;
                            case (int)CommonUtils.IntervalType.EveryWeek:
                                addStartDate = addStartDate.AddDays(fixedData.Interval * 7);
                                break;
                            case (int)CommonUtils.IntervalType.EveryYear:
                                addStartDate = addStartDate.AddYears(fixedData.Interval);
                                break;
                            default:
                                addStartDate = addStartDate.AddMonths(fixedData.Interval);
                                break;
                        }
                    }
                }

                if (addList.Count > 0)
                {
                    foreach (BillTbl tbl in addList)
                    {
                        cmd = conn.CreateCommand();
                        cmd.CommandText = "insert into d_bill (id, bill_date, user_id, sub_id, payment_id, annual_budget, amount, remarks, fixed_id, create_date, update_date) values (null, @date, @user_id, @sub_id, @payment_id, @annual_budget, @amount, @remarks, @fixed_id, @create_date, @update_date);";
                        cmd.Parameters.AddWithValue("payment_id", tbl.PaymentId);
                        cmd.Parameters.AddWithValue("annual_budget", tbl.AnnualBudget);
                        cmd.Parameters.AddWithValue("date", tbl.Date.ToShortDateString());
                        cmd.Parameters.AddWithValue("user_id", tbl.UserId);
                        cmd.Parameters.AddWithValue("sub_id", tbl.SubId);
                        cmd.Parameters.AddWithValue("amount", tbl.Amount);
                        cmd.Parameters.AddWithValue("remarks", tbl.Remarks);
                        cmd.Parameters.AddWithValue("fixed_id", tbl.FixedId);
                        cmd.Parameters.AddWithValue("create_date", DateTime.Now);
                        cmd.Parameters.AddWithValue("update_date", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
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
        #endregion

        #region Get Min & Max Bill Date
        public DateTime[] GetBillDateRange()
        {
            DateTime[] dateRange = { DateTime.Now, DateTime.Now };
            SQLiteConnection conn = null;
            SQLiteCommand cmd = null;
            SQLiteDataReader reader = null;

            try
            {
                conn = GetConnection();

                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT min(bill_date) from d_bill ";
                reader = cmd.ExecuteReader();
                if (reader.Read() && reader[0] != null)
                    try { dateRange[0] = DateTime.Parse(reader[0].ToString() + " 00:00:00"); }
                    catch { }

                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT max(bill_date) from d_bill ";
                reader = cmd.ExecuteReader();
                if (reader.Read() && reader[0] != null)
                    try { dateRange[1] = DateTime.Parse(reader[0].ToString() + " 00:00:00"); }
                    catch { }

                if (dateRange[1] <= DateTime.Now)
                    dateRange[1] = DateTime.Now;
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
            return dateRange;
        }
        #endregion
    }
}
