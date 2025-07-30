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
    public class BeneficiaryRepository : IBeneficiaryRepository
    {
        private readonly BankingDbContext _context;
        public BeneficiaryRepository(BankingDbContext context) => _context = context;

        public async Task<IEnumerable<Beneficiary>> GetAllAsync() => await _context.Beneficiaries.ToListAsync();
        public async Task<Beneficiary?> GetByIdAsync(int id) => await _context.Beneficiaries.FindAsync(id);
        public async Task AddAsync(Beneficiary beneficiary)
        {
            _context.Beneficiaries.Add(beneficiary);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var bene = await _context.Beneficiaries.FindAsync(id);
            if (bene != null) { _context.Beneficiaries.Remove(bene); await _context.SaveChangesAsync(); }
        }
    }
}
