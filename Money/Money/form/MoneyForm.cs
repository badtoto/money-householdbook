using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using System.IO;
using Money.db;
using Money.util;
using Money.instance;
using Money.control;
using ZedGraph;
using BrightIdeasSoftware;
using Nexus.Windows.Forms;
using Money.Properties;
using System.Globalization;

namespace Money.form
{
    public partial class MoneyForm : Form
    {
        #region Members
        private static Color[] columnColor = { Color.Blue, Color.Red, Color.Green, Color.DarkOrange};

        double[] x = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        double[] x_water = { 1, 3, 5, 7, 9, 11 };
        bool isInitialize = true;
        Random randRGB = null;
#if DEBUG
        System.Diagnostics.Stopwatch sw = null;
#endif
        #endregion

        #region Constructor
        public MoneyForm()
        {
            InitializeComponent();

            this.Text = CommonUtils.CaptionName;
            randRGB = new Random();

            #region initialize treelistview
            this.treeListView.CanExpandGetter = delegate(object x)
            {
                return (x is TreeNode);
            };
            this.treeListView.ChildrenGetter = delegate(object x)
            {
                TreeNode dir = (TreeNode)x;
                return dir.GetChildren();
            };

            // 
            // colCategory
            // 
            this.colCategory.AspectGetter = delegate(object x)
            {
                if (x is TreeNode)
                    return string.Format("{0} ({1})", ((TreeNode)x).Name, ((TreeNode)x).AmountSum.ToString("n0"));
                else if (x is BillTbl)
                    return ((BillTbl)x).SubName;
                return "";
            };
            // 
            // colUserName
            // 
            this.colUserName.AspectGetter = delegate(object x)
            {
                if (x is BillTbl) return ((BillTbl)x).UserName;
                else return "";
            };
            // 
            // colDate
            // 
            this.colDate.AspectGetter = delegate(object x)
            {
                if (x is BillTbl) return ((BillTbl)x).Date.ToString("yyyy/MM/dd");
                else return "";
            };
            // 
            // colAmount
            // 
            this.colAmount.AspectGetter = delegate(object x)
            {
                if (x is BillTbl) return ((BillTbl)x).Amount.ToString("n0");
                else return "";
            };
            // 
            // colRemarks
            // 
            this.colRemarks.AspectGetter = delegate(object x)
            {
                if (x is BillTbl) return ((BillTbl)x).Remarks;
                else return "";
            };
            // 
            // colAB
            // 
            this.colAB.AspectGetter = delegate(object x)
            {
                if (x is BillTbl) return ((BillTbl)x).AnnualBudget;
                else return null;
            };


            this.colAB.Renderer = new MappedImageRenderer(new Object[] { 1, Properties.Resources.Tick_16, 0, null });

            this.treeListView.RowFormatter = delegate(OLVListItem olvi)
            {
                if (olvi.RowObject is TreeNode)
                    olvi.Font = new Font(treeListView.Font.FontFamily, olvi.Font.Size, FontStyle.Bold);
            };

            #endregion

            // set chart select box
            MasterDB mdb = new MasterDB();
            mdb.GetSubList(tcbChart);

            CheckDefaultData();
            
            // set date
            SetDispDate();

            isInitialize = false;
        }
        #endregion

        #region Methods

        #region Bill
        private void NewBill()
        {
            BillForm f = new BillForm(0);
            DialogResult d = f.ShowDialog(this);
            f.Dispose();
        }

        private void EditBill()
        {
            int billId = -1;
            if (treeListView.SelectedObjects == null)
            {
                CommonUtils.ShowError(Properties.Resources.MSG_SELECT);
                return;
            }
            else
            {
                if (treeListView.SelectedObjects.Count == 0 || treeListView.SelectedObjects.Count > 1)
                {
                    CommonUtils.ShowError(Properties.Resources.MSG_EDIT);
                    return;
                }

                if (treeListView.SelectedObject is TreeNode)
                {
                    // 何もしない
                    return;
                }
            }
            billId = ((BillTbl)treeListView.SelectedObject).ID;

            BillForm f = new BillForm(billId);
            DialogResult d = f.ShowDialog(this);
            f.Dispose();
        }

