using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

                if (File.Exists(@"Models/CustomSeedData/activities.json"))
                {
                    var conn = context.Database.GetDbConnection();
                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT ActivityTypes ON");

                    using (var file = File.OpenText(@"Models/CustomSeedData/activityTypes.json"))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        List<ActivityType> activityTypes = (List<ActivityType>)serializer.Deserialize(file, typeof(List<ActivityType>));
                        context.ActivityTypes.AddRange(activityTypes);
                        context.SaveChanges();
                    }

                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT ActivityTypes OFF");
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Clients ON");
                    using (var file = File.OpenText(@"Models/CustomSeedData/clients.json"))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        List<Client> clients = (List<Client>)serializer.Deserialize(file, typeof(List<Client>));
                        context.Clients.AddRange(clients);
                        context.SaveChanges();
                    }

                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Clients OFF");
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Locations ON");

                    using (var file = File.OpenText(@"Models/CustomSeedData/locations.json"))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        List<Location> locations = (List<Location>)serializer.Deserialize(file, typeof(List<Location>));
                        context.Locations.AddRange(locations);
                        context.SaveChanges();
                    }

                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Locations OFF");
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Projects ON");

                    using (var file = File.OpenText(@"Models/CustomSeedData/projects.json"))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        List<Project> projects = (List<Project>)serializer.Deserialize(file, typeof(List<Project>));
                        context.Projects.AddRange(projects);
                        context.SaveChanges();
                    }

                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Projects OFF");
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT ActivityEntity ON");

                    using (var file = File.OpenText(@"Models/CustomSeedData/activities.json"))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        List<ActivityEntity> activities = (List<ActivityEntity>)serializer.Deserialize(file, typeof(List<ActivityEntity>));
                        context.ActivityEntity.AddRange(activities);
                        context.SaveChanges();
                    }

                    conn.Close();
                }
                else
                {
                    List<ActivityType> activityTypes = new List<ActivityType>();
                    var atAdmin = new ActivityType { Name = "Administrative", IsDefault = false };
                    var atCoding = new ActivityType { Name = "Coding", IsDefault = false };
                    var atConfer = new ActivityType { Name = "Conference Call", IsDefault = false };
                    var atEmail = new ActivityType { Name = "Email", IsDefault = false };
                    var atErrand = new ActivityType { Name = "Errand", IsDefault = false };
                    activityTypes.Add(atAdmin);
                    activityTypes.Add(atCoding);
                    activityTypes.Add(atConfer);
                    activityTypes.Add(atEmail);
                    activityTypes.Add(atErrand);
                    activityTypes.Add(new ActivityType { Name = "Misc", IsDefault = false });
                    activityTypes.Add(new ActivityType { Name = "Phone Call", IsDefault = false });
                    activityTypes.Add(new ActivityType { Name = "Research", IsDefault = false });
                    activityTypes.Add(new ActivityType { Name = "Training", IsDefault = true });

                    context.ActivityTypes.AddRange(activityTypes);

                    List<Client> clients = new List<Client>();
                    var clientA = new Client { Name = "Client B", IsDefault = false };
                    var clientB = new Client { Name = "Client S", IsDefault = true };
                    var clientW = new Client { Name = "Client W", IsDefault = false };
                    var clientP = new Client { Name = "Personal", IsDefault = false };
                    clients.Add(clientA);
                    clients.Add(clientB);
                    clients.Add(clientW);
                    clients.Add(clientP);
                    context.Clients.AddRange(clients);

                    List<Location> locations = new List<Location>();
                    var locHome = new Location { Name = "Home", IsDefault = true };
                    var locOffice = new Location() { Name = "Office", IsDefault = false };
                    locations.Add(locHome);
                    locations.Add(locOffice);
                    context.Locations.AddRange(locations);

                    context.SaveChanges();

                    List<Project> projects = new List<Project>();
                    var projectA = new Project()
                    {
                        Name = "Project A",
                        IsDefault = true,
                        DefaultClientId = clientB.Id,
                        DefaultLocationId = locHome.Id
                    };
                    var projectB = new Project { Name = "Project B", IsDefault = false };
                    var projectC = new Project { Name = "Project C", IsDefault = false };
                    projects.Add(projectA);
                    projects.Add(projectB);
                    projects.Add(projectC);
                    context.Projects.AddRange(projects);
                    context.SaveChanges();

                    string activityTypesJson = JsonConvert.SerializeObject(activityTypes.ToArray());
                    File.WriteAllText(@"Models/CustomSeedData/activityTypes.json", activityTypesJson);

                    string clientsJson = JsonConvert.SerializeObject(clients.ToArray());
                    File.WriteAllText(@"Models/CustomSeedData/clients.json", clientsJson);

                    string locationsJson = JsonConvert.SerializeObject(locations.ToArray());
                    File.WriteAllText(@"Models/CustomSeedData/locations.json", locationsJson);

                    string projectsJson = JsonConvert.SerializeObject(projects.ToArray());
                    File.WriteAllText(@"Models/CustomSeedData/projects.json", projectsJson);

                    List<ActivityEntity> activityEntities = new List<ActivityEntity>
                {
                    new ActivityEntity
                    {
                        StartTime = Convert.ToDateTime("2019-07-29 10:00 AM"),
                        EndTime = Convert.ToDateTime("2019-07-29 10:40 AM"),
                        ClientId = clientB.Id,
                        ActivityTypeId = atEmail.Id,
                        ProjectId = projectA.Id,
                        Billable = false,
                        Location = locHome,
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
                        Location = locHome,
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
                        Location = locOffice,
                        Tasks = "Bills"
                    }
                };

                    context.ActivityEntity.AddRange(activityEntities);

                    context.SaveChanges();

                    string activities = JsonConvert.SerializeObject(activityEntities.ToArray());
                    File.WriteAllText(@"Models/CustomSeedData/activities.json", activities);
                }
            }
        }
    }
}