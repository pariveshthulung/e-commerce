using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ebay.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ebay.ViewModel
{

    public class ProductUpdateVm
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Column(TypeName = "decimal(18,4)")] // <--

        public decimal Price { get; set; }
        public string? Brand { get; set; }
        public int Stock { get; set; }
        public string? Image { get; set; }
        [DisplayName("Upload Image")]
        public IFormFile? ImageFile { get; set; }

		public List<ProductCategory>? CategoryName { get; set; }
        public List<int> CategoryIds { get; set; }


        public int CategoryId { get; set; }
        public List<Category>? Categories { get; set; }

        public SelectList CategoryList()
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