        private void MergeBill()
        {
            BillTbl first = null;
            double amount = 0;
            int dataCnt = 0;
            string delStr = "(";

            if (treeListView.SelectedObjects == null)
            {
                CommonUtils.ShowError(Properties.Resources.MSG_MERGE);
                return;
            }

            foreach (object obj in treeListView.SelectedObjects)
            {
                if (obj is BillTbl)
                {
                    BillTbl tbl = (BillTbl)obj;
                    if (first == null)
                    {
                        first = tbl;
                    }
                    else if (tbl.SubId != first.SubId || tbl.UserId != first.UserId || tbl.AnnualBudget != first.AnnualBudget || tbl.Remarks != first.Remarks)
                    {
                        CommonUtils.ShowError(Properties.Resources.MSG_MERGE_INVALID);
                        return;
                    }
                    else
                    {
                        if (delStr.IndexOf(tbl.ID + ",") != -1 || first.ID == tbl.ID)
                            continue;
                        delStr += tbl.ID + ",";
                    }
                    dataCnt++;
                    amount += tbl.Amount;
                }
                else if (obj is TreeNode)
                {
                    TreeNode tr = (TreeNode)obj;
                    if (tr.IsSubCategory)
                    {
                        foreach (BillTbl tbl in (ArrayList)(((object[])tr.DataList[tr.ID])[2]))
                        {
                            if (first == null)
                            {
                                first = tbl;
                            }
                            else if (tbl.SubId != first.SubId || tbl.UserId != first.UserId || tbl.AnnualBudget != first.AnnualBudget || tbl.Remarks != first.Remarks)
                            {
                                CommonUtils.ShowError(Properties.Resources.MSG_MERGE_INVALID);
                                return;
                            }
                            else
                            {
                                if (delStr.IndexOf(tbl.ID + ",") != -1 || first.ID == tbl.ID)
                                    continue;
                                delStr += tbl.ID + ",";
                            }
                            dataCnt++;
                            amount += tbl.Amount;
                        }
                    }
                    else
                    {
                        // when Type and major, error
                        CommonUtils.ShowError(Properties.Resources.MSG_MERGE_INVALID);
                        return;
                    }
                }
            }

            // if only select one bill
            if (first == null || dataCnt <= 1)
            {
                CommonUtils.ShowError(Properties.Resources.MSG_MERGE);
                return;
            }

            delStr = delStr.Substring(0, delStr.Length - 1) + ")";
            // confirm
            if (CommonUtils.ShowWarningWithCancel(Properties.Resources.MSG_MERGE_CONFIRM) != DialogResult.OK)
            {
                return;
            }

            BillDB db = new BillDB();
            first.Amount = amount < 0 ? -amount : amount;
            if (db.UpdBill(first) && db.DelBill(delStr))
            {
                SetDispData(first.ID, null);
            }
        }

        private void DelBill()
        {

            if (treeListView.SelectedObjects == null || treeListView.SelectedObjects.Count <= 0)
            {
                CommonUtils.ShowError(Properties.Resources.MSG_SELECT);
                return;
            }


            if (CommonUtils.ShowWarningWithCancel(Properties.Resources.MSG_DELETE_CONFIRM) != DialogResult.OK)
            {
                return;
            }

            string delStr = "(";
            foreach (object obj in treeListView.SelectedObjects)
            {
                if (obj is BillTbl)
                {
                    BillTbl tbl = (BillTbl)obj;
                    delStr += tbl.ID + ",";
                }
                else if (obj is TreeNode)
                {
                    TreeNode tr = (TreeNode)obj;
                    if (tr.IsType)
                    {
                        foreach (int major_id in tr.DataList.Keys)
                        {
                            object[] res = (object[])tr.DataList[major_id];
                            if (res[2] != null)
                                foreach (BillTbl tbl in (ArrayList)res[2])
                                {
                                    delStr += tbl.ID + ",";
                                }
                        }
                    }
                    else if (tr.IsMajorCategory || tr.IsSubCategory)
                    {
                        foreach (BillTbl tbl in (ArrayList)(((object[])tr.DataList[tr.ID])[2]))
                        {
                            delStr += tbl.ID + ",";
                        }
                    }
                }
            }
            delStr = delStr.Substring(0, delStr.Length - 1) + ")";
            BillDB edb = new BillDB();
            edb.DelBill(delStr);

            SetDispData();
        }
        #endregion

