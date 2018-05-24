namespace MyBuyListDL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Articles
    {
        [Key]
        public int ArticleId { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Title { get; set; }

        [Column(TypeName = "ntext")]
        public string Abstract { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Body { get; set; }

        [Required]
        [StringLength(50)]
        public string Publisher { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
