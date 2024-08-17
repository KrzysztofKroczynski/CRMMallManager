namespace Shared.Core.Entities;

public sealed class Manager
{
    public string Id { get; set; } = null!;

    public AspNetUser IdNavigation { get; set; } = null!;

    public ICollection<SystemAccess> SystemAccesses { get; set; } = new List<SystemAccess>();
}