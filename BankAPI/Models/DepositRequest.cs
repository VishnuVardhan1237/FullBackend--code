

namespace BankAPI.Models
{
    public class DepositRequest
    {
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}