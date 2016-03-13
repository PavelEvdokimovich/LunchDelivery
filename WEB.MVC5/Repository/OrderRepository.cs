using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WEB.MVC5.Models;

namespace WEB.MVC5.Repository
{
	public class OrderRepository// : IRepository<Order>
	{
		/*private LunchContext db;

		public OrderRepository(LunchContext context)
		{
			this.db = context;
		}

		public IEnumerable<Order> GetAll()
		{
			return db.Orders.Include(o => o.UserId);
		}

		public Order Get(int id)
		{
			return db.Orders.Find(id);
		}

		public void Create(Order order)
		{
			db.Orders.Add(order);
		}

		public void Update(Order order)
		{
			db.Entry(order).State = EntityState.Modified;
		}

		public IEnumerable<Order> Find(Func<Order, Boolean> predicate)
		{
			return db.Orders.Include(o => o.UserId).Where(predicate).ToList();
		}

		public void Delete(int id)
		{
			Order order = db.Orders.Find(id);
			if (order != null)
			{
				db.Orders.Remove(order);
			}
		}*/
	}
}
