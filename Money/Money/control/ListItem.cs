using System;
using System.Collections.Generic;
using System.Text;

namespace Money.control
{
    class ListItem
    {
        #region Properties
        private object id;
        private string name;

        public object ID { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        #endregion

        #region Constructor
        public ListItem() { }

        public ListItem(object sid, string sname)
        {
            ID = sid;
            Name = sname;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return this.Name;
        }
        #endregion
    }
}
