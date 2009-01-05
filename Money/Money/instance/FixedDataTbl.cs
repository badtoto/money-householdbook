using System;
using System.Collections.Generic;
using System.Text;
using Money.util;

namespace Money.instance
{
    class FixedDataTbl : BillTbl
    {
        #region Properties
        private int interval;
        private int intervalType;
        private string intervalDetail;
        DateTime startDate;
        DateTime endDate;

        public int Interval { get { return interval; } set { interval = value; } }
        public int IntervalType { get { return intervalType; } set { intervalType = value; } }// 0: every day, 1: every week, 2:every month, 3:every year
        public string IntervalDetail { get { return intervalDetail; } set { intervalDetail = value; } }
        public DateTime StartDate { get { return startDate; } set { startDate = value; } }
        public DateTime EndDate { get { return endDate; } set { endDate = value; } }
        #endregion

        #region Constructor
        public FixedDataTbl():base()
        {
            SubId = -1;
            Interval = 0;
            IntervalType = (int)CommonUtils.IntervalType.EveryMonth;
            EndDate = DateTime.MaxValue;
        }
        #endregion
    }
}
