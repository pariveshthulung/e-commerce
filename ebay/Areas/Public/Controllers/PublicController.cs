using System.Transactions;
using AspNetCoreHero.ToastNotification.Abstractions;
using ebay.Data;
using ebay.Models;
using ebay.Provider.Interface;
using ebay.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ebay.Areas.Public.Controllers;
[Area("Public")]

public class PublicController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ICurrentUserProvider _currentUserProvder;
    private readonly INotyfService _notifyService;

    public PublicController(ApplicationDbContext context, ICurrentUserProvider currentUserProvider, INotyfService notifyService)
    {
        _context = context;
        _currentUserProvder = currentUserProvider;
        _notifyService = notifyService;
    }
    [AllowAnonymous]
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
    [AllowAnonymous]
    public async Task<IActionResult> Detail(int id)
    {
        var vm = new cartVm();
        vm.Product = await _context.Products.Where(x => x.id == id).FirstOrDefaultAsync();
        vm.Quantity = 1;
        vm.Product_id = id;
        return View(vm);
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Detail(cartVm vm)
    {
        // using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        // {
            var currentUser = _currentUserProvder.GetCurrentUserId();
            vm.User_id = currentUser;
            var cartExist = _context.Carts.Any(x => x.User_id == vm.User_id);
            if (!cartExist)
            {
                var cart = new Cart();
                cart.User_id = vm.User_id;
                await _context.AddAsync(cart);
                await _context.SaveChangesAsync();
            }

            var cartFrmDb = await _context.Carts.FirstOrDefaultAsync(x => x.User_id == vm.User_id);
            var cartItemExits = await _context.CartItems.FirstOrDefaultAsync(x => x.Cart_id == cartFrmDb.id && x.Product_id == vm.Product_id);
            if (cartItemExits == null)
            {
                var cartitem = new CartItem();
                cartitem.Product_id = vm.Product_id;
                cartitem.Cart_id = cartFrmDb.id;
                cartitem.Quantity = vm.Quantity;
                _context.Add(cartitem);
                await _context.SaveChangesAsync();
                _notifyService.Success("Added to cart");
            }
            else
            {
                cartItemExits.Quantity = cartItemExits.Quantity + vm.Quantity;
                _context.Add(cartItemExits);
                await _context.SaveChangesAsync();
                _notifyService.Success("Added to cart");
            }


        //     tx.Complete();
        // }
        return RedirectToAction(nameof(Index));
    }
}
