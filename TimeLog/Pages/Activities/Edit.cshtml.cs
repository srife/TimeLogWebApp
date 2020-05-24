using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TimeLog.Extensions;
using TimeLog.Models;

namespace TimeLog.Pages.Activities
{
    public class EditModel : BasePageModelModel
    {
        private readonly TimeLogContext _context;

        public EditModel(TimeLogContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ActivityEntity ActivityEntity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ActivityEntity = await _context.ActivityEntity
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ActivityEntity == null)
            {
                return NotFound();
            }

            PopulateActivityTypesDropDownList(_context, ActivityEntity.ActivityTypeId);
            PopulateClientDropDownList(_context, ActivityEntity.ClientId);
            PopulateProjectsDropDownList(_context, ActivityEntity.ProjectId);
            PopulateLocationDropDownList(_context, ActivityEntity.LocationId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var activityEntityToUpdate = await _context.ActivityEntity
                .FindAsync(id);

            if (await TryUpdateModelAsync(activityEntityToUpdate,
                "ActivityEntity",
                s => s.StartTime,
                s => s.EndTime,
                s => s.LocationId,
                s => s.ProjectId,
                s => s.ActivityTypeId,
                s => s.ClientId,
                s => s.Billable,
                s => s.Tasks,
                s => s.InvoiceStatement))
            {
                activityEntityToUpdate.StartTime = DateTimeExtensions.RoundUp2(activityEntityToUpdate.StartTime, TimeSpan.FromMinutes(1));

                if (activityEntityToUpdate.EndTime != null)
                {
                    activityEntityToUpdate.EndTime = activityEntityToUpdate.EndTime.Value;
                }

                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            PopulateActivityTypesDropDownList(_context, activityEntityToUpdate.ActivityTypeId);
            PopulateClientDropDownList(_context, activityEntityToUpdate.ClientId);
            PopulateProjectsDropDownList(_context, activityEntityToUpdate.ProjectId);
            PopulateLocationDropDownList(_context, activityEntityToUpdate.LocationId);

            return Page();
        }
    }
}