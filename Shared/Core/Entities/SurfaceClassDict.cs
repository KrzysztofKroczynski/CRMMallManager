namespace Shared.Core.Entities;

public partial class SurfaceClassDict
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal MinimalSurface { get; set; }

    public decimal MaximumSurface { get; set; }

    public virtual ICollection<LeaseApplication> LeaseApplications { get; set; } = new List<LeaseApplication>();
}