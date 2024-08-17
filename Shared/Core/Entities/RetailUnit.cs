namespace Shared.Core.Entities;

public sealed class RetailUnit
{
    public int Id { get; set; }

    public int FloorNumber { get; set; }

    public int LocalNumber { get; set; }

    public decimal LocalSurfaceArea { get; set; }

    public int RetailUnitPurposeId { get; set; }

    public ICollection<Lease> Leases { get; set; } = new List<Lease>();

    public RetailUnitPurpose RetailUnitPurpose { get; set; } = null!;
}