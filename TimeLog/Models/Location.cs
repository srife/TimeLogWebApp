using System.ComponentModel.DataAnnotations;

namespace TimeLog.Models
{
    public class Location
    {
        public int Id { get; set; }

        [Display(Name = "Location Name")]
        public string Name { get; set; }

        [Display(Name = "Default")]
        public bool IsDefault { get; set; }
    }
}