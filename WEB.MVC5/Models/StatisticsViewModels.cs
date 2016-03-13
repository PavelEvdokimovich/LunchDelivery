using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB.MVC5.Models
{
	public class MyData
	{
		public string Name { get; set; }

		public double Value { get; set; }
	}

	public class CountOrdersPerDay
	{
		public string Date { get; set; }

		public int Count { get; set; }
	}

	public class CountOrdersWithAndWithoutSoupPerDay
	{
		public string Date { get; set; }

		public int CountWithSoup { get; set; }

		public int CountWithoutSoup { get; set; }
	}

	public class MoneyPerDay
	{
		public string Date { get; set; }

		public decimal Sum { get; set; }
	}
}