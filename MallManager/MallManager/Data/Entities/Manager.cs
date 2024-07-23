using System;
using System.Collections.Generic;

namespace MallManager.Data.Entities;

public partial class Manager
{
    public string Id { get; set; } = null!;

    public virtual AspNetUser IdNavigation { get; set; } = null!;

    public virtual ICollection<SystemAccess> SystemAccesses { get; set; } = new List<SystemAccess>();
}
