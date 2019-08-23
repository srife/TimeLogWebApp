using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TimeLog.Models;

namespace TimeLog.Pages.Activities
{
    public class DeleteModel : PageModel
    {
        private readonly TimeLogContext _context;

        public DeleteModel(TimeLogContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ActivityEntity = await _context.ActivityEntity.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (ActivityEntity != null)
            {
                _context.ActivityEntity.Remove(ActivityEntity);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}