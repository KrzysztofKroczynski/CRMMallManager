namespace Shared.Core.Entities;

public partial class RetailUnitPurpose
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<RetailUnit> RetailUnits { get; set; } = new List<RetailUnit>();

    public virtual ICollection<LeaseApplication> LeaseApplications { get; set; } = new List<LeaseApplication>();
}