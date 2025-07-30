﻿using DAL.Contexts;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOS
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BankingDbContext _context;
        public CustomerRepository(BankingDbContext context) => _context = context;

        public async Task<IEnumerable<Customer>> GetAllAsync() => await _context.Customers.ToListAsync();
        public async Task<Customer?> GetByIdAsync(int id) => await _context.Customers.FindAsync(id);
        public async Task AddAsync(Customer customer) { _context.Customers.Add(customer); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(Customer customer) { _context.Customers.Update(customer); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null) { _context.Customers.Remove(customer); await _context.SaveChangesAsync(); }
        }

        // CustomerRepository.cs
public async Task<Customer?> GetByCredentialsAsync(string name, string password)
{
    return await _context.Customers.FirstOrDefaultAsync(
        c => c.Name == name && c.Password == password);
}

    }
}
