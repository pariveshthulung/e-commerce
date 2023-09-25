﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
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
            ProductAddVm vm = new(){
            CategoryList= _context.Categories.Select(x=> new SelectListItem{
                        Text=x.Name,
                        Value=x.id.ToString()
                    })
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Add(ProductAddVm vm)
        {
            try
            {

                if(ModelState.IsValid){
                //adding transactioScope for data integrity
                using (var tx= new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    
                    var items = new Product();
                    items.Name = vm.Name;
                    items.Description = vm.Description;
                    items.Price = vm.Price;
                    items.Brand = vm.Brand;
                    items.Color = vm.Color;
                    items.Quantity = vm.Quantity;
                    items.CategoryId = vm.CategoryId;
                    

                    _context.Products.Add(items);
                    await _context.SaveChangesAsync();

                    tx.Complete();
                }

                return RedirectToAction("Index");
                }
                else{
                     
                        vm.CategoryList= _context.Categories.Select(x=> new SelectListItem{
                        Text=x.Name,
                        Value=x.id.ToString()
                        });
                        return View(vm);
                    
                }

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
        public async Task<IActionResult> Edit(int id ,ProductionUpdateVm vm)
        {
            try
            {
                var items = await _context.Products.Where(x => x.id == id)
                    .FirstOrDefaultAsync();
                if (ModelState.IsValid)
                {
                    
                    using (var tx= new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        items.Name = vm.Name;
                        items.Description = vm.Description;
                        items.Price = vm.Price;
                        items.Brand = vm.Brand;
                        items.Color = vm.Color;
                        items.Quantity = vm.Quantity;
                        await _context.SaveChangesAsync();
                        
                        tx.Complete(); 
                    }

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
                using(var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var res = await _context.Products.Where(x=> x.id == id).FirstOrDefaultAsync();
                    
                    _context.Products.Remove(res);
                    _context.SaveChanges();
                    tx.Complete();
                }
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                return RedirectToAction("Index");
            }
        }

    }
}

