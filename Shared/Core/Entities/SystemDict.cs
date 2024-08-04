using System;
using System.Collections.Generic;

namespace Shared.Core.Entities;

public partial class SystemDict
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<SystemAccess> SystemAccesses { get; set; } = new List<SystemAccess>();
}
