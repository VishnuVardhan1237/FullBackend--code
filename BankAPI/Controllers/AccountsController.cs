using DAL.Models;
using REPOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _repo;
        public AccountsController(IAccountRepository repo) => _repo = repo;

        [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());
        [HttpGet("{id}")] public async Task<IActionResult> Get(string id) => Ok(await _repo.GetByIdAsync(id));
        [HttpPost] public async Task<IActionResult> Post(Account account) { await _repo.AddAsync(account); return Ok(); }
        [HttpPut("{id}")] public async Task<IActionResult> Put(string id, Account account) { account.AccountNumber = id; await _repo.UpdateAsync(account); return Ok(); }
        [HttpDelete("{id}")] public async Task<IActionResult> Delete(string id) { await _repo.DeleteAsync(id); return Ok(); }

    }
}
