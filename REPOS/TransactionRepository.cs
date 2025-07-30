using DAL.Contexts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOS
{

    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankingDbContext _context;
        public TransactionRepository(BankingDbContext context) => _context = context;

        public async Task<IEnumerable<Transaction>> GetAllAsync() => await _context.Transactions.ToListAsync();
        public async Task<Transaction> GetByIdAsync(int id) => await _context.Transactions.FindAsync(id);
        public async Task AddAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }


        public  async Task<IEnumerable<Transaction>> GetMiniStatementAsync(string accountNumber)
        {
            return await _context.Transactions
                .Where(t => t.AccountNumber == accountNumber)
                .OrderByDescending(t => t.Date)
                .Take(5)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetDetailedStatementAsync(string accountNumber, DateTime from, DateTime to)
        {
            return await _context.Transactions
                .Where(t => t.AccountNumber == accountNumber && t.Date >= from && t.Date <= to)
                .OrderBy(t => t.Date)
                .ToListAsync();
        }

    }
}