        #region Disp Data
        public void SetDispData(int billId, object obj)
        {
            if (obj is DateTime)
            {
                SetDispDate();
                DateTime dt = (DateTime)obj;
                tcbDate.Text = string.Format("{0}{2}{1:00}", dt.Year, dt.Month, CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator);
            }
            else
            {
                SetDispData();
            }

            #region set focus
            if (billId != -1)
            {
                BillTbl tbl = new BillTbl();
                BillDB bdb = new BillDB();
                tbl.ID = billId;
                if (bdb.GetBill(tbl))
                {
                    int index = treeListView.IndexOf(tbl);
                    if (index != -1)
                    {
                        treeListView.EnsureVisible(index);
                        treeListView.SelectedIndex = index;
                    }
                }
            }
            #endregion
        }

        public void SetDispData()
        {
            #region Initialize
            string dateStr = tcbDate.Text;
            if (!btnShowDate.Checked)
                dateStr = string.Empty;
            string searchStr = tcbType.Text;
            BillDB bdb = new BillDB();
            object[] result = null;
            Dictionary<int, object> dataList = null;
            double total = 0;
            double totalAB = 0;
            #endregion

            #region Set Data
            ArrayList roots = new ArrayList();

            if (tcbType.SelectedIndex == 0)
            {
                #region Expense
                total = 0;
                totalAB = 0;
                result = bdb.GetDetail(dateStr, string.Empty, (int)CommonUtils.BillType.Expense, false, false);
                total += (double)result[0];
                dataList = (Dictionary<int, object>)result[1];
                totalAB += (double)result[2];
                foreach (int major_id in dataList.Keys)
                {
                    object[] obj = (object[])dataList[major_id];

                    TreeNode tr = new TreeNode();
                    tr.ID = major_id;
                    tr.Name = (string)obj[0];
                    tr.AmountSum = (double)obj[1];
                    tr.IsMajorCategory = true;
                    tr.DataList = dataList;
                    roots.Add(tr);
                }

                // Status Label
                if (btnShowDate.Checked && btnMonth.Checked)
                {
                    // month expense without annual
                    total -= totalAB;
                }
                this.toolStripStatusLabel.Text = string.Format(Properties.Resources.GroupBoxExpenseTitle, NumericUtils.ToCurrency(total));
                gbBalance.Text = string.Format(Properties.Resources.GroupBoxExpenseTitle, NumericUtils.ToCurrency(total));
                #endregion
            }
            else if (tcbType.SelectedIndex == 1)
            {
                #region Income
                total = 0;
                totalAB = 0;
                result = bdb.GetDetail(dateStr, string.Empty, (int)CommonUtils.BillType.Income, true, false);
                total += (double)result[0];
                dataList = (Dictionary<int, object>)result[1];
                totalAB += (double)result[2];
                foreach (int user_id in dataList.Keys)
                {
                    object[] obj = (object[])dataList[user_id];

                    TreeNode tr = new TreeNode();
                    tr.ID = user_id;
                    tr.Name = (string)obj[0];
                    tr.AmountSum = (double)obj[1];
                    tr.IsMajorCategory = true;
                    tr.DataList = dataList;
                    roots.Add(tr);
                }

                // Status Label
                this.toolStripStatusLabel.Text = string.Format(Properties.Resources.GroupBoxIncomeTitle, NumericUtils.ToCurrency(total));
                gbBalance.Text = string.Format(Properties.Resources.GroupBoxIncomeTitle, NumericUtils.ToCurrency(total));
                #endregion
            }
            else if (tcbType.SelectedIndex == 2)
            {
                #region Annual Budget
                TreeNode tn = null;
                total = 0;
                totalAB = 0;

                // Expense
                result = bdb.GetDetail(dateStr, string.Empty, (int)CommonUtils.BillType.Expense, false, true);
                total += (double)result[0];
                dataList = (Dictionary<int, object>)result[1];
                totalAB += (double)result[2];
                if (dataList.Count > 0)
                {
                    tn = new TreeNode();
                    tn.ID = (int)CommonUtils.BillType.Expense;
                    tn.AmountSum = (double)result[0];
                    tn.DataList = dataList;
                    tn.IsType = true;
                    tn.Name = "Expense";
                    roots.Add(tn);
                }

                // Income
                result = bdb.GetDetail(dateStr, string.Empty, (int)CommonUtils.BillType.Income, false, true);
                total += (double)result[0];
                dataList = (Dictionary<int, object>)result[1];
                totalAB += (double)result[2];
                if (dataList.Count > 0)
                {
                    tn = new TreeNode();
                    tn.ID = (int)CommonUtils.BillType.Income;
                    tn.AmountSum = (double)result[0];
                    tn.DataList = dataList;
                    tn.IsType = true;
                    tn.Name = "Income";
                    roots.Add(tn);
                }

                // Status Label
                this.toolStripStatusLabel.Text = string.Format(Properties.Resources.GroupBoxAnnualBudgetTitle, NumericUtils.ToCurrency(total));
                gbBalance.Text = string.Format(Properties.Resources.GroupBoxAnnualBudgetTitle, NumericUtils.ToCurrency(total));
                #endregion
            }
            else
            {
                #region All Detail
                TreeNode tn = null;
                total = 0;
                totalAB = 0;

                // Expense
#if DEBUG
                sw = new System.Diagnostics.Stopwatch();
                sw.Start();
#endif
                result = bdb.GetDetail(dateStr, searchStr, (int)CommonUtils.BillType.Expense, false, false);
                total += (double)result[0];
                dataList = (Dictionary<int, object>)result[1];
                totalAB += (double)result[2];
                if (dataList.Count > 0)
                {
                    tn = new TreeNode();
                    tn.ID = (int)CommonUtils.BillType.Expense;
                    tn.AmountSum = (double)result[0];
                    tn.DataList = dataList;
                    tn.IsType = true;
                    tn.Name = "Expense";
                    roots.Add(tn);
                }
#if DEBUG
                sw.Stop();
                System.Diagnostics.Debug.WriteLine(string.Format("Get Disp Expense Data in {0}ms", sw.ElapsedMilliseconds));
#endif

                // Income
#if DEBUG
                sw = new System.Diagnostics.Stopwatch();
                sw.Start();
#endif
                result = bdb.GetDetail(dateStr, searchStr, (int)CommonUtils.BillType.Income, false, false);
                total += (double)result[0];
                dataList = (Dictionary<int, object>)result[1];
                totalAB += (double)result[2];
                if (dataList.Count > 0)
                {
                    tn = new TreeNode();
                    tn.ID = (int)CommonUtils.BillType.Income;
                    tn.AmountSum = (double)result[0];
                    tn.DataList = dataList;
                    tn.IsType = true;
                    tn.Name = "Income";
                    roots.Add(tn);
                }

                // Status Label
                string label = Properties.Resources.LabelBalanceWithAnnual;
                if (string.IsNullOrEmpty(tcbType.Text.Trim()))
                {
                    if (btnShowDate.Checked && btnMonth.Checked)
                    {
                        // month balance without annual
                        total -= totalAB;
                        label = Properties.Resources.LabelBalanceWithoutAnnual;
                    }
                }
                this.toolStripStatusLabel.Text = string.Format(label, NumericUtils.ToCurrency(total));
                gbBalance.Text = string.Format(Properties.Resources.GroupBoxBalanceTitle, NumericUtils.ToCurrency(total));
#if DEBUG
                sw.Stop();
                System.Diagnostics.Debug.WriteLine(string.Format("Get Disp Income Data in {0}ms", sw.ElapsedMilliseconds));
#endif
                #endregion
            }
            
            this.treeListView.Roots = roots;
            if (treeListView.Items.Count > 0)
                this.treeListView.ExpandAll();

            treeListView.Focus();
            treeListView.Select();
            #endregion

            #region Set Charts
            CreatePieChart();
            CreateHBarChart(null);
            #endregion

            #region Set Search Suggest
            tcbType.AutoCompleteCustomSource.Clear();
            tcbType.AutoCompleteCustomSource.AddRange(bdb.GetSearchSuggests());
            #endregion
        }
        #endregion

