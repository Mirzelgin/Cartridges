namespace CartridgesNS.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Devices
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Devices()
        {
            CartridgeRefillingDetails = new HashSet<CartridgeRefillingDetails>();
            MainLog = new HashSet<MainLog>();
        }

        [Key]
        [StringLength(50)]
        public string InventoryId { get; set; }

        [Required]
        [StringLength(1024)]
        public string Name { get; set; }

        public int Location { get; set; }

        public int Cartridge { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartridgeRefillingDetails> CartridgeRefillingDetails { get; set; }

        public virtual Cartridges Cartridges { get; set; }

        public virtual Locations Locations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MainLog> MainLog { get; set; }
    }
}
