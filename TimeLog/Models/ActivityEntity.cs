using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeLog.Models
{
    public class ActivityEntity
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [Column(Order = 1)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [NotMapped]
        [DataType(DataType.Date)]
        public DateTime Date => StartTime.Date;

        [Required]
        [Display(Name = "Start")]
        [Column(Order = 2)]
        public DateTimeOffset StartTime { get; set; }

        [Display(Name = "End")]
        [Column(Order = 3)]
        public DateTimeOffset? EndTime { get; set; }

        [Display(Name = "Project")]
        [Column(Order = 4)]
        public int? ProjectId { get; set; }

        [Display(Name = "Location")]
        [Column(Order = 5)]
        public int? LocationId { get; set; }

        [Display(Name = "Type")]
        [Column(Order = 6)]
        public int ActivityTypeId { get; set; }

        [Display(Name = "Client")]
        [Column(Order = 7)]
        public int? ClientId { get; set; }

        [Display(Name = "Activity Details")]
        [DataType(DataType.MultilineText)]
        [Column(Order = 8)]
        public string Tasks { get; set; }

        [Column(Order = 9)]
        public bool Billable { get; set; }

        [Display(Name = "Billing Rate")]
        [Column(TypeName = "money")]
        public decimal BillableRate { get; set; }

        [Display(Name = "Invoice Statement")]
        [DataType(DataType.MultilineText)]
        [Column(Order = 10)]
        public string InvoiceStatement { get; set; }

        [Display(Name = "Billing Rate")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ActualBilledDuration { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public long? DurationSec { get; private set; }

        public ActivityType ActivityType { get; set; }

        public Client Client { get; set; }

        public Location Location { get; set; }

        public Project Project { get; set; }

        public virtual ActivityEntity Parent { get; set; }

        public virtual ICollection<ActivityEntity> Children { get; set; }
    }
}