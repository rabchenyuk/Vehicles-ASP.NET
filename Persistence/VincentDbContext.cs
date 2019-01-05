using Microsoft.EntityFrameworkCore;
using Vincent.Models;

namespace Vincent.Persistence
{
    public class VincentDbContext : DbContext
    {
        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; }
        
        public VincentDbContext(DbContextOptions<VincentDbContext> options)
            : base(options) {}
    }
}