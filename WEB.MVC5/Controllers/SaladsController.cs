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
	public class SaladsController : Controller
    {
		private IGenericRepository<Salad> repository = null;

		public SaladsController(IGenericRepository<Salad> repositorySalad)
		{
			this.repository = repositorySalad;
		}

		// GET: Salads
		public ActionResult Index()
        {
			var salads = repository.SelectAll().ToList().OrderBy(s => s.Name);

			return View(salads);
        }

        // GET: Salads/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Salads/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SaladId,Name,Description,Weight")] Salad salad)
        {
            if (ModelState.IsValid)
            {
				repository.Insert(salad);
				repository.Save();

                return RedirectToAction("Index");
            }

            return View(salad);
        }

        // GET: Salads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

			var salad = repository.SelectByID(id);

            if (salad == null)
            {
                return HttpNotFound();
            }

            return View(salad);
        }

        // POST: Salads/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SaladId,Name,Description,Weight")] Salad salad)
        {
            if (ModelState.IsValid)
            {
				repository.Update(salad);
				repository.Save();

                return RedirectToAction("Index");
            }

            return View(salad);
        }

        // GET: Salads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

			var salad = repository.SelectByID(id);

            if (salad == null)
            {
                return HttpNotFound();
            }

            return View(salad);
        }

        // POST: Salads/Delete/5
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
