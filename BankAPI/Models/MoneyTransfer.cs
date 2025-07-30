

using System.ComponentModel.DataAnnotations;

namespace BankAPI.Models
{
    public class MoneyTransfer
    {
        [Required]
        public string FromAccount { get; set; }

        [Required]
        public string ToAccount { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

    }
}