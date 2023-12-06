using System;
using ebay.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ebay.ViewModel
{
	public class ProductSearchVm
	{
		public string? Name { get; set; }
		public List<Product>? Data;
		public Product? Product;
		// public List<ProductCategory>? CategoryName { get; set; }
		[ValidateNever]

		public List<Category>? Categories { get; set; }
		public int CategoryId { get; set; }

		public SelectList? CategoryLists()
		{
			return new SelectList(
				Categories,
				nameof(Category.id),
				nameof(Category.Name),
				CategoryId
			);

		}
		public int CartCount { get; set; }

	}
}

