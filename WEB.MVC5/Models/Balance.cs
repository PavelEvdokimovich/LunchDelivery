using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEB.MVC5.Models
{
	public class Balance
	{
		public int BalanceId { get; set; }

		public int UserId { get; set; }
		public User User { get; set; }

		[Display(Name = "Дата операции")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd'.'MM'.'yyyy}")]
		[DataType(DataType.Date)]
		public DateTime Date { get; set; }

		[Display(Name = "Поступление на счет")]
		[DisplayFormat(DataFormatString = "{0:C}")]
		public decimal Income { get; set; }

		[Display(Name = "Списание со счета")]
		[DisplayFormat(DataFormatString = "{0:C}")]
		public decimal Expence { get; set; }

		[Display(Name = "Назначение операции")]
		public string Comment { get; set; }
	}
}