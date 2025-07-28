using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL.Models;
using REPOS;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _repo;
        public CustomersController(ICustomerRepository repo) => _repo = repo;

        [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());
        [HttpGet("{id}")] public async Task<IActionResult> Get(int id) => Ok(await _repo.GetByIdAsync(id));
        [HttpPost] public async Task<IActionResult> Post(Customer customer) { await _repo.AddAsync(customer); return Ok(); }
        [HttpPut("{id}")] public async Task<IActionResult> Put(int id, Customer customer) { customer.CustomerId = id; await _repo.UpdateAsync(customer); return Ok(); }
        [HttpDelete("{id}")] public async Task<IActionResult> Delete(int id) { await _repo.DeleteAsync(id); return Ok(); }

    }
}
