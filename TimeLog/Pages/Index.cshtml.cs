using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public decimal TotalWeeklyHours { get; set; }

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

            TotalWeeklyHours = Summary.Sum(i => i.SumTotalDurationHours);

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
                    activityEntityToUpdate.EndTime = Extensions.DateTimeExtensions.RoundUp2(activityEntityToUpdate.EndTime.Value, TimeSpan.FromMinutes(1));
                    //activityEntityToUpdate.EndTime = activityEntityToUpdate.EndTime.Value;
                }

                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnGetClose(int id)
        {
            var activityEntityToUpdate = await _context.ActivityEntity.FindAsync(id);
            activityEntityToUpdate.EndTime = Extensions.DateTimeExtensions.RoundUp(DateTime.Now, TimeSpan.FromMinutes(1));
            //activityEntityToUpdate.EndTime = DateTime.Now;
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}