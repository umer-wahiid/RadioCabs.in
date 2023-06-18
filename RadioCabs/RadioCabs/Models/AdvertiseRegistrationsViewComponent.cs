using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RadioCabs.Models
{
	public class AdvertiseRegistrationsViewComponent : ViewComponent
    {
        public RCDbContext _context;

        public AdvertiseRegistrationsViewComponent(RCDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var AdsCompany = await _context.AdvertiseRegistrations.Where(m => m.PaymentStatus == "Received").ToListAsync();

            return View(AdsCompany.ToList());
        }
    }
}
