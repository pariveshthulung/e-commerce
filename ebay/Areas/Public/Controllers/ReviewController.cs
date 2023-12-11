using System;
using AspNetCoreHero.ToastNotification.Abstractions;
using ebay.Data;
using ebay.Models;
using ebay.Provider.Interface;
using ebay.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ebay.Areas.Public.Controllers;

[Area("Public")]

public class ReviewController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ICurrentUserProvider _currentUserProvder;
    private readonly INotyfService _notifyService;

    public ReviewController(ApplicationDbContext context, ICurrentUserProvider currentUserProvider, INotyfService notifyService)
    {
        _context = context;
        _currentUserProvder = currentUserProvider;
        _notifyService = notifyService;
    }

    public IActionResult Index(ReviewVm vm)
    {
        vm.User_id = _currentUserProvder.GetCurrentUserId();
        vm.ReviewExist = _context.Reviews.Where(x => x.User_id == vm.User_id).ToList();
        vm.OrderFrmDb = _context.Orders.Where(x => x.User_id == vm.User_id).ToList();
        vm.OrderItemFrmDb = _context.OrderItems.Include(x => x.Product).Include(x => x.Order).ToList();
        return View(vm);
    }
    public IActionResult AddReview(ReviewVm vm, int ProductId)
    {
        vm.User_id = _currentUserProvder.GetCurrentUserId();
        vm.Product_id = ProductId;
        vm.Product = _context.Products.Where(x => x.id == ProductId).FirstOrDefault();
        return View(vm);
    }
    [HttpPost]
    public IActionResult AddReview(ReviewVm vm)
    {
        var Review = new Review()
        {
            Product_id = vm.Product_id,
            User_id = vm.User_id,
            Comment = vm.Comment,
            RatingValue = vm.RatingValue,
            ReviewDate = DateTime.Now,
        };
        _context.Reviews.Add(Review);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    public IActionResult EditReview(ReviewVm vm, int ProductId)
    {
        vm.User_id = _currentUserProvder.GetCurrentUserId();
        vm.Product = _context.Products.Where(x => x.id == ProductId).FirstOrDefault();

        var Review = _context.Reviews.FirstOrDefault(x => x.User_id == vm.User_id & x.Product_id == ProductId);
        if (Review != null)
        {
            vm.Product_id = ProductId;
            vm.Comment = Review.Comment;
            vm.RatingValue = Review.RatingValue;
        }

        return View(vm);
    }
    [HttpPost]
    public IActionResult EditReview(ReviewVm vm)
    {
        vm.Product = _context.Products.Where(x => x.id == vm.Product_id).FirstOrDefault();
        var Review = _context.Reviews.FirstOrDefault(x => x.User_id == vm.User_id & x.Product_id == vm.Product_id);
        Review.RatingValue = vm.RatingValue;
        Review.Comment = vm.Comment;
        Review.ReviewDate = DateTime.Now;
        _context.SaveChanges();



        return RedirectToAction(nameof(Index));
    }

}
