using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeLog.Models;

namespace TimeLog.Pages.Invoices
{
    public class IndexModel : PageModel
    {
        private readonly TimeLogContext _context;

        public IndexModel(TimeLogContext context)
        {
            _context = context;
        }

        public IList<Invoice> Invoice { get; set; }

        public async Task OnGetAsync()
        {
            Invoice = await _context.Invoices.ToListAsync();
        }
    }
}