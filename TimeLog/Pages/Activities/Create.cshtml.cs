using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;
using TimeLog.Models;

namespace TimeLog.Pages.Activities
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
            ActivityEntity = new ActivityEntity() { StartTime = DateTime.Now };
            ViewData["ActivityTypeId"] =
                new SelectList(_context.ActivityTypes.OrderByDescending(x => x.IsDefault).ThenBy(x => x.Name), "Id",
                    "Name");

            ViewData["ClientId"] =
                new SelectList(_context.Clients.OrderByDescending(x => x.IsDefault).ThenBy(x => x.Name), "Id", "Name");

            ViewData["ProjectId"] =
                new SelectList(_context.Projects.OrderByDescending(x => x.IsDefault).ThenBy(x => x.Name), "Id", "Name");

            ViewData["LocationId"] =
                new SelectList(_context.Locations.OrderByDescending(x => x.IsDefault).ThenBy(x => x.Name), "Id",
                    "Name");

            return Page();
        }

        [BindProperty]
        public ActivityEntity ActivityEntity { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ActivityEntity.Add(ActivityEntity);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}