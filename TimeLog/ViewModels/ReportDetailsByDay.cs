using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeLog.ViewModels
{
    public class ReportDetailsByDay
    {
        public long Id { get; set; }
        public DateTime StartDay { get; set; }
        public string DayOfWeek { get; set; }

        [Display(Name = "Hrs")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Hrs { get; set; }

        [Display(Name = "Billable Amount")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amt { get; set; }
    }
}