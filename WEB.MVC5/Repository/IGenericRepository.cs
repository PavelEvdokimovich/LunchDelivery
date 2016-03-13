using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB.MVC5.Repository
{
	public interface IGenericRepository<T> : IDisposable where T : class
	{
		IEnumerable<T> SelectAll();

		T SelectByID(object id);

		void Insert(T obj);

		void Update(T obj);

		void Delete(object id);

		void Save();
	}
}
