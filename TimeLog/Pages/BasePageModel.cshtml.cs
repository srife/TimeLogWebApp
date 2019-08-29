using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TimeLog.Models;

namespace TimeLog.Pages
{
    public class BasePageModelModel : PageModel
    {
        public SelectList ProjectsSelectList { get; set; }
        public SelectList ActivityTypesSelectList { get; set; }
        public SelectList LocationSelectList { get; set; }
        public SelectList ClientSelectList { get; set; }

        protected void PopulateProjectsDropDownList(TimeLogContext context, object selectedProject = null)
        {
            ProjectsSelectList = new SelectList(context.Projects.AsNoTracking().OrderByDescending(x => x.IsDefault).ThenBy(x => x.Name), "Id", "Name", selectedProject);
        }

        protected void PopulateActivityTypesDropDownList(TimeLogContext context, object selectedActivityType = null)
        {
            ActivityTypesSelectList = new SelectList(context.ActivityTypes.AsNoTracking().OrderByDescending(x => x.IsDefault).ThenBy(x => x.Name), "Id", "Name", selectedActivityType);
        }

        protected void PopulateLocationDropDownList(TimeLogContext context, object selectedLocation = null)
        {
            LocationSelectList = new SelectList(context.Locations.AsNoTracking().OrderByDescending(x => x.IsDefault).ThenBy(x => x.Name), "Id", "Name", selectedLocation);
        }

        protected void PopulateClientDropDownList(TimeLogContext context, object selectedClient = null)
        {
            ClientSelectList = new SelectList(context.Clients.OrderByDescending(x => x.IsDefault).ThenBy(x => x.Name), "Id", "Name", selectedClient);
        }
    }
}