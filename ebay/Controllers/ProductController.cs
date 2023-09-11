using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ebay.Data;
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
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        public async Task<IActionResult> Search(ProductSearch vm)
        {
            vm.Data = await _context.Products
          .Where(x =>
              string.IsNullOrEmpty(vm.Name) || x.Name.Contains(vm.Name)
          ).ToListAsync();
            return View(vm);

  
        }
    }
}

