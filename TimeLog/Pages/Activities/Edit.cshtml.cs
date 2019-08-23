using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TimeLog.Models;

namespace TimeLog.Pages.Activities
{
    public class EditModel : ActivitiesBasePageModel
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

            var activityEntityToUpdate = await _context.ActivityEntity.FindAsync(id);

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
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopulateActivityTypesDropDownList(_context, ActivityEntity.ActivityTypeId);
            PopulateProjectsDropDownList(_context, ActivityEntity.ProjectId);
            return Page();

            //_context.Attach(ActivityEntity).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!ActivityEntityExists(ActivityEntity.Id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return RedirectToPage("./Index");
        }

        //private bool ActivityEntityExists(int id)
        //{
        //    return _context.ActivityEntity.Any(e => e.Id == id);
        //}
    }
}