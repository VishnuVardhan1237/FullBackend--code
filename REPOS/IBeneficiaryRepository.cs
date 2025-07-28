using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOS
{
    public interface IBeneficiaryRepository
    {
        Task<IEnumerable<Beneficiary>> GetAllAsync();
        Task<Beneficiary> GetByIdAsync(int id);
        Task AddAsync(Beneficiary beneficiary);
        Task DeleteAsync(int id);
    }
}
