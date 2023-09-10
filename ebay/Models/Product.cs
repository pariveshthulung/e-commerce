using System;
using System.ComponentModel.DataAnnotations;

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
        public int Price { get; set; }
		public string? Brand { get; set; }
        [Required]
        public int Quantity { get; set; }
		public int Sold { get; set; }
        [Required]
        public string? Color { get; set; }

	}
}

