using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using AspNetCoreHero.ToastNotification.Abstractions;
using ebay.Data;
using ebay.Models;
using ebay.Repository;
using ebay.Repository.IRepository;
using ebay.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ebay.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ProductController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;

        public INotyfService _notifyService { get; }

        public ProductController(
            IUnitOfWork unitOfWork,
            INotyfService notifyService,
            ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _notifyService = notifyService;
            _context = context;
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
            // vm.Categories = await _unitOfWork.CategoryRepo.GetAll();
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Add(ProductAddVm vm)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    // adding transactioScope for data integrity
                    using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        await _unitOfWork.ProductRepo.AddAsync(vm);
                        await _unitOfWork.Save();
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

        public IActionResult Edit(int id)
        {
            try
            {
                var items = _unitOfWork.ProductRepo.UpdateDisplay(id);
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
                if (ModelState.IsValid)
                {
                    using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        await _unitOfWork.ProductRepo.Update(id, vm);
                        await _unitOfWork.Save();
                        _notifyService.Success("Product updated successfully.");
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
                    // var res = await _context.Products.Where(x => x.id == id).FirstOrDefaultAsync();
                    var res = await _unitOfWork.ProductRepo.Get(x => x.id == id);

                    _unitOfWork.ProductRepo.Remove(res);
                    await _unitOfWork.Save();
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

