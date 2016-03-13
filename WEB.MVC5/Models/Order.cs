using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB.MVC5.Models
{
	public class Order
	{
		public int OrderId { get; set; }

		public int UserId { get; set; }
		public User User { get; set; }

		public int LunchId { get; set; }
		public Lunch Lunch { get; set; }

		[Display(Name = "Заказ без первого")]
		public bool WithoutSoup { get; set; }

		[Display(Name = "Заказ получен")]
		public bool Delivered { get; set; }

	}
}
