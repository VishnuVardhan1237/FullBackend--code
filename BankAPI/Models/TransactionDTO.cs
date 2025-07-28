namespace BankAPI.Models
{


    public class TransactionDTO
    {
        public string Type { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}