        #region Chart
        private int RandomRGB
        {
            get { return randRGB.Next(0, 51) * 5; }
        }

        private Color RandomColor
        {
            get { return Color.FromArgb(255, RandomRGB, RandomRGB, RandomRGB); }
        }

        public void SetChart()
        {
            if (!isInitialize)
            {
                int sub_id = Convert.ToInt32(((ListItem)tcbChart.SelectedItem).ID);
                string sub_name = ((ListItem)tcbChart.SelectedItem).Name;

                BillDB edb = new BillDB();
                Hashtable ht = edb.GetMonthlyAmountByYearForChart(sub_id, false);

                MasterDB mdb = new MasterDB();
                double[] optimal = mdb.GetSubOptimal(sub_id);
                this.CreateChartLine(ht, sub_id, sub_name, optimal[0], optimal[1]);
            }
        }

        private void CreateHBarChart(ArrayList majorIdList)
        {
#if DEBUG
            sw = new System.Diagnostics.Stopwatch();
            sw.Start();
#endif
            barChart.Items.Clear();
            BillDB bdb = new BillDB();
            ArrayList list = bdb.GetExpenseDetailForPieChart(!btnShowDate.Checked ? "" : tcbDate.Text, true, majorIdList);
            for (int i = 0; i < list.Count; i++)
            {
                BaseTbl tbl = (BaseTbl)list[i];
                barChart.Items.Add(new HBarItem(tbl.Amount, tbl.Name, RandomColor));
            }
            barChart.RedrawChart();
#if DEBUG
            sw.Stop();
            System.Diagnostics.Debug.WriteLine(string.Format("Set HbarChart in {0}ms", sw.ElapsedMilliseconds));
#endif
        }

