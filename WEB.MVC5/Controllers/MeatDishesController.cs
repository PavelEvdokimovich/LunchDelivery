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
	public class MeatDishesController : Controller
    {
		private IGenericRepository<MeatDish> repository = null;

		public MeatDishesController(IGenericRepository<MeatDish> repositoryMeatDish)
		{
			this.repository = repositoryMeatDish;
		}

		// GET: MeatDishes
		public ActionResult Index()
        {
			var meatDishes = repository.SelectAll().ToList().OrderBy(md => md.Name);

			return View(meatDishes);
        }

        // GET: MeatDishes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MeatDishes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MeatDishId,Name,Weight")] MeatDish meatDish)
        {
            if (ModelState.IsValid)
            {
				repository.Insert(meatDish);
				repository.Save();

                return RedirectToAction("Index");
            }

            return View(meatDish);
        }

        // GET: MeatDishes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

			var meatDish = repository.SelectByID(id);

            if (meatDish == null)
            {
                return HttpNotFound();
            }

            return View(meatDish);
        }

        // POST: MeatDishes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MeatDishId,Name,Weight")] MeatDish meatDish)
        {
            if (ModelState.IsValid)
            {
				repository.Update(meatDish);
				repository.Save();

                return RedirectToAction("Index");
            }

            return View(meatDish);
        }

        // GET: MeatDishes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

			var meatDish = repository.SelectByID(id);

            if (meatDish == null)
            {
                return HttpNotFound();
            }

            return View(meatDish);
        }

        // POST: MeatDishes/Delete/5
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
