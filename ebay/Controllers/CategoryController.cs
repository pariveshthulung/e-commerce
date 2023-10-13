using System.Transactions;
using AspNetCoreHero.ToastNotification.Abstractions;
using ebay.Data;
using ebay.Models;
using ebay.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ebay.Controllers;

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
        vm.Cate = await _context.Categories.ToListAsync();
        return View(vm);
    }

    public IActionResult Add()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Upsert(int? id, CategoryVm vm)
    {
        try
        {

            using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (id != null)
                {
                    var items = await _context.Categories.FirstOrDefaultAsync(x => x.id == id);
                    items.Name = vm.Name;
                    await _context.Categories.AddAsync(items);
                    _notifyService.Success("Category updated successfully");
                    await _context.SaveChangesAsync();

                }
                var dataExist = await _context.Categories.AnyAsync(x => x.Name.ToLower() == vm.Name.ToLower());
                if (!dataExist)
                {
                    var items = new Category();
                    items.Name = vm.Name;
                    await _context.Categories.AddAsync(items);
                    _notifyService.Success("Category added successfully");
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _notifyService.Error("Category exist already!!!");
                    return RedirectToAction("Index", "Category");
                }
                tx.Complete();
                return RedirectToAction("Index", "Category");
            }
        }
        catch (Exception e)
        {
            _notifyService.Error("operation fail!!!");
            throw new Exception(e.Message);

        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        var data = await _context.Categories.FirstOrDefaultAsync(x=>x.id==id);
        _context.Categories.Remove(data);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index", "Category");
    }

}
