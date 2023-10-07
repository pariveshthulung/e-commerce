using System.Transactions;
using ebay.Constants;
using ebay.Data;
using ebay.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ebay.Controllers;
[AllowAnonymous]
public class SeedingController : Controller
{
    private readonly ApplicationDbContext _context;

    public SeedingController(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IActionResult> SeedAdmin()
    {
        try
        {
            var adminExit = await _context.Users.AnyAsync(x => x.UserType == UserTypeConstants.Admin);
            if (!adminExit)
            {
                using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled); 
                var admin = new User()
                {
                    Email="admin@admin.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin"),
                    UserType =UserTypeConstants.Admin,
                    FirstName="admin",
                    LastName="admin"
                };
                await _context.AddAsync(admin);
                await _context.SaveChangesAsync();
                tx.Complete();
                return Content("Added admin!!!");
            }
            return Content("User exit");
        }
        catch(Exception e)
        {
            return Content(e.Message);
        }
    }
}
