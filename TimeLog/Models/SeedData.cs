using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace TimeLog.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context =
                new TimeLogContext(serviceProvider.GetRequiredService<DbContextOptions<TimeLogContext>>()))
            {
                if (context.ActivityTypes.Any())
                {
                    return;
                }

                context.ActivityTypes.AddRange(
                    new ActivityType { Name = "Administrative", IsDefault = false },
                    new ActivityType { Name = "Coding", IsDefault = false },
                    new ActivityType { Name = "Conference Call", IsDefault = false },
                    new ActivityType { Name = "Errand", IsDefault = false },
                    new ActivityType { Name = "Misc", IsDefault = false },
                    new ActivityType { Name = "Phone Call", IsDefault = false },
                    new ActivityType { Name = "Training", IsDefault = true });

                context.Clients.AddRange(
                    new Client { Name = "Bruce", IsDefault = false },
                    new Client { Name = "Srife LLC", IsDefault = true },
                    new Client { Name = "Personal", IsDefault = false },
                    new Client { Name = "webFCE", IsDefault = false });

                context.SaveChanges();
            }
        }
    }
}