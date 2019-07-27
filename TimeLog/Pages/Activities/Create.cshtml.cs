using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimeLog.Models;

namespace TimeLog.Pages.Activities
{
    public class CreateModel : PageModel
    {
        private readonly TimeLog.Models.TimeLogContext _context;

        public CreateModel(TimeLog.Models.TimeLogContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ActivityEntity = new ActivityEntity() { StartTime = DateTime.Now };
            //ActivityEntity.StartTime = DateTime.Now;
            ViewData["ActivityTypeId"] = new SelectList(_context.ActivityTypes.OrderByDescending(x => x.IsDefault).ThenBy(x => x.Name), "Id", "Name");
            ViewData["ClientId"] = new SelectList(_context.Clients.OrderByDescending(x => x.IsDefault).ThenBy(x => x.Name), "Id", "Name");
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