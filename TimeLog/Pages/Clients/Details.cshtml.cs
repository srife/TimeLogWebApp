﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TimeLog.Models;

namespace TimeLog.Pages.Clients
{
    public class DetailsModel : PageModel
    {
        private readonly TimeLogContext _context;

        public DetailsModel(TimeLogContext context)
        {
            _context = context;
        }

        public Client Client { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Client = await _context.Clients.FirstOrDefaultAsync(m => m.Id == id);

            if (Client == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}