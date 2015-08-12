using System;
using System.Collections.Generic;
using System.Text;
using BrightIdeasSoftware;
using System.Windows.Forms;
using Money.control;
using Money.util;
using System.Drawing;
using System.ComponentModel;
using Money.instance;

namespace Money.control
{
    public partial class MyListView : ObjectListView
    {
        /// <summary>
        /// Organise the view items into groups, based on the given column
        /// </summary>
        /// <param name="column">The column whose values should be used for sorting.</param>
        public override void BuildGroups(OLVColumn column)
        {
            if (column == null)
                column = this.GetColumn(0);

            this.Groups.Clear();

            // Getting the Count forces any internal cache of the ListView to be flushed. Without
            // this, iterating over the Items will not work correctly if the ListView handle
            // has not yet been created.
            int dummy = this.Items.Count;

            // Separate the list view items into groups, using the group key as the descrimanent
            Dictionary<object, List<OLVListItem>> map = new Dictionary<object, List<OLVListItem>>();
            foreach (OLVListItem olvi in this.Items)
            {
                object key = column.GetGroupKey(olvi.RowObject);
                if (key == null)
                    key = "{null}"; // null can't be used as the key for a dictionary
                if (!map.ContainsKey(key))
                    map[key] = new List<OLVListItem>();
                map[key].Add(olvi);
            }

            // Make a list of the required groups
            List<ListViewGroup> groups = new List<ListViewGroup>();
            foreach (object key in map.Keys)
            {
                ListViewGroup lvg = new ListViewGroup(column.ConvertGroupKeyToTitle(key));
                lvg.Tag = key;
                groups.Add(lvg);
            }

            // Sort the groups
            groups.Sort(new ListViewGroupComparer(this.LastSortOrder));

            // Put each group into the list view, and give each group its member items.
            // The order of statements is important here:
            // - the header must be calculate before the group is added to the list view,
            //   otherwise changing the header causes a nasty redraw (even in the middle of a BeginUpdate...EndUpdate pair)
            // - the group must be added before it is given items, otherwise an exception is thrown (is this documented?)
            string fmt = column.GroupWithItemCountFormatOrDefault;
            string singularFmt = column.GroupWithItemCountSingularFormatOrDefault;
            ColumnComparer itemSorter = new ColumnComparer((this.SortGroupItemsByPrimaryColumn ? this.GetColumn(0) : column),
                                                           this.LastSortOrder, this.SecondarySortColumn, this.SecondarySortOrder);
            foreach (ListViewGroup group in groups)
            {
                double total = 0;
                // get total amount 
                foreach (OLVListItem listItem in map[group.Tag])
                {
                    BillTbl p = (BillTbl)listItem.RowObject;
                    total += p.Amount;
                }
                group.Header = String.Format("{0} ({1})", group.Header, NumericUtils.ToCurrency(total));
                this.Groups.Add(group);
                // If there is no sort order, don't sort since the sort isn't stable
                if (this.LastSortOrder != SortOrder.None)
                    map[group.Tag].Sort(itemSorter);
                group.Items.AddRange(map[group.Tag].ToArray());
            }
        }

        /// <summary>
        /// For some reason, UseItemStyleForSubItems doesn't work for the colors
        /// when owner drawing the list, so we have to specifically give each subitem
        /// the desired colors
        /// </summary>
        /// <param name="olvi">The item whose subitems are to be corrected</param>
        protected override void CorrectSubItemColors(ListViewItem olvi)
        {
            // TODO : Remember to change ObjectListView to virtual
            if (this.OwnerDraw && olvi.UseItemStyleForSubItems)
            {
                foreach (ListViewItem.ListViewSubItem si in olvi.SubItems)
                {
                    si.BackColor = olvi.BackColor;
                    if (si.Text.IsNumeric() && double.Parse(si.Text.Replace(",", "")) < 0)
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
    }
}
