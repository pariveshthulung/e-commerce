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
		public int? CategoryId { get; set; }
		[ValidateNever]

		public List<Category>? Categories { get; set; }

		public SelectList? CategoryLists()
		{
			return new SelectList(
				Categories,
				nameof(Category.id),
				nameof(Category.Name),
				CategoryId
			);

		}

	}
}

