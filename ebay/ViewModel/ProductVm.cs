using ebay.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ebay.ViewModel;

public class ProductVm
{
    public Product? Product;
    public int CategoryId { get; set; }
    
    public List<Category>? Categories { get; set; }

    public SelectList CategoryLists()
        {
            return new SelectList(
                Categories,
                nameof(Category.id),
                nameof(Category.Name),
                CategoryId
            );

        }
}
