using Microsoft.AspNetCore.Mvc;
using AfriclaimMVC.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;

namespace AfriclaimMVC.Controllers
{
    public class ClaimController : Controller
    {
        public static List<ClaimViewModel> claims { get; } = new List<ClaimViewModel>();

        public IActionResult Index()
        {
            return View(claims); // Pass the claims to the view
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ClaimViewModel model, IFormFile document)
        {
            if (ModelState.IsValid)
            {
                const decimal hourlyRate = 130.00m; // set hourly rate
                model.TotalSalary = model.Amount * hourlyRate; // Calculate total salary
                model.SubmissionDate = DateTime.Now; // Set the current date and time as the submission date

                 // Unique Id to the claim
                model.Id = claims.Any() ? claims.Max(c => c.Id) + 1 : 1;

                // Saves the supporting document
                if (document != null && document.Length > 0)
                {
                    string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "path_to_save_files");

                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    var filePath = Path.Combine(directoryPath, document.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        document.CopyTo(stream);
                    }
                    model.DocumentName = document.FileName;
                }

                // Add the claim to the list
                claims.Add(model);
                return RedirectToAction("Index");
            }

            // If there are validation errors, show them
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }

            return View(model);
        }
    }
}
