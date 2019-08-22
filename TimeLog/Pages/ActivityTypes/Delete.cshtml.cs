using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TimeLog.Models;

namespace TimeLog.Pages.ActivityTypes
{
    public class DeleteModel : PageModel
    {
        private readonly TimeLogContext _context;

        public DeleteModel(TimeLogContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ActivityType = await _context.ActivityTypes.FindAsync(id);

            if (ActivityType != null)
            {
                _context.ActivityTypes.Remove(ActivityType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}