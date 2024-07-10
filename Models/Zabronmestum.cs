using System;
using System.Collections.Generic;

namespace Kinoteatr.Models;

public partial class Zabronmestum
{
    public int Id { get; set; }

    public int? Mestaid { get; set; }

    public string? Status { get; set; }

    public virtual Mestum? Mesta { get; set; }
}
