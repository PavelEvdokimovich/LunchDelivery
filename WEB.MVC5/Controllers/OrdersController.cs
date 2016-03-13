using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WEB.MVC5.Models;

namespace WEB.MVC5.Controllers
{
    public class OrdersController : Controller
    {
		private const decimal DELIVERY_SUM = 20000;

		private LunchContext db = new LunchContext();

		// GET: Orders/Order
		public ActionResult Order()
		{
			var lunches = db.Lunches.Include(l => l.Bread).Include(l => l.Garnish).Include(l => l.MeatDish).Include(l => l.Salad).Include(l => l.Soup).OrderBy(l => l.Date).Where(l => l.Date > DateTime.Now);

			List<OrderViewModel> list = new List<OrderViewModel>();

			foreach (Lunch item in lunches)
			{
				OrderViewModel orderViewModel = new OrderViewModel(item, false, false);

				list.Add(orderViewModel);
			}

			return View(list);
		}

		[HttpPost]
		public async Task<ActionResult> Order([Bind(/*Exclude ="Lunch", */Include = "Lunch, WithoutSoup, IsOrder")] List<OrderViewModel> list)
        {
			var currentUser = db.Users.Where(u => u.Email == HttpContext.User.Identity.Name).ToList().ElementAt(0);

			foreach (OrderViewModel ovm in list)
			{
				if (ovm.IsOrder == true)
				{
					Order order = new Order();
					order.UserId = currentUser.UserId;
					order.LunchId = ovm.Lunch.LunchId;
					if (ovm.WithoutSoup == true)
					{
						order.WithoutSoup = true;
					}

					db.Orders.Add(order);
					await db.SaveChangesAsync();
				}
				db.SaveChanges();

			}

			return RedirectToAction("MyOrders");
		}

		// GET: Orders/AllOrders
		public ActionResult AllOrders()
		{
			var orders = db.Orders;

			var lunches = db.Lunches;//.Where(l => l.Date >= DateTime.Now);

			var allOrders = lunches.GroupJoin(
				orders.Where(o => o.Delivered == false),
				l => l.LunchId,
				o => o.LunchId,
				(l, ol) => new AllOrdersViewModel
				{
					Date = l.Date,
					QuantityWithSoup = ol.Count(o => o.WithoutSoup == false),
					QuantityWithoutSoup = ol.Count(o => o.WithoutSoup == true),
					Pay = ol.Count(o => o.WithoutSoup == false) * l.PriceWithSoup + ol.Count(o => o.WithoutSoup == true) * l.PriceWithoutSoup,
				}).Where(ol => ol.Pay > 0);

			return View(allOrders.ToList().AsEnumerable());
		}

		// GET: Orders/OrderDetailsByDate
		public ActionResult OrderDetailsByDate(DateTime? date)
		{
			if (date == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var orders = db.Orders.Include(o => o.Lunch).Include(o => o.User)
							.Where(o => o.Lunch.Date == date).Where(o => o.Delivered == false);

			ViewBag.Date = date;
			ViewBag.QuantityWithSoup = orders.Where(o => o.WithoutSoup == false).Count();
			ViewBag.QuantityWithoutSoup = orders.Where(o => o.WithoutSoup == true).Count();
			ViewBag.Pay = orders.Select(o => o.Lunch.PriceWithSoup).Average() * orders.Where(o => o.WithoutSoup == false).Count() + orders.Select(o => o.Lunch.PriceWithoutSoup).Average() * orders.Where(o => o.WithoutSoup == true).Count();

			return View(orders.ToList());
		}

		// GET: Orders/MyOrders
		public ActionResult MyOrders()
		{
			var currentUser = db.Users.Where(u => u.Email == HttpContext.User.Identity.Name).ToList().ElementAt(0);

			var orders = db.Orders.Include(o => o.Lunch).Include(o => o.User)
							.Include(o => o.Lunch.Bread).Include(o => o.Lunch.Garnish).Include(o => o.Lunch.MeatDish).Include(o => o.Lunch.Salad).Include(o => o.Lunch.Soup)
							.Where(o => o.UserId == currentUser.UserId).Where(o => o.Delivered == false).OrderBy(o => o.Lunch.Date);

			return View(orders.ToList());
		}

		// GET: Orders/ConfirmOrderReceipt
		public async Task<ActionResult> ConfirmOrderReceipt(DateTime? date)
		{
			if (date == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var users = db.Users;

			var balances = db.Balances;

			var orders = db.Orders.Include(o => o.Lunch).Include(o => o.User).Where(o => o.Lunch.Date == date).Where(o => o.Delivered == false);

			decimal sumWithoutSoup, sumWithSoup;

			if (orders.Where(o => o.WithoutSoup == true).Count() > 0)
			{
				sumWithoutSoup = orders.Where(o => o.WithoutSoup == true).Sum(o => o.Lunch.PriceWithoutSoup);
			}
			else
			{
				sumWithoutSoup = 0;
			}

			if (orders.Where(o => o.WithoutSoup == false).Count() > 0)
			{
				sumWithSoup = orders.Where(o => o.WithoutSoup == false).Sum(o => o.Lunch.PriceWithSoup);
			}
			else
			{
				sumWithSoup = 0;
			}

			var sum = sumWithoutSoup + sumWithSoup;
			int countOrders = orders.Count();

			foreach (var order in orders)
			{
				order.Delivered = true;

				Balance balance = new Balance();
				User user = await db.Users.FindAsync(order.UserId);

				balance.Date = order.Lunch.Date;
				balance.UserId = order.UserId;

				if (sum > 170000)
				{
					if (order.WithoutSoup == false)
					{
						balance.Expence = order.Lunch.PriceWithSoup;
						user.Balance -= order.Lunch.PriceWithSoup;
					}
					else
					{
						balance.Expence = order.Lunch.PriceWithoutSoup;
						user.Balance -= order.Lunch.PriceWithoutSoup;
					}

					balance.Comment = "Списание за обед " + order.Lunch.Date.ToShortDateString();
				}
				else if (sum >= 100000)
				{
					if (order.WithoutSoup == false)
					{
						balance.Expence = Math.Round(order.Lunch.PriceWithSoup + DELIVERY_SUM / countOrders, 2);
						user.Balance -= balance.Expence;
					}
					else
					{
						balance.Expence = Math.Round(order.Lunch.PriceWithoutSoup + DELIVERY_SUM / countOrders, 2);
						user.Balance -= balance.Expence;
					}

					balance.Comment = "Списание за обед " + order.Lunch.Date.ToShortDateString() + " + оплата доставки";
				}

				db.Balances.Add(balance);
				db.Entry(user).State = EntityState.Modified;
			}

			await db.SaveChangesAsync();

			return View();
		}

		// GET: Orders/DeleteOrder
		public ActionResult DeleteOrder(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Order order	= db.Orders.Find(id);
			db.Orders.Remove(order);
			db.SaveChanges();

			return View();
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