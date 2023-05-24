using Microsoft.AspNetCore.Mvc;
using RadioCabs.Models;
using System.Diagnostics;

namespace RadioCabs.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
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

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}