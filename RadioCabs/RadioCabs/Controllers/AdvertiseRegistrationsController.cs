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
    public class AdvertiseRegistrationsController : Controller
    {
        private readonly RCDbContext _context;

        public AdvertiseRegistrationsController(RCDbContext context)
        {
            _context = context;
        }

        // GET: AdvertiseRegistrations
        public async Task<IActionResult> Index()
        {
              return _context.AdvertiseRegistrations != null ? 
                          View(await _context.AdvertiseRegistrations.ToListAsync()) :
                          Problem("Entity set 'RCDbContext.AdvertiseRegistrations'  is null.");
        }

        // GET: AdvertiseRegistrations/Details/5
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

        // GET: AdvertiseRegistrations/Create
        public IActionResult Create()
        {
            var id = HttpContext.Session.GetString("ID");
            var c = HttpContext.Session.GetString("CN");
            var dt = HttpContext.Session.GetString("C");
            var d = HttpContext.Session.GetString("D");
            var dg = HttpContext.Session.GetString("DG");
            var m = HttpContext.Session.GetString("M");
            var t = HttpContext.Session.GetString("T");
            var ad = HttpContext.Session.GetString("A");
            var f = HttpContext.Session.GetString("F");
            var e = HttpContext.Session.GetString("E");
            ViewBag.id = id;
            ViewBag.cnam = c;
            ViewBag.destination = dt;
            ViewBag.description = d;
            ViewBag.designation = dg;
            ViewBag.Mobile = m;
            ViewBag.Telephone = t;
            ViewBag.Address = ad;
            ViewBag.FaxNumber = f;
            ViewBag.Email = e;

            return View();
        }

        // POST: AdvertiseRegistrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdvertiseRegistration advertiseRegistration)
        {
            _context.Add(advertiseRegistration);
            await _context.SaveChangesAsync(); 

            return View(advertiseRegistration);
        }

        // GET: AdvertiseRegistrations/Edit/5
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

        // POST: AdvertiseRegistrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdvId,CompanyName,Destination,Designation,Address,Mobile,TelePhone,FaxNumber,Email,Description,PaymentType,UserId")] AdvertiseRegistration advertiseRegistration)
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

        // GET: AdvertiseRegistrations/Delete/5
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

        // POST: AdvertiseRegistrations/Delete/5
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
