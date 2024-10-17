using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AfriclaimMVCST10275486.Models;

namespace AfriclaimMVCST10275486.Controllers
{
    public class AccountController : Controller
    {
        
        public IActionResult Login()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                if (model.Email.EndsWith("@IIE.admin.za")) //email domain for admin role
                {
                   
                    HttpContext.Session.SetString("UserRole", "Admin");
                    return RedirectToAction("Index", "Home"); // Redirect to home for admin
                }
                else if (model.Email.EndsWith("@lecturer.co.za")) //email domain for lecturer role
                {
                    
                    HttpContext.Session.SetString("UserRole", "Lecturer");
                    return RedirectToAction("Index", "Home"); // Redirect to home for lecturer
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt."); // Invalid email domain
                }
            }

            return View(model); // Return to the login view with errors
        }

        
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); 
            return RedirectToAction("Login", "Account"); // Redirect to login
        }
    }
}
