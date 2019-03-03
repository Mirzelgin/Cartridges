namespace CartridgesNS.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CartridgeRefillingDetails
    {
        public int Id { get; set; }

        public int Report { get; set; }

        public int Service { get; set; }

        public int? Cartridge { get; set; }

        [StringLength(50)]
        public string Device { get; set; }

        public virtual CartridgeRefillingReports CartridgeRefillingReports { get; set; }

        public virtual Cartridges Cartridges { get; set; }

        public virtual Devices Devices { get; set; }

        public virtual Services Services { get; set; }
    }
}
