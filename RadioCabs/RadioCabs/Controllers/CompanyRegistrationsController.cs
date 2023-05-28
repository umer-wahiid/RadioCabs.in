﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RadioCabs.Models;

namespace RadioCabs.Controllers
{
    public class CompanyRegistrationsController : Controller
    {
        private readonly RCDbContext _context;

        public CompanyRegistrationsController(RCDbContext context)
        {
            _context = context;
        }

        // GET: CompanyRegistrations
        public async Task<IActionResult> Index()
        {
              return _context.CompanyRegistrations != null ? 
                          View(await _context.CompanyRegistrations.ToListAsync()) :
                          Problem("Entity set 'RCDbContext.CompanyRegistrations'  is null.");
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
        public async Task<IActionResult> Create([Bind("CompanyId,CompanyName,ContactPerson,Designation,Address,Mobile,Telephone,FaxNumber,Email,MembershipType,PaymentType,LogoImage,UserId")] CompanyRegistration companyRegistration)
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
            if (id == null || _context.CompanyRegistrations == null)
            {
                return NotFound();
            }

            var companyRegistration = await _context.CompanyRegistrations.FindAsync(id);
            if (companyRegistration == null)
            {
                return NotFound();
            }
            return View(companyRegistration);
        }

        // POST: CompanyRegistrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompanyId,CompanyName,ContactPerson,Designation,Address,Mobile,Telephone,FaxNumber,Email,MembershipType,PaymentType,LogoImage,UserId")] CompanyRegistration companyRegistration)
        {
            if (id != companyRegistration.CompanyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyRegistration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyRegistrationExists(companyRegistration.CompanyId))
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
            if (companyRegistration != null)
            {
                _context.CompanyRegistrations.Remove(companyRegistration);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyRegistrationExists(int id)
        {
          return (_context.CompanyRegistrations?.Any(e => e.CompanyId == id)).GetValueOrDefault();
        }
    }
}
