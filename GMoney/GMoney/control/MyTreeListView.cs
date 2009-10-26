using System;
using System.Collections.Generic;
using System.Text;
using BrightIdeasSoftware;
using System.Windows.Forms;
using System.Drawing;
using GMoney.util;
using GMoney.instance;

namespace GMoney.control
{
    class MyTreeListView : TreeListView
    {
        #region Overrides
#if DEBUG
        //protected override void CorrectSubItemColors(ListViewItem olvi)
        //{
        //    olvi.UseItemStyleForSubItems = false;
        //    if (this.OwnerDraw)
        //    {
        //        foreach (ListViewItem.ListViewSubItem si in olvi.SubItems)
        //        {
        //            si.BackColor = olvi.BackColor;
        //            if (NumericUtils.IsNumeric(si.Text) && double.Parse(si.Text.Replace(",", "")) < 0)
        //            {
        //                si.ForeColor = Color.Red;
        //            }
        //            else
        //            {
        //                si.ForeColor = olvi.ForeColor;
        //            }
        //        }
        //    }
        //}
#endif
        public override int IndexOf(Object modelObject)
        {
            for (int i = 0; i < this.GetItemCount(); i++)
            {
                if (this.GetModelObject(i) is BillTbl && modelObject is BillTbl)
                {
                    if (((BillTbl)this.GetModelObject(i)).ID == ((BillTbl)modelObject).ID)
                        return i;
                }
            }
            return -1;
        }
        #endregion

        #region Properties
        public int NodeCount
        {
            get
            {
                int total = 0;
                foreach (object obj in this.Objects)
                    if (obj is BillTbl)
                        total++;

                return total;
            }
        }
        #endregion
    }
}
