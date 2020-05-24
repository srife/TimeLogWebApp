using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TimeLog.Extensions;
using TimeLog.Models;

namespace TimeLog.Pages.Activities
{
    public class CreateModel : BasePageModelModel
    {
        private readonly TimeLogContext _context;

        public CreateModel(TimeLogContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ActivityEntity ActivityEntity { get; set; }

        public IActionResult OnGet()
        {
            ActivityEntity = new ActivityEntity() { StartTime = DateTime.Now };

            PopulateActivityTypesDropDownList(_context);
            PopulateClientDropDownList(_context);
            PopulateProjectsDropDownList(_context);
            PopulateLocationDropDownList(_context);

            return Page();
        }

        public bool InterruptCurrentActivity { get; set; } = true;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var emptyActivityEntity = new ActivityEntity();

            if (await TryUpdateModelAsync(
                emptyActivityEntity,
                "ActivityEntity",
                s => s.StartTime,
                s => s.LocationId,
                s => s.ProjectId,
                s => s.ActivityTypeId,
                s => s.ClientId,
                s => s.Billable,
                s => s.Tasks))
            {
                var currentTime = DateTimeExtensions.RoundUp2(emptyActivityEntity.StartTime, TimeSpan.FromMinutes(1));

                if (InterruptCurrentActivity)
                {
                    var currentActivity = await _context.ActivityEntity.OrderByDescending(i => i.StartTime).FirstOrDefaultAsync(i => i.EndTime == null);
                    if (!(currentActivity is null))
                    {
                        currentActivity.EndTime = currentTime;
                    }
                }

                emptyActivityEntity.StartTime = currentTime;
                _context.ActivityEntity.Add(emptyActivityEntity);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            PopulateActivityTypesDropDownList(_context, emptyActivityEntity.ActivityTypeId);
            PopulateClientDropDownList(_context, emptyActivityEntity.ClientId);
            PopulateProjectsDropDownList(_context, emptyActivityEntity.ProjectId);
            PopulateLocationDropDownList(_context, emptyActivityEntity.LocationId);

            return Page();
        }

        public JsonResult OnGetProjectSelected(int id)
        {
            var project = _context.Projects.Find(id);
            return new JsonResult(project);
        }
    }
}