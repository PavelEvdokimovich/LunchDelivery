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
using WEB.MVC5.Repository;

namespace WEB.MVC5.Controllers
{
	[Authorize(Roles = "admin")]
	public class SoupsController : Controller
    {
		private IGenericRepository<Soup> repository = null;

		public SoupsController(IGenericRepository<Soup> repositorySoup)
		{
			this.repository = repositorySoup;
		}

		// GET: Soups
		public ActionResult Index()
        {
			var soups = repository.SelectAll().ToList().OrderBy(s => s.Name);

			return View(soups);
        }

        // GET: Soups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Soups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SoupId,Name,Weight")] Soup soup)
        {
            if (ModelState.IsValid)
            {
				repository.Insert(soup);
				repository.Save();

                return RedirectToAction("Index");
            }

            return View(soup);
        }

        // GET: Soups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

			var soup = repository.SelectByID(id);

            if (soup == null)
            {
                return HttpNotFound();
            }

            return View(soup);
        }

        // POST: Soups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SoupId,Name,Weight")] Soup soup)
        {
            if (ModelState.IsValid)
            {
				repository.Update(soup);
				repository.Save();

                return RedirectToAction("Index");
            }

            return View(soup);
        }

        // GET: Soups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

			var soup = repository.SelectByID(id);

            if (soup == null)
            {
                return HttpNotFound();
            }

            return View(soup);
        }

        // POST: Soups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			repository.Delete(id);
			repository.Save();

            return RedirectToAction("Index");
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
