using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TimeLog.Models;

namespace TimeLog.Pages.Projects
{
    public class DetailsModel : PageModel
    {
        private readonly TimeLogContext _context;

        public DetailsModel(TimeLogContext context)
        {
            _context = context;
        }

        public Project Project { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project = await _context.Projects
                .Include(p => p.DefaultActivityType)
                .Include(p => p.DefaultClient)
                .Include(p => p.DefaultLocation).FirstOrDefaultAsync(m => m.Id == id);

            if (Project == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}