using Microsoft.AspNetCore.Mvc;

namespace AfriclaimMVC.Controllers
{
    public class InvoiceController : Controller
    {
      
        public IActionResult Index()
        {
            return View(); 
        }
    }
}
