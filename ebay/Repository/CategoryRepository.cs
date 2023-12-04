using ebay.Data;
using ebay.Models;

namespace ebay.Repository.IRepository;

public class CategoryRepository : Repository<ProductCategory>, ICategoryRepository
{
    public readonly ApplicationDbContext _context;
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task AddAsync(int Id, int productId)
    {
        var ProductCategory = new ProductCategory();
        
            ProductCategory.ProductId = productId;
            ProductCategory.CategoryId = Id;
        
        await dbset.AddAsync(ProductCategory);
    }
}
