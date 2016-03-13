using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WEB.MVC5.Models;

namespace WEB.MVC5.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private LunchContext db = null;

		private DbSet<T> table = null;

		public GenericRepository()
		{
			this.db = new LunchContext();
			table = db.Set<T>();
		}

		public GenericRepository(LunchContext db)
		{
			this.db = db;
			table = db.Set<T>();
		}

		public IEnumerable<T> SelectAll()
		{
			return table.ToList();
		}

		public T SelectByID(object id)
		{
			return table.Find(id);
		}

		public void Insert(T obj)
		{
			table.Add(obj);
		}

		public void Update(T obj)
		{
			table.Attach(obj);
			db.Entry(obj).State = EntityState.Modified;
		}

		public void Delete(object id)
		{
			T existing = table.Find(id);
			table.Remove(existing);
		}

		public void Save()
		{
			db.SaveChanges();
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}