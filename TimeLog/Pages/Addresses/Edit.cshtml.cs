using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeLog.Models;

namespace TimeLog.Pages.Addresses
{
    public class EditModel : PageModel
    {
        private readonly TimeLogContext _context;

        public EditModel(TimeLogContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Address Address { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Address = await _context.Addresses
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Address == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var addressToUpdate = await _context.Addresses.FindAsync(id);

            if (await TryUpdateModelAsync(addressToUpdate,
                "Address",
                s => s.AddressLine1,
                s => s.AddressLine2,
                s => s.AddressLine3,
                s => s.City,
                s => s.Region,
                s => s.PostalCode,
                s => s.County,
                s => s.Country))
            {
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}