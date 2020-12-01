using Microsoft.EntityFrameworkCore;

namespace kinetic.Model
{
    public class KineticDb : DbContext
    {
        public KineticDb(DbContextOptions<KineticDb> options) : base(options)
        {
        }
        public DbSet<Jar> Jars { get; set; }
        public DbSet<JarTransaction> JarTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jar>().HasData(
                new Jar {Id = 1, Total = 0, Volume = 42}
            );
        }
    }
}