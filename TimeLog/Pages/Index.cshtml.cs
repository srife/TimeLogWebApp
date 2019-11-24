using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
using System.Threading.Tasks;
using TimeLog.Models;
using TimeLog.ViewModels;

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

        public IList<Summary> Summary { get; set; }

        public decimal TotalWeeklyHours { get; set; }

        public string Project1Points { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ActivityEntity = await _context.ActivityEntity.FirstOrDefaultAsync(m => m.EndTime == null);

            ActivityEntityExists = (ActivityEntity != null);

            var startTime = DateTimeOffset.UtcNow;

            //remove hours, minutes, seconds, milliseconds
            startTime = startTime
                .AddMilliseconds(-startTime.Millisecond)
                .AddSeconds(-startTime.Second)
                .AddMinutes(-startTime.Minute)
                .AddHours(-startTime.Hour);

            //get current day of week
            var dayOfWeek = startTime.DayOfWeek;

            //if Sunday subtract 6 to get the previous Monday, otherwise subtract the day of week index plus one
            var daysToSubtract = (dayOfWeek) == DayOfWeek.Sunday ? -6 : -(int)dayOfWeek + 1;
            startTime = startTime.AddDays(daysToSubtract);
            var endTime = startTime.AddDays(6);

            //var p = new object[]
            //{
            //    new SqlParameter("@p0", SqlDbType.DateTimeOffset, 7) {Value = startTime},
            //    new SqlParameter("@p1", SqlDbType.DateTimeOffset, 7) {Value = endTime}
            //};

            Summary = new List<Summary>();

            //Summary = await _context
            //    .Summary
            //    .FromSql("exec sp_Summary @p0, p1", p)
            //    .ToListAsync();

            //for (int i = 0; i < Summary.Count; i++)
            //{
            //    var item = Summary[i];

            //    if (i == 0)
            //    {
            //        Project1Points += $"{100},{400 - item.SumTotalDurationHours * 50} ";
            //    }
            //    else
            //    {
            //        Project1Points += $"{i * 97 + 100},{400 - item.SumTotalDurationHours * 50} ";
            //    }
            //}

            //TotalWeeklyHours = Summary.Sum(i => i.SumTotalDurationHours);

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