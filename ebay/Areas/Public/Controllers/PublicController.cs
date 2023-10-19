using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ebay.Areas.Public.Controllers;
[Area("Public")]
[AllowAnonymous]
public class PublicController :Controller
{
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Cart()
    {
        return View();
    }
}
