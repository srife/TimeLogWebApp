using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeLog.Models;

namespace TimeLog.ViewModels
{
    public class Report
    {
        [Display(Name = "Client")]
        //[Column(Order = 7)]
        public int ClientId { get; set; }

        [Display(Name = "Project")]
        //[Column(Order = 4)]
        public int ProjectId { get; set; }

        //[Column(Order = 9)]
        public bool Billable { get; set; }

        [Display(Name = "Hrs")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Duration { get; set; }

        [Display(Name = "Billable Amount")]
        [Column(TypeName = "money")]
        public decimal BillableAmount { get; set; }

        public Client Client { get; set; }

        public Project Project { get; set; }
    }
}