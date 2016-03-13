using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEB.MVC5.Models
{
	public class UserBalance
	{
		public int UserId { get; set; }

		public string Name { get; set; }

		[DisplayFormat(DataFormatString = "{0:C}")]
		public decimal TotalBalance { get; set; }

		public DateTime Date { get; set; }

		[DisplayFormat(DataFormatString = "{0:C}")]
		public decimal Income { get; set; }

		[DisplayFormat(DataFormatString = "{0:C}")]
		public decimal Expence { get; set; }

		public string Comment { get; set; }
	}

	public class UserTotalBalance
	{
		public int UserId { get; set; }

		public string Name { get; set; }

		[DisplayFormat(DataFormatString = "{0:C}")]
		public decimal UserBalance { get; set; }

		[DisplayFormat(DataFormatString = "{0:C}")]
		public decimal TotalBalance { get; set; }
	}
}