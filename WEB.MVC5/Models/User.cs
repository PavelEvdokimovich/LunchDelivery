using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB.MVC5.Models
{
	public class User
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

		[Display(Name = "Администратор")]
		public bool isAdmin { get; set; }

		[Display(Name = "Пользователь")]
		public bool isUser { get; set; }

		public byte[] PhotoData { get; set; }

		public string PhotoMimeType { get; set; }

		public ICollection<Order> Orders { get; set; }

	}
}
