using System.ComponentModel.DataAnnotations;
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

        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}

