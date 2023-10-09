using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ebay.Controllers;

[AllowAnonymous]
public class PublicController :Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
