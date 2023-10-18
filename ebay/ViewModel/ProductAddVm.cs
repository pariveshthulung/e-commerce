using System.ComponentModel.DataAnnotations;
using ebay.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ebay.ViewModel
{
    public class ProductAddVm
	{

        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string? Brand { get; set; }
        public int Stock { get; set; }

		public string? Product_image { get; set; }
        public int CategoryId { get ; set; }
        // [ValidateNever]
        public List<Category>? Categories ;


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

