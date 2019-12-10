using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeLog.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        [Column(Order = 1)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Display(Name = "Invoice #")]
        public int InvoiceNumber { get; set; }

        [Display(Name = "Invoice Date")]
        public DateTimeOffset Date { get; set; }

        [Display(Name = "Client Purchase Order Number")]
        public string PurchaseOrderNumber { get; set; }

        public int ClientId { get; set; }
        public int ContactId { get; set; }
        public int AddressId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Total Hours")]
        public decimal TotalHours { get; set; }

        [Display(Name = "Total")]
        [Column(TypeName = "money")]
        public decimal TotalAmount { get; set; }

        public string PublicNote { get; set; }

        public string PrivateNote { get; set; }

        [Display(Name = "Date Submitted")]
        public DateTimeOffset SubmittedDate { get; set; }

        public int SubmitTypeId { get; set; }

        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}