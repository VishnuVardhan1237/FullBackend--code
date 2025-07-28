using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOS
{

    public class AccountRepository : IAccountRepository
    {
        private readonly BankDbContext _context;
        public AccountRepository(BankDbContext context) => _context = context;

        public async Task<IEnumerable<Account>> GetAllAsync() => await _context.Accounts.ToListAsync();
        public async Task<Account> GetByIdAsync(string accNo) => await _context.Accounts.FindAsync(accNo);
        public async Task AddAsync(Account account) { _context.Accounts.Add(account); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(Account account) { _context.Accounts.Update(account); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(string accNo)
        {
            var acc = await _context.Accounts.FindAsync(accNo);
            if (acc != null) { _context.Accounts.Remove(acc); await _context.SaveChangesAsync(); }
        }
    }
}
