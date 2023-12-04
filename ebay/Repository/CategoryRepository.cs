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

    public async Task AddAsync(List<int>? Id, int productId)
    {
        foreach (var ids in Id)
        {
            var ProductCategory = new ProductCategory();

            ProductCategory.ProductId = productId;
            ProductCategory.CategoryId = ids;
            _context.Add(ProductCategory);
             await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateAsync(List<int>? Id, int ProductId)
    {
        foreach (var ids in Id)
        {
            var ProductCategoriesExits = _context.ProductCategories.Any(x => x.CategoryId == ids & x.ProductId == ProductId);
            if (ProductCategoriesExits)
            {
                continue;
            }
            else
            {
                var ProductCategory = new ProductCategory();
                ProductCategory.ProductId = ProductId;
                ProductCategory.CategoryId = ids;
                _context.Add(ProductCategory);
                await _context.SaveChangesAsync();
            }
        }
    }
}
