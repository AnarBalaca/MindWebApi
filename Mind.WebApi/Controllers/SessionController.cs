using Microsoft.AspNetCore.Mvc;

namespace Mind.WebApi.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
