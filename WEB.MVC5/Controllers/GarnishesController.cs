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
	public class GarnishesController : Controller
    {
		private IGenericRepository<Garnish> repository = null;

		public GarnishesController(IGenericRepository<Garnish> repositoryGarnish)
		{
			this.repository = repositoryGarnish;
		}

		// GET: Garnishes
		public ActionResult Index()
        {
			var garnishes = repository.SelectAll().ToList().OrderBy(g => g.Name);

			return View(garnishes);
		}

        // GET: Garnishes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Garnishes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GarnishId,Name,Weight")] Garnish garnish)
        {
            if (ModelState.IsValid)
            {
				repository.Insert(garnish);
				repository.Save();

                return RedirectToAction("Index");
            }

            return View(garnish);
        }

        // GET: Garnishes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

			var garnish = repository.SelectByID(id);

            if (garnish == null)
            {
                return HttpNotFound();
            }

            return View(garnish);
        }

        // POST: Garnishes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GarnishId,Name,Weight")] Garnish garnish)
        {
            if (ModelState.IsValid)
            {
				repository.Update(garnish);
				repository.Save();

                return RedirectToAction("Index");
            }

            return View(garnish);
        }

        // GET: Garnishes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

			var garnish = repository.SelectByID(id);

            if (garnish == null)
            {
                return HttpNotFound();
            }

            return View(garnish);
        }

        // POST: Garnishes/Delete/5
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
