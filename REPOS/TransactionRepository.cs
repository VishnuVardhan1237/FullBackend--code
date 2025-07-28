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
        private readonly BankDbContext _context;
        public TransactionRepository(BankDbContext context) => _context = context;

        public async Task<IEnumerable<Transaction>> GetAllAsync() => await _context.Transactions.ToListAsync();
        public async Task<Transaction> GetByIdAsync(int id) => await _context.Transactions.FindAsync(id);
        public async Task AddAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }
    }
}
