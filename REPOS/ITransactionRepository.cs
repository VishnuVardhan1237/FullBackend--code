using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace REPOS
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllAsync();
        Task<Transaction> GetByIdAsync(int transactionId);
        Task AddAsync(Transaction transaction);

        Task<IEnumerable<Transaction>> GetMiniStatementAsync(string accountNumber);
        Task<IEnumerable<Transaction>> GetDetailedStatementAsync(string accountNumber, DateTime from, DateTime to);
    }
}
