using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ebay.Models
{
	public class Product
	{
		public int id { get; set; }
		[Required]
		public string? Name { get; set; }
		[Required]
		public string? Description { get; set; }
		[Required]
		[Column(TypeName = "decimal(18,4)")] // <--

		public decimal Price { get; set; }
		public int Stock { get; set; }
		public string? Brand { get; set; }
		public string? Product_image { get; set; }

		public int CategoryId { get; set; }
		[ForeignKey("CategoryId")]
		public virtual Category? Category { get; set; }
		

	}
}

