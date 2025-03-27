using Microsoft.AspNetCore.Mvc;

namespace ServerSide.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
