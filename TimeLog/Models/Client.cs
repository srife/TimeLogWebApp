using System.ComponentModel.DataAnnotations;

namespace TimeLog.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Display(Name = "Client Name")]
        public string Name { get; set; }

        [Display(Name = "Default")]
        public bool IsDefault { get; set; }
    }
}