using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WEB.MVC5.Controllers;
using System.Web.Mvc;

namespace UnitTestLunchDelivery.Controllers
{
	[TestClass]
	public class HomeControllerTest
	{
		[TestMethod]
		public void IndexViewResultNotNull()
		{
			HomeController controller = new HomeController();

			ViewResult result = controller.Index() as ViewResult;

			Assert.IsNotNull(result);
		}
	}
}
