﻿using Microsoft.AspNetCore.Mvc;
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
			var e = HttpContext.Session.GetString("E");
			var n = HttpContext.Session.GetString("N");
			var p = HttpContext.Session.GetString("P");
			ViewBag.a = e;
			ViewBag.b = n;
			ViewBag.c = p;
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