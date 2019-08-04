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

                var atAdmin = new ActivityType { Name = "Administrative", IsDefault = false };
                var atCoding = new ActivityType { Name = "Coding", IsDefault = false };
                var atConfer = new ActivityType { Name = "Conference Call", IsDefault = false };
                var atEmail = new ActivityType { Name = "Email", IsDefault = false };
                var atErrand = new ActivityType { Name = "Errand", IsDefault = false };

                context.ActivityTypes.AddRange(
                    atAdmin,
                    atCoding,
                    atConfer,
                    atEmail,
                    atErrand,
                    new ActivityType { Name = "Misc", IsDefault = false },
                    new ActivityType { Name = "Phone Call", IsDefault = false },
                    new ActivityType { Name = "Research", IsDefault = false },
                    new ActivityType { Name = "Training", IsDefault = true });

                var clientA = new Client { Name = "Client B", IsDefault = false };
                var clientB = new Client { Name = "Client S", IsDefault = true };
                var clientW = new Client { Name = "Client W", IsDefault = false };
                var clientP = new Client { Name = "Personal", IsDefault = false };
                context.Clients.AddRange(clientA, clientB, clientW, clientP);

                var locHome = new Location { Name = "Home", IsDefault = true };
                var locOffice = new Location() { Name = "Office", IsDefault = false };
                context.Locations.AddRange(locHome, locOffice);

                context.SaveChanges();

                var projectA = new Project()
                {
                    Name = "Project A",
                    IsDefault = true,
                    DefaultClientId = clientB.Id,
                    DefaultLocationId = locHome.Id
                };
                var projectB = new Project { Name = "Project B", IsDefault = false };
                var projectC = new Project { Name = "Project C", IsDefault = false };

                context.Projects.AddRange(projectA, projectB, projectC);

                context.SaveChanges();

                context.ActivityEntity.AddRange(
                    new ActivityEntity
                    {
                        StartTime = Convert.ToDateTime("2019-07-29 10:00 AM"),
                        EndTime = Convert.ToDateTime("2019-07-29 10:40 AM"),
                        ClientId = clientB.Id,
                        ActivityTypeId = atEmail.Id,
                        ProjectId = projectA.Id,
                        Billable = false,
                        Tasks = "email, news"
                    },

                    new ActivityEntity
                    {
                        StartTime = Convert.ToDateTime("2019-07-29 10:40 AM"),
                        EndTime = Convert.ToDateTime("2019-07-29 11:17 AM"),
                        ClientId = clientB.Id,
                        ActivityTypeId = atAdmin.Id,
                        ProjectId = projectB.Id,
                        Billable = false,
                        Tasks = "Bills"
                    },

                    new ActivityEntity
                    {
                        StartTime = Convert.ToDateTime("2019-07-29 11:17 AM"),
                        EndTime = Convert.ToDateTime("2019-07-29 12:39 AM"),
                        ClientId = clientB.Id,
                        ActivityTypeId = atAdmin.Id,
                        ProjectId = projectC.Id,
                        Billable = false,
                        Tasks = "Bills"
                    });

                context.SaveChanges();
            }
        }
    }
}