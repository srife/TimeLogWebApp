using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeLog.Models
{
    public class ActivityEntity
    {
        public int Id { get; set; }

        [NotMapped]
        [DataType(DataType.Date)]
        public DateTime? Date => StartTime.Date;

        [Required]
        [Display(Name = "Start")]
        [DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:H:mm}")]
        [System.ComponentModel.DefaultValue(typeof(DateTime), "")]
        public DateTime StartTime { get; set; }

        [Display(Name = "End")]
        //[DisplayFormat(DataFormatString = "{0:H:mm}")]
        [DataType(DataType.DateTime)]
        public DateTime? EndTime { get; set; }

        [Display(Name = "Type")]
        public int ActivityTypeId { get; set; }

        [Display(Name = "Activity Details")]
        [DataType(DataType.MultilineText)]
        public string Tasks { get; set; }

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

    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
    }
}