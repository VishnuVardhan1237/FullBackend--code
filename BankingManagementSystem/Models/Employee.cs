using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public int? UserId { get; set; }

    public string? Name { get; set; }

    public string? Designation { get; set; }

    public virtual User? User { get; set; }
}
