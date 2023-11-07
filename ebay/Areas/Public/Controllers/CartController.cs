using AspNetCoreHero.ToastNotification.Abstractions;
using ebay.Data;
using ebay.Models;
using ebay.Provider.Interface;
using ebay.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;

namespace ebay.Areas.Public.Controllers;
[Area("Public")]
public class CartController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ICurrentUserProvider _currentUserProvder;
    private readonly INotyfService _notifyService;
    [BindProperty]
    public CheckOutVm CheckOutVm { get; set; }

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
        var cart = await _context.Carts.FirstOrDefaultAsync(x => x.User_id == vm.User_id);
        vm.Cart_id = cart.id;
        vm.CartItemList = await _context.CartItems.Where(x => x.Cart_id == vm.Cart_id).Include(x => x.Product).ToListAsync();

        foreach (var cartitem in vm.CartItemList)
        {
            vm.Subtotal = vm.Subtotal + (cartitem.Quantity * cartitem.Product.Price);
        }

        return View(vm);
    }

    public async Task<IActionResult> Add(int id)
    {
        var cartitem = await _context.CartItems.FirstAsync(x => x.id == id);
        cartitem.Quantity++;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> MinusAsync(int id)
    {
        var cartitem = await _context.CartItems.FirstAsync(x => x.id == id);
        if (cartitem.Quantity <= 1)
        {
            _context.Remove(cartitem);
        }
        else
        {
            cartitem.Quantity--;
        }
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
        var cartitem = await _context.CartItems.FirstAsync(x => x.id == id);
        _context.Remove(cartitem);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> CheckOut(CheckOutVm vm)
    {
        vm.User_id = _currentUserProvder.GetCurrentUserId();
        var cart = await _context.Carts.FirstOrDefaultAsync(x => x.User_id == vm.User_id);
        vm.Cart_id = cart.id;
        vm.CartItemList = await _context.CartItems.Where(x => x.Cart_id == vm.Cart_id).Include(x => x.Product).ToListAsync();
        var address = await _context.Addresses.FirstOrDefaultAsync(x => x.User_id == vm.User_id && x.Is_Default == true);
        if (address != null)
        {
            vm.Address_Line = address.Address_Line;
            vm.Region = address.Region;
            vm.Postal_Code = address.Postal_Code;
            vm.Landmark = address.Landmark;
            vm.Countries = _context.Countries.Where(x => x.id == address.Country_id).ToList();
        }
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == vm.User_id);
        vm.FirstName = user.FirstName;
        vm.LastName = user.LastName;
        vm.PhoneNo = user.PhoneNo;
        vm.Email = user.Email;

        foreach (var cartitem in vm.CartItemList)
        {
            vm.Subtotal = vm.Subtotal + (cartitem.Quantity * cartitem.Product.Price);
        }
        vm.Countries = await _context.Countries.ToListAsync();
        return View(vm);
    }
    [HttpPost]
    public async Task<IActionResult> Checkout(CheckOutVm vm)
    {
        vm.User_id = _currentUserProvder.GetCurrentUserId();
        var addressFrmDb = await _context.Addresses.FirstOrDefaultAsync(x => x.User_id == vm.User_id);
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == vm.User_id);
        user.FirstName = vm.FirstName;
        user.LastName = vm.LastName;
        user.PhoneNo = vm.PhoneNo;
        if (addressFrmDb == null)
        {
            var address = new Address();
            address.Address_Line = vm.Address_Line;
            address.Landmark = vm.Landmark;
            address.Region = vm.Region;
            address.Postal_Code = vm.Postal_Code;
            address.User_id = vm.User_id;
            address.City = vm.City;
            address.Is_Default = true;
            address.Country = await _context.Countries.Where(x => x.id == vm.CountryId).FirstOrDefaultAsync();
            _context.Add(address);
        }
        else
        {
            addressFrmDb.Address_Line = vm.Address_Line;
            addressFrmDb.Landmark = vm.Landmark;
            addressFrmDb.Region = vm.Region;
            addressFrmDb.Postal_Code = vm.Postal_Code;
            addressFrmDb.Country = await _context.Countries.Where(x => x.id == vm.CountryId).FirstOrDefaultAsync();
        }
        var order = new Order()
        {
            User_id = vm.User_id,
            Order_total = vm.Subtotal
        };
        _context.Add(order);
        await _context.SaveChangesAsync();
        vm.CartItemList = await _context.CartItems.Where(x => x.Cart_id == vm.Cart_id).Include(x => x.Product).ToListAsync();
        
        var orderItemsFrmDb = _context.Orders.FirstOrDefault(x=>x.User_id==vm.User_id);

        foreach (var cartList in vm.CartItemList)
        {
            var orderItems = new OrderItems()
            {
                Order_id = orderItemsFrmDb.id,
                Product_id = cartList.Product_id,
                Quantity = cartList.Quantity,
                Price = cartList.Product.Price
            };
            _context.Add(orderItems);
            await _context.SaveChangesAsync();
        }

        // await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index), nameof(Public));
    }
}
