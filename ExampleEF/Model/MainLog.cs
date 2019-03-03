namespace ExampleEF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MainLog")]
    public partial class MainLog
    {
        public int Id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(50)]
        public string Device { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        public string Note { get; set; }

        public virtual Devices Devices { get; set; }
    }
}
