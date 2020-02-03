using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TimeLog.Models;
using System.Linq;

namespace TimeLog.Pages.Activities
{
    public class ReportModel : BasePageModelModel
    {
        private readonly TimeLogContext _context;

        public ReportModel(TimeLogContext context)
        {
            _context = context;
        }

        public Boolean LastWeek { get; set; } = false;
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public decimal Rate { get; set; }
        public IList<Client> Clients { get; set; }
        public IList<Project> Projects { get; set; }
        public IList<ViewModels.Report> Report { get; set; }
        public IList<ViewModels.ReportDetailsByDay> ReportDetailsByDay { get; set; }
        public IList<ActivityEntity> ActivityEntities { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            StartTime = DateTimeOffset.UtcNow;

            //remove hours, minutes, seconds, milliseconds
            StartTime = StartTime.AddMilliseconds(-StartTime.Millisecond).AddSeconds(-StartTime.Second).AddMinutes(-StartTime.Minute).AddHours(-StartTime.Hour);

            //get current day of week
            var dayOfWeek = StartTime.DayOfWeek;

            //if Sunday subtract 6 to get the previous Monday, otherwise subtract the day of week index plus one
            var daysToSubtract = (dayOfWeek) == DayOfWeek.Sunday ? -6 : -(int)dayOfWeek + 1;
            StartTime = StartTime.AddDays(daysToSubtract);
            EndTime = StartTime.AddDays(6);

            Rate = 37.5M;
            var p = new object[]
            {
                new SqlParameter("@p0", SqlDbType.DateTimeOffset, 7) {Value = StartTime},
                new SqlParameter("@p1", SqlDbType.DateTimeOffset, 7) {Value = EndTime},
                new SqlParameter("@p2", SqlDbType.Money, 4) {Value = Rate},
                new SqlParameter("@p3", SqlDbType.Bit, 1) {Value = true}
            };

            Clients = await _context.Clients.ToListAsync();
            Projects = await _context.Projects.ToListAsync();
            Report = await _context
                .Report
                .FromSql("execute sp_Report @p0, @p1, @p2, @p3", p)
                .TagWith("my statement")
                .ToListAsync();

            ReportDetailsByDay = await _context.ReportDetailsByDay
                .FromSql("execute sp_ReportDetailsByDay @p0, @p1, @p2, @p3", p)
                .TagWith("my second statement")
                .ToListAsync();

            //PopulateActivityTypesDropDownList(_context);
            //PopulateClientDropDownList(_context);
            //PopulateProjectsDropDownList(_context);
            //PopulateLocationDropDownList(_context);

            ActivityEntities = await PopulateActivityEntities(StartTime, EndTime);
            return Page();
        }

        public async Task<IActionResult> OnPostLastWeek()
        {
            LastWeek = true;

            StartTime = DateTimeOffset.UtcNow;

            //remove hours, minutes, seconds, milliseconds
            StartTime = StartTime.AddMilliseconds(-StartTime.Millisecond).AddSeconds(-StartTime.Second).AddMinutes(-StartTime.Minute).AddHours(-StartTime.Hour);

            if (LastWeek)
            {
                StartTime = StartTime.AddDays(-7);
            }

            //get current day of week
            var dayOfWeek = StartTime.DayOfWeek;

            //if Sunday subtract 6 to get the previous Monday, otherwise subtract the day of week index plus one
            var daysToSubtract = (dayOfWeek) == DayOfWeek.Sunday ? -6 : -(int)dayOfWeek + 1;
            StartTime = StartTime.AddDays(daysToSubtract);
            EndTime = StartTime.AddDays(6);

            Rate = 37.5M;
            var p = new object[]
            {
                new SqlParameter("@p0", SqlDbType.DateTimeOffset, 7) {Value = StartTime},
                new SqlParameter("@p1", SqlDbType.DateTimeOffset, 7) {Value = EndTime},
                new SqlParameter("@p2", SqlDbType.Money, 4) {Value = Rate},
                new SqlParameter("@p3", SqlDbType.Bit, 1) {Value = true}
            };

            Clients = await _context.Clients.ToListAsync();
            Projects = await _context.Projects.ToListAsync();
            Report = await _context
                .Report
                .FromSql("execute sp_Report @p0, @p1, @p2, @p3", p)
                .TagWith("my statement")
                .ToListAsync();

            ReportDetailsByDay = await _context.ReportDetailsByDay
                .FromSql("execute sp_ReportDetailsByDay @p0, @p1, @p2, @p3", p)
                .TagWith("my second statement")
                .ToListAsync();

            //PopulateActivityTypesDropDownList(_context);
            //PopulateClientDropDownList(_context);
            //PopulateProjectsDropDownList(_context);
            //PopulateLocationDropDownList(_context);
            ActivityEntities = await PopulateActivityEntities(StartTime, EndTime);
            return Page();
        }

