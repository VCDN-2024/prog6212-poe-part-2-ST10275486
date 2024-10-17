﻿namespace AfriclaimMVCST10275486.Models
{
    public class ClaimViewModel
    {
        public int Id { get; set; } // Unique identifier for the claim.
        public string Name { get; set; } // First name of the person making the claim.
        public string Surname { get; set; } //Surname of the person making the claim
        public int Amount { get; set; } // The number of hours or amount being claimed.
        public string Module { get; set; } // The module related to the claim
        public decimal TotalSalary { get; set; } // The total salary calculated based on the claim amount.
        public DateTime SubmissionDate { get; set; } // Date when the claim was submitted.
        public string? DocumentName { get; set; } //Name of the document uploaded with the claim
        public string Status { get; set; } = "Pending";  // Status of the claim
    }
}
