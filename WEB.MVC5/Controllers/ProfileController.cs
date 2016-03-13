using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WEB.MVC5.Models;
using WEB.MVC5.Repository;

namespace WEB.MVC5.Controllers
{
    public class ProfileController : Controller
    {
		private IGenericRepository<User> repository = null;

		public ProfileController(IGenericRepository<User> repositoryUser)
		{
			this.repository = repositoryUser;
		}

		// GET: Profile
		[Authorize(Roles = "user")]
		public ActionResult Index()
        {
			var user = repository.SelectAll().Where(u => u.Email == HttpContext.User.Identity.Name).ToList().ElementAt(0);

			Profile userProfile = new Profile();

			userProfile.UserId = user.UserId;
			userProfile.Name = user.Name;
			userProfile.LastName = user.LastName;
			userProfile.Phone = user.Phone;
			userProfile.Email = user.Email;
			userProfile.Balance = user.Balance;
			userProfile.PhotoData = user.PhotoData;

			return View(userProfile);
		}

		// GET: Profile/Edit/5
		[Authorize(Roles = "user")]
		public async Task<ActionResult> Edit(int? id)
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

		// POST: Profile/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "user")]
		public async Task<ActionResult> Edit(User user, HttpPostedFileBase image = null)
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

				return RedirectToAction("Index");
			}

			return View(user);
		}

		public FileContentResult GetPhoto(int userId)
		{
			User user = repository.SelectByID(userId);

			if (user != null)
			{
				return File(user.PhotoData, user.PhotoMimeType);
			}
			else
			{
				return null;
			}
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