using System.Transactions;
using AspNetCoreHero.ToastNotification.Abstractions;
using ebay.Data;
using ebay.Models;
using ebay.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ebay.Areas.Admin.Controllers;

[Area("Admin")]
public class ImageController : Controller
{

    private readonly ApplicationDbContext _context;
    private readonly INotyfService _notifyService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ImageController(ApplicationDbContext context, INotyfService notifyService, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _notifyService = notifyService;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Add(int productId, ImageVm vm)
    {
        vm.ProductId = productId;
        return View(vm);
    }
    [HttpPost]
    [ActionName("Add")]
    public IActionResult Add(ImageVm vm)
    {
        ConfigureImage(vm.ProductId, vm.Files);
        return View();
    }
    public string ConfigureImage(int productId, List<IFormFile> files)
    {
        using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            if (files != null)
            {
                // state wwwroot folder path
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                // assign new unique name
                foreach (IFormFile file in files)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    // declare file path where image get stored
                    string filePath = Path.Combine(wwwRootPath, @"Public/images/product" + productId);
                    string imagePath = Path.Combine(filePath, fileName);

                    // if (!Directory.Exists(imagePath))
                    // {
                    //     Directory.CreateDirectory(imagePath);
                    // }
                    // insert image
                    using (var fileStream = new FileStream(imagePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    ProductImages productImages = new()
                    {
                        ImageURl = fileName,
                        ProductId = productId
                    };
                    _context.ProductImages.Add(productImages);
                    _context.SaveChanges();

                }
                // // update image
                // if (!string.IsNullOrEmpty(productUrl))
                // {
                //     var oldimagePath = imagePath;
                //     if (System.IO.File.Exists(oldimagePath))
                //     {
                //         System.IO.File.Delete(oldimagePath);
                //     }
                // }

                // // return image's name
                // return fileName;
            }
            tx.Complete();
            return ("error adding images");
        }
    }
}
