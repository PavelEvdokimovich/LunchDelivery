using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEB.MVC5.Models
{
	public class AddComment
	{
		[Display(Name = "Дата отзыва")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd'.'MM'.'yyyy}")]
		[DataType(DataType.Date)]
		public DateTime Date { get; set; }

		public int UserId { get; set; }
		public User User { get; set; }

		[Display(Name = "Дата обеда")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd'.'MM'.'yyyy}")]
		[DataType(DataType.Date)]
		public DateTime LunchDate { get; set; }

		[Display(Name = "Оценка салата")]
		[Range(1, 5, ErrorMessage = "Допустимая оценка от 1 до 5")]
		public int SaladRating { get; set; }

		[Display(Name = "Отзыв о салате")]
		[DataType(DataType.MultilineText)]
		public string SaladComment { get; set; }

		[Display(Name = "Оценка супа")]
		[Range(1, 5, ErrorMessage = "Допустимая оценка от 1 до 5")]
		public int SoupRating { get; set; }

		[Display(Name = "Отзыв о супе")]
		[DataType(DataType.MultilineText)]
		public string SoupComment { get; set; }

		[Display(Name = "Оценка мясного")]
		[Range(1, 5, ErrorMessage = "Допустимая оценка от 1 до 5")]
		public int MeatDishRating { get; set; }

		[Display(Name = "Отзыв о мясном")]
		[DataType(DataType.MultilineText)]
		public string MeatDishComment { get; set; }

		[Display(Name = "Оценка гарнира")]
		[Range(1, 5, ErrorMessage = "Допустимая оценка от 1 до 5")]
		public int GarnishRating { get; set; }

		[Display(Name = "Отзыв о гарнире")]
		[DataType(DataType.MultilineText)]
		public string GarnishComment { get; set; }
	}
}