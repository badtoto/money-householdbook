using System;
using System.Collections.Generic;
using System.Text;

namespace Money.instance
{
    class BaseTbl
    {
        #region Properties
        private int id;
        private string name;
        private double amount;
        private DateTime date;
        private DateTime createDate;
        private DateTime updateDate;
        private string remarks;
        private int sort;
        private int deleteFlg;

        public int ID { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public double Amount { get { return amount; } set { amount = value; } }
        public DateTime Date { get { return date; } set { date = value; } }
        public DateTime CreateDate { get { return createDate; } set { createDate = value; } }
        public DateTime UpdateDate { get { return updateDate; } set { updateDate = value; } }
        public string Remarks { get { return remarks; } set { remarks = value; } }
        public int Sort { get { return sort; } set { sort = value; } }
        public int DeleteFlg { get { return deleteFlg; } set { deleteFlg = value; } }
        #endregion

        #region Constructor
        public BaseTbl()
        {
            ID = 0;
            CreateDate = DateTime.Now;
            UpdateDate = DateTime.Now;
            Remarks = string.Empty;
            Amount = 0;
            DeleteFlg = 0;
        }
        #endregion
    }
}
