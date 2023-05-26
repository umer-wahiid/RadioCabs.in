using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using RadioCabs.Models;

namespace RadioCabs.Controllers
{
    public class RegistrationsController : Controller
    {
        private readonly RCDbContext _context;
        IWebHostEnvironment iw;

        public RegistrationsController(RCDbContext context, IWebHostEnvironment iw)
        {
            _context = context;
            iw = iw;
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
        public async Task<IActionResult> Create(Registration registration, IFormFile img)
        {
            if (img != null)
            {
                string ext = Path.GetExtension(img.FileName);
                if (ext == ".jpg" || ext == "gif")
                {
                    string d = Path.Combine(iw.WebRootPath, "Image");
                    var fname = Path.GetFileName(img.FileName);
                    string filepath = Path.Combine(d, fname);
                    using (var fs = new FileStream(filepath, FileMode.Create))
                    {
                        await img.CopyToAsync(fs);
                    }
                    registration.Profile = @"Image/" + fname;
                    _context.Add(registration);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
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

				HttpContext.Session.SetString("E", re.Email);
                HttpContext.Session.SetString("N", reg.Name);
                HttpContext.Session.SetString("P", reg.Profile);
                int registrationId = reg.RegistrationId; // Replace with your registration ID value
                HttpContext.Session.SetString("A", registrationId.ToString());
                return RedirectToAction("Index", "Home");
                //ViewBag.m = "Correct Credentials";
            }
            else
            {
                ViewBag.m = "Wrong Credentials";
            }
            return View();
        }


		public async Task<IActionResult> UserLogout()
		{
			await HttpContext.SignOutAsync();

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
