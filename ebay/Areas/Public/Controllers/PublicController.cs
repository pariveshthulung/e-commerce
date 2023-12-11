using System.Transactions;
using AspNetCoreHero.ToastNotification.Abstractions;
using ebay.Data;
using ebay.Models;
using ebay.Provider.Interface;
using ebay.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


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
        // vm.Data = await _context.Products
        //   .Where(x =>
        //        x.Name.Contains(vm.Name) && vm.CategoryId == x.CategoryId || vm.CategoryId == x.CategoryId && string.IsNullOrEmpty(vm.Name) || vm.CategoryId == null
        //   ).ToListAsync();
        vm.Categories = await _context.Categories.ToListAsync();
        // vm.CategoryName = await _context.ProductCategories.ToListAsync();
        vm.Data = await _context.Products.Where(
            x => (vm.CategoryId == null || x.ProductCategories.Any(i => i.CategoryId == vm.CategoryId))
            && (string.IsNullOrEmpty(vm.Name) || x.Name.ToLower().Contains(vm.Name.ToLower()))
            ).ToListAsync();
        if (_currentUserProvder.IsLoggedIn())
        {
            var userId = _currentUserProvder.GetCurrentUserId();
            var cartFrmDb = _context.Carts.FirstOrDefault(x => x.User_id == userId);
            if (cartFrmDb != null)
            {
                vm.CartCount = (int)_context.CartItems.Where(x => x.Cart_id == cartFrmDb.id).LongCount();
            }
        }
        return View(vm);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Detail(int id)
    {
        var vm = new cartVm();
        vm.Product = await _context.Products.Where(x => x.id == id).FirstOrDefaultAsync();
        vm.ProductImages = _context.ProductImages.Where(x => x.ProductId == id).ToList();
        vm.Quantity = 1;
        vm.Product_id = id;
        vm.Reviews = _context.Reviews.Where(x => x.Product_id == id).ToList();
        var list = _context.Reviews.Where(x => x.Product_id == id).Select(x => x.RatingValue).ToList();
        if (!list.IsNullOrEmpty())
        {

            vm.AverageReview = list.Average();
        }
        else{
            vm.AverageReview = 0;
        }

        return View(vm);
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Detail(cartVm vm)
    {
        using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            // get user's id
            vm.User_id = _currentUserProvder.GetCurrentUserId();
            // check existing user's cart
            var cartExist = _context.Carts.Any(x => x.User_id == vm.User_id);
            if (!cartExist)
            {
                // if user's cart doesn't exist create new one
                var cart = new Cart();
                cart.User_id = vm.User_id;
                await _context.AddAsync(cart);
                await _context.SaveChangesAsync();
            }
            // check existing item in cart
            var cartFrmDb = await _context.Carts.FirstOrDefaultAsync(x => x.User_id == vm.User_id);
            var cartItemExits = await _context.CartItems.FirstOrDefaultAsync(x => x.Cart_id == cartFrmDb.id && x.Product_id == vm.Product_id);
            if (cartItemExits == null)
            {
                // create new item in cart
                var cartitem = new CartItem();
                cartitem.Product_id = vm.Product_id;
                cartitem.Cart_id = cartFrmDb.id;
                cartitem.Quantity = vm.Quantity;
                // cartFrmDb.CartCount = _context.CartItems.Where(x => x.Cart_id == cartFrmDb.id).Count();
                _context.Add(cartitem);
                await _context.SaveChangesAsync();
                _notifyService.Success("Added to cart");
            }
            else
            {
                // update existing item in cart
                cartItemExits.Quantity = cartItemExits.Quantity + vm.Quantity;
                await _context.SaveChangesAsync();
                _notifyService.Success("Added to cart");
            }
            tx.Complete();
        }
        return RedirectToAction(nameof(Index));
    }

    [AllowAnonymous]
    public IActionResult CustomerReview(ReviewVm vm,int ProductId)
    {
        vm.Product_id = ProductId;
        vm.ReviewExist = _context.Reviews.Where(x=>x.Product_id==ProductId).Include(x=>x.User).ToList();
        return View(vm);
    }
}
