using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Manager
{
    public int ManagerId { get; set; }

    public int? UserId { get; set; }

    public string? Name { get; set; }

    public string? Department { get; set; }

    public virtual User? User { get; set; }
}