        private void CreatePieChart()
        {
#if DEBUG
            sw = new System.Diagnostics.Stopwatch();
            sw.Start();
#endif
            pieChart.Items.Clear();
            BillDB bdb = new BillDB();
            ArrayList list = bdb.GetExpenseDetailForPieChart(!btnShowDate.Checked ? "" : tcbDate.Text, false, null);
            for (int i = 0; i < list.Count; i++)
            {
                BaseTbl tbl = (BaseTbl)list[i];
                PieChartItem item = new PieChartItem(tbl.Amount, RandomColor, tbl.Name, string.Format("{0} : {1}", tbl.Name, NumericUtils.ToCurrency(tbl.Amount)), 0);
                item.Tag = tbl.ID;
                pieChart.Items.Add(item);
            }
#if DEBUG
            sw.Stop();
            System.Diagnostics.Debug.WriteLine(string.Format("Set PieChart in {0}ms", sw.ElapsedMilliseconds));
#endif
        }

        private void CreateChartLine(Hashtable ht, int sub_id, string title, double optimalValue, double optimalRange)
        {
#if DEBUG
            sw = new System.Diagnostics.Stopwatch();
            sw.Start();
#endif
            GraphPane myPane = zgcExpense.GraphPane;
            myPane.CurveList.Clear();
            myPane.GraphObjList.Clear();

            // Set the title and axis labels
            myPane.Title.Text = title;
            myPane.XAxis.Title.Text = Properties.Resources.LabelMonth;
            myPane.YAxis.Title.Text = Properties.Resources.LabelAmount;

            LineItem myCurve = null;

            int colorIndex = 0;
            ArrayList tmp = null;

            // Fill the axis background with a color gradient
            myPane.Chart.Fill = new Fill(Color.FromArgb(255, 255, 245), Color.FromArgb(255, 255, 190), 90F);

            int endYear = DateTime.Now.Year;
            int staYear = endYear - ht.Keys.Count + 1;

            for (int i = staYear; i <= endYear; i++)
            {
                if (ht[i.ToString()] == null)
                    continue;
                tmp = (ArrayList)ht[i.ToString()];

                // Generate a red curve with "Curve 1" in the legend
                myCurve = myPane.AddCurve(i.ToString(), (sub_id != 9 ? x : x_water), (double[])tmp.ToArray(typeof(double)), columnColor[colorIndex++]);
                // Make the symbols opaque by filling them with white
                myCurve.Symbol.Fill = new Fill(Color.White);
            }

            // Set the XAxis labels
            myPane.XAxis.Scale.TextLabels = sub_id != 9 ? CommonUtils.MONTH_NAMES : CommonUtils.MONTH_NAMES_WATER;
            // Set the XAxis to Text type
            myPane.XAxis.Type = AxisType.Text;
            // Display the Y axis grid lines
            myPane.YAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MinorGrid.IsVisible = true;

            myPane.XAxis.IsVisible = true;
            myPane.YAxis.IsVisible = true;

            #region Optimal Range
            if (optimalValue > 0 && optimalRange > 0)
            {
                // Draw a box item to highlight a value range
                BoxObj box = new BoxObj(0, (optimalValue + optimalRange / 2), 1, optimalRange, Color.Empty, Color.FromArgb(150, Color.LightGreen));
                box.Fill = new Fill(Color.White, Color.FromArgb(200, Color.LightGreen), 45.0F);
                // Use the BehindAxis zorder to draw the highlight beneath the grid lines
                box.ZOrder = ZOrder.E_BehindCurves;
                // Make sure that the boxObj does not extend outside the chart rect if the chart is zoomed
                box.IsClippedToChartRect = true;
                // Use a hybrid coordinate system so the X axis always covers the full x range
                // from chart fraction 0.0 to 1.0
                box.Location.CoordinateFrame = CoordType.XChartFractionYScale;
                myPane.GraphObjList.Add(box);

                // Add a text item to label the highlighted range
                TextObj text = new TextObj("Optimal\nRange", 0.95f, optimalValue, CoordType.AxisXYScale,
                                        AlignH.Right, AlignV.Center);
                text.FontSpec.Fill.IsVisible = false;
                text.FontSpec.Border.IsVisible = false;
                text.FontSpec.IsBold = true;
                text.FontSpec.IsItalic = true;
                text.Location.CoordinateFrame = CoordType.XChartFractionYScale;
                text.IsClippedToChartRect = true;
                myPane.GraphObjList.Add(text);
            }
            #endregion

            // Fill the pane background with a gradient
            myPane.Fill = new Fill(Color.White, Color.SteelBlue, 45.0F);

            // Calculate the Axis Scale Ranges
            zgcExpense.AxisChange();
            zgcExpense.Invalidate();
#if DEBUG
            sw.Stop();
            System.Diagnostics.Debug.WriteLine(string.Format("Set LineChart in {0}ms", sw.ElapsedMilliseconds));
#endif
        }
        #endregion

