using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Transaction
{
    public int TransactionID { get; set; }

    public string? AccountNumber { get; set; }

    public string? Type { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? Date { get; set; }

    public virtual Account? AccountNumberNavigation { get; set; }
}
