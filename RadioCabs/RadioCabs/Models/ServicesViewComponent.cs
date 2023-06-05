using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RadioCabs.Models
{
    public class ServicesViewComponent : ViewComponent
    {
        public RCDbContext _context;

        public ServicesViewComponent(RCDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            //var ID = HttpContext.Session.GetInt32("ID");
            //CompanyRegistration comp = _context.CompanyRegistrations.FirstOrDefault(c => c.UserId == ID);
            var ab = _context.Services.FirstOrDefault(s => s.UserId == id);
			return View(ab);
        }
    }
}
