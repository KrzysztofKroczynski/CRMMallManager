namespace Shared.Core.Entities;

public partial class SignupStatusDict
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<LeaseApplication> LeaseApplications { get; set; } = new List<LeaseApplication>();

    public virtual ICollection<SystemAccess> SystemAccesses { get; set; } = new List<SystemAccess>();
}