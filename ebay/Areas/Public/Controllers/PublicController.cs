using ebay.Data;
using ebay.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ebay.Areas.Public.Controllers;
[Area("Public")]
[AllowAnonymous]
public class PublicController : Controller
{
    private readonly ApplicationDbContext _context;

    public PublicController(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> Index(ProductSearchVm vm)
    {
        vm.Data = await _context.Products
          .Where(x =>
               x.Name.Contains(vm.Name) && vm.CategoryId == x.CategoryId || vm.CategoryId == x.CategoryId && string.IsNullOrEmpty(vm.Name) || vm.CategoryId == null
          ).ToListAsync();
        vm.Categories = await _context.Categories.ToListAsync();
        return View(vm);
    }

    public async Task<IActionResult> Cart(ProductSearchVm vm)
    {
        // need to add redirect to index page through search
        vm.Data = await _context.Products
          .Where(x =>
               x.Name.Contains(vm.Name) && vm.CategoryId == x.CategoryId || vm.CategoryId == x.CategoryId && string.IsNullOrEmpty(vm.Name) || vm.CategoryId == null
          ).ToListAsync();
        vm.Categories = await _context.Categories.ToListAsync();
        return View(vm);

    }
    public async Task<IActionResult> Detail(int id)
    {
        var vm = new ProductVm();
        vm.Product = await _context.Products.Where(x => x.id == id).FirstOrDefaultAsync();
        return View(vm);
    }
}
