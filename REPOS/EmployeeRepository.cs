using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOS
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly BankDbContext _context;
        public EmployeeRepository(BankDbContext context) => _context = context;

        public async Task<IEnumerable<Employee>> GetAllAsync() => await _context.Employees.ToListAsync();
        public async Task<Employee?> GetByIdAsync(int id) => await _context.Employees.FindAsync(id);
        public async Task AddAsync(Employee employee) { _context.Employees.Add(employee); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(Employee employee) { _context.Employees.Update(employee); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp != null) { _context.Employees.Remove(emp); await _context.SaveChangesAsync(); }
        }
    }

}
