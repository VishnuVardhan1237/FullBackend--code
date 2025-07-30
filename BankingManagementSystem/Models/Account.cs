using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Account
{
    public string AccountNumber { get; set; } = null!;

    public int? CustomerID { get; set; }

    public string? AccountType { get; set; }

    public decimal? Balance { get; set; }

    public bool? IsLocked { get; set; }

    public virtual ICollection<Beneficiary> Beneficiaries { get; set; } = new List<Beneficiary>();

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
