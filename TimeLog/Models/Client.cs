using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeLog.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Column(Order = 1)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Display(Name = "Client Name")]
        public string Name { get; set; }

        [Display(Name = "Default")]
        public bool IsDefault { get; set; }

        [Display(Name = "Default Billing Rate")]
        [Column(TypeName = "money")]
        public decimal DefaultBillableRate { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}