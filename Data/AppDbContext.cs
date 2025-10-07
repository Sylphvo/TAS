using Microsoft.EntityFrameworkCore;
using TAS.Models;

namespace TAS.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<Product> Products { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Seed data mẫu
			modelBuilder.Entity<Product>().HasData(
				new Product { Id = 1, Name = "Laptop Dell", Description = "Laptop cao cấp", Price = 15000000, Stock = 10 },
				new Product { Id = 2, Name = "iPhone 15", Description = "Điện thoại thông minh", Price = 25000000, Stock = 20 },
				new Product { Id = 3, Name = "Samsung TV", Description = "Tivi 55 inch", Price = 12000000, Stock = 5 }
			);
		}
	}
}