        #region Others
        public int GetTabIndex()
        {
            return tcMain.SelectedIndex;
        }

        private void CheckDefaultData()
        {
            BillDB edb = new BillDB();
            edb.CheckExpenseFixed();
        }

        private void DoBackup()
        {
            bool bRet = false;
            try
            {
                // 1. check directory
                string backup_path = Path.Combine(Application.StartupPath, CommonUtils.SYSTEM_BACKUP_PATH);
                if (!Directory.Exists(backup_path))
                {
                    Directory.CreateDirectory(backup_path);
                }

                // 2. clear older data (older than specified days)
                string[] files = Directory.GetFiles(backup_path);
                foreach (string fileName in files)
                {
                    DateTime lastDate = File.GetLastWriteTime(fileName);
                    if (lastDate.AddDays(CommonUtils.KEEP_DB_BACKUP_FILE_DAYS).CompareTo(DateTime.Now) < 0)
                    {
                        File.Delete(fileName);
                    }
                }

                // 3. do backup
                string backup_file = DateTime.Now.ToString("yyyyMMdd") + "_" + CommonUtils.DB_NAME;
                backup_file = Path.Combine(backup_path, backup_file);
                File.Copy(BaseDB.SQLiteDB, backup_file, true);
                bRet = true;
            }
            catch (Exception e)
            {
                CommonUtils.ShowError(e.ToString());
            }

            if (bRet)
            {
                CommonUtils.ShowSuccess(Properties.Resources.MSG_BACKUP_OK);
            }
        }

        private void DoVacuum()
        {
            SystemDB sys = new SystemDB();

            if ((BaseDB.DoVacuum() && sys.ChangeLastVacuumDate(DateTime.Now.ToShortDateString())))
            {
                CommonUtils.ShowSuccess(Properties.Resources.MSG_VACUUM);
            }
        }

        private void ShowOption()
        {
            OptionForm f = new OptionForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.SetDispData();
            }
            f.Dispose();
        }

        private void ShowAbout()
        {
            AboutBox b = new AboutBox();
            b.ShowDialog(this);
            b.Dispose();
        }

        private void ExpandGroup()
        {
            if (treeListView.SelectedObjects != null && treeListView.SelectedObjects.Count == 1 && treeListView.SelectedObject is TreeNode)
                treeListView.Expand(treeListView.SelectedObject);
            else
                treeListView.ExpandAll();
        }

        private void CollapseGroup()
        {
            if (treeListView.SelectedObjects != null && treeListView.SelectedObjects.Count == 1 && treeListView.SelectedObject is TreeNode)
                treeListView.Collapse(treeListView.SelectedObject);
            else
                treeListView.CollapseAll();
        }

