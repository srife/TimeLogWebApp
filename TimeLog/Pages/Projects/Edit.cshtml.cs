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

            PopulateActivityTypesDropDownList(_context, Project.DefaultActivityTypeId);
            PopulateClientDropDownList(_context, Project.DefaultClientId);
            PopulateLocationDropDownList(_context, Project.DefaultLocationId);

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
                s => s.IsDefault,
                s => s.DefaultActivityTypeId,
                s => s.DefaultClientId,
                s => s.DefaultLocationId,
                s => s.DefaultBillableRate))
            {
                if (projectToUpdate.IsDefault)
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

            PopulateActivityTypesDropDownList(_context, projectToUpdate.DefaultActivityTypeId);
            PopulateClientDropDownList(_context, projectToUpdate.DefaultClientId);
            PopulateLocationDropDownList(_context, projectToUpdate.DefaultLocationId);

            return Page();
        }
    }
}