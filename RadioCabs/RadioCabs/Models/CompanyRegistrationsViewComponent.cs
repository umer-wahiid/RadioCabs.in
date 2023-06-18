using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace RadioCabs.Models
{
    public class CompanyRegistrationViewComponent : ViewComponent
    {
        public RCDbContext _context;

        public CompanyRegistrationViewComponent(RCDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var AdsCompany = await _context.AdvertiseRegistrations.FirstOrDefaultAsync(m => m.PaymentStatus == "Received");
            var companyRegistration = await _context.CompanyRegistrations.Where(m => m.CompanyId == AdsCompany.CompId).ToListAsync();

            //var ViewModel = new CompanyDetailVM
            //{
            //    CompData = companyRegistration,
            //};

            return View(companyRegistration.ToList());
        }
    }
}
