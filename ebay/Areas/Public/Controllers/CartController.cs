using AspNetCoreHero.ToastNotification.Abstractions;
using ebay.Data;
using ebay.Provider.Interface;
using ebay.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ebay.Areas.Public.Controllers;
[Area("Public")]
public class CartController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ICurrentUserProvider _currentUserProvder;
    private readonly INotyfService _notifyService;

    public CartController(ApplicationDbContext context, ICurrentUserProvider currentUserProvider, INotyfService notifyService)
    {
        _context = context;
        _currentUserProvder = currentUserProvider;
        _notifyService = notifyService;
    }
    public async Task<IActionResult> Index(cartVm vm)
    {
        // // need to add redirect to index page through search
        // vm.Data = await _context.Products
        //   .Where(x =>
        //        x.Name.Contains(vm.Name) && vm.CategoryId == x.CategoryId || vm.CategoryId == x.CategoryId && string.IsNullOrEmpty(vm.Name) || vm.CategoryId == null
        //   ).ToListAsync();
        // vm.Categories = await _context.Categories.ToListAsync();
        // get user's id
        vm.User_id = _currentUserProvder.GetCurrentUserId();
        var cart = await _context.Carts.FirstOrDefaultAsync(x=>x.User_id == vm.User_id);
        vm.Cart_id = cart.id;
        vm.CartItemList = await _context.CartItems.Where(x=> x.Cart_id==vm.Cart_id).Include(x=>x.Product).ToListAsync();

        foreach(var cartitem in vm.CartItemList)
        {
            vm.Subtotal = vm.Subtotal + (cartitem.Quantity * cartitem.Product.Price);
        }

        return View(vm);
    }

    public IActionResult Add()
    {
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Minus()
    {
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete()
    {
        return View();
    }
}
