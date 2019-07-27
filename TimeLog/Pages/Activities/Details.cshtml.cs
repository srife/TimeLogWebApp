using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimeLog.Models;

namespace TimeLog.Pages.Activities
{
    public class DetailsModel : PageModel
    {
        private readonly TimeLog.Models.TimeLogContext _context;

        public DetailsModel(TimeLog.Models.TimeLogContext context)
        {
            _context = context;
        }

        public ActivityEntity ActivityEntity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ActivityEntity = await _context.ActivityEntity
                .Include(a => a.ActivityType)
                .Include(a => a.Client).FirstOrDefaultAsync(m => m.Id == id);

            if (ActivityEntity == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
