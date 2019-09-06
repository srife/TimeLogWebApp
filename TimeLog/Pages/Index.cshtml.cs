using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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

        private IList<ViewModels.Summary> Summary { get; set; }

        public string Project1Points { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ActivityEntity = await _context.ActivityEntity.FirstOrDefaultAsync(m => m.EndTime == null);

            ActivityEntityExists = (ActivityEntity != null);

            Summary = await _context.Summary.FromSql("EXEC sp_Summary").ToListAsync();

            for (int i = 0; i < Summary.Count; i++)
            {
                var item = Summary[i];

                if (i == 0)
                {
                    Project1Points += $"{100},{400 - item.SumTotalDurationHours * 50} ";
                }
                else
                {
                    Project1Points += $"{i * 97 + 100},{400 - item.SumTotalDurationHours * 50} ";
                }
            }

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