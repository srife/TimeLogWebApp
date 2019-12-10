using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeLog.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Column(Order = 1)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public string DisplayName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int ClientId { get; set; }
    }
}