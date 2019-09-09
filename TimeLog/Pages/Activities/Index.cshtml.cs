using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeLog.Models;

namespace TimeLog.Pages.Activities
{
    public class IndexModel : PageModel
    {
        private readonly TimeLogContext _context;

        public IndexModel(TimeLogContext context)
        {
            _context = context;
        }

        public string DateSort { get; set; }

        public string CurrentFilter { get; set; }

        public string CurrentSort { get; set; }

        public IList<ActivityEntity> ActivityEntities { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            CurrentFilter = searchString;

            IQueryable<ActivityEntity> ae = _context.ActivityEntity
                .Include(a => a.ActivityType)
                .Include(a => a.Client)
                .Include(a => a.Project);

            if (!string.IsNullOrEmpty(searchString))
            {
                ae = ae.Where(s => s.Tasks.Contains(searchString) || s.InvoiceStatement.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Date":
                    ae = ae.OrderBy(s => s.StartTime);
                    break;

                case "date_desc":
                    ae = ae.OrderByDescending(s => s.StartTime);
                    break;

                default:
                    ae = ae.OrderByDescending(s => s.StartTime);
                    break;
            }

            ActivityEntities = await ae
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task OnGetFinish(int id)
        {
            var activityEntityToUpdate = await _context.ActivityEntity.FindAsync(id);
            activityEntityToUpdate.EndTime = Extensions.DateTimeExtensions.RoundUp(DateTime.Now, TimeSpan.FromMinutes(1));

            await _context.SaveChangesAsync();

            ActivityEntities = await _context.ActivityEntity
                .Include(a => a.ActivityType)
                .Include(a => a.Client)
                .Include(a => a.Project)
                .AsNoTracking()
                .OrderByDescending(x => x.StartTime)
                .ToListAsync();
        }

        public async Task<IActionResult> OnGetDuplicate(int id)
        {
            var activityEntityToDuplicate = await _context.ActivityEntity
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);

            var newActivityEntity = new ActivityEntity
            {
                ActivityTypeId = activityEntityToDuplicate.ActivityTypeId,
                Billable = activityEntityToDuplicate.Billable,
                ClientId = activityEntityToDuplicate.ClientId,
                LocationId = activityEntityToDuplicate.LocationId,
                ProjectId = activityEntityToDuplicate.ProjectId,
                StartTime = Extensions.DateTimeExtensions.RoundUp(DateTime.Now, TimeSpan.FromMinutes(1))
            };

            _context.ActivityEntity.Add(newActivityEntity);
            await _context.SaveChangesAsync();

            ActivityEntities = _context.ActivityEntity
                .Include(a => a.ActivityType)
                .Include(a => a.Client)
                .Include(a => a.Project)
                .OrderByDescending(x => x.StartTime)
                .ToList();

            return Page();
        }

        public ActionResult OnPostExport()
        {
            var activityTypes = _context.ActivityTypes.ToList();
            var clients = _context.Clients.ToList();
            var locations = _context.Locations.ToList();
            var projects = _context.Projects.ToList();
            var activityEntities = _context.ActivityEntity.ToList();

            string activityTypesJson = JsonConvert.SerializeObject(activityTypes.ToArray());
            System.IO.File.WriteAllText(@"Models/CustomSeedData/activityTypes.json", activityTypesJson);

            string clientsJson = JsonConvert.SerializeObject(clients.ToArray());
            System.IO.File.WriteAllText(@"Models/CustomSeedData/clients.json", clientsJson);

            string locationsJson = JsonConvert.SerializeObject(locations.ToArray());
            System.IO.File.WriteAllText(@"Models/CustomSeedData/locations.json", locationsJson);

            string projectsJson = JsonConvert.SerializeObject(projects.ToArray());
            System.IO.File.WriteAllText(@"Models/CustomSeedData/projects.json", projectsJson);

            string activities = JsonConvert.SerializeObject(activityEntities.ToArray());
            System.IO.File.WriteAllText(@"Models/CustomSeedData/activities.json", activities);

            ActivityEntities = _context.ActivityEntity
                .Include(a => a.ActivityType)
                .Include(a => a.Client)
                .Include(a => a.Project)
                .OrderByDescending(x => x.StartTime)
                .ToList();

            return Page();
        }
    }
}