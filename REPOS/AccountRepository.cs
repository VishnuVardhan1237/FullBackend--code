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

    public class AccountRepository : IAccountRepository
    {
        private readonly BankingDbContext _context;
        public AccountRepository(BankingDbContext context) => _context = context;

        public async Task<IEnumerable<Account>> GetAllAsync() => await _context.Accounts.ToListAsync();
        public async Task<Account?> GetByIdAsync(string accNo) => await _context.Accounts.FindAsync(accNo);
        public async Task AddAsync(Account account) { _context.Accounts.Add(account); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(Account account) { _context.Accounts.Update(account); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(string accNo)
        {
            var acc = await _context.Accounts.FindAsync(accNo);
            if (acc != null) { _context.Accounts.Remove(acc); await _context.SaveChangesAsync(); }
        }


        public async Task<Customer> FindCustomerAsync(string email, string customerId, string mobile)
        {
            email = email?.Trim();
            mobile = mobile?.Trim();

            Console.WriteLine($"Email: {email}, CustomerId: {customerId}, Mobile: {mobile}");

            if (!int.TryParse(customerId, out int parsedCustomerId))
            {
                Console.WriteLine("Invalid customer ID format.");
                return null;
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Email == email && c.UserID == parsedCustomerId && c.Phone == mobile);

            if (customer == null)
                Console.WriteLine("No customer matched all criteria.");
            else
                Console.WriteLine($"Customer found: {customer.Name}");

            return customer;
        }





        public async Task DepositAsync(string accountNumber, decimal amount)
        {
            var account = await _context.Accounts.FindAsync(accountNumber);
            if (account == null || account.IsLocked == true) throw new Exception("Account not valid");

            account.Balance += amount;

            _context.Transactions.Add(new Transaction
            {
                AccountNumber = accountNumber,
                Type = "Deposit",
                Amount = amount,
                Date = DateTime.Now
            });

            await _context.SaveChangesAsync();
        }

public async Task WithdrawAsync(string accountNumber, decimal amount)
{
    var account = await _context.Accounts.FindAsync(accountNumber);
    if (account == null || account.IsLocked==true) throw new Exception("Account not valid");

    if (account.Balance < amount) throw new Exception("Insufficient funds");

    account.Balance -= amount;

    _context.Transactions.Add(new Transaction
    {
        AccountNumber = accountNumber,
        Type = "Withdraw",
        Amount = amount,
        Date = DateTime.Now
    });

    await _context.SaveChangesAsync();
}

    }
}