        private void SetDispDate()
        {
            // get data
            BillDB bdb = new BillDB();
            DateTime[] dateRange = bdb.GetBillDateRange();
            bool monthVisible = btnMonth.Checked;

            // set data
            tcbDate.Items.Clear();
            int i = 0;
            DateTime cDate = DateTime.MinValue;
            while (true)
            {
                if (monthVisible)
                {
                    cDate = dateRange[0].AddMonths(i++);
                    tcbDate.Items.Add(string.Format("{0}{2}{1:00}", cDate.Year, cDate.Month, CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator));
                    if (cDate.Year >= dateRange[1].Year && cDate.Month >= dateRange[1].Month)
                        break;
                }
                else
                {
                    cDate = dateRange[0].AddYears(i++);
                    tcbDate.Items.Add(cDate.Year.ToString());
                    if (cDate.Year >= dateRange[1].Year)
                        break;
                }
            }

            // set disp data
            if (monthVisible)
                tcbDate.Text = string.Format("{0}{2}{1:00}", DateTime.Now.Year, DateTime.Now.Month, CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator);
            else
                tcbDate.Text = DateTime.Now.Year.ToString();
        }

        private void SetHomeButton(bool enabled)
        {
            btnNew.Enabled = enabled;
            btnEdit.Enabled = enabled;
            btnDel.Enabled = enabled;
            btnMerge.Enabled = enabled;
            btnCollapse.Enabled = enabled;
            btnExpand.Enabled = enabled;
        }
        #endregion

        #endregion

