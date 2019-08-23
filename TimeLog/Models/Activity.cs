using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeLog.Models
{
    public class ActivityEntity
    {
        public int Id { get; set; }

        [NotMapped]
        [DataType(DataType.Date)]
        public DateTime Date => StartTime.Date;

        [Required]
        [Display(Name = "Start")]
        [DataType(DataType.DateTime)]
        [System.ComponentModel.DefaultValue(typeof(DateTime), "")]
        public DateTime StartTime { get; set; }

        [Display(Name = "End")]
        [DataType(DataType.DateTime)]
        public DateTime? EndTime { get; set; }

        [Display(Name = "Project")]
        public int? ProjectId { get; set; }

        [Display(Name = "Location")]
        public int? LocationId { get; set; }

        [Display(Name = "Type")]
        public int ActivityTypeId { get; set; }

        [Display(Name = "Activity Details")]
        [DataType(DataType.MultilineText)]
        public string Tasks { get; set; }

        [Display(Name = "Client")]
        public int? ClientId { get; set; }

        public bool Billable { get; set; }

        [Display(Name = "Invoice Statement")]
        [DataType(DataType.MultilineText)]
        public string InvoiceStatement { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public ActivityType ActivityType { get; set; }

        public Client Client { get; set; }

        public Location Location { get; set; }

        public Project Project { get; set; }

        public virtual ActivityEntity Parent { get; set; }

        public virtual ICollection<ActivityEntity> Children { get; set; }
    }
}