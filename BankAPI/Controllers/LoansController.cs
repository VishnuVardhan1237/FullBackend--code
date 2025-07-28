using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL.Models;
using REPOS;

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
        [HttpPost] public async Task<IActionResult> Post(Loan loan) { await _repo.AddAsync(loan); return Ok(); }
        [HttpPut("{id}")] public async Task<IActionResult> Put(int id, Loan loan) { loan.LoanId = id; await _repo.UpdateAsync(loan); return Ok(); }
        [HttpDelete("{id}")] public async Task<IActionResult> Delete(int id) { await _repo.DeleteAsync(id); return Ok(); }

    }
}
