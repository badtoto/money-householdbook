using System;
using System.Collections.Generic;
using System.Text;

namespace Money.instance
{
    class SubTbl : BaseTbl
    {
        #region Properties
        private double optimal;
        private double range;

        public double Optimal { get { return optimal; } set { optimal = value; } }
        public double Range { get { return range; } set { range = value; } }
        #endregion

        #region Constructor
        public SubTbl()
        {
            Name = string.Empty;
            Optimal = 0;
            Range = 0;
        }
        #endregion
    }
}
