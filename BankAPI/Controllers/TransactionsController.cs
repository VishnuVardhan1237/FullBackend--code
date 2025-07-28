using BankAPI.Models;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REPOS;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionRepository _repo;
        public TransactionsController(ITransactionRepository repo) => _repo = repo;

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


    }
}
