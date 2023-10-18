using System.Linq.Expressions;
using System.Transactions;
using ebay.Data;
using ebay.Models;
using ebay.Repository.IRepository;
using ebay.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ebay.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly ApplicationDbContext _context;
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task AddAsync(ProductAddVm vm)
    {
        //adding transactioScope for data integrity
        // using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        // {

        var items = new Product();
        items.Name = vm.Name;
        items.Description = vm.Description;
        items.Price = vm.Price;
        items.Brand = vm.Brand;
        items.Stock = vm.Stock;
        items.Product_image = vm.Product_image;

        items.Category = _context.Categories.Where(x => x.id == vm.CategoryId).FirstOrDefault();


        await dbset.AddAsync(items);
        //     tx.Complete();
        // }
    }

    public async Task Update(int id, ProductUpdateVm vm)
    {
        var items = await dbset.Where(x => x.id == id)
                    .FirstOrDefaultAsync();
        // using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        // {
        items.Name = vm.Name;
        items.Description = vm.Description;
        items.Price = vm.Price;
        items.Brand = vm.Brand;
        items.Stock = vm.Stock;
        items.Product_image = vm.Product_image;
        items.Category = await _context.Categories.Where(x => x.id == vm.CategoryId).FirstOrDefaultAsync();

        //     tx.Complete();
        // }

    }

    public ProductUpdateVm UpdateDisplay(int? id)
    {
        if (id == 0 || id == null)
        {
            throw new Exception("No data found");
        }
        var obj = dbset.Where(x => x.id == id)
               .FirstOrDefault();
        var items = new ProductUpdateVm();

        items.Name = obj.Name;
        items.Description = obj.Description;
        items.Price = obj.Price;
        items.Brand = obj.Brand;
        items.Stock = obj.Stock;
        items.Brand = obj.Brand;
        items.Product_image = obj.Product_image;
        items.CategoryId = obj.CategoryId;
        items.Categories = _context.Categories.ToList();
        return items;
    }

}
