using Microsoft.AspNetCore.Mvc;
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

        public HomeController(RCDbContext context)
        {
            _context = context;
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
			return View();
		}
		public IActionResult CompanyForm()
		{
			return View();
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
        //public async Task<IActionResult> Profile(int? id)
        //{
        //    if (id == null || _context.Registrations == null)
        //    {
        //        return NotFound();
        //    }

        //    var registration = await _context.Registrations
        //        .FirstOrDefaultAsync(m => m.RegistrationId == id);
        //    if (registration == null)
        //    {
        //        return NotFound();
        //    }

        //    return View();
        //}
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

        public IActionResult EditProfile()
		{
			return View();
		}
		
		public IActionResult DriverOrComp()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}