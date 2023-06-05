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
			return View();
		}

		public IActionResult About()
		{
			return View();
		}

		public IActionResult Driver()
		{
            return View(_context.DriversRegistrations.ToList());
        }
		public IActionResult DriverForm()
		{
            var id = HttpContext.Session.GetInt32("ID");

            if (id != null)
            {
                var e = HttpContext.Session.GetString("E");
                var n = HttpContext.Session.GetString("N");
                var p = HttpContext.Session.GetString("P");
                var m = HttpContext.Session.GetString("M");
                var t = HttpContext.Session.GetString("T");
                var ad = HttpContext.Session.GetString("A");
                ViewBag.id = id;
                ViewBag.e = e;
                ViewBag.n = n;
                ViewBag.p = p;
                ViewBag.m = m;
                ViewBag.t = t;
                ViewBag.ad = ad;
                return View();
            }
            else
            {
                return RedirectToAction("UserLogin", "Registrations");
            }
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DriverForm(DriversRegistration driversRegistration, IFormFile image)
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
                    driversRegistration.DriverImg = @"Image/" + fname;
                    _context.Add(driversRegistration);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.m = "Wrong Picture Format";
                }
            }

            return View(driversRegistration);
        }
        public IActionResult Company()
		{
   //         var id = HttpContext.Session.GetInt32("ID");
			//CompanyRegistration reg = _context.CompanyRegistrations.Where(c => c.UserId == id).FirstOrDefault();
			//var CompanyRegistration = _context.CompanyRegistrations.ToList();

   //         var ViewModel = new CompanyViewModel
   //         {
   //             CompanyRegistrations = CompanyRegistration,
   //             CompanyRegistration = reg
   //         };

			//return View(ViewModel);
			return View(_context.CompanyRegistrations.ToList());
        }
		public IActionResult CompanyForm()
		{
			var id = HttpContext.Session.GetInt32("ID");

			if (id!=null)
            {
                var e = HttpContext.Session.GetString("E");
                var n = HttpContext.Session.GetString("N");
                var p = HttpContext.Session.GetString("P");
                var m = HttpContext.Session.GetString("M");
                var t = HttpContext.Session.GetString("T");
                var ad = HttpContext.Session.GetString("A");
                ViewBag.id = id;
                ViewBag.e = e;
                ViewBag.n = n;
                ViewBag.p = p;
                ViewBag.m = m;
                ViewBag.t = t;
                ViewBag.ad = ad;
			    return View();
            }
            else
            {
                return RedirectToAction("UserLogin", "Registrations");
            }

		}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompanyForm(CompanyRegistration companyRegistration, IFormFile image)
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

        public async Task<IActionResult> CompanyDetail(int id, Visitor visitor)
        {
            var use = HttpContext.Session.GetInt32("ID");
            if(use!=null)
            {
          	    var register = await _context.Registrations
                    .FirstOrDefaultAsync(m => m.RegistrationId == use);
                //visitor.VisitorId = 1;
                visitor.VisitorName= register.Name;
                visitor.VisitorProfile= register.Profile;
                visitor.VisitorEmail= register.Email;
                visitor.VisitorCity= register.City;
                visitor.VisitorMobile= register.Mobile;
                visitor.VisitDate= DateTime.Now;
                visitor.Compid = id;
                _context.Add(visitor);
                await _context.SaveChangesAsync();
            }

            var companyRegistration = await _context.CompanyRegistrations
                .FirstOrDefaultAsync(m => m.CompanyId == id);
            var registration = await _context.Registrations.FirstOrDefaultAsync(m => m.RegistrationId == companyRegistration.UserId);
            var services = await _context.Services.FirstOrDefaultAsync(s => s.UserId == id);

			var ViewModel = new CompanyDetailVM
			{
				CompData = companyRegistration,
				RegData = registration,
				ServData = services,
			};

            return View(ViewModel);
        }

            //return View(companyRegistration);


















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

		public async Task<IActionResult> Profile()
        {

            var v = HttpContext.Session.GetInt32("ID");
            var registration = await _context.Registrations
                .FirstOrDefaultAsync(m => m.RegistrationId == v);
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


        private bool RegistrationExists(int registrationId)
        {
            throw new NotImplementedException();
        }

        public IActionResult DriverOrComp()
		{
            //var e = HttpContext.Session.GetString("E");
            //if (e != null)
            //{
			    return View();
            //}
            //else
            //{
            //    return RedirectToAction("Index", "Home");
            //}
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}