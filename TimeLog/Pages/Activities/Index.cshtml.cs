//using System;
using System.Collections.Generic;

//using System.Linq;
using System.Threading.Tasks;

//using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimeLog.Models;

namespace TimeLog.Pages.Activities
{
    public class IndexModel : PageModel
    {
        private readonly TimeLogContext _context;

        public IndexModel(TimeLogContext context)
        {
            _context = context;
        }

        public IList<ActivityEntity> ActivityEntity { get; set; }

        public async Task OnGetAsync()
        {
            ActivityEntity = await _context.ActivityEntity
                .Include(a => a.ActivityType)
                .Include(a => a.Client).ToListAsync();
        }
    }
}