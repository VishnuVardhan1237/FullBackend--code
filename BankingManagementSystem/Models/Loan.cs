using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Loan
{
    public int LoanID { get; set; }

    public int? CustomerID { get; set; }

    public decimal? Amount { get; set; }

    public double? InterestRate { get; set; }

    public int? DurationInMonths { get; set; }

    public string? Status { get; set; }

    public virtual Customer? Customer { get; set; }
}
