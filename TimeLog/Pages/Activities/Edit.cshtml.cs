using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TimeLog.Models;

namespace TimeLog.Pages.Activities
{
    public class EditModel : PageModel
    {
        private readonly TimeLogContext _context;

        public EditModel(TimeLogContext context)
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
                .Include(a => a.ActivityType)
                .Include(a => a.Client).FirstOrDefaultAsync(m => m.Id == id);

            if (ActivityEntity == null)
            {
                return NotFound();
            }
            ViewData["ActivityTypeId"] = new SelectList(_context.ActivityTypes.OrderByDescending(x => x.IsDefault).ThenBy(x => x.Name), "Id", "Name");
            ViewData["ClientId"] = new SelectList(_context.Clients.OrderByDescending(x => x.IsDefault).ThenBy(x => x.Name), "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ActivityEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityEntityExists(ActivityEntity.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ActivityEntityExists(int id)
        {
            return _context.ActivityEntity.Any(e => e.Id == id);
        }
    }
}