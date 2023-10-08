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

namespace ebay.Controllers;
[AllowAnonymous]
public class AuthController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IAuthManager _authManger;
    private readonly INotyfService _notifyService;

    public AuthController(ApplicationDbContext context, IHttpContextAccessor contextAccessor, IAuthManager authManager, INotyfService notyfService)
    {
        _context = context;
        _contextAccessor = contextAccessor;
        _authManger = authManager;
        _notifyService = notyfService;
    }
    public IActionResult LogIn()
    {
        return View();
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
            return RedirectToAction("Index", "Home");
        }
        catch
        {

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
        try
        {
            var userExit = await _context.Users.AnyAsync(x => x.Email == vm.Email);
            if (!userExit)
            {
                using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var Users = new User();
                    Users.FirstName = vm.FirstName;
                    Users.LastName = vm.LastName;
                    Users.Email = vm.Email;
                    Users.PasswordHash = BCrypt.Net.BCrypt.HashPassword(vm.Password);
                    await _context.Users.AddAsync(Users);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("User registered successfully.");
                    tx.Complete();
                }
                return RedirectToAction("LogIn", "Auth");
            }
            _notifyService.Error("User already exits");
            return RedirectToAction("Registration", "Auth");

        }
        catch (Exception e)
        {
            _notifyService.Error("User registration failed." + e.Message);
            return RedirectToAction("Registration", "Auth");

        }
    }
    public async Task<IActionResult> LogOut()
    {
        await _authManger.LogOut();
        return RedirectToAction("LogIn", "Auth");
    }
}
