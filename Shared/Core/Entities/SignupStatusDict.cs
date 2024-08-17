namespace Shared.Core.Entities;

public sealed class SignupStatusDict
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<LeaseApplication> LeaseApplications { get; set; } = new List<LeaseApplication>();

    public ICollection<SystemAccess> SystemAccesses { get; set; } = new List<SystemAccess>();
}