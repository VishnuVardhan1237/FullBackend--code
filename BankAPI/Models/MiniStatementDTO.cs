namespace BankAPI.Models
{
    public class MiniStatementDTO
    {
        public string AccountNumber { get; set; } = string.Empty;
        public List<TransactionDTO> LastFiveTransactions { get; set; } = new();
    }
}
