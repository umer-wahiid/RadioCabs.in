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
    public class RegistrationsController : Controller
    {
        private readonly RCDbContext _context;
        IWebHostEnvironment iw;

        public RegistrationsController(RCDbContext context, IWebHostEnvironment i)
        {
            _context = context;
            iw = i;
        }

        // GET: Registrations
        public async Task<IActionResult> Index()
        {
            return _context.Registrations != null ?
                        View(await _context.Registrations.ToListAsync()) :
                        Problem("Entity set 'RCDbContext.Registrations'  is null.");
        }

        // GET: Registrations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Registrations == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations
                .FirstOrDefaultAsync(m => m.RegistrationId == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // GET: Registrations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Registrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Registration registration, IFormFile image)
        {
            if (image != null)
            {
                string ext = Path.GetExtension(image.FileName);
                if (ext == ".jpg" || ext == ".png")
                {
                    string d = Path.Combine(iw.WebRootPath, "Image");
                    var fname = Path.GetFileName(image.FileName);
                    string filepath = Path.Combine(d, fname);
                    using (var fs = new FileStream(filepath, FileMode.Create))
                    {
                        await image.CopyToAsync(fs);
                    }
                    registration.Profile = @"Image/" + fname;
                    _context.Add(registration);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(UserLogin));
                }
                else
                {
                    ViewBag.m = "Wrong Picture Format";
                }
            }
            return View(registration);
        }



        // GET: Registrations/Create
        public IActionResult UserLogin()
        {
            return View();
        }


        [HttpPost]
        public ActionResult UserLogin(Registration re)
        {
            var x = from a in _context.Registrations where a.Email.Equals(re.Email) && a.Password.Equals(re.Password) select a;
            if (x.Any())
            {
                Registration reg = _context.Registrations.Where(c => c.Email == re.Email).FirstOrDefault();
                CompanyRegistration comp = _context.CompanyRegistrations.FirstOrDefault(cm => cm.UserId ==reg.RegistrationId);
                DriversRegistration driver = _context.DriversRegistrations.FirstOrDefault(dv => dv.UserId ==reg.RegistrationId);

                if (reg.RoleId == 1)
                {
                    HttpContext.Session.SetString("E", re.Email);
                    HttpContext.Session.SetInt32("ID", reg.RegistrationId);
                    HttpContext.Session.SetString("N", reg.Name);
                    HttpContext.Session.SetString("M", reg.Mobile);
                    HttpContext.Session.SetString("T", reg.TelePhone);
                    HttpContext.Session.SetString("A", reg.Address);
                    HttpContext.Session.SetString("P", reg.Profile);
                    HttpContext.Session.SetString("C", reg.City);
                    HttpContext.Session.SetInt32("Comp", comp.CompanyId);
                    HttpContext.Session.SetInt32("driver", driver.DriverId);
                    return RedirectToAction("Index", "Home");
                }
                else if (reg.RoleId == 0)
                {
                    HttpContext.Session.SetString("E", re.Email);
                    HttpContext.Session.SetInt32("ID", reg.RegistrationId);
                    HttpContext.Session.SetString("N", reg.Name);
                    HttpContext.Session.SetString("M", reg.Mobile);
                    HttpContext.Session.SetString("T", reg.TelePhone);
                    HttpContext.Session.SetString("A", reg.Address);
                    HttpContext.Session.SetString("P", reg.Profile);
                    HttpContext.Session.SetString("C", reg.City);
                    if (comp!= null)
                    {
                        HttpContext.Session.SetInt32("Co", comp.CompanyId);
                    }
                    if (driver!= null)
                    {
                        HttpContext.Session.SetInt32("Dr", driver.DriverId);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            else
            {
                ViewBag.m = "Wrong Credentials";
            }
            return View();
        }


        public async Task<IActionResult> UserLogout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }


        // GET: Registrations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Registrations == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }
            return View(registration);
        }

        // POST: Registrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegistrationId,Name,Email,Password,ConfirmPassword,Address,Mobile,TelePhone,City,Profile")] Registration registration)
        {
            if (id != registration.RegistrationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistrationExists(registration.RegistrationId))
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
            return View(registration);
        }

        // GET: Registrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Registrations == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations
                .FirstOrDefaultAsync(m => m.RegistrationId == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // POST: Registrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Registrations == null)
            {
                return Problem("Entity set 'RCDbContext.Registrations'  is null.");
            }
            var registration = await _context.Registrations.FindAsync(id);
            if (registration != null)
            {
                _context.Registrations.Remove(registration);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistrationExists(int id)
        {
            return (_context.Registrations?.Any(e => e.RegistrationId == id)).GetValueOrDefault();
        }
    }
}
