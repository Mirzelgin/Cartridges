namespace CartridgesNS.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CartridgeRefillingReports
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CartridgeRefillingReports()
        {
            CartridgeRefillingDetails = new HashSet<CartridgeRefillingDetails>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Number { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartridgeRefillingDetails> CartridgeRefillingDetails { get; set; }
    }
}
