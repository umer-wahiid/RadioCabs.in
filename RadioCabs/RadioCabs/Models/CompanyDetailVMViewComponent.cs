using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RadioCabs.Models
{
    public class CompanyDetailVMViewComponent : ViewComponent
    {
        public RCDbContext _context;

        public CompanyDetailVMViewComponent(RCDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var ID = HttpContext.Session.GetInt32("ID");
            var companyRegistration = await _context.CompanyRegistrations.FirstOrDefaultAsync(m => m.UserId == ID);
            var driverRegistration = await _context.DriversRegistrations.FirstOrDefaultAsync(d => d.UserId == ID);
            var registration = await _context.Registrations.FirstOrDefaultAsync(r => r.RegistrationId == companyRegistration.UserId);

            var ViewModel = new CompanyDetailVM
            {
                CompData = companyRegistration,
                RegData = registration,
                DriverData = driverRegistration,
            };

            return View(ViewModel);
        }
    }
}
