using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using AspNetCoreHero.ToastNotification.Abstractions;
using ebay.Data;
using ebay.Models;
using ebay.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ebay.Controllers
{
    public class ProductController : Controller
    {

        private readonly ApplicationDbContext _context;
        public INotyfService _notifyService { get; }

        public ProductController(ApplicationDbContext context, INotyfService notifyService)
        {
            _context = context;
            _notifyService = notifyService;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index(ProductSearchVm vm)
        {
            vm.Data = await _context.Products
          .Where(x =>
              string.IsNullOrEmpty(vm.Name) || x.Name.Contains(vm.Name)
          ).Include(x => x.Category).ToListAsync();

            return View(vm);


        }

        public async Task<IActionResult> Add()
        {
            var vm = new ProductAddVm();
            vm.Categories = await _context.Categories.ToListAsync();

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Add(ProductAddVm vm)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    //adding transactioScope for data integrity
                    using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {

                        var items = new Product();
                        items.Name = vm.Name;
                        items.Description = vm.Description;
                        items.Price = vm.Price;
                        items.Brand = vm.Brand;
                        items.Color = vm.Color;
                        items.Quantity = vm.Quantity;

                        items.Category = await _context.Categories.Where(x => x.id == vm.CategoryId).FirstOrDefaultAsync();


                        _context.Products.Add(items);
                        await _context.SaveChangesAsync();
                        _notifyService.Success("Product added successfully.");
                        tx.Complete();
                    }

                    return RedirectToAction("Index");
                }
                else
                {

                    vm.Categories = await _context.Categories.ToListAsync();
                    return View(vm);

                }

            }
            catch (Exception)
            {
                _notifyService.Error("Operation failed.");

                return RedirectToAction("Index");
            }


        }

        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                var vm = await _context.Products.Where(x => x.id == id)
                .FirstOrDefaultAsync();
                var items = new ProductUpdateVm();

                items.Name = vm.Name;
                items.Description = vm.Description;
                items.Price = vm.Price;
                items.Brand = vm.Brand;
                items.Color = vm.Color;
                items.Quantity = vm.Quantity;
                items.CategoryId = vm.CategoryId;
                items.Categories = await _context.Categories.ToListAsync();


                if (id == 0 || id == null)
                {
                    _notifyService.Error("No data found.");
                }
                return View(items);
            }
            catch (Exception)
            {
                _notifyService.Error("Operation failed.");

                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductUpdateVm vm)
        {
            try
            {
                var items = await _context.Products.Where(x => x.id == id)
                    .FirstOrDefaultAsync();
                if (ModelState.IsValid)
                {

                    using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        items.Name = vm.Name;
                        items.Description = vm.Description;
                        items.Price = vm.Price;
                        items.Brand = vm.Brand;
                        items.Color = vm.Color;
                        items.Quantity = vm.Quantity;
                        items.Category = await _context.Categories.Where(x => x.id == vm.CategoryId).FirstOrDefaultAsync();

                        await _context.SaveChangesAsync();
                        _notifyService.Success("Product edited successfully.");
                        tx.Complete();
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    vm.Categories = await _context.Categories.ToListAsync();
                    return View(vm);
                }
            }
            catch (Exception)
            {
                _notifyService.Error("Operation failed.");

                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var res = await _context.Products.Where(x => x.id == id).FirstOrDefaultAsync();

                    _context.Products.Remove(res);
                    _context.SaveChanges();
                    _notifyService.Success("Product deleted successfully.");
                    tx.Complete();
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                _notifyService.Error("Operation failed.");
                return RedirectToAction("Index");
            }
        }

    }
}

