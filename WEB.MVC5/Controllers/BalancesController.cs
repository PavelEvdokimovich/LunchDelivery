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
    public class BalancesController : Controller
    {
		private LunchContext db = new LunchContext();

		// GET: Balances/MyBalance
		[Authorize(Roles = "user")]
		public ActionResult MyBalance()
		{
			var user = db.Users.Where(u => u.Email == HttpContext.User.Identity.Name).ToList().ElementAt(0);

			var balances = db.Balances.Where(b => b.UserId == user.UserId).OrderByDescending(b => b.Date);

			ViewBag.User = user;

			decimal debit = balances.Any() ? balances.Sum(b => b.Income) : Decimal.Parse("0");
			decimal credit = balances.Any() ? balances.Sum(b => b.Expence) : Decimal.Parse("0");
			ViewBag.Result = debit - credit;

			return View(balances.ToList());
		}

		// GET: Balances/UserBalanceHistory/1
		[Authorize(Roles = "admin")]
		public ActionResult UserBalanceHistory(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var balances = db.Balances.Where(b => b.UserId == id).OrderByDescending(b => b.Date);
			var user = db.Users.Where(u => u.UserId == id).ToList().ElementAt(0);

			ViewBag.User = user;

			decimal debit = balances.Any() ? balances.Sum(b => b.Income) : Decimal.Parse("0");
			decimal credit = balances.Any() ? balances.Sum(b => b.Expence) : Decimal.Parse("0");
			ViewBag.Result = debit - credit;

			return View(balances.ToList());
		}

		/*public ActionResult UsersBalance()
		{
			var users = db.Users;

			var balances = db.Balances;

			var usersBalance = users.Join(
				balances,
				u => u.UserId,
				b => b.UserId,
				(u, b) => new UserBalance {
					UserId = u.UserId,
					Name = u.LastName + " " + u.Name + " - " + u.Email,
					TotalBalance = u.Balance,
					Date = b.Date,
					Income = b.Income,
					Expence = b.Expence,
					Comment = b.Comment }
				).GroupBy(ub => ub.Name);

			return View(usersBalance.ToList());
		}*/

		// GET: Balances/UsersTotalBalance
		[Authorize(Roles = "admin")]
		public ActionResult UsersTotalBalance()
		{
			var users = db.Users;

			var balances = db.Balances;

			var usersTotalBalance = users.GroupJoin(
				balances,
				u => u.UserId,
				b => b.UserId,
				(u, bs) => new UserTotalBalance
				{
					UserId = u.UserId,
					Name = u.LastName + " " + u.Name + " - " + u.Email,
					UserBalance = u.Balance,
					TotalBalance = bs.Sum(b => b.Income ) - bs.Sum(b => b.Expence),
				});

			decimal sum = 0;

			foreach (var item in usersTotalBalance)
				{
					sum += item.TotalBalance;
				}

			ViewBag.Cashbox = sum.ToString("0,0.00", CultureInfo.InvariantCulture);

			return View(usersTotalBalance.ToList().AsEnumerable());
		}

		// GET: Balances/UsersList
		[Authorize(Roles = "user")]
		public ActionResult UsersList()
		{
			return View(db.Users.ToList());
		}

		// GET: Balances/GiveBalance/5
		[Authorize(Roles = "user")]
		public ActionResult GiveBalance(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			User user = db.Users.Find(id);

			if (user == null)
			{
				return HttpNotFound();
			}

			AddBalanceToUser giveBalance = new AddBalanceToUser();
			giveBalance.UserId = user.UserId;
			giveBalance.Name = user.Name;
			giveBalance.LastName = user.LastName;
			giveBalance.Email = user.Email;
			giveBalance.AddBalance = 0;

			var currentUser = db.Users.Where(u => u.Email == HttpContext.User.Identity.Name).ToList().ElementAt(0);

			ViewBag.CurrentUser = currentUser;

			return View(giveBalance);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "user")]
		public async Task<ActionResult> GiveBalance(AddBalanceToUser addBalanceToUser)
		{
			var currentUser = db.Users.Where(u => u.Email == HttpContext.User.Identity.Name).ToList().ElementAt(0);

			if (ModelState.IsValid && addBalanceToUser.AddBalance <= currentUser.Balance && addBalanceToUser.AddBalance > 0)
			{
				User user = await db.Users.FindAsync(addBalanceToUser.UserId);

				if (user == null)
				{
					return HttpNotFound();
				}

				Balance balance = new Balance();
				Balance currentBalance = new Balance();

				user.Balance += addBalanceToUser.AddBalance;
				currentUser.Balance -= addBalanceToUser.AddBalance;

				balance.Date = DateTime.Now;
				balance.UserId = user.UserId;
				balance.Income = addBalanceToUser.AddBalance;
				balance.Comment = "С вами поделился балансом " + currentUser.Name + " " + currentUser.LastName + " (" + currentUser.Email + ")";
				db.Balances.Add(balance);

				currentBalance.Date = DateTime.Now;
				currentBalance.UserId = currentUser.UserId;
				currentBalance.Expence = addBalanceToUser.AddBalance;
				currentBalance.Comment = "Вы поделились балансом с " + user.Name + " " + user.LastName + " (" + user.Email + ")";
				db.Balances.Add(currentBalance);

				db.Entry(user).State = EntityState.Modified;

				await db.SaveChangesAsync();

				return RedirectToAction("UsersList");
			}

			return RedirectToAction("GiveBalance", new { id = addBalanceToUser.UserId });
		}

	}
}