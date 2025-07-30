using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL.Models;
using REPOS;

using BankAPI.models;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly ILoanRepository _repo;
        public LoansController(ILoanRepository repo) => _repo = repo;

        [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());
        [HttpGet("{id}")] public async Task<IActionResult> Get(int id) => Ok(await _repo.GetByIdAsync(id));
        // [HttpPost] public async Task<IActionResult> Post(Loan loan) { await _repo.AddAsync(loan); return Ok(); }

        [HttpPost]
public async Task<IActionResult> Post([FromBody] LoanRequestDTO request)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    var loan = new Loan
    {
        CustomerID = request.CustomerID,
        Amount = request.Amount,
        InterestRate = request.InterestRate,
        DurationInMonths = request.DurationInMonths,
        Status = request.Status ?? "Pending" // fallback default
    };

    await _repo.AddAsync(loan);
    return Ok(new { message = "Loan request submitted successfully", loanId = loan.LoanID });
}

        [HttpPut("{id}")] public async Task<IActionResult> Put(int id, Loan loan) { loan.LoanID = id; await _repo.UpdateAsync(loan); return Ok(); }
        [HttpDelete("{id}")] public async Task<IActionResult> Delete(int id) { await _repo.DeleteAsync(id); return Ok(); }

    }
}
