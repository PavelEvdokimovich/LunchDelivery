using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB.MVC5.Models
{
	public class Lunch
	{
		public int LunchId { get; set; }

		[Display(Name = "Дата обеда")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd'.'MM'.'yyyy}")]
		[DataType(DataType.Date)]
		public DateTime Date { get; set; }

		[Display(Name = "Салат")]
		public int SaladId { get; set; }
		public Salad Salad { get; set; }

		//[Required]
		[Display(Name = "Суп")]
		public int SoupId { get; set; }
		//[ForeignKey("CountryId")]
		public Soup Soup { get; set; }

		[Display(Name = "Гарнир")]
		public int GarnishId { get; set; }
		public Garnish Garnish { get; set; }

		[Display(Name = "Мясное блюдо")]
		public int MeatDishId { get; set; }
		public MeatDish MeatDish { get; set; }

		[Display(Name = "Хлеб")]
		public int BreadId { get; set; }
		public Bread Bread { get; set; }

		[Display(Name = "Список блюд")]
		[DataType(DataType.MultilineText)]
		public string Menu { get; set; }

		[Display(Name = "Стоимость обеда")]
		[DisplayFormat(DataFormatString = "{0:C}")]
		public decimal PriceWithSoup { get; set; }

		[Display(Name = "Стоимость без первого")]
		[DisplayFormat(DataFormatString = "{0:C}")]
		public decimal PriceWithoutSoup { get; set; }

		public ICollection<Order> Orders { get; set; }

	}
}
