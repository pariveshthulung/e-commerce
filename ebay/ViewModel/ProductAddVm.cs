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
        public int Price { get; set; }
        public string? Brand { get; set; }
        [Required]
        public int Quantity { get; set; }
        public int Sold { get; set; }
        [Required]
        public string? Color { get; set; }
        public int CategoryId { get; set; }
        [ValidateNever]
        public List<Category>? Categories { get; set; }


        public SelectList CategoryList(){
            return new SelectList(
                Categories,
                nameof(Category.id),
                nameof(Category.Name)
            );
            
        }
    }
}

