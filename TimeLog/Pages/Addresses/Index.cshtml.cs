﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimeLog.Models;

namespace TimeLog.Pages.Addresses
{
    public class IndexModel : PageModel
    {
        private readonly TimeLog.Models.TimeLogContext _context;

        public IndexModel(TimeLog.Models.TimeLogContext context)
        {
            _context = context;
        }

        public IList<Address> Address { get;set; }

        public async Task OnGetAsync()
        {
            Address = await _context.Addresses.ToListAsync();
        }
    }
}
