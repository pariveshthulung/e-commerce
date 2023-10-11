using System.Linq.Expressions;
using System.Transactions;
using ebay.Data;
using ebay.Models;
using ebay.Repository.IRepository;
using ebay.ViewModel;

namespace ebay.Repository;

public class ProductRepository : Repository<Product> , IProductRepository
{
    private readonly ApplicationDbContext _context;
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task AddAsync(ProductAddVm vm)
    {
         //adding transactioScope for data integrity
                    using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {

                        var items = new Product();
                        items.Name = vm.Name;
                        items.Description = vm.Description;
                        items.Price = vm.Price;
                        items.Brand = vm.Brand;
                        items.Color = vm.Color;
                        items.Quantity = vm.Quantity;

                        items.Category = await _context.Categories.Where(x => x.id == vm.CategoryId).FirstOrDefaultAsync();


                        _context.Products.Add(items);
                        await _context.SaveChangesAsync();
                        _notifyService.Success("Product added successfully.");
                        tx.Complete();
                    }
    }

    public void Update(ProductUpdateVm vm)
    {
        throw new NotImplementedException();
    }
}
