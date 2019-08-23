using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TimeLog.Models;

namespace TimeLog.Pages.Activities
{
    public class DetailsModel : PageModel
    {
        private readonly TimeLogContext _context;

        public DetailsModel(TimeLogContext context)
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
                .AsNoTracking()
                .Include(a => a.ActivityType)
                .Include(a => a.Client)
                .Include(a => a.Project)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ActivityEntity == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}