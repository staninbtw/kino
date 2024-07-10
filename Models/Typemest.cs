using System;
using System.Collections.Generic;

namespace Kinoteatr.Models;

public partial class Typemest
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public int? Price { get; set; }

    public virtual ICollection<Mestum> Mesta { get; } = new List<Mestum>();
}
