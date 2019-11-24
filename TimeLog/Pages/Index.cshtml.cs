using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
using System.Threading.Tasks;
using TimeLog.Models;

//using TimeLog.ViewModels;

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

        public IList<ViewModels.ReportDetailsByDay> ReportDetailsByDay { get; set; }

        public decimal TotalWeeklyHours { get; set; }

        public string Project1Points { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ActivityEntity = await _context.ActivityEntity.FirstOrDefaultAsync(m => m.EndTime == null);

            ActivityEntityExists = (ActivityEntity != null);

            var startTime = DateTimeOffset.UtcNow;

            //remove hours, minutes, seconds, milliseconds
            startTime = startTime.AddMilliseconds(-startTime.Millisecond).AddSeconds(-startTime.Second).AddMinutes(-startTime.Minute).AddHours(-startTime.Hour);

            //get current day of week
            var dayOfWeek = startTime.DayOfWeek;

            //if Sunday subtract 6 to get the previous Monday, otherwise subtract the day of week index plus one
            var daysToSubtract = (dayOfWeek) == DayOfWeek.Sunday ? -6 : -(int)dayOfWeek + 1;
            startTime = startTime.AddDays(daysToSubtract);

            var Rate = 37.5M;
            var p = new object[]
            {
                new SqlParameter("@p0", SqlDbType.DateTimeOffset, 7) {Value = startTime},
                new SqlParameter("@p1", SqlDbType.DateTimeOffset, 7) {Value = startTime.AddDays(6)},
                new SqlParameter("@p2", SqlDbType.Money, 4) {Value = Rate},
                new SqlParameter("@p3", SqlDbType.Bit, 1) {Value = false}
            };

            ReportDetailsByDay = await _context.ReportDetailsByDay
                .FromSql("execute sp_ReportDetailsByDay @p0, @p1, @p2, @p3", p)
                .ToListAsync();

            //var startTime = DateTimeOffset.UtcNow;

            ////remove hours, minutes, seconds, milliseconds
            //startTime = startTime
            //    .AddMilliseconds(-startTime.Millisecond)
            //    .AddSeconds(-startTime.Second)
            //    .AddMinutes(-startTime.Minute)
            //    .AddHours(-startTime.Hour);

            ////get current day of week
            //var dayOfWeek = startTime.DayOfWeek;

            ////if Sunday subtract 6 to get the previous Monday, otherwise subtract the day of week index plus one
            //var daysToSubtract = (dayOfWeek) == DayOfWeek.Sunday ? -6 : -(int)dayOfWeek + 1;
            //startTime = startTime.AddDays(daysToSubtract);
            //var endTime = startTime.AddDays(6);

            //var p = new object[]
            //{
            //    new SqlParameter("@p0", SqlDbType.DateTimeOffset, 7) {Value = startTime},
            //    new SqlParameter("@p1", SqlDbType.DateTimeOffset, 7) {Value = endTime}
            //};

            //Summary = new List<Summary>();

            //Summary = await _context
            //    .Summary
            //    .FromSql("exec sp_Summary @p0, p1", p)
            //    .ToListAsync();

            for (int i = 0; i < ReportDetailsByDay.Count; i++)
            {
                var item = ReportDetailsByDay[i];

                if (i == 0)
                {
                    Project1Points += $"{100},{400 - item.Hrs * 50} ";
                }
                else
                {
                    Project1Points += $"{i * 97 + 100},{400 - item.Hrs * 50} ";
                }
            }

            //TotalWeeklyHours = Summary.Sum(i => i.SumTotalDurationHours);
            TotalWeeklyHours = ReportDetailsByDay.Sum(i => i.Hrs);

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