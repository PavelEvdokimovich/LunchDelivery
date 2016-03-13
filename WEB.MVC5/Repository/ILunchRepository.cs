using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB.MVC5.Models;

namespace WEB.MVC5.Repository
{
	public interface ILunchRepository : IDisposable
	{
		IEnumerable<Lunch> SelectAll();

		Lunch SelectByID(object id);

		void Insert(Lunch obj);

		void Update(Lunch obj);

		void Delete(object id);

		void Save();

		IEnumerable<Bread> SelectAllBreads();

		IEnumerable<Garnish> SelectAllGarnishes();

		IEnumerable<MeatDish> SelectAllMeatDishes();

		IEnumerable<Salad> SelectAllSalads();

		IEnumerable<Soup> SelectAllSoups();

		//IEnumerable<Lunch> Find(Func<Lunch, Boolean> predicate);
	}
}
