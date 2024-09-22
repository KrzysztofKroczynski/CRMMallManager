namespace Shared.Core.Entities;

public sealed partial class Appsetting
{
    public int Id { get; set; }

    public string Key { get; set; } = null!;

    public string Value { get; set; } = null!;
}
