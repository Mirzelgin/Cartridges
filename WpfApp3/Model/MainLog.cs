namespace WpfApp3.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MainLog")]
    public partial class MainLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "datetime2")]
        public DateTime DateTime { get; set; }

        public int LocationId { get; set; }

        public int? CartridgeId { get; set; }

        [Required]
        [StringLength(100)]
        public string DeviceId { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        public string Note { get; set; }

        public virtual DeviceAssociation DeviceAssociation { get; set; }
    }
}
