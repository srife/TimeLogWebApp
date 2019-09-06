using System;

namespace TimeLog.ViewModels
{
    public class Summary
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Month { get; set; }

        public string DayOfWeek { get; set; }

        public decimal SumTotalDurationHours { get; set; }

        public int ProjectId { get; set; }

        public string ProjectName { get; set; }
    }
}