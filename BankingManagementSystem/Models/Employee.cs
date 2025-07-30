using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Employee
{
    public int EmployeeID { get; set; }

    public int? UserID { get; set; }

    public string? Name { get; set; }

    public string? Designation { get; set; }

    public string? Password { get; set; }

    public virtual User? User { get; set; }
}
