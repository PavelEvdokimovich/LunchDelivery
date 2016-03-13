using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEB.MVC5.Models
{
	public class Comment
	{
		public int CommentId { get; set; }

		public DateTime Date { get; set; }

		public int UserId { get; set; }
		public User User { get; set; }

		public int LunchId { get; set; }
		public Lunch Lunch { get; set; }

		public int SaladRating { get; set; }

		public string SaladComment { get; set; }

		public int SoupRating { get; set; }

		public string SoupComment { get; set; }

		public int MeatDishRating { get; set; }

		public string MeatDishComment { get; set; }

		public int GarnishRating { get; set; }

		public string GarnishComment { get; set; }
	}
}