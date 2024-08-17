using Ardalis.SharedKernel;

namespace Shared.Core.Entities;

public partial class RetailUnit : IAggregateRoot
{
    public int Id { get; set; }

    public int FloorNumber { get; set; }

    public int LocalNumber { get; set; }

    public decimal LocalSurfaceArea { get; set; }

    public int RetailUnitPurposeId { get; set; }

    public virtual ICollection<Lease> Leases { get; set; } = new List<Lease>();

    public virtual RetailUnitPurpose RetailUnitPurpose { get; set; } = null!;
}