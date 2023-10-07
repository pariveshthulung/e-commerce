using System.Collections.Specialized;
using System.Security.Claims;
using AspNetCoreHero.ToastNotification.Abstractions;
using ebay.Data;
using ebay.Entity;
using ebay.Manager.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace ebay.Manager;

public class AuthManager : IAuthManager
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _contextAccessor;
    public INotyfService _notifyService { get; }

    public AuthManager(ApplicationDbContext context, IHttpContextAccessor contextAccessor, INotyfService notifyService)
    {
        _context = context;
        _contextAccessor = contextAccessor;
        _notifyService = notifyService;
    }
    public async Task LogIn(string Username, string Password)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == Username);
            if (user == null)
            {
                _notifyService.Error("Invalid username.");
                throw new Exception("Invalid username");
            }
            if (!BCrypt.Net.BCrypt.Verify(Password, user.PasswordHash))
            {
                _notifyService.Error("Invalid username and password.");
                throw new Exception("Username and password do not match");
            }
            var httpContext = _contextAccessor.HttpContext;
            var claim = new List<Claim>
            {
                new ("Id" , user.Id.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }
        catch
        {
            
        }
    }

    public async Task LogOut()
    {
        await _contextAccessor.HttpContext.SignOutAsync();
    }
}
