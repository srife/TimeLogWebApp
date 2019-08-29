using System.ComponentModel.DataAnnotations;

namespace TimeLog.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Display(Name = "Project Name")]
        public string Name { get; set; }

        [Display(Name = "Default")]
        public bool IsDefault { get; set; }

        [Display(Name = "Default Client")]
        public int? DefaultClientId { get; set; }

        [Display(Name = "Default Location")]
        public int? DefaultLocationId { get; set; }

        [Display(Name = "Default Activity Type")]
        public int? DefaultActivityTypeId { get; set; }

        [Display(Name = "Default Client")]
        public Client DefaultClient { get; set; }

        [Display(Name = "Default Location")]
        public Location DefaultLocation { get; set; }

        [Display(Name = "Default Activity Type")]
        public ActivityType DefaultActivityType { get; set; }
    }
}