namespace ExampleEF.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Cartridges> Cartridges { get; set; }
        public virtual DbSet<Devices> Devices { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<MainLog> MainLog { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cartridges>()
                .HasMany(e => e.Devices)
                .WithRequired(e => e.Cartridges)
                .HasForeignKey(e => e.Cartridge)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Devices>()
                .HasMany(e => e.MainLog)
                .WithRequired(e => e.Devices)
                .HasForeignKey(e => e.Device)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Locations>()
                .HasMany(e => e.Devices)
                .WithRequired(e => e.Locations)
                .HasForeignKey(e => e.Location)
                .WillCascadeOnDelete(false);
        }
    }
}
