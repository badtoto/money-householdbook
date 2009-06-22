using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Money.util
{
    public static class NumericUtils
    {
        #region Methods
        public static bool IsNumeric(object ValueToCheck)
        {
            double Dummy = new double();
            return double.TryParse(Convert.ToString(ValueToCheck), System.Globalization.NumberStyles.Any, null, out Dummy);
        }

        public static string ToCurrency(double Value)
        {
            return Value.ToString("n0");
        }
        #endregion
    }
}
