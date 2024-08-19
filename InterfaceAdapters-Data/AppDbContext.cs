using InterfaceAdpaters_Models;
using Microsoft.EntityFrameworkCore;

namespace InterfaceAdapters_Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<BeerModel> Beers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BeerModel>().ToTable("Beer");
        }
    }
}
