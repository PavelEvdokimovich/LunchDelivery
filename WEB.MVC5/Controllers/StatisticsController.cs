using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB.MVC5.Models;

namespace WEB.MVC5.Controllers
{
	[Authorize(Roles = "admin")]
	public class StatisticsController : Controller
    {
		private LunchContext db = new LunchContext();

		// GET: Statistics
		public ActionResult Index()
        {
			var ordersWithoutSoup = db.Orders.Where(o => o.Delivered == true).Where(o => o.WithoutSoup == true).Count();
			var ordersWithSoup = db.Orders.Where(o => o.Delivered == true).Where(o => o.WithoutSoup == false).Count();

			ViewBag.OrdersWithoutSoup = ((double) ordersWithoutSoup / (ordersWithoutSoup + ordersWithSoup)).ToString("0.00", CultureInfo.InvariantCulture);
			ViewBag.OrdersWithSoup =((double) ordersWithSoup / (ordersWithoutSoup + ordersWithSoup)).ToString("0.00", CultureInfo.InvariantCulture);

			return View();

		}

		public JsonResult GetCountOrdersPerDay()
		{
			var orders = db.Orders;

			var lunches = db.Lunches;

			var getCountOrdersPerDay = lunches.GroupJoin(
				orders.Where(o => o.Delivered == true),
				l => l.LunchId,
				o => o.LunchId,
				(l, lo) => new CountOrdersPerDay
				{
					Date = l.Date.ToString(),
					Count = lo.Count()
				}).Where(lo => lo.Count > 0);

			return Json(getCountOrdersPerDay.OrderBy(o => o.Date).ToList(), JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetCountOrdersWithAndWithoutSoupPerDay()
		{
			var orders = db.Orders;

			var lunches = db.Lunches;

			var getCountOrdersWithAndWithoutSoupPerDay = lunches.GroupJoin(
				orders.Where(o => o.Delivered == true),
				l => l.LunchId,
				o => o.LunchId,
				(l, lo) => new CountOrdersWithAndWithoutSoupPerDay
				{
					Date = l.Date.ToString(),
					CountWithSoup = lo.Where(o => o.WithoutSoup == false).Count(),
					CountWithoutSoup = lo.Where(o => o.WithoutSoup == true).Count()
				}).Where(lo => (lo.CountWithoutSoup + lo.CountWithSoup) > 0);

			return Json(getCountOrdersWithAndWithoutSoupPerDay.OrderBy(o => o.Date).ToList(), JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetMoneyPerDay()
		{
			var orders = db.Orders;

			var lunches = db.Lunches;

			var getMoneyPerDay = lunches.GroupJoin(
				orders.Where(o => o.Delivered == true),
				l => l.LunchId,
				o => o.LunchId,
				(l, lo) => new MoneyPerDay
				{
					Date = l.Date.ToString(),
					Sum = lo.Where(o => o.WithoutSoup == false).Count() * l.PriceWithSoup + lo.Where(o => o.WithoutSoup == true).Count() * l.PriceWithoutSoup
				}).Where(lo => lo.Sum > 0);

			return Json(getMoneyPerDay.OrderBy(o => o.Date).ToList(), JsonRequestBehavior.AllowGet);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}