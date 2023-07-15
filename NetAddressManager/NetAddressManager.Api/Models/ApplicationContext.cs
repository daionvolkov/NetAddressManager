using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace NetAddressManager.Api.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<CoreSwitch> CoreSwitch { get; set; }
        public DbSet<AggregationSwitch> AggregationSwitch { get; set; }
        public DbSet<AccessSwitch> AccessSwitch { get; set; }
        public DbSet<PostalAddress> PostalAddress { get; set; }
        public DbSet<EquipmentManufacturer> EquipmentManufacturer { get; set; }
        public DbSet<SwitchPort> SwitchPort { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CoreSwitch>().Property(e => e.PostalAddressId).HasColumnName("PostalAddressId1");
            modelBuilder.Entity<AggregationSwitch>().Property(e => e.PostalAddressId).HasColumnName("PostalAddressId1");
            modelBuilder.Entity<AccessSwitch>().Property(e => e.PostalAddressId).HasColumnName("PostalAddressId1");

            base.OnModelCreating(modelBuilder);
        }
    }
}
