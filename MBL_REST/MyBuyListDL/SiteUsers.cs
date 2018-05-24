namespace MyBuyListDL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SiteUsers
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string DisplayName { get; set; }

        public int UserTypeId { get; set; }

        [Required]
        [StringLength(250)]
        public string Email { get; set; }

        public bool AgreeToMail { get; set; }

        public virtual UserTypes UserTypes { get; set; }
    }
}
