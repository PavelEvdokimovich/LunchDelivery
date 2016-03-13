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

namespace WEB.MVC5.Controllers
{
    public class CommentsController : Controller
    {
        private LunchContext db = new LunchContext();

        // GET: Comments
        public async Task<ActionResult> Index()
        {
            var comments = db.Comments.Include(c => c.Lunch).Include(c => c.User)
							.Include(c => c.Lunch.Salad).Include(c => c.Lunch.Soup).Include(c => c.Lunch.MeatDish).Include(c => c.Lunch.Garnish).OrderByDescending(c => c.Lunch.Date);
            return View(await comments.ToListAsync());
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddComment addComment/*[Bind(Include = "CommentId,Date,UserId,LunchId,SaladRating,SaladComment,SoupRating,SoupComment,MeatDishRating,MeatDishComment,GarnishRating,GarnishComment")] Comment comment*/)
        {
			if (ModelState.IsValid && db.Lunches.Where(l => l.Date == addComment.LunchDate).Count() != 0)
            {
				Comment comment = new Comment();
				comment.Date = DateTime.Now;
				comment.UserId = db.Users.Where(u => u.Email == HttpContext.User.Identity.Name).ToList().ElementAt(0).UserId;
				comment.LunchId = db.Lunches.Where(l => l.Date == addComment.LunchDate).ToList().ElementAt(0).LunchId;
				comment.SaladRating = addComment.SaladRating;
				comment.SaladComment = addComment.SaladComment;
				comment.SoupRating = addComment.SoupRating;
				comment.SoupComment = addComment.SoupComment;
				comment.MeatDishRating = addComment.MeatDishRating;
				comment.MeatDishComment = addComment.MeatDishComment;
				comment.GarnishRating = addComment.GarnishRating;
				comment.GarnishComment = addComment.GarnishComment;

				SetSaladRating(comment);
				SetSoupRating(comment);
				SetMeatDishRating(comment);
				SetGarnishRating(comment);

				db.Comments.Add(comment);

				await db.SaveChangesAsync();

				return RedirectToAction("Index");
            }

            return View();
        }

		private void SetSaladRating(Comment comment)
		{
			var sumSaladRatings = 0;

			double saladRating;

			Lunch lunch = db.Lunches.Find(comment.LunchId);

			var saladId = lunch.SaladId;

			int countSaladRatings = db.Comments.Where(c => c.Lunch.SaladId == saladId).Count();

			if (countSaladRatings == 0)
			{
				saladRating = comment.SaladRating;
			}
			else
			{
				sumSaladRatings = db.Comments.Include(c => c.Lunch).Where(c => c.Lunch.SaladId == saladId).Sum(c => c.SaladRating);

				saladRating = (double)(sumSaladRatings + comment.SaladRating) / (countSaladRatings + 1);
			}

			Salad salad = db.Salads.Find(saladId);

			salad.Rating = saladRating;

			db.Entry(salad).State = EntityState.Modified;
		}

		private void SetSoupRating(Comment comment)
		{
			var sumSoupRatings = 0;

			double soupRating;

			Lunch lunch = db.Lunches.Find(comment.LunchId);

			var soupId = lunch.SoupId;

			int countSoupRatings = db.Comments.Where(c => c.Lunch.SoupId == soupId).Count();

			if (countSoupRatings == 0)
			{
				soupRating = comment.SoupRating;
			}
			else
			{
				sumSoupRatings = db.Comments.Include(c => c.Lunch).Where(c => c.Lunch.SoupId == soupId).Sum(c => c.SoupRating);

				soupRating = (double)(sumSoupRatings + comment.SoupRating) / (countSoupRatings + 1);
			}

			Soup soup = db.Soups.Find(soupId);

			soup.Rating = soupRating;

			db.Entry(soup).State = EntityState.Modified;
		}

		private void SetMeatDishRating(Comment comment)
		{
			var sumMeatDishRatings = 0;

			double meatDishRating;

			Lunch lunch = db.Lunches.Find(comment.LunchId);

			var meatDishId = lunch.MeatDishId;

			int countMeatDishRatings = db.Comments.Where(c => c.Lunch.MeatDishId == meatDishId).Count();

			if (countMeatDishRatings == 0)
			{
				meatDishRating = comment.MeatDishRating;
			}
			else
			{
				sumMeatDishRatings = db.Comments.Include(c => c.Lunch).Where(c => c.Lunch.MeatDishId == meatDishId).Sum(c => c.MeatDishRating);

				meatDishRating = (double)(sumMeatDishRatings + comment.MeatDishRating) / (countMeatDishRatings + 1);
			}

			MeatDish meatDish = db.MeatDishes.Find(meatDishId);

			meatDish.Rating = meatDishRating;

			db.Entry(meatDish).State = EntityState.Modified;
		}

		private void SetGarnishRating(Comment comment)
		{
			var sumGarnishRatings = 0;

			double garnishRating;

			Lunch lunch = db.Lunches.Find(comment.LunchId);

			var garnishId = lunch.GarnishId;

			int countGarnishRatings = db.Comments.Where(c => c.Lunch.GarnishId == garnishId).Count();

			if (countGarnishRatings == 0)
			{
				garnishRating = comment.GarnishRating;
			}
			else
			{
				sumGarnishRatings = db.Comments.Include(c => c.Lunch).Where(c => c.Lunch.GarnishId == garnishId).Sum(c => c.GarnishRating);

				garnishRating = (double)(sumGarnishRatings + comment.GarnishRating) / (countGarnishRatings + 1);
			}

			Garnish garnish = db.Garnishes.Find(garnishId);

			garnish.Rating = garnishRating;

			db.Entry(garnish).State = EntityState.Modified;
		}

		protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

		public async Task<ActionResult> SaladRatings()
		{
			return View(await db.Salads.OrderByDescending(s => s.Rating).ToListAsync());
		}

		public async Task<ActionResult> SoupRatings()
		{
			return View(await db.Soups.OrderByDescending(s => s.Rating).ToListAsync());
		}

		public async Task<ActionResult> MeatDishRatings()
		{
			return View(await db.MeatDishes.OrderByDescending(m => m.Rating).ToListAsync());
		}

		public async Task<ActionResult> GarnishRatings()
		{
			return View(await db.Garnishes.OrderByDescending(g => g.Rating).ToListAsync());
		}

		public async Task<ActionResult> SaladCommentsHistory(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var comments = db.Comments.Include(c => c.Lunch).Include(c => c.User).Where(c => c.Lunch.SaladId == id);

			ViewBag.Name = db.Salads.Find(id).Name;

			return View(await comments.ToListAsync());
		}

		public async Task<ActionResult> SoupCommentsHistory(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var comments = db.Comments.Include(c => c.Lunch).Include(c => c.User).Where(c => c.Lunch.SoupId == id);

			ViewBag.Name = db.Soups.Find(id).Name;

			return View(await comments.ToListAsync());
		}

		public async Task<ActionResult> MeatDishCommentsHistory(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var comments = db.Comments.Include(c => c.Lunch).Include(c => c.User).Where(c => c.Lunch.MeatDishId == id);

			ViewBag.Name = db.MeatDishes.Find(id).Name;

			return View(await comments.ToListAsync());
		}

		public async Task<ActionResult> GarnishCommentsHistory(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var comments = db.Comments.Include(c => c.Lunch).Include(c => c.User).Where(c => c.Lunch.GarnishId == id);

			ViewBag.Name = db.Garnishes.Find(id).Name;

			return View(await comments.ToListAsync());
		}
	}
}
