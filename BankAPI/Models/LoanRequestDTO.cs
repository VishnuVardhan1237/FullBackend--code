

namespace BankAPI.models
{

    // File: DAL/Models/LoanRequest.cs or DAL/DTOs/LoanRequest.cs
    public class LoanRequestDTO
    {
        public int CustomerID { get; set; }
        public decimal Amount { get; set; }
        public float InterestRate { get; set; }
        public int DurationInMonths { get; set; }
        public string Status { get; set; } = "Pending"; // Optional: default value
    }
}