using System;
using System.Collections.Generic;
using System.Text;
using GMoney.util;

namespace GMoney.instance
{
    class BillTbl : BaseTbl,IComparable
    {
        #region Properties
        private int majorId;
        private string majorName;
        private int majorSort;
        private int subId;
        private string subName;
        private int subSort;
        private int billType;
        private int userId;
        private string userName;
        private int paymentId;
        private string paymentName;
        private int annualBudget;
        private int splitMonths;
        private int fixedId;

        public int MajorId { get { return majorId; } set { majorId = value; } }
        public string MajorName { get { return majorName; } set { majorName = value; } }
        public int MajorSort { get { return majorSort; } set { majorSort = value; } }
        public int SubId { get { return subId; } set { subId = value; } }
        public string SubName { get { return subName; } set { subName = value; } }
        public int SubSort { get { return subSort; } set { subSort = value; } }
        public int BillType { get { return billType; } set { billType = value; } }
        public int UserId { get { return userId; } set { userId = value; } }
        public string UserName { get { return userName; } set { userName = value; } }
        public int PaymentId { get { return paymentId; } set { paymentId = value; } }
        public string PaymentName { get { return paymentName; } set { paymentName = value; } }
        public int AnnualBudget { get { return annualBudget; } set { annualBudget = value; } }
        public int SplitMonths { get { return splitMonths; } set { splitMonths = value; } }
        public int FixedId { get { return fixedId; } set { fixedId = value; } }
        public bool IsAnnualBudget { get { return AnnualBudget == (int)CommonUtils.AnnualBudget.Yes; } }
        #endregion

        #region Constructor
        public BillTbl()
        {
            ID = 0;
            CreateDate = DateTime.Now;
            UpdateDate = DateTime.Now;
            Remarks = string.Empty;
            MajorId = 0;
            MajorName = null;
            MajorSort = 0;
            SubId = 0;
            SubName = null;
            SubSort = 0;
            Amount = 0;
            UserId = CommonUtils.DEFAULT_USER_ID;
            SplitMonths = 1;
            BillType = (int)CommonUtils.BillType.Expense;

            PaymentId = 0;
            PaymentName = null;
            AnnualBudget = (int)CommonUtils.AnnualBudget.No;
            FixedId = -1;
        }
        #endregion

        #region IComparable Members

        public int CompareTo(object obj)
        {
            BillTbl p = obj as BillTbl;
            if (p == null)
            {
                return -1;
            }
            if (MajorSort == p.MajorSort)
                return SubSort - p.SubSort;
            return MajorSort - p.MajorSort;
        }

        #endregion
    }
}
