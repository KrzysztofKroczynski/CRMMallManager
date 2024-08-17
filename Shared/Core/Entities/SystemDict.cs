namespace Shared.Core.Entities;

public sealed class SystemDict
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<SystemAccess> SystemAccesses { get; set; } = new List<SystemAccess>();
}