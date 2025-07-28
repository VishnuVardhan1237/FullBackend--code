using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Beneficiary
{
    public int BeneficiaryId { get; set; }

    public int? CustomerId { get; set; }

    public string? BeneficiaryAccountNumber { get; set; }

    public virtual Account? BeneficiaryAccountNumberNavigation { get; set; }

    public virtual Customer? Customer { get; set; }
}
