using BankAPI.Models;
using DAL.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace BankAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly BankingDbContext _context;
        public AuthController(IConfiguration config, BankingDbContext context)
        {
            _config = config;
            _context = context;
             }

        // [AllowAnonymous] // 🔓 Allows login endpoint to be accessed without token
        // [HttpPost("login")]
        // public IActionResult Login([FromBody] LoginRequestDTO login)
        // {

        //     var token = GenerateJwtToken(login.Username, "Manager");

        //     return Ok(new LoginResponseDTO
        //     {
        //         Username = login.Username,
        //         Role = "Manager",
        //         Token = token
        //     });
        // }

        [AllowAnonymous]
[HttpPost("login")]
public IActionResult Login([FromBody] LoginRequestDTO login)
{
    if (string.IsNullOrWhiteSpace(login.Username) || string.IsNullOrWhiteSpace(login.Password))
        return BadRequest("Username and Password are required");

    // Try Customer
    var customer = _context.Customers.FirstOrDefault(c => c.Name == login.Username && c.Password == login.Password);
    if (customer != null)
    {
        var token = GenerateJwtToken(customer.Name, "Customer");
        return Ok(new LoginResponseDTO { Username = customer.Name, Role = "Customer", Token = token });
    }

    // Try Employee
    var employee = _context.Employees.FirstOrDefault(e => e.Name == login.Username && e.Password == login.Password);
    if (employee != null)
    {
        var token = GenerateJwtToken(employee.Name, "Employee");
        return Ok(new LoginResponseDTO { Username = employee.Name, Role = "Employee", Token = token });
    }

    // Try Manager
    var manager = _context.Managers.FirstOrDefault(m => m.Name == login.Username && m.Password == login.Password);
    if (manager != null)
    {
        var token = GenerateJwtToken(manager.Name, "Manager");
        return Ok(new LoginResponseDTO { Username = manager.Name, Role = "Manager", Token = token });
    }

    return Unauthorized("Invalid credentials");
}


        private string GenerateJwtToken(string username, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role),
                
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"] ?? "60")),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}