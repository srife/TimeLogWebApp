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
    }
}