        public async Task<IActionResult> OnPostTwoWeeksAgo()
        {
            LastWeek = true;

            StartTime = DateTimeOffset.UtcNow;

            //remove hours, minutes, seconds, milliseconds
            StartTime = StartTime.AddMilliseconds(-StartTime.Millisecond).AddSeconds(-StartTime.Second).AddMinutes(-StartTime.Minute).AddHours(-StartTime.Hour);

            if (LastWeek)
            {
                StartTime = StartTime.AddDays(-14);
            }

            //get current day of week
            var dayOfWeek = StartTime.DayOfWeek;

            //if Sunday subtract 6 to get the previous Monday, otherwise subtract the day of week index plus one
            var daysToSubtract = (dayOfWeek) == DayOfWeek.Sunday ? -6 : -(int)dayOfWeek + 1;
            StartTime = StartTime.AddDays(daysToSubtract);
            EndTime = StartTime.AddDays(6);

            Rate = 37.5M;
            var p = new object[]
            {
                new SqlParameter("@p0", SqlDbType.DateTimeOffset, 7) {Value = StartTime},
                new SqlParameter("@p1", SqlDbType.DateTimeOffset, 7) {Value = EndTime},
                new SqlParameter("@p2", SqlDbType.Money, 4) {Value = Rate},
                new SqlParameter("@p3", SqlDbType.Bit, 1) {Value = true}
            };

            Clients = await _context.Clients.ToListAsync();
            Projects = await _context.Projects.ToListAsync();
            Report = await _context
                .Report
                .FromSql("execute sp_Report @p0, @p1, @p2, @p3", p)
                .TagWith("my statement")
                .ToListAsync();

            ReportDetailsByDay = await _context.ReportDetailsByDay
                .FromSql("execute sp_ReportDetailsByDay @p0, @p1, @p2, @p3", p)
                .TagWith("my second statement")
                .ToListAsync();

            //PopulateActivityTypesDropDownList(_context);
            //PopulateClientDropDownList(_context);
            //PopulateProjectsDropDownList(_context);
            //PopulateLocationDropDownList(_context);
            ActivityEntities = await PopulateActivityEntities(StartTime, EndTime);
            return Page();
        }

        public TimeSpan MyConvert(long? seconds)
        {
            if (seconds is null)
            {
                return new TimeSpan(0, 0, 0, 0);
            }
            else
            {
                return TimeSpan.FromSeconds(Convert.ToInt64(seconds));
            }
        }

        public async Task<List<ActivityEntity>> PopulateActivityEntities(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            IQueryable<ActivityEntity> ae = _context.ActivityEntity
                .Include(a => a.ActivityType)
                .Include(a => a.Client)
                .Include(a => a.Project)
                .Where(a => a.StartTime > startDate && a.StartTime < endDate && a.Billable == true)
                .OrderBy(a => a.StartTime)
                .AsNoTracking();

            return await ae.ToListAsync();
        }
    }
}