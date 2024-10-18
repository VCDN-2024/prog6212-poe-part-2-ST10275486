using Microsoft.AspNetCore.Mvc;
using AfriclaimMVC.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System;

namespace AfriclaimMVC.Controllers
{
    public class ClaimController : Controller
    {
        // Static list to store claims
        public static List<ClaimViewModel> claims { get; } = new List<ClaimViewModel>();

        // Displays the list of claims
        public IActionResult Index()
        {
            return View(claims); // Pass the claims to the view
        }

        // GET: Create a new claim
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create a new claim and handle file upload
        [HttpPost]
        public IActionResult Create(ClaimViewModel model, IFormFile document)
        {
            if (ModelState.IsValid)
            {
                const decimal hourlyRate = 130.00m; // Hourly rate is fixed
                model.TotalSalary = model.Amount * hourlyRate; // Calculate total salary
                model.SubmissionDate = DateTime.Now; // Set the current date and time as the submission date

                // Assign a unique ID to the claim
                model.Id = claims.Any() ? claims.Max(c => c.Id) + 1 : 1;

                // Handle the supporting document upload
                if (document != null && document.Length > 0)
                {
                    try
                    {
                        // Save files to 'wwwroot/uploads'
                        string uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                        if (!Directory.Exists(uploadsPath))
                        {
                            Directory.CreateDirectory(uploadsPath);
                        }

                        var filePath = Path.Combine(uploadsPath, document.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            document.CopyTo(stream);
                        }

                        // Store the path as a relative URL
                        model.DocumentName = "/uploads/" + document.FileName;
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("Document", "There was an error uploading the file. Please try again.");
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    ModelState.AddModelError("Document", "Please upload a valid document.");
                }

                // Add the claim to the list
                claims.Add(model);
                return RedirectToAction("Index"); // Redirect to the claim list view
            }

            // If the model state is invalid, return to the form with errors
            return View(model);
        }
    }
}
