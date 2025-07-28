using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOS
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly BankDbContext _context;
        public ManagerRepository(BankDbContext context) => _context = context;

        public async Task<IEnumerable<Manager>> GetAllAsync() => await _context.Managers.ToListAsync();
        public async Task<Manager> GetByIdAsync(int id) => await _context.Managers.FindAsync(id);
        public async Task AddAsync(Manager manager) { _context.Managers.Add(manager); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(Manager manager) { _context.Managers.Update(manager); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(int id)
        {
            var mgr = await _context.Managers.FindAsync(id);
            if (mgr != null) { _context.Managers.Remove(mgr); await _context.SaveChangesAsync(); }
        }
    }
}
