using Microsoft.AspNetCore.Mvc;

namespace AfriclaimMVCST10275486.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // The Home page is publicly accessible
            return View();
        }
    }
}
