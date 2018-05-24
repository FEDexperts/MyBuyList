using System;

namespace MyBuyList.Shared.Entities
{
    [Serializable]
    partial class MeasurementUnit
    {
        public bool AllowDelete
        {
            get
            {
                return true;
            }
        }
    }
}