using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RadioCabs.Models;

namespace RadioCabs.Controllers
{
	public class AdminController : Controller
	{
        private readonly RCDbContext _context;

        public AdminController(RCDbContext context)
        {
            _context = context;
        }
        // GET: AdminController
        public ActionResult Index()
		{
			var role = HttpContext.Session.GetInt32("R");

			DateTime currentDate = DateTime.Now.Date;
			DateTime startDate = new DateTime(currentDate.Year, currentDate.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            //DateTime currentDay = DateTime.Now.Date;

            var user = HttpContext.Session.GetInt32("ID");
			var name = HttpContext.Session.GetString("N");
			var Co = HttpContext.Session.GetInt32("Co");
			var Dr = HttpContext.Session.GetInt32("Dr");
			int count = _context.Visitors.Count(z => (z.Compid == Co || z.Driveid == Dr) && z.VisitorName != name);
            int day = _context.Visitors.Count(z => (z.Compid == Co || z.Driveid == Dr) && z.VisitorName != name && z.VisitDate.Date == currentDate);
            int month = _context.Visitors.Count(z => (z.Compid==Co || z.Driveid == Dr) && z.VisitorName!=name && z.VisitDate >= startDate && z.VisitDate <= endDate);
			int Allcount = _context.Visitors.Count(z => z.Compid != null  || z.Driveid != null);
            int Allday = _context.Visitors.Count(z => (z.Compid != null || z.Driveid != null) && z.VisitDate.Date == currentDate);
            int Allmonth = _context.Visitors.Count(z => (z.Compid != null || z.Driveid != null) && z.VisitDate >= startDate && z.VisitDate <= endDate);
            ViewBag.SD = startDate;
            ViewBag.ED = endDate;
			if (role == 0)
			{
				ViewBag.count = count;
				ViewBag.day = day;
				ViewBag.month = month;
			}
			else
			{
				ViewBag.count = Allcount;
				ViewBag.day = Allday;
				ViewBag.month = Allmonth;
			}

            return View();
		}

		public ActionResult ShowFeed()
		{
            return View(_context.FeedBacks.Where(a => a.Type != "nll").ToList());
		}
		public ActionResult ShowContact()
		{
			return View(_context.FeedBacks.Where(a=>a.Type=="nll").ToList());
		}
		public ActionResult ShowCompanies()
		{
			return View(_context.CompanyRegistrations.ToList());
		}
		public ActionResult ShowDrivers()
		{
			return View(_context.DriversRegistrations.ToList());
		}
		public ActionResult ShowUsers()
		{
			return View(_context.Registrations.ToList());
		}
		public ActionResult ShowAds()
		{
			return View(_context.AdvertiseRegistrations.ToList());
		}

		// GET: AdminController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: AdminController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: AdminController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: AdminController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: AdminController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: AdminController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: AdminController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
