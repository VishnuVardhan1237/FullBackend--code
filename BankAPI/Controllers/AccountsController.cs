using DAL.Models;
using REPOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankAPI.Models;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _repo;
        public AccountsController(IAccountRepository repo) => _repo = repo;

        [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());
        // [HttpGet("{id}")] public async Task<IActionResult> Get(string id) => Ok(await _repo.GetByIdAsync(id));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var account = await _repo.GetByIdAsync(id);
            if (account == null)
                return NotFound();

            return Ok(account); // ✅ Return the data
        }

        [HttpPost] public async Task<IActionResult> Post(Account account) { await _repo.AddAsync(account); return Ok(); }
        // [HttpPut("{id}")] public async Task<IActionResult> Put(string id, Account account) { account.AccountNumber = id; await _repo.UpdateAsync(account); return Ok(); }
        [HttpDelete("{id}")] public async Task<IActionResult> Delete(string id) { await _repo.DeleteAsync(id); return Ok(); }

        [HttpPut("{id}")]
        // public async Task<IActionResult> Put(string id, Account account)
        // {
        //     account.AccountNumber = id;
        //     await _repo.UpdateAsync(account);
        //     return Ok();
        // }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] DepositRequest request)
        {
            try
            {
                await _repo.DepositAsync(request.AccountNumber, request.Amount);
                return Ok(new { message = "Deposit successful" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] WithdrawRequest request)
        {
            try
            {
                await _repo.WithdrawAsync(request.AccountNumber, request.Amount);
                return Ok(new { message = "Withdraw successful" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpGet("verify-customer")]
public async Task<IActionResult> VerifyCustomer(string gmail, string customerId, string registeredMobile)
{
    if (!int.TryParse(customerId, out int parsedCustomerId))
        return BadRequest("Customer ID must be numeric.");

    var customer = await _repo.FindCustomerAsync(gmail, customerId, registeredMobile);

    if (customer == null)
        return NotFound("No customer found with the provided information.");

    return Ok(new
    {
        Message = "Customer verified successfully.",
        Name = customer.Name
    });
}



    }
}
