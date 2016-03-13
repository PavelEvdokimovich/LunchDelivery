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
using Ninject;

namespace WEB.MVC5.Controllers
{
	[Authorize(Roles = "admin")]
	public class BreadsController : Controller
    {
        //private LunchContext db = new LunchContext();

		private IGenericRepository<Bread> repository = null;

		public BreadsController(IGenericRepository<Bread> repositoryBread)
		{
			//this.repository = new GenericRepository<Bread>();

			/*IKernel ninjectKernel = new StandardKernel();
			ninjectKernel.Bind<IGenericRepository<Bread>>().To<GenericRepository<Bread>>();
			this.repository = ninjectKernel.Get<IGenericRepository<Bread>>();*/

			this.repository = repositoryBread;
		}

		// GET: Breads
		public ActionResult Index()
        {
			var breads = repository.SelectAll().ToList().OrderBy(b => b.Name);
			return View(breads);

			/*var breads = db.Breads.OrderBy(b => b.Name);

			return View(await breads.ToListAsync());*/
		}

        // GET: Breads/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Breads/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BreadId,Name,Weight")] Bread bread)
        {
            if (ModelState.IsValid)
            {
				repository.Insert(bread);
				repository.Save();
				/*db.Breads.Add(bread);
                await db.SaveChangesAsync();*/

				return RedirectToAction("Index");
            }

            return View(bread);
        }

        // GET: Breads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

			var bread = repository.SelectByID(id);

			//Bread bread = await db.Breads.FindAsync(id);
            if (bread == null)
            {
                return HttpNotFound();
            }
            return View(bread);
        }

        // POST: Breads/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BreadId,Name,Weight")] Bread bread)
        {
            if (ModelState.IsValid)
            {
				repository.Update(bread);
				repository.Save();
				/*db.Entry(bread).State = EntityState.Modified;
                await db.SaveChangesAsync();*/
				return RedirectToAction("Index");
            }
            return View(bread);
        }

        // GET: Breads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			var bread = repository.SelectByID(id);
			//Bread bread = await db.Breads.FindAsync(id);
            if (bread == null)
            {
                return HttpNotFound();
            }
            return View(bread);
        }

        // POST: Breads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			repository.Delete(id);
			repository.Save();

			/*Bread bread = await db.Breads.FindAsync(id);
            db.Breads.Remove(bread);
            await db.SaveChangesAsync();*/

            return RedirectToAction("Index");
        }

		/*protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/

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
