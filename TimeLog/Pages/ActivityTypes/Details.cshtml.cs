using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TimeLog.Models;

namespace TimeLog.Pages.ActivityTypes
{
    public class DetailsModel : PageModel
    {
        private readonly TimeLogContext _context;

        public DetailsModel(TimeLogContext context)
        {
            _context = context;
        }

        public ActivityType ActivityType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ActivityType = await _context.ActivityTypes.FirstOrDefaultAsync(m => m.Id == id);

            if (ActivityType == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}