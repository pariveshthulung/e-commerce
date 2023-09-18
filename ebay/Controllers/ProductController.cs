using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ebay.Data;
using ebay.Models;
using ebay.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ebay.Controllers
{
    public class ProductController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index(ProductSearchVm vm)
        {
            vm.Data = await _context.Products
          .Where(x =>
              string.IsNullOrEmpty(vm.Name) ||  x.Name.Contains(vm.Name)
          ).ToListAsync();
            return View(vm);


        }
        
        public IActionResult Add()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(ProductAddVm vm)
        {
            try
            {
                var items = new Product();
                items.Name = vm.Name;
                items.Description = vm.Description;
                items.Price = vm.Price;
                items.Brand = vm.Brand;
                items.Color = vm.Color;
                items.Quantity = vm.Quantity;

                _context.Products.Add(items);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");

            }
            catch(Exception)
            {
                return RedirectToAction("Index");
            }

            
        }

        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                var item = await _context.Products.Where(x => x.id == id)
                .FirstOrDefaultAsync();

                if (id == 0 || id == null)
                {
                    throw new Exception("Item not found");
                }

                return View(item);
            }
            catch(Exception)
            {
                return RedirectToAction("Index");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product items)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Products.Update(items);
                    await _context.SaveChangesAsync();
                    TempData["success"] = "Product updated successfully";
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception)
            {
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
                var res = await _context.Products.Where(x=> x.id == id).FirstOrDefaultAsync();
                _context.Products.Remove(res);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                return RedirectToAction("Index");
            }
        }
        


    }
}

