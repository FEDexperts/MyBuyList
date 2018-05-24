using System;
using System.Data.Linq;

namespace MyBuyList.Shared.Entities
{
    [Serializable]
    partial class MCategory
    {
        public EntitySet<MCategory> ParentMCategories
        {
            get
            {
                return this._MCategories;
            }
        }

        public MCategory ParentMCategory
        {
            get { return this.MCategory1; }
            set { this.MCategory1 = value; }
        }

        private int _menusCount;
        public int MenusCount
        {
            get { return _menusCount; }
            set { _menusCount = value; }
        }

        private bool _allowDelete;
        public bool AllowDelete
        {
            get { return _allowDelete; }
            internal set { _allowDelete = value; }
        }
    }
}