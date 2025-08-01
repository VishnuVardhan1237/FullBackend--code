using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOS
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllAsync();
        Task<Account> GetByIdAsync(string accountNumber);
        Task AddAsync(Account account);
        Task UpdateAsync(Account account);
        Task DeleteAsync(string accountNumber);

     Task<Customer> FindCustomerAsync(string email, string customerId, string mobile);



        Task DepositAsync(string accountNumber, decimal amount);
    Task WithdrawAsync(string accountNumber, decimal amount);
    }
}
