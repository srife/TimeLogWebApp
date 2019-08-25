using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

        public IList<ActivityEntity> ActivityEntity { get; set; }

        public async Task OnGetAsync()
        {
            ActivityEntity = await _context.ActivityEntity
                .Include(a => a.ActivityType)
                .Include(a => a.Client)
                .Include(a => a.Project)
                .OrderByDescending(x => x.StartTime)
                .ToListAsync();
        }

        public async Task OnGetClose(int id)
        {
            var activityEntityToUpdate = await _context.ActivityEntity.FindAsync(id);
            activityEntityToUpdate.EndTime = DateTime.Now;
            await _context.SaveChangesAsync();

            ActivityEntity = await _context.ActivityEntity
                .Include(a => a.ActivityType)
                .Include(a => a.Client)
                .Include(a => a.Project)
                .OrderByDescending(x => x.StartTime)
                .ToListAsync();
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

            ActivityEntity = _context.ActivityEntity
                .Include(a => a.ActivityType)
                .Include(a => a.Client).ToList();

            return Page();
        }
    }
}