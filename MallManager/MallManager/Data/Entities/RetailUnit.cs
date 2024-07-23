using System;
using System.Collections.Generic;

namespace MallManager.Data.Entities;

public partial class RetailUnit
{
    public int Id { get; set; }

    public int FloorNumber { get; set; }

    public int LocalNumber { get; set; }

    public decimal LocalSurfaceArea { get; set; }

    public int RetailUnitPurposeId { get; set; }

    public virtual ICollection<Lease> Leases { get; set; } = new List<Lease>();

    public virtual RetailUnitPurpose RetailUnitPurpose { get; set; } = null!;
}
