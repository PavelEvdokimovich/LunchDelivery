using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB.MVC5.Models;
using WEB.MVC5.Repository;

namespace WEB.MVC5.Controllers
{
	public class HomeController : Controller
	{
		private IGenericRepository<User> repository = null;

		public HomeController(IGenericRepository<User> repositoryUser)
		{
			this.repository = repositoryUser;
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "chudo-pechka.by";
			
			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Администраторы";

			return View(repository.SelectAll().ToList().Where(u => u.isAdmin == true));
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