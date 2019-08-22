using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeLog.Models;

namespace TimeLog.Pages.Projects
{
    public class IndexModel : PageModel
    {
        private readonly TimeLogContext _context;

        public IndexModel(TimeLogContext context)
        {
            _context = context;
        }

        public IList<Project> Project { get; set; }

        public async Task OnGetAsync()
        {
            Project = await _context.Projects
                .Include(p => p.DefaultActivityType)
                .Include(p => p.DefaultClient)
                .Include(p => p.DefaultLocation)
                .OrderByDescending(x => x.IsDefault)
                .ThenBy(x => x.Name)
                .ToListAsync();
        }
    }
}