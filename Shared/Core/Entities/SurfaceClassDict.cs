namespace Shared.Core.Entities;

public sealed class SurfaceClassDict
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal MinimalSurface { get; set; }

    public decimal MaximumSurface { get; set; }

    public ICollection<LeaseApplication> LeaseApplications { get; set; } = new List<LeaseApplication>();
}