using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeLog.ViewModels
{
    public class Summary
    {
        public int Id { get; set; }

        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Display(Name = "Weekday")]
        public string DayOfWeek { get; set; }

        [Display(Name = "Total Hours")]
        [DisplayFormat(DataFormatString = "{0:#.####}")]
        [Column(TypeName = "decimal(8,1)")]
        public decimal SumTotalDurationHours { get; set; }
    }
}