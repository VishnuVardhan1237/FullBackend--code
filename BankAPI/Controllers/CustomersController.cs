using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL.Models;
using REPOS;
using BankAPI.Models;

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
        // [HttpPost] public async Task<IActionResult> Post(Customer customer) { await _repo.AddAsync(customer); return Ok(); }

        [HttpPost]
public async Task<IActionResult> Post([FromBody] CustomerCreateDto dto)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    var customer = new Customer
    {
        Name = dto.Name,
        Email = dto.Email,
        Phone = dto.Phone,
        Address = dto.Address,
        Password = dto.Password, // You may want to hash this
    };

    await _repo.AddAsync(customer);
    return Ok(new { message = "Customer created successfully" });
}

        [HttpPut("{id}")] public async Task<IActionResult> Put(int id, Customer customer) { customer.CustomerID = id; await _repo.UpdateAsync(customer); return Ok(); }
        [HttpDelete("{id}")] public async Task<IActionResult> Delete(int id) { await _repo.DeleteAsync(id); return Ok(); }

    }
}
