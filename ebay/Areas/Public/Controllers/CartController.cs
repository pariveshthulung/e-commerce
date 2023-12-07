using System.Transactions;
using AspNetCoreHero.ToastNotification.Abstractions;
using ebay.Constants;
using ebay.Data;
using ebay.Models;
using ebay.Provider.Interface;
using ebay.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using Stripe;
using Stripe.Checkout;

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
        // try
        // {
        //     using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        //     {
        // tx.Complete();
        //     }
        // }
        // catch(Exception e)
        // {
        //     return Content(e.Message);
        // }

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
    [HttpPost]
    [ActionName("Index")]
    public IActionResult IndexPost(cartVm vm)
    {
        try
        {
            using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var cartFalse = _context.CartItems.Where(x => x.Cart_id == vm.Cart_id).ToList();
                foreach (var item in cartFalse)
                {
                    item.Checked = false;
                    _context.SaveChanges();
                }

                var cartItemFrmDb = _context.CartItems.Where(x => x.Cart_id == vm.Cart_id & (vm.Checked.Contains(x.Product_id))).ToList();
                foreach (var cartitem in cartItemFrmDb)
                {
                    cartitem.Checked = true;
                    _context.SaveChanges();
                }
                tx.Complete();
                return RedirectToAction(nameof(CheckOut));
            }
        }
        catch (Exception e)
        {
            return Content(e.Message);
        }

    }

    public async Task<IActionResult> Add(int id)
    {
        try
        {
            using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var cartitem = await _context.CartItems.FirstAsync(x => x.id == id);
                cartitem.Quantity++;
                await _context.SaveChangesAsync();
                tx.Complete();
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception e)
        {
            return Content(e.Message);
        }
    }

    public async Task<IActionResult> MinusAsync(int id)
    {
        try
        {
            using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var cartitem = await _context.CartItems.FirstAsync(x => x.id == id);
                if (cartitem.Quantity <= 1)
                {
                    _notifyService.Success("Item deleted !!!");
                    _context.Remove(cartitem);
                }
                else
                {
                    cartitem.Quantity--;
                }
                await _context.SaveChangesAsync();
                tx.Complete();
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception e)
        {
            return Content(e.Message);
        }

    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var cartitem = await _context.CartItems.FirstAsync(x => x.id == id);
                _context.Remove(cartitem);
                await _context.SaveChangesAsync();
                _notifyService.Success("Item deleted !!!");
                tx.Complete();
                return RedirectToAction(nameof(Index));

            }
        }
        catch (Exception e)
        {
            return Content(e.Message);
        }
    }
    public async Task<IActionResult> CheckOut(CheckOutVm vm)
    {
        try
        {
            using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                vm.User_id = _currentUserProvder.GetCurrentUserId();
                var cart = await _context.Carts.FirstOrDefaultAsync(x => x.User_id == vm.User_id);
                vm.Cart_id = cart.id;
                vm.CartItemList = await _context.CartItems.Where(x => x.Cart_id == vm.Cart_id & x.Checked == true).Include(x => x.Product).ToListAsync();
                var address = await _context.Addresses.FirstOrDefaultAsync(x => x.User_id == vm.User_id && x.Is_Default == true);
                if (address != null)
                {
                    vm.Address_Line = address.Address_Line;
                    vm.Region = address.Region;
                    vm.Postal_Code = address.Postal_Code;
                    vm.Landmark = address.Landmark;
                    vm.City = address.City;
                    vm.Countries = _context.Countries.Where(x => x.id == address.Country_id).ToList();
                    vm.CountryId = address.Country_id;
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
                tx.Complete();
                return View(vm);
            }
        }
        catch (Exception e)
        {
            return Content(e.Message);
        }

    }
    [HttpPost]
    public async Task<IActionResult> Checkout(CheckOutVm vm)
    {
        try
        {
            using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                vm.User_id = _currentUserProvder.GetCurrentUserId();
                var addressFrmDb = await _context.Addresses.FirstOrDefaultAsync(x => x.User_id == vm.User_id);
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == vm.User_id);
                user.FirstName = vm.FirstName;
                user.LastName = vm.LastName;
                user.PhoneNo = vm.PhoneNo;
                if (addressFrmDb == null)
                {
                    var address = new Models.Address();
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
                    addressFrmDb.City = vm.City;
                    addressFrmDb.Country = await _context.Countries.Where(x => x.id == vm.CountryId).FirstOrDefaultAsync();
                }
                 vm.CartItemList = await _context.CartItems.Where(x => x.Cart_id == vm.Cart_id & x.Checked==true).Include(x => x.Product).ToListAsync();

                // var orderItemsFrmDb = _context.Orders.FirstOrDefault(x => x.User_id == vm.User_id);
                foreach (var cartitem in vm.CartItemList)
                {
                    vm.Subtotal = vm.Subtotal + (cartitem.Quantity * cartitem.Product.Price);
                }
                var order = new Order()
                {
                    User_id = vm.User_id,
                    Order_total = vm.Subtotal
                };
                _context.Add(order);
                await _context.SaveChangesAsync();
               
                foreach (var cartList in vm.CartItemList)
                {
                    var orderItems = new OrderItems()
                    {
                        Order_id = order.id,
                        Product_id = cartList.Product_id,
                        Quantity = cartList.Quantity,
                        Price = cartList.Product.Price
                    };
                    _context.Add(orderItems);
                    await _context.SaveChangesAsync();
                }
                var domain = "http://localhost:5191/";
                var options = new SessionCreateOptions
                {
                    SuccessUrl = domain + $"Public/Cart/Thankyou?orderId={order.id}&cartId={vm.Cart_id}",
                    CancelUrl = domain + "Public/Cart/Index",
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                };
                foreach (var item in vm.CartItemList)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.Product.Price * 100),
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Name
                            }
                        },
                        Quantity = item.Quantity
                    };
                    options.LineItems.Add(sessionLineItem);
                }
                var service = new SessionService();
                Session session = service.Create(options);
                UpdateStripePaymentID(order.id, session.Id, session.PaymentIntentId);
                _context.SaveChanges();
                Response.Headers.Add("Location", session.Url);
                tx.Complete();
                return new StatusCodeResult(303);
            }
        }
        catch (Exception e)
        {
            return Content(e.Message);
        }
    }
    
    public IActionResult Thankyou(int orderId, int cartId)
    {
        try
        {
            using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var orderfromDb = _context.Orders.Where(x => x.id == orderId).FirstOrDefault();
                var orderItemFromDb = _context.OrderItems.Where(x => x.Order_id == orderId).ToList();
                foreach (var item in orderItemFromDb)
                {
                    if (item.Order_status == PaymentStatusConstant.Pending)
                    {
                        var service = new SessionService();
                        Session session = service.Get(orderfromDb.SessionId);
                        if (session.PaymentStatus.ToLower() == "paid")
                        {
                            UpdateStripePaymentID(orderId, session.Id, session.PaymentIntentId);
                            UpdateStatus(item.id, OrderStatusConstants.Approved, PaymentStatusConstant.Approved);
                            _context.SaveChanges();
                        }
                    }
                }
                List<CartItem> cartItems = _context.CartItems.Where(x => x.Cart_id == cartId & x.Checked==true).ToList();
                _context.RemoveRange(cartItems);
                _context.SaveChanges();
                tx.Complete();
                return View();
            }
        }
        catch (Exception e)
        {
            return Content(e.Message);
        }

    }
    [HttpPost]
    public IActionResult CancelOrder(int orderId, int orderItemId)
    {
        try
        {
            using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var orderFromDb = _context.Orders.FirstOrDefault(x => x.id == orderId);
                var orderItemsFrmDb = _context.OrderItems.Where(x => x.id == orderItemId).FirstOrDefault();
                if (orderItemsFrmDb.PaymentStatus == "Approved")
                {
                    var options = new RefundCreateOptions
                    {
                        PaymentIntent = orderFromDb.PaymentIntentId,
                        Amount = (long?)orderItemsFrmDb.Price * 100 * orderItemsFrmDb.Quantity
                    };
                    var service = new RefundService();
                    service.Create(options);
                    UpdateStatus(orderItemsFrmDb.id, OrderStatusConstants.Cancelled, PaymentStatusConstant.Refund);
                }
                _context.SaveChanges();
                _notifyService.Success("Order cancelLed Sucessfully!!!");

                tx.Complete();
                return RedirectToAction("Myorder", "Profile");
            }
        }
        catch (Exception e)
        {
            return Content(e.Message);
        }

    }

    public void UpdateStatus(int itemId, string orderStatus, string? paymentStatus)
    {
        var orderItemFromDb = _context.OrderItems.FirstOrDefault(x => x.id == itemId);
        if (orderItemFromDb != null)
        {
            orderItemFromDb.Order_status = orderStatus;
            if (!string.IsNullOrEmpty(paymentStatus))
            {
                orderItemFromDb.PaymentStatus = paymentStatus;
            }
        }
    }
    public void UpdateStripePaymentID(int id, string sessionId, string PaymentIntentId)
    {
        var orderFromDb = _context.Orders.FirstOrDefault(x => x.id == id);
        orderFromDb.SessionId = sessionId;
        orderFromDb.PaymentIntentId = PaymentIntentId;
        orderFromDb.PaymentDate = DateTime.Now;
    }
}
