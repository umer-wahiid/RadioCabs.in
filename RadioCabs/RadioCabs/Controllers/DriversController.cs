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
    public class DriversController : Controller
    {
        private readonly RCDbContext _context;

        public DriversController(RCDbContext context)
        {
            _context = context;
        }

        // GET: Drivers
        public async Task<IActionResult> Index()
        {
            var rCDbContext = _context.DriversRegistrations.Include(d => d.Registration);
            return View(await rCDbContext.ToListAsync());
        }

        // GET: Drivers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DriversRegistrations == null)
            {
                return NotFound();
            }

            var driversRegistration = await _context.DriversRegistrations
                .Include(d => d.Registration)
                .FirstOrDefaultAsync(m => m.DriverId == id);
            if (driversRegistration == null)
            {
                return NotFound();
            }

            return View(driversRegistration);
        }

        // GET: Drivers/Create
        public IActionResult Create()
        {
            ViewData["RegistrationId"] = new SelectList(_context.Registrations, "RegistrationId", "Address");
            return View();
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DriverId,Experience,Description,RegistrationId")] DriversRegistration driversRegistration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(driversRegistration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RegistrationId"] = new SelectList(_context.Registrations, "RegistrationId", "Address", driversRegistration.RegistrationId);
            return View(driversRegistration);
        }

        // GET: Drivers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DriversRegistrations == null)
            {
                return NotFound();
            }

            var driversRegistration = await _context.DriversRegistrations.FindAsync(id);
            if (driversRegistration == null)
            {
                return NotFound();
            }
            ViewData["RegistrationId"] = new SelectList(_context.Registrations, "RegistrationId", "Address", driversRegistration.RegistrationId);
            return View(driversRegistration);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DriverId,Experience,Description,RegistrationId")] DriversRegistration driversRegistration)
        {
            if (id != driversRegistration.DriverId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(driversRegistration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DriversRegistrationExists(driversRegistration.DriverId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RegistrationId"] = new SelectList(_context.Registrations, "RegistrationId", "Address", driversRegistration.RegistrationId);
            return View(driversRegistration);
        }

        // GET: Drivers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DriversRegistrations == null)
            {
                return NotFound();
            }

            var driversRegistration = await _context.DriversRegistrations
                .Include(d => d.Registration)
                .FirstOrDefaultAsync(m => m.DriverId == id);
            if (driversRegistration == null)
            {
                return NotFound();
            }

            return View(driversRegistration);
        }

        // POST: Drivers/Delete/5
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
