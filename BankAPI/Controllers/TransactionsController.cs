using BankAPI.Models;
using DAL.Contexts;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REPOS;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionRepository _repo;
        private readonly BankingDbContext _context;
        public TransactionsController(ITransactionRepository repo, BankingDbContext context)
        {
            _repo = repo;
            _context = context;
        }
        [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());
        [HttpGet("{id}")] public async Task<IActionResult> Get(int id) => Ok(await _repo.GetByIdAsync(id));
        [HttpPost] public async Task<IActionResult> Post(Transaction transaction) { await _repo.AddAsync(transaction); return Ok(); }


        [HttpGet("mini-statement/{accountNumber}")]
        public async Task<IActionResult> GetMiniStatement(string accountNumber)
        {
            var transactions = await _repo.GetMiniStatementAsync(accountNumber);
            if (!transactions.Any()) return NotFound("No transactions found.");

            var dto = transactions.Select(t => new TransactionDTO
            {
                Type = t.Type,
                Amount = (decimal)t.Amount,
                Date = (DateTime)t.Date
            }).ToList();

            return Ok(new MiniStatementDTO
            {
                AccountNumber = accountNumber,
                LastFiveTransactions = dto
            });
        }

        [HttpGet("detailed-statement/{accountNumber}")]
        public async Task<IActionResult> GetDetailedStatement(string accountNumber, [FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var transactions = await _repo.GetDetailedStatementAsync(accountNumber, from, to);
            if (!transactions.Any()) return NotFound("No transactions found in the given date range.");

            var dto = transactions.Select(t => new TransactionDTO
            {
                Type = t.Type,
                Amount = (decimal)t.Amount,
                Date = (DateTime)t.Date
            }).ToList();

            return Ok(new DetailedStatementDTO
            {
                AccountNumber = accountNumber,
                FromDate = from,
                ToDate = to,
                Transactions = dto
            });
        }
            [HttpPost("transfer")]
 public async Task<IActionResult> Transfer([FromBody] MoneyTransfer request)
 {
     if (!ModelState.IsValid)
         return BadRequest(ModelState);

     var fromAccount = await _context.Accounts
         .FirstOrDefaultAsync(a => a.AccountNumber == request.FromAccount);
     var toAccount = await _context.Accounts
         .FirstOrDefaultAsync(a => a.AccountNumber == request.ToAccount);

     if (fromAccount == null || toAccount == null)
         return NotFound("One or both accounts not found.");

     if (fromAccount.Balance < request.Amount)
         return BadRequest("Insufficient balance.");

     // Update balances
     fromAccount.Balance -= request.Amount;
     toAccount.Balance += request.Amount;

     // Create and store debit transaction
     var debitTransaction = new Transaction
     {
         AccountNumber = request.FromAccount,
         Type = "Debit",
         Amount = request.Amount,
         Date = DateTime.UtcNow
     };

     // Create and store credit transaction
     var creditTransaction = new Transaction
     {
         AccountNumber = request.ToAccount,
         Type = "Credit",
         Amount = request.Amount,
         Date = DateTime.UtcNow
     };

     _context.Transactions.Add(debitTransaction);
     _context.Transactions.Add(creditTransaction);

     // Save all changes
     await _context.SaveChangesAsync();

     return Ok(new
     {
         Status = "Success",
         From = fromAccount.AccountNumber,
         To = toAccount.AccountNumber,
         Amount = request.Amount,
         Date = DateTime.UtcNow
     });
 }


    }
}
