using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Manager
{
    public int ManagerID { get; set; }

    public int? UserID { get; set; }

    public string? Name { get; set; }

    public string? Department { get; set; }

    public string? Password { get; set; }

    public virtual User? User { get; set; }
}
