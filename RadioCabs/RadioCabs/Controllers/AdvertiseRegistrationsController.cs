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
            var id = HttpContext.Session.GetInt32("Co");
            var ad = _context.AdvertiseRegistrations.FirstOrDefault(a => a.CompId == id);
            if (ad==null) {
                var comp = _context.CompanyRegistrations.FirstOrDefault(a => a.CompanyId == id);
                ViewBag.id = comp.CompanyId;
                ViewBag.cnam = comp.CompanyName;
                ViewBag.PaymentStatus = "Pending";
                ViewBag.description = comp.Description;
                ViewBag.designation = comp.Designation;
                ViewBag.Mobile = comp.Mobile;
                ViewBag.Telephone = comp.Telephone;
                ViewBag.Address = comp.Address;
                ViewBag.FaxNumber = comp.FaxNumber;
                ViewBag.Email = comp.Email;

                return View();
            }
            else
            {
                return RedirectToAction("Index","Admin");
            }
        }

        // POST: AdvertiseRegistrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdvertiseRegistration advertiseRegistration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(advertiseRegistration);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index","Admin");
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
        public async Task<IActionResult> Edit(int id, [Bind("AdvId,CompanyName,PaymentStatus,Designation,Address,Mobile,TelePhone,FaxNumber,Email,Description,PaymentType,UserId")] AdvertiseRegistration advertiseRegistration)
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








        // GET: AdvertiseRegistrations/Edit/5
        public async Task<IActionResult> PaymentEdit(int? id)
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
        public async Task<IActionResult> PaymentEdit(int id,AdvertiseRegistration advertiseRegistration)
        {
            var existingAdv = await _context.AdvertiseRegistrations.FindAsync(id);
            if (existingAdv != null)
            {
                //var adv = await _context.AdvertiseRegistrations.FirstOrDefault(a=>a.AdvId==id);
                existingAdv.CompanyName = advertiseRegistration.CompanyName ?? existingAdv.CompanyName;
                existingAdv.Designation = advertiseRegistration.Designation ?? existingAdv.Designation;
                existingAdv.Address = advertiseRegistration.Address ?? existingAdv.Address;
                existingAdv.Mobile = advertiseRegistration.Mobile ?? existingAdv.Mobile;
                existingAdv.TelePhone = advertiseRegistration.TelePhone ?? existingAdv.TelePhone;
                existingAdv.FaxNumber = advertiseRegistration.FaxNumber ?? existingAdv.FaxNumber;
                existingAdv.Email = advertiseRegistration.Email ?? existingAdv.Email;
                existingAdv.Description = advertiseRegistration.Description ?? existingAdv.Description;
                existingAdv.PaymentType = advertiseRegistration.PaymentType ?? existingAdv.PaymentType;
                existingAdv.CompId = existingAdv.CompId;
                if (existingAdv.PaymentStatus=="Pending")
                {
                    existingAdv.PaymentStatus = "Received";
                }
                else
                {
                    existingAdv.PaymentStatus = "Pending";
                }

                _context.Update(existingAdv);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index","Admin");
        }















        private bool AdvertiseRegistrationExists(int id)
        {
          return (_context.AdvertiseRegistrations?.Any(e => e.AdvId == id)).GetValueOrDefault();
        }
    }
}
