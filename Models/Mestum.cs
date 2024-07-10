using System;
using System.Collections.Generic;

namespace Kinoteatr.Models;

public partial class Mestum
{
    public int Id { get; set; }

    public int? Number { get; set; }

    public int? Typeid { get; set; }

    public string? Status { get; set; }

    public virtual Typemest? Type { get; set; }

    public virtual ICollection<Zabronmestum> Zabronmesta { get; } = new List<Zabronmestum>();
}
