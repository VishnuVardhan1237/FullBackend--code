namespace BankAPI.Models
{
    public class DetailedStatementDTO
    {
        public string AccountNumber { get; set; } = string.Empty;
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public List<TransactionDTO> Transactions { get; set; } = new();
    }
}
