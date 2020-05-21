using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using TimeLog.Models;
using System.Linq;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

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
        public SelectList TimeFramesSelectList { get; set; }
        public string SelectedTimeFrame { get; set; }
        public IList<Client> Clients { get; set; }
        public IList<Project> Projects { get; set; }
        public IList<ViewModels.Report> Report { get; set; }
        public IList<ViewModels.ReportDetailsByDay> ReportDetailsByDay { get; set; }
        public IList<ActivityEntity> ActivityEntities { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            PopulateTimeFrameDropDownList(_context, "");
            StartTime = DateTimeOffset.UtcNow.ToLocalTime();

            //remove hours, minutes, seconds, milliseconds
            StartTime = StartTime.AddMilliseconds(-StartTime.Millisecond).AddSeconds(-StartTime.Second).AddMinutes(-StartTime.Minute).AddHours(-StartTime.Hour);

            //get current day of week
            var dayOfWeek = StartTime.DayOfWeek;

            //if Sunday subtract 6 to get the previous Monday, otherwise subtract the day of week index plus one
            var daysToSubtract = (dayOfWeek) == DayOfWeek.Sunday ? -6 : -(int)dayOfWeek + 1;
            StartTime = StartTime.AddDays(daysToSubtract);
            EndTime = StartTime.AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);

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

            ActivityEntities = await PopulateActivityEntities(StartTime, EndTime);

            return Page();
        }

        public async Task<IActionResult> OnPostTimeFrame(string selectedTimeFrame)
        {
            SelectedTimeFrame = selectedTimeFrame;
            var splitSelectedTimeFrame = SelectedTimeFrame.Split("-");

            PopulateTimeFrameDropDownList(_context, splitSelectedTimeFrame[0]);

            StartTime = DateTimeOffset.Parse(splitSelectedTimeFrame[0]);
            EndTime = DateTimeOffset.Parse(splitSelectedTimeFrame[1]);

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

            ActivityEntities = await PopulateActivityEntities(StartTime, EndTime);
            return Page();
        }

        public TimeSpan ConvertDurationToTimeSpan(long? seconds)
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

        public decimal ConvertDurationToDecimal(double? seconds)
        {
            if (seconds is null)
            {
                return 0;
            }
            else
            {
                var firstvalue = (decimal)seconds;
                var secondvalue = (firstvalue / 900);
                var thirdvalue = Math.Ceiling(secondvalue);
                var adjustedSeconds = thirdvalue * 900;
                var result = Math.Round(adjustedSeconds / 60.0m / 60.0m, 2);
                return result;

                //return Math.Round((double)seconds / 60.0m / 60.0m, 2);
            }
        }

        private void PopulateTimeFrameDropDownList(TimeLogContext context, string selected)
        {
            //create a list of 1 week start/end times with a label such as this week, 1 week ago, 2 weeks ago, etc
            DateTimeOffset reportStartTime = DateTimeOffset.UtcNow.ToLocalTime();
            DateTimeOffset reportEndTime;
            reportStartTime = reportStartTime.AddMilliseconds(-reportStartTime.Millisecond)
                                             .AddSeconds(-reportStartTime.Second)
                                             .AddMinutes(-reportStartTime.Minute)
                                             .AddHours(-reportStartTime.Hour);
            //get current day of week
            var dayOfWeek = reportStartTime.DayOfWeek;

            //if Sunday subtract 6 to get the previous Monday, otherwise subtract the day of week index plus one
            var daysToSubtract = (dayOfWeek) == DayOfWeek.Sunday ? -6 : -(int)dayOfWeek + 1;
            reportStartTime = reportStartTime.AddDays(daysToSubtract);
            reportEndTime = reportStartTime.AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);

            List<MyDropDownItem> listOfReportDates = new List<MyDropDownItem>();
            var firstItem = new MyDropDownItem()
            {
                Name = $"{reportStartTime:yyyy/MM/dd} - {reportEndTime:yyyy/MM/dd}",
                Value = $"{reportStartTime:yyyy/MM/dd} - {reportEndTime:yyyy/MM/dd}"
            };
            if (firstItem.Name == selected)
            {
                firstItem.Selected = true;
            }
            listOfReportDates.Add(firstItem);

            for (int i = 0; i < 52; i++)
            {
                reportStartTime = reportStartTime.AddDays(-7);
                reportEndTime = reportEndTime.AddDays(-7);

                if (context.ActivityEntity.Any(x => x.StartTime > reportStartTime && x.EndTime < reportEndTime))
                {
                    var item = new MyDropDownItem()
                    {
                        Name = $"{reportStartTime:yyyy/MM/dd} - {reportEndTime:yyyy/MM/dd}",
                        Value = $"{reportStartTime:yyyy/MM/dd} - {reportEndTime:yyyy/MM/dd}"
                    };
                    if (item.Name == selected)
                    {
                        item.Selected = true;
                    }
                    listOfReportDates.Add(item);
                }
                else
                {
                    break;
                }
            }
            TimeFramesSelectList = new SelectList(listOfReportDates, "Name", "Value", "");

            //foreach (SelectListItem item in TimeFramesSelectList.Items)
            //{
            //    if (item.Text == selected)
            //    {
            //        item.Selected = true;
            //        break;
            //    }
            //}
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

    public class MyDropDownItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool Selected { get; set; }
    }
}