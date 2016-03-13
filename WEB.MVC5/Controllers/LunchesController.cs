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
	public class LunchesController : Controller
    {
		private ILunchRepository repository = null;

		public LunchesController(ILunchRepository repositoryLunch)
		{
			this.repository = repositoryLunch;
		}

		// GET: Lunches
		[Authorize(Roles = "admin")]
		public ActionResult Index()
        {
			var lunches = repository.SelectAll().ToList().OrderByDescending(l => l.Date);

			return View(lunches);
        }

		// GET: Lunches/Create
		[Authorize(Roles = "admin")]
		public ActionResult Create()
        {
            ViewBag.BreadId = new SelectList(repository.SelectAllBreads(), "BreadId", "Name").OrderBy(b => b.Text);
            ViewBag.GarnishId = new SelectList(repository.SelectAllGarnishes(), "GarnishId", "Name").OrderBy(g => g.Text);
            ViewBag.MeatDishId = new SelectList(repository.SelectAllMeatDishes(), "MeatDishId", "Name").OrderBy(md => md.Text);
            ViewBag.SaladId = new SelectList(repository.SelectAllSalads(), "SaladId", "Name").OrderBy(s => s.Text);
            ViewBag.SoupId = new SelectList(repository.SelectAllSoups(), "SoupId", "Name").OrderBy(s => s.Text);

            return View();
        }

        // POST: Lunches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "admin")]
		public ActionResult Create([Bind(Include = "LunchId,Date,SaladId,SoupId,GarnishId,MeatDishId,BreadId,Menu,PriceWithSoup,PriceWithoutSoup")] Lunch lunch)
        {
            if (ModelState.IsValid)
            {
				repository.Insert(lunch);
				repository.Save();

                return RedirectToAction("Index");
            }

            ViewBag.BreadId = new SelectList(repository.SelectAllBreads(), "BreadId", "Name", lunch.BreadId);
            ViewBag.GarnishId = new SelectList(repository.SelectAllGarnishes(), "GarnishId", "Name", lunch.GarnishId);
            ViewBag.MeatDishId = new SelectList(repository.SelectAllMeatDishes(), "MeatDishId", "Name", lunch.MeatDishId);
            ViewBag.SaladId = new SelectList(repository.SelectAllSalads(), "SaladId", "Name", lunch.SaladId);
            ViewBag.SoupId = new SelectList(repository.SelectAllSoups(), "SoupId", "Name", lunch.SoupId);

            return View(lunch);
        }

		// GET: Lunches/Edit/5
		[Authorize(Roles = "admin")]
		public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

			var lunch = repository.SelectByID(id);

            if (lunch == null)
            {
                return HttpNotFound();
            }

            ViewBag.BreadId = new SelectList(repository.SelectAllBreads(), "BreadId", "Name", lunch.BreadId).OrderBy(b => b.Text);
            ViewBag.GarnishId = new SelectList(repository.SelectAllGarnishes(), "GarnishId", "Name", lunch.GarnishId).OrderBy(g => g.Text);
            ViewBag.MeatDishId = new SelectList(repository.SelectAllMeatDishes(), "MeatDishId", "Name", lunch.MeatDishId).OrderBy(md => md.Text);
            ViewBag.SaladId = new SelectList(repository.SelectAllSalads(), "SaladId", "Name", lunch.SaladId).OrderBy(s => s.Text);
            ViewBag.SoupId = new SelectList(repository.SelectAllSoups(), "SoupId", "Name", lunch.SoupId).OrderBy(s => s.Text);

            return View(lunch);
        }

        // POST: Lunches/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "admin")]
		public ActionResult Edit([Bind(Include = "LunchId,Date,SaladId,SoupId,GarnishId,MeatDishId,BreadId,Menu,PriceWithSoup,PriceWithoutSoup")] Lunch lunch)
        {
            if (ModelState.IsValid)
            {
				repository.Update(lunch);
				repository.Save();

                return RedirectToAction("Index");
            }

            ViewBag.BreadId = new SelectList(repository.SelectAllBreads(), "BreadId", "Name", lunch.BreadId);
            ViewBag.GarnishId = new SelectList(repository.SelectAllGarnishes(), "GarnishId", "Name", lunch.GarnishId);
            ViewBag.MeatDishId = new SelectList(repository.SelectAllMeatDishes(), "MeatDishId", "Name", lunch.MeatDishId);
            ViewBag.SaladId = new SelectList(repository.SelectAllSalads(), "SaladId", "Name", lunch.SaladId);
            ViewBag.SoupId = new SelectList(repository.SelectAllSoups(), "SoupId", "Name", lunch.SoupId);

            return View(lunch);
        }

		// GET: Lunches/Delete/5
		[Authorize(Roles = "admin")]
		public ActionResult Delete(int? id)
        {
			if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

			var lunch = repository.SelectByID(id);

			if (lunch == null)
            {
                return HttpNotFound();
            }

            return View(lunch);
        }

        // POST: Lunches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = "admin")]
		public ActionResult DeleteConfirmed(int id)
        {
			repository.Delete(id);
			repository.Save();

            return RedirectToAction("Index");
        }

		// GET: Lunches/ActualLunches
		[Authorize(Roles = "admin, user")]
		public ActionResult ActualLunches()
		{
			var lunches = repository.SelectAll().ToList().OrderBy(l => l.Date).Where(l => l.Date.Date >= DateTime.Now.Date);

			return View(lunches);
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
