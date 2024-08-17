namespace Shared.Core.Entities;

public sealed class RetailUnitPurpose
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public ICollection<RetailUnit> RetailUnits { get; set; } = new List<RetailUnit>();

    public ICollection<LeaseApplication> LeaseApplications { get; set; } = new List<LeaseApplication>();
}