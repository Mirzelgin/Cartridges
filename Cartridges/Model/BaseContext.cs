namespace CartridgesNS.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BaseContext : DbContext
    {
        public BaseContext()
            : base("name=BaseContext")
        {
        }

        public virtual DbSet<CartridgeRefillingDetails> CartridgeRefillingDetails { get; set; }
        public virtual DbSet<CartridgeRefillingReports> CartridgeRefillingReports { get; set; }
        public virtual DbSet<Cartridges> Cartridges { get; set; }
        public virtual DbSet<Devices> Devices { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<MainLog> MainLog { get; set; }
        public virtual DbSet<Services> Services { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartridgeRefillingReports>()
                .HasMany(e => e.CartridgeRefillingDetails)
                .WithRequired(e => e.CartridgeRefillingReports)
                .HasForeignKey(e => e.Report)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cartridges>()
                .HasMany(e => e.CartridgeRefillingDetails)
                .WithOptional(e => e.Cartridges)
                .HasForeignKey(e => e.Cartridge);

            modelBuilder.Entity<Cartridges>()
                .HasMany(e => e.Devices)
                .WithRequired(e => e.Cartridges)
                .HasForeignKey(e => e.Cartridge)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Devices>()
                .HasMany(e => e.CartridgeRefillingDetails)
                .WithOptional(e => e.Devices)
                .HasForeignKey(e => e.Device);

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

            modelBuilder.Entity<Services>()
                .HasMany(e => e.CartridgeRefillingDetails)
                .WithRequired(e => e.Services)
                .HasForeignKey(e => e.Service)
                .WillCascadeOnDelete(false);
        }
    }
}
