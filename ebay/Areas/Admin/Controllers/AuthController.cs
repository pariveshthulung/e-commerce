using System.Transactions;
using AspNetCoreHero.ToastNotification.Abstractions;
using ebay.Constants;
using ebay.Data;
using ebay.Entity;
using ebay.Manager.Interface;
using ebay.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ebay.Areas.Admin.Controllers;
[AllowAnonymous]
[Area("Admin")]

public class AuthController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IAuthManager _authManger;
    private readonly INotyfService _notifyService;

    public AuthController(ApplicationDbContext context, IAuthManager authManager, INotyfService notyfService)
    {
        _context = context;
        _authManger = authManager;
        _notifyService = notyfService;
    }
    public IActionResult LogIn()
    {
        var vm = new AuthSignInVm();
        return View(vm);
    }
    [HttpPost]
    public async Task<IActionResult> LogIn(AuthSignInVm vm)
    {
        if (!ModelState.IsValid)
        {
            return View(vm);
        }
        try
        {
            await _authManger.LogIn(vm.Username, vm.Password);
            return RedirectToAction("Index", "Public", new { area = "Public" });
        }
        catch (Exception e)
        {
            vm.ErrorMessage = e.Message;
            return View(vm);

        }
    }
    public IActionResult Registration()
    { 
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Registration(AuthRegisterVm vm)
    {
        if(!ModelState.IsValid)
        {
            return View(vm);
        }
        try
        {
            var userExit = await _context.Users.AnyAsync(x => x.Email == vm.Email);
            if (!userExit)
            {
                await _authManger.Register(vm);
                return RedirectToAction("LogIn", "Auth");
            }
            _notifyService.Error("User already exits with same Email !!!");
            return View(vm);

        }
        catch (Exception e)
        {
            _notifyService.Error("User registration failed." + e.Message);
            return View(vm);

        }
    }
    public async Task<IActionResult> LogOut()
    {
        await _authManger.LogOut();
        return RedirectToAction("Index", "Public", new { area = "Public" });
    }
}
