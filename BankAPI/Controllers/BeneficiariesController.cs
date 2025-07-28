using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL.Models;
using REPOS;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiariesController : ControllerBase
    {
        private readonly IBeneficiaryRepository _repo;
        public BeneficiariesController(IBeneficiaryRepository repo) => _repo = repo;

        [HttpGet] public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());
        [HttpGet("{id}")] public async Task<IActionResult> Get(int id) => Ok(await _repo.GetByIdAsync(id));
        [HttpPost] public async Task<IActionResult> Post(Beneficiary beneficiary) { await _repo.AddAsync(beneficiary); return Ok(); }
        [HttpDelete("{id}")] public async Task<IActionResult> Delete(int id) { await _repo.DeleteAsync(id); return Ok(); }

    }
}
