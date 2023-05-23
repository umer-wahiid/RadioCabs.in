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
    public class AdvertiseController : Controller
    {
        private readonly RCDbContext _context;

        public AdvertiseController(RCDbContext context)
        {
            _context = context;
        }

        // GET: Advertise
        public async Task<IActionResult> Index()
        {
              return _context.AdvertiseRegistrations != null ? 
                          View(await _context.AdvertiseRegistrations.ToListAsync()) :
                          Problem("Entity set 'RCDbContext.AdvertiseRegistrations'  is null.");
        }

        // GET: Advertise/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AdvertiseRegistrations == null)
            {
                return NotFound();
            }

            var advertiseRegistration = await _context.AdvertiseRegistrations
                .FirstOrDefaultAsync(m => m.AdvId == id);
            if (advertiseRegistration == null)
            {
                return NotFound();
            }

            return View(advertiseRegistration);
        }

        // GET: Advertise/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Advertise/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdvId,CompanyName,Destination,Designation,Address,Mobile,TelePhone,FaxNumber,Email,Description,PaymentType")] AdvertiseRegistration advertiseRegistration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(advertiseRegistration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(advertiseRegistration);
        }

        // GET: Advertise/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AdvertiseRegistrations == null)
            {
                return NotFound();
            }

            var advertiseRegistration = await _context.AdvertiseRegistrations.FindAsync(id);
            if (advertiseRegistration == null)
            {
                return NotFound();
            }
            return View(advertiseRegistration);
        }

        // POST: Advertise/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdvId,CompanyName,Destination,Designation,Address,Mobile,TelePhone,FaxNumber,Email,Description,PaymentType")] AdvertiseRegistration advertiseRegistration)
        {
            if (id != advertiseRegistration.AdvId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(advertiseRegistration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertiseRegistrationExists(advertiseRegistration.AdvId))
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
            return View(advertiseRegistration);
        }

        // GET: Advertise/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AdvertiseRegistrations == null)
            {
                return NotFound();
            }

            var advertiseRegistration = await _context.AdvertiseRegistrations
                .FirstOrDefaultAsync(m => m.AdvId == id);
            if (advertiseRegistration == null)
            {
                return NotFound();
            }

            return View(advertiseRegistration);
        }

        // POST: Advertise/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AdvertiseRegistrations == null)
            {
                return Problem("Entity set 'RCDbContext.AdvertiseRegistrations'  is null.");
            }
            var advertiseRegistration = await _context.AdvertiseRegistrations.FindAsync(id);
            if (advertiseRegistration != null)
            {
                _context.AdvertiseRegistrations.Remove(advertiseRegistration);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvertiseRegistrationExists(int id)
        {
          return (_context.AdvertiseRegistrations?.Any(e => e.AdvId == id)).GetValueOrDefault();
        }
    }
}
