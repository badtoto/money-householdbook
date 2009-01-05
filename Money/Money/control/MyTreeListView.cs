using System;
using System.Collections.Generic;
using System.Text;
using BrightIdeasSoftware;
using System.Windows.Forms;
using System.Drawing;
using Money.util;
using Money.instance;

namespace Money.control
{
    class MyTreeListView : TreeListView
    {
        #region Overrides
        protected override void CorrectSubItemColors(ListViewItem olvi)
        {
            if (this.OwnerDraw && olvi.UseItemStyleForSubItems)
            {
                foreach (ListViewItem.ListViewSubItem si in olvi.SubItems)
                {
                    si.BackColor = olvi.BackColor;
                    if (NumericUtils.IsNumeric(si.Text) && double.Parse(si.Text.Replace(",", "")) < 0)
                    {
                        si.ForeColor = Color.Red;
                    }
                    else
                    {
                        si.ForeColor = olvi.ForeColor;
                    }
                }
            }
        }

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