        #region Events
        #region Context Menu Strip
        private void mergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MergeBill();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditBill();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DelBill();
        }

        private void expandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpandGroup();
        }

        private void collapseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CollapseGroup();
        }
        #endregion

        #region TreeListView Event
        private void treeListView_ItemActivate(object sender, EventArgs e)
        {
            Object model = this.treeListView.SelectedObject;
            if (model != null)
                this.treeListView.ToggleExpansion(model);
        }

        private void treeListView_SelectionChanged(object sender, EventArgs e)
        {
            if (treeListView.SelectedObjects.Count > 0)
            {
                double total = 0;
                foreach (Object obj in treeListView.SelectedObjects)
                {
                    if (obj is BillTbl)
                        total += ((BillTbl)obj).Amount;
                }
                this.toolStripStatusLabel.Text = String.Format(Resources.StatusLabel, treeListView.SelectedObjects.Count.ToString("n0"), treeListView.NodeCount.ToString("n0"), NumericUtils.ToCurrency(total));
            }
        }

        private void List_DoubleClick(object sender, EventArgs e)
        {
            EditBill();
        }

        private void List_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DelBill();
            }
        }
        #endregion

        #region Tab Event
        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tcMain.SelectedIndex)
            {
                case 1:
                    // chart
                    SetChart();
                    SetHomeButton(false);
                    break;
                default:
                    SetHomeButton(true);
                    treeListView.Focus();
                    break;
            }
            tcbChart.Visible = (tcMain.SelectedIndex == 1);
            bool isHome = (tcMain.SelectedIndex == 0);
            btnShowDate.Visible = isHome;
            btnPrev.Visible = isHome;
            btnNext.Visible = isHome;
            tcbDate.Visible = isHome;
            tcbType.Visible = isHome;
            btnMonth.Visible = isHome;
            toolStripSeparator2.Visible = isHome;
        }
        #endregion

        #region ToolStrip
        private void btnNewBill_Click(object sender, EventArgs e)
        {
            NewBill();
        }

        private void btnOption_Click(object sender, EventArgs e)
        {
            ShowOption();
        }

        private void btnExpand_Click(object sender, EventArgs e)
        {
            ExpandGroup();
        }

        private void btnCollapse_Click(object sender, EventArgs e)
        {
            CollapseGroup();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            EditBill();
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            MergeBill();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DelBill();
        }

        private void btnVacuum_Click(object sender, EventArgs e)
        {
            DoVacuum();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            DoBackup();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            ShowAbout();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tcbChart_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetChart();
        }

        private void tcbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDispData();
        }

        private void tcbType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string s = tcbType.Text.Trim().ToLower();

                bool isSelected = false;
                foreach (string item in tcbType.Items)
                {
                    if (item.ToLower() == s)
                    {
                        isSelected = true;
                        s = item;
                    }
                }
                if (isSelected)
                    tcbType.Text = s;
                else
                    SetDispData();
            }
        }

        private void btnShowDate_Click(object sender, EventArgs e)
        {
            btnShowDate.Checked = !btnShowDate.Checked;
            bool showDate = btnShowDate.Checked;
            btnPrev.Enabled = showDate;
            btnNext.Enabled = showDate;
            tcbDate.Enabled = showDate;
            btnMonth.Enabled = showDate;
            SetDispData();
        }

        private void btnMonth_Click(object sender, EventArgs e)
        {
            btnMonth.Checked = !btnMonth.Checked;
            SetDispDate();
        }

        private void tcbDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPrev.Enabled = !(tcbDate.SelectedIndex == 0);
            btnNext.Enabled = !(tcbDate.SelectedIndex == tcbDate.Items.Count - 1);
            SetDispData();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            tcbDate.SelectedIndex--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tcbDate.SelectedIndex++;
        }
        #endregion

        #region PieChart
        private void pieChart_ItemClicked(object sender, PieChart.PieChartItemEventArgs e)
        {
            if (e.Item.IsOffset)
            {
                e.Item.Offset = Math.Max(0, e.Item.Offset - 20);
                e.Item.IsOffset = false;
            }
            else
            {
                e.Item.Offset += 20;
                e.Item.IsOffset = true;
            }

            ArrayList list = new ArrayList();
            foreach (PieChartItem item in pieChart.Items)
                if (item.IsOffset) list.Add((int)item.Tag);

            CreateHBarChart(list);
        }
        #endregion
        #endregion

        #region Tree Node Class
        internal sealed class TreeNode
        {
            #region Properties
            private bool isType;
            private bool isMajorCategory;
            private bool isSubCategory;
            private int id;
            private string name;
            private double amountSum;
            private Dictionary<int, object> dataList;

            public bool IsType { get { return isType; } set { isType = value; } }
            public bool IsMajorCategory { get { return isMajorCategory; } set { isMajorCategory = value; } }
            public bool IsSubCategory { get { return isSubCategory; } set { isSubCategory = value; } }
            public int ID { get { return id; } set { id = value; } }
            public string Name { get { return name; } set { name = value; } }
            public double AmountSum { get { return amountSum; } set { amountSum = value; } }
            public Dictionary<int, object> DataList { get { return dataList; } set { dataList = value; } }
            #endregion

            #region Constructor
            public TreeNode()
            {
                IsType = false;
                IsMajorCategory = false;
                IsSubCategory = false;
                AmountSum = 0;
            }
            #endregion

            #region Methods
            public ArrayList GetChildren()
            {
                ArrayList list = new ArrayList();
                if (IsType)
                {
                    if (DataList != null)
                    {
                        foreach (int major_id in DataList.Keys)
                        {
                            object[] obj = (object[])DataList[major_id];

                            TreeNode tr = new TreeNode();
                            tr.ID = major_id;
                            tr.Name = (string)obj[0];
                            tr.AmountSum = (double)obj[1];
                            tr.IsMajorCategory = true;
                            tr.DataList = DataList;
                            list.Add(tr);
                        }
                    }
                }
                else if (IsMajorCategory)
                {
                    if (DataList != null)
                    {
                        object[] obj = (object[])DataList[ID];

                        Dictionary<int, object> dic = new Dictionary<int, object>();
                        string sub_name = string.Empty;
                        ArrayList tmp = null;
                        double total = 0;
                        ArrayList billList = (ArrayList)obj[2];
                        foreach (BillTbl tbl in billList)
                        {
                            object[] sObj = null;
                            if (!dic.ContainsKey(tbl.SubId))
                            {
                                sObj = new object[3];
                                tmp = new ArrayList();
                                total = 0;
                                sub_name = tbl.SubName;
                            }
                            else
                            {
                                sObj = (object[])dic[tbl.SubId];
                                sub_name = (string)sObj[0];
                                total = (double)sObj[1];
                                tmp = (ArrayList)sObj[2];
                            }

                            total += tbl.Amount;
                            tmp.Add(tbl);
                            sObj[0] = sub_name;
                            sObj[1] = total;
                            sObj[2] = tmp;

                            dic[tbl.SubId] = sObj;
                        }

                        foreach (int sub_id in dic.Keys)
                        {
                            object[] sObj = (object[])dic[sub_id];
                            tmp = (ArrayList)sObj[2];
                            if (tmp.Count > 1)
                            {
                                TreeNode tr = new TreeNode();
                                tr.ID = sub_id;
                                tr.Name = (string)sObj[0];
                                tr.AmountSum = (double)sObj[1];
                                tr.IsSubCategory = true;
                                tr.DataList = dic;
                                list.Add(tr);
                            }
                            else
                            {
                                list.Add(tmp[0]);
                            }
                        }


                        return list;
                    }
                }
                else if (IsSubCategory)
                {
                    if (DataList != null)
                    {
                        object[] obj = (object[])DataList[ID];
                        return (ArrayList)obj[2];
                    }
                }

                return list;
            }
            #endregion
        }
        #endregion
    }
}