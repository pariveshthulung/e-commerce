using AspNetCoreHero.ToastNotification.Abstractions;
using ebay.Data;
using ebay.Manager.Interface;
using ebay.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ebay.Controllers;
[AllowAnonymous]
public class AuthController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IAuthManager _authManger;
    private readonly INotyfService _notifyService;

    public AuthController(ApplicationDbContext context, IHttpContextAccessor contextAccessor,IAuthManager authManager,INotyfService notyfService)
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
        if(!ModelState.IsValid)
        {
            return View(vm);
        }
        try
        {
            await _authManger.LogIn(vm.Username,vm.Password);
            return RedirectToAction("Index","Home");
        }
        catch
        {
            
            return View(vm);
        }
    }
    public async Task<IActionResult> LogOut()
    {
        await _authManger.LogOut();
        return RedirectToAction("LogIn","Auth");
    }
}
