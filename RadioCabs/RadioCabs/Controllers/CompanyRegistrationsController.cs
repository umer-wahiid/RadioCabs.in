using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RadioCabs.Models;

namespace RadioCabs.Controllers
{
    public class CompanyRegistrationsController : Controller
    {
        private readonly RCDbContext _context;
        IWebHostEnvironment iw;

        public CompanyRegistrationsController(RCDbContext context, IWebHostEnvironment i)
        {
            _context = context;
            iw = i;
        }

        // GET: CompanyRegistrations
        public async Task<IActionResult> Index()
        {
              return _context.CompanyRegistrations != null ? 
                          View(await _context.CompanyRegistrations.ToListAsync()) :
                          Problem("Entity set 'RCDbContext.CompanyRegistrations'  is null.");
        }
        
        public async Task<IActionResult> visitor()
        {
            var role = HttpContext.Session.GetInt32("R");
            var user = HttpContext.Session.GetInt32("ID");
            var name = HttpContext.Session.GetString("N");
            CompanyRegistration Compreg = _context.CompanyRegistrations.Where(c => c.UserId == user).FirstOrDefault();
            DriversRegistration Drireg = _context.DriversRegistrations.Where(c => c.UserId == user).FirstOrDefault();
            if (role!=0)
            {
                return View(await _context.Visitors.Where(u => u.Compid != 0 || u.Driveid!=0).ToListAsync());
            }
            else if (role == 0 && Compreg != null)
            {
                return View(await _context.Visitors.Where(u=>u.Compid==Compreg.CompanyId && u.VisitorName != name).ToListAsync());
            }
            else if (role == 0 && Drireg !=null)
            {
                return View(await _context.Visitors.Where(u=>u.Driveid==Drireg.DriverId && u.VisitorName != name).ToListAsync());
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
            //return _context.Visitors != null ?
            //              View(await _context.Visitors.Where(u=>u.Compid==Compreg.CompanyId).ToListAsync()) :
            //              Problem("Entity set 'RCDbContext.Visitors'  is null.");
        }

        // GET: CompanyRegistrations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CompanyRegistrations == null)
            {
                return NotFound();
            }

            var companyRegistration = await _context.CompanyRegistrations
                .FirstOrDefaultAsync(m => m.CompanyId == id);
            if (companyRegistration == null)
            {
                return NotFound();
            }

            return View(companyRegistration);
        }

        // GET: CompanyRegistrations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CompanyRegistrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyId,CompanyName,ContactPerson,Designation,Address,Mobile,Telephone,FaxNumber,Email,MembershipType,PaymentType,City,Description,LogoImage,UserId")] CompanyRegistration companyRegistration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(companyRegistration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(companyRegistration);
        }

        // GET: CompanyRegistrations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var v = HttpContext.Session.GetInt32("ID");

            CompanyRegistration Compreg = _context.CompanyRegistrations.Where(c => c.UserId == v).FirstOrDefault();
            if (id!=null) {
                var companyRegistration = await _context.CompanyRegistrations.FindAsync(id);
                if (companyRegistration == null)
                {
                    return NotFound();
                }
                return View(companyRegistration);
            }
            else
            {
                var companyRegistration = await _context.CompanyRegistrations.FindAsync(Compreg.CompanyId);
                if (companyRegistration == null)
                {
                    return NotFound();
                }
                return View(companyRegistration);
            }
        }

        // POST: CompanyRegistrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CompanyRegistration companyRegistration, IFormFile image)
        {
            if (image != null)
            {
                string ext = Path.GetExtension(image.FileName);
                if (ext == ".jpg" || ext == ".png" || ext == ".jpeg")
                {
                    string d = Path.Combine(iw.WebRootPath, "Image");
                    var fname = Path.GetFileName(image.FileName);
                    string filepath = Path.Combine(d, fname);
                    using (var fs = new FileStream(filepath, FileMode.Create))
                    {
                        await image.CopyToAsync(fs);
                    }
                    companyRegistration.LogoImage = @"Image/" + fname;
                    _context.Update(companyRegistration);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Admin");
                    //return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.m = "Wrong Picture Format";
                }
            }
            return View(companyRegistration);
        }

        // GET: CompanyRegistrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CompanyRegistrations == null)
            {
                return NotFound();
            }

            var companyRegistration = await _context.CompanyRegistrations
                .FirstOrDefaultAsync(m => m.CompanyId == id);
            if (companyRegistration == null)
            {
                return NotFound();
            }

            return View(companyRegistration);
        }

        // POST: CompanyRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CompanyRegistrations == null)
            {
                return Problem("Entity set 'RCDbContext.CompanyRegistrations'  is null.");
            }
            var companyRegistration = await _context.CompanyRegistrations.FindAsync(id);
            var service = await _context.Services.FirstOrDefaultAsync(a=> a.CompanyId==id);
            var visit = await _context.Visitors.FirstOrDefaultAsync(a=> a.Compid==id);
            var vis = await _context.Visitors.FindAsync(visit.VisitorId);
            if (companyRegistration != null)
            {
                _context.CompanyRegistrations.Remove(companyRegistration);
                _context.Services.Remove(service);
                _context.Visitors.Remove(vis);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Admin");
        }

        private bool CompanyRegistrationExists(int id)
        {
          return (_context.CompanyRegistrations?.Any(e => e.CompanyId == id)).GetValueOrDefault();
        }
    }
}
