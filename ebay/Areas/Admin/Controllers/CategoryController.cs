using System.Transactions;
using AspNetCoreHero.ToastNotification.Abstractions;
using ebay.Constants;
using ebay.Data;
using ebay.Entity;
using ebay.Models;
using ebay.Provider.Interface;
using ebay.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ebay.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly INotyfService _notifyService;

    public CategoryController(ApplicationDbContext context, INotyfService notifyService)
    {
        _context = context;
        _notifyService = notifyService;

    }
    public async Task<IActionResult> Index(CategoryVm vm)
    {
        vm.Display = await _context.Categories.ToListAsync();
        return View(vm);
    }

    public IActionResult Add()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Add(CategoryAddVm vm)
    {
        try
        {
            if (ModelState.IsValid)
            {
                using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var items = new Category();
                    items.Name = vm.Name;
                    await _context.Categories.AddAsync(items);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Category added successfully.");
                    tx.Complete();
                }

                return RedirectToAction("Index", "Category");
            }
            _notifyService.Error("Invalid data.");
            return View(vm);
        }
        catch (Exception e)
        {
            _notifyService.Error(e.Message);
            return RedirectToAction("Index", "Category");
        }
    }
    public async Task<IActionResult> Update(int id)
    {
        var vm = new CategoryAddVm();
        var dataSelected = await _context.Categories.FirstOrDefaultAsync(x => x.id == id);
        vm.Name = dataSelected.Name;
        return View(vm);
    }
    [HttpPost]
    public async Task<IActionResult> Update(int id, CategoryAddVm vm)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                _notifyService.Error("Invalid data selected.");
                return View(vm);
            }
            var selectedData = await _context.Categories.FirstOrDefaultAsync(x => x.id == id);
            using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                selectedData.Name = vm.Name;
                await _context.SaveChangesAsync();
                _notifyService.Success("Category updated successfully.");
                tx.Complete();
            }
            return RedirectToAction("Index", "Category");

        }
        catch (Exception e)
        {
            _notifyService.Error(e.Message);
            return RedirectToAction("Index", "Category");
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        var data = await _context.Categories.FirstOrDefaultAsync(x => x.id == id);
        _context.Categories.Remove(data);
        await _context.SaveChangesAsync();
        _notifyService.Success("Category deleted successfully.");
        return RedirectToAction("Index", "Category");
    }

}
