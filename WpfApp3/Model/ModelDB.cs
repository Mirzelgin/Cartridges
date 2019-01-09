namespace WpfApp3.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelDB : DbContext
    {
        public ModelDB()
            : base(Properties.Settings.Default.ModelDB)
        {
        }

        public virtual DbSet<Cartridges> Cartridges { get; set; }
        public virtual DbSet<DeviceAssociation> DeviceAssociation { get; set; }
        public virtual DbSet<Devices> Devices { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<MainLog> MainLog { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cartridges>()
                .HasMany(e => e.DeviceAssociation)
                .WithRequired(e => e.Cartridges)
                .HasForeignKey(e => e.CartridgeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DeviceAssociation>()
                .HasMany(e => e.MainLog)
                .WithOptional(e => e.DeviceAssociation)
                .HasForeignKey(e => new { e.DeviceId, e.CartridgeId, e.LocationId });

            modelBuilder.Entity<Devices>()
                .HasMany(e => e.DeviceAssociation)
                .WithRequired(e => e.Devices)
                .HasForeignKey(e => e.DeviceId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Locations>()
                .HasMany(e => e.DeviceAssociation)
                .WithRequired(e => e.Locations)
                .HasForeignKey(e => e.LocationId)
                .WillCascadeOnDelete(false);
        }
    }
}
