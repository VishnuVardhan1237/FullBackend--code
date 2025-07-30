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

    public class LoanRepository : ILoanRepository
    {
        private readonly BankingDbContext _context;
        public LoanRepository(BankingDbContext context) => _context = context;

        public async Task<IEnumerable<Loan>> GetAllAsync() => await _context.Loans.ToListAsync();
        public async Task<Loan> GetByIdAsync(int id) => await _context.Loans.FindAsync(id);
        public async Task AddAsync(Loan loan) { _context.Loans.Add(loan); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(Loan loan) { _context.Loans.Update(loan); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(int id)
        {
            var loan = await _context.Loans.FindAsync(id);
            if (loan != null) { _context.Loans.Remove(loan); await _context.SaveChangesAsync(); }
        }
    }
}
