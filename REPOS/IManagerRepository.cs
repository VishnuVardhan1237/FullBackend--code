using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOS
{
    public interface IManagerRepository
    {
        Task<IEnumerable<Manager>> GetAllAsync();
        Task<Manager> GetByIdAsync(int id);
        Task AddAsync(Manager manager);
        Task UpdateAsync(Manager manager);
        Task DeleteAsync(int id);

        // ICustomerRepository.cs
        Task<Manager?> GetByCredentialsAsync(string email, string password);

    }
}
