using Microsoft.AspNetCore.Mvc;
using AfriclaimMVC.Models;
using System.Linq;
using System.Collections.Generic;

namespace AfriclaimMVC.Controllers
{
    public class SubmissionController : Controller
    {
        // Reference to the ClaimController's claims list
        private static List<ClaimViewModel> claims => ClaimController.claims;

        // Display the list of claims on the submissions page
        public IActionResult Index()
        {
            return View(claims); // Pass the claims to the view
        }

        // Action to update the claim status (Approve/Reject)
        [HttpPost]
        public IActionResult UpdateClaimStatus(int id, string status)
        {
            // Find the claim by its unique Id
            var claim = claims.FirstOrDefault(c => c.Id == id);
            if (claim != null)
            {
                // Update the status of the claim (Approved or Rejected)
                claim.Status = status;

            }
            else
            {
                // If no matching claim was found, log an error or return an error message
               
                Console.WriteLine($"No claim found with Id {id}");
            }

            // After updating the claim status, refresh the submissions page
            return RedirectToAction("Index");
        }
    }
}
