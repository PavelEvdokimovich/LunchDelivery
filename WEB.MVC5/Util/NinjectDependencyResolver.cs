using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB.MVC5.Models;
using WEB.MVC5.Repository;

namespace WEB.MVC5.Util
{
	public class NinjectDependencyResolver : IDependencyResolver
	{
		private IKernel kernel;

		public NinjectDependencyResolver(IKernel kernelParam)
		{
			kernel = kernelParam;
			AddBindings();
		}

		public object GetService(Type serviceType)
		{
			return kernel.TryGet(serviceType);
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return kernel.GetAll(serviceType);
		}

		private void AddBindings()
		{
			kernel.Bind(typeof(IGenericRepository<>)).To(typeof(GenericRepository<>));

			//kernel.Bind<IGenericRepository<Lunch>>().To<LunchRepository>();
			kernel.Bind<ILunchRepository>().To<LunchRepository>();
		}
	}
}