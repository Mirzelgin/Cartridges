using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace WpfApp3.Model
{
    class CartridgeContext : DbContext
    {
        public CartridgeContext() : base(Properties.Settings.Default.connectionStrings) { }

        public DbSet<Cartridge> Cartridges { get; set; }
    }
}
