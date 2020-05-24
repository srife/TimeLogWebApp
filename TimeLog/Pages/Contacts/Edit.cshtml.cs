using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeLog.Models;

namespace TimeLog.Pages.Contacts
{
    public class EditModel : BasePageModelModel
    {
        private readonly TimeLog.Models.TimeLogContext _context;

        public EditModel(TimeLog.Models.TimeLogContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Contact Contact { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Contact = await _context.Contacts
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Contact == null)
            {
                return NotFound();
            }

            PopulateClientDropDownList(_context, Contact.ClientId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var contactToUpdate = await _context.Contacts.FindAsync(id);

            if (await TryUpdateModelAsync(contactToUpdate,
                "Contact",
                s => s.DisplayName,
                s => s.FirstName,
                s => s.LastName,
                s => s.ClientId))
            {
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            PopulateClientDropDownList(_context, Contact.ClientId);
            return Page();
        }
    }
}