using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TimeLog.Models;
using TimeLog.Extensions;

namespace TimeLog.Pages.Activities
{
    public class CreateModel : ActivitiesBasePageModel
    {
        private readonly TimeLogContext _context;

        public CreateModel(TimeLogContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ActivityEntity = new ActivityEntity() { StartTime = DateTime.Now };

            PopulateActivityTypesDropDownList(_context);
            PopulateClientDropDownList(_context);
            PopulateProjectsDropDownList(_context);
            PopulateLocationDropDownList(_context);

            return Page();
        }

        [BindProperty]
        public ActivityEntity ActivityEntity { get; set; }

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
                emptyActivityEntity.StartTime = DateTimeExtensions.RoundUp(emptyActivityEntity.StartTime, TimeSpan.FromMinutes(1));
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
    }
}