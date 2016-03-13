using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEB.MVC5.Models
{
	public class AddBalanceToUser
	{
		public int UserId { get; set; }

		[Display(Name = "Имя")]
		public string Name { get; set; }

		[Display(Name = "Фамилия")]
		public string LastName { get; set; }

		[Display(Name = "Электронная почта")]
		public string Email { get; set; }

		[Display(Name = "Сумма")]
		public decimal AddBalance { get; set; }
	}

	public class Profile
	{
		public int UserId { get; set; }

		[Display(Name = "Имя")]
		public string Name { get; set; }

		[Display(Name = "Фамилия")]
		public string LastName { get; set; }

		[Display(Name = "Номер телефона")]
		public string Phone { get; set; }

		[Display(Name = "Электронная почта")]
		public string Email { get; set; }

		[Display(Name = "Баланс")]
		[DisplayFormat(DataFormatString = "{0:C}")]
		public decimal Balance { get; set; }

		public byte[] PhotoData { get; set; }

	}
}