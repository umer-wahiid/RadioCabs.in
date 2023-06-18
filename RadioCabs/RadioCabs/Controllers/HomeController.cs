using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RadioCabs.Models;
using System.Data.SqlTypes;
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
			return View(_context.FeedBacks.ToList());
		}

		public IActionResult Driver(string? city)
		{
            if (city == null)
            {
                return View(_context.DriversRegistrations.ToList());
            }
            else
            {
                return View(_context.DriversRegistrations.Where(a => a.City == city).ToList());
            }
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
            //int driv = _context.DriversRegistrations.Max(e => e.DriverId);
            int driv = _context.DriversRegistrations.Any()? _context.DriversRegistrations.Max(e => e.DriverId): 0;
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
                    HttpContext.Session.SetInt32("Dr", driv);
                    return RedirectToAction("Index", "Home");
				}
                else
                {
                    ViewBag.m = "Wrong Picture Format";
                }
            }

            return View(driversRegistration);
        }
        public IActionResult Company(string? city)
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
            if (city==null)
            {
			    return View(_context.CompanyRegistrations.ToList());
            }
            else
            {
			    return View(_context.CompanyRegistrations.Where(a=>a.City==city).ToList());
            }
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
            int comp = _context.CompanyRegistrations.Any() ? _context.CompanyRegistrations.Max(e => e.CompanyId) : 0;
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
                    HttpContext.Session.SetInt32("Co", comp);
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
            if (use != null)
            {
                var register = await _context.Registrations.FirstOrDefaultAsync(m => m.RegistrationId == use);
                visitor.VisitorName = register.Name;
                visitor.VisitorProfile = register.Profile;
                visitor.VisitorEmail = register.Email;
                visitor.VisitorCity = register.City;
                visitor.VisitorMobile = register.Mobile;
                visitor.VisitDate = DateTime.Now;
                visitor.Compid = id;
                _context.Add(visitor);
                await _context.SaveChangesAsync();
            }

            var companyRegistration = await _context.CompanyRegistrations
                .FirstOrDefaultAsync(m => m.CompanyId == id);
            var registration = await _context.Registrations.FirstOrDefaultAsync(m => m.RegistrationId == companyRegistration.UserId);

            var serv = await _context.Services.FirstOrDefaultAsync(s => s.CompanyId == id);
   //         if(serv.HService2==null && serv.HService3 == null)
   //         {
			//	var services = await _context.Services.Where(s => s.CompanyId == id).Select(s => new { s.HService1, s.DService1, s.HService2, s.DService2, s.HService3, s.DService3 }).FirstOrDefaultAsync();
			//	ViewBag.h1 = services.HService1;
			//	ViewBag.d2 = services.DService2;
			//}
   //         else if(serv.HService3 == null)
   //         {
   //             var services = await _context.Services.Where(s => s.CompanyId == id).Select(s => new { s.HService1, s.DService1, s.HService2, s.DService2 }).FirstOrDefaultAsync();
   //             ViewBag.h1 = services.HService1;
   //             ViewBag.h2 = services.HService2;
   //             ViewBag.d1 = services.DService1;
   //             ViewBag.d2 = services.DService2;
   //         }
   //         else
   //         {
                //var services = await _context.Services.Where(s => s.CompanyId == id).Select(s => new { s.HService1, s.DService1, s.HService2, s.DService2, s.HService3, s.DService3 }).FirstOrDefaultAsync();
                //ViewBag.h1 = services.HService1;
                //ViewBag.h2 = services.HService2;
                //ViewBag.d1 = services.DService1;
                //ViewBag.d2 = services.DService2;
                //ViewBag.h3 = services.HService3;
                //ViewBag.d3 = services.DService3;
            //}

            var ViewModel = new CompanyDetailVM
            {
                CompData = companyRegistration,
                RegData = registration,
                ServData = serv
            };
            return View(ViewModel);


            //try
            //{
            //    var services = await _context.Services.FirstOrDefaultAsync(s => s.CompanyId == id);
            //    var ViewModel = new CompanyDetailVM
            //    {
            //        CompData = companyRegistration,
            //        RegData =  registration,
            //        ServData = services
            //    };
            //    return View(ViewModel);
            //}
            //catch(SqlNullValueException e)
            //{
            //}
            //return View();
        }

        public async Task<IActionResult> DriverDetail(int id, Visitor visitor)
        {
            var use = HttpContext.Session.GetInt32("ID");
            if(use!=null)
            {
          	    var register = await _context.Registrations
                    .FirstOrDefaultAsync(m => m.RegistrationId == use);
                visitor.VisitorName= register.Name;
                visitor.VisitorProfile= register.Profile;
                visitor.VisitorEmail= register.Email;
                visitor.VisitorCity= register.City;
                visitor.VisitorMobile= register.Mobile;
                visitor.VisitDate= DateTime.Now;
                visitor.Driveid = id;
                _context.Add(visitor);
                await _context.SaveChangesAsync();
            }

            var drivreg = await _context.DriversRegistrations
                .FirstOrDefaultAsync(m => m.DriverId == id);

            return View(drivreg);
        }

		//public async Task<IActionResult> DriverDetail(int id)
		//{
		//    var drivreg = await _context.DriversRegistrations
		//        .FirstOrDefaultAsync(m => m.DriverId == id);

		//    return View(drivreg);
		//}

		//return View(companyRegistration);


















		public IActionResult Feedback()
		{
			return View();
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Feedback(FeedBack feedBack)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feedBack);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }
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