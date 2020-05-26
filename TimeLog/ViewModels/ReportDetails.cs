using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeLog.Models;

namespace TimeLog.ViewModels
{
    public class ReportDetail
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [Column(Order = 1)]
        public DateTime StartDay { get; set; }

        [Display(Name = "Start")]
        [Column(Order = 2)]
        public TimeSpan StartTime { get; set; }

        [Display(Name = "End")]
        [Column(Order = 3)]
        public TimeSpan EndTime { get; set; }

        [Display(Name = "Hours")]
        [Column(Order = 4)]
        public TimeSpan Duration { get; set; }

        [Display(Name = "Rounded Hours")]
        [Column(Order = 5)]
        public decimal RoundedHours { get; set; }

        [Display(Name = "Billable Amount")]
        [Column(Order = 6, TypeName = "money")]
        public decimal BillableAmount { get; set; }

        [Display(Name = "Client")]
        [Column(Order = 7)]
        public int? ClientId { get; set; }

        [Display(Name = "Project")]
        [Column(Order = 8)]
        public int? ProjectId { get; set; }

        [Display(Name = "Activity Type")]
        [Column(Order = 9)]
        public int ActivityTypeId { get; set; }

        [NotMapped]
        public string ProjectAndType
        {
            get
            {
                string result = string.Empty;
                if (!string.IsNullOrEmpty(Project.Name))
                {
                    result = $"{ Project.Name}<br />";
                }
                if (!string.IsNullOrEmpty(ActivityType.Name))
                {
                    result += $"{ActivityType.Name}";
                }
                return result;
            }
        }

        [Column(Order = 10)]
        public bool Billable { get; set; }

        [Display(Name = "Activity Details")]
        [DataType(DataType.MultilineText)]
        [Column(Order = 11)]
        public string Tasks { get; set; }

        [Display(Name = "Invoice Statement")]
        [DataType(DataType.MultilineText)]
        [Column(Order = 12)]
        public string InvoiceStatement { get; set; }

        public Client Client { get; set; }

        public Project Project { get; set; }

        public ActivityType ActivityType { get; set; }
    }
}