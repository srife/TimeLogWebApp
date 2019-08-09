using Microsoft.EntityFrameworkCore;

namespace TimeLog.Models
{
    public class TimeLogContext : DbContext
    {
        public TimeLogContext(DbContextOptions<TimeLogContext> options)
            : base(options)
        {
        }

        public DbSet<ActivityEntity> ActivityEntity { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActivityEntity>()
                .HasMany(e => e.Children)
                .WithOne(m => m.Parent)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}