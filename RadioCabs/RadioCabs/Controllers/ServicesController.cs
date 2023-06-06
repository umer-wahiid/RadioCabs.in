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
    public class ServicesController : Controller
    {
        private readonly RCDbContext _context;
        IWebHostEnvironment iw;
        public ServicesController(RCDbContext context, IWebHostEnvironment i)
        {
            _context = context;
            iw = i;
        }

        // GET: Services
        public async Task<IActionResult> Index()
        {
              return _context.Services != null ? 
                          View(await _context.Services.ToListAsync()) :
                          Problem("Entity set 'RCDbContext.Services'  is null.");
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var services = await _context.Services
                .FirstOrDefaultAsync(m => m.ServicesId == id);
            if (services == null)
            {
                return NotFound();
            }

            return View(services);
        }

        // GET: Services/Create
        public IActionResult Create()
        {
            var ID = HttpContext.Session.GetInt32("ID");
            CompanyRegistration comp = _context.CompanyRegistrations.FirstOrDefault(a => a.UserId == ID);
            Services service = _context.Services.FirstOrDefault(s => s.CompanyId == comp.CompanyId);
            if (service == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Edit", "Services");
            }
           

        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServicesId,HService1,DService1,HService2,DService2,HService3,DService3")] Services services)
        {
            if (ModelState.IsValid)
            {
                var user = HttpContext.Session.GetInt32("ID");
                CompanyRegistration comp =await _context.CompanyRegistrations.FirstOrDefaultAsync(a => a.UserId == user);
                services.CompanyId = comp.CompanyId;
                _context.Add(services);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(services);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit()
        {
            var ID = HttpContext.Session.GetInt32("ID");
            CompanyRegistration comp = _context.CompanyRegistrations.FirstOrDefault(a => a.UserId == ID);
            Services service = _context.Services.FirstOrDefault(s => s.CompanyId == comp.CompanyId);

            if (service.ServicesId == null || _context.Services == null)
            {
                return NotFound();
            }

            var services = await _context.Services.FindAsync(service.ServicesId);
            if (services == null)
            {
                return NotFound();
            }
            return View(services);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServicesId,HService1,DService1,HService2,DService2,HService3,DService3,UserId")] Services services)
        {
            if (id != services.ServicesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(services);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicesExists(services.ServicesId))
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
            return View(services);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var services = await _context.Services
                .FirstOrDefaultAsync(m => m.ServicesId == id);
            if (services == null)
            {
                return NotFound();
            }

            return View(services);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Services == null)
            {
                return Problem("Entity set 'RCDbContext.Services'  is null.");
            }
            var services = await _context.Services.FindAsync(id);
            if (services != null)
            {
                _context.Services.Remove(services);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicesExists(int id)
        {
          return (_context.Services?.Any(e => e.ServicesId == id)).GetValueOrDefault();
        }
    }
}
