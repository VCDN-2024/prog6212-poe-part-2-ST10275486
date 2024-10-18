using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AfriclaimMVC.Models;

namespace AfriclaimMVC.Controllers
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
                //Please note that the Admins are the Program Coordinator and Academic Manager
                if (model.Email.EndsWith("@IIE.admin.za")) // email domain for admin role 
                {
                    /***************************************************************************************
                     *   Title: Pass UserID with HttpContext.Session to different Controller
                     *    Author: Stackoverflow
                     *    Date: 19 June 2020
                     *    Code version: Version 1
                     *    Availability: https://stackoverflow.com/questions/62474963/pass-userid-with-httpcontext-session-to-different-controller
                     *
                     ***************************************************************************************/
                    HttpContext.Session.SetString("UserRole", "Admin");
                    return RedirectToAction("Index", "Home"); // Redirect to home for admin
                }
                else if (model.Email.EndsWith("@lecturer.co.za")) // email domain for lecturer role
                {
                    HttpContext.Session.SetString("UserRole", "Lecturer");
                    /***************************************************************************************
                     *   Title: Pass UserID with HttpContext.Session to different Controller
                     *    Author: Stackoverflow
                     *    Date: 19 June 2020
                     *    Code version: Version 1
                     *    Availability: https://stackoverflow.com/questions/62474963/pass-userid-with-httpcontext-session-to-different-controller
                     *
                     ***************************************************************************************/
                    return RedirectToAction("Index", "Home"); // Redirect to home for lecturer
                }
                else
                {
                    // Error message for invalid login attempts
       /***************************************************************************************
       *   Title: Pro C 7 with .NET and .NET Core
       *    Author: Andrew Troelsen; Philip Japikse
       *    Date: 2017
       *    Code version: Version 1
       *    Availability: Textbook/Ebook
       *
       ***************************************************************************************/
                    ModelState.AddModelError(string.Empty, "Error, please use the correct log in details and try again.");
                }
            }

            // Return to the login view with errors
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account"); // Redirect to login
        }
    }
}
