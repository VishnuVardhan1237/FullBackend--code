using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public int? UserId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<Beneficiary> Beneficiaries { get; set; } = new List<Beneficiary>();

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();

    public virtual User? User { get; set; }
}
