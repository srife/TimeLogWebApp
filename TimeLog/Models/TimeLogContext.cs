using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

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

        [NotMapped]
        public virtual DbSet<ViewModels.Report> Report { get; set; }

        [NotMapped]
        public virtual DbSet<ViewModels.Summary> Summary { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActivityEntity>()
                .HasMany(e => e.Children)
                .WithOne(m => m.Parent)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ActivityEntity>()
                .Property(p => p.StartTime)
                .HasColumnType("datetimeoffset(7)");

            modelBuilder.Entity<ActivityEntity>()
                .Property(p => p.EndTime)
                .HasColumnType("datetimeoffset(7)");

            modelBuilder.Entity<ActivityEntity>()
                .Property(c => c.RowVersion).IsRowVersion();

            modelBuilder.Entity<ViewModels.Report>()
                .HasKey(c => new { c.ClientId, c.ProjectId });
        }
    }
}