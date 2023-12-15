using AspNetCoreHero.ToastNotification.Abstractions;
using ebay.Constants;
using ebay.Data;
using ebay.Models;
using ebay.Provider.Interface;
using ebay.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace ebay.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles ="Admin")]

public class OrderManagementController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly INotyfService _notifyService;
    private readonly ICurrentUserProvider _currentUserProvder;

    public OrderManagementController(ApplicationDbContext context , INotyfService notifyService, ICurrentUserProvider currentUserProvider)
    {
        _context = context;
        _notifyService = notifyService;
        _currentUserProvder = currentUserProvider;

    }
    public IActionResult Index()
    {
        List<OrderItems> OrderList = _context.OrderItems.Include(x=>x.Order.User).Include(x=>x.Product).ToList();  

        // List<Order> OrderList = _context.Orders.ToList();  

        return View(OrderList);
    }
    [HttpPost]
    public IActionResult Index(int itemid,string orderstatus)
    {
        UpdateStatus(itemid,orderstatus,null);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Manage(OrderMangementVm vm)
    {
        vm.UserId = _currentUserProvder.GetCurrentUserId(); 
        return View(vm);
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
            if(orderStatus==OrderStatusConstants.Shipped)
            {
                var Product = _context.Products.FirstOrDefault(x=>x.id ==orderItemFromDb.Product_id);
                Product.Stock = Product.Stock - orderItemFromDb.Quantity;
            }
            if(orderItemFromDb.Order_status==(OrderStatusConstants.Shipped) || orderItemFromDb.Order_status==(OrderStatusConstants.Delivered))
            {
                if(orderStatus==OrderStatusConstants.Cancelled)
                {
                    var Product = _context.Products.FirstOrDefault(x=>x.id ==orderItemFromDb.Product_id);
                    Product.Stock = Product.Stock - orderItemFromDb.Quantity;
                }
            }
        }
        
        _context.SaveChanges();
    }

    




    // #region API CALLS
    // [HttpGet]
    // public IActionResult GetAll()
    // {
    //     List<OrderItems> OrderList = _context.OrderItems.Include(x=>x.Order.User).ToList();  
    //     return Json(new {data= OrderList});
    // }
    // #endregion
}
