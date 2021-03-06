using Microsoft.EntityFrameworkCore;
using Vincent.Core.Models;

namespace Vincent.Core
{
    public class VincentDbContext : DbContext
    {
        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Photo> Photos { get; set; }
        
        public VincentDbContext(DbContextOptions<VincentDbContext> options)
            : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleFeature>().HasKey(vf =>
                new { vf.VehicleId, vf.FeatureId });
        }
    }
}