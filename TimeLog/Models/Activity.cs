using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeLog.Models
{
    public class ActivityEntity
    {
        public int Id { get; set; }

        [NotMapped]
        [DataType(DataType.Date)]
        public DateTime? Date => this.StartTime.Date;

        [Required]
        [Display(Name = "Start")]
        [DataType(DataType.DateTime)]
        [System.ComponentModel.DefaultValue(typeof(DateTime), "")]
        public DateTime StartTime { get; set; }

        [Display(Name = "End")]
        [DataType(DataType.DateTime)]
        public DateTime? EndTime { get; set; }

        [Display(Name = "Activity Type")]
        public int ActivityTypeId { get; set; }

        [DataType(DataType.MultilineText)] public string Tasks { get; set; }

        [Display(Name = "Client")]
        public int ClientId { get; set; }

        public bool Billable { get; set; }

        public ActivityType ActivityType { get; set; }

        public Client Client { get; set; }
    }

    public class ActivityType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDefault { get; set; }
    }

    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDefault { get; set; }
    }
}