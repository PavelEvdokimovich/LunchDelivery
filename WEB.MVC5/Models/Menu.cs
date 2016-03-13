using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEB.MVC5.Models
{
	public class Bread
	{
		public int BreadId { get; set; }

		[Display(Name = "Наименование")]
		public string Name { get; set; }

		[Display(Name = "Выход")]
		public string Weight { get; set; }

		public ICollection<Lunch> Lunches { get; set; }
	}

	public class Garnish
	{
		public int GarnishId { get; set; }

		[Display(Name = "Наименование")]
		public string Name { get; set; }

		[Display(Name = "Выход")]
		public string Weight { get; set; }

		[Display(Name = "Общий рейтинг")]
		public double Rating { get; set; }

		public ICollection<Lunch> Lunches { get; set; }
	}

	public class MeatDish
	{
		public int MeatDishId { get; set; }

		[Display(Name = "Наименование")]
		public string Name { get; set; }

		[Display(Name = "Выход")]
		public string Weight { get; set; }

		[Display(Name = "Общий рейтинг")]
		public double Rating { get; set; }

		public ICollection<Lunch> Lunches { get; set; }
	}

	public class Salad
	{
		public int SaladId { get; set; }

		[Display(Name = "Наименование")]
		public string Name { get; set; }

		[Display(Name = "Описание")]
		public string Description { get; set; }

		[Display(Name = "Выход")]
		public string Weight { get; set; }

		[Display(Name = "Общий рейтинг")]
		public double Rating { get; set; }

		public ICollection<Lunch> Lunches { get; set; }
	}

	public class Soup
	{
		public int SoupId { get; set; }

		[Display(Name = "Наименование")]
		public string Name { get; set; }

		[Display(Name = "Выход")]
		public string Weight { get; set; }

		[Display(Name = "Общий рейтинг")]
		public double Rating { get; set; }

		public ICollection<Lunch> Lunches { get; set; }
	}
}