namespace MyBuyListDL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MissingLists
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MissingLists()
        {
            MissingListDetails = new HashSet<MissingListDetails>();
        }

        public int ID { get; set; }

        public int CREATED_BY { get; set; }

        public DateTime CREATE_DATE { get; set; }

        public bool? ACTIVE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MissingListDetails> MissingListDetails { get; set; }

        public virtual Users Users { get; set; }
    }
}
