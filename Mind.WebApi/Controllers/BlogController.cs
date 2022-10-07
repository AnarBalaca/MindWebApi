using Microsoft.AspNetCore.Mvc;

namespace Mind.WebApi.Controllers;

public class BlogController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
