using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL.Models;
using REPOS;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _repo;
        public EmployeesController(IEmployeeRepository repo) => _repo = repo;

        [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());
        [HttpGet("{id}")] public async Task<IActionResult> Get(int id) => Ok(await _repo.GetByIdAsync(id));
        [HttpPost] public async Task<IActionResult> Post(Employee employee) { await _repo.AddAsync(employee); return Ok(); }
        [HttpPut("{id}")] public async Task<IActionResult> Put(int id, Employee employee) { employee.EmployeeId = id; await _repo.UpdateAsync(employee); return Ok(); }
        [HttpDelete("{id}")] public async Task<IActionResult> Delete(int id) { await _repo.DeleteAsync(id); return Ok(); }

    }
}
