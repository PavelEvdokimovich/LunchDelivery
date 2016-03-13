using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WEB.MVC5.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WEB.MVC5.Repository;

namespace WEB.MVC5.Controllers
{
	[Authorize(Roles = "admin")]
	public class UsersController : Controller
    {
		private IGenericRepository<User> repository = null;

		public UsersController(IGenericRepository<User> repositoryUser)
		{
			this.repository = repositoryUser;
		}

		// GET: Users
		public ActionResult Index()
        {
			return View(repository.SelectAll().ToList().OrderBy(u => u.LastName));
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

			var user = repository.SelectByID(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
				if (image != null)
				{
					user.PhotoMimeType = image.ContentType;
					user.PhotoData = new byte[image.ContentLength];
					image.InputStream.Read(user.PhotoData, 0, image.ContentLength);
				}

				repository.Update(user);
				repository.Save();

				AddOrRemoveUserRole(user);

                return RedirectToAction("Index");
            }

            return View(user);
        }

		private void AddOrRemoveUserRole(User user)
		{
			ApplicationDbContext context = new ApplicationDbContext();

			var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

			ApplicationUser appUser = userManager.FindByEmail(user.Email);

			if (user.isAdmin)
			{
				userManager.AddToRole(appUser.Id, "admin");
			}
			else
			{
				userManager.RemoveFromRole(appUser.Id, "admin");
			}

			if (user.isUser)
			{
				userManager.AddToRole(appUser.Id, "user");
			}
			else
			{
				userManager.RemoveFromRole(appUser.Id, "user");
			}
		}

		// GET: Users/AddBalance/5
		public ActionResult AddBalance(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var user = repository.SelectByID(id);

			if (user == null)
			{
				return HttpNotFound();
			}

			AddBalanceToUser addBalanceToUser = new AddBalanceToUser();
			addBalanceToUser.UserId = user.UserId;
			addBalanceToUser.Name = user.Name;
			addBalanceToUser.LastName = user.LastName;
			addBalanceToUser.Email = user.Email;
			addBalanceToUser.AddBalance = 0;

			return View(addBalanceToUser);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddBalance([Bind(Include = "UserId,Name,LastName,Email,AddBalance")] AddBalanceToUser addBalanceToUser)
		{
			if (ModelState.IsValid)
			{
				User user = repository.SelectByID(addBalanceToUser.UserId);

				if (user == null)
				{
					return HttpNotFound();
				}

				user.Balance += addBalanceToUser.AddBalance;

				repository.Update(user);
				repository.Save();

				AddBalanceHistory(addBalanceToUser);

				return RedirectToAction("Index");
			}
			return View(addBalanceToUser);
		}

		private void AddBalanceHistory(AddBalanceToUser addBalanceToUser)
		{
			LunchContext db = new LunchContext();

			Balance balance = new Balance();

			balance.Date = DateTime.Now;
			balance.UserId = addBalanceToUser.UserId;

			if (addBalanceToUser.AddBalance >= 0)
			{
				balance.Income = addBalanceToUser.AddBalance;
				balance.Comment = "Пополнение счета администратором.";
			}
			else
			{
				balance.Expence = (-1) * addBalanceToUser.AddBalance;
				balance.Comment = "Выдача со счета администратором.";
			}

			db.Balances.Add(balance);
			db.SaveChanges();
			db.Dispose();
		}

		protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
