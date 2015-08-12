using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using GMoney.db;
using GMoney.instance;
using GMoney.util;
using System.Collections;
using BrightIdeasSoftware;

namespace GMoney.control
{
    public partial class CategoryList : DropDownControl
    {
        bool _ignoreChange = false;

        public CategoryList()
        {
            InitializeComponent();
            InitializeDropDown(olvCategory);

            //olvType.AspectGetter = delegate(object x)
            //{
            //    BillTbl t = (BillTbl)x;

            //    return t.BillType == (int)CommonUtils.BillType.Expense ? "Expense" : "Income";
            //};
            olvCategory.SelectionChanged += new EventHandler(olvCategory_SelectionChanged);
            olvCategory.CustomSorter = delegate(OLVColumn column, SortOrder order)
            {
                olvCategory.ListViewItemSorter = new ColumnComparer(new OLVColumn("ignore", "MajorSort"), SortOrder.Ascending, column, order);
            };
        }

        void olvCategory_SelectionChanged(object sender, EventArgs e)
        {
            if (_ignoreChange) return;

            if (this.DropState == eDropState.Dropping || this.DropState == eDropState.Closing) return;
            if (olvCategory.SelectedItems.Count == 0) return;

            BillTbl t = (BillTbl)olvCategory.SelectedObject;

            this.Text = string.Format("{0} : {1}", t.MajorName, t.SubName);
            this.CloseDropDown();
            if (CategoryChanged != null)
                CategoryChanged(null, null);
        }

        public event EventHandler CategoryChanged;

        public void SetItems(IEnumerable items)
        {
            olvCategory.SetObjects(items);
        }

        public int BillType
        {
            get { return ((BillTbl)olvCategory.SelectedObject).BillType; }
        }

        public int ID
        {
            get { return ((BillTbl)olvCategory.SelectedObject).SubId; }
            set
            {
                if (olvCategory.Objects == null)
                    return;

                foreach (object obj in olvCategory.Objects)
                {
                    BillTbl t = (BillTbl)obj;
                    if (t.SubId == value)
                    {
                        _ignoreChange = true;
                        olvCategory.SelectedObject = t;
                        _ignoreChange = false;
                    }
                }
            }
        }
    }
}
