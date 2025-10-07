using System.ComponentModel.DataAnnotations;

namespace TAS.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[StringLength(500)]
		public string Description { get; set; }

		[Range(0, double.MaxValue)]
		public decimal Price { get; set; }

		public int Stock { get; set; }

		public DateTime CreatedDate { get; set; } = DateTime.Now;
	}
}
