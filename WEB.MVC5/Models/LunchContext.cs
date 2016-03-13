using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WEB.MVC5.Models
{
	public class LunchContext : DbContext
	{
		public LunchContext() : base("DefaultConnection")
		{

		}

		public DbSet<User> Users { get; set; }

		public DbSet<Order> Orders { get; set; }

		public DbSet<Lunch> Lunches { get; set; }

		public DbSet<Salad> Salads { get; set; }

		public DbSet<Soup> Soups { get; set; }

		public DbSet<MeatDish> MeatDishes { get; set; }

		public DbSet<Garnish> Garnishes { get; set; }

		public DbSet<Bread> Breads { get; set; }

		public DbSet<Balance> Balances { get; set; }

		public DbSet<Comment> Comments { get; set; }


		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{

		}

		/*static LunchContext()
		{
			Database.SetInitializer<LunchContext>(new StoreDbInitializer());
		}*/

		//public LunchContext(string connectionString) : base(connectionString) {}
	}

	/*public class StoreDbInitializer : DropCreateDatabaseIfModelChanges<LunchContext>
	{
		protected override void Seed(LunchContext db)
		{
			db.Lunches.Add(new Lunch { Date = DateTime.Parse("2016-01-11"), Menu = "Суп, каша", PriceWithSoup = 35000, PriceWithoutSoup = 30000 });
			db.Lunches.Add(new Lunch { Date = DateTime.Parse("2016-01-12"), Menu = "Суп, пюре", PriceWithSoup = 35000, PriceWithoutSoup = 30000 });
			db.Lunches.Add(new Lunch { Date = DateTime.Parse("2016-01-13 12:00:00.000"), Menu = "Суп, каша, драники", PriceWithSoup = 35000, PriceWithoutSoup = 30000 });
			db.Lunches.Add(new Lunch { Date = DateTime.Parse("2016-01-14 12:00:00.000"), Menu = "Суп, пюреб тефтели", PriceWithSoup = 35000, PriceWithoutSoup = 30000 });
			db.Lunches.Add(new Lunch { Date = DateTime.Parse("2016-01-15 12:00:00.000"), Menu = "Хлеб", PriceWithSoup = 35000, PriceWithoutSoup = 30000 });

			db.Users.Add(new User { Name = "Ivan", LastName = "Ivanov", Phone = "101", Email = "1@1.by", Balance = 1000000});
			db.Users.Add(new User { Name = "Petr", LastName = "Petrov", Phone = "102", Email = "2@2.by", Balance = 2000000 });

			db.SaveChanges();
		}
	}*/
}
