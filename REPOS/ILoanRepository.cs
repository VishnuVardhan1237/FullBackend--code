using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOS
{
    public interface ILoanRepository
    {
        Task<IEnumerable<Loan>> GetAllAsync();
        Task<Loan> GetByIdAsync(int id);
        Task AddAsync(Loan loan);
        Task UpdateAsync(Loan loan);
        Task DeleteAsync(int id);
    }
}
