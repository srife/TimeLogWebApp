using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimeLog.Models;

namespace TimeLog.Pages.Contacts
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
            Contact = new Contact();
            PopulateClientDropDownList(_context);

            return Page();
        }

        [BindProperty]
        public Contact Contact { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var emptyContact = new Contact();
            if (await TryUpdateModelAsync(
                emptyContact,
                "Contact",
                s => s.DisplayName,
                s => s.FirstName,
                s => s.LastName,
                s => s.ClientId))
                _context.Contacts.Add(Contact);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}