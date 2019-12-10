using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeLog.Models
{
    public class InvoiceItem
    {
        public int Id { get; set; }

        [Column(Order = 1)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public DateTimeOffset Date { get; set; }

        public DateTimeOffset DateEnd { get; set; }

        public TimeSpan TotalDuration { get; set; }

        public string Description { get; set; }

        [Display(Name = "Total")]
        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        public virtual ICollection<ActivityEntity> Activities { get; set; }

        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }
}