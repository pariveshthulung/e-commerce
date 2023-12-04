using System.Drawing;
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

    // define method to add and update image
    public string ConfigureImage(IFormFile image, string productUrl)
    {
        if (image != null)
        {
            // state wwwroot folder path
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            // assign new unique name
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            // declare file path where image get stored
            string filePath = Path.Combine(wwwRootPath, @"Public/images/product");
            string imagePath = Path.Combine(filePath, fileName);
            // update image
            if (!string.IsNullOrEmpty(productUrl))
            {
                var oldimagePath = imagePath;
                if (System.IO.File.Exists(oldimagePath))
                {
                    System.IO.File.Delete(oldimagePath);
                }
            }
            // insert image
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }
            // return image's name
            return fileName;
        }
        return ("error adding images");
    }

    public  int AddAsync(ProductAddVm vm)
    {
        //adding transactioScope for data integrity
        using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var items = new Product();
            items.Name = vm.Name;
            items.Description = vm.Description;
            items.Price = vm.Price;
            items.Brand = vm.Brand;
            items.Stock = vm.Stock;
            //  call method to insert image(pass items.product_image to check existing image)
            vm.Image = ConfigureImage(vm.ImageFile, items.Product_image);
            items.Product_image = vm.Image;
            // items.Category = _context.Categories.Where(x => x.id == vm.CategoryId).FirstOrDefault();
            //  dbset.AddAsync(items);
             _context.Products.Add(items);
             _context.SaveChanges();
            tx.Complete();
            return (items.id);
        }
    }

    public async Task Update(int id, ProductUpdateVm vm)
    {
        var items = await dbset.Where(x => x.id == id)
                    .FirstOrDefaultAsync();

        using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            // update image if user add new one (pass items.product_image to check existing image)
            if (vm.ImageFile != null)
            {
                vm.Image = ConfigureImage(vm.ImageFile, items.Product_image);
                items.Product_image = vm.Image;
            }
            items.Name = vm.Name;
            items.Description = vm.Description;
            items.Price = vm.Price;
            items.Brand = vm.Brand;
            items.Stock = vm.Stock;
            // items.Category = await _context.Categories.Where(x => x.id == vm.CategoryId).FirstOrDefaultAsync();

            tx.Complete();
        }

    }
    // display selected items in edit page
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
        items.CategoryIds = _context.ProductCategories.Where(x=>x.ProductId==id).Select(x=>x.CategoryId).ToList();
        items.Categories = _context.Categories.ToList();
        return items;
    }

}
