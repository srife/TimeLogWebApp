using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using TimeLog.Models;

namespace TimeLog.Pages.Projects
{
    public class CreateModel : PageModel
    {
        private readonly TimeLogContext _context;

        public CreateModel(TimeLogContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["DefaultActivityTypeId"] = new SelectList(_context.ActivityTypes, "Id", "Name");
            ViewData["DefaultClientId"] = new SelectList(_context.Clients, "Id", "Name");
            ViewData["DefaultLocationId"] = new SelectList(_context.Locations, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Project Project { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Project.IsDefault)
            {
                var projects = _context.Projects.Where(x => x.IsDefault);
                foreach (var p in projects)
                {
                    p.IsDefault = false;
                }
            }

            _context.Projects.Add(Project);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}