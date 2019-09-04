using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeLog.Models;

namespace TimeLog.Pages
{
    public class IndexModel : BasePageModelModel
    {
        private readonly TimeLogContext _context;

        public IndexModel(TimeLogContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ActivityEntity ActivityEntity { get; set; }

        public bool ActivityEntityExists { get; set; }

        public IList<ViewModels.Summary> Summary { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ActivityEntity = await _context.ActivityEntity.FirstOrDefaultAsync(m => m.EndTime == null);

            ActivityEntityExists = (ActivityEntity != null);

            return Page();
        }

        public async Task<IActionResult> OnPostSaveAe(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var activityEntityToUpdate = await _context.ActivityEntity.FindAsync(ActivityEntity.Id);

            if (await TryUpdateModelAsync(activityEntityToUpdate,
                "ActivityEntity",
                s => s.EndTime,
                s => s.Tasks))
            {
                if (activityEntityToUpdate.EndTime != null)
                {
                    activityEntityToUpdate.EndTime =
                        Extensions.DateTimeExtensions.RoundUp(activityEntityToUpdate.EndTime.Value, TimeSpan.FromMinutes(1));
                }

                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            return Page();
        }

        public async Task OnGetClose(int id)
        {
            var activityEntityToUpdate = await _context.ActivityEntity.FindAsync(id);
            activityEntityToUpdate.EndTime =
                Extensions.DateTimeExtensions.RoundUp(DateTime.Now, TimeSpan.FromMinutes(1));
            await _context.SaveChangesAsync();
        }
    }
}