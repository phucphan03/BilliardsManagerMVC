using DataAccessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CueStick> CueSticks { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<TableProduct> TableProducts { get; set; }
        public DbSet<TableSession> TableSessions { get; set; }
        public DbSet<TableSessionCue> TableSessionCues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Table>(entity =>
            {
                entity.Property(e => e.Status).HasConversion<string>();
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
