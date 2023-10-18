using System.ComponentModel.DataAnnotations;
using ebay.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ebay.ViewModel
{

    public class ProductUpdateVm
    {
		public string? Name { get; set; }
		public string? Description { get; set; }
        public decimal Price { get; set; }
		public string? Brand { get; set; }
        public int Stock { get; set; }
        public string? Product_image { get; set; }

        public int CategoryId { get; set; }
        public List<Category>? Categories { get; set; }

        public SelectList CategoryList(){
            return new SelectList(
                Categories,
                nameof(Category.id),
                nameof(Category.Name),
                CategoryId
            );
        }
    }
}