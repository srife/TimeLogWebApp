using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

//using System.Threading.Tasks;
//using TimeLog.Models;

namespace TimeLog.Pages
{
    public class IndexModel : PageModel
    {
        //private readonly TimeLogContext _context;

        //public IndexModel(TimeLogContext context)
        //{
        //    _context = context;
        //}

        public IList<ViewModels.Summary> Summary { get; set; }

        //public async Task OnGetAsync()
        //{
        //}
    }
}