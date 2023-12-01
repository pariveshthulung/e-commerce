using AspNetCoreHero.ToastNotification.Abstractions;
using ebay.Data;
using ebay.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ebay.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles ="Admin")]

public class OrderManagementController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly INotyfService _notifyService;

    public OrderManagementController(ApplicationDbContext context , INotyfService notifyService)
    {
        _context = context;
        _notifyService = notifyService;
    }
    public IActionResult Index()
    {
        // List<Order> OrderList = _context.Orders.ToList();  

        return View();
    }
    #region API CALLS
    [HttpGet]
    public IActionResult GetAll()
    {
        List<Order> OrderList = _context.Orders.Include(x=>x.User).ToList();  
        return Json(new {data= OrderList});
    }
    #endregion
}
