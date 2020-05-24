using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimeLog.Models;

namespace TimeLog.Pages.Contacts
{
    public class IndexModel : PageModel
    {
        private readonly TimeLogContext _context;

        public IndexModel(TimeLogContext context)
        {
            _context = context;
        }

        public IList<Contact> Contact { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<Contact> c = _context.Contacts
                .Include(a => a.Client);

            Contact = await c.AsNoTracking().ToListAsync();
        }
    }
}