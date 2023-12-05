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
        vm.Images = _context.ProductImages.Where(x => x.ProductId == vm.ProductId).ToList();

        vm.ProductId = productId;
        return View(vm);
    }
    [HttpPost]
    [ActionName("Add")]
    public IActionResult Add(ImageVm vm)
    {
        ConfigureImage(vm.ProductId, vm.Files);
        return RedirectToAction(nameof(Index), nameof(Product));
    }

    [HttpPost]
    public IActionResult Delete(int imageId)
    {
        string wwwRootPath = _webHostEnvironment.WebRootPath;

        var deleleRequest = _context.ProductImages.FirstOrDefault(x => x.Id == imageId);
        var oldimagePath = Path.Combine(wwwRootPath,deleleRequest.ImageURl.TrimStart('/'));
        if (System.IO.File.Exists(oldimagePath))
        {
            System.IO.File.Delete(oldimagePath);
        }
        _context.ProductImages.Remove(deleleRequest);
        _context.SaveChanges();
        _notifyService.Success("Image deleted!!!");
        return RedirectToAction(nameof(Add));
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
                    string imageUrl = Path.Combine(@"/Public/images/product" + productId, fileName);
                    string filePath = Path.Combine(wwwRootPath, @"Public/images/product" + productId);
                    string imagePath = Path.Combine(filePath, fileName);

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    // insert image
                    using (var fileStream = new FileStream(imagePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    ProductImages productImages = new()
                    {
                        ImageURl = imageUrl,
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
            _notifyService.Success("Image uploaded.");
            return ("error adding images");
        }
    }
}
