using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TimeLog.Models;

namespace TimeLog.Pages.ActivityTypes
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
            return Page();
        }

        [BindProperty]
        public ActivityType ActivityType { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ActivityTypes.Add(ActivityType);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}