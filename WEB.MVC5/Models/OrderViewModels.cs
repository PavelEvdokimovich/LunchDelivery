using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEB.MVC5.Models
{
	public class OrderViewModel
	{
		public Lunch Lunch { get; set; }

		[Display(Name = "Без первого")]
		public bool WithoutSoup { get; set; }

		[Display(Name = "Заказать")]
		public bool IsOrder { get; set; }

		public OrderViewModel()
		{
			//for asp.net mvc model binding
		}

		public OrderViewModel(Lunch lunch, bool withoutSoup, bool isOrder)
		{
			Lunch = lunch;
			WithoutSoup = withoutSoup;
			IsOrder = isOrder;
		}
	}

	public class AllOrdersViewModel
	{
		[Display(Name = "Дата обеда")]
		public DateTime Date { get; set; }

		[Display(Name = "Количество полных обедов")]
		public int QuantityWithSoup { get; set; }

		[Display(Name = "Количество обедов без первого")]
		public int QuantityWithoutSoup { get; set; }

		[Display(Name = "Сумма к оплате")]
		[DisplayFormat(DataFormatString = "{0:C}")]
		public decimal Pay { get; set; }
	}
}