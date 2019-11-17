using System.ComponentModel.DataAnnotations;
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

        public decimal Duration { get; set; }

        [Display(Name = "Billable Amount")]
        public decimal BillableAmount { get; set; }

        public Client Client { get; set; }

        public Project Project { get; set; }
    }
}