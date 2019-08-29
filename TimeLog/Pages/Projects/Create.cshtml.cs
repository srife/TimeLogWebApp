using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TimeLog.Models;

namespace TimeLog.Pages.Projects
{
    public class CreateModel : BasePageModelModel
    {
        private readonly TimeLogContext _context;

        public CreateModel(TimeLogContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateActivityTypesDropDownList(_context);
            PopulateClientDropDownList(_context);
            PopulateLocationDropDownList(_context);

            return Page();
        }

        [BindProperty] public Project Project { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var emptyProject = new Project();

            if (await TryUpdateModelAsync(
                emptyProject,
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

                    _context.Projects.Add(emptyProject);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
            }

            PopulateActivityTypesDropDownList(_context);
            PopulateClientDropDownList(_context);
            PopulateLocationDropDownList(_context);

            return Page();
        }
    }
}