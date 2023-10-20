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
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductRepository(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment) : base(context)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
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
        items.Product_image = vm.Image;

        items.Category = _context.Categories.Where(x => x.id == vm.CategoryId).FirstOrDefault();


        await dbset.AddAsync(items);
        //     tx.Complete();
        // }
    }

    public async Task Update(int id, ProductUpdateVm vm)
    {
        var items = await dbset.Where(x => x.id == id)
                    .FirstOrDefaultAsync();
        if (vm.ImageFile != null)
        {
            // state wwwroot folder path
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            // assign new unique name
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.ImageFile.FileName);
            // declare file path where image get stored
            string filePath = Path.Combine(wwwRootPath, @"Public/images/product");
            string imagePath = Path.Combine(filePath, fileName);
            if (!string.IsNullOrEmpty(items.Product_image))
            {
                var oldimagePath = imagePath;
                if (System.IO.File.Exists(oldimagePath))
                {
                    System.IO.File.Delete(oldimagePath);
                }
            }

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await vm.ImageFile.CopyToAsync(fileStream);
            }
            vm.Image = fileName;
            items.Product_image= vm.Image;

        }

        using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            items.Name = vm.Name;
            items.Description = vm.Description;
            items.Price = vm.Price;
            items.Brand = vm.Brand;
            items.Stock = vm.Stock;
            items.Category = await _context.Categories.Where(x => x.id == vm.CategoryId).FirstOrDefaultAsync();

            tx.Complete();
        }

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
        items.Image = obj.Product_image;
        items.CategoryId = obj.CategoryId;
        items.Categories = _context.Categories.ToList();
        return items;
    }

}
