using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RadioCabs.Models;
using System.Diagnostics;

namespace RadioCabs.Controllers
{
	public class HomeController : Controller
	{
		//private readonly ILogger<HomeController> _logger;

		//public HomeController(ILogger<HomeController> logger)
		//{
		//	_logger = logger;
		//}

        private readonly RCDbContext _context;
        IWebHostEnvironment iw;

        public HomeController(RCDbContext context, IWebHostEnvironment i)
        {
            _context = context;
            iw = i;
        }

        public IActionResult Index()
		{
			var id = HttpContext.Session.GetInt32("ID");
			var e = HttpContext.Session.GetString("E");
			var n = HttpContext.Session.GetString("N");
			var p = HttpContext.Session.GetString("P");
			var a = HttpContext.Session.GetString("A");
			ViewBag.id = id;
			ViewBag.a = e;
			ViewBag.b = n;
			ViewBag.c = p;
			ViewBag.d = a;
			return View();
		}

		public IActionResult About()
		{
			return View();
		}
		public IActionResult DriverView()
		{
			return View();
		}
		public IActionResult DriverForm()
		{
			return View();
		}
		public IActionResult CompanyView()
		{
			return View(_context.CompanyRegistrations.ToList());
		}
		public IActionResult CompanyForm()
		{
			return View();
		}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompanyForm(CompanyRegistration companyRegistration, IFormFile image)
        {
            var id = HttpContext.Session.GetInt32("ID");
            var e = HttpContext.Session.GetString("E");
            var n = HttpContext.Session.GetString("N");
            var p = HttpContext.Session.GetString("P");
            var m = HttpContext.Session.GetString("M");
            var t = HttpContext.Session.GetString("T");
            var a = HttpContext.Session.GetString("A");
            ViewBag.id = id;
            ViewBag.a = e;
            ViewBag.b = n;
            ViewBag.c = p;
            ViewBag.mo = m;
            ViewBag.t = t;
            ViewBag.ad = a;
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
                    companyRegistration.LogoImage= @"Image/" + fname;
                    _context.Add(companyRegistration);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.m = "Wrong Picture Format";
                }
            }
            return View(companyRegistration);
        }

        public IActionResult Feedback()
		{
			return View();
		}
		public IActionResult Contact()
		{
			return View();
		}
		
		public IActionResult Login()
		{
			return View();
		}

		public async Task<IActionResult> Profile(int? id)
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

        //      public IActionResult EditProfile()
        //{
        //	return View();
        //}

        public async Task<IActionResult> EditProfile(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> EditProfile(int id, Registration registration, IFormFile image)
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
                    _context.Update(registration);
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


        //public async Task<IActionResult> EditProfile(int id, Registration registration, IFormFile image)
        //{
        //    if (id != registration.RegistrationId)
        //    {

        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            if (image != null)
        //            {
        //                string ext = Path.GetExtension(image.FileName);
        //                if (ext == ".jpg" || ext == ".png")
        //                {
        //                    string d = Path.Combine(iw.WebRootPath, "Image");
        //                    var fname = Path.GetFileName(image.FileName);
        //                    string filepath = Path.Combine(d, fname);
        //                    using (var fs = new FileStream(filepath, FileMode.Create))
        //                    {
        //                        await image.CopyToAsync(fs);
        //                    }
        //                    registration.Profile = @"Image/" + fname;
        //                    _context.Update(registration);
        //                    await _context.SaveChangesAsync();
        //                    return RedirectToAction(nameof(Index));
        //                }
        //                else
        //                {
        //                    ViewBag.m = "Wrong Picture Format";
        //                }
        //            }
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!RegistrationExists(registration.RegistrationId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(registration);
        //}

        private bool RegistrationExists(int registrationId)
        {
            throw new NotImplementedException();
        }

        public IActionResult DriverOrComp()
		{
            var e = HttpContext.Session.GetString("E");
            if (e != null)
            {
			    return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}