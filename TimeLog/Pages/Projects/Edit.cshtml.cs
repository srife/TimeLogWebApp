using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TimeLog.Models;

namespace TimeLog.Pages.Projects
{
    public class EditModel : BasePageModelModel
    {
        private readonly TimeLogContext _context;

        public EditModel(TimeLogContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Project Project { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Project = await _context.Projects
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Project == null)
            {
                return NotFound();
            }

            PopulateActivityTypesDropDownList(_context);
            PopulateClientDropDownList(_context);
            PopulateLocationDropDownList(_context);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var projectToUpdate = await _context.Projects.FindAsync(id);

            if (await TryUpdateModelAsync(projectToUpdate,
                "Project",
                s => s.Name,
                s => s.IsDefault))
            {
                if (Project.IsDefault)
                {
                    var projects = _context.Projects.Where(x => x.IsDefault);
                    foreach (var p in projects)
                    {
                        p.IsDefault = false;
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopulateActivityTypesDropDownList(_context);
            PopulateClientDropDownList(_context);
            PopulateLocationDropDownList(_context);

            return Page();
        }
    }
}