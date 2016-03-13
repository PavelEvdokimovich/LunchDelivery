using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WEB.MVC5.Models;

namespace WEB.MVC5.Repository
{
	public class LunchRepository : ILunchRepository
	{
		private LunchContext db;

		public LunchRepository(LunchContext context)
		{
			this.db = context;
		}

		public IEnumerable<Lunch> SelectAll()
		{
			return db.Lunches.Include(l => l.Bread).Include(l => l.Garnish).Include(l => l.MeatDish).Include(l => l.Salad).Include(l => l.Soup);
		}

		public Lunch SelectByID(object id)
		{
			return db.Lunches.Find(id);
		}

		public void Insert(Lunch lunch)
		{
			db.Lunches.Add(lunch);
		}

		public void Update(Lunch lunch)
		{
			db.Entry(lunch).State = EntityState.Modified;
		}

		/*public IEnumerable<Lunch> Find(Func<Lunch, Boolean> predicate)
		{
			return db.Lunches.Where(predicate).ToList();
		}*/

		public void Delete(object id)
		{
			Lunch lunch = db.Lunches.Find(id);
			if (lunch != null)
			{
				db.Lunches.Remove(lunch);
			}
		}

		public void Save()
		{
			db.SaveChanges();
		}

		public IEnumerable<Bread> SelectAllBreads()
		{
			return db.Breads;
		}

		public IEnumerable<Garnish> SelectAllGarnishes()
		{
			return db.Garnishes;
		}

		public IEnumerable<MeatDish> SelectAllMeatDishes()
		{
			return db.MeatDishes;
		}

		public IEnumerable<Salad> SelectAllSalads()
		{
			return db.Salads;
		}

		public IEnumerable<Soup> SelectAllSoups()
		{
			return db.Soups;
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}
