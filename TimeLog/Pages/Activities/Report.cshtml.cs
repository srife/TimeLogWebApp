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

        public DateTime StartTime { get; set; }
        public decimal Rate { get; set; }
        public IList<Client> Clients { get; set; }
        public IList<Project> Projects { get; set; }
        public IList<ViewModels.Report> Report { get; set; }
        public IList<ViewModels.ReportDetailsByDay> ReportDetailsByDay { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var dayofweek = DateTime.UtcNow.DayOfWeek;
            StartTime = dayofweek == DayOfWeek.Sunday ? DateTime.UtcNow.AddDays(-7) : DateTime.UtcNow.AddDays(-(int)dayofweek + (int)DayOfWeek.Sunday);

            Rate = 37.5M;
            var p = new object[]
            {
                new SqlParameter("@p0", SqlDbType.DateTimeOffset, 7) {Value = StartTime.ToUniversalTime()},
                new SqlParameter("@p1", SqlDbType.DateTimeOffset, 7) {Value = StartTime.AddDays(8)},
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