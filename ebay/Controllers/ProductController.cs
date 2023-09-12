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
        public async Task<IActionResult> Index(ProductSearch vm)
        {
            vm.Data = await _context.Products
          .Where(x =>
              string.IsNullOrEmpty(vm.Name) || x.Name.Contains(vm.Name)
          ).ToListAsync();
            return View(vm);


        }
        
        public IActionResult Add()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(ProductAdd vm)
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
            catch
            {
                return RedirectToAction("Index");
            }

            
        }

        public IActionResult Edit()
        {
            return View();
        }


    }
}

