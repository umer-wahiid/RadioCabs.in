using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RadioCabs.Models;

namespace RadioCabs.Controllers
{
    public class DriversRegistrationsController : Controller
    {
        private readonly RCDbContext _context;
        IWebHostEnvironment iw;

        public DriversRegistrationsController(RCDbContext context, IWebHostEnvironment i)
        {
            _context = context;
            iw = i;
        }

        // GET: DriversRegistrations
        public async Task<IActionResult> Index()
        {
              return _context.DriversRegistrations != null ? 
                          View(await _context.DriversRegistrations.ToListAsync()) :
                          Problem("Entity set 'RCDbContext.DriversRegistrations'  is null.");
        }

        // GET: DriversRegistrations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DriversRegistrations == null)
            {
                return NotFound();
            }

            var driversRegistration = await _context.DriversRegistrations
                .FirstOrDefaultAsync(m => m.DriverId == id);
            if (driversRegistration == null)
            {
                return NotFound();
            }

            return View(driversRegistration);
        }

        // GET: DriversRegistrations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DriversRegistrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DriverId,DriverName,ContactPerson,Address,City,Mobile,Telephone,Email,Experience,Description,PaymentType,DriverImg,UserId")] DriversRegistration driversRegistration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(driversRegistration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(driversRegistration);
        }

        // GET: DriversRegistrations/Edit/5
        public async Task<IActionResult> Edit()
        {
            var v = HttpContext.Session.GetInt32("ID");
            DriversRegistration Drireg = _context.DriversRegistrations.Where(c => c.UserId == v).FirstOrDefault();
            
            if (Drireg.DriverId == null || _context.DriversRegistrations == null)
            {
                return NotFound();
            }

            var driversRegistration = await _context.DriversRegistrations.FindAsync(Drireg.DriverId);
            if (driversRegistration == null)
            {
                return NotFound();
            }
            return View(driversRegistration);
        }

        // POST: DriversRegistrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DriversRegistration driversRegistration, IFormFile image)
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
                    driversRegistration.DriverImg = @"Image/" + fname;
                    ViewBag.m = "Updated";
                    _context.Update(driversRegistration);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Edit));
                }
                else
                {
                    ViewBag.m = "Wrong Picture Format";
                }

            }
            return View(driversRegistration);
        }

        // GET: DriversRegistrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DriversRegistrations == null)
            {
                return NotFound();
            }

            var driversRegistration = await _context.DriversRegistrations
                .FirstOrDefaultAsync(m => m.DriverId == id);
            if (driversRegistration == null)
            {
                return NotFound();
            }

            return View(driversRegistration);
        }

        // POST: DriversRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DriversRegistrations == null)
            {
                return Problem("Entity set 'RCDbContext.DriversRegistrations'  is null.");
            }
            var driversRegistration = await _context.DriversRegistrations.FindAsync(id);
            if (driversRegistration != null)
            {
                _context.DriversRegistrations.Remove(driversRegistration);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DriversRegistrationExists(int id)
        {
          return (_context.DriversRegistrations?.Any(e => e.DriverId == id)).GetValueOrDefault();
        }
    }
}
