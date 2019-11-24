using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TimeLog.Models;

namespace TimeLog.Pages.Activities
{
    public class ReportModel : BasePageModelModel
    {
        private readonly TimeLogContext _context;

        public ReportModel(TimeLogContext context)
        {
            _context = context;
        }

        public DateTimeOffset StartTime { get; set; }
        public decimal Rate { get; set; }
        public IList<Client> Clients { get; set; }
        public IList<Project> Projects { get; set; }
        public IList<ViewModels.Report> Report { get; set; }
        public IList<ViewModels.ReportDetailsByDay> ReportDetailsByDay { get; set; }

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

            Rate = 37.5M;
            var p = new object[]
            {
                new SqlParameter("@p0", SqlDbType.DateTimeOffset, 7) {Value = StartTime},
                new SqlParameter("@p1", SqlDbType.DateTimeOffset, 7) {Value = StartTime.AddDays(6)},
                new SqlParameter("@p2", SqlDbType.Money, 4) {Value = Rate},
                new SqlParameter("@p3", SqlDbType.Bit, 1) {Value = true}
            };

            Clients = await _context.Clients.ToListAsync();
            Projects = await _context.Projects.ToListAsync();
            Report = await _context
                .Report
                .FromSql("execute sp_Report @p0, @p1, @p2, @p3", p)
                .ToListAsync();

            ReportDetailsByDay = await _context.ReportDetailsByDay
                .FromSql("execute sp_ReportDetailsByDay @p0, @p1, @p2, @p3", p)
                .ToListAsync();

            PopulateActivityTypesDropDownList(_context);
            PopulateClientDropDownList(_context);
            PopulateProjectsDropDownList(_context);
            PopulateLocationDropDownList(_context);
            return Page();
        }
    }
}